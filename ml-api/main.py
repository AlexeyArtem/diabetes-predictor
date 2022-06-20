import numpy as np
import sklearn
from flask import Flask, jsonify, request, abort
from keras.models import load_model
from joblib import load

app = Flask(__name__)
model = load_model('neural_pred_diabet.h5')
regr_model = load('regr_model.joblib')
name_attributes = ["HighBP", "HighChol", "CholCheck", "BMI", "Smoker", "Stroke", "HeartDiseaseorAttack", "PhysActivity",
                   "Fruits", "Veggies", "HvyAlcoholConsump", "GenHlth", "MentHlth", "PhysHlth", "DiffWalk", "Sex",
                   "Age"]


@app.route('/')
def index():
    return '<h1>Welcome to API for predict diabetes</h1>' \
           '<br><a href="https://github.com/AlexeyArtem/diabetes-predictor" target="_blank">GitHub link</a>'


@app.route('/predict', methods=['POST'])
def get_predict():
    if not request.json or not request.is_json:
        abort(400, 'The request content is not a json format')

    try:
        data_json = request.get_json()
        input_data = []
        for attribute in name_attributes:

            if attribute not in data_json:
                abort(400, 'Key ' + attribute + ' not found in json request content')

            input_data.append(data_json[attribute])

        probabilities = model.predict(np.array([input_data]))
        p = float(probabilities[0][1])

        result = {'Probability_diabetes': p}
        return jsonify(result)

    except:
        abort(400)


@app.route('/risk-coefficients', methods=['GET'])
def get_coefficients():

    resultJson = { }
    coefs = regr_model.coef_.ravel()
    for i in range(coefs.size):
        resultJson[name_attributes[i]] = coefs[i]

    return jsonify(resultJson)


if __name__ == '__main__':
    print(sklearn.__version__)
    app.run()
