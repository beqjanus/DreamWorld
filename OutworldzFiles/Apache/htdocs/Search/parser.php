<?php
// runs parse right now

require( "flog.php" );
include("databaseinfo.php");

//Supress all Warnings/Errors
//error_reporting(0);

$now = time();

// Attempt to connect to the database
 try {
   $db = new PDO("mysql:host=$DB_HOST;port=$DB_PORT;dbname=$DB_NAME", $DB_USER, $DB_PASSWORD);
   $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_SILENT);
   $db1 = new PDO("mysql:host=$DB_HOST;port=$DB_PORT;dbname=$DB_NAME", $DB_USER, $DB_PASSWORD);
   $db1->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_SILENT);
 }
catch(PDOException $e)
{
  echo "Error connecting to the search database\n";
  file_put_contents('../../../PHPLog.log', $e->getMessage() . "\n-----\n", FILE_APPEND);
  exit;
}


function GetURL($host, $port, $url)
{
    $url = "http://$host:$port/$url";

    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, $url);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
    curl_setopt($ch, CURLOPT_CONNECTTIMEOUT, 5);
    curl_setopt($ch, CURLOPT_TIMEOUT, 5);

    $data = curl_exec($ch);
    if (curl_errno($ch) == 0)
    {
        curl_close($ch);
        return $data;
    }

    curl_close($ch);
    return "";
}
function Delete ($gateway)  {
    
    $query = $GLOBALS['db1']->prepare("DELETE FROM objects  WHERE gateway = ?");
    $query->execute( array($gateway) );
    $query = $GLOBALS['db1']->prepare("DELETE FROM allparcels  WHERE gateway = ?");
    $query->execute( array($gateway) );
    $query = $GLOBALS['db1']->prepare("DELETE FROM parcels  WHERE gateway = ?");
    $query->execute( array($gateway) );
    $query = $GLOBALS['db1']->prepare("DELETE FROM parcelsales  WHERE gateway = ?");
    $query->execute( array($gateway) );
    $query = $GLOBALS['db1']->prepare("DELETE FROM popularplaces  WHERE gateway = ?");
    $query->execute( array($gateway) );
    $query = $GLOBALS['db1']->prepare("DELETE FROM regions  WHERE gateway = ?");
    $query->execute( array($gateway) );
}

function CheckHost($gateway, $hostname, $port)
{
    global $db, $now;
    
    $xml = GetURL($hostname, $port, "collector/?method=collector");
    if ($xml == "") {//No data was retrieved? (CURL may have timed out)
        echo " - failed\n";
        $failcounter = 'failcounter + 1';
        Delete($gateway);
    } else {
        echo " - success\n";
        $failcounter = "0";
    }
        

    //Update nextcheck to be 10 minutes from now. The current OS instance
    //won't be checked again until at least this much time has gone by.
    $next = $now + 600;

    $query = $db->prepare("UPDATE hostsregister SET nextcheck = ?," .
                          " checked = checked + 1, failcounter = $failcounter" .
                          " WHERE host = ? AND port = ?");
    $query->execute( array($next, $hostname, $port) );

    if ($xml != "") {
        
        parse($gateway, $hostname, $port, $xml);
    }
}

