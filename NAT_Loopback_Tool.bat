echo off
setlocal
REM Initial Script by Joerk Landgraf. wget by Jonas Eberle, Zak.Spot(at)hypergrid.org
REM Mods for Dreamgrid by Fred Beckhusen
REM The network interface that needs to be set to the external IP
REM Adjust this! 
REM (You will receive help if you start the script with a non-existent interface.)
set Interface=%1%

REM Thanks to this internet service, we can get the IP without frills
Set URL="http://api.ipify.org/"

echo NAT_Loopback_Tool.bat v1.3
echo Dreamgrid Edition by Fred Beckhusen(at)outworldz.com
echo _____________________________________________
echo.
REM changing to directory containing this batch file
cd /d %~dps0

:start
echo Looking for external IP
set NewIP=
for /f %%a in ('wget -q -O - %URL%') do set NewIP=%%a
IF "%NewIP%"=="" goto errorNoIP
echo Found: %NewIP%

REM Fire up the Loopback adapter with the external IP address
echo Setting interface %Interface% to external IP (OS is %OS%)
REM netsh needs OS differentiation
IF "%OS%"=="Windows_NT" netsh interface ip set address name=%Interface% source=static addr=%NewIP%  mask=255.255.255.0
IF NOT "%OS%"=="Windows_NT" netsh interface ipv4 set address name=%Interface% source=static addr=%NewIP%  mask=255.255.255.0
goto end

errorNoIP:
echo No IP found
:end

