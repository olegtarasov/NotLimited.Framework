@echo off

SET nuSource="%~dp0..\packages\NuSource\tools\NuSource.exe"

if not exist %nuSource% (
	"%~dp0..\.nuget\NuGet.exe" "Install" "NuSource" "-OutputDirectory" "%~dp0..\packages" "-ExcludeVersion"
)

call %nuSource% ProcessPackage -path="%~dp0Packages\NotLimited.Framework.Common.PublicPackage\NuSource.xml"
call %nuSource% ProcessPackage -path="%~dp0Packages\NotLimited.Framework.Wpf.PublicPackage\NuSource.xml"