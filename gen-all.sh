#!/bin/bash -eu

./tool/gen-masterdata.sh
./tool/gen-code.sh
./tool/gen-assetbundle.sh
./tool/gen-asset-db.sh
./tool/gen-asset-url.sh
