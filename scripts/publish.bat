@echo off
setlocal

if ("%1"=="") goto error

set VERSION=%1
echo %VERSION%
rem dotnet clean
rem dotnet build -c Release
dotnet nuget push .\src\Collections\bin\Release\Collections.NET.%VERSION%.nupkg -s https://api.nuget.org/v3/index.json
goto done

:error
echo Specify the version number

:done
