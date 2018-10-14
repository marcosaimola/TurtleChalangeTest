using System;
using System.Collections.Generic;
using System.Linq;

namespace TurtleChallengeTest.Library
{
    public class SetupGame
    {
        public static Board ValidateBoard(Configuration cfg, List<Action> act)
        {
            if (cfg.Board.SizeX < 0 || cfg.Board.SizeY < 0)
            {
                throw new Exception($"Invalid board size: X = {cfg.Board.SizeX}, Y = {cfg.Board.SizeY}");
            }

            if (cfg.ExitPoint.PosX < 0 || cfg.ExitPoint.PosX > cfg.Board.SizeX - 1 || cfg.ExitPoint.PosY < 0 || cfg.ExitPoint.PosY > cfg.Board.SizeY - 1)
            {
                throw new Exception($"Invalid Exit position: X = {cfg.ExitPoint.PosX}, Y = {cfg.ExitPoint.PosY}");
            }

            if (cfg.Turtle.StartPosX < 0 || cfg.Turtle.StartPosX > cfg.Board.SizeX - 1 || cfg.Turtle.StartPosY < 0 || cfg.Turtle.StartPosY > cfg.Board.SizeY - 1)
            {
                throw new Exception($"Invalid Turtle startup position: X = {cfg.Turtle.StartPosX}, Y = {cfg.Turtle.StartPosY}");
            }

            foreach (var mine in cfg.Mines)
            {
                if (mine.PosX < 0 || mine.PosX > cfg.Board.SizeX-1 || mine.PosY < 0 || mine.PosY > cfg.Board.SizeY - 1)
                {
                    throw new Exception($"Invalid mine position: X = {mine.PosX}, Y = {mine.PosY}");
                }
            }

            var invalidInitialPosition = cfg.Mines
                .FirstOrDefault(x => x.PosX == cfg.Turtle.StartPosX && x.PosY == cfg.Turtle.StartPosY);

            if (invalidInitialPosition != null)
            {
                throw new Exception($"It is not possible to set the turtle start position in a mine tile: X = {invalidInitialPosition.PosX}, Y = {invalidInitialPosition.PosY}");
            }


            if (act == null || act.Count == 0)
            {
                    throw new Exception($"There are no actions informed, the poor turtle is going to dead stopped.");
            }

            if (cfg.Turtle.ActualPosition == null)
            {
                cfg.Turtle.ActualPosition = new Tile()
                {
                    PosX = cfg.Turtle.StartPosX,
                    PosY = cfg.Turtle.StartPosY,
                    Type = TileType.Normal
                };
            }

            return new Board(cfg.Board.SizeX, cfg.Board.SizeY, cfg.Mines, cfg.ExitPoint);
        }

        public static List<Turn> Play(Configuration Conf, List<Action> Actions)
        {
            var gameover = false;
            var error = false;

            var outputAction = "";

            var turns = new List<Turn>();
            
            foreach (var action in Actions)
            {
                var turn = new Turn();

                var outputMessage = "Success: The little is safe and keep trying";

                turn.Action = action;
                
                switch (action)
                {
                    case Action.Move:
                        Conf.Turtle.Move();
                        outputAction = ($"[{ Conf.Turtle.ActualPosition.PosX }-{ Conf.Turtle.ActualPosition.PosY }] - ");
                        break;
                    case Action.Rotate:
                        Conf.Turtle.Rotate();
                        outputAction = ($"{ Conf.Turtle.Direction.ToString() } - ");
                        break;
                }


                if (Conf.Turtle.ActualPosition.PosX < 0)
                {
                    Conf.Turtle.ActualPosition.PosX = 0;
                    outputMessage = "Fail: The little turtle hit at the wall!";
                    error = true;
                }
                if (Conf.Turtle.ActualPosition.PosY < 0)
                {
                    Conf.Turtle.ActualPosition.PosY = 0;
                    outputMessage = "Fail: The little turtle hit at the wall!";
                    error = true;
                }
                if (Conf.Turtle.ActualPosition.PosY > Conf.Board.SizeY - 1)
                {
                    Conf.Turtle.ActualPosition.PosY = Conf.Board.SizeY - 1;
                    outputMessage = "Fail: The little turtle hit at the wall!";
                    error = true;
                }
                if (Conf.Turtle.ActualPosition.PosX > Conf.Board.SizeX - 1)
                {
                    Conf.Turtle.ActualPosition.PosX = Conf.Board.SizeX - 1;
                    outputMessage = "Fail: The little turtle hit at the wall!";
                    error = true;
                }

                if (Conf.Board.BoardTiles[Conf.Turtle.ActualPosition.PosX, Conf.Turtle.ActualPosition.PosY].Type ==
                    TileType.Mine)
                {
                    outputMessage = "Fatal Fail: The little turtle hit a mine!";
                    gameover = true;
                    turn.ActionResult = ActionResult.MineExploded;
                }

                if (Conf.Board.BoardTiles[Conf.Turtle.ActualPosition.PosX, Conf.Turtle.ActualPosition.PosY].Type ==
                    TileType.Exit)
                {
                    outputMessage = "Success: The little turtle has finally found the exit";
                    error = false;
                    gameover = true;
                    turn.ActionResult = ActionResult.ExitFound;
                }

                turn.Result = string.Format("{0} {1}",outputAction, outputMessage);

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
