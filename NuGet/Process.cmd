@echo off

SET solutionPath=%~dp0..
SET nuSourceCmd="%~dp0NuSource\NuSource.exe"
SET nugetCmd="%solutionPath%\.nuget\NuGet.exe"
SET packagesPath=%~dp0Packages

@echo Processing packages...
%nuSourceCmd% -s="%solutionPath%" -p="NotLimited.Framework.Common"
%nuSourceCmd% -s="%solutionPath%" -p="NotLimited.Framework.Common" -i=false -n=false -w=false -a=false -f="NotLimited.Framework.Common.Public"

del %packagesPath%\*.nupkg

@echo Creating packages...
%nugetCmd% pack "%packagesPath%\NotLimited.Framework.Common\NotLimited.Framework.Common.nuspec" -OutputDirectory "%packagesPath%"
%nugetCmd% pack "%packagesPath%\NotLimited.Framework.Common.Public\NotLimited.Framework.Common.Public.nuspec" -OutputDirectory "%packagesPath%"

@echo Pushing packages...
cd %packagesPath%
for /r %%i in (*.nupkg) do (
	echo %%i
	%nugetCmd% push %%i
)