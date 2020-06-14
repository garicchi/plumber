import re

def str_to_columus(column_str: str, delim=','):
    columns = []
    for c in column_str.split(delim):
        type_infos = c.lstrip(' ').split(' ')
        columns.append({
            'name': type_infos[0],
            'type': type_infos[1],
            'additional': type_infos[2:] if len(type_infos) > 2 else []
        })
    return columns


def columns_to_str(columns: list):
    results = []
    for col in columns:
        results.append(' '.join([col['name'], col['type']] + col['additional']))
    return ','.join(results)


def create_table_to_schema(schema_str: str):
    m = re.findall(r'CREATE TABLE (.*) \((.*)\)', schema_str)[0]
    table_name = m[0]
    columns = str_to_columus(m[1])
    schema = {
        'name': table_name,
        'columns': columns
    }
    return schema


CSHARP_TYPE_MAP = {
    'text': 'string'
}


def convert_type_csharp(type_str: str):
    if type_str in CSHARP_TYPE_MAP:
        return CSHARP_TYPE_MAP[type_str]
    return type_str


PYTHON_TYPE_MAP = {
    'text': 'str'
}


def convert_type_python(type_str: str):
    if type_str in PYTHON_TYPE_MAP:
        return PYTHON_TYPE_MAP[type_str]
    return type_str