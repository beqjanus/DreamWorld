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
    
    $query = "SELECT * FROM regions where  $qtype  like  CONCAT('%', :text1, '%')

    and gateway not like 'http://127.%'
    and gateway not like 'http://10.%'
    and gateway not like 'http://192.168.%'
    order by  $sort  $ord";
    
   // $sqldata = array();
    flog ($query);
    
    $query = $db->prepare($query);
    flog($sqldata);

    $result = $query->execute($sqldata);

    while ($row = $query->fetch(PDO::FETCH_ASSOC))
    { 
        $gateway = $row["gateway"] . "+" .$row["regionname"];
        $gateway = substr($gateway,7,999);
        flog("Gateway is " . $gateway);
        if ($row["gateway"] == '') {} else {
            $hop = "<a href=\"secondlife://http|!!". $gateway . "\"  class=\"hop\"><img src=\"images/Hop.png\" height=\"25\"></a>";
        }
        $row = array("hop"=>$hop,
                     "Grid"=>$row["gateway"],
                     "RegionName"=>$row["regionname"] ,
                     "Owner"=>$row["owner"]
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
  
    