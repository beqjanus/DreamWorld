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
  $text = "%$text%";
  $sqldata['text1'] = $text;
  

  $rc = intval($_GET['rp']);
   
  if ($rc == "") {
      $rc = 100;
  }
  

  $sort = $_GET['sortname'];
  if ($sort == 'Name') {
    $sort = 'Name';
  }else{
    $sort = 'Description';
  }
  
  $ord = $_GET['sortorder'];
  if ($ord == 'asc') {
    $ord = 'asc';
  } else {
    $ord = 'desc';
  }
  
  $qtype = $_GET['qtype'];
  if ($qtype == 'Name') {
    $qtype = 'Name';
  } else {
    $qtype = 'Description';
  }
  
  flog("text= $text");
  flog("qtype= $qtype");
  flog("ord= $ord");
  flog("sort= $sort");
  
  $total = 0;
   
  $page =  $_GET['page'];
  if ($page == "" ) {
    $page = 1;
  }
  
  $stack = array();

  $q = "SELECT Regions.gateway as AGateway, Name, Description, Location, Regions.Regionname as  Regioname FROM Objects
    INNER JOIN Regions ON Objects.regionuuid = Regions.regionuuid 
    INNER JOIN hostsregister ON Objects.gateway = hostsregister.gateway    
            where
            failcounter = 0
            and " . $qtype . "  like CONCAT('%', :text1, '%')
    order by " . $sort . ' ' .  $ord ;
    

    flog($q );  
    $query = $db->prepare($q);
    $result = $query->execute($sqldata);
    flog($sqldata );  
    class OUT {}
    class Row {}
    
    $out = new OUT();
    $counter= 0;
    while ($row = $query->fetch(PDO::FETCH_ASSOC))
    {
        
        $location = $row["Location"];
        
        $gateway = $row["AGateway"];
        flog("Gateway: $gateway");
        $gateway = substr($gateway,7);
        
        flog("Gateway: $gateway");
        
        if ($gateway == "" ) {
          $hop = '';  
        } else {
          $hop = "<a href=\"secondlife://http|!!" . $gateway.  "+" . $row["Regioname"] . "/" . $location .  "\"  class=\"hop\"><img src=\"images/Hop.png\" height=\"25\"></a>";
        }
        
        $description = wordwrap($row["Description"],30, "<br>\n", false);
        $name = wordwrap($row["Name"],35, "<br>\n", false);
        
        $row = array("hop"=>$hop,
                     "Name"=>$name,
                     "Description"=>$description,
                     "Regionname"=>$row["Regioname"]. '<br>' . $gateway ,
                     "Location"=>$location);
        
        $rowobj = new Row();
        $rowobj->cell = $row;
        
        #$myJSON = json_encode($rowobj);
        #echo $myJSON . "<br>";
        if ($total >= (($page-1) *$rc) && $total < ($page) *$rc) {
          array_push($stack, $rowobj);
        }
        $total++;
        
    }
    if ($total == 0) {
	  flog("Nothing found");
      $row = array("hop"=>"", "Name"=>"No records","Description"=>"No records","Regionname"=>"No records","Location"=>"No records");
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
