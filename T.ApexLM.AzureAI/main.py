
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from dotenv import load_dotenv
import os
import json
from azure.ai.textanalytics import TextAnalyticsClient
from azure.core.credentials import AzureKeyCredential

# Load environment variables
load_dotenv()

app = FastAPI(
    title="Azure AI Text Analytics Service",
    description="A comprehensive text analytics service using Azure Cognitive Services",
    version="1.0.0"
)

# Pydantic model for requests
class TextRequest(BaseModel):
    text: str

# Custom Text Classification
class ClassificationRequest(BaseModel):
    text: str
    project_name: str = "classify-text-lab"
    deploment_name: str = "articles"  #"production"

# Configuration
def get_azure_client():
    endpoint = os.getenv('AI_SERVICE_ENDPOINT')
    key = os.getenv('AI_SERVICE_KEY')
    
    # Validate environment variables
    if not endpoint or not key:
        missing = []
        if not endpoint: missing.append("AI_SERVICE_ENDPOINT")
        if not key: missing.append("AI_SERVICE_KEY")
        raise ValueError(f"Missing environment variables: {', '.join(missing)}")
    
    credential = AzureKeyCredential(key)
    return TextAnalyticsClient(endpoint=endpoint, credential=credential)

# Initialize client
try:
    client = get_azure_client()
    print("✅ Azure AI client initialized successfully")
except Exception as e:
    print(f"❌ Failed to initialize Azure client: {e}")
    client = None

# Custom Classification Function
#def classify_text_custom(text: str, project_name: str, deployment_name: str):
def classify_text_custom(text: str, project_name: str = "classify-text-lab", deployment_name: str = "articles"):
    """
    Custom text classification using Azure Custom Text
    Note: This requires a trained custom classification model in Azure
    """
    if client is None:
        raise HTTPException(status_code=500, detail="Azure client not configured")
    
    try:
        # For Azure Custom Text classification (requires custom model)
        # This is a placeholder - actual implementation depends on your custom model
        poller = client.begin_single_label_classify(
            documents=[text],
            project_name=project_name,
            deployment_name=deployment_name
        )
        
        result = poller.result()
        document_result = result[0]
        
        classifications = []
        for classification in document_result.classifications:
            classifications.append({
                "category": classification.category,
                "confidence_score": classification.confidence_score
            })
        
        return {
            "classifications": classifications,
            "top_classification": max(classifications, key=lambda x: x["confidence_score"]) if classifications else None
        }
        
    except Exception as e:
        # Fallback to rule-based classification if custom model is not available
        return fallback_classification(text)

def fallback_classification(text: str):
    """
    Fallback rule-based classification when custom model is not available
    """
    text_lower = text.lower()
    
    categories = {
        "support": ["help", "support", "problem", "issue", "error", "bug", "fix"],
        "sales": ["buy", "purchase", "price", "cost", "order", "sale", "discount"],
        "technical": ["technical", "setup", "install", "configure", "api", "integration"],
        "billing": ["bill", "invoice", "payment", "charge", "refund", "subscription"],
        "general": ["hello", "hi", "thanks", "thank you", "information"]
    }
    
    matches = {}
    for category, keywords in categories.items():
        score = sum(1 for keyword in keywords if keyword in text_lower)
        if score > 0:
            matches[category] = min(score / len(keywords) * 2, 1.0)  # Normalize score
    
    # If no matches, classify as "other"
    if not matches:
        matches["other"] = 1.0
    
    # Convert to classification format
    total_score = sum(matches.values())
    classifications = [
        {
            "category": category,
            "confidence_score": score / total_score
        }
        for category, score in matches.items()
    ]
    
    return {
        "classifications": classifications,
        "top_classification": max(classifications, key=lambda x: x["confidence_score"]),
        "note": "Using fallback rule-based classification"
    }

# Service functions (all in one file)
def detect_language(text: str):
    if client is None:
        raise HTTPException(status_code=500, detail="Azure client not configured")
    try:
        response = client.detect_language(documents=[text])[0]
        return {
            "detected_language": response.primary_language.name,
            "confidence_score": response.primary_language.confidence_score,
            "iso6391_name": response.primary_language.iso6391_name
        }
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Language detection failed: {str(e)}")

def analyze_sentiment(text: str):
    if client is None:
        raise HTTPException(status_code=500, detail="Azure client not configured")
    try:
        response = client.analyze_sentiment(documents=[text])[0]
        return {
            "sentiment": response.sentiment,
            "confidence_scores": {
                "positive": response.confidence_scores.positive,
                "neutral": response.confidence_scores.neutral,
                "negative": response.confidence_scores.negative
            },
            "sentences": [
                {
                    "text": sentence.text,
                    "sentiment": sentence.sentiment,
                    "confidence_scores": {
                        "positive": sentence.confidence_scores.positive,
                        "neutral": sentence.confidence_scores.neutral,
                        "negative": sentence.confidence_scores.negative
                    }
                } for sentence in response.sentences
            ] if response.sentences else []
        }
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Sentiment analysis failed: {str(e)}")

