# HttpConnector
Simple library which enables API calls to be made in one coherent method call.

## Usage
The library acts as a wrapper around [HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient), and as such one needs to be instatiated to use it. As per the documentation, there should only be one instance per application. If you are using dependency injection, add a singleton or scoped. Otherwise create a static class with an HttpClient property.

### Using Dependency Injection
```cs
HttpClient client = new HttpClient();
client.Init();
services.AddSingleton(client);
```

### Without Dependency Injection
```cs
public static class ClassName
{
    public static HttpClient Client { get; set; } = new HttpClient();
}
```
```cs
class Program
{
    static void Main(string[] args)
    {
        Client.Init();
    }
}
```

Calling an API endpoint is, for the most part, universal for all protocols. There are differences, for example a POST request might accompany data, whereas GET would not. The BadRequest referred to by the OnBadRequest method is a catchall for any status code that does not indicate success, not specifically the Bad Request status code. The actual status code received is included as a parameter for the response handler, along with the reason phrase, if there is one.

```cs
await Client.Get("https://api.example.com/endpoint")
    .OnBadRequest<BadRequestType>((result, statusCode, reasonPhrase) =>
    {
        // do something
    })
    .OnOK<OKType>((result) =>
    {
        // do something
    })
    .SendAsync();
```

```cs
Data data = GetData();

await Client.Post("https://api.example.com/endpoint", data)
    .OnBadRequest<BadRequestType>((result, statusCode, reasonPhrase) =>
    {
        // do something
    })
    .OnOK<OKType>((result) =>
    {
        // do something
    })
    .SendAsync();
```

DELETE and PUT follow the same structure as GET and POST respectively. I included a type parameter in the handler for BadRequest in case of a situation where some data structure accompanies non-success response. If this is not the case, as in the example project use `object` or `dynamic` as the type and ignore the parameter.

Included with the repo is an example console application which uses the library to make a GET request to the [sunrise-sunset.org](https://sunrise-sunset.org/api) API, using the example URLs they provide.
