@echo off

set nuSource="%~dp0..\packages\NuSource\tools\PublishPackage.cmd"

if not exist %nuSource% (
	"%~dp0..\.nuget\NuGet.exe" "Install" "NuSource" "-OutputDirectory" "%~dp0..\packages" "-ExcludeVersion"
)

call %nuSource% NotLimited.Framework.Common
call %nuSource% NotLimited.Framework.Common public
call %nuSource% NotLimited.Framework.Wpf public