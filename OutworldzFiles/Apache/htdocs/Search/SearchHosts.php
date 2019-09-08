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
 
 
    $text = $_GET['query'];     
    $sqldata['text1'] = $text;
       
    $rc = intval($_GET['rp'] )  ;
    
    if ($rc == "") {
      $rc = 100;
    }    
    
    $sort = $_GET['sortname'] ;
    $sort = 'Grid';
    
    $ord = $_GET['sortorder']   ;
    if ($ord == 'asc') {
        $ord = 'asc';
    } else {
        $ord = 'desc';
    }
    
    $qtype = $_GET['qtype'] ;
    $qtype = 'gateway';
    
    $total = 0;
    
    $page =  $_GET['page'];
    if ($page == "" ) {
        $page = 1;
    }
    
    $stack = array();
    
    class OUT {}
    class Row {}
  
    $out = new OUT();

    $counter = 0;
    
    $sql = "SELECT distinct lower(gateway) as gateway FROM hostsregister  where $qtype  like  CONCAT('%', :text1, '%')
        and failcounter = 0
            and  $qtype  like  CONCAT('%', :text1, '%')
            and gateway not like 'http://192.168%'
            and gateway not like 'http://172.16%'
            and gateway not like 'http://172.17%'
            and gateway not like 'http://172.18%'
            and gateway not like 'http://172.19%'
            and gateway not like 'http://172.20%'
            and gateway not like 'http://172.21%'
            and gateway not like 'http://172.22%'
            and gateway not like 'http://172.23%'
            and gateway not like 'http://172.24%'
            and gateway not like 'http://172.25%'
            and gateway not like 'http://172.26%'
            and gateway not like 'http://172.27%'
            and gateway not like 'http://172.28%'
            and gateway not like 'http://172.29%'
            and gateway not like 'http://172.30%'
            and gateway not like 'http://172.31%'            
            and gateway not like 'http://127.0.0.1%'
            and gateway not like 'http://10.%'
        order by gateway ";
    flog($sql );     
    $query = $db->prepare($sql);
    
    $result = $query->execute($sqldata);
    flog($sqldata);
    
    $counter = 0;
    while ($row = $query->fetch(PDO::FETCH_ASSOC))
    {
        
        $row["gateway"] = str_replace('http://','',$row["gateway"]);
        $v3    = "secondlife:///app/teleport/" . $row["gateway"] ;             
        $hours = $row["checked"] - 1;
        $link = "<a href=\"$v3\"><img src=\"v3hg.png\" height=\"24\"></a><br>";
        
        $row = array(
                     "hop"=>$link,
                     "Grid"=>$row["gateway"],
                     "Hours"=> $hours
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
  