# Use the official Python image as the base image
FROM ubuntu

RUN apt-get update
RUN apt-get install -y python3 python3-pip
RUN pip3 install flask

# Set the working directory in the container
WORKDIR /app

# Copy the current directory contents into the container at /app
COPY app.py /app

# Make port 5000 available to the world outside this container
EXPOSE 5000

# Run app.py when the container launches
ENTRYPOINT FLASK_APP=app.py flask run --host=0.0.0.0
