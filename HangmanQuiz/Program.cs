using System;
using System.IO;
using HangmanQuiz.Helpers;
namespace HangmanQuiz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome in HangmanApp!");
            Console.WriteLine("Let's get started");
            Console.WriteLine("Please press any key to start..");
            Console.ReadKey();
            Console.Clear();
            Clue clue = new Clue();
            string randomWord = clue.DisplayClue();
        }

    }
}
