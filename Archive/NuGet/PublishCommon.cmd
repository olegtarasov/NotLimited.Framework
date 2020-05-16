@echo off

SET nuSource="%~dp0..\packages\NuSource\tools\NuSource.exe"

if not exist %nuSource% (
	"%~dp0..\.nuget\NuGet.exe" "Install" "NuSource" "-OutputDirectory" "%~dp0..\packages" "-ExcludeVersion"
)

call %nuSource% Publish -path="%~dp0Packages\NotLimited.Framework.Common.PublicPackage\NuSource.xml"