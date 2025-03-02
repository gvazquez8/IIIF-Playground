from PIL import Image

def transform_size(image: Image.Image, size: str) -> Image.Image:

    if size == "max":
        return image
    

    return