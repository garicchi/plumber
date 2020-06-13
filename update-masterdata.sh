#!/bin/bash -xeu
set -o pipefail

SCRIPT_PATH=$(cd $(dirname $0); pwd)

python ${SCRIPT_PATH}/tool/gen_masterdata.py \
  --input-dir ${SCRIPT_PATH}/masterdata \
  --output-dir ${SCRIPT_PATH}/server/api/md
