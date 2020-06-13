from pathlib import Path
import boto3
from argparse import ArgumentParser
import os

AWS_ACCESS_KEY = os.environ['AWS_ACCESS_KEY']
AWS_SECRET_KEY = os.environ['AWS_SECRET_KEY']
    

def main(args):
    root_dir = Path(args.root_dir)
    bucket_name = args.bucket_name
    s3_url = args.s3_url
    print('start to upload')
    s3 = boto3.resource(
        's3',
        endpoint_url=s3_url,
        aws_access_key_id=AWS_ACCESS_KEY,
        aws_secret_access_key=AWS_SECRET_KEY
    )
    
    buckets = [x for x in s3.buckets.all() if x.name == bucket_name]
    if not buckets:
        s3.create_bucket(Bucket=bucket_name)
    bucket = s3.Bucket(bucket_name)
    
    path_list = root_dir.glob('**/*')
    for path in path_list:
        if path.is_dir():
            continue
        relative_path = path.relative_to(root_dir).as_posix()
        bucket.upload_file(str(path), str(relative_path))
        print(relative_path)
    
    print('finish to upload')


if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('--root-dir', type=str, required=True)
    parser.add_argument('--bucket-name', type=str, required=True)
    parser.add_argument('--s3-url', type=str, required=True)
    args = parser.parse_args()
    main(args)