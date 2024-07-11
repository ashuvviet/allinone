import os
from flask import Flask
# from werkzeug.urls import url_quote_plus 

app = Flask(__name__)

@app.route('/')
def main():
    return 'Welcome on 27th Jan!'

@app.route('/how are you')
def hello_world():
    return 'I am good, how about you?'

if __name__ == '__main__':
    app.run(debug=True)
