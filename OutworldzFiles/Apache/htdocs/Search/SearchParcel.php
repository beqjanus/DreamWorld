<?php
    
    // AGPL 3.0 by Fred Beckhusen
    require( "flog.php" );
    
    include("databaseinfo.php");
    include("../Metromap/includes/config.php");
    
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
        
    
    $rc = intval($_GET['rp'] )  ;
    
    if ($rc == "") {
      $rc = 100;
    }
    
    
    $sort = $_GET['sortname'] ;
    if ($sort == 'Grid') {
        $sort = 'Grid';
    }else{
        $sort = 'Description';
    }
    
    $ord = $_GET['sortorder']   ;
    if ($ord == 'asc') {
        $ord = 'asc';
    } else {
        $ord = 'desc';
    }
    
    $qtype = $_GET['qtype'] ;
    if ($qtype == 'Grid') {
        $qtype = 'Grid';
    } else {
        $qtype = 'Description';
    }
    
    $total = 0;
    
    $page =  $_GET['page'];
    if ($page == "" ) {
        $page = 1;
    }
    
    $stack = array();
    
    $query = "SELECT * FROM parcels  t1 inner join  regions on t1.regionUUID = regions.regionUUID where public = 'true'  order by t1.parcelname";      
    $sqldata = array();      
    $query = $db->prepare($query);      
    $result = $query->execute($sqldata);
    
    #$row["searchcategory"]
    
    # 1 Any
    # 2 Linden Location
    # 3 Arts and Culture
    # 4 Business
    # 5 Educational
    # 6 Gaming
    # 7 Hideout
    # 8 Newcomer Friendly
    # 9 Parks & Nature
    # 10 Residential
    # 11 Shopping
    # 12 Rental
    # 13 Other
    # 14 ???? !!!!
    
    class OUT {}
    class Row {}
  
    $out = new OUT();

    $counter = 0;
    while ($row = $query->fetch(PDO::FETCH_ASSOC))
    {
        $gateway = $row["gateway"];
        $gateway = substr($gateway,7,999);
        
        $category = "";
        if ($row["searchcategory"] == 1) { $category = "Any"; }
        else if ($row["searchcategory"] == 2) { $category = "Linden Location";}
        else if ($row["searchcategory"] == 3) { $category = "Arts and Culture";}
        else if ($row["searchcategory"] == 4) { $category = "Business";}
        else if ($row["searchcategory"] == 5) { $category = "Educational";}
        else if ($row["searchcategory"] == 6) { $category = "Gaming";}
        else if ($row["searchcategory"] == 7) { $category = "Hideout";}
        else if ($row["searchcategory"] == 8) { $category = "Newcomer Friendly";}
        else if ($row["searchcategory"] == 9) { $category = "Parks & Nature";}
        else if ($row["searchcategory"] == 10) { $category = "Residential";}
        else if ($row["searchcategory"] == 11) { $category = "Shopping";}
        else if ($row["searchcategory"] == 12) { $category = "Rental";}
        else if ($row["searchcategory"] == 13) { $category = "Other";}
        else if ($row["searchcategory"] == 14) { $category = "????";}
        else $category = "Nothing";
        
        $location = $row["landingpoint"];
        
        $hop = "<a href=\"secondlife://http|!!". $gateway . "+" . $row["regionname"] . "/" . $row["landingpoint"].  "\"  class=\"hop\"><img src=\"images/Hop.png\" height=\"25\"></a>";
        $row = array("hop"=>$hop,
                     "Grid"=>$row["gateway"],
                     "Description"=>$row["description"],
                     "Regionname"=>$row["regionname"] ,
                     "Parcelname"=>$row["parcelname"],
                     "Location"=>$row["landingpoint"],
                     "Category"=>$category,
                     "Mature"=>$row["mature"]
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
  