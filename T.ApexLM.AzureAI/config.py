import os
from dotenv import load_dotenv
from azure.ai.textanalytics import TextAnalyticsClient
from azure.core.credentials import AzureKeyCredential

def validate_environment():
    """Validate required environment variables"""
    load_dotenv()
    
    endpoint = os.getenv('AI_SERVICE_ENDPOINT')
    key = os.getenv('AI_SERVICE_KEY')
    
    if not endpoint or not key:
        missing = []
        if not endpoint: missing.append("AI_SERVICE_ENDPOINT")
        if not key: missing.append("AI_SERVICE_KEY")
        raise ValueError(f"Missing environment variables: {', '.join(missing)}")
    
    # Basic format validation
    if not endpoint.startswith('https://'):
        raise ValueError("AI_SERVICE_ENDPOINT must use HTTPS")
    
    if '.cognitiveservices.azure.com' not in endpoint:
        raise ValueError("Invalid Azure Cognitive Services endpoint")
    
    return endpoint, key

def get_azure_client():
    """Initialize and return Azure Text Analytics client"""
    endpoint, key = validate_environment()
    credential = AzureKeyCredential(key)
    return TextAnalyticsClient(endpoint=endpoint, credential=credential)
