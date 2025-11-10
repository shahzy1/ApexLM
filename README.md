# Azure AI Text Analytics API
Azure AI Text Analytics API
A comprehensive text analytics service built with FastAPI (Python) that provides multiple natural language processing capabilities using Azure Cognitive Services. Includes a C# Web API proxy layer for enterprise integration.

🚀 Features
Sentiment Analysis - Detect positive, negative, and neutral sentiment

Language Detection - Identify the language of text with confidence scores

Key Phrase Extraction - Extract important phrases and topics

Named Entity Recognition - Identify people, places, organizations, and more

PII Detection - Detect and redact personally identifiable information

Linked Entity Recognition - Connect entities to knowledge base entries

Batch Processing - Analyze multiple texts in a single request

Health Monitoring - Service status and connectivity checks

🏗️ Architecture
text
TextAnalyticsAPI/
├── 📁 AiService/                 # Python FastAPI Service
│   ├── main.py                  # FastAPI application entry point
│   ├── requirements.txt         # Python dependencies
│   └── .env                    # Environment variables
│
├── 📁 TextAnalyticsAPI/         # C# Web API Proxy
│   ├── Controllers/
│   │   └── TextAnalyticsController.cs
│   ├── Program.cs
│   └── appsettings.json
│
└── 📄 README.md
📋 Prerequisites
Python 3.8+ with pip

.NET 6.0+ SDK

Azure Cognitive Services account with Text Analytics API

Azure CLI (optional, for deployment)

⚙️ Setup & Installation
1. Clone the Repository
bash
git clone https://github.com/yourusername/azure-ai-text-analytics.git
cd azure-ai-text-analytics
2. Python FastAPI Service Setup
bash
cd AiService

# Create virtual environment
python -m venv venv
source venv/bin/activate  # On Windows: venv\Scripts\activate

# Install dependencies
pip install -r requirements.txt

# Configure environment variables
cp .env.example .env
# Edit .env with your Azure credentials
Environment Variables (.env):

env
AI_SERVICE_ENDPOINT=https://your-resource.cognitiveservices.azure.com/
AI_SERVICE_KEY=your-api-key-here
3. C# Web API Setup
bash
cd TextAnalyticsAPI

# Restore NuGet packages
dotnet restore

# Update appsettings.json with your Python service URL
appsettings.json:

json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
🚀 Running the Services
Start Python FastAPI Service
bash
cd AiService
uvicorn main:app --reload --host 0.0.0.0 --port 8000
Start C# Web API Service
bash
cd TextAnalyticsAPI
dotnet run
📚 API Documentation
FastAPI Service (Port 8000)
Interactive Docs: http://localhost:8000/docs

Alternative Docs: http://localhost:8000/redoc

Health Check: http://localhost:8000/health

C# Web API (Port 7000)
Swagger Docs: http://localhost:7000/swagger

Health Check: http://localhost:7000/api/TextAnalytics/health

🔌 API Endpoints
Single Text Analysis
http
POST /api/TextAnalytics/sentiment
POST /api/TextAnalytics/language
POST /api/TextAnalytics/key-phrases
POST /api/TextAnalytics/entities
POST /api/TextAnalytics/pii
POST /api/TextAnalytics/linked-entities
Comprehensive Analysis
http
POST /api/TextAnalytics/all
Request Body
json
{
  "text": "Your text to analyze here"
}
💡 Usage Examples
cURL Examples
bash
# Sentiment Analysis
curl -X POST "http://localhost:7000/api/TextAnalytics/sentiment" \
  -H "Content-Type: application/json" \
  -d '{"text": "I absolutely love this product! The quality is outstanding."}'

# Language Detection
curl -X POST "http://localhost:7000/api/TextAnalytics/language" \
  -H "Content-Type: application/json" \
  -d '{"text": "Este es un texto en español"}'

# Comprehensive Analysis
curl -X POST "http://localhost:7000/api/TextAnalytics/all" \
  -H "Content-Type: application/json" \
  -d '{"text": "Microsoft Corporation, headquartered in Redmond, Washington, was founded by Bill Gates and Paul Allen. My email is john.doe@example.com."}'
C# Client Example
csharp
var client = new HttpClient { BaseAddress = new Uri("https://localhost:7000") };
var request = new { text = "Azure AI services are amazing!" };
var response = await client.PostAsJsonAsync("/api/TextAnalytics/sentiment", request);
var result = await response.Content.ReadFromJsonAsync<SentimentResponse>();
Python Client Example
python
import requests

response = requests.post(
    "http://localhost:8000/analyze/sentiment",
    json={"text": "This is fantastic!"}
)
print(response.json())
📊 Response Examples
Sentiment Analysis Response
json
{
  "sentiment": "positive",
  "confidence_scores": {
    "positive": 0.95,
    "neutral": 0.03,
    "negative": 0.02
  },
  "sentences": [
    {
      "text": "I love this product!",
      "sentiment": "positive",
      "confidence_scores": {
        "positive": 0.98,
        "neutral": 0.01,
        "negative": 0.01
      }
    }
  ]
}
PII Detection Response
json
{
  "pii_entities": [
    {
      "text": "john.doe@example.com",
      "category": "Email",
      "confidence_score": 0.95
    }
  ],
  "redacted_text": "My email is [REDACTED] and phone is [REDACTED]."
}
🔧 Configuration
Azure Cognitive Services
Create a Language Service in Azure Portal

Get your Endpoint and API Key

Update the .env file with your credentials

C# Proxy Configuration
The C# service acts as a proxy to the Python service. Update the base URL in Program.cs if needed.

🛠️ Development
Adding New Features
Add new service module in AiService/services/

Create corresponding endpoint in main.py

Add proxy method in TextAnalyticsController.cs

Update documentation

Testing
bash
# Test Python service
cd AiService
python -m pytest

# Test C# service
cd TextAnalyticsAPI
dotnet test
🚀 Deployment
Docker Deployment
dockerfile
# Python Service
FROM python:3.9
COPY . /app
WORKDIR /app
RUN pip install -r requirements.txt
CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8000"]
Azure App Service
bash
# Deploy Python service
az webapp up --name my-python-ai-service --resource-group my-rg

# Deploy C# service
az webapp up --name my-csharp-proxy --resource-group my-rg
🤝 Contributing
Fork the repository

Create a feature branch (git checkout -b feature/amazing-feature)

Commit your changes (git commit -m 'Add amazing feature')

Push to the branch (git push origin feature/amazing-feature)

Open a Pull Request

📄 License
This project is licensed under the MIT License - see the LICENSE file for details.

🙏 Acknowledgments
Azure Cognitive Services

FastAPI

ASP.NET Core

📞 Support
For support, please open an issue in the GitHub repository or contact the development team.

Note: Ensure your Azure Cognitive Services resource is properly configured and has the necessary permissions for text analytics operations.

📄 License

This project is licensed under the MIT License — feel free to use and modify it.

👨‍💻 Author

Shahzad Khan Senior Azure Developer | Cloud & AI Engineer 🔗 shahzadblog.com