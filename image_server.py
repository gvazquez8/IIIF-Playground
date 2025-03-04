from flask import Flask, render_template
from PIL import Image
from base64 import b64encode
from region_handler import transform_region
from size_handler import transform_size
from CIIIFImageRequest import CIIIImageRequest
import os
import io
import json

CAT_IMAGES_FOLDER = os.path.join('static', 'cats')

app = Flask(__name__) 
app.config['CATS_FOLDER'] = CAT_IMAGES_FOLDER

@app.route("/")
def main_entry():
    static_cats_files = os.listdir(app.config['CATS_FOLDER'])
    nav_list = []
    for file in static_cats_files:
        href = os.path.splitext(file)[0] + '/square'
        nav_list.append({'href': href, 'caption': file})
    
    return render_template("index.html", navList = nav_list)

@app.route("/<id>/")
def error(id):
    return "error", 404

@app.route("/<id>/<region>/<size>/<rotation>/<quality>.<format>", methods=['GET'])
def render_image(id, region, size, rotation, quality, format):

    # Create ImageRequest obj with info.json
    # ImageRequest will convert URI into its standardized form.
    image_info_file_path = os.path.join(app.config['CATS_FOLDER'], f'{id}', 'info.json')
    
    with open(image_info_file_path, 'r') as image_info_file:
        image_info = json.load(image_info_file)
    
    image_req = CIIIImageRequest(id, image_info, region, size, rotation, quality, format)

    img_io = io.BytesIO()
    
    # Region Transform
    # result_image = transform_region(full_filepath, region)
    
    # Size Transform
    # root_img = transform_size(result_image, size)
    # root_img.save(img_io, format='png')
    # img_io.seek(0)
    # dataurl = 'data:image/png;base64,' + b64encode(img_io.getvalue()).decode('ascii')
    # return render_template("image.html", image_data = dataurl)
