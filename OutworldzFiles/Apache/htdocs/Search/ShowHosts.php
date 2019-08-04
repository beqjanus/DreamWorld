<?php
//include the source file
require( "flog.php" );

include("databaseinfo.php");

 // Attempt to connect to the database
  try {
    $db = new PDO("mysql:host=$DB_HOST;port=$DB_port;dbname=$DB_NAME", $DB_USER, $DB_PASSWORD);
    $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_SILENT);
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
        <style type="text/css">
            tr.header
            {
                font-weight:bold;
            }
            tr.alt
            {
                background-color: #777777;
            }
        </style>
        <script type="text/javascript">
            $(document).ready(function(){
               $('.striped tr:even').addClass('alt');
            });
        </script>
        <title>Show Hosts</title>
        <link rel="shortcut icon" href="/favicon.ico">
    </head>
    <body>
        
        <table class="striped">
            <tr class="header">
                <td>Host</td>
                <td>Port</td>
            </tr>
            
            <?php
             
             $sql = "SELECT * FROM hostsregister order by host ";
             
             $sqldata = array();
             
             $query = $db->prepare($sql);
             $result = $query->execute($sqldata);
             $counter = 0;
               while ($row = $query->fetch(PDO::FETCH_ASSOC))
               {
                 echo "<tr>\n";
                 echo "<td>". $row["host"] . "</td>";
                 echo "<td>". $row["port"] . "</td>";
                 echo "</tr>\n";
                 $counter++;
               }
            ?>
        </table>
        <?php
        echo "Count = " . $counter;
        ?>
    </body>
</html>