
@rem makes a full back into Autobackup of robust and opensim

for /f "tokens=2 delims==" %%a in ('wmic OS Get localdatetime /value') do set "dt=%%a"
set "YY=%dt:~2,2%" & set "YYYY=%dt:~0,4%" & set "MM=%dt:~4,2%" & set "DD=%dt:~6,2%"
set "HH=%dt:~8,2%" & set "Min=%dt:~10,2%" & set "Sec=%dt:~12,2%"
set "fullstamp=%YYYY%-%MM%-%DD%_%HH%-%Min%-%Sec%"

echo use Robust; > ..\..\Autobackup\Robust_%fullstamp%.sql
mysqldump.exe --opt --hex-blob --add-drop-table --allow-keywords --single-transaction  --quick  -uroot --max_allowed_packet=1G --verbose  Robust >> ..\..\Autobackup\Robust_%fullstamp%.sql
IF ERRORLEVEL 0 GOTO Label1
@ECHO Something went wrong, your backup of Robust database  failed
GOTO End

:Label1
echo use Opensim; > ..\..\Autobackup\Opensim_%fullstamp%.sql
mysqldump.exe --opt --hex-blob --add-drop-table --allow-keywords --single-transaction  --quick  -uroot --verbose  Opensim  >> ..\..\Autobackup\Opensim_%fullstamp%.sql

IF ERRORLEVEL 0 GOTO Label0
@ECHO Something went wrong, your backup of Opensim database failed
GOTO End

:Label0
@ECHO Backup Finished
:End
@pause

@echo Finished!