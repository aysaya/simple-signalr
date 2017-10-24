
# A basic implementation of live rate feed using .NET Core 2.0, SignalR, Azure Service Bus, Azure Cosmos DB.
This is a simple implementation of message sending to a Queue, Pub/Sub through Topics and Subscriptions, SignalR and Angular, and Cosmos DB as document storage. 


## blogs:
* [Creating Live Rate Feed](https://messagedriven.wordpress.com/)

## what you will need:
* Visual Studio 2017
  or
* VS Code
* dotnet CLI

## Visual Studio steps
* open the projects (RateWebhook, QueueEngine, Pricing, Notification, Payments.App) in Visual Studio.
* [add user secrets](https://messagedriven.wordpress.com/2017/09/03/managing-user-secrets/)
* build and run.
* [use postman to send to webhook sample payload](https://messagedriven.wordpress.com/2017/08/26/sending-and-consuming-messages-in-azure-service-bus/)

## VS Code steps
* [add user secrets](https://messagedriven.wordpress.com/2017/09/03/managing-user-secrets/)
* in RateWebhook folder 
  open powershell/cmd and type in dotnet run
* in QuoteEngine folder
  open another powershell/cmd and type in dotnet run
* in Pricing folder 
  open powershell/cmd and type in dotnet run
* in Notification folder
  open another powershell/cmd and type in dotnet run
* in Payments.App folder
  open another powershell/cmd and type in dotnet run
  


