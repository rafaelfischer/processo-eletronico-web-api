#!/bin/bash

export RANCHER_STACK=demo
export IMAGE_NAME=$DOCKER_IMAGE:$DOCKER_TAG

. ./scripts/.deploy.sh