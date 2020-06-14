FROM python:3.7-buster

RUN apt update && apt upgrade -y

COPY requirements.txt /var/requirements.txt

RUN pip install -r /var/requirements.txt

ENV TZ=Asia/Tokyo

RUN mkdir -p /app

ENV PYTHONPATH=/app
COPY ./app /app

WORKDIR /app

CMD ["python", "main.py"]