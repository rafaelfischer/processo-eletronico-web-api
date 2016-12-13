#!/bin/bash

docker build -t $DOCKER_IMAGE-hmg -f ./Dockerfile .

docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push $DOCKER_IMAGE-hmg

export RANCHER_URL=http://cloud.datacenter.es.gov.br.local/v1/projects/1a10541 #env processoeletronico (1a10541)
export RANCHER_COMPOSE_URL=http://cloud.datacenter.es.gov.br.local/v1/projects/1a10541/environments/1e102/composeconfig #stack dev (1e102)
export RANCHER_STACK=hmg #stack hmg (1e102)
export RANCHER_SERVICE_NAME=processoeletronico-api

#Atualiza a infra
git clone https://github.com/prodest/gerencio-upgrade.git
cd gerencio-upgrade
npm install
node ./client $RANCHER_SERVICE_NAME 40000
