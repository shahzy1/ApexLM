# 🧠 Azure AI Text Analytics API

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](./LICENSE)
![Python 3.9+](https://img.shields.io/badge/Python-3.9+-yellow.svg)
![.NET 6.0+](https://img.shields.io/badge/.NET-6.0+-purple.svg)
![FastAPI](https://img.shields.io/badge/Framework-FastAPI-green.svg)
![ASP.NET Core](https://img.shields.io/badge/Framework-ASP.NET%20Core-blue.svg)
![Azure Cognitive Services](https://img.shields.io/badge/Azure-Cognitive%20Services-lightblue.svg)

---

A comprehensive **text analytics service** powered by **FastAPI (Python)** and **Azure Cognitive Services**, with a **C# ASP.NET Core Web API proxy layer** for enterprise-grade integration and secure API management.

---

## 🚀 Features

- 🧩 **Sentiment Analysis** — Detect positive, negative, and neutral sentiment  
- 🌐 **Language Detection** — Identify the language of text with confidence scores  
- 🗝️ **Key Phrase Extraction** — Extract important phrases and topics  
- 🧍 **Named Entity Recognition (NER)** — Identify people, places, and organizations  
- 🕵️ **PII Detection** — Detect and redact personally identifiable information  
- 🔗 **Linked Entity Recognition** — Connect entities to knowledge base entries  
- 🧾 **Batch Processing** — Analyze multiple texts in a single request  
- 🩺 **Health Monitoring** — Service status and connectivity checks  

---

## 🏗️ Architecture

```bash
TextAnalyticsAPI/
├── 📁 AiService/                 # Python FastAPI Service
│   ├── main.py                  # FastAPI app entry point
│   ├── requirements.txt         # Python dependencies
│   └── .env                     # Environment variables
│
├── 📁 TextAnalyticsAPI/         # C# Web API Proxy
│   ├── Controllers/
│   │   └── TextAnalyticsController.cs
│   ├── Program.cs
│   └── appsettings.json
│
└── 📄 README.md


## 📋 Prerequisites

- 🐍 **Python 3.8+** with pip  
- 💠 **.NET 6.0+ SDK**  
- ☁️ **Azure Cognitive Services** account (Text Analytics API enabled)  
- 💻 **Azure CLI** *(optional, for deployment)*  

---

## ⚙️ Setup & Installation

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/yourusername/azure-ai-text-analytics.git
cd azure-ai-text-analytics
2️⃣ Python FastAPI Service Setup

cd AiService

# Create virtual environment
python -m venv venv
source venv/bin/activate   # On Windows: venv\Scripts\activate

# Install dependencies
pip install -r requirements.txt

# Configure environment variables
cp .env.example .env
Update .env with your Azure credentials:

env
AI_SERVICE_ENDPOINT=https://your-resource.cognitiveservices.azure.com/
AI_SERVICE_KEY=your-api-key-here
3️⃣ C# Web API Setup
bash
cd TextAnalyticsAPI
dotnet restore
Update appsettings.json:

json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "Services": {
    "AiServiceUrl": "http://localhost:8000"
  }
}
🚀 Running the Services
▶️ Start Python FastAPI Service
bash
cd AiService
uvicorn main:app --reload --host 0.0.0.0 --port 8000
▶️ Start C# Web API Proxy
bash
cd TextAnalyticsAPI
dotnet run
📚 API Documentation
FastAPI (Port 8000)
Interactive Docs (Swagger UI)

Alternative Docs (ReDoc)

Health Check

C# Web API (Port 7000)
Swagger Docs

Health Check

🔌 API Endpoints
Endpoint	Method	Description
/api/TextAnalytics/sentiment	POST	Analyze sentiment
/api/TextAnalytics/language	POST	Detect language
/api/TextAnalytics/key-phrases	POST	Extract key phrases
/api/TextAnalytics/entities	POST	Recognize entities
/api/TextAnalytics/pii	POST	Detect and redact PII
/api/TextAnalytics/linked-entities	POST	Recognize linked entities
/api/TextAnalytics/all	POST	Perform comprehensive analysis

💡 Usage Examples
🔹 cURL
bash

curl -X POST "http://localhost:7000/api/TextAnalytics/sentiment" \
  -H "Content-Type: application/json" \
  -d '{"text": "I absolutely love this product!"}'
🔹 C# Client
csharp

var client = new HttpClient { BaseAddress = new Uri("https://localhost:7000") };
var request = new { text = "Azure AI services are amazing!" };
var response = await client.PostAsJsonAsync("/api/TextAnalytics/sentiment", request);
var result = await response.Content.ReadFromJsonAsync<SentimentResponse>();
🔹 Python Client
python

import requests

response = requests.post(
    "http://localhost:8000/analyze/sentiment",
    json={"text": "This is fantastic!"}
)
print(response.json())
📊 Sample Responses
✅ Sentiment Analysis
json

{
  "sentiment": "positive",
  "confidence_scores": {
    "positive": 0.95,
    "neutral": 0.03,
    "negative": 0.02
  }
}
🔒 PII Detection
json

{
  "pii_entities": [
    {
      "text": "john.doe@example.com",
      "category": "Email",
      "confidence_score": 0.95
    }
  ],
  "redacted_text": "My email is [REDACTED]."
}
🔧 Configuration
Azure Cognitive Services
Create a Language Service in the Azure Portal.

Retrieve your Endpoint and API Key.

Update .env with your credentials.

C# Proxy
Update the AiServiceUrl in Program.cs or appsettings.json if the Python service runs on a different host.

🧩 Development
Adding New Features
Add a new module in AiService/services/

Expose it in main.py

Create a corresponding endpoint in TextAnalyticsController.cs

Update the docs

Testing
bash

# Python tests
cd AiService
python -m pytest

# C# tests
cd TextAnalyticsAPI
dotnet test
🚀 Deployment
🐳 Docker
Python Service

dockerfile
FROM python:3.9
COPY . /app
WORKDIR /app
RUN pip install -r requirements.txt
CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8000"]
☁️ Azure App Service
bash

# Deploy Python service
az webapp up --name my-python-ai-service --resource-group my-rg

# Deploy C# proxy
az webapp up --name my-csharp-proxy --resource-group my-rg
📸 Screenshots (Optional)
FastAPI Docs	ASP.NET Swagger

(Place screenshots under docs/images/ in your repo)

🤝 Contributing
Contributions are welcome!
Follow these steps:

bash

# 1. Fork the repository
# 2. Create a new branch
git checkout -b feature/amazing-feature

# 3. Commit your changes
git commit -m "Add amazing feature"

# 4. Push and open a Pull Request
git push origin feature/amazing-feature
📄 License
This project is licensed under the MIT License — see the LICENSE file for details.

👨‍💻 Author
Shahzad Khan
Senior Azure Developer | Cloud & AI Engineer
🔗 shahzadblog.com

🙏 Acknowledgments
Azure Cognitive Services

FastAPI

ASP.NET Core


