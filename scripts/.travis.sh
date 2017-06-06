#!/bin/bash

cd ProcessoEletronicoWebAPI/src/WebAPI/
dotnet restore && dotnet publish -c release -r debian.8-x64 -o publish ./

cd ../../../

docker build -t $DOCKER_IMAGE -f ./Dockerfile .
export DOCKER_TAG=${TRAVIS_COMMIT:0:7}
