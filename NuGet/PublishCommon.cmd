@echo off

SET solutionPath=%~dp0..
SET nuSource="%~dp0..\packages\NuSource\tools\NuSource.exe"

if not exist %nuSource% (
	"%~dp0..\.nuget\NuGet.exe" "Install" "NuSource" "-OutputDirectory" "%~dp0..\packages" "-ExcludeVersion"
)

call %nuSource% Publish -s="%solutionPath%" -p="NotLimited.Framework.Common" -i=false -n=false -a=false -f="NotLimited.Framework.Common.Public"
call %nuSource% Publish -s="%solutionPath%" -p="NotLimited.Framework.Common" -i=false -n=false -f="NotLimited.Framework.Common.PublicPackage"