# Flipt REST Client Generation Guide

This guide outlines the steps to generate a REST client for Flipt using the `nswag` tool, based on the Flipt OpenAPI specification.

## Prerequisites

Before starting, ensure you have the following prerequisites installed on your system:

- **.NET 8.0 SDK or higher**: Necessary for running the `nswag` tool. It can be downloaded from [https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download).

- **NSwag**: This tool generates client code for REST APIs described by OpenAPI specifications. Install it globally using the .NET CLI with the following command:

```
dotnet tool install -g Microsoft.dotnet-openapi
```


## Updating the REST Client

To generate or update the Flipt REST client, follow these steps:

### 1. Download the OpenAPI Specification

First, download the latest `openapi.yml` file from the Flipt GitHub repository. This can be done manually or by using a command like `curl`:

```

curl https://raw.githubusercontent.com/flipt-io/flipt-openapi/main/openapi.yml -o openapi.yml
```

### 2. Generate the Client Code

With the `openapi.yml` file in your working directory, run the following `nswag` command to generate the REST client code. Make sure to correct the command as shown below:


```
nwsag openapi2csclient /className:FliptRestClient /namespace:Flipt.Rest /input:"openapi.yml" /output:"./Flipt.Rest.Client.cs" /GenerateExceptionClasses:true /OperationGenerationMode:SingleClientFromPathSegments /JsonLibrary:SystemTextJson /GenerateOptionalParameters:true /GenerateDefaultValues:true /GenerateResponseClasses:true /GenerateClientInterfaces:true /GenerateClientClasses:true /GenerateDtoTypes:true /ExceptionClass:FliptException /GenerateNativeRecords:true /UseBaseUrl:false /GenerateBaseUrlProperty:false 
```


## Notes

- Ensure the `nswag` CLI tool is correctly installed and accessible from your terminal or command prompt.
- The command provided generates a C# client for interacting with the Flipt API, leveraging the System.Text.Json library for JSON serialization/deserialization.
- The generated client will include features such as exception classes, optional parameters, default values, response classes, client interfaces, DTO types, and native records, according to the specified options.
- This process assumes you're working in a directory that contains the `openapi.yml` file and will generate the `Flipt.Rest.Client.cs` file in the same directory.

