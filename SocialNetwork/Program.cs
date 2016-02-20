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

                if (userInput.Contains("->"))
                    Commands.PostMessage(userInput, Users);

                else if (userInput.Contains("follows"))
                    Commands.AddUserToFollowingForUser(userInput, Users);

                else if (userInput.Contains("wall"))
                    Console.WriteLine(Commands.GetWall(userInput, Users));

                else if (!userInput.Contains(" "))
                    Console.WriteLine(Commands.GetFormattedTimeline(userInput, Users));
            }

        }
    }
}
