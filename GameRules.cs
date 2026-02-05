using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static MemoryGame.Player;

namespace MemoryGame
{
    /// <summary>
    /// Provides validation and input-handling utilities for the Memory game.
    /// Handles player names, board dimensions, cell coordinates, and game flow prompts.
    /// </summary>
    public class GameRules
    {
        private static bool nameValidation(StringBuilder i_name)
        {
            bool correctName = true;
            bool nameIsNull = i_name == null;
            bool nameIsEmpty = i_name.Length == 0;
            
            if (nameIsNull || nameIsEmpty)
            {
                correctName = false;
            }

            bool nameLongerThenTewenty = i_name.Length > 20;
            
            if (nameLongerThenTewenty)
            {
                correctName = false;
            }

            for (int i = 0; i < i_name.Length; i++)
            {
                if (!char.IsLetter(i_name[i]))
                {
                    correctName = false;
                }
            }

            return correctName;
        }

        /// <summary>Prompts for and validates a player name (letters only, up to 20 characters, no spaces).</summary>
        /// <returns>The validated player name.</returns>
        public static string GetThePlayerName()
        {
            StringBuilder playerName = new StringBuilder(Console.ReadLine());
            
            while (!nameValidation(playerName))
            {
                Console.WriteLine("The name you wrote is not a valid name, please enter a valid name" +
                    " (name without spaces and up to 20 characters):");
                playerName.Clear();
                playerName.Append(Console.ReadLine());
            }

            return playerName.ToString();
        }

        private static bool playerTypeValidation(StringBuilder i_PlayerType)
        {
            bool correctType = true;
            bool playerTypeIsNull = i_PlayerType == null;
            bool PlayerTypeIsNotInTheLength = i_PlayerType.Length != 1;
            
            if (playerTypeIsNull || PlayerTypeIsNotInTheLength)
            {
                correctType = false;
            }
            else
            {
                bool noCharForComputer = i_PlayerType[0] != 'C';
                bool noCharForPerson = i_PlayerType[0] != 'P';
                
                if (noCharForComputer && noCharForPerson)
                {
                    correctType = false;
                }
            }

            return correctType;
        }

        /// <summary>Prompts for and validates the second player type (C for computer, P for person).</summary>
        /// <returns>The selected player type as <see cref="eWhoPlay"/>.</returns>
        public static eWhoPlay GetTheSecondPlayerType()
        {
            eWhoPlay playerType;
            StringBuilder playerTypeStr = new StringBuilder(Console.ReadLine());
            
            while (!playerTypeValidation(playerTypeStr))
            {
                Console.WriteLine("What you wrote is invalid," +
                    " please write C to play against the computer or P to play against another player:");
                playerTypeStr.Clear();
                playerTypeStr.Append(Console.ReadLine());
            }

            bool playerIsComputer = playerTypeStr.ToString()[0] == 'C';
            
            if (playerIsComputer)
            {
                playerType = eWhoPlay.Computer;
            }
            else
            {
                playerType = eWhoPlay.Person;
            }

            return playerType;
        }

        private static bool numberValidation(StringBuilder i_NumberStr)
        {
            bool checkOfLength = i_NumberStr.Length == 1;
            bool theNumberIsBiggerThenFour = i_NumberStr[0] >= '4';
            bool theNumberIsSmallerThenSix = i_NumberStr[0] <= '6';
            
            return checkOfLength && theNumberIsBiggerThenFour && theNumberIsSmallerThenSix;
        }

        public static int GetNumberOfRowsAndColumns()
        {
            StringBuilder numberStr = new StringBuilder(Console.ReadLine());
            
            while (!numberValidation(numberStr))
            {
                Console.WriteLine("What you wrote is invalid (insert a number between 4 and 6, the number of cells must be even):");
                numberStr.Clear();
                numberStr.Append(Console.ReadLine());
            }

            int number;
            int.TryParse(numberStr.ToString(), out number);

            return number;
        }

        /// <summary>Checks whether the board has an even number of cells (required for paired cards).</summary>
        /// <param name="i_NumberOfRows">Number of rows.</param>
        /// <param name="i_NumberOfColumns">Number of columns.</param>
        /// <returns>True if the total cell count is even; otherwise false.</returns>
        public static bool ValidSizeBoard(int i_NumberOfRows, int i_NumberOfColumns)
        {
            bool theNumberOfCellsIsEven = (i_NumberOfRows * i_NumberOfColumns) % 2 == 0;
            
            return theNumberOfCellsIsEven;
        }

        /// <summary>Checks whether the player's input indicates an exit request (Q).</summary>
        /// <param name="i_PlayrAnswer">The player's input string.</param>
        /// <returns>True if the input is "Q"; otherwise false.</returns>
        public static bool ValidExit(string i_PlayrAnswer)
        {
            bool checkAnswer = true;
            bool playerAnswerIsNull = i_PlayrAnswer == null;
            bool playerAnswerLengthIsNotOne = i_PlayrAnswer.Length != 1;
            
            if (playerAnswerIsNull || playerAnswerLengthIsNotOne)
            {
                checkAnswer = !checkAnswer;
            }
            else 
            {
                bool playerAnswerIsNoExit = i_PlayrAnswer != "Q";
                
                if (playerAnswerIsNoExit)
                {
                    checkAnswer = !checkAnswer;
                }
            }

            return checkAnswer;
        }

