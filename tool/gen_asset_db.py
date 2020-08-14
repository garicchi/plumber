from pathlib import Path
from argparse import ArgumentParser
import yaml
import re
import sqlite3


PLATFORMS = ['Windows', 'Android']


def _insert_asset_record(conn, path):
    dep_path = str(path) + '.dep'
    conn.execute('INSERT OR IGNORE INTO asset VALUES (?, ?)', (path, ""))
    conn.execute('INSERT OR IGNORE INTO asset VALUES (?, ?)', (dep_path, ""))
    

def _insert_asset_table(conn, tsv_path, assetbundle_root_path):
    with open(tsv_path) as f:
        header = f.readline().rstrip('\n').split('\t')
        header_types = [{
            'name': x.split(' ')[0],
            'type': x.split(' ')[1]
        } for x in header]
        if not [x for x in header_types if x['type'] == 'asset']:
            return
        
        for line in f:
            cells = line.rstrip('\n').split('\t')        
            for i, h in enumerate(header_types):
                if h['type'] != 'asset':
                    continue
                cel = cells[i]
                for p in PLATFORMS:
                    path = p + '/' + cel
                    dep_path = assetbundle_root_path / (str(path) + '.dep')
                    _insert_asset_record(conn, path)
                    if not dep_path.exists():
                        continue
                    with open(dep_path) as f:
                        for line in f:
                            dep_child_path = p + '/' + line
                            _insert_asset_record(conn, dep_child_path)
                conn.commit()


def main(args):
    assetbundle_root_path = Path(args.assetbundle_root_path).resolve()
    masterdata_root_path = Path(args.masterdata_root_path).resolve()
    sqlite_path = Path(args.sqlite_path).resolve()
    for manifest in (assetbundle_root_path).glob("**/*.manifest"):
        with open(manifest) as f:
            manifest_data = yaml.safe_load(f)
        if not 'Dependencies' in manifest_data.keys():
            continue
        deps = manifest_data['Dependencies']
        dep_file = manifest.parent / (manifest.stem + '.dep')
        _deps = []
        for d in deps:
            _d = str(Path(d).as_posix())
            m = re.findall(r'^.*/AssetBundles/(.*)$', _d)
            p = '/'.join(m[0].split('/')[1:])
            _deps.append(p)
        with open(dep_file, 'w') as f:
            f.writelines(_deps)
    
    conn = sqlite3.connect(sqlite_path)
    conn.execute('DELETE FROM asset')
    conn.commit()
    
    tsv_list = list(masterdata_root_path.glob("**/*.tsv"))
    for tsv in tsv_list:
        _insert_asset_table(conn, tsv, assetbundle_root_path) 
    conn.commit()
            
    print('finish to generate dependency')


if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('--assetbundle-root-path', type=str, required=True)
    parser.add_argument('--masterdata-root-path', type=str, required=True)
    parser.add_argument('--sqlite-path', type=str, required=True)
    
    args = parser.parse_args()
    main(args)
