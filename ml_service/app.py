from flask import Flask, request, jsonify
from flasgger import Swagger
import pickle
import numpy as np
import requests

app = Flask(__name__)
Swagger(app)  # Yeh line zaroori hai Swagger ke liye

def build_ann_model():
    pass

with open('hybrid_house_model.pkl', 'rb') as file:
    data_loaded = pickle.load(file)
    model = data_loaded['random_forest']

@app.route('/predict', methods=['POST'])
def predict():
    """
    House Price Prediction
    ---
    parameters:
      - name: body
        in: body
        required: true
        schema:
          properties:
            totalArea:
              type: number
            bedrooms:
              type: integer
            latitude:
              type: number
            longitude:
              type: number
    """
    data = request.get_json()
    features = np.array([[
        data['totalArea'], 
        data['bedrooms'], 
        data['latitude'], 
        data['longitude']
    ]])
    prediction = model.predict(features)
    return jsonify({'predictedPrice': float(prediction[0])})


@app.route("/predict-trend", methods=["POST"])
def predict_trend():
  """Real Estate Trend Prediction (Local Fallback)

  ---
  parameters:
    - name: body
      in: body
      required: true
      schema:
        properties:
          inputs:
            type: string
  """
  data = request.get_json()
  text = data.get("inputs", "")
  # Local response jo bina net ke foran chal jayega
  result = (
      f"Trend Analysis: Real estate demand for '{text}' is projected to grow"
      " steadily by 12% over the next fiscal year due to urban expansion."
  )
  return jsonify({"generated_text": result})


@app.route("/predict-safety", methods=["POST"])
def predict_safety():
  """Neighborhood Safety Analysis (Local Fallback)

  ---
  parameters:
    - name: body
      in: body
      required: true
      schema:
        properties:
          inputs:
            type: string
  """
  data = request.get_json()
  text = data.get("inputs", "")
  # Local sentiment response
  return jsonify([{
      "label": "Positive",
      "score": 0.95,
      "message": "The area is evaluated as safe and family-friendly.",
  }])

if __name__ == '__main__':
    app.run(port=5000, debug=True)