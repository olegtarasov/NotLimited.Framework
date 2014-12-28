@echo off

SET solutionPath="%~dp0\.."
SET nuSourceCmd="%~dp0NuSource\NuSource.exe"

%nuSourceCmd% -s="%solutionPath%" -p="NotLimited.Framework.Common"
%nuSourceCmd% -s="%solutionPath%" -p="NotLimited.Framework.Common" -i=false -n=false -w=false -a=false -f="NotLimited.Framework.Common.Public"