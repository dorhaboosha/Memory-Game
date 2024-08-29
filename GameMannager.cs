using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;
using static Ex02.Player;

namespace Ex02
{
    public class GameMannager
    {
        private Board m_BoardGame;
        private Player[] m_Players;
        private int m_CurrentPlayerIndex;

        public void StartGame()
        {
            setPlayers();
            setBoard();
            playGame();
        }

        private void setPlayers()
        {
            Console.WriteLine("Welcome to the Memory Game!!!");
            Console.WriteLine("Please fist player write your name (up to 20 characters allowed, no spaces):");
            string firstPlayerName = GameRules.GetThePlayerName();
            m_Players = new Player[2];
            m_Players[0] = new Player(firstPlayerName, eWhoPlay.Person);
            Console.WriteLine("To play against the computer enter C," +
                " to play against another player enter P (another character will not be accepted).");
            bool secondPlayerIsComputer = GameRules.GetTheSecondPlayerType() == eWhoPlay.Computer;
            
            if (secondPlayerIsComputer)
            {
                m_Players[1] = new Player("Computer", eWhoPlay.Computer);
            }
            else 
            {
                Console.WriteLine("Please second player write your name (up to 20 characters allowed, no spaces):");
                string secondPlayerName = GameRules.GetThePlayerName();
                
                m_Players[1] = new Player(secondPlayerName, eWhoPlay.Person);
            }
        }

        private void setBoard()
        {
            bool checkSizeBoard = true;
            
            while (checkSizeBoard)
            {
                Console.WriteLine("Write the number of rows of the game board " +
                    "(can be a number between 4 and 6, the number of cells on the board must be even):");
                int numberOfRows = GameRules.GetNumberOfRowsAndColumns();
                Console.WriteLine("Write the number of columns of the game board " +
                    "(can be a number between 4 and 6, the number of cells on the board must be even):");
                int numberOfColumns = GameRules.GetNumberOfRowsAndColumns();
                
                if (GameRules.ValidSizeBoard(numberOfRows, numberOfColumns))
                {
                    checkSizeBoard = false;
                    m_BoardGame = new Board(numberOfRows, numberOfColumns);
                }
                else 
                {
                    Console.WriteLine("The number od cells numst be even, Please write another numbers for rows and columns.");
                }
            }
        }

        private void playGame()
        {
            m_CurrentPlayerIndex = 0;
            
            for (int i = 0; i < m_Players.Length; i++)
            {
                m_Players[i].Score = 0;
            }
            
            bool gameRunning = true;
            
            while (gameRunning && !m_BoardGame.CheckIfTheBoardComplete())
            {
                displayBoard(m_BoardGame);
                int numberRowFirstCard;
                int numberColumnFirstCard;
                bool thePlayerIsComputer = m_Players[m_CurrentPlayerIndex].WhoPlay == "Computer";
                
                if (thePlayerIsComputer)
                {
                    Console.WriteLine("\n{0} now is your turn, please write the first cell you want to to open:",
                        m_Players[m_CurrentPlayerIndex].Name);
                    computerTurn(out numberRowFirstCard, out numberColumnFirstCard);
                }
                else
                {
                    Console.WriteLine("\n{0} now is your turn, please enter the first cell you want to open or Q to exit:",
                        m_Players[m_CurrentPlayerIndex].Name);
                    bool firstMove = playerTurn(out numberRowFirstCard, out numberColumnFirstCard);
                    
                    if (!firstMove)
                    {
                        gameRunning = !gameRunning;
                        Console.WriteLine("\n{0} chose to exit the game so game over, Goodbye!", m_Players[m_CurrentPlayerIndex].Name);
                        continue;
                    }
                }

                Screen.Clear();
                m_BoardGame.OpenCard(numberRowFirstCard, numberColumnFirstCard);
                displayBoard(m_BoardGame);
                int numberRowSecondCard;
                int numberColumnSecondCard;
                
                if (thePlayerIsComputer)
                {
                    Console.WriteLine("\n{0} now is your turn, please enter the second cell you want to open:",
                        m_Players[m_CurrentPlayerIndex].Name);
                    computerTurn(out numberRowSecondCard, out numberColumnSecondCard);
                }
                else
                {
                    Console.WriteLine("\n{0} now is your turn, please enter the second cell you want to open or Q to exit:",
                        m_Players[m_CurrentPlayerIndex].Name);
                    bool secondMove = playerTurn(out numberRowSecondCard, out numberColumnSecondCard);
                    
                    if (!secondMove)
                    {
                        gameRunning = !gameRunning;
                        Console.WriteLine("\n{0} chose to exit the game so game over, Goodbye!", m_Players[m_CurrentPlayerIndex].Name);
                        continue;
                    }
                }
                
                m_BoardGame.OpenCard(numberRowSecondCard, numberColumnSecondCard);
                
                if (GameRules.MatchCards(m_BoardGame.GetCard(numberRowFirstCard, numberColumnFirstCard), 
                    m_BoardGame.GetCard(numberRowSecondCard, numberColumnSecondCard)))
                {
                    m_Players[m_CurrentPlayerIndex].Score++;
                    Screen.Clear();
                    displayBoard(m_BoardGame);
                    Console.WriteLine("\nYou found match cards :)");
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    Screen.Clear();
                    displayBoard(m_BoardGame);
                    Console.WriteLine("\nYou didn't find match cards :(");
                    System.Threading.Thread.Sleep(2000);
                    m_BoardGame.CloseCard(numberRowFirstCard, numberColumnFirstCard);
                    m_BoardGame.CloseCard(numberRowSecondCard, numberColumnSecondCard);
                    m_CurrentPlayerIndex = (m_CurrentPlayerIndex + 1) % 2; 
                }
                Screen.Clear();
            }

            if (gameRunning)
            {
                printTheWinner(m_Players[0], m_Players[1]);
                playAgain();
            }
        }

