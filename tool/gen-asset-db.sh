#!/bin/bash -eu
set -o pipefail

SCRIPT_PATH=$(cd $(dirname $0); pwd)

source ${SCRIPT_PATH}/../server/.env

docker build -t plumber-build ${SCRIPT_PATH}

export MSYS_NO_PATHCONV=1

docker run -it -v ${SCRIPT_PATH}/../:/work plumber-build \
  python /work/tool/gen_asset_db.py \
    --sqlite-path /work/server/api/static/masterdata.db \
    --assetbundle-root-path /work/client/AssetBundles \
    --masterdata-root-path /work/masterdata

echo "success to generate url"