# IIIF Playground Project
---

The IIIF Playground Project is a pair of client + server applications used to implement the IIIF Image API 3.0.0

There are two version to the web server that implements the Image API.


## Requirements for Running PlaygroundClient csproj

To build and run the `PlaygroundClient` WinUI application, ensure your environment meets the following requirements:

- **Operating System:** Windows 10 version 17763 (October 2018 Update) or later
- **.NET SDK:** .NET 9.0 or later with Windows 10.0.20348.0 SDK
- **Visual Studio:** Visual Studio 2022 (v17.0) or later, with the following workloads:
  - .NET Desktop Development
  - Universal Windows Platform development
- **Windows App SDK:** Version 1.4 or later (installed via NuGet)
- **NuGet Packages:** Restore all NuGet dependencies (see [`PlaygroundClient.csproj`](client/winui/PlaygroundClient.csproj) for details)
- **Supported Architectures:** x86, x64, ARM64

Before running, make sure to restore NuGet packages.

## Requirements for Running the Python IIIF Image API Server
To run the Python-based IIIF Image API server in the server folder, ensure your environment meets the following requirements:

- **Python:** Version 3.10 or later (recommended 3.11+)
- **Virtual Environment:** (Recommended) Use a Python virtual environment
- **Dependencies:** Install all required packages using pip and the requirements.txt file

### Setup Instructions
Open a terminal and navigate to the `server` directory:

```sh
cd server
```
(Optional) Create and activate a virtual environment:
```sh
python -m venv venv
# On Windows:
venv\Scripts\activate
# On macOS/Linux:
source venv/bin/activate
```
Install dependencies:
```sh
pip install -r requirements.txt
```
Run the server:
```sh
python image_server.py
```
The server uses Flask and Pillow. Make sure the static/cats folder contains your image data.