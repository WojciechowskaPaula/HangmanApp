using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanQuiz
{
    public class Clue
    {
        private List<string> GetClueList()
        {
            string directoryPath = @"C:\Users\paull\source\repos\HangmanQuiz\HangmanQuiz\Clues";
            string filePath = @$"{directoryPath}\Clues.txt";
            bool directoryExist = Directory.Exists(directoryPath);
            if (!directoryExist)
            {
                Directory.CreateDirectory(directoryPath);
                using (FileStream file = File.Open(filePath, FileMode.CreateNew, FileAccess.ReadWrite)) 
                {
                    using (StreamWriter streamWritter = new StreamWriter(file))
                    {
                        streamWritter.WriteLine("keyboard");
                        streamWritter.WriteLine("mouse");
                        streamWritter.WriteLine("network");
                        streamWritter.WriteLine("browser");
                        streamWritter.WriteLine("server");
                    }
                }
            }
            var clues = File.ReadLines(filePath);
            return clues.ToList();
        }
        public string GetRandomClue() //losowanie
        {
            List<string> clues = GetClueList();
            string word = clues.OrderBy(x => Guid.NewGuid()).First();
            return word;
        }

        public string DisplayClue()
        {
            string word = GetRandomClue();
            Console.WriteLine($"Your clue consist of {word.Length} letters");
            char underscore = '_';
            char whiteSpace = ' ';
            string hiddenWord = "";
            
            for (int i = 0; i < word.Length; i++)
            {
                hiddenWord = hiddenWord + underscore + whiteSpace;
            }
            word = string.Join(" ", word.ToCharArray());
            Console.WriteLine(hiddenWord);
            
            while (hiddenWord.Contains(underscore))
            {
                Console.WriteLine("Enter only one letter:");
                string userResponse = Console.ReadLine();
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i].ToString() == userResponse)
                    {
                        hiddenWord = hiddenWord.Remove(i, 1).Insert(i, userResponse);
                    }
                }
                Console.WriteLine(hiddenWord);
            }
            return hiddenWord;

        }
    }
}