        /// <summary>Converts a row character ('1'–'9') to a 0-based row index.</summary>
        /// <param name="i_NumberRowChar">The row character (e.g. '1' for first row).</param>
        /// <returns>The 0-based row index.</returns>
        public static int GetNumberRow(char i_NumberRowChar)
        {
            int numberRow = i_NumberRowChar - '1';
            
            return numberRow;
        }

        public static int GetNumberColumn(char i_NumberColumnChar)
        {
            int numberColumn = i_NumberColumnChar - 'A';

            return numberColumn;
        }

        /// <summary>Validates that a cell input (e.g. "A1") is within the board bounds.</summary>
        /// <param name="i_PlayerAnswer">The player's cell input (column + row).</param>
        /// <param name="i_NumberOfRows">Number of rows on the board.</param>
        /// <param name="i_NumberOfColumns">Number of columns on the board.</param>
        /// <returns>True if the input represents a valid cell; otherwise false.</returns>
        public static bool ValidCard(string i_PlayerAnswer, int i_NumberOfRows, int i_NumberOfColumns)
        {
            bool checkAnswer = true;
            bool playerAnswerIsNull = i_PlayerAnswer == null;
            bool playerAnswerLengthIsNoTwo = i_PlayerAnswer.Length != 2;
           
            if (playerAnswerIsNull || playerAnswerLengthIsNoTwo)
            {
                checkAnswer = !checkAnswer;
            }
            else
            {
                bool playerAnswerIsSmallerThenA = i_PlayerAnswer[0] < 'A';
                bool playerAnswerIsNoInTheRangeOfColumns = i_PlayerAnswer[0] >= 'A' + i_NumberOfColumns;
                bool playerAnswerIsSmallerThenOne = i_PlayerAnswer[1] < '1';
                bool playerAnswerIsNoInTheRangeOfRows = i_PlayerAnswer[1] >= '1' + i_NumberOfRows;
                
                if (playerAnswerIsSmallerThenA || playerAnswerIsNoInTheRangeOfColumns || 
                    playerAnswerIsSmallerThenOne || playerAnswerIsNoInTheRangeOfRows)
                {
                    checkAnswer = !checkAnswer;
                }
            }

            return checkAnswer;
        }

        /// <summary>Prompts for and validates a cell selection or exit (Q) from the player.</summary>
        /// <param name="i_NumberOfRows">Number of rows on the board.</param>
        /// <param name="i_NumberOfColumns">Number of columns on the board.</param>
        /// <returns>A valid cell string (e.g. "A1") or "Q" to exit.</returns>
        public static string GetAnswerFromPlayer(int i_NumberOfRows, int i_NumberOfColumns)
        {
            StringBuilder PlayerAnswer = new StringBuilder(Console.ReadLine());
            
            while (!ValidExit(PlayerAnswer.ToString()) && !ValidCard(PlayerAnswer.ToString(), i_NumberOfRows, i_NumberOfColumns))
            {
                Console.WriteLine("What you wrote is invalid (please write the cell of the card you want to reveal or Q to exit):");
                PlayerAnswer.Clear();
                PlayerAnswer.Append(Console.ReadLine());
            }
            
            return PlayerAnswer.ToString();
        }

        /// <summary>Checks whether two cards have matching signs.</summary>
        /// <param name="i_FirstCard">The first card.</param>
        /// <param name="i_SecondCard">The second card.</param>
        /// <returns>True if the cards match; otherwise false.</returns>
        public static bool MatchCards(MemoryCard i_FirstCard, MemoryCard i_SecondCard)
        {
            bool matchCards = i_FirstCard.CardSign == i_SecondCard.CardSign;
            
            return matchCards;
        }

        private static bool validPlayAgain(string i_PlayerAnswer)
        {
            bool playerAnswerLengthIsOne = i_PlayerAnswer.Length == 1;
            bool playerAnswerIsYesOrNo = i_PlayerAnswer == "Y" || i_PlayerAnswer == "N";
            
            return playerAnswerLengthIsOne && playerAnswerIsYesOrNo;
        }

        /// <summary>Prompts for and validates a play-again choice (Y or N).</summary>
        /// <returns>"Y" to play again or "N" to exit.</returns>
        public static string GetAnswerForAgain()
        {
            StringBuilder PlayerAnswer = new StringBuilder(Console.ReadLine());
            
            while (!validPlayAgain(PlayerAnswer.ToString()))
            {
                Console.WriteLine("What you wrote is invalid (please write Y to play again or N to exit):");
                PlayerAnswer.Clear();
                PlayerAnswer.Append(Console.ReadLine());
            }
            
            return PlayerAnswer.ToString();
        }

        /// <summary>Generates a random valid cell selection for the computer (an unrevealed card).</summary>
        /// <param name="i_BoardGame">The game board.</param>
        /// <param name="i_NumberOfRows">Number of rows on the board.</param>
        /// <param name="i_NumberOfColumns">Number of columns on the board.</param>
        /// <returns>A cell string (e.g. "A1") representing the chosen position.</returns>
        public static string GetAnswerFromComputer(Board i_BoardGame, int i_NumberOfRows, int i_NumberOfColumns)
        {
            Random randomNumber = new Random();
            int numberRow;
            int numberColumn;
            
            do
            {
                numberRow = randomNumber.Next(i_NumberOfRows);
                numberColumn = randomNumber.Next(i_NumberOfColumns);
            }

            while (i_BoardGame.GetCard(numberRow, numberColumn).IsCardRevealed);
            char numberRowChar = (char) ('1' + numberRow);
            char numberColumnChar = (char)('A' + numberColumn);
           
            return numberColumnChar.ToString() + numberRowChar.ToString();
        }
    }
}