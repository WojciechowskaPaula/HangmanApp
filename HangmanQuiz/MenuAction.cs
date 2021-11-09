using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HangmanQuiz.Helpers;

namespace HangmanQuiz
{
    public class MenuAction
    {
        public static void ChooseClueCategory()
        {
            Console.WriteLine("Choose one category:");
            Console.WriteLine($"1.{Category.CategoryName.IT}\n2.{Category.CategoryName.Countries}\n3.{Category.CategoryName.Food}");
            char userAnswer = Console.ReadKey().KeyChar;
            if(userAnswer == '1')
            {
                Console.WriteLine(Category.CategoryName.IT);
            }
            else if(userAnswer == '2')
            {
                Console.WriteLine(Category.CategoryName.Countries);
            }
            else if (userAnswer == '3')
            {
                Console.WriteLine(Category.CategoryName.Food);
            }
        }
        
    }
}
