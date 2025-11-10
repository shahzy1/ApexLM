from fastapi import HTTPException

def extract_key_phrases(client, text: str):
    try:
        response = client.extract_key_phrases(documents=[text])[0]
        return {
            "key_phrases": response.key_phrases
        }
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Key phrase extraction failed: {str(e)}")
