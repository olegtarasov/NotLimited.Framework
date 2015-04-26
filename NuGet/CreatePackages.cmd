@echo off

SET solutionPath=%~dp0..
SET nuSource="%~dp0..\packages\NuSource\tools\NuSource.exe"

if not exist %nuSource% (
	"%~dp0..\.nuget\NuGet.exe" "Install" "NuSource" "-OutputDirectory" "%~dp0..\packages" "-ExcludeVersion"
)

call %nuSource% ProcessPackage -s="%solutionPath%" -p="NotLimited.Framework.Common" -i=false -n=false -f="NotLimited.Framework.Common.PublicPackage"
call %nuSource% ProcessPackage -s="%solutionPath%" -p="NotLimited.Framework.Wpf" -i=false -n=false -f="NotLimited.Framework.Wpf.PublicPackage"