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
        <!--
        <meta http-equiv="refresh" content="1;URL='/Search'" />
        -->
        <title></title>
    </head>
    <body>
       
            <?php
             
                $query = "insert into classifieds ( 
                        classifieduuid, 
                        creatoruuid,
                        creationdate,
                        expirationdate,
                        category,
                        name,
                        description,
                        parceluuid,
                        parentestate,
                        snapshotuuid,
                        simname,
                        posglobal,
                        parcelname,
                        classifiedflags,
                        priceforlisting
                    ) values (
                        :classifieduuid,       
                        :creatoruuid,
                        :creationdate,
                        :expirationdate,
                        :category,
                        :name,
                        :description,
                        :parceluuid,
                        :parentestate,
                        :snapshotuuid,
                        :simname,
                        :posglobal,
                        :parcelname,
                        :classifiedflags,
                        :priceforlisting     
                    )";
            
                    $sqldata = array();
                    $now = time();
                    $now -= idate("Z");     //Adjust for timezone
            
                    $then = $now + 86400;   //Time for end of day
                    $sqldata['classifieduuid']     = uniqid();
                    $sqldata['creatoruuid']        = uniqid();
                    $sqldata['creationdate']       = $now;
                    $sqldata['expirationdate']     = $now + 86400;
                    $sqldata['category']           = $_POST['category'];
                    $sqldata['name']               = $_POST['name'];
                    $sqldata['description']        = $_POST['description'];
                    $sqldata['parceluuid']         = uniqid();
                    $sqldata['parentestate']       = "00000000-0000-0000-0000-000000000000";
                    $sqldata['snapshotuuid']       = $_POST['snapshotuuid'] || "00000000-0000-0000-0000-000000000000";
                    $sqldata['simname']            = $_POST['simname'];
                    $sqldata['parcelname']         = "";
                    $sqldata['posglobal']          = "";
                    $sqldata['classifiedflags']    = "4"; 
                    $sqldata['priceforlisting']    = $_POST['price'];
                    
                    
                    flog($query);
                    flog($sqldata);
                    try {
                        $query = $db->prepare($query);
                        $result = $query->execute($sqldata);
                    }
                    catch(Exception $e)
                    {
                      echo "Error";
                      flog($e->getMessage());
                      exit;
                    }

                    echo "Inserted";
            ?>
        </table>
    </body>
</html>