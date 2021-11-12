using System;
using HangmanQuiz.Helpers;

namespace HangmanQuiz
{
    public class MenuAction
    {
        public static void ChooseClueCategory()
        {
            Console.WriteLine("Choose one category:");
            Console.WriteLine($"1.{CategoryName.IT}\n2.{CategoryName.Countries}\n3.{CategoryName.Food}");
            char userAnswer = Console.ReadKey().KeyChar;
            Clue clue = new Clue();
            if (userAnswer == '1')
            {
                var category = CategoryName.IT;
                clue.DisplayClue(category);
            }
            else if(userAnswer == '2')
            {
                var category = CategoryName.Countries;
                clue.DisplayClue(category);
            }
            else if (userAnswer == '3')
            {
                var category = CategoryName.Food;
                clue.DisplayClue(category);
            }
        }
    }
}