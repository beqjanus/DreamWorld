@REM program to renew SSL certificate
@echo off
cd C:\Opensim\Outworldz_Dreamgrid\OutworldzFiles\SSL
.\wacs.exe --accepttos --source manual --host dns.outworldz.net --validation filesystem --webroot "C:\Opensim\Outworldz_Dreamgrid\OutworldzFiles\Apache\htdocs" --store pemfiles --pemfilespath "C:\Opensim\Outworldz_Dreamgrid\OutworldzFiles\Apache\Certs"   --closeonfinish --test 
Exit /B %ERRORLEVEL%
