<?php

include("../Metromap/includes/config.php");
$dsn = "mysql:host=$CONF_db_server;port=$CONF_db_port;dbname=ossearch";


$options = [
    PDO::ATTR_EMULATE_PREPARES   => false, // turn off emulation mode for "real" prepared statements
    PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION, //turn on errors in the form of exceptions
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC, //make the default fetch be an associative array
    PDO::MYSQL_ATTR_INIT_COMMAND => "SET NAMES 'utf8'"
];

try {

$db = new PDO($dsn,  $CONF_db_user, $CONF_db_pass, $options);

} catch (Exception $e) {
    error_log($e->getMessage());
    exit('Something weird happened:' . $e->getMessage()); //something a user can understand
}

try {

$db1 = new PDO($dsn,  $CONF_db_user, $CONF_db_pass, $options);

} catch (Exception $e) {
    error_log($e->getMessage());
    exit('Something weird happened:'  . $e->getMessage()); //something a user can understand
}

?>
