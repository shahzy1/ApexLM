from fastapi import HTTPException

def recognize_entities(client, text: str):
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
