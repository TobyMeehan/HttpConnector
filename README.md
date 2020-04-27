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

### Sending a Request

The library extends `HttpClient` with methods for GET, POST, PUT and DELETE, in the form `HttpClient.[Protocol]`. This creates an `IHttpRequest` object representing the request. POST and PUT have an additional parameter for data to send with the request. The `SendAsync` method sends the request.

```cs
await Client.Get("https://api.example.com/endpoint").SendAsync();

await Client.Post("https://api.example.com/endpoint", data).SendAsync();
```

By default, the library serializes the data to JSON and attaches it as `JsonContent`. If you need to send custom `HttpContent`, for example attaching files, you can use the `[Protocol]HttpContent` methods.

```cs
await Client.PostHttpContent("https://api.example.com/endpoint", httpContent).SendAsync();

await Client.PutHttpContent("https://api.example.com/endpoint", httpContent).SendAsync();
```

### Response Handlers

`IHttpRequest` allows you to attach response handlers; delegates which are invoked for different response status codes. For basic success and non-success responses, use the `OnOK` and `OnBadRequest` handlers. These cover all status codes which indicate success or otherwise.

```cs
await Client.Get("https://api.example.com/endpoint")
    .OnOK(() => 
    {
        // do something
    })
    .OnBadRequest(() => 
    {
        // do something else
    })
    .SendAsync();
```

If you need to handle a specific status code, you can use generic response handlers, which are invoked for their specified status code.

```cs
await Client.Get("https://api.example.com/endpoint")
    .On(HttpStatusCode.Forbidden, () =>
    {
        // do something
    })
    .On(HttpStatusCode.NotFound, () => 
    {
        // do something else
    })
    .SendAsync();
```

Unconditional handlers can be used if you need something to happen regardless of the status code.

```cs
await Client.Get("https://api.example.com/endpoint")
    .Always(() =>
    {
        // do something
    })
    .SendAsync();
```

#### Reading the Response

Response handlers can accept a generic type parameter, to which any JSON in the response will be deserialized.

```cs
await Client.Get("https://api.example.com/endpoint")
    .OnOK<T>((response) =>
    {
        Foo(response.Property);
    })
    .OnBadRequest<U>((response) =>
    {
        Bar(response.DifferentProperty);
    })
    .SendAsync();
```

If you need the status code of the response, you can include a status code parameter in the handler delegate.

```cs
await Client.Get("https://api.example.com/endpoint")
    .OnOK<T>((response, statusCode) =>
    {
        Foo(response);
        Bar(statusCode);
    })
    .OnBadRequest((statusCode) =>
    {
        Bar(statusCode);
    })
    .SendAsync();
```

Included with the repo is an example console application which uses the library to call each endpoint at http://dummy.restapiexample.com/, and demonstrates the uses of different requests and response handlers.
