
# Office 2013 Kostenfrei installieren


### Download
Windows ISO Downloader - https://www.chip.de/downloads/Windows-ISO-Downloader_95133731.html
Office 2013 Proffessional Retail ISO - https://officeredir.microsoft.com/r/rlidO15C2RMediaDownload?p1=db&p2=de-DE&p3=ProfessionalRetail
XML Files - https://mega.nz/file/p7AR1QCL#lFbnprKOVzjWCZdps4se1L95Jbt2TxSnpVKx7hAqwQ8


### Commands

Keys zu aktivierung bei der Installation
```
27NXC-XW8JQ-GC3V8-TK8CJ-4VR29
GYNKJ-H2J6Q-82B62-8HB4Y-7MF29
VJKVW-NH9QG-P6V6Q-3THDH-CYQ6X
HCGTN-XWF2W-XYRWM-RTWWR-FCXKK
```

Ordner Suchbefehl
```
if exist "%ProgramFiles%\Microsoft Office\Office15\ospp.vbs" set folder="%ProgramFiles%\Microsoft Office\Office15"
if exist "%ProgramFiles(x86)%\Microsoft Office\Office15\ospp.vbs" set folder="%ProgramFiles(x86)%\Microsoft Office\Office15"
```

Befehl zum ändern zur Volume Version
```
for /f %x in ('dir /b *') do cscript %folder%\ospp.vbs /inslic:%x
```

Aktivierungsbefehle für die Volume Version
```
cscript %folder%\ospp.vbs /inpkey:YC7DK-G2NP3-2QQC3-J6H88-GVGXT
cscript %folder%\ospp.vbs /sethst:[KMS SERVER]
cscript %folder%\ospp.vbs /setprt:1688
cscript %folder%\ospp.vbs /act
```

### Infos
Py-KMS - https://github.com/SystemRage/py-kms

Keys - https://github.com/SystemRage/py-kms/blob/master/docs/Keys.md

KMS Server Liste - https://gist.github.com/mokoshalb/b87326bbb62805e94da72f8d0f73f563
