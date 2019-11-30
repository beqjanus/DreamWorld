<?php
// AGPL 3.0 by Fred Beckhusen
require( "flog.php" );

include("../Metromap/includes/config.php");

  $dsn = "mysql:host=$CONF_db_server;port=$CONF_db_port;dbname=$CONF_db_database";

  $options = [
    PDO::ATTR_EMULATE_PREPARES   => false, // turn off emulation mode for "real" prepared statements
    PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION, //turn on errors in the form of exceptions
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC, //make the default fetch be an associative array
  ];
  try {

    $db = new PDO($dsn,  $CONF_db_user, $CONF_db_pass, $options);
    $db1 = new PDO($dsn,  $CONF_db_user, $CONF_db_pass, $options);
  } catch (Exception $e) {
    error_log($e->getMessage());
    exit('Something weird happened'); //something a user can understand
  }

    $text = $_GET['query'];
	
    $sqldata['text1'] = $text;

    $rc = intval($_GET['rp'] )  ;

    if ($rc == "") {
      $rc = 100;
    }

    $sort = $_GET['sortname'] ;
    $sort = 'host';

    $ord = $_GET['sortorder']   ;
    if ($ord == 'asc') {
        $ord = 'asc';
    } else {
        $ord = 'desc';
    }

    $qtype = $_GET['qtype'] ;
    $qtype = 'gateway';

    $total = 0;

    $page =  $_GET['page'];
    if ($page == "" ) {
        $page = 1;
    }

    $stack = array();

    class OUT {}
    class Row {}

    $out = new OUT();

    $counter = 0;

    $sql = "SELECT distinct host as host FROM ossearch.hostsregister  where
            failcounter = 0
            and gateway not like '192.168%'
            and gateway not like '172.16%'
            and gateway not like '172.17%'
            and gateway not like '172.18%'
            and gateway not like '172.19%'
            and gateway not like '172.20%'
            and gateway not like '172.21%'
            and gateway not like '172.22%'
            and gateway not like '172.23%'
            and gateway not like '172.24%'
            and gateway not like '172.25%'
            and gateway not like '172.26%'
            and gateway not like '172.27%'
            and gateway not like '172.28%'
            and gateway not like '172.29%'
            and gateway not like '172.30%'
            and gateway not like '172.31%'
            and gateway not like '127.0.0.1%'
            and gateway not like '10.%'
			and " . $qtype . "  like CONCAT('%', :text1, '%')
            order by " . $sort . ' ' .  $ord ;
            

    $query = $db->prepare($sql);
    $result = $query->execute($sqldata);

    $host = '';

    while ($row = $query->fetch(PDO::FETCH_ASSOC))
    {
        $host = $row["host"];

        // get the port
        $sql1 = "SELECT gateway FROM ossearch.hostsregister  where host = :text1";

        $query1 = $db1->prepare($sql1);
        $result1 = $query1->execute(array('text1' => $host));

        $gateway = '';
        while ($row1 = $query1->fetch(PDO::FETCH_ASSOC))
        {
            $gateway = $row1["gateway"];
        }

        // make hyperlink
        $v3    = "hop://" . $gateway;
        $link = "<a href=\"$v3\"><img src=\"v3hg.png\" height=\"24\"></a><br>";

        // get the hours of runtime
        $sql1 = "SELECT sum(checked) as minutes FROM ossearch.hostsregister where host = :text1";
        $query1 = $db1->prepare($sql1);
        $result1 = $query1->execute(array('text1'=>$host));

        $minutes = 0;
        while ($row1 = $query1->fetch(PDO::FETCH_ASSOC))
        {
            $minutes = $row1["minutes"] * 10;
        }

        if ($minutes > 0 ) {
            $minutes = round($minutes /60,1) ;
        } else {
            $minutes = 0;
        }

        $row = array(
                   "hop"=>$link,
                   "Grid"=>$host,
                   "Hours"=> $minutes
                   );

        $rowobj = new Row();
        $rowobj->cell = $row;

        if ($total >= (($page-1) *$rc) && $total < ($page) *$rc) {
        array_push($stack, $rowobj);
        }

        $total++;
    }

    if ($total == 0) {
        flog("Nothing found");
        $row = array("Grid"=>"No records");
        $rowobj = new Row();
        $rowobj->cell = $row;
        array_push($stack, $rowobj);
    }

    $out->domain = $CONF_domain;
    $out->port = $CONF_port;
    $out->page  = $page;
    $out->total = $total;
    $out->rows  = $stack;

    $myJSON = json_encode($out);

    echo $myJSON;

  ?>
