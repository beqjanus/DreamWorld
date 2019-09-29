<?php
// AGPL 3.0 by Fred Beckhusen
  require( "flog.php" );

  include("databaseinfo.php");
   
  $dsn = "mysql:host=$CONF_db_server;port=$CONF_db_port;dbname=$CONF_db_database";
  
  $options = [
    PDO::ATTR_EMULATE_PREPARES   => false, // turn off emulation mode for "real" prepared statements
    PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION, //turn on errors in the form of exceptions
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC, //make the default fetch be an associative array
  ];
  try {
    
    $db = new PDO($dsn,  $CONF_db_user, $CONF_db_pass, $options);
    
  } catch (Exception $e) {
    error_log($e->getMessage());
    exit('Something weird happened'); //something a user can understand
  }
    
?>


<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <!--
        <meta http-equiv="refresh" content="1;URL='/Search'" />
        -->
        <title>Classifieds</title>
        <link rel="shortcut icon" href="/favicon.ico">
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
                    $sqldata['category']           = $_POST['category'];        // 0 = G rating?
                    $sqldata['name']               = $_POST['name'];
                    $sqldata['description']        = $_POST['description'];
                    $sqldata['parceluuid']         = uniqid();
                    $sqldata['parentestate']       = "101";
                    $sqldata['snapshotuuid']       = $_POST['snapshotuuid'] || "00000000-0000-0000-0000-000000000000";
                    $sqldata['simname']            = $_POST['simname'];
                    $sqldata['parcelname']         = "";
                    $sqldata['posglobal']          = "";
                    $sqldata['classifiedflags']    = $_POST['rating'];
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