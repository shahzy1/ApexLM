from fastapi import HTTPException

def analyze_sentiment(client, text: str):
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
