from argparse import ArgumentParser
from pathlib import Path
import sqlite3

MASTARDATA_NAME = 'masterdata.db'


def _convert_header(header):
    for h in header:
        cols = h.split(' ')
        if cols[1] == 'asset':
            cols[1] = 'text'
        yield ' '.join(cols)


def _create_md_table(conn, tsv_path):
    with open(tsv_path) as f:
        header = f.readline().rstrip('\n').split('\t')
        header = list(_convert_header(header))
        table_name = tsv_path.stem
        header_q = ','.join(header)
        conn.execute(f'CREATE TABLE {table_name} ({header_q})')
        query_placeholder = ','.join(['?' for x in header])
        for line in f:
            row = line.rstrip('\n').split('\t')
            conn.execute(f'INSERT INTO {table_name} VALUES ({query_placeholder})', row)
        

def main(args):
    input_dir = Path(args.input_dir)
    output_dir = Path(args.output_dir)
    if not output_dir.exists():
        output_dir.mkdir(parents=True)
    
    tsv_list = list(input_dir.glob("**/*.tsv"))
    output_path = output_dir / MASTARDATA_NAME
    if output_path.exists():
        output_path.unlink()
    
    conn = sqlite3.connect(output_path)
    
    for tsv in tsv_list:
        _create_md_table(conn, tsv) 
    conn.commit()
    
    conn.execute('CREATE TABLE asset (path text, url text)')
    conn.commit()
    
    print(f'generate masterdata in [{output_path}]')
    
    

if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('--input-dir', type=str, required=True)
    parser.add_argument('--output-dir', type=str, required=True)
    args = parser.parse_args()
    main(args)