---
title: Calling Tasks Safely
layout: single
permalink: /user-guides/calling-tasks-safely/
sidebar:
  nav: "user-guides"
toc: true
---

The `Boa.Constrictor.Safety` namespace contains several methods for calling Tasks safely using the Screenplay Pattern.
"Safe" actions are actions that should be executed despite any exceptions.
If an action causes an exception, then the exception should be caught and stored for the future.
This allows all actions to be attempted before aborting.
This is useful for when failed interactions are expected or not critical for the test to succeed.


## Adding the SafeActions Ability

In order to call Tasks safely, you must first add the Ability to run safe actions to the Actor.
You can optionally add an exception handler here.
Below is an example of a `SafeActions` Ability that logs exceptions to the console.

```csharp
using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Safety;

var actor = new Actor(logger: new ConsoleLogger());
var safeActions = new SafeActions((ex) =>
{
    actor.Logger.Log(ex.Message, LogSeverity.Error);
});

actor.Can(RunSafeActions.Using(safeActions));
```


## Running a Task Safely

Now that the Actor has the Ability to run safe actions,
you can attempt to call a Task, wrapping the call in the `Safely.Run` method:

```csharp
actor.AttemptsTo(Safely.Run(UnsafeTask.ThatThrows()));
```

If `UnsafeTask.ThatThrows` throws an exception,
it will be caught by `Safely.Run`,
call the exception handler,
and the exception will be added to the list of failures in `SafeActions`.


## Handling Failures

In addition to handling exceptions as they occur,
you can also throw an aggregate exception containing all exceptions that were caught by `Safely.Run`.
The aggregate exception message concatenates the messages of all exceptions it contains.
It also has a `Failures` property that can be used to iterate over the individual exceptions.
The following example logs the concatenated exception messages:

```csharp
try
{
    safeActions.ThrowAnyFailures();
}
catch (SafeActionsException ex)
{
    // Log the concatenated exception messages
    actor.Logger.Log(ex.Message, LogSeverity.Error);
    foreach (Exception innerException in ex.Failures)
    {
        // You could also handle exceptions individually here
    }
}
```
