from CIIIFImageRequest import CIIIImageRequest
from PIL import Image

class CImageTransformer:
    def __init__(self):
        self.pipeline = []
        self.pipeline.append(self.get_region)
        self.pipeline.append(self.get_size)
        self.pipeline.append(self.rotate)
        self.pipeline.append(self.get_quality)
        self.pipeline.append(self.format)

    def run(self, image: Image.Image, image_request: CIIIImageRequest) -> Image.Image:
        modified_iamge = None
        for transform in self.pipeline:
            modified_iamge = transform(image if modified_iamge is None else modified_iamge, image_request)
        return modified_iamge

    def get_region(self, image: Image.Image, image_request: CIIIImageRequest) -> Image.Image:
        requested_region: str | list = image_request.canon_region
        print("requested region:", requested_region)
        if requested_region != "full":
            return image.crop((
                requested_region[0],
                requested_region[1],
                requested_region[0] + requested_region[2],
                requested_region[1] + requested_region[3]
            ))
        else:
            return image

    def get_size(self, image: Image.Image, image_request: CIIIImageRequest) -> Image.Image:
        requested_size: str = image_request.canon_size
        
        if requested_size == "max":
            return image
        return image.resize((requested_size[0], requested_size[1]))

    def rotate(self, image: Image.Image, image_request: CIIIImageRequest) -> Image.Image:
        if image_request.mirror_before_rotation:
            image = image.transpose(Image.Transpose.FLIP_LEFT_RIGHT)

        return image.rotate(image_request.canon_rotation, expand=True)

    def get_quality(self, image: Image.Image, image_request: CIIIImageRequest) -> Image.Image:

        if image_request.canon_quality == "gray":
            return image.convert('L')
        elif image_request.canon_quality == "bitonal":
            return image.convert('1', dither=Image.Dither.NONE)
        
        return image

    def format(self, image: Image.Image, image_request: CIIIImageRequest) -> Image.Image:
        if image_request.canon_format != "jpg":
            raise NotImplementedError(f"{image_request.canon_format} not supported.")
        return image
