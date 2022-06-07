import numpy as np
from flask import Flask, jsonify, request, abort
from keras.models import load_model

app = Flask(__name__)
model = load_model('neural_model_pred_diabet.h5')
name_attributes = [ "HighBP", "HighChol", "CholCheck", "BMI", "Smoker", "Stroke", "HeartDiseaseorAttack", "PhysActivity", "Fruits", "Veggies", "HvyAlcoholConsump", "GenHlth", "MentHlth", "PhysHlth", "DiffWalk", "Sex", "Age", "Education" ]


@app.route('/')
def index():
    return '<h1>Welcome to API for predict diabetes</h1>' \
           '<br><a href="https://github.com/AlexeyArtem/diabetes-predictor" target="_blank">Github link</a>'


@app.route('/predict', methods=['POST'])
def get_predict():
    if not request.json:
        abort(400, 'The request content is not a json format')

    data_json = request.get_json()
    input_data = []
    for attribute in name_attributes:
        if attribute not in data_json:
            abort(400, 'Key ' + attribute + ' not found in json request content')
        input_data.append(data_json[attribute])

    probabilities = model.predict(np.array([input_data]))
    p = float(probabilities[0][1])

    result = {
            'Probability_diabetes': p,
        }
    return jsonify(result)


if __name__ == '__main__':
    app.run()
