
#FAST API Entry point for Azure OpenAI and Azure Cognitive Search integration
from fastapi import FastAPI
from config import get_azure_client, validate_environment
# Remove this line: from models.request_models import TextRequest
# Add this instead:
from pydantic import BaseModel

from services import language, key_phrases, entities, linked_entities, pii

app = FastAPI(
    title="Azure AI Text Analytics Service",
    description="A comprehensive text analytics service using Azure Cognitive Services",
    version="1.0.0"
)

# Initialize Azure client
try:
    client = get_azure_client()
    print("✅ Azure AI client initialized successfully")
except Exception as e:
    print(f"❌ Failed to initialize Azure client: {e}")
    client = None

@app.get("/")
def read_root():
    return {
        "message": "Azure AI Text Analytics Service",
        "status": "running",
        "endpoints": [
            "/analyze/language",
            "/analyze/sentiment", 
            "/analyze/key-phrases",
            "/analyze/entities",
            "/analyze/linked-entities",
            "/analyze/pii"
        ]
    }

@app.get("/health")
def health_check():
    if client is None:
        return {"status": "unhealthy", "message": "Azure client not configured"}
    return {"status": "healthy", "message": "Service is running properly"}

@app.post("/analyze/language")
def analyze_language(request: TextRequest):
    return language.detect_language(client, request.text)

@app.post("/analyze/sentiment")
def analyze_sentiment(request: TextRequest):
    from services.sentiment import analyze_sentiment as sentiment_service
    return sentiment_service(client, request.text)

@app.post("/analyze/key-phrases")
def analyze_key_phrases(request: TextRequest):
    return key_phrases.extract_key_phrases(client, request.text)

@app.post("/analyze/entities")
def analyze_entities(request: TextRequest):
    return entities.recognize_entities(client, request.text)

@app.post("/analyze/linked-entities")
def analyze_linked_entities(request: TextRequest):
    return linked_entities.recognize_linked_entities(client, request.text)

@app.post("/analyze/pii")
def analyze_pii(request: TextRequest):
    return pii.detect_pii(client, request.text)

@app.post("/analyze/all")
def analyze_all(request: TextRequest):
    """Comprehensive text analysis combining all services"""
    if client is None:
        return {"error": "Azure client not configured"}
    
    return {
        "language": language.detect_language(client, request.text),
        "sentiment": sentiment.analyze_sentiment(client, request.text),
        "key_phrases": key_phrases.extract_key_phrases(client, request.text),
        "entities": entities.recognize_entities(client, request.text),
        "linked_entities": linked_entities.recognize_linked_entities(client, request.text),
        "pii": pii.detect_pii(client, request.text)
    }

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)