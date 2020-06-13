from flask import Flask, send_from_directory
from api_pb2 import Product
from masterdata import *

app = Flask(__name__)


@app.route('/login')
def hello_world():
    a = list(m_home_image.select())
    print(a)
    return a[0].name


@app.route('/product')
def get_product():
    d = Product()
    d.name = "hoge"
    s = d.SerializeToString()
    return s


@app.route('/masterdata')
def get_masterdata():
    return send_from_directory('/var', filename='masterdata.db')


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)