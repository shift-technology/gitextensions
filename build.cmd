@echo off
powershell -ExecutionPolicy ByPass -NoProfile -command "& """%~dp0scripts\build.ps1""" -restore -build -publish -c Release -bl %*"
exit /b %ErrorLevel%
