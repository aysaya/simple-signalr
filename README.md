
# A basic consumption of messages from Azure Service Bus and .NET Core 2.0

## blogs:
* [A simple message consumption of messages from Azure Service Bus Queues.](http://aysaya.azurewebsites.net/2017/09/02/simple-consumption-of-messages-from-azure-servicebus-queues/)

## what you will need:
* VS Code
* dotnet CLI

## steps
* open vs code terminal window
* create new webapi
* dotnet new webapi -n BasicQueueSender
* dotnet add package Microsoft.Azure.ServiceBus
* create bus connection provider class
* create message sender class
* inject connection provider and message sender in startup
* create secret for the bus connection details
* dotnet run
* dotnet new webapi -n BasicMessageConsumer
* dotnet add package Microsoft.Azure.ServiceBus
* add bus conn provider
* add message handlers
* add class to register the message handlers
* inject dependencies in startup
* register handlers

* run both projects
* open postman to post message to send BasicQueueSender endpoint
* open postman to get the messages processed from the BasicMessageConsumer endpoint


