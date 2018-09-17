using System;
using System.Collections.Generic;
using System.Text;

namespace TurtleChallengeTest.Library
{
    public class Turtle : ITurtle
    {
        public Direction Direction;

        public Turtle(Direction direction)
        {
            Direction = direction;
        }

        public int StartPosX { get; set; }
        public int StartPosY { get; set; }
        public Direction StartDirection { get; set; }
        public Tile ActualPosition { get; set; }

        public void Move()
        {
            switch (Direction)
            {
                case Direction.North:
                        ActualPosition.PosY--;
                    break;

                case Direction.South:
                        ActualPosition.PosY++;
                    break;

                case Direction.East:
                        ActualPosition.PosX++;
                    break;

                case Direction.West:
                        ActualPosition.PosX--;
                    break;
            }
        }

        public void Rotate()
        {
            switch (Direction)
            {
                case Direction.North:
                    Direction = Direction.East;
                    break;

                case Direction.South:
                    Direction = Direction.West;
                    break;

                case Direction.East:
                    Direction = Direction.South;
                    break;

                case Direction.West:
                    Direction = Direction.North;
                    break;
            }
        }

        public Tile GetActualPosition()
        {
            return ActualPosition;
        }
    }
}
