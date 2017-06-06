#!/bin/bash

docker tag $DOCKER_IMAGE $DOCKER_IMAGE-dev:$DOCKER_TAG

docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push $DOCKER_IMAGE-dev:$DOCKER_TAG

#Atualiza a infra
git clone https://github.com/prodest/api-cloud-v2.git
cd api-cloud-v2
npm install
node ./client --ENVIRONMENT=SEP/Organograma \
    --STACK=dev --SERVICE=processoeletronico-api \
    --IMAGE=$DOCKER_IMAGE-dev:$DOCKER_TAG --START_FIRST=true