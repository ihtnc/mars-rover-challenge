@echo off

echo Starting web client...
start "Web client" /D "rovercmd" "cmd /C npm start"

echo Starting api...
start "Web server" /D "rovercmd.api" "cmd /C dotnet run --"
