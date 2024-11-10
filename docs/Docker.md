# Docker guide

Here are some quick guide to help you get started with Docker.

## Table of contents

- [What is Docker?](#what-is-docker)
- [Containerization process](#containerization-process)
  - [How it works?](#how-it-works)
- [Dockerfile](#dockerfile)
  - [Setup a Dockerfile](#setup-a-dockerfile)
- [Commands](#commands)

---

## What is Docker?

Docker is a platform for developing, shipping, and running applications using containerization. It allows you to package your application and all its dependencies into a container, which can then be run on any machine that has Docker installed.

---

## Containerization process

Docker uses a technology called containerization to create and manage containers. A container is a lightweight, standalone, executable package of software that includes everything needed to run an application: code, runtime, system tools, system libraries, and settings.

### How it works?

![Containerization process](./imgs/containerization-process.png)

1. We need to create a `Dockerfile` which is a set of instructions that tells Docker how to take our application and turn it into a image.
   ps: The image is one of those things that you can distribute somewhere into anything that's running Docker.
2. Run the `dockerfile` through the docker engine to create an image.
3. We end up with an image that we can run in a container.

---

## Dockerfile

A `Dockerfile` is a text document that contains all the commands a user could call on the command line to assemble an image. Using `docker build` users can create an automated build that executes several command-line instructions in succession.

### Setup a Dockerfile

Follow the [official microsoft tutorial](https://learn.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=windows&pivots=dotnet-8-0) to create a `Dockerfile` for a .NET Core application.

---

## Commands

### Build an image

In the `dockerfile` directory, enter the following code:

```bash
docker build -t <your_docker_hub_id>/<image_name> .
```

ps: The `.` at the end of the command tells Docker to look for the `Dockerfile` in the current directory. Also, use lowercase for the image name.

### Run an image as a container

```bash
docker run -p 8080:8080 -d <your_docker_hub_id>/<image_name>
```

`-p` flag tells Docker to map port 8080 on the host (your machine) to port 8080 on the container.

`-d` flag tells Docker to run the container in the background.

ps: Every time you run an image, Docker creates a container from that image, it's not the same as restarting a container if you stop it. To restart a container, [see below](#restart-a-container).

### List all running containers

```bash
docker ps -a
```

`-a` flag tells Docker to list all containers, including the ones that are not running.

### Stop a running container

```bash
docker stop <container_id>
```

### Restart a container

```bash
docker start <container_id>
```

### Remove a container

```bash
docker rm <container_id>
```

### Remove an image

```bash
docker rmi <image_id>
```

### Push an image to Docker Hub

```bash
docker push <your_docker_hub_id>/<image_name>
```
