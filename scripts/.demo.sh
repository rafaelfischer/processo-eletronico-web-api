#!/bin/bash

docker build -t $DOCKER_IMAGE -f ./Dockerfile .

docker login -u="$DOCKER_USERNAME" -p="$DOCKER_PASSWORD"
docker push $DOCKER_IMAGE

export RANCHER_ENV_ID=1a10541 #env processoeletronico (1a10541)
export RANCHER_STACK_ID=1e256 #stack demo (1e256)
export RANCHER_STACK=demo #stack demo (1e256)

export RANCHER_URL=http://cloud.datacenter.es.gov.br.local/v1/projects/$RANCHER_ENV_ID
export RANCHER_COMPOSE_URL=http://cloud.datacenter.es.gov.br.local/v1/projects/$RANCHER_ENV_ID/environments/$RANCHER_STACK_ID/composeconfig

#Atualiza a infra
git clone https://github.com/prodest/gerencio-upgrade.git
cd gerencio-upgrade
npm install
node ./client $RANCHER_SERVICE_NAME 40000