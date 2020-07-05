dotnet test --blame /p:CollectCoverage=true /p:Exclude=\"[xunit.*]*\" /p:CoverletOutput=.\.coverage\result /p:CoverletOutputFormat=opencover
reportgenerator -reports:.\**\result.opencover.xml -targetdir:.\.coverage\report -reporttypes:SonarQube
