# HttpConnector
Simple library which enables API calls to be made in one coherent method call.

[![nuget](https://img.shields.io/nuget/v/TobyMeehan.Http)](https://www.nuget.org/packages/TobyMeehan.Http/)

## Usage
The library acts as a wrapper around [HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient), and as such one needs to be instatiated to use it. As per the documentation, there should only be one instance per application. If you are using dependency injection, add a singleton or scoped. Otherwise create a static class with an HttpClient property.

### Using Dependency Injection
```cs
services.AddSingleton<HttpClient>();
```

### Without Dependency Injection
```cs
public static class ClassName
{
    public static HttpClient Client { get; set; } = new HttpClient();
}
```

### Basic Structure

The basic structure for sending requests is below, demonstrated with GET and POST. There is a handler for a success and non-success status code.

```cs
await Client.Get("https://api.example.com/endpoint")
    .OnBadRequest<T>((result, statusCode, reasonPhrase) =>
    {
        // do something
    })
    .OnOK<U>((result) =>
    {
        // do something
    })
    .SendAsync();
```

```cs
Data data = GetData();

await Client.Post("https://api.example.com/endpoint", data)
    .OnBadRequest<T>((result, statusCode, reasonPhrase) =>
    {
        // do something
    })
    .OnOK<U>((result) =>
    {
        // do something
    })
    .SendAsync();
```

DELETE and PUT follow the same structure as GET and POST respectively. I included a type parameter in the handler for BadRequest in case of a situation where some data structure accompanies non-success response. If this is not the case, as in the example project use `object` or `dynamic` as the type and ignore the parameter.


### Sending Custom HttpContent

There are additional request types for PUT and POST, for cases where a custom HttpContent needs to be sent, rather than have an object serialised to JSON.

```cs
HttpContent content = GetHttpContent();

Client.PostHttpContent("https://api.example.com/endpoint", content)
    ...
    
Client.PutHttpContent("https://api.example.com/endpoint", content)
    ...
```

### Generic Response Handlers

Sometimes specific actions are needed to handle specific status codes. The default OnOK and OnBadRequest methods cover all success and non-success status codes, so the generic `On` method can be used to assign a handler to a status code.

```cs
await Client.Get("https://api.example.com/endpoint")
    .On<T>(HttpStatusCode.Forbidden, (result) =>
    {
        // do something
    }
    .On<U>(HttpStatusCode.NotFound, (result) =>
    {
        // do something else
    }
```

Multiple generic handlers can be added for different status codes in every request.

### Unconditional Response Handlers

For situations where a task always needs to be completed regardless of the state of the response, unconditional handlers can be added to a request, and will always run when a response is received. The unconditional handler is run before any conditional handlers. The `Always` method adds an unconditional handler to a request.

```cs
await Client.Get("https://api.example.com/endpoint")
    .Always<T>((result, statusCode) =>
    {
        // do something
    }
```

Each request only supports a single unconditional handler, if `Always` is called multiple times, the handler will be overwritten.

Included with the repo is an example console application which uses the library to call each endpoint at http://dummy.restapiexample.com/, and demonstrates the uses of different requests and response handlers.
