<?php
  
	header("content-type: text/html; charset=UTF-8");
  
	require( "flog.php" );
	include("database.php");
  
	$text = '';
    if (isset($_GET['query']))     $text = $_GET['query'];	
    $sqldata['text1'] = $text;

	$rc = '';
    if (isset($_GET['rp']))    $rc = intval($_GET['rp'] )  ;

    if ($rc == "") {
      $rc = 100;
    }

	$sort = '';
	if (isset($_GET['sort']))     $sort = $_GET['sortname'] ;
    $sort = 'host';

	$ord = '';
    if (isset($_GET['sortorder']))    $ord = $_GET['sortorder']   ;
    if ($ord == 'asc') {
        $ord = 'asc';
    } else {
        $ord = 'desc';
    }

	$qtype = '';
    if (isset($_GET['qtype']))    $qtype = $_GET['qtype'] ;
    $qtype = 'gateway';

    $total = 0;
	
	$page = '';

    if (isset($_GET['page']))    $page =  intval($_GET['page']);
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
	    and online = 1
            and host not like '192.168%'
            and host not like '172.16%'
            and host not like '172.17%'
            and host not like '172.18%'
            and host not like '172.19%'
            and host not like '172.20%'
            and host not like '172.21%'
            and host not like '172.22%'
            and host not like '172.23%'
            and host not like '172.24%'
            and host not like '172.25%'
            and host not like '172.26%'
            and host not like '172.27%'
            and host not like '172.28%'
            and host not like '172.29%'
            and host not like '172.30%'
            and host not like '172.31%'
            and host not like '127.0.0.1%'
            and host not like '10.%'
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

        $query1 = $db->prepare($sql1);
        $result1 = $query1->execute(array('text1' => $host));

        $gateway = '';
        while ($row1 = $query1->fetch(PDO::FETCH_ASSOC))
        {
            $gateway = $row1["gateway"];
        }

        $gateway = str_replace (':', '|', $gateway );
        #$regionname = str_replace(' ','+',$row["regionname"]);
        
        // make hyperlink
        #$v3    = "hop://" . $gateway;
        $v3     =  "secondlife://http|!!" . $gateway  ;
        $link = "<a href=\"$v3\"><img src=\"v3hg2.png\" height=\"24\"></a><br>";

        // get the hours of runtime
        $sql1 = "SELECT sum(checked) as minutes FROM ossearch.hostsregister where host = :text1";
        $query1 = $db->prepare($sql1);
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
