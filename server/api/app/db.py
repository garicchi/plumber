from sqlalchemy import create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import Column, Integer, String
from argparse import ArgumentParser
from sqlalchemy.orm import sessionmaker
import os

DB_USER = os.environ['DB_USER']
DB_PASS = os.environ['DB_PASS']
DB_DATABASE = os.environ['DB_DATABASE']

engine = create_engine(f'mysql://{DB_USER}:{DB_PASS}@db/{DB_DATABASE}', echo=True)
Base = declarative_base()

session = sessionmaker()
session.configure(bind=engine)

class User(Base):
    __tablename__ = 'users'
    
    id = Column(Integer, primary_key=True)
    name = Column(String(500))
    
    
def reset(args):
    Base.metadata.create_all(engine)
    

if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('COMMAND', choices=['RESET'])
    args = parser.parse_args()
    if args.COMMAND == 'RESET':
        reset(args)
    else:
        raise Exception("invalid argument")
        