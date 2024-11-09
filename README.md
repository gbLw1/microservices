# microservices

## Testing

To keep it simples, we are going to test the application endpoints using http rather than https for the most part for various reasons, more especially when you come to kubernetes, it becomes very complex and it's going to detract from the main focus of the course.

## Containerization process overview

![Containerization process](./docs/imgs/containerization-process.png)

1. We need to create a `Dockerfile` which is a set of instructions that tells Docker how to take our application and turn it into a image.
   ps: The image is one of those things that you can distribute somewhere into anything that's running Docker.
2. Run the `dockerfile` through the docker engine to create an image.
3. We end up with an image that we can run in a container.
