﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{


    struct Player
    {
        public string _playerName;
        public int _playerHealth;
        public int _playerDamage;
        public int _playerLevel;
    }
    
    struct Item
    {
        public string name;
        public int statBoost;

    }

    class Game
    {
        bool _gameOver = false;
        Player player1;
        Player player2;
        Random random = new Random();
        public int _levelScaleMax;
        int _roomNum = 0;
        //Run the game
        public void Run()
        {
            Start();

            while (_gameOver == false)
            {
                Update();
            }

            End();
        }
        public void ModeChoices()
        {
            char input;
            GetInput(out input, "Single Player", "Multiplayer", "Choose a mode");
            if (input == '1')
            {
                Console.Clear();
                SelectCharacter();
                ClimbLadder(0);
            }
            else
            {
                Console.Clear();
                GetWeapon();
                MultiplayerBattle();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //RNG Example:
        // Random random;
        //random = new Random();
        //int randomNumber = random.Next(0,1);
        //if(randomNumber == 0)
        //{
        //   kill player
        //}
        //else
        //{
        //   heal player
        //}
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //This function handles the battles for our ladder. roomNum is used to update the our opponent to be the enemy in the current room. 
        //turnCount is used to keep track of how many turns it took the player to beat the enemy
        void GetInput(out char input, string option1, string option2, string query)
        {
            Console.WriteLine(query);
            input = ' ';
            while (input != '1' && input != '2')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }
        }
        void GetInput(out char input, string option1, string option2)
        {
            input = ' ';
            while (input != '1' && input != '2')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }

        }
        bool StartBattle(int roomNum, ref int turnCount)
        {
            //initialize default enemy stats
            int enemyHealth = 0;
            int enemyAttack = 0;
            string enemyName = "";
            //Changes the enemy's default stats based on our current room number. 
            //This is how we make it seem as if the player is fighting different enemies
            switch (roomNum)
            {
                case 0:
                    {
                        enemyHealth = 100;
                        enemyAttack = 20;
                        enemyName = "Wizard";
                        break;
                    }
                case 1:
                    {
                        enemyHealth = 80;
                        enemyAttack = 30;
                        enemyName = "Troll";
                        break;
                    }
                case 2:
                    {

                        enemyHealth = 200;
                        enemyAttack = 40;
                        enemyName = "Giant";
                        break;
                    }
            }

            //Loops until the player or the enemy is dead
            while (player1._playerHealth >= 0 && enemyHealth >= 0)
            {
                //Displays the stats for both charactersa to the screen before the player takes their turn
                PrintStats(player1._playerName, player1._playerHealth, player1._playerDamage);
                PrintStats(enemyName, enemyHealth, enemyAttack);

                //Get input from the player
                char input = ' ';
                GetInput(out input, "Attack", "Defend");
                Console.WriteLine();
                //If input is 1, the player wants to attack. By default the enemy blocks any incoming attack
                if (input == '1')
                {
                    Console.WriteLine(" You dealt " + (player1._playerDamage) + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.WriteLine(enemyName + " dealt " + enemyAttack + " damage.");
                    player1._playerHealth -= enemyAttack;

                }
                //If the player decides to defend the enemy just takes their turn. However this time the block attack function is
                //called instead of simply decrementing the health by the enemy's attack value.
                else
                {
                    Console.WriteLine(enemyName + " dealt " + (enemyAttack) + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.Clear();
                //After the player attacks, the enemy takes its turn. Since the player decided not to defend, the block attack function is not called.
                Console.ReadKey();

                turnCount++;

            }
            //Return whether or not our player died
            return player1._playerHealth >= 0;

        }


        //void Shop()
        //{
        //char input;
        //PrintStats(_playerName, _playerHealth, _playerDamage, _playerDefense);
        //Console.WriteLine("\nYou encountered a strange old man! He wants to help you out and brings you to his shop.");
        //Console.WriteLine("\n What would you like to buy or use?");
        //"Stand on the healing pad", "Buy the weird potion", "Most powerful twig", "The most boring basic sword");

        //case '1':



        //}
        //Decrements the health of a character. The attack value is subtracted by that character's defense
        //Scales up the player's stats based on the amount of turns it took in the last battle
        void LevelUp(int turnCount)
        {
            //Subtract the amount of turns from our maximum level scale to get our current level scale
            int scale = _levelScaleMax - turnCount;
            if (scale <= 0)
            {
                scale = 1;
            }
            player1._playerHealth += 10 * scale;
            player1._playerDamage *= scale;
        }
        void PrintStats(string name, int health, int damage)
        {
            Console.WriteLine(name);
            Console.WriteLine("Health: " + health);
            Console.WriteLine("Damage: " + damage);
        }

        //This is used to progress through our game. A recursive function meant to switch the rooms and start the battles inside them.
        void ClimbLadder(int roomNum)
        {
            //Displays context based on which room the player is in
            switch (roomNum)
            {
                case 0:
                    {
                        Console.WriteLine("A wizard blocks your path");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("A troll stands before you");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("A giant has appeared!");
                        break;
                    }
                default:
                    {
                        _gameOver = true;
                        return;
                    }
            }
            int turnCount = 0;
            //Starts a battle. If the player survived the battle, level them up and then proceed to the next room.
            if (StartBattle(roomNum, ref turnCount))
            {
                LevelUp(turnCount);
                return;
            }
            _gameOver = true;

        }

        //Displays the character selection menu. 
        void SelectCharacter()
        {
            char input = ' ';
            //Loops until a valid option is choosen
            while (input != '1' && input != '2' && input != '3')
            {
                //Prints options
                Console.WriteLine("Welcome! Please select a character.");
                Console.WriteLine("1.Sir Kibble");
                Console.WriteLine("2.Gnojoel");
                Console.WriteLine("3.Joedazz");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                //Sets the players default stats based on which character was picked
                switch (input)
                {
                    case '1':
                        {
                            player1._playerName = "Sir Kibble";
                            player1._playerHealth = 120;
                            player1._playerDamage = 40;
                            break;
                        }
                    case '2':
                        {
                            player1._playerName = "Gnojoel";
                            player1._playerHealth = 40;
                            player1._playerDamage = 70;
                            break;
                        }
                    case '3':
                        {
                            player1._playerName = "Joedazz";
                            player1._playerHealth = 200;
                            player1._playerDamage = 25;
                            break;
                        }
                    //If an invalid input is selected display and input message and input over again.
                    default:
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.Write("> ");
                            Console.ReadKey();
                            break;
                        }
                }
                Console.Clear();
            }
            //Prints the stats of the choosen character to the screen before the game begins to give the player visual feedback
            PrintStats(player1._playerName, player1._playerHealth, player1._playerDamage);
            Console.WriteLine("Press any key to continue.");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
        }

        void GetWeapon()
        {
            char input;
            GetInput(out input, "Dirt", "Rubber Duck", "Choose a weapon NOW!");
            if (input == '1')
            {
                player1._playerDamage = 30;
            }
            else
            {
                player1._playerDamage = 10;
            }
            Console.Clear();
            GetInput(out input, "Dirt", "Rubber Duck", "Choose a weapon NOW!");
            if (input == '1')
            {
                player2._playerDamage = 30;
            }
            else
            {
                player2._playerDamage = 10;
            }

        }
        void MultiplayerBattle()
        {
            while (player1._playerHealth > 0 && player2._playerHealth > 0)
            {
                PrintStat(player1);
                PrintStat(player2);
                random.Next(0, 10);

                char input;
                GetInput(out input, "Attack", "Do nothing", "Player1 turn");
                if (input == '1')
                {


                }
            }
        }
        void InitializeCharacters()
        {
            player1._playerName = "#Player1Sucks";
            player1._playerDamage = 25;
            player1._playerHealth = 100;
            player2._playerName = "#WhyPlayer2#Player3Gang";
            player2._playerDamage = 25;
            player2._playerHealth = 100;





        }

        void PrintStat(Player player)
        {
            Console.WriteLine("\n" + player._playerHealth);
            Console.WriteLine("Health: " + player._playerHealth);
            Console.WriteLine("Damage: " + player._playerDamage);
            Console.WriteLine("Level: " + player._playerLevel);
        }
        //Performed once when the game begins
        public void Start()
        {
            InitializeCharacters();
        }

        public void ChooseGameMode()
        {










        }

        //Repeated until the game ends
        public void Update()
        {
            ClimbLadder(_roomNum);
            _roomNum++;
        }

        //Performed once when the game ends
        public void End()
        {
            //If the player died print death message
            if (player1._playerHealth <= 0 == true)
            {
                Console.WriteLine("Failure");
                return;
            }
            {
                Console.WriteLine();
            }
            //Print game over message
            Console.WriteLine("Congrats");
        }
    }
}
