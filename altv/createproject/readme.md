
# Alt:V Create Project

Hier der versprochende Code:


```
    <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <AppendRuntimeIdentifierToOutputPath>false</AppendRuntimeIdentifierToOutputPath>

    <BaseOutputPath>C:\PATH</BaseOutputPath>
    <OutputPath></OutputPath>
```
# Install

https://visualstudio.microsoft.com/de/vs/

https://code.visualstudio.com/

https://nodejs.org/en/download/


```
npm i @altv/types-client
```
```
npm i @altv/types-natives
```
```
npm i @altv/types-webview
```

```
/// <reference types="@altv/types-client" />
/// <reference types="@altv/types-natives" />

import * as alt from 'alt-client';
import * as native from 'natives';
```

# TypeScript
https://github.com/Stuyk/altv-typescript
