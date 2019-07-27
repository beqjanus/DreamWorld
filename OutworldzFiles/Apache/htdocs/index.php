<html>
 <head>
  <title>Welcome to Dreamworld</title>
 </head>
 <body>


<?php
//include the log and database files

include("MetroMap/includes/config.php");



// Attempt to connect to the database
try {
  $db = new PDO("mysql:host=$CONF_db_server;dbname=$CONF_db_database", $CONF_db_user, $CONF_db_pass, $CONF_db_port);
  #$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
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
        <title>Dreamgrid Opensimulator by www.outworldz.com</title>
    </head>
    <body>
    
Log in to <a href="http://<?php echo $CONF_domain.":".$CONF_port ?>">Opensimulator</a>
<p>
      <iframe frameborder="0" height="900" width="1600" src="/Metromap/index.php"></iframe>
      
 
<a rel="license" href="https://www.outworldz.com">Dreamgrid by Outworldz.com is licensed AGPL 3.0</a>  
 </body>
</html>
