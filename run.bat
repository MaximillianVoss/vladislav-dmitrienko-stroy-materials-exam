@echo off
setlocal
cd /d "%~dp0"

dotnet --list-runtimes | findstr /I "Microsoft.WindowsDesktop.App 8." >nul 2>nul
if errorlevel 1 (
    echo Не найден .NET 8 Windows Desktop Runtime.
    echo Установите runtime: https://dotnet.microsoft.com/download/dotnet/8.0
    pause
    exit /b 1
)

if exist "publish\win-x64\StroyMaterials.App.exe" (
    start "" "publish\win-x64\StroyMaterials.App.exe"
    exit /b 0
)

dotnet --list-sdks >nul 2>nul
if errorlevel 1 (
    echo Не найден .NET SDK для запуска из исходников.
    echo Установите .NET 8 SDK: https://dotnet.microsoft.com/download/dotnet/8.0
    pause
    exit /b 1
)

dotnet run --project "StroyMaterials.App\StroyMaterials.App.csproj"
