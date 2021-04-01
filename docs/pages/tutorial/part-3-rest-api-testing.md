---
title: Tutorial Part 3 - REST API Testing
layout: single
permalink: /tutorial/part-3-rest-api-testing/
toc: true
---

In this part of the tutorial, you will use Boa Constrictor to automate REST API tests for [Dog API](https://dog.ceo/dog-api/),
a public API for random pictures of dogs.
The first set of tests will show the basics of Boa Constrictor's REST API interactions,
while the second set of tests will show advanced techniques.


**RestSharp:**
Boa Constrictor's REST API interactions use [RestSharp](https://restsharp.dev/).
{: .notice--info}

**Prerequisite:**
Make sure you already completed both [Part 1]({{ "/tutorial/part-1-setup" | relative_url }})
and [Part 2]({{ "/tutorial/part-2-web-ui-testing" | relative_url }}).
Part 3 expects you to use the same example project
and to already know [Screenplay]({{ "/getting-started/screenplay" | relative_url }}) concepts.
{: .notice--warning}


## Basic Interactions

Boa Constrictor adapts [RestSharp](https://restsharp.dev/) into the Screenplay Pattern.
Basic Screenplay calls are thin wrappers around RestSharp calls.
Making REST API calls through Boa Constrictor provides automatic logging and enables them to be called by other interactions.


### 1. Creating a Test Class with an Actor

Inside the `Boa.Constrictor.Example` project,
create a new file in the `Tests` directory named `ScreenplayRestApiBasicTest.cs`.
Add the following code to the file:

```csharp
using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using NUnit.Framework;

namespace Boa.Constrictor.Example
{
    public class ScreenplayRestApiBasicTest
    {
        private IActor Actor;

        [SetUp]
        public void InitializeScreenplay()
        {
            Actor = new Actor(name: "Andy", logger: new ConsoleLogger());
        }
    }
}
```

`ScreenplayRestApiBasicTest` will contain the NUnit test cases for the first half of Part 3.
Each test case should use its own Actor,
which is initialized in the `[SetUp]` method.
The line to create the Actor is the same as
[the Actor line from `ScreenplayWebUiTest`]({{ "/tutorial/part-2-web-ui-testing/#2-creating-the-actor" | relative_url }}).


### 2. Adding REST API Abilities

Add the following import statements to `ScreenplayRestApiBasicTest`:

```csharp
using Boa.Constrictor.RestSharp;
using RestSharp;
```

Then, add this line to the `[SetUp]` method:

```csharp
Actor.Can(CallRestApi.Using(new RestClient("https://dog.ceo/")));
```

Boa Constrictor's `CallRestApi` Ability enables Actors to call REST APIs using RestSharp.
It is part of the `Boa.Constrictor.RestSharp` namespace.
Let's unpack how this Ability works:

| Code | Purpose |
| ---- | ------- |
| `Actor.Can(...)` | Adds the given Ability to the Actor. |
| `CallRestApi.Using(...)` | Constructs the Ability with the given RestSharp client. |
| [`RestClient`](https://restsharp.dev/getting-started/getting-started.html#basic-usage) | A RestSharp class for REST API clients that holds information (like base URL and authentication) and executes requests. |
| `"https://dog.ceo/"` | Dog API's base URL. |

In this line, the `actor` Actor is given a `CallRestApi` Ability with a `RestClient` object that targets `"https://dog.ceo/"`.
The code for this Ability should look similar to the code for [adding Web UI Abilities]({{ "/tutorial/part-2-web-ui-testing/#3-adding-web-ui-abilities" | relative_url }}).

**Authentication:**
Boa Constrictor does not handle authentication, but RestSharp does.
You can add [authenticators](https://restsharp.dev/usage/authenticators.html#using-simpleauthenticator)
directly to the RestSharp client.
{: .notice--warning}


### 3. Modeling Requests

The REST API endpoint we want to test is the
[HTTP `GET` method](https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods/GET) for
[https://dog.ceo/api/breeds/image/random](https://dog.ceo/api/breeds/image/random).
It should yield a response like this:

```json
{
    "message": "https://images.dog.ceo/breeds/schipperke/n02104365_9489.jpg",
    "status": "success"
}
```

Since this is a basic `GET` request with no headers or body,
you could even enter the absolute URL
[directly into your web browser](https://dog.ceo/api/breeds/image/random)
to test the response.

The response has two parts:

| Field | Value |
| ----- | ----- |
| `message` | A hyperlink to a random picture of a dog. This link should be different every time the request is called. |
| `status` | A string indicating the success-or-failure status of fetching the image link. |

RestSharp uses the [`IRestRequest` interface](https://restsharp.dev/usage/parameters.html)
for creating requests that the client executes.
`IRestRequest` supports all types of request fields: headers, parameters, bodies, etc.
Boa Constrictor does *not* add anything on top - it uses `IRestRequest` directly for interactions.

Requests can be long.
Many tests may need to call the same requests, too.
As a best practice, requests typically should not be written in-line where they are called.
Instead, they should be written in separate classes as builer methods so that they can be reused anywhere,
just like [locators for web elements]({{ "/tutorial/part-2-web-ui-testing/#4-modeling-web-pages" | relative_url }}).

Create a new directory in the `Boa.Constrictor.Example` project named `Requests`.
Inside this new folder, create a new file named `DogRequests.cs` with the following content:

```csharp
using RestSharp;

namespace Boa.Constrictor.Example
{
    public static class DogRequests
    {
        public static IRestRequest GetRandomDog() =>
            new RestRequest("api/breeds/image/random", Method.GET);
    }
}
```

`DogRequests` is a class that contains builder methods for `IRestRequest` objects.
Like [page classes with locators]({{ "/tutorial/part-2-web-ui-testing/#4-modeling-web-pages" | relative_url }}),
it is *static* so that it does not maintain any state of its own.
It only provides builders.

The `GetRandomDog` method constructs a new RestSharp request.
The request's method is `GET`, and its resource path is `api/breeds/image/random`.
You can use `IRestRequest`'s fluent syntax to add other fields, like headers and parameters.
Builder methods like this should have descriptive names and declarative bodies.
They may also take in arguments to customize parts of the request, such as IDs or request parameter values.


### 4. Calling Basic Requests

For our first test, we will call the Dog API endpoint and verify a successful response.
Add the following test stub to `ScreenplayRestApiBasicTest`:

```csharp
[Test]
public void TestDogApiStatusCode()
{

}
```

To call the endpoint, add the following code to the test:

```csharp
var request = DogRequests.GetRandomDog();
var response = Actor.Calls(Rest.Request(request));
```

The first line builds the request object.
The second line calls the request using a Boa Constrictor interaction.
Read that second line in plain English:
"The actor calls the REST request to get a random dog."
Let's break it down:

| Code | Purpose |
| ---- | ------- |
| `response` | The `IRestResponse` object returned by the REST API call. |
| `Actor.Calls(...)` | Calls any type of interaction. Alias for `Actor.AsksFor(...)` or `Actor.AttemptsTo(...)`. |
| `Rest.Request(...)` | The Question that calls the given request. Under the hood, it uses the Ability's `IRestClient` object to execute the given `IRestRequest` object. |
| `request` | The `IRestRequest` object for calling the Dog API endpoint. |

The `Rest` class shown in the code is actually syntactic Screenplay sugar.
Boa Constrictor's REST requests are actually `RestApiCall` Questions that return `IRestResponse` answers.
The two lines below are equivalent:

```csharp
// The concise, readable way to call REST APIs
Actor.Calls(Rest.Request(request));

// The "traditional" way to call REST APIs
Actor.AsksFor(new RestApiCall(request));
```

**Warning:**
Do not try to call `RestApiCall` directly.
Its constructor is not public.
The example above serves only to show how REST requests follow the Screenplay Pattern.
Use `Rest.Request(...)` to call REST API interactions.
{: .notice--danger}

All REST requests return RestSharp `IRestResponse` objects.
Just like for requests, Boa Constrictor does *not* add anything to RestSharp's responses.
It simply passes response objects through the interaction.
Check the [RestSharp docs for `IRestResponse`](https://restsharp.dev/api/RestSharp.html#interface-irestresponse)
to see all response values.

The simplest way to verify if the call was successful is to check the response code.
The recommended assertion library to use with Boa Constrictor is
[Fluent Assertions](https://fluentassertions.com/).
To check the status code, add the following import statements to `ScreenplayRestApiBasicTest`:

```csharp
using FluentAssertions;
using System.Net;
```

Then, add this line to `ScreenplayRestApiBasicTest`:

```csharp
response.StatusCode.Should().Be(HttpStatusCode.OK);
```

Build and run the test.
It should take about 1 second to execute, and it should pass.
If you want to make sure the assertion is really working,
you can temporarily change it to `Should().NotBe(HttpStatusCode.OK)` and watch the test fail.


### 5. Parsing Response Bodies

(Coming soon!)

* Use type generics to automatically parse
* Check more fields from Data
* Info: you can make custom serializers
* Warning: be careful with request parts
* Warning: duplication in test cases is for examples


## Advanced Interactions

(Coming soon!)


### 6. Creating Another Test Class

(Coming soon!)


### 7. Calling Multiple Base URLs

(Coming soon!)

(Warn about customizing base url for different environments)


### 8. Downloading Files

(Coming soon!)


### 9. Dumping Responses

(Coming soon!)


### 10. Simplifying Calls for Custom Abilities

(Coming soon!)

(Info block - this isn't required)


## Conclusion

(Coming soon!)
