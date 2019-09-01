<?php
    // AGPL 3.0 by Fred Beckhusen
    require( "flog.php" );
    
    include("databaseinfo.php");
     // Attempt to connect to the database
      try {
        $db = new PDO("mysql:host=$DB_HOST;port=$DB_PORT;dbname=$DB_NAME", $DB_USER, $DB_PASSWORD);
        $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_SILENT);
      }
    catch(PDOException $e)
    {
      echo "Error connecting to database\n";
      file_put_contents('../../../PHPLog.log', $e->getMessage() . "\n-----\n", FILE_APPEND);
      exit;
    }
    
    $text = $_GET['query'];     
    $sqldata['text1'] = $text;

    $rc = intval($_GET['rp'] )  ;
    
    if ($rc == "") {
      $rc = 100;
    }    
    
    $sort = $_GET['sortname'];
    if ($sort == 'Grid') {
        $sort = 'url';
    } else if ($sort == 'RegionName') {
        $sort = 'regionname';
    } else {
        $sort = 'owner';
    }
    
    $ord = $_GET['sortorder']   ;
    if ($ord == 'asc') {
        $ord = 'asc';
    } else {
        $ord = 'desc';
    }
    
    $qtype = $_GET['qtype'];
    if ($qtype == 'Grid') {
        $qtype = 'url';
    } else if ($qtype == 'Regionname') {
        $qtype = 'regionname';
    } else if ($qtype == 'Owner') {
        $qtype = 'owner';
    } else {
        flog('wtf?' . $qtype);
    }
    flog('qtype:' . $qtype);
    
    $total = 0;
    
    $page =  $_GET['page'];
    if ($page == "" ) {
        $page = 1;
    }
    
    flog("text= $text");
    flog("qtype= $qtype");
    flog("ord= $ord");
    flog("sort= $sort");
    
    
    $stack = array();
    
    class OUT {}
    class Row {}
  
    $out = new OUT();

    $counter = 0;
     
    $query = "SELECT * FROM ossearch.classifieds";
    
    flog($query);
    
    $sqldata = array();
    
    $query = $db->prepare($query);
    $result = $query->execute($sqldata);
    $counter = 0;
      
    while ($row = $query->fetch(PDO::FETCH_ASSOC))
    {
      
        flog("Name:". $row["name"]);
        $name = wordwrap($row["name"],25,"<br>\n", true);
        $description = wordwrap($row["description"],50,"<br>\n");
        
        $gateway = $row["gateway"];
        $gateway = substr($gateway,7);
        
        if ($gateway == "" ) {
            $hop = '';
        } else {
            $hop = "<a href=\"secondlife://http|!!". $gateway . "\"  class=\"hop\"><img src=\"images/Hop.png\" height=\"25\"></a>";
        }
        
        if ($row["simname"] == '')
        {
            $simname = '';
        }else{
            $simname = $row["simname"];
        }
        if ($row["expirationdate"] == '')
        {
            $date = '';
        }else{
            $date = $row["expirationdate"];
        }
        
        if ($row["category"] == '' )
        {
            $category = '';
        } else {
            $category = $row["category"];
        }
        
        $row = array("hop"=>$hop,
                     "Grid"=>$row["gateway"],
                      //$row["classifieduuid"],
                       //$row["creatoruuid"],
                       //$row["creationdate"],
                      "ExpirationDate"     => $row["expirationdate"],
                      "Category"           => $category,
                      "Name"               => $name,
                      "Description"        => $description,
                      //$row["parceluuid"],
                      //$row["parentestate"],
                      //$row["snapshotuuid"],
                      "Region"             => $simname,
                      //$row["posglobal"],
                      //$row["parcelname"],
                      //$row["classifiedflags"],
                      //$row["priceforlisting"],
                 );
              
        $rowobj = new Row();
        $rowobj->cell = $row;
            
        if ($total >= (($page-1) *$rc) && $total < ($page) *$rc) {
          array_push($stack, $rowobj);
        }
        
        $total++;
    }
    
    if ($total == 0) {
        flog("Nothing found");
        $row = array("Grid"=>"No records");
        $rowobj = new Row();
        $rowobj->cell = $row;
        array_push($stack, $rowobj);
    }
     
    $out->domain = $CONF_domain;
    $out->port = $CONF_port;
    $out->page  = $page;
    $out->total = $total;
    $out->rows  = $stack;
        
    $myJSON = json_encode($out);
    
    echo $myJSON;
     
?>
  
                 
