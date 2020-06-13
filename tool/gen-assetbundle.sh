#!/bin/bash -eu
set -o pipefail

SCRIPT_PATH=$(cd $(dirname $0); pwd)

source ${SCRIPT_PATH}/../server/.env

python ${SCRIPT_PATH}/batch_unity.py \
  --project-path ${SCRIPT_PATH}/../client \
  --execute-method AssetBundleBuilder.BuildAssetBundle

docker build -t plumber-build ${SCRIPT_PATH}

export MSYS_NO_PATHCONV=1

docker run -it -v ${SCRIPT_PATH}/../:/work plumber-build \
  python /work/tool/upload_s3.py \
    --masterdata-path /work/server/api/md/masterdata.db \
    --bucket-name ${BUCKET_NAME} \
    --aws-access-key ${AWS_ACCESS_KEY} \
    --aws-secret-key ${AWS_SECRET_KEY} \
    --s3-url http://host.docker.internal:9000

echo "success to generate assetbundle"