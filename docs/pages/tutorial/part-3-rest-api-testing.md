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

RestSharp uses the [`IRestRequest` interface](https://restsharp.dev/api/restsharp.html#interface-irestrequest)
for creating requests that the client executes.
`IRestRequest` supports all types of
[request fields](https://restsharp.dev/usage/parameters.html):
headers, parameters, bodies, etc.
Boa Constrictor does *not* add anything on top - it uses `IRestRequest` directly for interactions.

Requests can be long.
Many tests may need to call the same requests, too.
As a best practice, requests typically should not be written in-line where they are called.
Instead, they should be written in separate classes as builder methods so that they can be reused anywhere,
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
Like page classes with locators, it is *static* so that it does not maintain any state of its own.
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

The `Rest` class shown in the code is actually [syntactic Screenplay sugar](https://en.wikipedia.org/wiki/Syntactic_sugar).
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

Then, add this line to `TestDogApiStatusCode`:

```csharp
response.StatusCode.Should().Be(HttpStatusCode.OK);
```

The completed test case should now look like this:

```csharp
[Test]
public void TestDogApiStatusCode()
{
    var request = DogRequests.GetRandomDog();
    var response = Actor.Calls(Rest.Request(request));

    response.StatusCode.Should().Be(HttpStatusCode.OK);
}
```

Build and run the test.
It should take about 1 second to execute, and it should pass.
If you want to make sure the assertion is really working,
you can temporarily change it to `Should().NotBe(HttpStatusCode.OK)` and watch the test fail.


### 5. Deserializing Response Bodies

Checking a response's status code is a valuable assertion,
but checking a response's content is arguably more important.
Most response bodies are formatted using JSON or XML.
For convenience,
RestSharp can automatically [deserialize](https://restsharp.dev/usage/serialization.html) responses.
Deserialized objects make it possible to check fields in response bodies,
such as the `message` and `status` strings from the Dog API responses.

To deserialize the content of an `IRestResponse` object,
RestSharp needs a class with properties or instance variables that match the structure of the response body.
Create a new directory named `Responses` under the `Boa.Constrictor.Example` project.
Then, create a new file named `DogResponses.cs` in this folder with the following code:

```csharp
namespace Boa.Constrictor.Example
{
    public class DogResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
```

`DogResponse` is the serialization class for Dog API's `GET` response.
Notice how its properties mirror the structure of the API's actual JSON response:

```json
{
    "message": "https://images.dog.ceo/breeds/schipperke/n02104365_9489.jpg",
    "status": "success"
}
```

Let's write a new test case to show how to use `DogResponse` for deserializing responses.
Add the following test stub to `ScreenplayRestApiBasicTest`:

```csharp
[Test]
public void TestDogApiContent()
{

}
```

Next, add the following code to the new test:

```csharp
var request = DogRequests.GetRandomDog();
var response = Actor.Calls(Rest.Request<DogResponse>(request));
```

Compare this call to the one from the previous test, `TestDogApiStatusCode`.
The only difference is the `DogResponse` type generic tacked onto `Rest.Request<DogResponse>(...)`.
Adding the type generic makes the interaction automatically deserialize the response body into the given type.
Boa Constrictor simply passes it through to RestSharp.
The response object will by typed as `IRestResponse<DogResponse>`,
and it will have a special member named `Data` that is the `DogResponse` object parsed from the response's body.

Finally, add assertions to the test:

```csharp
response.StatusCode.Should().Be(HttpStatusCode.OK);
response.Data.Status.Should().Be("success");
response.Data.Message.Should().NotBeNullOrWhiteSpace();
```

The first assertion is the same status code check.
However, the second and third assertions check values in `response.Data`.
The status should indicate success, and the message should contain a URL to a random image.

**Custom Serializers:**
RestSharp lets you provide [custom serializers](https://restsharp.dev/usage/serialization.html)
for request and response bodies.
You can use serializers provided by RestSharp,
such as one for [Json.Net](https://restsharp.dev/usage/serialization.html#newtonsoftjson-aka-json-net),
or you can [implement your own](https://restsharp.dev/usage/serialization.html#custom).
Simply add custom serializers directly to the RestSharp client object before adding the `CallRestApi` Ability to the Actor.
{: .notice--info}

**Troubleshooting:**
Requests can be tricky to call and deserialize properly.
Make sure to specify all parts carefully.
{: .notice--warning}

The completed test should look like this:

```csharp
[Test]
public void TestDogApiContent()
{
    var request = DogRequests.GetRandomDog();
    var response = Actor.Calls(Rest.Request<DogResponse>(request));

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    response.Data.Status.Should().Be("success");
    response.Data.Message.Should().NotBeNullOrWhiteSpace();
}
```

Build and run the test.
It should pass.

**Test Duplication:**
This tutorial added two very similar tests to `ScreenplayRestApiBasicTest`.
`TestDogApiContent` essentially supersedes `TestDogApiStatusCode`.
In a real-world test project, `TestDogApiStatusCode` should arguably be removed.
However, this tutorial project retains both to provide side-by-side examples of calling REST APIs with and without deserializing responses.
{: .notice--danger}



## Advanced Interactions

The first half of Tutorial Part 3 shows how to call basic REST APIs using Boa Constrictor.
The `ScreenplayRestApiBasicTest` tests showed how to call the Dog API endpoint to get a random image of a dog.
However, endpoint did not return an image file - it merely returned a *hyperlink* to an image file.
Downloading the actual image requires some advanced REST API techniques, which will be covered next.


### 6. Creating Another Test Class

Let's create a second test class for "advanced" tests.
Create a class named `ScreenplayRestApiAdvancedTest.cs` under the `Tests` folder,
and add the following code:

```csharp
using Boa.Constrictor.Logging;
using Boa.Constrictor.RestSharp;
using Boa.Constrictor.Screenplay;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;

namespace Boa.Constrictor.Example
{
    public class ScreenplayRestApiAdvancedTest
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

This initial code stub should look very similar to the
[stub for the basic tests](#1-creating-a-test-class-with-an-actor).

**Imports:**
For convenience, this stub includes all the `using` statements from the start.
The previous steps showed which parts come from which namespaces.
{: .notice--info}


### 7. Calling Multiple Base URLs

Downloading the image file for the randomly-selected dog poses a problem.
Compare the base URLs (in **bold** text below) between the Dog API and the image hyperlink:

| Service | URL |
| ------- | --- |
| Dog API | **https://dog.ceo/**api/breeds/image/random |
| Dog Image API | **https://images.dog.ceo/**breeds/schipperke/n02104365_9489.jpg |

Unfortunately, the base URLs are different.
They will need to use different RestSharp clients.
Unfortunately, the Actor from the basic tests was set up to use only *one* RestSharp client.
We need to enable the Actor to use *multiple* clients.

**Overriding Base URLs:**
RestSharp allows request objects to override the client's base URL.
You could use an `IRestClient` client with the `https://dog.ceo/` base URL to execute an `IRestRequest` request
whose resource is the absolute url `https://images.dog.ceo/breeds/schipperke/n02104365_9489.jpg`.
However, this is not good practice because it can make automation code confusing to understand.
{: .notice--warning}

The best way to enable Actors to call REST APIs with multiple base URLs is to create a custom Ability for each.
Each Ability can have its own `IRestClient` object.
Then, interactions can choose the Ability to use via type generics.

Start by creating a new directory named `Abilities` in the `Boa.Constrictor.Example` project.
In this new folder, add two new classes named `CallDogApi.cs` and `CallDogImagesApi.cs`.

Add the following code to `CallDogApi.cs`:

```csharp
using Boa.Constrictor.RestSharp;
using RestSharp;

namespace Boa.Constrictor.Example
{
    public class CallDogApi : AbstractRestSharpAbility
    {
        public const string BaseUrl = "https://dog.ceo/";

        private CallDogApi(RestClient client) :
            base(client) { }

        public static CallDogApi UsingBaseUrl() =>
            new CallDogApi(new RestClient(BaseUrl));
    }
}
```

And add the following code to `CallDogImagesApi.cs`:

```csharp
using Boa.Constrictor.RestSharp;
using RestSharp;

namespace Boa.Constrictor.Example
{
    public class CallDogImagesApi : AbstractRestSharpAbility
    {
        public const string BaseUrl = "https://images.dog.ceo/";

        private CallDogImagesApi(RestClient client) :
            base(client) { }

        public static CallDogImagesApi UsingBaseUrl() =>
            new CallDogImagesApi(new RestClient(BaseUrl));
    }
}
```

Both of these new classes are custom Abilities.
They extend `AbstractRestSharpAbility`, which provides helpful properties like a `IRestClient` object.
They also declare base URLs as constants for convenience.
Their builder methods use the base URLs to construct `RestClient` objects that get passed through to the base class's constructor.

**Hard-Coded URLs:**
Hard-coding URLs in code is typically not a good practice.
Typically, automated tests need to run against different environments, which use different base URLs.
Tests should read URLs as inputs from an external source (like a config file).
This tutorial hard-codes URLs merely for simplicity.
{: .notice--danger}

Next, add these new Abilities to the Actor.
Add the following code to the `[SetUp]` method in `ScreenplayRestApiAdvancedTest`:

```csharp
Actor.Can(CallDogApi.UsingBaseUrl());
Actor.Can(CallDogImagesApi.UsingBaseUrl());
```

Now, the Actor can use two different RestSharp clients!
The `CallRestApi` Ability from the [basic tests](#2-adding-rest-api-abilities)
is a "default" or "generic" RestSharp Ability,
whereas the `CallDogApi` and `CallDogImagesApi` are "custom" Abilities.

The Actor could directly access the `IRestClient` objects through the Abilities like this:

```csharp
var dogClient = Actor.Using<CallDogApi>().Client;
var dogImagesClient = Actor.Using<CallDogImagesApi>().Client;
```

However, the Actor should avoid calling these clients directly.
Instead, it should use Boa Constrictor's REST request Question like this:

```csharp
var response =
    Actor.Calls(
        Rest<CallDogApi>
            .Request<DogResponse>(
                DogRequests.GetRandomDog()));
```

Let's unpack this line:

| Code | Purpose |
| ---- | ------- |
| `response` | The `IRestResponse` object returned by the REST API call. |
| `Actor.Calls` | Calls any type of interaction. |
| `Rest<CallDogApi>` | The builder class for REST API interactions. Uses a type generic to specify the RestSharp Ability (e.g. `<CallDogApi>`) to use when executing the given request. |
| `Request<DogResponse>` | The builder method that creates a Question to call a RestSharp request and deserialize the response as a `DogResponse` object. |
| `DogRequests.GetRandomDog()` | The builder method that creates an `IRestRequest` object for calling the Dog API endpoint. |

The only difference between this call and the call from the
[basic test in Step 5](#5-deserializing-response-bodies)
is that the `Rest` builder class bears a type generic for the Ability to use.

Let's use this new call in a test.
Add this new test to `ScreenplayRestApiAdvancedTest` to test the new Abilities:

```csharp
[Test]
public void TestDogApi()
{
    var request = DogRequests.GetRandomDog();
    var response = Actor.Calls(Rest<CallDogApi>.Request<DogResponse>(request));

    response.StatusCode.Should().Be(HttpStatusCode.OK);
    response.Data.Status.Should().Be("success");
    response.Data.Message.Should().NotBeNullOrWhiteSpace();
}
```

Build the project and run the tests.
All tests should pass.
The next step will add a test that uses `CallDogImagesApi`.

**Should I make a custom RestSharp Ability?**
If you only need to call APIs with one base URL,
then you can use the "default" `CallRestApi` Ability and avoid adding type generics.
However, if you need to use multiple base URLs,
or if you think you might need to do so in the future,
then create custom RestSharp Abilities.
{: .notice--info}


### 8. Downloading Files

In addition to receiving JSON and XML bodies, REST requests can also receive file data.
For example, a REST API call could download the random dog image given by the Dog API.
Files are just another type of response body.

Boa Constrictor provides a special Question for downloading files.
Take a look at the following code:

```csharp
// Image endpoint: https://images.dog.ceo/breeds/schipperke/n02104365_9489.jpg

var imageRequest = new RestRequest("breeds/schipperke/n02104365_9489.jpg");
byte[] imageData = Actor.Calls(Rest<CallDogImagesApi>.Download(imageRequest, ".jpg"));
```

These lines download the image file as an array of bytes.
Let's break them down:

| Code | Purpose |
| ---- | ------- |
| `byte[] imageData` | The raw data for the downloaded file. |
| `Actor.Calls` | Calls any type of interaction. |
| `Rest<CallDogImagesApi>` | The builder class for REST API interactions using `CallDogImagesApi`. |
| `Download` | A builder method for the Question to download the file given by the request. |
| `imageRequest` | A `RestRequest` object whose resource is the path to the image. |
| `".jpg"` | The extension for the file to download. |

Just like [`Rest.Request`](#4-calling-basic-requests),
`Rest.Download` is syntactic Screenplay sugar.
Underneath, it constructs a `RestApiDownload` Question.
Once the file is downloaded as a byte array,
its contents could be checked with assertions,
or it could be piped to other destinations.

**File Data:**
When Boa Constrictor downloads a file, the file data is stored in memory.
The file is *not* automatically saved to the file system by default.
[Step 11](#11-dumping-responses) shows how to configure RestSharp Abilities to automatically save downloads to the file system.
{: .notice--warning}

Let's write a test to verify that the image can be successfully downloaded.
The test should have the following steps:

1. Call the Dog API to get a random dog image link.
2. Call the Dog Images API to download the image file at the hyperlink returned by the Dog API request.
3. Verify that the file contents are not empty.

The code below implements this test.
Add it to `ScreenplayRestApiAdvancedTest`:

```csharp
[Test]
public void TestDogApiImage()
{
    // Call the Dog API to get a random dog image link
    var request = DogRequests.GetRandomDog();
    var response = Actor.Calls(Rest<CallRestApi>.Request<DogResponse>(request));

    // Call the Dog Images API to download the image file
    var resource = new Uri(response.Data.Message).AbsolutePath;
    var imageRequest = new RestRequest(resource);
    var extension = System.IO.Path.GetExtension(resource);
    var imageData = Actor.Calls(Rest<CallRestImagesApi>.Download(imageRequest, extension));

    // Verify that the file contents are not empty
    imageData.Should().NotBeNullOrEmpty();
}
```

The tricky part about this test is using the values in the first request's response to create the second request.
`new Uri(response.Data.Message).AbsolutePath` parses the resource path from the full hyperlink returned by Dog API.
`System.IO.Path.GetExtension(resource)` gets the file extension from the resource. 

**Download Assertions:**
Checking that the file is not empty is a weak assertion.
It would not determine if the file is incorrect or corrupted.
A real-world test should try stricter assertions.
{: .notice--warning}

**Response Assertions:**
`Rest.Download` returns *only* the file data.
It does not capture other parts of the REST response.
If you need to perform assertions on other parts of the response,
then use `Rest.Request`, and simply read the file data from the response's body.
{: .notice--warning}

Build and run the new test.
It should pass, but it might take a little more time to complete since it makes two requests.


### 9. Creating Workflows

The `TestDogApiImage` test from the previous step is longer than all the other tests.
Not only does it call two requests, but it calls two *interlocking* requests.
The response from the first request becomes part of the second request.
Request sequences like this are common in both applications and test automation.
Many workflows require chains of [CRUD](https://en.wikipedia.org/wiki/Create,_read,_update_and_delete) operations.

Workflows should be written as Screenplay interactions.
Rather than calling a series of REST requests,
an Actor could call just one interaction to perform the workflow.
For example, when fetching random dog images,
the caller probably doesn't care what endpoints need to be called -
they just want the picture of the dog!

Create a new class in the `Interactions` directory named `RandomDogImage.cs`,
and add the follow code to it:

```csharp
using Boa.Constrictor.RestSharp;
using Boa.Constrictor.Screenplay;
using RestSharp;
using System;
using System.IO;

namespace Boa.Constrictor.Example
{
    public class RandomDogImage : IQuestion<byte[]>
    {
        private RandomDogImage() { }

        public static RandomDogImage FromDogApi() =>
            new RandomDogImage();

        public byte[] RequestAs(IActor actor)
        {
            var request = DogRequests.GetRandomDog();
            var response = actor.Calls(Rest<CallDogApi>.Request<DogResponse>(request));

            var resource = new Uri(response.Data.Message).AbsolutePath;
            var imageRequest = new RestRequest(resource);
            var extension = Path.GetExtension(resource);
            var imageData = actor.Calls(DogImagesApi.Download(imageRequest, extension));

            return imageData;
        }
    }
}
```

`RandomDogImage` is a Question that gets a random dog image from Dog API.
Most of its code comes directly from the `TestDogApiImage` test.

Now, refactor the `TestDogApiImage` test to use `RandomDogImage`:

```csharp
[Test]
public void TestDogApiImage()
{
    var imageData = Actor.AsksFor(RandomDogImage.FromDogApi());
    imageData.Should().NotBeNullOrEmpty();
}
```

Read the first line in plain English:
"The actor asks for a random dog image from Dog API."
Concise, descriptive calls like this make the Screenplay Pattern great.
Screenplay's syntax puts focus on intent, not mechanics.
What ultimately matters is that the Actor gets a picture of a dog.
Screenplay interactions can compose low-level actions like REST API calls into reusable workflows.

**Web UI + REST API:**
Screenplay interactions can compose Web UI and REST API interactions together.
For example, a `Login` Task could authenticate a user via a REST API,
add the authentication token to a browser,
and refresh the browser to become logged in as that user.
{: .notice--info}

Run the refactored test to make sure it still passes.


### 10. Simplifying REST Syntax

Let's take a closer look at the Screenplay calls from the new `RandomDogImage` Question:

```csharp
actor.Calls(Rest<CallDogApi>.Request<DogResponse>(request))
actor.Calls(Rest<CallDogImagesApi>.Download(imageRequest))
```

These calls work, but the type generics on the `Rest` class look out of place.
Try to read the first line in plain English:
"The actor calls REST call Dog API request dog response request."
*What?* That doesn't make sense.
We know what these calls will do, but the syntax is not very fluent.

As stated previously, the `Rest` class is merely syntactic Screenplay sugar.
It provides a cleaner way to make REST requests using RestSharp.
`Rest` works great for the "default" `CallRestApi` Ability,
but it doesn't work well for custom RestSharp Abilities.

The simplest way to improve the syntax is to use C#'s
[using alias directives](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive),
also known as "type aliases".
Add the following lines to the top of `RandomDogImage.cs`, immediately beneath the `using` statements:

```csharp
using DogApi = Boa.Constrictor.RestSharp.Rest<Boa.Constrictor.Example.CallDogApi>;
using DogImagesApi = Boa.Constrictor.RestSharp.Rest<Boa.Constrictor.Example.CallDogImagesApi>;
```

Then, rewrite the Screenplay calls:

```csharp
// Old calls using the type generics
actor.Calls(Rest<CallDogApi>.Request<DogResponse>(request))
actor.Calls(Rest<CallDogImagesApi>.Download(imageRequest))

// New calls using the aliases
actor.Calls(DogApi.Request<DogResponse>(request))
actor.Calls(DogImagesApi.Download(imageRequest))
```

Read the new calls in plain English:

* "The actor calls Dog API to request a dog response from request."
* "The actor calls Dog Images API to download an image request."

These calls are much more readable now.

The full code for `RandomDogImage.cs` should now look like this:

```csharp
using Boa.Constrictor.Screenplay;
using RestSharp;
using System;
using System.IO.

using DogApi = Boa.Constrictor.RestSharp.Rest<Boa.Constrictor.Example.CallDogApi>;
using DogImagesApi = Boa.Constrictor.RestSharp.Rest<Boa.Constrictor.Example.CallDogImagesApi>;

namespace Boa.Constrictor.Example
{
    public class RandomDogImage : IQuestion<byte[]>
    {
        private RandomDogImage() { }

        public static RandomDogImage FromDogApi() =>
            new RandomDogImage();

        public byte[] RequestAs(IActor actor)
        {
            var request = DogRequests.GetRandomDog();
            var response = actor.Calls(DogApi.Request<DogResponse>(request));

            var resource = new Uri(response.Data.Message).AbsolutePath;
            var imageRequest = new RestRequest(resource);
            var extension = Path.GetExtension(resource);
            var imageData = actor.Calls(DogImagesApi.Download(imageRequest, extension));

            return imageData;
        }
    }
}
```

Rebuild and rerun the tests.
They should all pass.

**Aliases are Not Required:**
You do not *need* to create type aliases for RestSharp Abilities.
Type aliases are optional.
They simply improve code readability.
{: .notice--info}

**Limited Scope:**
Unfortunately, `using` aliases are local to the file in which they are declared.
They [cannot be given global scope](https://bytes.com/topic/c-sharp/answers/745345-using-alias-global-scope).
You will need to define aliases in each file that uses them.
{: .notice--warning}


### 11. Dumping Responses

So far in this tutorial, Boa Constrictor has handled responses in memory.
It can also dump requests and downloads to files so that testers can review them later.
Request dumps include the full request and response, field by field.
Download dumps are the actual files downloaded in response bodies, like PDFs or PNGs.
Dumps plainly show problems like error messages, bad status codes, and corrupted files.

**Dumping is Optional:**
Dumping requests and downloads is optional.
You do not need to dump files when using Boa Constrictor.
Dumping does not happen by default - you must explicitly enable it.
{: .notice--info}

The basic `CallRestApi` Ability provides extra builder methods to configure dumping.
Each file type must be given a destination directory path and a filename prefix.
The example call below shows how to configure the Ability for dumping:

```csharp
Actor.Can(
    CallRestApi.At("https://dog.ceo/")
        .DumpingRequestsTo("/path/to/dump/requests/", "DogApiRequest")
        .DumpingDownloadsTo("/path/to/dump/downloads/", "DogApiImage"))
```

With this configuration, the `Rest.Request` and `Request.Download` methods will automatically dump their responses to these directories.
Boa Constrictor will create these directories if they do not already exist.
The dumped files will be named `"<prefix>_<timestamp>.<extension>"`.
Request are dumped as JSON files, while downloads are dumped using their given file extension.
For example, a dog request dump file could be named `"DogApiRequest_202104061352038197.json"`.

**Dumping Naming Conventions:**
Automation can make several requests and downloads during one execution.
Boa Constrictor uses a prefix like `"DogApiRequest"` to categorize all responses from one Ability (meaning one base URL).
It uses timestamps to give a chronological sequence to dumped files.
{: .notice--info}

Custom Abilities like `CallDogApi` and `CallDogImagesApi` do not inherit these dumping methods.
They need to specify dumping on their own.
They can either copy `CallRestApi`'s builder methods for dumping, or they can customize how dumping is handled.

Let's update `CallDogApi` to automatically dump requests for Dog API.
Since Dog API does not provide the images to download, it does *not* need to be configured for dumping downloaded files.
Change `CallDogApi.cs` to the following code:

```csharp
using Boa.Constrictor.RestSharp;
using RestSharp;
using System.IO;

namespace Boa.Constrictor.Example
{
    public class CallDogApi : AbstractRestSharpAbility
    {
        public const string BaseUrl = "https://dog.ceo/";
        public const string RequestToken = "DogApiRequest";

        private CallDogApi(RestClient client, string dumpDir) :
            base(client)
        {
            RequestDumper = new RequestDumper(
                "Dog API Request Dumper",
                Path.Combine(dumpDir, RequestToken),
                RequestToken);
        }

        public static CallDogApi DumpingTo(string dumpDir) =>
            new CallDogApi(new RestClient(BaseUrl), dumpDir);
    }
}
```

Let's break down the changes:

| Change | Reason |
| ------ | ------ |
| `using System.IO;` | Required by the `Path` class. |
| `RequestToken` | A string constant for the request dump filename prefix. |
| `CallDogApi` constructor | The constructor now has a `dumpDir` argument for the path to the dump directory. |
| `RequestDumper` | The `AbstractRestSharpAbility` property for the request dumper. |
| `new RequestDumper(...)` | The class used for dumping requests. Needs a name, a dumping directory path, and a dump filename prefix. |
| `Path.Combine(dumpDir, RequestToken)` | Adds a subdirectory to the dumping directory for dumping Dog API requests. |
| `DumpingTo` | Builder method that replaces `UsingBaseUrl`. Takes in the dumping directory path. |

Let's make a similar update to `CallDogImagesApi`.
This Ability should dump both requests and downloads.
Change `CallDogImagesApi.cs` to the following code:

```csharp
using Boa.Constrictor.Dumping;
using Boa.Constrictor.RestSharp;
using RestSharp;
using System.IO;

namespace Boa.Constrictor.Example
{
    public class CallDogImagesApi : AbstractRestSharpAbility
    {
        public const string BaseUrl = "https://images.dog.ceo/";
        public const string DownloadToken = "DogImagesApiDownload";
        public const string RequestToken = "DogImagesApiRequest";

        private CallDogImagesApi(RestClient client, string dumpDir) :
            base(client)
        {
            RequestDumper = new RequestDumper(
                "Dog Images API Request Dumper",
                Path.Combine(dumpDir, RequestToken),
                RequestToken);
            DownloadDumper = new ByteDumper(
                "Dog Images API Download Dumper",
                Path.Combine(dumpDir, DownloadToken),
                DownloadToken);
        }

        public static CallDogImagesApi DumpingTo(string dumpDir) =>
            new CallDogImagesApi(new RestClient(BaseUrl), dumpDir);
    }
}
```

These changes are very similar to the ones made for `CallDogApi`,
but `CallDogImagesApi` sets `DownloadDumper` to a new `ByteDumper` object.
The dumping subdirectories are different to keep files organized.

To use the updated Abilities, update `ScreenplayRestApiAdvancedTest`.
Add the following import statement:

```csharp
using System.IO;
```

And edit the `[Setup]` method:

```csharp
[SetUp]
public void InitializeScreenplay()
{
    Actor = new Actor(name: "Andy", logger: new ConsoleLogger());
    AssemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    Actor.Can(CallDogApi.DumpingTo(AssemblyDir));
    Actor.Can(CallDogImagesApi.DumpingTo(AssemblyDir));
}
```

Both custom Abilities set the dumping directory to `AssemblyDir`,
which is the directory where the `Boa.Constrictor.Example` is located.
They can share the same dumping directory because each Ability adds subdirectories for different file types.

**Assembly Directory:**
The assembly directory is typically the build output directory.
It is a decent location for dumping files when running tests on a local machine.
However, you should consider using a better output path when running tests in a Continuous Integration system.
{: .notice--warning}

Rerun the tests, and make sure they all pass.
Then, check the assembly directory for the dumped files.
(The assembly file is most likely located at
`boa-constrictor\Boa.Constrictor.Example\bin\Debug\net5.0`.)
It should contain the following directories:

```
Assembly Directory
│
├── DogApiRequest
├── DogImagesApiDownload
└── DogImagesApiRequest
```

Request dumps can be large.
Below is a snippet from one of the dumps for a Dog API request:

```json
{
  "Duration": {
    "StartTime": "2021-04-06T16:54:25.0631404Z",
    "EndTime": "2021-04-06T16:54:33.5413263Z",
    "Duration": "00:00:08.4781859"
  },
  "Request": {
    "Method": "GET",
    "Uri": "https://dog.ceo/api/breeds/image/random",
    "Resource": "api/breeds/image/random",
    "Parameters": [],
    "Body": null
  },
  "Response": {
    "Uri": "https://dog.ceo/api/breeds/image/random",
    "StatusCode": 200,
    "ErrorMessage": null,
    "Content": "{\"message\":\"https:\\/\\/images.dog.ceo\\/breeds\\/brabancon\\/n02112706_2087.jpg\",\"status\":\"success\"}",
    "Headers": [
      {
        "Name": "Date",
        "Value": "Tue, 06 Apr 2021 16:54:32 GMT",
        "Type": "HttpHeader"
      },
      {
        "Name": "Transfer-Encoding",
        "Value": "chunked",
        "Type": "HttpHeader"
      },
      // ...
    ],
    // ...
  }
}
```

And below is an image of a dog downloaded from Dog Images API:

![Example Dog Image]({{ "/assets/images/example-dog-image.png" | relative_url }})

**File Storage:**
File dumps can fill up storage space on the file system.
Remember to delete old dumps or archive them from time to time.
{: .notice--warning}


## Conclusion

Congrats on finishing Part 3 of the tutorial!
The example project should now be structured like this:

```
Boa.Constrictor.Example
│
├── Abilities
│   ├── CallDogApi.cs
│   └── CallDogImagesApi.cs
│
├── Interactions
│   ├── RandomDogImage.cs
│   └── SearchDuckDuckGo.cs
│
├── Pages
│   ├── ResultPage.cs
│   └── SearchPage.cs
│
├── Requests
│   └── DogRequests.cs
│
├── Responses
│   └── DogResponse.cs
│
└── Tests
    ├── ScreenplayRestApiAdvancedTest.cs
    ├── ScreenplayRestApiBasicTest.cs
    └── ScreenplayWebUiTest.cs
```

Now that you have completed the tutorial,
you should be ready to use Boa Constrictor in your own projects!
