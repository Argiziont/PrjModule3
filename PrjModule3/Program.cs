using CommandResolver;
using CommandResolver.Exceptions;
using CommandResolver.Helpers;
using CommandResolver.Interfaces;
using System;
using System.Collections.Generic;

namespace PrjModule3
{
    class Program
    {
        static void Main()
        {

            #region Dot in console
            //Dot in console
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            //
            #endregion

            while (true)
            {
                Console.Write("Write");
                ConsoleWithColor(" EXIT ", ConsoleColor.Red);
                Console.Write("to end the script\n");

                ConsoleWithColor("\nYour script here ---------------\n\n", ConsoleColor.Red);

                Menu();

                Console.WriteLine("Press 'e' for close programm or 'enter' for continue");

                ConsoleKeyInfo exitState = Console.ReadKey();
                switch (exitState.Key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        continue;
                    case ConsoleKey.E:
                        return; ;
                    default:
                        return;
                }
            }
        }
        private static void Menu()
        {
            MutableKeyValuePair<string, object> MutablePairs = new MutableKeyValuePair<string, object>();
            Stack<MutableKeyValuePair<string, object>> MainStack = new Stack<MutableKeyValuePair<string, object>>();

            CommandFactory commandFactory = new CommandFactory(ref MutablePairs, ref MainStack);

            List<ICommand> commands = new List<ICommand>();

            while (true)
            {
                string inputCommand = Console.ReadLine();

                if (inputCommand == "EXIT")
                    break;

                if (inputCommand.Contains("#"))
                {
                    inputCommand = inputCommand.Split("#")[0];
                    inputCommand=inputCommand.Trim();
                    if (inputCommand == "")
                        continue;
                }

                try
                {
                    commands.Add(commandFactory.GetCommand(inputCommand));
                }
                catch (CommandFactoryException ex)
                {
                    ConsoleWithColor("\n" + ex.Message + "\n", ConsoleColor.Red);
                }
            }

            ConsoleWithColor("\nYour script  results here ---------------\n\n", ConsoleColor.Red);
            for (int i = 0; i < commands.Count; i++)
            {
                try
                {
                    commands[i].Run();
                }
                catch (CommandExecutionException ex)
                {
                    ConsoleWithColor($"\n!!! Error on line {i + 1} with error: {ex.Message}\n\n", ConsoleColor.Red);
                }
            }
        }

        private static void ConsoleWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
