<html>
 <head>
  <title>Welcome to DreamGrid</title>
  <link rel="shortcut icon" href="/favicon.ico">
</head>
<body>
<?php
  include("../Metromap/includes/config.php");
?>

[ <a href="/Search">Search</a>  | 
Log in to <a href="http://<?php echo $CONF_domain.":".$CONF_port ?>">DreamGrid</a> |
<a href="/Stats">Visitor Stats</a> |
<a href="http://<?php echo $CONF_domain.":".$CONF_port ?>/wifi/map.html">Map</a> ] 
<p>
<iframe frameborder="0" height="900" width="1600" src="/Metromap/index.php"></iframe>
</p>
<a rel="license" href="https://www.outworldz.com">Dreamgrid by Outworldz.com is licensed AGPL 3.0</a>
<p>
 DreamGrid is a <a href="https://www.outworldz.com/Outworldz_installer/">free Opensimulator server</a> powered by <a href="https://www.Outworldz.com">Outworldz.com</a>
</p>

 </body>
</html>
