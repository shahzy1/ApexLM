
from pydantic import BaseModel, Field

class TextRequest(BaseModel):
    text: str = Field(..., min_length=1, max_length=5000, description="Text to analyze")

class BatchTextRequest(BaseModel):
    texts: list[str] = Field(..., min_items=1, max_items=10, description="List of texts to analyze")
