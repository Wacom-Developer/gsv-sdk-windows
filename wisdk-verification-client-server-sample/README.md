# Wacom Ink SDK for verification Sample Server

This repository contains the **Wacom Ink SDK for verification** sample server and client applications. 

## Project Layout

Before you build the sample, you will need to install the verification SDK on the build machine. After the engine is installed, open the project solution file under `GSV-Server-Client/GSV-Server-Client.sln`. 

The solution consists of 2 components that make up the sample client application:

* GSVClient - Contains the interfaces used for server communication 
* WacomInkVerificationSample - UI Application that integrates the client framework 

And 2 server-side components:

* GsvPersistance - Framework that is used to configure the persistence layer on the server (currently uses a SQLite DB for demo)
* GSVServer - Worker component that uses the verification SDK engine to perform verification requests
  
There is also a .NET core API that provides the REST interface of the server.

## Building the sample

After setting the configuration files to match your system layout, select the GsvServer project and select  `Debug->Start without debugging`  to start the server component on the system. Once this has built, the server will run and the API documentation can be viewed using the local swagger implementation. By default, the URL will be: 

https://localhost:44317/swagger/index.html

**NB:** the port number can be adjusted within *launchSettings.json* and *Program.cs*.

You can now build the client app by selecting the `WacomInkVerificationSample` target in the solution and selecting the debug option. This will allow you to create a new template, capture or load signatures and send the verification requests to the local running server.

# Additional resources 

## Sample Code
For further samples check Wacom's Developer additional samples, see [https://github.com/Wacom-Developer](https://github.com/Wacom-Developer)

## Documentation
For further details on using the SDK see [Wacom Ink SDK for verification documentation](http://developer-docs.wacom.com/sdk-for-verification/) 

## Support
If you experience issues with the technology components, please see related [FAQs](https://developer-support.wacom.com/hc/en-us)

For further support file a ticket in our **Developer Support Portal** described here: [Request Support](https://developer-support.wacom.com/hc/en-us/requests/new)

## Developer Community 
Join our developer community:

- [LinkedIn - Wacom for Developers](https://www.linkedin.com/company/wacom-for-developers/)
- [Twitter - Wacom for Developers](https://twitter.com/Wacomdevelopers)

## License 
This sample code is licensed under the [MIT License](https://choosealicense.com/licenses/mit/)

---

    
