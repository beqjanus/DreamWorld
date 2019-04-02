<?php
//include the source file
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
        <title></title>
    </head>
    <body>
        
        <table class="striped">
            <tr class="header">
                <td>regionname </td>
                <td>regionUUID </td>
                <td>regionhandle </td>
                <td>url</td>
                <td>owner</td>
                <td>owneruuid </td>
                
            </tr>
            <?php
             
             
             $query = "SELECT * FROM regions order by regionname ";
             $sqldata = array();
             
             $query = $db->prepare($query);
             $result = $query->execute($sqldata);


               while ($row = $query->fetch(PDO::FETCH_ASSOC))
               {
                echo "<tr valign=\"top\">";
                 
                 echo "<td>" .$row["regionname"] . "</td>";
                 echo "<td>" .$row["regionUUID"] . "</td>";
                 echo "<td>" .$row["regionhandle"] . "</td>";
                 echo "<td>" . $row["url"] . "</td>";
                 echo "<td>" . $row["owner"] . "</td>";
                 echo "<td>" . $row["owneruuid"] . "</td>";
                 echo "</tr>";
               }
            ?>
        </table>
    </body>
</html>