@echo off
setlocal

if ("%1"=="") goto error

set VERSION=%1
echo %VERSION%
dotnet clean -c Release
dotnet build -c Release -p:Version=%VERSION%
dotnet pack .\src\Collections\Collections.csproj -c Release --include-symbols --include-source -p Version=%VERSION%
dotnet nuget push .\src\Collections\bin\Release\Collections.NET.%VERSION%.nupkg -s https://api.nuget.org/v3/index.json

dotnet clean -c Release_Explicit
dotnet build -c Release_Explicit -p:Version=%VERSION%
dotnet pack .\src\Collections\Collections.csproj -c Release_Explicit --include-symbols --include-source -p Version=%VERSION%
dotnet nuget push .\src\Collections\bin\Release_Explicit\Collections.NET.ExplicitNs.%VERSION%.nupkg -s https://api.nuget.org/v3/index.json
goto done

:error
echo Specify the version number
nuget search Collections.NET -take 1

:done
