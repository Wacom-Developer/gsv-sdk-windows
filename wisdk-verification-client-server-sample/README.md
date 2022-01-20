# Wacom Ink SDK for verification Sample Server

This repository contains the **Wacom Ink SDK for verification** sample server and client applications. 

## Project Layout

Before you build the sample, you will need to install the verification SDK on the build machine. After the engine is installed, open the project solution file under `GSV-Server-Client/GSV-Server-Client.sln`. 

The solution consists of 2 components that make up the sample client application:

* GSV-Client-Framework - Contains the interfaces used for server communication 
* WacomInkVerificationSample - UI Application that integrates the client framework 

And 3 server side components:

* Persistance - Framework that is used to configure the persistence layer on the server (currently uses a SQLite DB for demo)
* GSV-Server - Worker component that uses the verification SDK engine to perform verification requests
* API - .net core API that provides the REST interface of the server

## Building the sample

After setting the configuration files to match your system layout, select the API project and select  `Debug->Start without debugging`  to start the server component on the system. Once this has built, the server will run and the APi documentation can be viewed using the local swagger implementation:

https://localhost:5001/swagger/index.html

You can now build the client app by selecting the `WacomInkVerificationSample` target in the solution and selecting the debug option. This will allow you to create a new template, capture or load signatures and send the verification requests to the local running server.



    
