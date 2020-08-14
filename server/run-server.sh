#!/bin/bash -xe

SCRIPT_PATH=$(cd $(dirname $0); pwd)

cd $SCRIPT_PATH

docker-compose build
docker-compose up
