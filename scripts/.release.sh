#!/bin/bash

export RANCHER_STACK=hgm
export IMAGE_NAME=$DOCKER_IMAGE-hmg:$DOCKER_TAG

. ./scripts/.deploy.sh