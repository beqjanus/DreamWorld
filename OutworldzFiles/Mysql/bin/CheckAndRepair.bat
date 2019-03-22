@echo off
echo Upgrading MySql Database. Useage:  CheckAndRepair [Port number]
SET PORT = %1
IF "%PORT%" == "" (SET PORT=3306)
mysql_upgrade.exe --port=%PORT%

myisamchk --force --fast --update-state ..\data\mysql\*.MYI
myisamchk --force --fast --update-state ..\data\robust\*.MYI

echo Checking Database
mysqlcheck.exe --port %PORT% -u root -A 
set /p fixmysql=Repair and Optimize [y/n]?:
@echo Repairing, please wait!
IF "%fixmysql%" == "y" mysqlcheck.exe --port %PORT% -A  -u root -r
@echo Optimizing, please wait!
IF "%fixmysql%" == "y" mysqlcheck.exe --port %PORT% -A  -u root -o

set /p temp="Press enter to exit"


