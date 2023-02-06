# Bowling Game 

[![ci](https://github.com/larsmichael/BowlingGame/actions/workflows/ci.yml/badge.svg)](https://github.com/larsmichael/BowlingGame/actions/workflows/ci.yml)
[![Coverage Status](https://coveralls.io/repos/github/larsmichael/BowlingGame/badge.svg?branch=master)](https://coveralls.io/github/larsmichael/BowlingGame?branch=master)

This is a C# implementation of a domain model for a bowling game scoreboard.

## The `Game` class
The `Game` class serves as the API of the system. This class has the following methods:

```csharp
void Roll(int pins);
int GetScore();
Frame GetFrame(int frameNo);
```

The `Roll` method is used to register the number of pins knocked down in a roll.

At any time, the current game score can be retrieved using the `GetScore` method.

At any time, the status for each of the 10 frames can be retieved using the `GetFrame` method which returns a [Frame](#the-frame-class) object.

When a new `Game` class is created, the `Status` property is set to `OnGoing`. When the last ball is rolled, the status is set to `Finished`.

Once a game is finished, an exception will be thrown if the `Roll` method is called.

## The `Frame` class
A `Frame` object holds information about the number of pins knocked down in each of the rolls in a frame (the `First`, `Second` and `Last` properties).

As soon as it is ready, it also holds information about the score of the frame in the `Score` property.

The methods:

```csharp
bool IsStrike();
bool IsSpare();
```

tell whether the frame is registered as a strike or a spare.

The solution is inspired by (but more advanced than) the [Bowling Game Kata](http://butunclebob.com/ArticleS.UncleBob.TheBowlingGameKata) by Uncle Bob Martin.
