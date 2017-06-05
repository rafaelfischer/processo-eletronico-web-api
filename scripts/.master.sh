#!/bin/bash

docker tag $DOCKER_IMAGE $DOCKER_IMAGE:$TRAVIS_COMMIT

docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push $DOCKER_IMAGE:$TRAVIS_COMMIT

#Atualiza a infra
git clone https://github.com/prodest/api-cloud-v2.git
cd api-cloud-v2
npm install
node ./client --ENVIRONMENT=SEP/Organograma \
    --STACK=prd --SERVICE=processoeletronico-api \
    --IMAGE=$DOCKER_IMAGE:$TRAVIS_COMMIT --START_FIRST=true