# Kubernetes guide

Here are some quick guides to help you get started with Kubernetes.

## Table of contents

- [What is Kubernetes?](#what-is-kubernetes)
  - [How it works](#how-it-works)
  - [Architecture](#architecture)
  - [YAML file](#yaml-file)
- [Installation](#installation)
  - [Windows](#windows)
  - [Linux](#linux)
- [Getting started](#getting-started)
  - [Create a deployment](#create-a-deployment)
  - [Create a service](#create-a-service)
- [Commands](#commands)

---

## What is Kubernetes?

Kubernetes is an open-source container orchestration platform that automates the deployment, scaling, and management of containerized applications.

### How it works

Kubernetes works by creating a **cluster** of machines that run containers. Each machine in the cluster is called a **node**, and each node can run one or more containers inside a **pod**. A pod is the smallest unit of deployment in Kubernetes and is a group of one or more containers that share the same network and storage. Kubernetes manages the deployment of containers across the cluster, ensuring that they are running and healthy.

Unlike Docker, where you run a container directly on a single machine, Kubernetes is designed to run containers in a distributed environment, across multiple machines. To access your application from outside the cluster, you need to create a service that will expose it. One way to do this is by using a **NodePort** service, which opens a specific port on each node in the cluster. This NodePort will route traffic from that external port on each node to the pods running your application.

Although **NodePort** allows you to expose your application, it requires you to manually manage the node's IP addresses and the specific port number to access it. For more advanced setups, like those in cloud environments, Kubernetes also supports a **LoadBalancer** service type. This service type automatically creates an external load balancer to distribute traffic across your pods, but it’s typically available only in cloud setups that support load balancers.

### Architecture

It is composed by multiple layers, the main ones are:

- **Cluster**: A cluster is a set of physical or virtual machines that are connected to each other and run Kubernetes.
- **Node**: A node is a physical or virtual machine that is part of a Kubernetes cluster.
- **Pod**: A pod is the smallest unit of deployment in Kubernetes. It is a group of one or more containers that share the same network and storage.

![K8S architecture](./imgs/k8s-architecture.png)

### YAML file

The YAML file is a human-readable data serialization standard that is commonly used for configuration files. In Kubernetes, YAML files are used to define the deployment configuration.

- **apiVersion**: The version of the Kubernetes API that you are using.
- **kind**: The type of resource that you are creating (e.g., Deployment, Service).
- **metadata**: Contains information about the deployment, such as the name and labels.
- **spec**: Contains the deployment configuration, such as the number of replicas and the container image.
  - **replicas**: The number of replicas that you want to create (horizontal scaling).
  - **selector**: Basically, the labels that the deployment will use to select the pods.
  - **template**: Definition of what we want to deploy, the pod config and its containers.

---

## Installation

### Windows

1. Install [Docker Desktop](https://www.docker.com/products/docker-desktop)
2. Enable Kubernetes in Docker Desktop settings
3. Ready to go!

### Linux

1. Install [Docker](https://docs.docker.com/engine/install/)
   Required to create images and run containers.
   ps: Remember to add your user to the `docker` group to avoid using `sudo` every time you run a Docker command.

   ```bash
    sudo usermod -aG docker $USER
   ```

2. Install [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/)
   Required to interact with the Kubernetes cluster. This is the command-line tool that allows you to run commands against Kubernetes clusters.

3. Install [Minikube](https://minikube.sigs.k8s.io/docs/start/)
   Required to create a local Kubernetes cluster. This will allow you to test your deployments locally before deploying them to a production cluster.

4. Start Minikube

```bash
minikube start --driver=docker
```

`--driver=docker` is used to specify the container runtime to be used by Minikube. The default is `docker`, but you can also use `podman` or `containerd`.

---

## Getting started

### Create a deployment

This will deploy your application to the Kubernetes cluster, running your application in one or more pods.

1. Define a deployment file
   The deployment file is a YAML file that defines the deployment configuration.

2. Apply the deployment
   Applying the deployment will create a deployment in the Kubernetes cluster. To do this, in the same directory as the deployment file, run the following command:

   ```bash
   kubectl apply -f <deployment-file>.yaml
   ```

3. You should see the deployment created by running the following command:

   ```bash
   kubectl get deployments
   ```

### Create a service

Creating a service will expose your application to the outside world, where you can access it from your machine.

1. Define a service file
   The service file is a YAML file that defines the service configuration.

2. Apply the service
   Applying the service will create a service in the Kubernetes cluster. To do this, in the same directory as the service file, run the following command:

   ```bash
   kubectl apply -f <service-file>.yaml
   ```

3. You should see the service created by running the following command:

   ```bash
   kubectl get services
   ```

4. Get the external IP of the service

   ```bash
   minikube service <service-name> --url
   ```

   This command will return the external IP of the service, which you can use to access the application.

   ps: If you are using Docker Desktop, you can skip this step and access the application at `localhost`, just use the port that the service is using.

5. Access the application
   Open a browser and navigate to the external IP of the service to access the application.

---

## Commands

Here are some useful commands to interact with Kubernetes.

### Cluster

- Get cluster information

```bash
kubectl cluster-info
```

### Resources

Replace `<resource>` with the resource you want to interact with (e.g., deployments, services, nodes, pods).

#### Get resources

```bash
kubectl get <resource>
```

#### Get resource details

```bash
kubectl describe <resource> <resource-name>
```

#### Delete resource

```bash
kubectl delete <resource> <resource-name>
```