function parse($gateway,$hostname, $port, $xml)
{
    global $db, $now;

    ///////////////////////////////////////////////////////////////////////
    //
    // Search engine sim scanner
    //

    //
    // Load XML doc from URL
    //
    $objDOM = new DOMDocument();
    $objDOM->resolveExternals = false;

    //Don't try and parse if XML is invalid or we got an HTML 404 error.
    if ($objDOM->loadXML($xml) == False) {
        echo "No XML\n";
        Delete($gateway);
        return;
    }

    //
    // Get the region data to update
    //
    $regiondata = $objDOM->getElementsByTagName("regiondata");

    //If returned length is 0, collector method may have returned an error
    if ($regiondata->length == 0) {
        echo "No regiondata in XML\n";
        Delete($gateway);
        return;
    }

    $regiondata = $regiondata->item(0);

    //
    // Update nextcheck so this host entry won't be checked again until after
    // the DataSnapshot module has generated a new set of data to be parsed.
    //
    $expire = $regiondata->getElementsByTagName("expire")->item(0)->nodeValue;
    $next = $now + $expire;

    $query = $db->prepare("UPDATE hostsregister SET nextcheck = ? WHERE host = ? AND port = ?");
    $query->execute( array($next, $hostname, $port) );

    //
    // Get the region data to be saved in the database
    //
    $regionlist = $regiondata->getElementsByTagName("region");

    foreach ($regionlist as $region)
    {
        $regioncategory = $region->getAttributeNode("category")->nodeValue;

        //
        // Start reading the Region info
        //
        $info = $region->getElementsByTagName("info")->item(0);

        $regionuuid = $info->getElementsByTagName("uuid")->item(0)->nodeValue;

        $regionname = $info->getElementsByTagName("name")->item(0)->nodeValue;

        $regionhandle = $info->getElementsByTagName("handle")->item(0)->nodeValue;

        $url = $info->getElementsByTagName("url")->item(0)->nodeValue;

        //
        // First, check if we already have a region that is the same
        //
        $check = $db->prepare("SELECT * FROM regions WHERE regionUUID = ?");
        $check->execute( array($regionuuid) );

        if ($check->rowCount() > 0)
        {
            $query = $db->prepare("DELETE FROM regions WHERE regionUUID = ?");
            $query->execute( array($regionuuid) );
            $query = $db->prepare("DELETE FROM parcels WHERE regionUUID = ?");
            $query->execute( array($regionuuid) );
            $query = $db->prepare("DELETE FROM allparcels WHERE regionUUID = ?");
            $query->execute( array($regionuuid) );
            $query = $db->prepare("DELETE FROM parcelsales WHERE regionUUID = ?");
            $query->execute( array($regionuuid) );
            $query = $db->prepare("DELETE FROM objects WHERE regionuuid = ?");
            $query->execute( array($regionuuid) );
        }

        $data = $region->getElementsByTagName("data")->item(0);
        $estate = $data->getElementsByTagName("estate")->item(0);

        $username = $estate->getElementsByTagName("name")->item(0)->nodeValue;
        $useruuid = $estate->getElementsByTagName("uuid")->item(0)->nodeValue;

        $estateid = $estate->getElementsByTagName("id")->item(0)->nodeValue;

        //
        // Second, add the new info to the database
        // gateway modified fkb
        
        echo "Insert region: $regionname\n";
        $query = $db->prepare("INSERT INTO regions VALUES(:r_name, :r_uuid, :r_handle, :url, :u_name, :u_uuid, :r_gateway)");
        $query->execute( array( "r_name" => $regionname,
                                "r_uuid" => $regionuuid,
                                "r_handle" => $regionhandle,
                                "url" => $url,
                                "u_name" => $username,
                                "u_uuid" => $useruuid,
                                "r_gateway" => $gateway
                              ));

        //
        // Start reading the parcel info
        //
        $parcel = $data->getElementsByTagName("parcel");

        foreach ($parcel as $value)
        {
            $parcelname = $value->getElementsByTagName("name")->item(0)->nodeValue;
            
            echo "Insert parcel: $parcelname\n";

            $parceluuid = $value->getElementsByTagName("uuid")->item(0)->nodeValue;

            $infouuid = $value->getElementsByTagName("infouuid")->item(0)->nodeValue;

            $parcellanding = $value->getElementsByTagName("location")->item(0)->nodeValue;

            $parceldescription = $value->getElementsByTagName("description")->item(0)->nodeValue;

            $parcelarea = $value->getElementsByTagName("area")->item(0)->nodeValue;

            $parcelcategory = $value->getAttributeNode("category")->nodeValue;

            $parcelsaleprice = $value->getAttributeNode("salesprice")->nodeValue;

            $dwell = $value->getElementsByTagName("dwell")->item(0)->nodeValue;

            //The image tag will only exist if the parcel has a snapshot image
            $has_pic = 0;
            $image_node = $value->getElementsByTagName("image");

            if ($image_node->length > 0)
            {
                $image = $image_node->item(0)->nodeValue;

                if ($image != "00000000-0000-0000-0000-000000000000")
                    $has_pic = 1;
            }

            $owner = $value->getElementsByTagName("owner")->item(0);

            $owneruuid = $owner->getElementsByTagName("uuid")->item(0)->nodeValue;

            // Adding support for groups

            $group = $value->getElementsByTagName("group")->item(0);

            if ($group != "")
            {
                $groupuuid = $group->getElementsByTagName("groupuuid")->item(0)->nodeValue;
            }
            else
            {
                $groupuuid = "00000000-0000-0000-0000-000000000000";
            }

            //
            // Check bits on Public, Build, Script
            //
            $parcelforsale = $value->getAttributeNode("forsale")->nodeValue;
            $parceldirectory = $value->getAttributeNode("showinsearch")->nodeValue;
            $parcelbuild = $value->getAttributeNode("build")->nodeValue;
            $parcelscript = $value->getAttributeNode("scripts")->nodeValue;
            $parcelpublic = $value->getAttributeNode("public")->nodeValue;

          

            $query = $db->prepare("DELETE FROM allparcels WHERE parcelUUID = ?");
            $query->execute( array($parceluuid) );


            //
            // Save
            //
            // Fred b mod: caused duplicate key
            
            echo "Insert into all parcels: $parcelname\n";
            // Missing delete - should really be an update_or_insert
            $query = $db->prepare("INSERT INTO allparcels VALUES(" .
                                    ":r_uuid, :p_name, :o_uuid, :g_uuid, " .
                                    ":landing, :p_uuid, :i_uuid, :area, :r_gateway )");
            $query->execute( array(
                                   "r_uuid"  => $regionuuid,
                                   "p_name"  => $parcelname,
                                   "o_uuid"  => $owneruuid,
                                   "g_uuid"  => $groupuuid,
                                   "landing" => $parcellanding,
                                   "p_uuid"  => $parceluuid,
                                   "i_uuid"  => $infouuid,
                                   "area"    => $parcelarea,
                                   "r_gateway" => $gateway
                                  ) );
            
            
            //Prepare for the insert of data in to the popularplaces table. This gets
            //rid of any obsolete data for parcels no longer set to show in search.
            $query = $db->prepare("DELETE FROM popularplaces WHERE parcelUUID = ?");
            $query->execute( array($parceluuid) );

            if ($parceldirectory == "true")
            {
                echo "Insert popularplaces: $parcelname\n";
                $query = $db->prepare("INSERT INTO parcels VALUES(" .
                                       ":r_uuid, :p_name, :p_uuid, :landing, " .
                                       ":desc, :cat, :build, :script, :public, ".
                                       ":dwell, :i_uuid, :r_cat, :r_gateway )");
                $query->execute( array(
                                       "r_uuid"  => $regionuuid,
                                       "p_name"  => $parcelname,
                                       "p_uuid"  => $parceluuid,
                                       "landing" => $parcellanding,
                                       "desc"    => $parceldescription,
                                       "cat"     => $parcelcategory,
                                       "build"   => $parcelbuild,
                                       "script"  => $parcelscript,
                                       "public"  => $parcelpublic,
                                       "dwell"   => $dwell,
                                       "i_uuid"  => $infouuid,
                                       "r_cat"   => $regioncategory,
                                       "r_gateway" => $gateway
                                      ) );

                $query = $db->prepare("INSERT INTO popularplaces VALUES(" .
                                       ":p_uuid, :p_name, :dwell, " .
                                       ":i_uuid, :has_pic, :r_cat, :r_gateway )");
                $query->execute( array(
                                       "p_uuid"  => $parceluuid,
                                       "p_name"  => $parcelname,
                                       "dwell"   => $dwell,
                                       "i_uuid"  => $infouuid,
                                       "has_pic" => $has_pic,
                                       "r_cat"   => $regioncategory,
                                       "r_gateway" => $gateway
                                    ) );
            }

            if ($parcelforsale == "true")
            {
                echo "Insert parcelsales: $parcelname\n";
                $query = $db->prepare("INSERT INTO parcelsales VALUES(" .
                                       ":r_uuid, :p_name, :p_uuid, :area, " .
                                       ":price, :landing, :i_uuid, :dwell, " .
                                       ":e_id, :r_cat, :r_gateway)");
                $query->execute( array(
                                       "r_uuid"  => $regionuuid,
                                       "p_name"  => $parcelname,
                                       "p_uuid"  => $parceluuid,
                                       "area"    => $parcelarea,
                                       "price"   => $parcelsaleprice,
                                       "landing" => $parcellanding,
                                       "i_uuid"  => $infouuid,
                                       "dwell"   => $dwell,
                                       "e_id"    => $estateid,
                                       "r_cat"   => $regioncategory,
                                       "r_gateway" => $gateway
                                    ) );
            }
        }

        //
        // Handle objects
        //
        $objects = $data->getElementsByTagName("object");

        foreach ($objects as $value)
        {
            $uuid = $value->getElementsByTagName("uuid")->item(0)->nodeValue;

            $regionuuid = $value->getElementsByTagName("regionuuid")->item(0)->nodeValue;

            $parceluuid = $value->getElementsByTagName("parceluuid")->item(0)->nodeValue;

            $location = $value->getElementsByTagName("location")->item(0)->nodeValue;

            $title = $value->getElementsByTagName("title")->item(0)->nodeValue;
            echo "Insert object: $title\n";
            $description = $value->getElementsByTagName("description")->item(0)->nodeValue;

            $flags = $value->getElementsByTagName("flags")->item(0)->nodeValue;

            $query = $db->prepare("INSERT INTO objects VALUES(" .
                                   ":uuid, :p_uuid, :location, " .
                                   ":title, :desc, :r_uuid, :r_gateway)");
            $query->execute( array(
                                   "uuid"     => $uuid,
                                   "p_uuid"   => $parceluuid,
                                   "location" => $location,
                                   "title"    => $title,
                                   "desc"     => $description,
                                   "r_uuid"   => $regionuuid,
                                   "r_gateway" => $gateway
                                  ) );
        }
    }
}


