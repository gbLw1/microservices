# Microservices

The simplest microservices application that you can build with .NET

## Introduction

This repository is [.NET Microservices – Full Course](https://youtu.be/DgVjEo3OGBI) from Les Jackson on Youtube

## About the project

Theme: **Command Service System**

The project involves developing a system that manages two main types of microservices:

- **Platform Service**: This service handles operations related to the platform, which can be considered part of the infrastructure or a database of information related to products, systems, or users.
- **Command Service**: This service manages specific actions or "commands" that can be executed based on requests made to the system.

The goal of the course is to guide the student in building these two microservices and how they communicate, either synchronously (via **HTTP** or **gRPC**) or asynchronously (using an Event Bus with **RabbitMQ**).

## Requirements

- [.NET Core 5 or later](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Account on Docker Hub](https://hub.docker.com/)
- [Insomnia](https://insomnia.rest/download) or [Postman](https://www.postman.com/downloads/)

## Testing

To keep it simple, we are going to test the application endpoints using http rather than https for the most part for various reasons, more especially when you come to kubernetes, it becomes very complex and it's going to detract from the main focus of the course.

## Docker guide

You can find a Docker guide in the [Docker.md](./docs/Docker.md) file that might be useful to help you get started and follow along with the course.

## Kubernetes guide

You can find a Kubernetes guide in the [Kubernetes.md](./docs/Kubernetes.md) file that might be useful to understand how it works and some basic commands to help you get started.

## Messaging

You can check the [Messaging.md](./docs/Messaging.md) file to understand how **asynchronous** and **synchronous** messaging works in the microservices architecture.

## K8S Development process

You can check the [K8S README.md](./K8S/README.md) file to understand the development process of deploying the application to Kubernetes.
