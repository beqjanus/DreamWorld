<?php
    
    // AGPL 3.0 by Fred Beckhusen
    require( "flog.php" );
 
    header("content-type: text/html; charset=UTF-8");
   
    include("database.php");
    
    $text = $_GET['query'];     
    $sqldata['text1'] = $text;
       
    $rc = intval($_GET['rp']);
    if ($rc == "") {
      $rc = 100;
    }    
    
    $sort = $_GET['sortname'];
    if ($sort == 'Parcelname') {
        $sort = 'parcelname';
    } else if ($sort == 'dwell') {
        $sort = 'dwell';
    } else if ($sort == 'Description') {
        $sort = 'Description';
    } else {
        $sort = 'dwell';
    }
    
    $ord = $_GET['sortorder'];
    if ($ord == 'asc') {
        $ord = 'asc';
    } else {
        $ord = 'desc';
    }
    
    $qtype = $_GET['qtype'];
    if ($qtype == 'Grid') {
        $qtype = 't1.gateway';
    } else if ($qtype == 'Description') {
        $qtype = 'Description';
    } else if ($qtype == 'Parcelname') {
        $qtype = 'Parcelname';
    } else if ($qtype == 'Mature') {
        $qtype = 'Mature';        
    } else {
        $qtype = 't1.gateway';
    }
    
    flog("text= $text");
    flog("qtype= $qtype");
    flog("ord= $ord");
    flog("sort= $sort");
    
    $total = 0;
    
    $page =  intval($_GET['page']);
    if ($page == "" ) {
     $page = 1;
    }
    
    $stack = array();
    
    $query = "SELECT * FROM ossearch.parcels  t1
            inner join  ossearch.regions on t1.regionUUID = regions.regionUUID
            
            where
                t1.gateway not like '192.168%'
            and t1.gateway not like '172.16%'
            and t1.gateway not like '172.17%'
            and t1.gateway not like '172.18%'
            and t1.gateway not like '172.19%'
            and t1.gateway not like '172.20%'
            and t1.gateway not like '172.21%'
            and t1.gateway not like '172.22%'
            and t1.gateway not like '172.23%'
            and t1.gateway not like '172.24%'
            and t1.gateway not like '172.25%'
            and t1.gateway not like '172.26%'
            and t1.gateway not like '172.27%'
            and t1.gateway not like '172.28%'
            and t1.gateway not like '172.29%'
            and t1.gateway not like '172.30%'
            and t1.gateway not like '172.31%'            
            and t1.gateway <> 'http://127.0.0.1'
            and t1.gateway not like '10.%'
            and $qtype  like  CONCAT('%', :text1, '%')
            order by  $sort  $ord";

    flog($query);
    $query = $db->prepare($query);
    
    flog($sqldata);
    $result = $query->execute($sqldata);
    
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
    # 14 ???? !!!!
    
    class OUT {}
    class Row {}
  
    $out = new OUT();

    $counter = 0;
    while ($row = $query->fetch(PDO::FETCH_ASSOC))
    {
        $gateway = str_replace (':', '|', $row["gateway"] );
        $regionname = str_replace(' ','+',$row["regionname"]);
        
        
        #$hop    = "hop://" . $row["gateway"]  . '/' . $row["landingpoint"];
        $v3     = "secondlife://http|!!" . $gateway  .  '+' . $regionname. '/' . $row["landingpoint"];
        #$hg     = "secondlife://" . $row["gateway"]  . '/' . $row["landingpoint"];
            
        
        #$link = "<a href=\"$hop\"><img src=\"hop.png\" height=\"24\"></a>";
        $link = "<a href=\"$v3\"><img src=\"v3hg2.png\" height=\"24\"></a>";
        #$link .= "<br><a href=\"$hg\"><img src=\"hg.png\" height=\"24\"></a>";
  
        
        
        $category = "";
        if ($row["searchcategory"] == 0) { $category = "Any"; }
        else if ($row["searchcategory"] == 1) { $category = "Linden Location";}
        else if ($row["searchcategory"] == 2) { $category = "Arts and Culture";}
        else if ($row["searchcategory"] == 3) { $category = "Business";}
        else if ($row["searchcategory"] == 4) { $category = "Educational";}
        else if ($row["searchcategory"] == 5) { $category = "Gaming";}
        else if ($row["searchcategory"] == 6) { $category = "Hideout";}
        else if ($row["searchcategory"] == 7) { $category = "Newcomer Friendly";}
        else if ($row["searchcategory"] == 8) { $category = "Parks & Nature";}
        else if ($row["searchcategory"] == 9) { $category = "Residential";}
        else if ($row["searchcategory"] == 10) { $category = "Shopping";}
        else if ($row["searchcategory"] == 11) { $category = "Rental";}
        else if ($row["searchcategory"] == 12) { $category = "Other";}
        else if ($row["searchcategory"] == 13) { $category = "Other";}
        else $category = "?";
        
        $location = $row["landingpoint"];
        
        $x = '<input type="checkbox" checked="false">';
        if ($row["build"] == 'true') {
            $x = '<input type="checkbox" checked="true">';
        }
        $y = '<input type="checkbox" checked="false">';
        if ($row["script"] == 'true') {
            $y = '<input type="checkbox" checked="true">';
        } 
        
        $description = $row["gateway"] . "<br>" . $row["description"];
        $parcelname = $row["parcelname"];
        $row["mature"] = str_replace('Mature','M',$row["mature"]);
        $row = array("hop"=>$link,
                     "Grid"=>$row["gateway"],
                     "Description"=>$description ,
                     "Regionname"=>$row["regionname"] ,
                     "Parcelname"=>$parcelname,
                     "Build"=>$x,
                     "Script"=>$y,
                     "Dwell"=>$row["dwell"],
                     "Location"=>$row["landingpoint"],
                     "Category"=>$category,
                     "Mature"=>$row["mature"],
                     "dwell"=> $row["dwell"]
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
  