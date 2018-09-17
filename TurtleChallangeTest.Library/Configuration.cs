using System;
using System.Collections.Generic;
using System.Text;

namespace TurtleChallengeTest.Library
{
    public class Configuration
    {
        public Board Board { get; set; }
        public Turtle Turtle { get; set; }
        public ExitPoint ExitPoint { get; set; }
        public List<Mine> Mines { get; set; }
    }
}
