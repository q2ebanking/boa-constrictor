---
title: Calling multiple REST APIs
layout: single
permalink: /user-guides/calling-multiple-rest-apis/
sidebar:
  nav: "user-guides"
toc: true
---

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