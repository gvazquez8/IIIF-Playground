from PIL import Image

def transform_region(file_path: str, region: str) -> Image.Image:
    img = Image.open(file_path)

    if region == "full":
        return img
    
    coords = [0, 0, 0, 0]
    if region == "square":
        # TODO: The region may be positioned anywhere in the longer dimension of the full image at the server’s discretion, and centered is often a reasonable default.
        min_length = min(img.size)
        coords[2] = coords[3] = min_length
        print(f"SQUARE: Resizing to {coords}")
        return img.crop(coords)
    
    as_ratio = False
    if region.startswith("pct:"):
        region = region[4:]
        as_ratio = True
    
    split_region = region.split(",")
    if len(split_region) > 4:
        # raise something
        pass

    #clean up
    coords[0] = float(split_region[0]) / 100 * (img.width if as_ratio else 1) # x
    coords[1] = float(split_region[1]) / 100  * (img.height if as_ratio else 1) # y
    coords[2] = (float(split_region[2]) / 100  * (img.width if as_ratio else 1)) + coords[0] # w + x
    coords[3] = (float(split_region[3]) / 100  * (img.height if as_ratio else 1)) + coords[1] # h + y
    
    print(f"Original {split_region}")
    print(f"Resizing to x,y,w,h {coords}")
    return img.crop(coords)

    


    
    
   
