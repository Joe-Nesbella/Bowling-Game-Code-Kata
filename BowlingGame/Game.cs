using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;

 /*
    Name: Joe Nesbella
    Desc: Bowling Game Class for Code Kata.
    Date: 4/8/2016 
 */

namespace BowlingGame
{
    class Game
    {
        const int NumFrames = 10;
        private int activePins; //Current number of pins that are 'up'.
        private Random randGenerator;
        private int curFrame;

        private int[] frameScores;

        public Game() //Constructor
        {
            randGenerator = new Random();
            activePins = 10;
            curFrame = 0;

            frameScores = new int[21]; //2x potential scores + 1 if you get a strike/spare on the last frame.

            for (int totalFrames = 0; totalFrames < 21; totalFrames++)
            {
                frameScores[totalFrames] = 0;
            }   
        }

        ~Game() //Destructor
        {
        }

        public bool SpecialFrame()
        {
            //Add the final two frames, if it's 10 we got a strike or spare.
            return frameScores[18] + frameScores[19] == 10;
        }

        public int Score()
        {
            int totalScore = 0;
            for(int s = 0; s < NumFrames; s++)
            {
                if (frameScores[s * 2] == 10) //Is this a strike?
                {
                    totalScore += frameScores[s * 2];
                    if (s * 2 + 3 < NumFrames * 2)
                    {
                        totalScore += frameScores[s * 2 + 2]; //Add Strike Bonus.
                        totalScore += frameScores[s * 2 + 3];
                    }
                }else if (frameScores[s * 2] + frameScores[s * 2 + 1] == 10){ //Is it a spare?
                    totalScore += frameScores[s * 2];
                    totalScore += frameScores[s * 2 + 1];
                    if (s * 2 + 2 < NumFrames * 2)
                        totalScore += frameScores[s * 2 + 2]; //Add Spare Bonus.
                }else{ //Normal Circumstance just add the scores. :)
                    totalScore += frameScores[s * 2];
                    totalScore += frameScores[s * 2 + 1];
                }
            }

            totalScore += frameScores[20]; //Add Extra Frame.

            return totalScore;
        }

        public void PrintScore()
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("|  1  |  2  |  3  |  4  |  5  |  6  |  7  |  8  |  9  | 10  |     |");
            Console.WriteLine("-------------------------------------------------------------------");
            for (int i = 0; i < 10; i++)
            {
                if(frameScores[i * 2] == 10)
                    Console.Write("|  X  ");
                else if(frameScores[i * 2] + frameScores[i * 2 + 1] == 10)
                    Console.Write("|{0}/ /", frameScores[i * 2].ToString("00"));
                else
                    Console.Write("|{0}/{1}", frameScores[i * 2].ToString("00"), frameScores[i * 2+1].ToString("00"));
            }

            if (curFrame > 10)
            {
                if (frameScores[20] == 10)
                    Console.Write("|  X  ");
                else
                    Console.Write("| {0}  ", frameScores[20].ToString("00"));
            }

            Console.Write("|");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------");
        }

        private void RollBall(int numPins, bool ballTwo = false)
        {
            activePins -= numPins;

            //Print the activity of rolling the ball.
            Console.WriteLine();
            if (numPins == 0)
            {
                Console.WriteLine("Bummer, Gutterball.");
            }else if (numPins == 10 && !ballTwo)
            {
                Console.WriteLine("***STRIKE***");
            }else{
                if (activePins == 0)
                {
                    Console.WriteLine("Radical! Picked up a spare. :)");
                }else{
                    Console.WriteLine("Great shot! You knocked down {0} Pins!", numPins);
                }
            }
        }

        public int GetNumFrames()
        {
            return NumFrames;
        }

        public void DoFrame()
        {
            activePins = 10; //Reset the pins at the beginning of the frame.

            int numKnockdown = randGenerator.Next(0, activePins+1); //Generate a random amount of pins to knockdown.
            Console.WriteLine();
            if(curFrame < 10)
                Console.WriteLine("Beginning Frame {0} Ball 1.", curFrame + 1);
            else
                Console.WriteLine("Beginning Extra Frame");

            RollBall(numKnockdown);

            if (curFrame < 10)
                frameScores[curFrame * 2] = numKnockdown;
            else
                frameScores[20] = numKnockdown;

            if (activePins > 0 && curFrame < 10) //We didn't get a strike & have the other half of the frame to do.
            {
                Console.WriteLine();
                Console.WriteLine("Frame {0} Ball 2.", curFrame + 1);
                numKnockdown = randGenerator.Next(0, activePins+1);
                RollBall(numKnockdown, true);
                frameScores[curFrame * 2 + 1] = numKnockdown;
            }

            curFrame++;
        }
    }
}
