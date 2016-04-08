using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
   Name: Joe Nesbella
   Desc: Bowling Game Main for Code Kata.
   Date: 4/8/2016 
*/

namespace BowlingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game bowlGame = new Game();
            Console.WriteLine("Welcome to the bowling game.");

            for (int i = 0; i < bowlGame.GetNumFrames(); i++)
            {
                Console.Clear();
                bowlGame.PrintScore();
                bowlGame.DoFrame();
                Console.ReadLine();
            }

            //Check if the final frame was a strike or a spare.
            if (bowlGame.SpecialFrame())
            {
                Console.Clear();
                bowlGame.PrintScore();
                bowlGame.DoFrame();
                Console.ReadLine();
            }

            Console.Clear();
            bowlGame.PrintScore();
            Console.WriteLine();
            Console.WriteLine("Game Over, good try. :)");
            Console.WriteLine("Final score {0}", bowlGame.Score());
            Console.WriteLine("\nPress return to quit.");
            Console.ReadLine();
        }
    }
}
