@REM program to renew SSL certificate
@echo off
cd C:\Opensim\Outworldz_Dreamgrid\SSL
.\wacs.exe --accepttos --source manual --host test.outworldz.net --validation filesystem --webroot "C:\Opensim\Outworldz_Dreamgrid\OutworldzFiles\Apache\htdocs" --store pemfiles --pemfilespath "C:\Opensim\Outworldz_Dreamgrid\OutworldzFiles\Apache\Certs"   --emailaddress fred@outworldz.com  --closeonfinish --test 
Exit /B %ERRORLEVEL%
