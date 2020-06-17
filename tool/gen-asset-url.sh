#!/bin/bash -eu
set -o pipefail

SCRIPT_PATH=$(cd $(dirname $0); pwd)

source ${SCRIPT_PATH}/../server/.env

docker build -t plumber-build ${SCRIPT_PATH}

export MSYS_NO_PATHCONV=1

docker run -it -v ${SCRIPT_PATH}/../:/work plumber-build \
  python /work/tool/gen_asset_url.py \
    --masterdata-path /work/server/api/static/masterdata.db \
    --bucket-name ${BUCKET_NAME} \
    --aws-access-key ${AWS_ACCESS_KEY} \
    --aws-secret-key ${AWS_SECRET_KEY} \
    --s3-url ${STORAGE_URL}

echo "success to generate url"