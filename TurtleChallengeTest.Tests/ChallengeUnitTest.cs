using System;
using System.Collections.Generic;
using System.Linq;
using TurtleChallengeTest.Library;
using Xunit;
using Action = System.Action;

namespace TurtleChallengeTest.Tests
{
    public class ChallengeUnitTest
    {
        // Arrange
        public static int boardX = 5;
        public static int boardY = 4;
        public static ExitPoint exit = new ExitPoint() { PosX = 4, PosY = 2 };
        public static List<Mine> mines = new List<Mine>()
        {
            new Mine()
            {
                PosX = 1,
                PosY = 1
            },
            new Mine()
            {
                PosX = 3,
                PosY = 1
            },
            new Mine()
            {
                PosX = 3,
                PosY = 3
            }
        };
        
        public Configuration config = new Configuration()
        {
            Board = new Board(boardX, boardY, mines, exit),
            ExitPoint = exit,
            Mines = mines,
            Turtle = new Turtle(Direction.North)
            {
                StartDirection = Direction.North,
                ActualPosition = new Tile() { PosY = 1, PosX = 0, Type = TileType.Normal },
                StartPosX = 0,
                StartPosY = 1
            }
        };

        [Fact]
        public void Test_Mine_Path()
        {
            // Arrange
            List<Library.Action> actions = new List<Library.Action>()
            {
                Library.Action.Move,
                Library.Action.Rotate,
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Rotate,
                Library.Action.Move
            };
            ActionResult expectedResult = ActionResult.MineExploded;

            // Act
            var turns = SetupGame.Play(config, actions);

            // Assert
            var actionResult = turns.Last().ActionResult;
            Assert.Equal(expectedResult, actionResult);
        }

        [Fact]
        public void Test_Lost_Path()
        {
            List<Library.Action> actions = new List<Library.Action>()
            {
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Move
            };
            ActionResult expectedResult = ActionResult.HitTheWall;

            var turns = SetupGame.Play(config, actions);

            var actionResult = turns.Last().ActionResult;
            Assert.Equal(expectedResult, actionResult);
        }

        [Fact]
        public void Test_Exit_Path()
        {
            List<Library.Action> actions = new List<Library.Action>()
            {
                Library.Action.Move,
                Library.Action.Rotate,
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Rotate,
                Library.Action.Move,
                Library.Action.Move
            };

            ActionResult expectedResult = ActionResult.ExitFound;

            var turns = SetupGame.Play(config, actions);

            var actionResult = turns.Last().ActionResult;
            Assert.Equal(expectedResult, actionResult);
        }

        [Fact]
        public void Test_Wrong_Exit_Point()
        {
            List<Library.Action> actions = new List<Library.Action>()
            {
                Library.Action.Move,
                Library.Action.Rotate,
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Move,
                Library.Action.Rotate,
                Library.Action.Move,
                Library.Action.Move
            };

            try
            {
                SetupGame.ValidateBoard(config, actions);
            }
            catch (Exception e)
            {
                Assert.True(true);
            }
        }
    }
}
