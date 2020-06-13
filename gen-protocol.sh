#!/bin/bash -xeu
set -o pipefail

SCRIPT_PATH=$(cd $(dirname $0); pwd)

protoc --python_out=${SCRIPT_PATH}/server/api/app -I=${SCRIPT_PATH}/protocol ${SCRIPT_PATH}/protocol/*.proto
protoc --csharp_out=${SCRIPT_PATH}/client/Assets/Scripts/Common -I=${SCRIPT_PATH}/protocol ${SCRIPT_PATH}/protocol/*.proto