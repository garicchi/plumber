from pathlib import Path
import boto3
from argparse import ArgumentParser
import os
import sqlite3


def main(args):
    md_path = args.masterdata_path
    bucket_name = args.bucket_name
    aws_access_key = args.aws_access_key
    aws_secret_key = args.aws_secret_key
    s3_url = args.s3_url
    print('start to upload')
    s3 = boto3.resource(
        's3',
        endpoint_url=s3_url,
        aws_access_key_id=aws_access_key,
        aws_secret_access_key=aws_secret_key
    )
    
    buckets = [x for x in s3.buckets.all() if x.name == bucket_name]
    if not buckets:
        s3.create_bucket(Bucket=bucket_name)
    client = s3.meta.client
    bucket = s3.Bucket(bucket_name)
    conn = sqlite3.connect(md_path)
    root_dir = Path(__file__).parent.parent / 'client/AssetBundle'
    expire_sec = 60 * 60 * 24 * 30
    for asset in conn.execute('SELECT * FROM asset'):
        asset_path = asset[0]
        file_path = root_dir / asset_path
        bucket.upload_file(str(file_path), str(asset_path))
        url = client.generate_presigned_url(
            'get_object',
            Params={
                'Bucket': bucket_name,
                'Key': asset_path
            },
            ExpiresIn=expire_sec
        )
        conn.execute('UPDATE asset SET url = ? WHERE path = ?', (url, asset_path))
        print(asset_path)
    conn.commit()
    
    print('finish to upload')


if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('--masterdata-path', type=str, required=True)
    parser.add_argument('--bucket-name', type=str, required=True)
    parser.add_argument('--aws-access-key', type=str, required=True)
    parser.add_argument('--aws-secret-key', type=str, required=True)
    parser.add_argument('--s3-url', type=str, required=True)
    args = parser.parse_args()
    main(args)