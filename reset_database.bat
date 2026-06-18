@echo off
setlocal
cd /d "%~dp0"

if exist "src\StroyMaterials.App\bin" rmdir /s /q "src\StroyMaterials.App\bin"
if exist "publish\win-x64\Data\stroymaterials.db" copy /y "src\StroyMaterials.App\Data\stroymaterials.db" "publish\win-x64\Data\stroymaterials.db" >nul

echo Локальная база данных сброшена до исходного состояния.
pause
