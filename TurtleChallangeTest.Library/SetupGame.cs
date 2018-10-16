using System;
using System.Collections.Generic;
using System.Linq;

namespace TurtleChallengeTest.Library
{
    public class SetupGame
    {
        private Configuration _conf;
        private List<Action> _act;

        public int SizeX;
        public int SizeY;
        public int ExitPosX;
        public int ExitPosY;
        public int StartPosX;
        public int StartPosY;

        public SetupGame(Configuration cfg, List<Action> act)
        {
            _conf = cfg;
            _act = act;

            SizeX = _conf.Board.SizeX - 1;
            SizeY = _conf.Board.SizeY - 1;
            ExitPosX = _conf.ExitPoint.PosX;
            ExitPosY = _conf.ExitPoint.PosY;
            StartPosX = _conf.Turtle.StartPosX;
            StartPosY = _conf.Turtle.StartPosY;
        }
        
        public Board ValidateBoard()
        {
        
            if (SizeX < 0 || SizeY < 0)
            {
                throw new Exception($"Invalid board size: X = {SizeX}, Y = {SizeY}");
            }

            if (ExitPosX < 0 || ExitPosX > SizeX || ExitPosY < 0 || ExitPosY > SizeY)
            {
                throw new Exception($"Invalid Exit position: X = {ExitPosX}, Y = {ExitPosY}");
            }

            if (StartPosX < 0 || StartPosX > SizeX || StartPosY < 0 || StartPosY > SizeY)
            {
                throw new Exception($"Invalid Turtle startup position: X = {StartPosX}, Y = {StartPosY}");
            }

            foreach (var mine in _conf.Mines)
            {
                if (mine.PosX < 0 || mine.PosX > SizeX || mine.PosY < 0 || mine.PosY > SizeY)
                {
                    throw new Exception($"Invalid mine position: X = {mine.PosX}, Y = {mine.PosY}");
                }
            }

            var invalidInitialPosition = _conf.Mines
                .FirstOrDefault(x => x.PosX == StartPosX && x.PosY == StartPosY);

            if (invalidInitialPosition != null)
            {
                throw new Exception($"It is not possible to set the turtle start position in a mine tile: X = {invalidInitialPosition.PosX}, Y = {invalidInitialPosition.PosY}");
            }


            if (_act == null || _act.Count == 0)
            {
                    throw new Exception($"There are no actions informed, the poor turtle is going to dead stopped.");
            }

            if (_conf.Turtle.ActualPosition == null)
            {
                _conf.Turtle.ActualPosition = new Tile()
                {
                    PosX = StartPosX,
                    PosY = StartPosY,
                    Type = TileType.Normal
                };
            }

            return new Board(SizeX+1, SizeY+1, _conf.Mines, _conf.ExitPoint);
        }

    }
    
}
