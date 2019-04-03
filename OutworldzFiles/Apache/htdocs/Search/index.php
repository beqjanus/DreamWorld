<?php
//include the log and database files
require( "flog.php" );
include("databaseinfo.php");

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

?>

<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
        <title>Search Opensimulator</title>
    </head>
    <body>
      <form action="Classifieds.htm">
        <input type="Submit" name="Classified" value = "Create Classified Ad">
      </form>
      <form action="SearchClassifieds.php">
        <input type="Submit" name="Classified" value = "Dump Classified Ad">
      </form>
      <form action="SearchParcel.php">
        <input type="Submit" name="Parcels" value = "List Parcels">
      </form>
      <form action="ShowHosts.php">
        <input type="Submit" name="Ports" value = "Dump Region Ports">
      </form>
      <form action="SearchRegions.php">
        <input type="Submit" name="Regions" value = "Dump Regions">
      </form>
      <form action="SearchObjects.php">
        <input type="Submit" name="Objects" value = "Dump Objects">
      </form>
        <h1>Search</h1>
        <form action="search.php" method="POST">
          <input type="text" name="SearchTerm" >
            <input type="Submit" name="Search">
        </form>
       <a rel="license" href="https://www.gnu.org/licenses/agpl-3.0.en.html">AGPL 3.0</a>
    </body>
</html>