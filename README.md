# PLUMBER

APIサーバーありのUnityのミニプロジェクトです

AssetBundleビルドやマスターデータ生成などを一通り体験できます

## Getting started

シークレットファイルを作成します

```
cp server/.env.template server.env
```

STORAGE_URLにはサーバーPCのIPアドレをを指定してください
```
vi server/.env
```

サーバーを起動します
```
./run-server.sh
```

自動コード生成やABビルドを行います(別のターミナル、Unityは閉じておく)
```
./gen-all.sh
```

DBをresetしてcreate tableします

```
./tool/reset-db.sh
```

