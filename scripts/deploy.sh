#!/bin/bash
set -e

export RANCHER_ENV=processo-eletronico
export RANCHER_START_FIRST=true

export PROJECT=$1
export RANCHER_STACK=$2
export DOCKER_TAG=$3

if [ "$PROJECT" = "app" ]; then
    export RANCHER_SERVICE=processoeletronico-app
    export DOCKER_IMAGE=prodest/processoeletronico-app:$DOCKER_TAG

    echo "Docker build da imagem $DOCKER_IMAGE."
    docker build -t $DOCKER_IMAGE -f ./Dockerfile-app .
else
    export RANCHER_SERVICE=processoeletronico-api
    export DOCKER_IMAGE=prodest/processoeletronico-api:$DOCKER_TAG

    echo "Docker build da imagem $DOCKER_IMAGE."
    docker build -t $DOCKER_IMAGE -f ./Dockerfile-api .
fi

# Deploy Rancher
docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"

docker push $DOCKER_IMAGE

#Atualiza a infra
echo "Deploy no Rancher da imagem $DOCKER_IMAGE, env $RANCHER_ENV, stack $RANCHER_STACK, service $RANCHER_SERVICE."
git clone https://github.com/prodest/api-cloud-v2.git
cd api-cloud-v2
npm install
node ./client --ENVIRONMENT=$RANCHER_ENV \
    --STACK=$RANCHER_STACK --SERVICE=$RANCHER_SERVICE \
    --IMAGE=$DOCKER_IMAGE --START_FIRST=$RANCHER_START_FIRST
