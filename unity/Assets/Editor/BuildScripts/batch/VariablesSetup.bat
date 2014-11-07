if not "%UNITY_FOLDER%"=="" GOTO :label_after_unity_folder
set UNITY_FOLDER=C:\Program Files (x86)\Unity\Editor\Unity.exe
:label_after_unity_folder
set BUILD_FOLDER=C:\build
set PROJECT_NAME=flaming-bear

SET BUILD_NAME_NO_EXT=%BUILD_FOLDER%\%PROJECT_NAME%\%BUILD_TARGET%\%BUILD_VERSION%\%PROJECT_NAME%_%BUILD_VERSION%

set PREVIOUS_CD=%CD%
cd .\..\..\..\
set PROJECT_PATH=%CD%
cd %PREVIOUS_CD%