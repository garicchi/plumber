from flask import Flask, send_from_directory, g, Response
from flask.views import View
from route import routes
from db import engine
from sqlalchemy.orm import sessionmaker
import logging
logging.basicConfig(level=logging.DEBUG)

app = Flask(__name__)


class ApiRoot(View):
    def __init__(self, func):
        self.func = func
        
    def dispatch_request(self):
        Session = sessionmaker()
        Session.configure(bind=engine)
        session = Session()
        g.session = session
        try:
            res = Response(self.func(), 200)
            session.commit()
        except Exception as e:
            session.rollback()
            logging.debug(e)
            return Response(str(e), 500)
        return res



for rule, endpoint, func in routes:
    app.add_url_rule(
        rule,
        endpoint,
        view_func=ApiRoot.as_view(endpoint, func),
        methods=['POST']
    )
    
    
@app.route('/masterdata', methods=['GET'])
def get_masterdata():
    return send_from_directory('/var/static', filename='masterdata.db')
