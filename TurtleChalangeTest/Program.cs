using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using TurtleChallengeTest.Library;
using Action = TurtleChallengeTest.Library.Action;

namespace TurtleChallengeTest.View
{
    public class Program
    {
        const string ConfigFile = "game-settings";
        const string ActionsFile = "moves";

        public static Configuration Conf = new Configuration();
        public static List<Action> Actions = new List<Action>();
        
        static void Main(string[] args)
        {
            ReadFiles(ref Conf,ref Actions);
            
            try
            {
                Conf.Board = SetupGame.ValidateBoard(Conf, Actions);
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

        static void ReadFiles(ref Configuration configuration, ref List<Action> actions)
        {
            try
            {
                using (StreamReader r = new StreamReader(ConfigFile + ".json"))
                {
                    var json = r.ReadToEnd();
                    configuration = JsonConvert.DeserializeObject<Configuration>(json);
                }

                using (StreamReader r = new StreamReader(ActionsFile + ".json"))
                {
                    var json = r.ReadToEnd();
                    actions = JsonConvert.DeserializeObject<List<Action>>(json);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        static void PlayGame()
        {

            var turns = SetupGame.Play(Conf, Actions);
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
