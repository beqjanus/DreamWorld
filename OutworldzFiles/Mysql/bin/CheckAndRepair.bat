@echo off
SET PORT = %1
IF "%PORT%" == "" (SET PORT=3306)

SET PWD = %2
if NOT %2 == "" (SET CMD=-p%2) ELSE  (SET CMD="") 
if NOT %2 == "" (SET CMDB=-p) ELSE  (SET CMDB="") 
echo Root password = %2
echo Re-enter your MySQL root password:
mysql_upgrade.exe --user=root --port=%PORT% %CMDB%
myisamchk --force --fast --update-state ..\data\mysql\*.MYI

echo Checking Database
mysqlcheck.exe --port %PORT% -u root %CMD% -A
set /p fixmysql=Repair and Optimize [y/n]?:
@echo Repairing, please wait!
IF "%fixmysql%" == "y" mysqlcheck.exe --port %PORT% -u root  %CMD%  -r -A 
@echo Optimizing, please wait!
IF "%fixmysql%" == "y" mysqlcheck.exe --port %PORT% -u root  %CMD% -o -A 

set /p temp="Press enter to exit"


