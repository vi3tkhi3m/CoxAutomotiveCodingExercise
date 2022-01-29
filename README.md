# Cox Automotive Programming Challenge

## Instructions:

Using the provided [API](http://api.coxauto-interview.com/), create a program that retrieves a datasetID, retrieves all vehicles and dealers for that dataset, and successfully posts to the **answer** endpoint. Each vehicle and dealer should be requested only once. You will receive a response structure when you post to the **answer** endpoint that describes status and total ellapsed time; your program should output this response.

The server has a built in delay that varies between 1 and 4 seconds for each request for a vehicle or for a dealer. **Focus on optimizing for low total elapsed time between retrieving the datasetid and posting the answer. A successful submission will complete in significantly less than 30 seconds.**

Your program should be implemented in the primary language for the position you are applying for. If there is uncertainty about this, please reach out to us. There is information about client code generation [here](http://api.coxauto-interview.com/client).

The cheat endpoint returns a correct answer for the dataset. It is provided for debugging purposes; it should not be used in your submitted program.

This exercise is designed to take approximately 1-2 hours.

## About this project:

The project is developed in .NET 6. You can access the Swagger UI from https://localhost:7279/swagger/index.html and execute the /DataSet endpoint there. Or you can use Postman to send a GET request to https://localhost:7279/DataSet.

This project has the following NuGet packages:
- AutoMapper
- AutoMapper.Extensions.Microsoft.DependencyInjection
- RestSharp
- Moq

## Result:
![Challenge Result](https://i.ibb.co/fdCk6hd/result.jpg "Challenge Result")
