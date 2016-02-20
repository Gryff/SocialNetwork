using System;
using System.Collections.Generic;
using SocialNetwork.Core;

namespace SocialNetwork
{
    class Program
    {
        private static readonly List<User> Users = new List<User>();

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my social network");
            Console.WriteLine("To post a message follow the format {name} -> {message}");

            while (true)
            {
                string userInput = Console.ReadLine();

                CommandType commandType = Commands.DetermineCommandType(userInput);

                switch (commandType)
                {
                    case CommandType.Post:
                        Commands.PostMessage(userInput, Users);
                        break;

                    case CommandType.Follow:
                        Commands.AddUserToFollowingForUser(userInput, Users);
                        break;

                    case CommandType.Timeline:
                        Console.WriteLine(Commands.GetFormattedTimeline(userInput, Users));
                        break;

                    case CommandType.Wall:
                        Console.WriteLine(Commands.GetWall(userInput, Users));
                        break;
                }
            }
        }
    }
}
