@ECHO OFF

:: --- Build Version ---
set BUILD_VERSION=%1
GOTO prepareBuild

:missingBUILD_VERSION
cls
echo.
echo Select Version # (default : 0.0.0)
echo.
echo.

:promptBUILD_VERSION
set /P VersionChoice=[x.x.x]: 
echo.

if "%VersionChoice%"=="" GOTO chooseBUILD_VERSIONDefault
GOTO chooseBUILD_VERSIONSelect

:chooseBUILD_VERSIONDefault
set BUILD_VERSION=0.0.0
GOTO prepareBuild

:chooseBUILD_VERSIONSelect
set BUILD_VERSION=%VersionChoice%
GOTO prepareBuild


:: --- BUILD_TARGET ---

:missingBuildTarget
echo.
echo Select BUILD_TARGET (default: Windows)
echo.
echo [1] Windows 64
echo [2] Windows 32
echo [3] Web
echo [4] Android (PRO ONLY)
echo [5] IOS (PRO ONLY)
echo.

:promptBuildTarget
set /P BuildTargetChoice=[Choice]: 
echo.

if "%BuildTargetChoice%"=="" GOTO chooseWindows64
if "%BuildTargetChoice%"=="1" GOTO chooseWindows64
if "%BuildTargetChoice%"=="2" GOTO chooseWindows32
if "%BuildTargetChoice%"=="3" GOTO chooseWeb
REM if "%BuildTargetChoice%"=="4" GOTO chooseAndroid
REM if "%BuildTargetChoice%"=="5" GOTO chooseIos
GOTO missingBuildTarget

:chooseWindows64
set BUILD_TARGET=windows64
set BUILD_SCRIPT=.\batch\BuildWindows64.bat
GOTO prepareBuild

:chooseWindows32
set BUILD_TARGET=windows32
set BUILD_SCRIPT=.\batch\BuildWindows32.bat
GOTO prepareBuild

:chooseWeb
set BUILD_TARGET=web
set BUILD_SCRIPT=.\batch\BuildWeb.bat
GOTO prepareBuild

:chooseAndroid
set BUILD_TARGET=android
set BUILD_SCRIPT=.\batch\BuildAndroid.bat
GOTO prepareBuild

:chooseIos
set BUILD_TARGET=ios
set BUILD_SCRIPT=.\batch\BuildIos.bat
GOTO prepareBuild


:prepareBuild
cls
IF "%BUILD_VERSION%"=="" GOTO missingBUILD_VERSION
IF "%BUILD_TARGET%"=="" GOTO missingBuildTarget
GOTO build


:build
CALL .\batch\VariablesSetup.bat
CALL %BUILD_SCRIPT%


pause