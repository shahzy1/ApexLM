from fastapi import HTTPException

def detect_pii(client, text: str):
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
