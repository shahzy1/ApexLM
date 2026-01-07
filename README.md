![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![Python](https://img.shields.io/badge/Python-3776AB?logo=python&logoColor=white)
![Blazor](https://img.shields.io/badge/Blazor-512BD4?logo=blazor&logoColor=white)
![FastAPI](https://img.shields.io/badge/FastAPI-009688?logo=fastapi&logoColor=white)
![Azure](https://img.shields.io/badge/Azure-0078D4?logo=microsoftazure&logoColor=white)
![SQL](https://img.shields.io/badge/Azure_SQL-CC2927?logo=microsoftsqlserver&logoColor=white)
![Azure Cognitive Services](https://img.shields.io/badge/Azure_Cognitive_Services-0078D4?logo=microsoftazure&logoColor=white)
![GPT-5](https://img.shields.io/badge/GPT--5-Enabled-412991?style=flat&logo=openai&logoColor=white)

# 🧠 ApexLM — Hybrid AI Text Analytics and Summarization Service (Lab Project)

> From text analytics to summarization — ApexLM brings Azure AI and GPT-5 together in one intelligent workspace.

ApexLM is a personal lab and learning project focused on hands-on exploration of Azure OpenAI and Azure Cognitive Services for text analytics and summarization. The project brings together modern application patterns and AI APIs in a controlled, non-production environment to understand integration approaches, deployment models, and secure design considerations.

ApexLM is not affiliated with or sponsored by any employer and is intended solely for learning and experimentation.

The application is built as a Blazor WebAssembly frontend with ASP.NET Core (C#) and FastAPI (Python) backend services. It integrates Azure Cognitive Services and Azure OpenAI APIs to explore intelligent text processing capabilities, including:

Text summarization using Azure OpenAI

Sentiment analysis

Language detection

Entity recognition

PII detection

The platform demonstrates secure handling of user content using Azure SQL, Azure Blob Storage, and Managed Identity, avoiding hard-coded secrets or connection strings and emphasizing secure, cloud-native integration patterns.

---

## ⚙️ Architecture Overview
```
Blazor WebAssembly (UI)
↓
ASP.NET Core Web API ←→ FastAPI (Python AI Services)
↓
Azure Cognitive Services + GPT-5
↓
Azure SQL (User & Notebook Data)
↓
Azure Blob Storage (Document Files)
```
---

## 🧩 Tech Stack

![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![Python](https://img.shields.io/badge/Python-3776AB?logo=python&logoColor=white)
![Blazor](https://img.shields.io/badge/Blazor-512BD4?logo=blazor&logoColor=white)
![ASP.NET Web API](https://img.shields.io/badge/ASP.NET_Web_API-512BD4?logo=dotnet&logoColor=white)
![FastAPI](https://img.shields.io/badge/FastAPI-009688?logo=fastapi&logoColor=white)
![Azure](https://img.shields.io/badge/Azure-0078D4?logo=microsoftazure&logoColor=white)
![SQL](https://img.shields.io/badge/Azure_SQL-CC2927?logo=microsoftsqlserver&logoColor=white)

---

## 🔐 Security & Integration

- Managed Identity authentication (no secrets or SAS tokens)  
- Azure Key Vault integration for API and resource credentials  
- Role-based access and notebook-level ownership control

---

🚀 Getting Started  
1. Clone the repository  
2. Configure Azure SQL, Blob Storage, and Cognitive Services  
3. Launch the Blazor WebAssembly app and FastAPI backend  
4. Upload documents → Generate summaries, insights, and entities using **GPT-5** and Azure AI  

---

## 🔮 Future Enhancements

- 🧠 **Semantic Search & Knowledge Grounding** — connect notebook data to GPT-based reasoning  
- 🗂️ **Folder-based Organization** — richer hierarchy for notebooks and document sets  
- 🎤 **Multimodal Support** — extend analysis to voice transcripts and images  
- 🧾 **Document History & Versioning** — track evolution and insights over time  
- 🔍 **Query Engine Integration** — natural language queries across stored content  

---

📘 *ApexLM is designed for intelligent content understanding — secure, scalable, and built on Azure-first principles.*

## 📋 Prerequisites

- 🐍 **Python 3.8+** with pip  
- 💠 **.NET 6.0+ SDK**  
- 🤖 **GPT-5** model integration for text summarization and intelligent responses  
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
GPT-5
FastAPI
ASP.NET Core


