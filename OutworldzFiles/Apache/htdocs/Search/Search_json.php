<?php
//include the source file
require( "flog.php" );

include("databaseinfo.php");
include("../Metromap/includes/config.php");

// Attempt to connect to the database
try {
  $db = new PDO("mysql:host=$DB_HOST;dbname=$DB_NAME", $DB_USER, $DB_PASSWORD);
  $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
 
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
$lim1 = intval($_GET['page']-1);  // for sql injection, make it only ints
$lim2 = intval($_GET['rp']);

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
if ($qtype = 'Name') {
  $qtype = 'Name';
} else {
  $qtype = 'Description';
}

$counter = 0;
 
$page = 1;
$stack = array();

$q = "SELECT Name, Description, Location, Regions.Regionname as  Regioname FROM Objects INNER JOIN Regions ON Objects.regionuuid = Regions.regionuuid 
    where " . $qtype . "  like :text1 
    order by " . $sort . ' ' .  $ord 
    . " limit " . $lim1 . "," . $lim2;     

#echo $q;

    $query = $db->prepare($q);
    $result = $query->execute($sqldata);
    
    $counter = 0;
    class OUT {}
    class Row {}
    
    $out = new OUT();
    
    while ($row = $query->fetch(PDO::FETCH_ASSOC))
    {
        $counter++;
        $location = $row["Location"];
        $location = str_replace("/"," ", $location );
        
        #$hop  = "<a href=\"". $url . "\"class=\"hop\"><img src=\"images/Hop.png\" height=\"25\"></a>";
        
        $row = array("hop"=>$hop, "Name"=>$row["Name"],"Description"=>$row["Description"],"Regionname"=>$row["Regioname"],"Location"=>$location);
        
        $rowobj = new Row();
        $rowobj->cell = $row;
        
       # foreach($rowobj->cell as $x=>$x_value)
       # {
       #   echo "Key=" . $x . ", Value=" . $x_value;
       #   echo "<br>";
       # }
  
        
        #$myJSON = json_encode($rowobj);
        #echo $myJSON . "<br>";
        array_push($stack, $rowobj);
    }

    $out->domain = $CONF_domain;
    $out->port = $CONF_port;
    $out->page  = $page;
    $out->total = $counter;
    $out->rows  = $stack;
        
    $myJSON = json_encode($out);

    echo $myJSON;
   
   
    
?>
