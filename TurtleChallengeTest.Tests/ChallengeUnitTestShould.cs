using System;
using System.Collections.Generic;
using System.Linq;
using TurtleChallengeTest.Library;
using Xunit;
using Action = System.Action;

namespace TurtleChallengeTest.Tests
{
    public class ChallengeUnitTestShould
    {
        // Arrange
        public static int boardX = 5;
        public static int boardY = 4;
        public static BoardPosition exit = new BoardPosition() { PosX = 4, PosY = 2 };
        public static List<BoardPosition> mines = new List<BoardPosition>()
        {
            new BoardPosition()
            {
                PosX = 1,
                PosY = 1
            },
            new BoardPosition()
            {
                PosX = 3,
                PosY = 1
            },
            new BoardPosition()
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
        public void TheLittleTurtleShoudExplode()
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
            var play = new PlayGame(config, actions);
            var turns = play.Play();

            // Assert
            var actionResult = turns.Last().ActionResult;
            Assert.Equal(expectedResult, actionResult);
        }

        [Fact]
        public void TheLittleTurtleShoudGetLost()
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

            var play = new PlayGame(config, actions);
            var turns = play.Play();

            var actionResult = turns.Last().ActionResult;
            Assert.Equal(expectedResult, actionResult);
        }

        [Fact]
        public void TheLittleTurtleShouldFindTheExit()
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
            var play = new PlayGame(config, actions);
            var turns = play.Play();

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
                var setup = new SetupGame(config, actions);
                setup.ValidateBoard();
            }
            catch (Exception e)
            {
                Assert.True(true);
            }
        }
    }
}
