
/*
Name: GoingToBoston
Author: Daniel Bearman
Creation: 01/12/2017
State: COMPLETE
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GoingToBoston
{
    class Program
    {
        static void Main(string[] args)
        {
            //Asks the player to choose a mode
            TypeWrite("Welcome to Going to Boston\nWhat mode do you want to play?\n1) Score Play\n2) Match Play");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num == 1)
                ScorePlay(); //starts the ScorePlay method
            else if (num == 2)
                MatchPlay(); //starts the MatchPlay method
            return;
        }

        // Fancier output to make the console less boring
        static void TypeWrite(string text)
        {
            //iterates through each letter with a 5ms delay to add a typewriter effect
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                System.Threading.Thread.Sleep(5);
            }
            //adds a new line at the end to mimic "Console.WriteLine"
            Console.Write("\n");
        }

        static void ScorePlay() //the score play game style
        {
            Console.Clear(); //clears the console
            TypeWrite("Welcome to Score Play");
            TypeWrite("Players add up their total after each round. The player with the highest score after 5 rounds wins the game.");
            TypeWrite("Press any key to continue");
            Console.ReadKey(); //waits for the player to read the introduction

            bool inGame = true; //sets this to true to control the loop
            int player = 1; //this variable controls which player is "playing"
            int turn = 1; //what turn is it
            int P1dice = 3; // player 1s dice counter
            int P2dice = 3; // player 2s dice counter
            int dice =0; // a temp dice value holder so i dont have to have 2 nigh on identical scripts
            int roll = 0; // a temp value holder that holds the current roll's sum

            //the 2 lines below arent strictly necesary but better safe than sorry
            Player.setScore(1, 0); // resets the players score to 0
            Player.setScore(2, 0); // resets the players score to 0
            while (inGame) //woo game loop
            {
                Console.Clear();
                if (player == 3) // if the player variable overflows over 2, this shoves it back to 1, so player 1 can play
                    player = 1; // sets the player back to 1
                TypeWrite($"Player {player}'s turn\nScore {Player.getScore(player)}\nTurn {turn}"); // gives the player some basic game info
                TypeWrite("How hard do you want to roll the dice?"); // gives the player the illusion of control
                Console.ReadLine(); //waits for the player to hit enter
                if (player == 1) //sets dice to P1Dice so i dont need to use 2 nigh on identical scripts
                    dice = P1dice; // ^
                if (player == 2) // sets dice to P2Dice so i dont need to use 2 nigh on identical scripts
                    dice = P2dice; // ^

                if (dice <= 1) //if the player only has 1 dice, dont bother with the loop, just roll one dice
                {
                    TypeWrite("Rolling one die");
                    dice = Die.Roll(); // calls the dice roller
                }
                else
                {
                    TypeWrite($"Rolling {dice} dice"); // tells the player how many dice theyre rolling
                    for (int i = 1; i < dice; i++) // loops until its rolled x ammount of dice
                        roll = roll + Die.Roll(); 
                }
                TypeWrite($"You got a {roll}!"); //tells the player what they scored
                Player.setScore(player, (Player.getScore(player) + roll)); //sets the players score
                
                if (player == 1) //takes a dice off of whoever was just playing
                    P1dice--;
                if (player == 2)
                    P2dice--;
                if (player == 2)
                    turn++;
                player++;
                System.Threading.Thread.Sleep(2000); //pauses the thread to give the player chance to read

                if (turn == 6) // ends the game at the end of the 5th turn, but due to the above "turn++" it checks if the turn is he 6th
                {
                    Console.Clear(); //clears the console
                    if (Player.getScore(1) > Player.getScore(2)) //did player 1 get a higher score
                    {
                        TypeWrite($"Player 1 Won!\nWith {Player.getScore(1)} points");
                    }
                    else if (Player.getScore(2) > Player.getScore(1)) //did player 2 get a higher score
                    {
                        TypeWrite($"Player 2 Won!\nWith {Player.getScore(2)} points");
                    }
                    else //if the above statements were false, it had to be a draw
                    {
                        TypeWrite("It was a draw O.O");
                    }
                    System.Threading.Thread.Sleep(2000); //pauses the thread to give the player chance to read
                    return; //ends the loop
                }

            }
        }

        static void MatchPlay()
        {
            TypeWrite("Welcome to Match Play");
            TypeWrite("Players win a point each round. The first to 5 wins the game");
            TypeWrite("Press any key to continue");
            Console.ReadKey();

            bool inGame = true;
            int player = 1;
            Player.setScore(1, 0);
            Player.setScore(2, 0);
            int P1TempScore;
            int P2TempScore;
            int roll;
            int turn =3;
            while (inGame)
            {
                //Player 1
                Console.Clear();
                roll = 0;
                if (player == 3)
                    player = 1;
                TypeWrite($"Player {player}'s turn\nScore {Player.getScore(player)}\n");
                TypeWrite("How hard do you want to roll the dice?");
                Console.ReadLine();
                if (turn <= 1)
                {
                    TypeWrite("Rolling one die");
                    roll = Die.Roll();
                }
                else
                {
                    TypeWrite($"Rolling {turn} dice");
                    for (int i = 1; i < turn; i++)
                        roll = roll + Die.Roll();
                }
                
                TypeWrite($"You got a {roll}!");
                P1TempScore = roll;
                player++;
                System.Threading.Thread.Sleep(2000); //pauses the thread to give the player chance to read

                //Player 2
                Console.Clear();
                roll = 0;
                TypeWrite($"Player {player}'s turn\nScore {Player.getScore(player)}\n");
                TypeWrite("How hard do you want to roll the dice?");
                Console.ReadLine();
                if (turn <= 1)
                {
                    TypeWrite("Rolling one die");
                    roll = Die.Roll();
                }
                else
                {
                    TypeWrite($"Rolling {turn} dice");
                    for (int i = 1; i < turn; i++)
                        roll = roll + Die.Roll();
                }
                TypeWrite($"You got a {roll}!");
                P2TempScore = roll;
                player++;
                System.Threading.Thread.Sleep(2000); //pauses the thread to give the player chance to read

                turn--;
                if (P1TempScore > P2TempScore)
                {
                    Player.setScore(1, Player.getScore(1) + 1);
                    TypeWrite($"Player 1 wins the point \n({P1TempScore}/{P2TempScore})");
                    TypeWrite($"Player 1 has {Player.getScore(1)} points\nPlayer 2 has {Player.getScore(2)} points");
                }
                else if (P2TempScore > P1TempScore)
                {
                    Player.setScore(2, Player.getScore(1) + 1);
                    TypeWrite($"Player 2 wins the point \n({P1TempScore}/{P2TempScore})");
                    TypeWrite($"Player 1 has {Player.getScore(1)} points\nPlayer 2 has {Player.getScore(2)} points");
                }else if (P1TempScore == P2TempScore)
                {
                    TypeWrite($"Nobody wins a point this round \n({P1TempScore}/{P2TempScore})");
                    TypeWrite($"Player 1 has {Player.getScore(1)} points\nPlayer 2 has {Player.getScore(2)} points");
                }
                System.Threading.Thread.Sleep(2000); //pauses the thread to give the player chance to read

                // checks who won and how
                if (Player.getScore(1) == 5 && Player.getScore(2) == 5) // players got the same score
                {
                    TypeWrite("Its a draw!");
                    TypeWrite($"You both got {Player.getScore(1)}");
                    return;
                }
                else if (Player.getScore(1) == 5) // player 1 got a score of 5
                {
                    TypeWrite("Player 1 won!");
                    TypeWrite($"Player 1 got {Player.getScore(1)}, Player 2 got {Player.getScore(2)}");

                    return;
                }
                else if (Player.getScore(2) == 5) // player 2 got a score of 5
                {
                    TypeWrite("Player 2 won!");
                    TypeWrite($"Player 1 got {Player.getScore(1)}, Player 2 got {Player.getScore(2)}");
                    return;
                }
                

            }
            TypeWrite("Press any key to continue");
            Console.ReadKey();
        }


        class Die // a class that contains the dice, because apparently this cant be in the main class ¯\_(ツ)_/¯
        {
            public static int Roll() //gives a value between 1 and 6... like a dice
            {
                int num = new Random().Next(1, 6);
                return (num);
            }

        }


        class Player // a load of methods that REALLY shouldnt be in a seperate class, but i have to have a player class apparently
        {
            static int P1score; //Player 1s score
            static int P2score; //Player 2s score


            public static void setScore(int player, int score) //sets the players score
            {
                if (player == 1)
                    P1score = score; 
                if (player == 2)
                    P2score = score;
            }

            public static int getScore(int player)  //gets the players score
            {
                if (player == 1) 
                    return P1score; //returns the desired score
                else if (player == 2)
                    return P2score;
                else
                    return 0; //if some erroneous input is entered, this will return 0
            }

        }
    }
}
