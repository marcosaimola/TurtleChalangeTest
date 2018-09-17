using System;
using System.Collections.Generic;
using System.Text;

namespace TurtleChallengeTest.Library
{
    public interface ITurtle
    {
        void Move();
        void Rotate();
        Tile GetActualPosition();
    }
}