        private bool playerTurn(out int o_NumberRow, out int o_NumberColumn)
        {
            bool stopTurn = true;
            bool checkInput = true;
            o_NumberRow = -1;
            o_NumberColumn = -1;

            while (checkInput)
            {
                string playerAnswer = GameRules.GetAnswerFromPlayer(m_BoardGame.NumberOfRows, m_BoardGame.NumberOfColumns);
                bool playerExit = playerAnswer == "Q";
                
                if (playerExit)
                {
                    stopTurn = !stopTurn;
                    checkInput = !checkInput;
                }
                else
                {
                    o_NumberRow = GameRules.GetNumberRow(playerAnswer[1]);
                    o_NumberColumn = GameRules.GetNumberColumn(playerAnswer[0]);
                    
                    if (m_BoardGame.GetCard(o_NumberRow, o_NumberColumn).IsCardRevealed)
                    {
                        Console.WriteLine("The card in the cell is already exposed," +
                            " please choose another cell in which you want to reveal the card (write the cell you want to open or Q to exit):");
                    }
                    else
                    {
                        checkInput = !checkInput;
                    }
                }
            }
    
            return stopTurn;
        }

        private void printTheWinner(Player i_FirstPlayer, Player i_SecondPlayer)
        {
            bool firstPlayerWin = i_FirstPlayer.Score > i_SecondPlayer.Score;
            bool secondPlayerWin = i_FirstPlayer.Score < i_SecondPlayer.Score;
            
            if (firstPlayerWin)
            {
                Console.WriteLine("Congratulations to {0} the winner of the memory game.", i_FirstPlayer.Name);
            }
            
            else if (secondPlayerWin)
            {
                Console.WriteLine("Congratulations to {0} the winner of the memory game.", i_SecondPlayer.Name);
            }
            else
            {
                Console.WriteLine("There is no winner for the memory game, there is a tie between the players!");
            }

            Console.WriteLine("{0} received {1} points.", i_FirstPlayer.Name, i_FirstPlayer.Score);
            Console.WriteLine("{0} received {1} points.", i_SecondPlayer.Name, i_SecondPlayer.Score);
        }

        private void playAgain()
        {
            Console.WriteLine("Please write Y to play again or N to exit (other character will not be accepted)");
            string playerAnswer = GameRules.GetAnswerForAgain();
            bool playerWantAgain = playerAnswer == "Y";
            
            if (playerWantAgain)
            {
                setBoard();
                playGame();
            }
            else 
            {
                Console.WriteLine("You chose to exit, it was nice game, Goodbye!");
            }
        }

        private void computerTurn(out int o_NumberOfRow, out int o_NumberOfColumn)
        {
            string computerAnswer = GameRules.GetAnswerFromComputer(m_BoardGame, m_BoardGame.NumberOfRows, m_BoardGame.NumberOfColumns);
            Console.WriteLine(computerAnswer);
            System.Threading.Thread.Sleep(1000);
            o_NumberOfRow = GameRules.GetNumberRow(computerAnswer[1]);
            o_NumberOfColumn = GameRules.GetNumberColumn(computerAnswer[0]);
        }

        private static void displayBoard(Board i_GameBoard)
        {
            int numberOfRows = i_GameBoard.NumberOfRows;
            int numberOfColumns = i_GameBoard.NumberOfColumns;
            Console.Write("  ");
            
            for (char column = 'A'; column < 'A' + numberOfColumns; column++)
            {
                Console.Write($"  {column} ");
            }

            Console.WriteLine();
            Console.Write("  ");
            Console.WriteLine(new string('=', numberOfColumns * 4 + 1));
            
            for (int i = 0; i < numberOfRows; i++)
            {
                Console.Write((i + 1) + " |");
                
                for (int j = 0; j < numberOfColumns; j++)
                {
                    if (i_GameBoard.GetCard(i, j).IsCardRevealed)
                    {
                        Console.Write(" " + i_GameBoard.GetCard(i, j).CardSign + " |");
                    }
                    else
                    {
                        Console.Write("   |");
                    }
                }

                Console.WriteLine();
                Console.Write("  ");
                Console.WriteLine(new string('=', numberOfColumns * 4 + 1));
            }
        }
    }
}