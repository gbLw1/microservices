# Command Service

## Endpoints

The command service is a service that manages a list of commands for a specific platform. It has the following endpoints:

- **GET ~/c/platforms**: Returns a list of all platforms.

  This endpoint will return a list of all platforms from the command's service own database.

- **GET ~/c/platforms/{platformId}/commands**: Returns a list of all commands for a specific platform.
- **GET ~/c/platforms/{platformId}/commands/{commandId}**: Returns a specific command for a specific platform.
- **POST ~/c/platforms/{platformId}/commands**: Creates a new command for a specific platform.

## Message Broker

As the command service is the consumer, we need to have a background service that listens to the message broker and processes the messages. We have the [`MessageBusSubscriber.cs`](./AsyncDataServices/MessageBusSubscriber.cs) class that is responsible for listening to the message broker.

It connects to the RabbitMQ server and binds to the queue to receive messages and process them.

To process the messages, we have the [`EventProcessor.cs`](./EventProcessing/EventProcessor.cs) class that is responsible for processing the messages.

It basically deserializes the message and determines the type of event that was published and processes it accordingly (in our case, it creates a platform in the command service database).
