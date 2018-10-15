using System;
using System.Collections.Generic;
using System.Text;

namespace TurtleChallengeTest.Library
{
    public class PlayGame
    {
        private Configuration _conf;
        private List<Action> _act;

        public int SizeX;
        public int SizeY;
        public int ExitPosX;
        public int ExitPosY;
        public int StartPosX;
        public int StartPosY;

        public PlayGame(Configuration cfg, List<Action> act)
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
        
        public List<Turn> Play()
        {
            var gameover = false;
            var error = false;

            var outputAction = "";

            var turns = new List<Turn>();

            foreach (var action in _act)
            {
                var turn = new Turn();

                var outputMessage = "Success: The little is safe and keep trying";

                turn.Action = action;

                switch (action)
                {
                    case Action.Move:
                        _conf.Turtle.Move();
                        outputAction = ($"[{ _conf.Turtle.ActualPosition.PosX }-{ _conf.Turtle.ActualPosition.PosY }] - ");
                        break;

                    case Action.Rotate:
                        _conf.Turtle.Rotate();
                        outputAction = ($"{ _conf.Turtle.Direction.ToString() } - ");
                        break;
                }


                if (_conf.Turtle.ActualPosition.PosX < 0)
                {
                    _conf.Turtle.ActualPosition.PosX = 0;
                    outputMessage = "Fail: The little turtle hit at the wall!";
                    error = true;
                }
                if (_conf.Turtle.ActualPosition.PosY < 0)
                {
                    _conf.Turtle.ActualPosition.PosY = 0;
                    outputMessage = "Fail: The little turtle hit at the wall!";
                    error = true;
                }
                if (_conf.Turtle.ActualPosition.PosY > SizeY)
                {
                    _conf.Turtle.ActualPosition.PosY = SizeY;
                    outputMessage = "Fail: The little turtle hit at the wall!";
                    error = true;
                }
                if (_conf.Turtle.ActualPosition.PosX > SizeX)
                {
                    _conf.Turtle.ActualPosition.PosX = SizeX;
                    outputMessage = "Fail: The little turtle hit at the wall!";
                    error = true;
                }

                if (_conf.Board.BoardTiles[_conf.Turtle.ActualPosition.PosX, _conf.Turtle.ActualPosition.PosY].Type ==
                    TileType.Mine)
                {
                    outputMessage = "Fatal Fail: The little turtle hit a mine!";
                    gameover = true;
                    turn.ActionResult = ActionResult.MineExploded;
                }

                if (_conf.Board.BoardTiles[_conf.Turtle.ActualPosition.PosX, _conf.Turtle.ActualPosition.PosY].Type ==
                    TileType.Exit)
                {
                    outputMessage = "Success: The little turtle has finally found the exit";
                    error = false;
                    gameover = true;
                    turn.ActionResult = ActionResult.ExitFound;
                }

                turn.Result = $"{outputAction} {outputMessage}";

                turn.Hit = error;
                turn.GameOver = gameover;
                turns.Add(turn);

                if (error)
                {
                    turn.ActionResult = ActionResult.HitTheWall;
                }

                if (gameover)
                {
                    break;
                }
            }
            return turns;
        }
    }
}
