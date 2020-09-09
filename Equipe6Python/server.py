import os
from flask import Flask, render_template, request, redirect, url_for, jsonify
from classifier.predictor import Predictor

#### API
app = Flask(__name__)
@app.route('/predict', methods=['POST'])
def index():
    req = request.json
    print("Request received:\r\n\t{}".format(req))
   
    pred,prob = predictor.predict(req["message"])

    print("Results:\r\n\tPrediction: {}\r\n\tProbabilities: {}".format(pred,prob))

    return jsonify(
        message=req["message"],
        predict = pred,
        probability = prob
    )


if __name__ == '__main__':
    #port = int(os.environ.get('PORT', 5000))
    port=5000
    predictor = Predictor('mlp')
    #predictor = Predictor(None)
    app.run(host='0.0.0.0', port=port, threaded=True)