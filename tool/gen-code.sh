#!/bin/bash -eu
set -o pipefail

SCRIPT_PATH=$(cd $(dirname $0); pwd)

docker build -t plumber-build ${SCRIPT_PATH}

export MSYS_NO_PATHCONV=1

docker run -it -v ${SCRIPT_PATH}/../:/work plumber-build \
    python /work/tool/gen_code.py \
      --masterdata-path /work/server/api/static/masterdata.db

echo "success to generate"