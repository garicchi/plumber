#!/bin/bash -eu
set -o pipefail

SCRIPT_PATH=$(cd $(dirname $0); pwd)

source ${SCRIPT_PATH}/../server/.env

export MSYS_NO_PATHCONV=1

if [[ $OCHESTRATION = "k8s" ]]; then
  POD_NAME=$(kubectl get pods -o NAME -l app=plumber-batch | head -n 1)
  kubectl exec -it ${POD_NAME} -- python /app/db.py RESET
else
  API_CONTAINER=$(docker ps -f "ancestor=server_api" -q)
  docker exec -it ${API_CONTAINER} python /app/db.py RESET
fi
