<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en-us">

<head>
<meta charset="utf-8">
<title>Opensimular search</title>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
<script type="text/javascript"  src="/flexgrid/js/flexigrid.js"></script>
<script type="text/javascript">

$(document).ready(function(){
	
	$("a").click(function(){
		top.window.location.href=$(this).attr("href");
		$("#playing").html( '<p>&nbsp;Opening...</p>' );
		return true;
	})
	
	$("#flex1").flexigrid({
		url: '/Search/search_json.php',
		dataType: 'json',
        method: 'GET',
		colModel : [
		//	{display: 'Hop', name : 'hop', width : 45, sortable : false, align: 'left'},		
			{display: 'Name', name : 'Name', width : 145, sortable : true, align: 'left'},			
			{display: 'Description', name : 'Description', width : 170, sortable : true, align: 'left'},			
			{display: 'Region name', name : 'Regionname', width :125, sortable : true, align: 'left'},
			{display: 'Location', name : 'Location', width : 200, sortable : true, align: 'left'}
			],
		
		searchitems : [
		 {display: 'Name', name :'Name', isdefault: true},
		 {display: 'Description', name :'Description', isdefault: false}
		 
		],
		sortname: 'Name',
		sortorder: 'asc',
		usepager: true,
		title: 'Click header to sort. Search button is at the bottom left.',
		useRp: true,
		rp: 100,
		showTableToggleBtn: false,
		width: 700,
		height: 400
	});


	$(document).on("click", ".hop", function(event){
		event.preventDefault();
		$("#playing").html( '<p>&nbsp;Teleporting...</p>' );
		var url = $(this).attr('href');
		window.location.href = url;
    });

});

</script>

<link rel="stylesheet" type="text/css" media="all" href="/flexgrid/css/flexigrid.css" />

<style>
body {
    /* background: #333333 url(/radio/radioback6.jpg) left top no-repeat;*/
}
a {
    color: #0060B6;
    text-decoration: underline;
}
p {
    background-color: #1F1A25;
}
#greet {
    
    width: 700px;
    height: 750px;
    color: white;
    font-family: Calibri;
    font-size: 100%;
    margin-left: 0px;
    margin-top: 0px;
}


a {
	color: #000000;
    text-decoration: none;
}
td {
	font-size:12px;	
}
</style>



</head>

<body>
	
<div id="greet">
    <div id="playing"></div>
    <div id="flex1" ></div>
	
</div>

</body>

</html>