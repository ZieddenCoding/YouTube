# Windows Server 2012 R2 Installieren

###### Was wird benötigt?
[Exchange Setup](https://www.microsoft.com/de-DE/download/details.aspx?id=58392 "Exchange Setup")
[Visual C++ Redistributable Packages for Visual Studio 2013](https://www.microsoft.com/en-us/download/details.aspx?id=40784 "Visual C++ Redistributable Packages for Visual Studio 2013")
[.NET Framework 4.7.2 (Direkt Download)](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net472-web-installer ".NET Framework 4.7.2 (Direkt Download)")
[Unified Communications Managed API 4.0 Runtime](https://www.microsoft.com/en-us/download/details.aspx?id=34992 "Unified Communications Managed API 4.0 Runtime")
[Microsoft Office 2010 Filter Packs](https://www.microsoft.com/en-us/download/details.aspx?id=17062 "Microsoft Office 2010 Filter Packs")
[Service Pack 2 für Microsoft Office 2010 Filter Pack (KB2687447) 64-Bit-Edition](https://www.microsoft.com/de-de/download/confirmation.aspx?id=39668 "Service Pack 2 für Microsoft Office 2010 Filter Pack (KB2687447) 64-Bit-Edition")

###### Installations Command
```shell
Install-WindowsFeature AS-HTTP-Activation, Desktop-Experience, NET-Framework-45-Features, RPC-over-HTTP-proxy, RSAT-Clustering, Web-Mgmt-Console, WAS-Process-Model, Web-Asp-Net45, Web-Basic-Auth, Web-Client-Auth, Web-Digest-Auth, Web-Dir-Browsing, Web-Dyn-Compression, Web-Http-Errors, Web-Http-Logging, Web-Http-Redirect, Web-Http-Tracing, Web-ISAPI-Ext, Web-ISAPI-Filter, Web-Lgcy-Mgmt-Console, Web-Metabase, Web-Mgmt-Console, Web-Mgmt-Service, Web-Net-Ext45, Web-Request-Monitor, Web-Server, Web-Stat-Compression, Web-Static-Content, Web-Windows-Auth, Web-WMI, Windows-Identity-Foundation
```

###### Keys
[Microsoft Static Keys](https://gist.github.com/jamesy0ung/186bcc47ffe59a126052ee0f1cded8b3 "Microsoft Static Keys")
```shell
Exchange Server 2013 Enterprise: MV2FQ-2MVJD-WK2VK-CB8XP-3Q2D9
Exchange Server 2013 Standard: CPJFG-C9D94-J7F4K-T9Q48-FWKP7
```
