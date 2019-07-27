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
        <title></title>
    </head>
    <body>
        
        <table class="striped">
            <tr class="header">
                <td>classifieduuid</td>
                <td>creatoruuid</td>
                <td>creationdate</td>
                <td>expirationdate</td>
                <td>category</td>
                <td>name</td>
                <td>description</td>
                <td>parceluuid</td>
                <td>parentestate</td>
                <td>snapshotuuid</td>
                <td>simname</td>
                <td>posglobal</td>
                <td>parcelname</td>
                <td>classifiedflags</td>
                <td>priceforlisting</td>
            </tr>
            <?php
             
             
             $query = "SELECT * FROM classifieds ";
             $sqldata = array();
             
             $query = $db->prepare($query);
             $result = $query->execute($sqldata);


               while ($row = $query->fetch(PDO::FETCH_ASSOC))
               {
                echo "<tr valign=\"top\">";
                 
                 echo "<td>" .$row["classifieduuid"] . "</td>";
                 echo "<td>" .$row["creatoruuid"] . "</td>";
                 echo "<td>" .$row["creationdate"] . "</td>";
                 echo "<td>" . $row["expirationdate"] . "</td>";
                 echo "<td>" . $row["category"] . "</td>";
                 echo "<td>" . $row["name"] . "</td>";
                 echo "<td>" . $row["description"] . "</td>";
                 echo "<td>" . $row["parceluuid"] . "</td>";
                 echo "<td>" . $row["parentestate"] . "</td>";
                 echo "<td>" . $row["snapshotuuid"] . "</td>";
                 echo "<td>" . $row["simname"] . "</td>";
                 echo "<td>" . $row["posglobal"] . "</td>";
                 echo "<td>" . $row["parcelname"] . "</td>";
                 echo "<td>" . $row["classifiedflags"] . "</td>";
                 echo "<td>" . $row["priceforlisting"] . "</td>";
                 
                 echo "</tr>";
               }
            ?>
        </table>
    </body>
</html>