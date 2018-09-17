# TurtleChalangeTest
This code was written using C# .Net Core as a part of a job application in a company that I really like to work for.

## The solution has three projects:  
- **TurtleChalangeTest.Tests** - Where unit tests were written to validate the result of actions
- **TurtleChalangeTest.Library** - Where are the classed template and business rules for the game
- **TurtleChalangeTest** - console application where game outputs are displayed
### Dependencies ###
Only the **Newtonsoft.Json** package was used and should be downloaded in the project library.

``` Install-Package Newtonsoft.Json -Version 11.0.2 ```

## Configuration Files ##
Two diferent files are used to setup the game:
### game-settings.json ###
```C#
{
  "Board": {
    "sizeX": 5,
    "sizeY": 4
  },

  "Turtle": {
    "startPosX": 0,
    "startPosY": 1,
    "startDirection": "north"
  },

  "ExitPoint": {
    "posX": 4,
    "posY": 2
  },

  "Mines": 
    [
      {
        "posX": 1,
        "posY": 1
      },
      {
        "posX": 3,
        "posY": 1
      },
      {
        "posX": 3,
        "posY": 3
      }
    ]
}

```
- **Board** - Must be defined the sizes X and Y
- **Turtle** - Must be defined the startup X and Y position
- **ExitPoint** - Must be defined the X and Y position of the exit tile
- **Mines** - Must be defined the amount of mine (at least one is required)

### move.json ###
It contains the turtle actions sequence
```Json
[
  "move",
  "rotate",
  "move",
  "move",
  "move",
  "move",
  "rotate",
  "move",
  "move"
]
```
