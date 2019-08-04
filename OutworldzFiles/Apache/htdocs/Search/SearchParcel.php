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
        <title>Search Parcel</title>
        <link rel="shortcut icon" href="/favicon.ico">
    </head>
    <body>
        
        <table class="striped">
            <tr class="header">
                <td>parcel_id</td>
                <td>Name</td>
                <td>Description</td>
                <td>Landing Point</td>
                <td>searchcategory</td>
                <td>build</td>
                <td>script</td>
                <td>public</td>
                <td>dwell</td>
                <td>Mature</td>
            </tr>
            <?php
             
             
             $query = "SELECT * FROM parcels where public = 'true'  order by parcelname ";
             $sqldata = array();
             
             $query = $db->prepare($query);
             $result = $query->execute($sqldata);


               while ($row = $query->fetch(PDO::FETCH_ASSOC))
               {
                echo "<tr valign=\"top\">";
                 echo "<td nowrap> InfoUUID:<br>". $row["infouuid"] . "<br>Region UUID:<br>" . $row["regionUUID"] . "<br>ParcelUUID:<br>" . $row["parcelUUID"] . "</td>";
                 echo "<td>" .$row["parcelname"] . "</td>";
                 echo "<td>" .$row["description"] . "</td>";
                 echo "<td>" .$row["landingpoint"] . "</td>";
                 echo "<td>" . $row["mature"] . "</td>";
                 echo "<td>" . $row["searchcategory"] . "</td>";
                 echo "<td>" . $row["build"] . "</td>";
                 echo "<td>" . $row["script"] . "</td>";
                 echo "<td>" . $row["public"] . "</td>";
                 echo "<td>" . $row["dwell"] . "</td>";
                 
                 echo "</tr>";
               }
            ?>
        </table>
    </body>
</html>