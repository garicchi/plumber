from flask import request, g
from protocol import *
from masterdata import *
from urllib.parse import unquote
import random
from db import User, Score
import logging
logging.basicConfig(level=logging.DEBUG)


def api_login():
    request_data = request.get_data()
    payload = unquote(request_data.decode())
    session = g.session
    req = api_login_req.deserialize(payload)
    res = api_login_res()
    # ユーザーIDがない場合は新規ユーザーなのでuser idを発行する
    user = User()
    if not req.user_id:
        user.id = random.randint(0, 100000)
        session.add(user)
        session.commit()
    else:
        user = list(session.query(User).filter(User.id == req.user_id))[0]
    
    res.user_id = user.id
    
    return res.serialize()


def api_finish_game():
    request_data = request.get_data()
    payload = unquote(request_data.decode())
    session = g.session
    req = api_finish_game_req.deserialize(payload)
    
    user = list(session.query(User).filter(User.id == req.user_id))[0]
    score = Score()
    score.user_id = user.id
    score.score = req.score
    session.add(score)
    session.commit()
    
    res = api_finish_game_res()
    return res.serialize()
    