# Windows Server 2012 R2 Installieren

###### Was wird benötigt?
[Windows Server 2012 R2 ISO](https://www.microsoft.com/de-de/evalcenter/download-windows-server-2012-r2 "Windows Server 2012 R2 ISO")

###### Commands
```shell
DISM.exe /Online /Get-TargetEditions

DISM.exe /online /Set-Edition:ServerStandard /ProductKey:D2N9P-3P6X9-2R39C-7RTCD-MDVJX /AcceptEula

slmgr /ipk D2N9P-3P6X9-2R39C-7RTCD-MDVJX

slmgr /skms [KMS SERVER]

slmgr /ato
```


## FAQ
###### Wo finde ich die Keys?
[Link zur py-kms Key Datei](https://github.com/SystemRage/py-kms/blob/master/docs/Keys.md "Link zur py-kms Key Datei")

###### Wo finden ich ein KMS Server?
Teilweise sind online Welche ausgeschrieben. Sollte man keinen finden kann man sich selber einen aufsetzten: [py-kms](https://github.com/SystemRage/py-kms "pykms")