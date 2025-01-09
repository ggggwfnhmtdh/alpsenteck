conda create -n yolo python=3.8 -y
conda activate yolo
git clone https://github.com/ultralytics/ultralytics.git
cd ultralytics
pip install chardet
pip install notebook
pip install -e .
yolo checks
pip check
pause