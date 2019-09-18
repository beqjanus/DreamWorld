@echo off
echo Upgrading MySql Database. Usage:  CheckAndRepair [Port number]

IF [%1]==[] (ECHO Port Value Missing
pause
exit
)

IF [%1] == [] SET PORT=3306

mysql_upgrade.exe --port %1

myisamchk --force --fast --update-state ..\data\mysql\*.MYI
myisamchk --force --fast --update-state ..\data\robust\*.MYI

echo Checking Database
mysqlcheck.exe --port %1 -u root -A 
set /p fixmysql=Repair and Optimize [y/n]?:
@echo Repairing, please wait!
IF "%fixmysql%" == "y" mysqlcheck.exe --port %1 -A  -u root -r
@echo Optimizing, please wait!
IF "%fixmysql%" == "y" mysqlcheck.exe --port %1 -A  -u root -o

set /p temp="Press enter to exit"


