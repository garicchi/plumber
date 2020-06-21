FROM python:3.7-buster

RUN apt update && apt upgrade -y

RUN apt install -y default-mysql-client

COPY requirements.txt /var/requirements.txt

RUN pip install -r /var/requirements.txt

ENV TZ=Asia/Tokyo

RUN mkdir -p /app

ENV PYTHONPATH=/app
COPY ./app /app

WORKDIR /app

CMD ["python", "main.py"]