using System;
using System.Collections.Generic;
using System.Text;

namespace TurtleChallengeTest.Library
{
    public sealed class Tile
    {
        public int PosY { get; set; }
        public int PosX { get; set; }
        public TileType Type { get; set; }
    }
}
