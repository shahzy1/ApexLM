
from fastapi import HTTPException

def detect_language(client, text: str):
    try:
        response = client.detect_language(documents=[text])[0]
        return {
            "detected_language": response.primary_language.name,
            "confidence_score": response.primary_language.confidence_score,
            "iso6391_name": response.primary_language.iso6391_name
        }
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Language detection failed: {str(e)}")