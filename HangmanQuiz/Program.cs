﻿using System;
using System.IO;

namespace HangmanQuiz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome in HangmanApp!");
            Console.WriteLine("Let's get started");
            Console.WriteLine("Please press any key to start..");
            Console.ReadLine();
            Clue clue = new Clue();
            string randomWord = clue.DisplayClue();
        }

    }
}
