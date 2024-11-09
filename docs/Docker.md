# Docker cheat sheet

Here are some useful Docker commands to help you get started with Docker.

## Build an image

In the dockerfile directory, enter the following code:

```bash
docker build -t <your_docker_hub_id>/<image_name> .
```

ps: The `.` at the end of the command tells Docker to look for the `Dockerfile` in the current directory. Also, use lowercase for the image name.

## Run an image as a container

```bash
docker run -p 8080:8080 -d <your_docker_hub_id>/<image_name>
```

`-p` flag tells Docker to map port 8080 on the host to port 8080 on the container.
`-d` flag tells Docker to run the container in the background.

ps: Every time you run an image, Docker creates a container from that image, it's not the same as restarting a container if you stop it. To restart a container, [see below](#restart-a-container).

## List all running containers

```bash
docker ps -a
```

`-a` flag tells Docker to list all containers, including the ones that are not running.

## Stop a running container

```bash
docker stop <container_id>
```

## Restart a container

```bash
docker start <container_id>
```

## Remove a container

```bash
docker rm <container_id>
```

## Remove an image

```bash
docker rmi <image_id>
```

## Push an image to Docker Hub

```bash
docker push <your_docker_hub_id>/<image_name>
```
