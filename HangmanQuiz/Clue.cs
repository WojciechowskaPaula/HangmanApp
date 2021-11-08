using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanQuiz.Helpers;


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
            string hiddenWord = "";
            for (int i = 0; i < word.Length; i++)
            {
                hiddenWord = hiddenWord + Characters.Underscore + Characters.WhiteSpace;
            }
            Console.WriteLine(hiddenWord);
            GuessClue(word,hiddenWord);
            return hiddenWord;
        }
        public bool CheckUserAnswer(string userResponse)
        {
            bool result = false;
            userResponse = userResponse.ToLower();
            char[] userResponseArr = userResponse.ToCharArray();
            if (userResponseArr.Length >= 2)
            {
                result = false;
                Console.WriteLine("You enter to many signs, please try again.");
            }
            else if (String.IsNullOrEmpty(userResponse))
            {
                result = false;
                Console.WriteLine("You didn't give an answer, Please try again.");
            }
            else if (!Char.IsLetter(userResponseArr[0]))
            {
                result = true;
                Console.WriteLine("Incorrect answer, please enter a letter.");
            }
            return result;
        }
        public string GuessClue(string word, string hiddenWord)
        {
            int numberOfTrialsLimit = word.Length + 2;
            int numberOfTrials = 0;
            word = string.Join(" ", word.ToCharArray());
            while (hiddenWord.Contains(Characters.Underscore))
            {
                string userResponse = "";
                Console.WriteLine("Enter only one letter:");
                userResponse = Console.ReadLine();
                CheckUserAnswer(userResponse);
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i].ToString() == userResponse)
                    {
                        hiddenWord = hiddenWord.Remove(i, 1).Insert(i, userResponse);
                    }
                }
                if (!word.Contains(userResponse))
                {
                    numberOfTrials++;
                    Console.WriteLine($"Incorrect answer, You have {numberOfTrialsLimit - numberOfTrials} trails left");
                }
                if (numberOfTrials >= numberOfTrialsLimit)
                {
                    Console.WriteLine("Game over!");
                    break;
                }
                Console.WriteLine(hiddenWord);
            }
            return hiddenWord;
        }
    }
}
