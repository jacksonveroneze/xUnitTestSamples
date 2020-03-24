#!/bin/bash

rm -rf .sonarqube
rm 01-Unit-Tests/xUnitTestSamples.Features.Tests/coverage.opencover.xml
rm 01-Unit-Tests/xUnitTestSamples.Basic.Tests/coverage.opencover.xml

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover


dotnet sonarscanner begin \
/d:sonar.host.url="https://sonarcloud.io" \
/o:jacksonveroneze \
/d:sonar.login=d7f50518d0cc81c1ef1bc03a04031be58ae71e3d \
/k:xUnitTestSamples \
/d:sonar.cs.opencover.reportsPaths="01-Unit-Tests/xUnitTestSamples.Features.Tests/coverage.opencover.xml, 01-Unit-Tests/xUnitTestSamples.Basic.Tests/coverage.opencover.xml" \
/d:sonar.exclusions="01-Unit-Tests/xUnitTestSamples.Features/**, 01-Unit-Tests/UnitTestSamples.Features.Tests/**" \
/d:sonar.coverage.exclusions="01-Unit-Tests/xUnitTestSamples.Features/**, 01-Unit-Tests/UnitTestSamples.Features.Tests/**" \
/v:1.0.0

dotnet build

dotnet sonarscanner end /d:sonar.login=d7f50518d0cc81c1ef1bc03a04031be58ae71e3d