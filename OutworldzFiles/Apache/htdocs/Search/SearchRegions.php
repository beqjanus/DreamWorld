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

<html>
<html lang="en-us">
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" media="all" href="/flexgrid/css/flexigrid.css" />
    <link rel="stylesheet" type="text/css" media="all" href="/Search/style.css" />

        <script type="text/javascript">
            $(document).ready(function(){
               $('.striped tr:even').addClass('alt');
            });
        </script>

  <title>Search Regions</title>
  <link rel="shortcut icon" href="/favicon.ico">
</head>

<body>
  <div id="Links">
<a href="index.php"><button>Objects</button></a>
<a href="SearchClassifieds.php"><button>Classifieds</button></a>
<a href="SearchParcel.php"><button>Parcels</button></a>
<a href="ShowHosts.php"><button>Hosts</button></a>
<a href="SearchRegions.php"><button>Regions</button></a>
<button onclick="location.reload();">Refresh Page</button>

</div>

  <table class="striped">
    <tr class="header">
      <td>Grid</td>           
      <td>Region name</td>           
      <td>Region url</td>
      <td>Owner<td>
    </tr>
    <?php
      $query = "SELECT * FROM regions order by regionname ";
      $sqldata = array();
      
      $query = $db->prepare($query);
      $result = $query->execute($sqldata);


        while ($row = $query->fetch(PDO::FETCH_ASSOC))
        {
         echo "<tr class=\"striped\">";
          echo "<td><a href=\"". $row["gateway"] . "\">" . $row["gateway"] . "</a></td>\n";
          echo "<td>" .$row["regionname"] . "</td>";
          echo "<td>" . $row["url"] . "</td>";
          echo "<td>" . $row["owner"] . "</td>";
          
          echo "</tr>";
        }
        echo "</table>      ";
        echo "<br><input type=\"button\" value=\"Go Back\" onclick=\"history.back(-1)\" />"; 
     ?>
  
  
</body>
</html>
