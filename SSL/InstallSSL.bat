@REM program to renew SSL certificate
@echo off
cd C:\Users\Fred\Desktop\Dreamgrid\Dreamworld\SSL
.\wacs.exe --accepttos --source manual --host smartboot.outworldz.net --validation filesystem --webroot "C:\Users\Fred\Desktop\Dreamgrid\Dreamworld\OutworldzFiles\Apache\htdocs" --store pemfiles --pemfilespath "C:\Users\Fred\Desktop\Dreamgrid\Dreamworld\OutworldzFiles\Apache\Certs"   --emailaddress fred@outworldz.com   
Exit /B %ERRORLEVEL%
