using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class Utilities
    {
        public static string getCommand(string Message)
        {
            var words = Message.Split(" ");

            words = RemoveWhiteSpacesIn(words);

            words = RemoveWordsThatAreNotCommands(words);

            if (words.Count() > 0) return words.First();

            return null;
        }

        public static string[] getCommands(string Message)
        {
            var words = Message.Split(" ");

            words = RemoveWhiteSpacesIn(words);

            var commands = RemoveWordsThatAreNotCommands(words);

            if (commands.Count() > 0) return commands;

            return null;
        }

        private static string[] RemoveWordsThatAreNotCommands(string[] words)
        {
            return Enumerable.ToArray(words.Where(x => x.Contains("/stock=")));
        }

        private static string[] RemoveWhiteSpacesIn(string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = new string(words[i].ToArray()
                    .Where(x => !Char.IsWhiteSpace(x))
                    .ToArray());
            }

            return words;
        }
    }
}