def extract_key_phrases(text: str):
    if client is None:
        raise HTTPException(status_code=500, detail="Azure client not configured")
    try:
        response = client.extract_key_phrases(documents=[text])[0]
        return {
            "key_phrases": response.key_phrases
        }
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Key phrase extraction failed: {str(e)}")

def recognize_entities(text: str):
    if client is None:
        raise HTTPException(status_code=500, detail="Azure client not configured")
    try:
        response = client.recognize_entities(documents=[text])[0]
        return {
            "entities": [
                {
                    "text": entity.text,
                    "category": entity.category,
                    "subcategory": entity.subcategory,
                    "confidence_score": entity.confidence_score
                } for entity in response.entities
            ]
        }
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Entity recognition failed: {str(e)}")

def recognize_linked_entities(text: str):
    if client is None:
        raise HTTPException(status_code=500, detail="Azure client not configured")
    try:
        response = client.recognize_linked_entities(documents=[text])[0]
        return {
            "linked_entities": [
                {
                    "name": entity.name,
                    "url": entity.url,
                    "data_source": entity.data_source,
                    "matches": [
                        {
                            "text": match.text,
                            "confidence_score": match.confidence_score
                        } for match in entity.matches
                    ]
                } for entity in response.entities
            ]
        }
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Linked entity recognition failed: {str(e)}")

def detect_pii(text: str):
    if client is None:
        raise HTTPException(status_code=500, detail="Azure client not configured")
    try:
        response = client.recognize_pii_entities(documents=[text])[0]
        
        # Create redacted text
        redacted_text = text
        for entity in sorted(response.entities, key=lambda x: x.offset, reverse=True):
            redacted_text = redacted_text[:entity.offset] + '[REDACTED]' + redacted_text[entity.offset + entity.length:]
        
        return {
            "pii_entities": [
                {
                    "text": entity.text,
                    "category": entity.category,
                    "subcategory": entity.subcategory,
                    "confidence_score": entity.confidence_score
                } for entity in response.entities
            ],
            "redacted_text": redacted_text
        }
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"PII detection failed: {str(e)}")

# API endpoints
@app.get("/")
def read_root():
    return {
        "message": "Azure AI Text Analytics Service",
        "status": "running",
        "version": "1.0.0",
        "endpoints": [
            "/analyze/language",
            "/analyze/sentiment", 
            "/analyze/key-phrases",
            "/analyze/entities",
            "/analyze/linked-entities",
            "/analyze/pii",
            "/analyze/classify",
            "/analyze/all"
        ]
    }

@app.get("/health")
def health_check():
    if client is None:
        return {"status": "unhealthy", "message": "Azure client not configured"}
    return {"status": "healthy", "message": "Service is running properly"}

@app.post("/analyze/classify")
def classify_text(request: ClassificationRequest):
    """
    Classify text into custom categories
    - Uses Azure Custom Text if model is available
    - Falls back to rule-based classification
    """
    return classify_text_custom(
        request.text, 
        request.project_name, 
        request.deploment_name
    )

@app.post("/analyze/classify/simple")
def classify_text_simple(request: TextRequest):
    """
    Simple classification with default project settings
    """
    return classify_text_custom(
        request.text, 
        "classify-text-lab", 
        "articles"
    )

@app.post("/analyze/language")
def analyze_language_endpoint(request: TextRequest):
    return detect_language(request.text)

@app.post("/analyze/sentiment")
def analyze_sentiment_endpoint(request: TextRequest):
    return analyze_sentiment(request.text)

@app.post("/analyze/key-phrases")
def analyze_key_phrases_endpoint(request: TextRequest):
    return extract_key_phrases(request.text)

@app.post("/analyze/entities")
def analyze_entities_endpoint(request: TextRequest):
    return recognize_entities(request.text)

@app.post("/analyze/linked-entities")
def analyze_linked_entities_endpoint(request: TextRequest):
    return recognize_linked_entities(request.text)

@app.post("/analyze/pii")
def analyze_pii_endpoint(request: TextRequest):
    return detect_pii(request.text)

@app.post("/analyze/all")
def analyze_all_endpoint(request: TextRequest):
    """Comprehensive text analysis combining all services"""
    if client is None:
        raise HTTPException(status_code=500, detail="Azure client not configured")
    
    return {
        "language": detect_language(request.text),
        "sentiment": analyze_sentiment(request.text),
        "key_phrases": extract_key_phrases(request.text),
        "entities": recognize_entities(request.text),
        "linked_entities": recognize_linked_entities(request.text),
        "pii": detect_pii(request.text),
        "classification": classify_text_custom(
            request.text, 
            "classify-text-lab",  # Your project name
            "articles"            # Your deployment name
        )
    }

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)