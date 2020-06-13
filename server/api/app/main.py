from flask import Flask, send_from_directory
app = Flask(__name__)


@app.route('/login')
def hello_world():
    return 'Hello, World!'


@app.route('/masterdata')
def get_masterdata():
    return send_from_directory('/var', filename='masterdata.db')


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)