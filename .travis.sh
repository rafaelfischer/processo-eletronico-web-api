#!/bin/bash
 
docker build -t $DOCKER_IMAGE-publico -f ./Dockerfile-publico .
docker build -t $DOCKER_IMAGE-restrito -f ./Dockerfile-restrito .
