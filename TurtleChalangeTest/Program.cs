using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TurtleChallengeTest.Library;
using Action = TurtleChallengeTest.Library.Action;

namespace TurtleChallengeTest.View
{
    public class Program
    {
        public static Library.Configuration Conf = new Library.Configuration();
        public static List<Action> Actions = new List<Action>();
        public static SetupGame setup;
        public static PlayGame play;

        static void Main(string[] args)
        {
            ReadFiles();
            
            try
            {
                setup = new SetupGame(Conf,Actions);
                play = new PlayGame(Conf, Actions);

                Conf.Board = setup.ValidateBoard();
                PlayGame();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                Console.ReadKey();
            }  
        }

        static void ReadFiles()
        {

            string ConfigFile = "game-settings.json";
            string ActionsFile = "moves.json";

            try
            {
                using (StreamReader r = new StreamReader(ConfigFile))
                {
                    var json = r.ReadToEnd();
                    Conf = JsonConvert.DeserializeObject<Library.Configuration>(json);
                }

                using (StreamReader r = new StreamReader(ActionsFile))
                {
                    var json = r.ReadToEnd();
                    Actions = JsonConvert.DeserializeObject<List<Action>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void PlayGame()
        {

            var turns = play.Play();
            var error = false;

            var i = 0;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"Turtle Start Position X: {Conf.Turtle.ActualPosition.PosX} - Y: {Conf.Turtle.ActualPosition.PosY}");
            Console.WriteLine($" | Start Direction: {Conf.Turtle.StartDirection.ToString()}");
            Console.WriteLine("***********************************************************");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Action result: ");

            foreach (var turn in turns)
            {
                i++;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(turn.Action + " ");
                Console.ResetColor();
                Console.WriteLine(turn.Result);

                error = turn.Hit;
            }
            
            if (error)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Fatal Fail: The little turtle is going to be lost forever.");
            }

            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
