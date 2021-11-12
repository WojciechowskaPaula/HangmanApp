using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HangmanQuiz.Helpers;


namespace HangmanQuiz
{
    public class Clue
    {
        public string DisplayClue(CategoryName category)
        {
            string word = GetRandomClue(category);
            Console.Clear();
            Console.WriteLine($"Your clue consist of {word.Length} letters");
            string hiddenWord = "";
            for (int i = 0; i < word.Length; i++)
            {
                hiddenWord = hiddenWord + Characters.Underscore + Characters.WhiteSpace;
            }
            Console.WriteLine(hiddenWord);
            GuessClue(word, hiddenWord, category);
            return hiddenWord;
        }

        private List<string> GetClueList(CategoryName categoryName)
        {
            bool directoryExist = Directory.Exists(Helpers.Path.directoryPath);
            if (!directoryExist)
            {
                Directory.CreateDirectory(Helpers.Path.directoryPath);
                using (FileStream streamFile = File.Open(Helpers.Path.iTPath, FileMode.CreateNew, FileAccess.ReadWrite))
                { 
                    using (StreamWriter streamWriterIt = new StreamWriter(streamFile))
                    {
                        streamWriterIt.WriteLine("keyboard");
                        streamWriterIt.WriteLine("mouse");
                        streamWriterIt.WriteLine("network");
                        streamWriterIt.WriteLine("browser");
                        streamWriterIt.WriteLine("server");
                        streamWriterIt.WriteLine("cookies");
                    }
                }
                using (FileStream streamFile = File.Open(Helpers.Path.countriesPath, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    using (StreamWriter streamWriterCountries = new StreamWriter(streamFile))
                    {
                        streamWriterCountries.WriteLine("denmark");
                        streamWriterCountries.WriteLine("norway");
                        streamWriterCountries.WriteLine("finland");
                        streamWriterCountries.WriteLine("switzerland");
                        streamWriterCountries.WriteLine("germany");
                        streamWriterCountries.WriteLine("russia");
                    }
                }
                using (FileStream streamFile = File.Open(Helpers.Path.foodPath, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    using (StreamWriter streamWriterFood = new StreamWriter(streamFile))
                    {
                        streamWriterFood.WriteLine("watermelon");
                        streamWriterFood.WriteLine("chicken");
                        streamWriterFood.WriteLine("salmon");
                        streamWriterFood.WriteLine("salad");
                        streamWriterFood.WriteLine("yogurt");
                        streamWriterFood.WriteLine("cucumber");
                    }
                }
            }
            if(categoryName == CategoryName.IT)
            {
                List<string> iTClues = File.ReadLines(Helpers.Path.iTPath).ToList();
                return iTClues;
            }
            else if(categoryName == CategoryName.Countries)
            {
                List<string> countryClues = File.ReadLines(Helpers.Path.countriesPath).ToList();
                return countryClues;
            }
            else
            {
                List<string> foodClues = File.ReadLines(Helpers.Path.foodPath).ToList();
                return foodClues;
            }
        }

        private string GetRandomClue(CategoryName categoryName)
        {
            List<string> clues = GetClueList(categoryName);
            string word = clues.OrderBy(x => Guid.NewGuid()).First();
            return word;
        }

        private bool CheckUserAnswer(string userResponse)
        {
            bool result = false;
            userResponse = userResponse.ToLower();
            char[] userResponseArr = userResponse.ToCharArray();
            if (userResponseArr.Length >= 2)
            {
                result = false;
                Console.WriteLine("You enter to many signs, please try again.");
            }
            else if (string.IsNullOrEmpty(userResponse))
            {
                result = false;
                Console.WriteLine("You didn't give an answer, Please try again.");
            }
            else if (!char.IsLetter(userResponseArr[0]))
            {
                result = true;
                Console.WriteLine("Incorrect answer, please enter a letter.");
            }
            return result;
        }

        private void GuessClue(string word, string hiddenWord, CategoryName categoryName)
        {
            int numberOfTrialsLimit = word.Length + 2;
            int numberOfTrials = 0;
            word = string.Join(" ", word.ToCharArray());
            while (hiddenWord.Contains(Characters.Underscore))
            {
                Console.WriteLine("Enter only one letter:");
                string userResponse = Console.ReadLine();
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
                    Console.WriteLine("Press any key to continue..");
                    break;
                }
                Console.WriteLine(hiddenWord);
            }
            if(categoryName == CategoryName.Countries)
            {
                char[] wordarr = word.ToCharArray();
                wordarr[0] = char.ToUpper(wordarr[0]);
                string newWord = wordarr[0] + word.Substring(1);
                Console.WriteLine($"Your clue is: {newWord}");
            }
            else 
            {
                Console.WriteLine($"Your clue is: {word}");
            }
            Console.ReadKey();
            Console.Clear();
            NewGame();
        }

        private void NewGame()
        {
            Console.WriteLine("Are you ready for the next game?");
            Console.WriteLine("Click right answer:");
            Console.WriteLine("1.Play again!\n2.Exit");
            char game = Console.ReadKey().KeyChar;
            Console.Clear();
            switch (game)
            {
                case '1':
                    MenuAction.ChooseClueCategory();
                    break;
                case '2':
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Your action does not exist");
                    Console.Clear();
                    break;
            }
        }
    }
}