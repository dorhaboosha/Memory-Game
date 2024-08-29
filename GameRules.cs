using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static Ex02.Player;

namespace Ex02
{
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

        public static bool ValidSizeBoard(int i_NumberOfRows, int i_NumberOfColumns)
        {
            bool theNumberOfCellsIsEven = (i_NumberOfRows * i_NumberOfColumns) % 2 == 0;
            
            return theNumberOfCellsIsEven;
        }

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