<?php
//include the source file
require( "flog.php" );

include("databaseinfo.php");

// Attempt to connect to the database
try {
  $db = new PDO("mysql:host=$DB_HOST;dbname=$DB_NAME", $DB_USER, $DB_PASSWORD);
  $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  $dba = new PDO("mysql:host=$DB_HOST;dbname=$DB_NAME", $DB_USER, $DB_PASSWORD);
  $dba->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
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
                background-color: #eeeeee;
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
                <td>Map</td>
                <td>Name</td>
                <td>Description</td>
                <td>Teleport</td>
            </tr>
            <?php
            
                function RegionName($uuid) {
                    
                    echo $uuid;
                    $q = "SELECT regionname from regions where regionUUID=:text1" ;
                    $sqldata['text1'] = $uuid;
                    
                    $query = $dba->prepare($q);
                    #$result = $query->execute($sqldata);
                    
                    
                    #if  ($row = $query->fetch(PDO::FETCH_ASSOC))
                    #{
                    #    return $row["regionname"];
                    #}
                    return "???";
                }
            
            $text = $_POST['SearchTerm'];
            $text = "%$text%";
            $sqldata['text1'] = $text;
            $sqldata['text2'] = $text;
             
            #echo "term:" . $sqldata['text1'];
             
            $q = "SELECT Name, Description, regionuuid, location FROM Objects where Name like :text1 or Description like :text2  order by name, description ";
            #$query = "SELECT Name, Description FROM Objects order by name, description ";
            
            $query = $db->prepare($q);
            $result = $query->execute($sqldata);
            
            $counter = 0;
            while ($row = $query->fetch(PDO::FETCH_ASSOC))
            {
                $counter++;
                $port = 8064;
                $map = "http://127.0.0.1:" . $port . "/index.php?method=regionImage" . str_replace("-","",$row["regionuuid"]);
                $hop =  RegionName();
                #$hop = "<a href=\"hop://127.0.0.1:8002/" . RegionName() . "\" . RegionName() . <img src=\"images\hop.png\"></a>";
                
                echo "<tr valign=\"top\">";
                echo "<td><img src=\"" . $map . "\"</a></td>";
                echo "<td>" . $row["Name"] . "</td>";
                echo "<td>" . $row["Description"] . "</td>";
                echo "<td>" . $hop . "</td>";
                echo "</tr>";
            }
            if ($counter == 0) {
                echo "<tr valign=\"top\">";
                echo "<td> </td>";
                echo "<td>Nothing found</td>";
                echo "</tr>";
            }
            echo "</table>";
            echo "<input type=\"button\" value=\"Go Back\" onclick=\"history.back(-1)\" />";
            
            ?>
        </table>
    </body>
</html>