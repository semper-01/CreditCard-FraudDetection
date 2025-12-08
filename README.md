# CreditCard-FraudDetection
use machine learning to detect the credit card frauds and stop it

# Credit Card Fraud Detection Project
 ##Project Description
This project focuses on building a robust fraud detection system for credit card transactions. Students will work with anonymized transaction data to train models that identify potentially fraudulent activity. 

# Module 1: Use the Kaggle dataset to build a complete pipeline from scratch to a live web demo.
The emphasis is on feature engineering, handling imbalanced datasets, training classification models, and deploying a professional web demo.

#Requirements
```
   #Languages:
.Python → ML pipeline
.C# → ASP.NET Core backend for web demo

   #Frameworks & Libraries:
.Python: pandas, numpy, scikit-learn, xgboost, imbalanced-learn, shap
.Web: ASP.NET Core, Blazor WebAssembly

   #Data Sources:
.Kaggle Credit Card Fraud Detection dataset (initial)

  #Tools:
.Git for version control
.Redis for feature service
.Azure/Heroku for hosting
 ```
 # Module : Kaggle Dataset to Live Web Demo

# Step 1: Data Acquisition
```
.Download Kaggle dataset (creditcard.csv).
.Inspect schema: Time, V1–V28 (PCA features), Amount, Class (fraud label).
```
# Step 2: Data Preprocessing
```
.Normalize Amount using log transform.
.Handle imbalance: undersampling, oversampling, class weights.
.Split data with time-aware strategy (train/validation/test).
```
# Step 3: Feature Engineering
```
.Use PCA features directly.
.Add derived features: amount_log, time-of-day, day-of-week.
```
# Step 4: Model Training
```
.Train baseline Logistic Regression.
.Train advanced models: XGBoost, LightGBM, CatBoost.
.Evaluate using Precision, Recall, F1, PR AUC
```
# Step 5: Threshold Tuning
```
.Select decision threshold based on cost curve.
.Balance false positives vs false negatives.
```
# Step 6: Fraud Action Policy
```
.Low risk: Approve transaction.
.Medium risk: Step-up verification (OTP, device check).
.High risk: Hold and send to manual review.
```
# Step 7: Web Demo Implementation
```
.Backend: ASP.NET Core API with endpoints for scoring, review queue, metrics.
.Frontend: Blazor WebAssembly dashboard with transaction feed, risk badges, SHAP explanations.
.Model Service: Python FastAPI hosting trained model.
.Feature Service: Redis counters for velocity features.
```

# Step 8: Monitoring & Retraining
.Monitor Precision, Recall, PR AUC.
Detect data drift with PSI/KS tests.
Retrain weekly or when drift detected.
