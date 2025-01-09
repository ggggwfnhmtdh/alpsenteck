from ultralytics import YOLO
model = YOLO("yolov8n.pt")
model.export(format="onnx")  # creates 'yolov8n.onnx'

