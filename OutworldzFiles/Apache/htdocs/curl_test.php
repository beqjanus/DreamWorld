<?php
    error_reporting(E_ALL);
    ini_set('display_errors', '1');
    $ch = curl_init();
    curl_setopt($ch, CURLOPT_URL, 'http://www.outworldz.com/');
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
    $contents = curl_exec ($ch);
    echo "This:\n";
	echo $contents;
    curl_close ($ch);
?>
	