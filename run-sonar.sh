#!/bin/bash

rm -rf .sonarqube
find . -name '*.opencover.xml' -exec rm {} \;

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

FILES=$(find . -type f -name "*.opencover.xml")

dotnet sonarscanner begin \
/d:sonar.host.url="https://sonarcloud.io" \
/o:jacksonveroneze \
/d:sonar.login=d7f50518d0cc81c1ef1bc03a04031be58ae71e3d \
/k:xUnitTestSamples \
/d:sonar.cs.opencover.reportsPaths="01-Unit-Tests/xUnitTestSamples.Features.Tests/coverage.opencover.xml, 01-Unit-Tests/xUnitTestSamples.Basic.Tests/coverage.opencover.xml, 02-TDD/tests/NerdStore.Vendas.Domain.Tests/coverage.opencover.xml" \
/d:sonar.exclusions="01-Unit-Tests/xUnitTestSamples.Features.Tests/**, 01-Unit-Tests/UnitTestSamples.Features.Tests/**, 02-TDD/tests/NerdStore.Vendas.Application.Tests/**, 02-TDD/tests/NerdStore.Vendas.Domain.Tests/**" \
/d:sonar.coverage.exclusions="01-Unit-Tests/xUnitTestSamples.Features.Tests/**, 01-Unit-Tests/UnitTestSamples.Features.Tests/**, 02-TDD/tests/NerdStore.Vendas.Application.Tests/**, 02-TDD/tests/NerdStore.Vendas.Domain.Tests/**" \
/v:1.0.0

dotnet build

dotnet sonarscanner end /d:sonar.login=d7f50518d0cc81c1ef1bc03a04031be58ae71e3d