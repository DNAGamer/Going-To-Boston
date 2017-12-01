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
            Console.WriteLine("Welcome to Going to Boston\nWhat mode do you want to play?\n1) Score Play\n2) Match Play");
            int num = Convert.ToInt32(Console.ReadLine());
            if (num == 1)
                ScorePlay();
            else if (num == 2)
                MatchPlay();
            return;
        }

        static void ScorePlay()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Score Play");
            Console.WriteLine("Players add up their total after each round. The player with the highest score after 5 rounds wins the game.");
            bool inGame = true;
            int player = 1;
            int turn = 1;
            while (inGame)
            {
                Console.Clear();
                if (player == 3)
                    player = 1;
                Console.WriteLine($"Player {player}'s turn\nScore {Player.getScore(player)}\nTurn {turn}");
                Console.WriteLine("How hard do you want to roll the dice?");
                Console.ReadLine();
                int roll = Die.Roll();
                Console.WriteLine($"You got a {roll}!");
                Player.setScore(player, (Player.getScore(player) + roll));
                player++;
                turn++;
                System.Threading.Thread.Sleep(2000);
                if (turn == 6)
                {
                    Console.Clear();
                    if (Player.getScore(1) > Player.getScore(2))
                    {
                        Console.Write("Player 1 Won!");
                    } else if (Player.getScore(2) > Player.getScore(1))
                    {
                        Console.Write("Player 2 Won!");
                    }else
                    {
                        Console.WriteLine("It was a draw O.O");
                    }
                    System.Threading.Thread.Sleep(2000);
                    return;
                }
                   
            }
        }

        static void MatchPlay()
        {
            Console.WriteLine("Welcome to Match Play");
            Console.WriteLine("Players win a point each round. The first to 5 wins the game");
        }
    }


    class Die
    {
        public static int Roll()
        {
            int num = new Random().Next(1, 6);
            return (num);
        }

    }


    class Player
    {
        static int P1score;
        static int P2score; 


        public static void setScore(int player, int score)
        {
            if (player == 1)
                P1score = score;
            if (player == 2)
                P2score = score;
        }

        public static int getScore(int player)
        {
            if (player == 1)
                return P1score;
            else if (player == 2)
                return P2score;
            else
                return 0;
        }

    }


    class AI
    {

    }
}
