from sqlalchemy import create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import relationship
from sqlalchemy import Column, Integer, String, ForeignKey
from argparse import ArgumentParser
import os

DB_HOST = os.environ['DB_HOST']
DB_USER = os.environ['DB_USER']
DB_PASS = os.environ['DB_PASS']
DB_DATABASE = os.environ['DB_DATABASE']

engine = create_engine(f'mysql://{DB_USER}:{DB_PASS}@{DB_HOST}/{DB_DATABASE}', echo=True)
Base = declarative_base()


class User(Base):
    __tablename__ = 'users'
    
    id = Column(Integer, primary_key=True)
    name = Column(String(500))
    children = relationship("Score")


class Score(Base):
    __tablename__ = 'scores'
    id = Column(Integer, primary_key=True, autoincrement=True)
    user_id = Column(Integer, ForeignKey('users.id'))
    score = Column(Integer)
    
    
def reset(args):
    Base.metadata.create_all(engine)
    

if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('COMMAND', choices=['RESET'])
    args = parser.parse_args()
    if args.COMMAND == 'RESET':
        reset(args)
        print('resetdb completed!')
    else:
        raise Exception("invalid argument")
        
