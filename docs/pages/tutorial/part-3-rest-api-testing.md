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

(Coming soon!)


### 4. Calling Basic Requests

(Coming soon!)


### 5. Parsing Response Bodies

(Coming soon!)


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
