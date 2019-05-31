echo off
setlocal
REM Initial Script by Joerk Landgraf. wget by Jonas Eberle, Zak.Spot(at)hypergrid.org
REM Mods for Dreamgrid by Fred Beckhusen
REM The network interface that needs to be set to the external IP
REM Adjust this! 
REM (You will receive help if you start the script with a non-existent interface.)
set Interface=%1%

REM Thanks to this internet service, we can get the IP without frills
Set URL="http://hypergrid.org/my_external_ip.php"

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
REM old: netsh interface ip set address %Interface% static %NewIP% 255.255.255.0
IF NOT "%OS%"=="Windows_NT" netsh interface ipv4 set address name=%Interface% source=static addr=%NewIP%  mask=255.255.255.0
REM old: netsh interface ipv4 set address %Interface% static %NewIP%

IF NOT %ERRORLEVEL%==0 goto errorSetIp
goto ok

:errorNoIP
Set Log=ERROR: URL did not return an IP: %URL%
echo Could not fetch your external IP from 
echo %URL%
echo Network error? You might want try that URL in your browser. 
echo.
choice /c:AR /t:r,10 Press 'A' to abort, 'R' to retry. Retrying in 10 seconds. 
IF %ERRORLEVEL%==1 goto end
echo.
goto start

:errorSetIp
Set Log=ERROR: Could not set IP: %Interface%: %NewIp%
echo Error on interface %Interface%. Is the adapter enabled? Did you run this  with the name of the adapter as a parameter?
echo Showing some information about your interfaces now - press a key!
pause > nul
netsh interface show interface
goto error

:error
echo.
echo Press a key to finish
pause > nul
goto end

:ok
Set Log=SUCCESS: %Interface%: %NewIp%
echo Everything fine, exiting....
REM This is the nasty way to say "sleep x" when the sleep tool is not available
ping 127.0.0.1 -n 5 -w 1000 > nul

:end
REM Some logging...
For /F %%I in ('date /T') Do Set StrDate=%%I
For /F %%I in ('time /T') Do Set StrTime=%%I
echo %StrDate% %StrTime%: %Log% >> SET_externalIP-log.txt
endlocal
