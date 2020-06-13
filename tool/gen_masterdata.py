from argparse import ArgumentParser
from pathlib import Path
import sqlite3

MASTARDATA_NAME = 'masterdata.db'


def _convert_query(header_cell, body_cell):
    header_name, header_type = header_cell.split(' ')
    if header_type in ['text']:
        return f"'{body_cell}'"
    return body_cell


def _create_table(conn, tsv_path):
    with open(tsv_path) as f:
        header = f.readline().rstrip('\n').split('\t')
        table_name = tsv_path.stem
        header_q = ','.join(header)
        conn.execute(f'CREATE TABLE {table_name} ({header_q})')
        for line in f:
            row = line.rstrip('\n').split('\t')
            row = [_convert_query(header[i], c) for i, c in enumerate(row)]
            row_q = ','.join(row)
            conn.execute(f'INSERT INTO {table_name} VALUES ({row_q})')
        

def main(args):
    input_dir = Path(args.input_dir)
    output_dir = Path(args.output_dir)
    
    if not output_dir.exists():
        output_dir.mkdir(parents=True)
    output_path = output_dir / MASTARDATA_NAME
    if output_path.exists():
        output_path.unlink()
    
    conn = sqlite3.connect(output_path)
    
    tsv_list = input_dir.glob("*.tsv")
    for tsv in tsv_list:
        _create_table(conn, tsv) 
        conn.commit()
    print(f'generate masterdata in [{output_path}]')
    
    

if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('--input-dir', type=str, required=True)
    parser.add_argument('--output-dir', type=str, required=True)
    args = parser.parse_args()
    main(args)