$failcounter = 0;

$sql = "SELECT gateway, host, port FROM hostsregister
            where
            host not like '192.168%'
            and host not like '172.16%'
            and host not like '172.17%'
            and host not like '172.18%'
            and host not like '172.19%'
            and host not like '172.20%'
            and host not like '172.21%'
            and host not like '172.22%'
            and host not like '172.23%'
            and host not like '172.24%'
            and host not like '172.25%'
            and host not like '172.26%'
            and host not like '172.27%'
            and host not like '172.28%'
            and host not like '172.29%'
            and host not like '172.30%'
            and host not like '172.31%'            
            and host <> '127.0.0.1'
            and host not like '10.%'  
            and nextcheck < $now AND failcounter < 10
            order by host asc
            LIMIT 0,20";

// Skip after 10 tries, they need to re-register

$jobsearch = $db->query($sql);

//
// If the sql query returns no rows, all entries in the hostsregister
// table have been checked. Re-run the
// query to select the next set of hosts to be checked.
//
if ($jobsearch->rowCount() == 0)
{
    
    echo "Nothing to do\n";
  
    #$jobsearch = $db->query("UPDATE hostsregister SET checked = 0");
    # the above is a bad idea. The 

    $jobsearch = $db->query($sql);
}

while ($jobs = $jobsearch->fetch(PDO::FETCH_NUM))
{    
    echo "Checking " . $jobs[0] . " @ " . $jobs[1] . ":" . $jobs[2] ;
    $jobs[0] = str_replace('http://','',$jobs[0]);
    CheckHost($jobs[0], $jobs[1],$jobs[2]);
}

$db = NULL;
?>
