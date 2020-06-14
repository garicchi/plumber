#!/bin/bash -eu
set -o pipefail

SCRIPT_PATH=$(cd $(dirname $0); pwd)

docker build -t plumber-build ${SCRIPT_PATH}

export MSYS_NO_PATHCONV=1

docker run -it -v ${SCRIPT_PATH}/../:/work plumber-build \
  python /work/tool/gen_masterdata.py \
    --input-dir /work/masterdata \
    --output-dir /work/server/api/static

echo "success to generate masterdata"