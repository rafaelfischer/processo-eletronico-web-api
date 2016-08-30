#!/bin/bash
 
docker rename $DOCKER_IMAGE "$DOCKER_IMAGE-dev"

docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push "$DOCKER_IMAGE-dev"

git clone https://github.com/prodest/gerencio-upgrade.git
cd gerencio-upgrade
npm install
node ./client $SERVICE_NAME 40000