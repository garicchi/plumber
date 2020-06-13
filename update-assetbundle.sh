#!/bin/bash -xeu
set -o pipefail

SCRIPT_PATH=$(cd $(dirname $0); pwd)

source ${SCRIPT_PATH}/server/.env

python ${SCRIPT_PATH}/tool/batch_unity.py \
  --project-path ${SCRIPT_PATH}/../client \
  --execute-method AssetBundleBuilder.BuildAssetBundle

export AWS_ACCESS_KEY=${AWS_ACCESS_KEY}
export AWS_SECRET_KEY=${AWS_SECRET_KEY}

python ${SCRIPT_PATH}/tool/upload_s3.py \
  --root-dir ${SCRIPT_PATH}/../client/AssetBundle \
  --bucket-name ${BUCKET_NAME} \
  --s3-url http://localhost:9000


