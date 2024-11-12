# K8S

Here's a step by step notes following along with the course to deploy the application to Kubernetes.

---

## Table of Contents

- [Deploying the Platform service to Kubernetes](#deploying-the-platform-service-to-kubernetes)
- [Creating the NodePort service](#creating-the-nodeport-service)
- [Creating the ClusterIP service](#creating-the-clusterip-service)

---

## Deploying the Platform service to Kubernetes

The first we are going to do is deploy a **Pod** with our Platform service to Kubernetes.

![k8s step 1](../docs/imgs/k8s-step1.png)

The first step is to create a deployment file ([platforms-depl.yaml](./platforms-depl.yaml)) with the following content:

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: platforms-depl
spec:
  replicas: 1
  selector:
    matchLabels:
      app: platformservice
  template:
    metadata:
      labels:
        app: platformservice
    spec:
      containers:
        - name: platformservice
          image: <namespace>/platformservice:latest
```

Then we can create the deployment with the following command:

```bash
kubectl apply -f platforms-depl.yaml
```

And there we have it, the deployment is created, you can check it with the following command:

```bash
kubectl get deployments
```

And we can check the status of the pods with the following command:

```bash
kubectl get pods
```

---

## Creating the NodePort service

The next step is to create a **NodePort** service to expose the Platform service to the outside world, so we can access it from a Postman or any other HTTP client.

![k8s step 2](../docs/imgs/k8s-step2.png)

The first step is to create a service file ([platforms-np-srv.yaml](./platforms-np-srv.yaml)) with the following content:

```yaml
apiVersion: v1
kind: Service
metadata:
  name: platformnpservice-srv
spec:
  type: NodePort
  selector:
    app: platformservice
  ports:
    - name: platformservice
      protocol: TCP
      port: 8080
      targetPort: 8080
```

We are mapping the port 8080 from the NodePort service to the targetPort 8080 which is the port that the application is listening to.

Then we can create the service with the following command:

```bash
kubectl apply -f platforms-np-srv.yaml
```

And there we have it, the service is created, you can check it with the following command:

```bash
kubectl get services
```

In order to access the service from the outside world, we need to get the NodePort that was assigned to the service, we can do that with the following command:

```bash
kubectl describe service platformnpservice-srv
```

By now you should be able to access the service from the Postman or any other HTTP client.

---

## Creating the ClusterIP service

### Recap

In the previous section we created the HTTP Client to call the Command service, we've stablished the connection between the Platform and Command services, but it's still running/attached to the localhost.

![k8s step 3](../docs/imgs/k8s-step3.png)

_ps: skipping the docker build and push to the registry part, as it's already covered in the previous sections._

### ClusterIP service

In this section we are making this two services (Platform and Container) talk to each other via ClusterIP Services in Kubernetes.

So the endpoint that the Platform service is going to reach out to is the ClusterIP service attached to the Command service pod as shown below:

![k8s step 4](../docs/imgs/k8s-step4.png)

The first step is to create a service, to keep it simple we are going to create the ClusterIP service in the same file ([platforms-depl.yaml](./platforms-depl.yaml)) that we created the deployment.

Let's add the ClusterIP service to the file:

```yaml
---
apiVersion: v1
kind: Service
metadata:
  name: platforms-clusterip-srv
spec:
  type: ClusterIP
  selector:
    app: platformservice
  ports:
    - name: platformservice
      protocol: TCP
      port: 8080
      targetPort: 8080
```

Basically, the `name` defined in the `metadata` is the endpoint that the Platform service is going to call.

Then, we're going to create the ClusterIP service for the Command service as well.

You can check the file [commands-depl.yaml](./commands-depl.yaml) to see the ClusterIP service for the Command service, it's pretty much the same as the one we created for the Platform service.

After that, before we apply the files, we need to create a [`appsettings.Production.json`](../src/PlatformService/appsettings.Production.json) file to configure the Platform service to reach out to the Command service endpoint via the ClusterIP service.

```json
{
  "CommandService": "http://commands-clusterip-srv:8080/c/platforms"
}
```

As we changed the Platform service, we need to repackage the image and push it to the registry.

After that, we can apply the files:

```bash
kubectl apply -f platforms-depl.yaml
kubectl apply -f commands-depl.yaml
```

Just a reminder, as we already had a deployment for the Platform service, we need to refresh the deployment to apply the changes, we can do that with the following command:

```bash
kubectl rollout restart deployment platforms-depl
```

And that's it, the services are now talking to each other via ClusterIP services.

---
