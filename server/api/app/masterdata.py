import sqlite3

def dict_factory(cursor, row):
    d = {}
    for idx, col in enumerate(cursor.description):
        d[col[0]] = row[idx]
    return d



class m_game_param:
    
    key: str
    
    value: int
    
    @classmethod
    def select(cls, cond: str='', param: str=''):
        con = sqlite3.connect("/var/masterdata.db")
        con.row_factory = dict_factory
        cur = con.cursor()
        for row in cur.execute(f'SELECT * FROM m_game_param {cond}', param):
            r = cls()
            for k, v in row.items():
                r.__setattr__(k, v)
            yield r
        con.close()


class m_home_image:
    
    name: str
    
    asset_path: str
    
    @classmethod
    def select(cls, cond: str='', param: str=''):
        con = sqlite3.connect("/var/masterdata.db")
        con.row_factory = dict_factory
        cur = con.cursor()
        for row in cur.execute(f'SELECT * FROM m_home_image {cond}', param):
            r = cls()
            for k, v in row.items():
                r.__setattr__(k, v)
            yield r
        con.close()


class asset:
    
    path: str
    
    url: str
    
    @classmethod
    def select(cls, cond: str='', param: str=''):
        con = sqlite3.connect("/var/masterdata.db")
        con.row_factory = dict_factory
        cur = con.cursor()
        for row in cur.execute(f'SELECT * FROM asset {cond}', param):
            r = cls()
            for k, v in row.items():
                r.__setattr__(k, v)
            yield r
        con.close()

