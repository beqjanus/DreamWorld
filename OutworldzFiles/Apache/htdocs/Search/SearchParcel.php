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
  <head>
    <link rel="stylesheet" type="text/css" media="all" href="/flexgrid/css/flexigrid.css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <link rel="stylesheet" type="text/css" media="all" href="/flexgrid/css/flexigrid.css" />
    <link rel="stylesheet" type="text/css" media="all" href="/Search/style.css" />
    <script type="text/javascript">
            $(document).ready(function(){
               $('.striped tr:even').addClass('alt');
            });
    </script>
    
    <title>Search Parcel</title>
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
      <td>Region Name</td>
      <td>Parcel</td>
      <td>Description</td>
      <td>Landing Point</td>
      <td>Category</td>
      <td>Mature</td>
    </tr>
    <?php
      $query = "SELECT * FROM parcels  t1 inner join  regions on t1.regionUUID = regions.regionUUID where public = 'true'  order by t1.parcelname";
      flog($query);
      $sqldata = array();
      flog("prepare 1");
      $query = $db->prepare($query);
      flog("prepare 2");
      $result = $query->execute($sqldata);
      flog("execute");
      
      #$row["searchcategory"]
      
      # 1 Any
      # 2 Linden Location
      # 3 Arts and Culture
      # 4 Business
      # 5 Educational
      # 6 Gaming
      # 7 Hideout
      # 8 Newcomer Friendly
      # 9 Parks & Nature
      # 10 Residential
      # 11 Shopping
      # 12 Rental
      # 13 Other
      
      
      $counter = 0;
        while ($row = $query->fetch(PDO::FETCH_ASSOC))
        {
          
          $category = "";
          if ($row["searchcategory"] == 1) { $category = "Any"; }
          else if ($row["searchcategory"] == 2) { $category = "Linden Location";}
          else if ($row["searchcategory"] == 3) { $category = "Arts and Culture";}
          else if ($row["searchcategory"] == 4) { $category = "Business";}
          else if ($row["searchcategory"] == 5) { $category = "Educational";}
          else if ($row["searchcategory"] == 6) { $category = "Gaming";}
          else if ($row["searchcategory"] == 7) { $category = "Hideout";}
          else if ($row["searchcategory"] == 8) { $category = "Newcomer Friendly";}
          else if ($row["searchcategory"] == 9) { $category = "Parks & Nature";}
          else if ($row["searchcategory"] == 10) { $category = "Residential";}
          else if ($row["searchcategory"] == 11) { $category = "Shopping";}
          else if ($row["searchcategory"] == 12) { $category = "Rental";}
          else if ($row["searchcategory"] == 13) { $category = "Other";}
          
           echo "<tr class=\"striped\" valign=\"top\">";
          echo "<td nowrap>" .  $row["regionname"] . "</td>\n";
          echo "<td>" .$row["parcelname"] . "</td>\n";
          echo "<td>" .$row["description"] . "</td>\n";
          echo "<td>" .$row["landingpoint"] . "</td>\n";
          echo "<td>$category</td>\n";
          echo "<td>" . $row["mature"] . "</td>\n";
          
          
          $counter += 1;
          echo "</tr>\n";
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
