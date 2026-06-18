@echo off
setlocal
cd /d "%~dp0"

if exist "StroyMaterials.App\bin" rmdir /s /q "StroyMaterials.App\bin"
if exist "publish\win-x64\Data\stroymaterials.db" copy /y "StroyMaterials.App\Data\stroymaterials.db" "publish\win-x64\Data\stroymaterials.db" >nul

echo Локальная база данных сброшена до исходного состояния.
pause
