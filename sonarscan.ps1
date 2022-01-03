dotnet sonarscanner begin /k:"domain-playground" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="5e498f7e509591486370fabc8a62a1e9e9e73e00" /d:sonar.coverageReportPaths=".\sonarqubecoverage\SonarQube.xml" /d:sonar.cs.xunit.reportsPaths="./*.Tests/TestResults/TestResults.xml"

dotnet build

dotnet test --no-build --collect:"XPlat Code Coverage"  --logger:xunit

reportgenerator "-reports:*\TestResults\*\coverage.cobertura.xml" "-targetdir:sonarqubecoverage" "-reporttypes:SonarQube"
 

dotnet sonarscanner end /d:sonar.login="5e498f7e509591486370fabc8a62a1e9e9e73e00"