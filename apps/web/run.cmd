@echo off
echo ##############################
echo Cleaning output directories...
del rovercmd\build /Q
del rovercmd.api\wwwroot /Q

echo ##############################
echo Building web client...
cmd /C "cd rovercmd && npm run build"

echo ##############################
echo Copying files...
xcopy rovercmd\build rovercmd.api\wwwroot /S /Y

echo ##############################
echo Building and running api...
cmd /C "cd rovercmd.api && dotnet run"
