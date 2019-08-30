<?php
//////////////////////////////////////////////////////////////////////////////
// register.php                                                             //
// This file contains the registration of a simulator to the database       //
// and checks if the simulator is new in the database or a reconnected one  //
//                                                                          //
// If the simulator is old, check if the nextcheck date > registration      //
// When the date is older, make a request to the Parser to grab new data    //
//////////////////////////////////////////////////////////////////////////////

// mod by fkb for gateway for centralized search across grids

require( "flog.php" );

include("databaseinfo.php");

$gateway = "";
$hostname = "";
$port = "";
$service = "";

if (isset($_GET['gatekeeper']))    $gateway = $_GET['gatekeeper'];
if (isset($_GET['host']))    $hostname = $_GET['host'];
if (isset($_GET['port']))    $port = $_GET['port'];
if (isset($_GET['service'])) $service = $_GET['service'];

flog("gateway:" . $gateway );
flog("hostname:" . $hostname );
flog("port:" . $port);
flog("service:" . $service );

if ($hostname == "" || $port == "")
{
    echo "Missing host name and/or port address\n";
    flog("Missing host name and/or port address");
    exit;
}

if ($gateway == "")
{
    $gateway = 'http://' . $hostname . ':8002'; 
}

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

if ($service == "online")
{
    file_put_contents('../../../PHPLog.log', "Registered as online:" . $hostname . ":" . $port . "\n", FILE_APPEND);
    // Check if there is already a database row for this host
    $query = $db->prepare("SELECT register FROM hostsregister WHERE host = ? AND port = ?");
    $query->execute( array($hostname, $port) );

    // Get the request time as a timestamp for later
    $timestamp = $_SERVER['REQUEST_TIME'];

    // If a database row was returned check the nextcheck date
    if ($query->rowCount() > 0)
    {
        $query = $db->prepare("UPDATE hostsregister SET " .
                     "register = ?, " .
                     "nextcheck = 0, checked = 0, failcounter = 0, gateway = ? " .
                     "WHERE host = ? AND port = ?");
        $query->execute( array($timestamp, $gateway, $hostname, $port) );
    }
    else
    {
        // The SELECT did not return a result. Insert a new record.
        $query = $db->prepare("INSERT INTO hostsregister VALUES (?, ?, ?, 0, 0, 0, ?)");
        $query->execute( array($hostname, $port, $timestamp, $gateway) );
    }
}

if ($service == "offline")
{
    flog("offline:" .  $hostname . ":" . $port);
    $query = $db->prepare("DELETE FROM hostsregister WHERE host = ? AND port = ?");
    $query->execute( array($hostname, $port) );
}

$db = NULL;
?>
