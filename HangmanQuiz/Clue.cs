using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HangmanQuiz.Helpers;


namespace HangmanQuiz
{
    public class Clue
    {
        private List<string> GetClueList(CategoryName categoryName)
        {
            string directoryPath = @"C:\Users\paull\source\repos\HangmanQuiz\HangmanQuiz\Clues";
            string iTPath = @"C:\Users\paull\source\repos\HangmanQuiz\HangmanQuiz\Clues\IT.txt";
            string countriesPath = @"C:\Users\paull\source\repos\HangmanQuiz\HangmanQuiz\Clues\Coutries.txt";
            string foodPath = @"C:\Users\paull\source\repos\HangmanQuiz\HangmanQuiz\Clues\Food.txt";
            bool directoryExist = Directory.Exists(directoryPath);
            if (!directoryExist)
            {
                Directory.CreateDirectory(directoryPath);
                using (FileStream streamFile = File.Open(iTPath, FileMode.CreateNew, FileAccess.ReadWrite))
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
                using (FileStream streamFile = File.Open(countriesPath, FileMode.CreateNew, FileAccess.ReadWrite))
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
                using (FileStream streamFile = File.Open(foodPath, FileMode.CreateNew, FileAccess.ReadWrite))
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
                List<string> iTClues = File.ReadLines(iTPath).ToList();
                return iTClues;
            }
            else if(categoryName == CategoryName.Countries)
            {
                List<string> countryClues = File.ReadLines(countriesPath).ToList();
                return countryClues;
            }
            else
            {
                List<string> foodClues = File.ReadLines(foodPath).ToList();
                return foodClues;
            }
        }

        public string GetRandomClue(CategoryName categoryName)
        {
            List<string> clues = GetClueList(categoryName);
            string word = clues.OrderBy(x => Guid.NewGuid()).First();
            return word;
        }

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
            GuessClue(word,hiddenWord,category);
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
        public string GuessClue(string word, string hiddenWord, CategoryName categoryName)
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

            return hiddenWord;
        }
        public void NewGame()
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
