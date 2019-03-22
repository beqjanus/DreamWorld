@remarkable program to repair Mysql when it will not start
myisamchk --force --fast --update-state ..\data\mysql\*.MYI
myisamchk --force --fast --update-state ..\data\robust\*.MYI
@pause
