using System.Collections.Generic;

namespace TurtleChallengeTest.Library
{
    public class Configuration
    {
        public Board Board { get; set; }
        public Turtle Turtle { get; set; }
        public BoardPosition ExitPoint { get; set; }
        public List<BoardPosition> Mines { get; set; }
    }
}
