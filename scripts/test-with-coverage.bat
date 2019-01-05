dotnet test .\tests\Collection.Tests\Collection.Tests.csproj /p:CollectCoverage=true /p:Exclude=\"[xunit.*]*\" /p:CoverletOutputFormat=opencover
reportgenerator -reports:.\tests\Collection.Tests\coverage.opencover.xml -targetdir:.\coverage
