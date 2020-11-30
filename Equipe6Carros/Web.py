import os
import pickle
import pandas as pd
from sklearn.ensemble import RandomForestRegressor
from flask import Flask, render_template, request, redirect, url_for, jsonify


app = Flask(__name__)
@app.route('/predicao', methods=['POST'])
def index():
    req = request.json

    dados = dict()
    dados["Ano"] = [int(req["ano"])]
    dados["MesRef"] = [int(req["mesRef"])]
    dados["AnoRef"] = [int(req["anoRef"])]
    dados["DiffAno"] = [(int(req["anoRef"]) - int(req["ano"]))]

    for combustivel in combustiveis:
        if (req["combustivel"] == combustivel):
            dados[combustivel] = [1]
        else:
            dados[combustivel] = [0]

    for marca in marcas:
        if (req["marca"] == marca):
            dados[marca] = [1]
        else:
            dados[marca] = [0]

    result = pd.DataFrame.from_dict(dados, orient='columns')

    return jsonify(list(forest.predict(result))[0])

    """return jsonify(
        ano=req["ano"],
        mesAnoRef=req["mesAnoRef"],
        combustivel=req["combustivel"],
        marca=req["marca"]
    )"""

@app.route('/marcas', methods=['GET'])
def marcas():
    return jsonify(list(marcas))

@app.route('/combustiveis', methods=['GET'])
def combustiveis():
    return jsonify(list(combustiveis))

if __name__ == '__main__':
    port=5123
    marcas = pickle.load(open('colunas.sav', 'rb'))
    combustiveis = pickle.load(open('combustiveis.sav', 'rb'))
    forest = pickle.load(open('rfr.sav', 'rb'))
    app.run(host='0.0.0.0', port=port, threaded=True)
