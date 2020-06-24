from pathlib import Path
from jinja2 import Template
from argparse import ArgumentParser
import os
import sqlite3
from util import create_table_to_schema, convert_type_csharp, convert_type_python
import copy
import yaml


template_path = Path(__file__).parent / 'code_template'
client_path = Path(__file__).parent.parent / 'client/Assets/Scripts/Common'
server_path = Path(__file__).parent.parent / 'server/api/app'
protcol_path = Path(__file__).parent.parent / 'protocol'


def _gen_protocol():
    protocol_list = protcol_path.glob('**/*.yml')
    protocol = {}
    for file in protocol_list:
        with open(file) as f:
            d = yaml.safe_load(f)
            for k, v in d.items():
                protocol[k] = v    
    
    protocol_for_cl = copy.deepcopy(protocol)
    for name, p in protocol_for_cl.items():
        if not p['req']:
            p['req'] = {}
        for k, v in p['req'].items():
            p['req'][k] = convert_type_csharp(v)
        if not p['res']:
            p['res'] = {}
        for k, v in p['res'].items():
            p['res'][k] = convert_type_csharp(v)
            
    protocol_for_sv = copy.deepcopy(protocol)
    for name, p in protocol_for_sv.items():
        if not p['req']:
            p['req'] = {}
        for k, v in p['req'].items():
            p['req'][k] = convert_type_python(v)
        if not p['res']:
            p['res'] = {}
        for k, v in p['res'].items():
            p['res'][k] = convert_type_python(v)
        
    with open(template_path / 'Protocol.cs.j2') as f:
        template = f.read()
    t = Template(template)
    r = t.render(protocol=protocol_for_cl)
    with open(client_path / 'Protocol.cs', 'w') as f:
        f.write(r)
        
    with open(template_path / 'protocol.py.j2') as f:
        template = f.read()
    t = Template(template)
    r = t.render(protocol=protocol_for_sv)
    with open(server_path / 'protocol.py', 'w') as f:
        f.write(r)
        
    with open(template_path / 'route.py.j2') as f:
        template = f.read()
    t = Template(template)
    r = t.render(protocol=protocol_for_sv)
    with open(server_path / 'route.py', 'w') as f:
        f.write(r)


def _gen_masterdata_code(md_path: Path):    
    conn = sqlite3.connect(md_path)
    md_tables = [
        {
        'name': x[1],
        'schema': x[4]
        } for x in conn.execute('SELECT * FROM sqlite_master') 
        if x[0] == 'table'
    ]
    
    tables = []
    for table in md_tables:
        schema = create_table_to_schema(table['schema'])
        tables.append(schema)
        
    tables_for_cl = copy.deepcopy(tables)
    for table in tables_for_cl:
        for col in table['columns']:
            col['type'] = convert_type_csharp(col['type'])
        
    with open(template_path / 'MasterData.cs.j2') as f:
        template = f.read()
    t = Template(template)
    r = t.render(tables=tables_for_cl)
    with open(client_path / 'MasterData.cs', 'w') as f:
        f.write(r)
        
    tables_for_sv = copy.deepcopy(tables)
    for table in tables_for_sv:
        for col in table['columns']:
            col['type'] = convert_type_python(col['type'])
        
    with open(template_path / 'masterdata.py.j2') as f:
        template = f.read()
    t = Template(template)
    r = t.render(tables=tables_for_sv)
    with open(server_path / 'masterdata.py', 'w') as f:
        f.write(r)

def main(args):
    print('start to generate code')
    md_path = args.masterdata_path
    _gen_masterdata_code(md_path)
    _gen_protocol()
    print('finish to generate code')


if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('--masterdata-path', type=str, required=True)
    args = parser.parse_args()
    main(args)