
# Windows Server 2012 Kostenfrei installieren


### Download
https://www.microsoft.com/de-de/evalcenter/download-windows-server-2012-r2


### Commands

```
DISM.exe /Online /Get-TargetEditions
DISM.exe /online /Set-Edition:ServerStandard /ProductKey:[KEY] /AcceptEula
slmgr /ipk [KEY]
slmgr /skms [KMS_SERVER]
slmgr /ato
```

### Infos
Py-KMS - https://github.com/SystemRage/py-kms

Keys - https://github.com/SystemRage/py-kms/blob/master/docs/Keys.md
