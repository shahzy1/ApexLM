from fastapi import HTTPException

def recognize_linked_entities(client, text: str):
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
