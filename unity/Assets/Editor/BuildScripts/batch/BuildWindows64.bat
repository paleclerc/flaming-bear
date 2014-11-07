@ECHO OFF

echo Windows 64 Build Starting...

echo Change Version #
"%UNITY_FOLDER%" -batchmode -projectPath "%PROJECT_PATH%" -executeMethod "BuildCommand.SetVersionNumber" -quit -BuildVersion %BUILD_VERSION%

echo Build
"%UNITY_FOLDER%" -batchmode -projectPath "%PROJECT_PATH%" -buildWindows64Player "%BUILD_NAME_NO_EXT%.exe" -quit

echo Windows 64 Build Completed
