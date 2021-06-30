<?php
  // AGPL 3.0 by Fred Beckhusen
  
    require( "flog.php" );
    include("database.php");
    header("content-type: text/html; charset=UTF-8");    
 

    $text = $_GET['query'];     
    $sqldata['text1'] = $text;

    $rc = intval($_GET['rp'] )  ;
    
    if ($rc == "") {
      $rc = 100;
    }    
    
    $sort = $_GET['sortname'];
    
    
    if ($sort == 'Grid') {
        $sort = 'url';
    } else if ($sort == 'RegionName') {
        $sort = 'regionname';
    } else {
        $sort = 'owner';
    }
        
    $ord = $_GET['sortorder']   ;
    if ($ord == 'asc') {
        $ord = 'asc';
    } else {
        $ord = 'desc';
    }
    
    $qtype = $_GET['qtype'];
    if ($qtype == 'Grid') {
        $qtype = 'url';
    } else if ($qtype == 'Regionname') {
        $qtype = 'regionname';
    } else if ($qtype == 'Owner') {
        $qtype = 'owner';
    } else {
        $qtype = 'url';
    }
    flog('qtype:' . $qtype);
    
    $total = 0;
    
    $page =  intval($_GET['page']);
    if ($page == "" ) {
        $page = 1;
    }
    
    flog("text= $text");
    flog("qtype= $qtype");
    flog("ord= $ord");
    flog("sort= $sort");
    
    
    $stack = array();
    
    class OUT {}
    class Row {}
  
    $out = new OUT();

    $counter = 0;
    
    $query = "SELECT * FROM ossearch.regions  where " .  $qtype  . " like  CONCAT('%', :text1, '%')  
            and regions.gateway not like '192.16%'
            and regions.gateway not like '172.16%'
            and regions.gateway not like '172.17%'
            and regions.gateway not like '172.18%'
            and regions.gateway not like '172.19%'
            and regions.gateway not like '172.20%'
            and regions.gateway not like '172.21%'
            and regions.gateway not like '172.22%'
            and regions.gateway not like '172.23%'
            and regions.gateway not like '172.24%'
            and regions.gateway not like '172.25%'
            and regions.gateway not like '172.26%'
            and regions.gateway not like '172.27%'
            and regions.gateway not like '172.28%'
            and regions.gateway not like '172.29%'
            and regions.gateway not like '172.30%'
            and regions.gateway not like '172.31%'            
            and regions.gateway <> 'http:127.0.0.1'
            and regions.gateway not like '10.%'
            order by " .  $sort . " " . " $ord";
    
   
    flog ($query);
    
    $query = $db->prepare($query);
    flog($sqldata);

    $result = $query->execute($sqldata);

    while ($row = $query->fetch(PDO::FETCH_ASSOC))
    {
      
        //flog($row["gateway"]);
      
        $gateway = str_replace (':', '|', $row["gateway"] );
        $regionname = str_replace(' ','+',$row["regionname"]);
        
       # $hop    = "hop://" . $row["gateway"] .  '/' .$row["Regioname"] ;
        $v3     = "secondlife://http|!!" . $gateway  .  '+' . $regionname ;
        #$hg     = "secondlife://" . $row["gateway"]  .   '/' .$row["Regioname"];
        
        
        #$link = "<a href=\"$hop\"><img src=\"hop.png\" height=\"24\"></a>";
        $link = "<a href=\"$v3\"><img src=\"v3hg2.png\" height=\"24\"></a>";
        #$link .= "<br><a href=\"$hg\"><img src=\"hg.png\" height=\"24\"></a>";
        
        
        $row = array("hop"=>$link,
                     "Grid"         =>$row["gateway"],
                     "RegionName"   => $row["regionname"],                     
                     "Owner"=>$row["owner"]
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
  
    