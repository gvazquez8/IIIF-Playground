
class CIIIImageRequest:
    def __init__(self, id: str, info: dict, region: str, 
                 size: str, rotation: str, quality: str, format: str):
        self.id = id
        self.info = info
        self.source_width = int(info["width"])
        self.source_height = int(info["height"])

        # ==== REGION ====
        use_source_region = False
        if region == 'square':
            min_region = min(self.source_width, self.source_height)
            self.canon_region = [0, 0, min_region, min_region]
        elif ',' in region:
            region_coordinates = region.split(',')

            if len(region_coordinates) != 4:
                raise ValueError('Invalid Region Parameter: ', region)
            
            as_ratio = False
            if region_coordinates[0].startswith("pct:"):
                as_ratio = True
            
            region_coordinates = [float(coord) if not as_ratio else float(coord) / 100 
                                                for coord in region_coordinates]

            # treat region_coordinates as ratio if region expressed as a percentage
            self.canon_region = [
                region_coordinates[0] * (self.source_width if as_ratio else 1),
                region_coordinates[1] * (self.source_height if as_ratio else 1),
                region_coordinates[2] * (self.source_width if as_ratio else 1),
                region_coordinates[3] * (self.source_height if as_ratio else 1)
            ]
        elif region == "full":
            use_source_region = True
            self.canon_region = region
        else:
            raise ValueError(f"Unknown Region Uri Pattern: {region}")

        # ==== SIZE ====
        if size == "max" or size == "^max":
            self.canon_size = size
        else:
            # allow_upscaling = False
            if size.startswith("^"):
                # allow_upscaling = True
                # size = size[1:]
                raise NotImplementedError() # TODO: Impl upscaling
            
            if size.startswith("!"):
                raise NotImplementedError() # TODO: Impl ! special case
            
            if size.startwith("pct:"):
                ratio = int(size[4:])
                if not use_source_region:
                    self.canon_size = [self.canon_region[2] * ratio, self.canon_region[3] * ratio]
                else:
                    self.canon_size = [self.source_width * ratio, self.source_height * ratio]
            elif size.count(',') == 1:
                size_w, size_h = size.split(',')
                if size_h == '':
                    # ILLEGAL?: size_w > region
                    self.canon_size = [int(size_w), 
                                       self.source_height if use_source_region else self.canon_region[4]]
                elif size_w == '':
                    self.canon_size = [self.source_width if use_source_region else self.canon_region[3], 
                                       int(size_h)]
                elif size_w != '' and size_h != '':
                    self.canon_size = [int(size_w), int(size_h)]
                else:
                    raise ValueError(f"Unknown Size Uri Pattern: {size}")
        
        # ==== ROTATION ====
        self.mirror_before_rotation = True if rotation.startswith("!") else False
        try:
            possible_canon_rotation = float(rotation[1:]) if self.mirror_before_rotation else float(rotation)
        except Exception:
            raise ValueError(f"Unknown Rotation Uri Pattern: {rotation}")
        
        if 0 <= possible_canon_rotation <= 360:
            self.canon_rotation = possible_canon_rotation
        else:
            raise ValueError("Rotation out of range")
        
        # ==== QUALITY ====
        if quality not in ["default", "bitonal", "gray", "color"]:
            raise ValueError(f"Unknown Quality Uri Pattern: {quality}")
        self.canon_quality = quality

        # ==== FORMAT ====
        if format not in ["jpg", "tif", "png", "gif", "jp2", "pdf", "webp"]:
            raise ValueError(f"Unknown Format Uri Pattern: {format}")
        self.canon_format = format            

        print("Image Request Canonical Uri Params:")
        print(f"Region: {self.canon_region}")
        print(f"Size: {self.canon_size}")
        print(f"Rotation: {self.canon_rotation}")
        print(f"Mirror before Rotation? {self.mirror_before_rotation}")
        print(f"Quality: {quality}")
        print(f"Format: {format}")

            


        