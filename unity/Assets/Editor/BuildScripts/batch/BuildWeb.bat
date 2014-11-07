@ECHO OFF

echo Web Build Starting...

echo Change Version #
"%UNITY_FOLDER%" -batchmode -projectPath "%PROJECT_PATH%" -executeMethod "BuildCommand.SetVersionNumber" -quit -BuildVersion %BUILD_VERSION%

echo Build
"%UNITY_FOLDER%" -batchmode -projectPath "%PROJECT_PATH%" -buildWebPlayer "%BUILD_NAME_NO_EXT%" -quit

echo Web Build Completed
