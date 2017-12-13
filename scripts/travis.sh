#!/bin/bash
set -e

cd ProcessoEletronicoWebAPI/src/WebAPI/
dotnet restore
dotnet publish -c release -r debian.8-x64 -o publish ./

cd ../../../

cd ProcessoEletronicoWebAPI/src/WebAPP/

bower install

dotnet restore 
dotnet publish -c release -r debian.8-x64 -o publish ./

cd ../../../
