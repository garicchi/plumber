#!/bin/bash -eu
set -o pipefail

SCRIPT_PATH=$(cd $(dirname $0); pwd)

source ${SCRIPT_PATH}/../server/.env

export MSYS_NO_PATHCONV=1

API_CONTAINER=$(docker ps -f "ancestor=server_api" -q)

docker exec -it ${API_CONTAINER} mysql -hdb -u${DB_USER} -p${DB_PASS} ${DB_DATABASE}
