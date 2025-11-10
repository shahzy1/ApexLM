
import re

def validate_text_length(text: str, max_length: int = 5000) -> bool:
    """Validate text length for Azure Cognitive Services"""
    return len(text) <= max_length

def sanitize_text(text: str) -> str:
    """Basic text sanitization"""
    return text.strip()

def chunk_text(text: str, max_chunk_size: int = 5000) -> list[str]:
    """Split long text into chunks for processing"""
    if len(text) <= max_chunk_size:
        return [text]
    
    # Simple sentence-aware chunking
    sentences = re.split(r'[.!?]+', text)
    chunks = []
    current_chunk = ""
    
    for sentence in sentences:
        if len(current_chunk) + len(sentence) + 1 <= max_chunk_size:
            current_chunk += sentence + ". "
        else:
            if current_chunk:
                chunks.append(current_chunk.strip())
            current_chunk = sentence + ". "
    
    if current_chunk:
        chunks.append(current_chunk.strip())
    
    return chunks