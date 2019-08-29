<?php
// AGPL 3.0 by Fred Beckhusen
require( "flog.php" );

include("databaseinfo.php");
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

    
?>


<html lang="en-us">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
  <link rel="stylesheet" type="text/css" media="all" href="/flexgrid/css/flexigrid.css" />
  <link rel="stylesheet" type="text/css" media="all" href="/Search/style.css" />
  <script type="text/javascript">
            $(document).ready(function(){
               $('.striped tr:even').addClass('alt');
            });
  </script>
  <title>Search Classifieds</title>
  <link rel="shortcut icon" href="/favicon.ico">
</head>

<body>
  

 <div id="Links">
<a href="index.php"><button>Objects</button></a>
<a href="SearchClassifieds.php"><button>Classifieds</button></a>
<a href="SearchParcel.php"><button>Parcels</button></a>
<a href="ShowHosts.php"><button>Hosts</button></a>
<a href="SearchRegions.php"><button>Regions</button></a>
<button onclick="location.reload();">Refresh</button>

</div>

  <table class="striped">
    <tr class="header">
      
      <td>Grid</td>
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
  </table>
</body>
</html>
<?php
             
             
             $query = "SELECT * FROM classifieds ";
             $sqldata = array();
             
             $query = $db->prepare($query);
             $result = $query->execute($sqldata);
             $counter = 0;


               while ($row = $query->fetch(PDO::FETCH_ASSOC))
               {
                echo "<tr valign=\"top\">";
                 echo "<td><a href=\"". $row["gateway"] . "\">" . $row["gateway"] . "</a></td>\n";
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
                 $counter += 1;
                 echo "</tr>";
               }
               
               if ($counter == 0) {
                echo "<tr valign=\"top\">";
                echo "<td> </td>";
                echo "<td>Nothing found</td>";
                echo "</tr>";
            }
            echo "</table>";
            echo "<br><input type=\"button\" value=\"Go Back\" onclick=\"history.back(-1)\" />"; 
            ?>
        </table>
    </body>
</html>