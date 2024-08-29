using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex02.MemoryCard;

namespace Ex02
{
    public class Board
    {
        private readonly int r_NumberOfRows;
        private readonly int r_NumberOfColumns;
        private MemoryCard[,] m_BoardGame;
        private int m_NumberOfRevealedCards;

        public Board(int i_NumberOfRows, int i_NumberOfColumns)
        {
            r_NumberOfRows = i_NumberOfRows;
            r_NumberOfColumns = i_NumberOfColumns;
            m_BoardGame = new MemoryCard[r_NumberOfRows, r_NumberOfColumns];
            m_NumberOfRevealedCards = 0;
            
            intializeBorad();
        }

        public int NumberOfRows
        {
            get 
            {
                return r_NumberOfRows;
            }
        }

        public int NumberOfColumns
        {
            get
            {
                return r_NumberOfColumns;
            }
        }

        private void intializeBorad()
        {
            List<MemoryCard> cards = new List<MemoryCard>();
            int numberOfPairCards = (r_NumberOfRows * r_NumberOfColumns) / 2;
            Array cardSigns = Enum.GetValues(typeof(eCardSign));
            
            for (int i = 0; i < numberOfPairCards; i++)
            {
                const bool v_IsCardRevealed = true;

                eCardSign sign = (eCardSign)cardSigns.GetValue(i);
                cards.Add(new MemoryCard(sign, !v_IsCardRevealed));
                cards.Add(new MemoryCard(sign, !v_IsCardRevealed));
            }

            Random randomParameter = new Random();
            shuffleCards(cards, randomParameter);
            placeCardsOnBoard(cards);
        }

        private void shuffleCards(List<MemoryCard> i_Cards, Random i_RandomParameter)
        {
            int numberOfCards = i_Cards.Count;
            bool checkNumberCards = numberOfCards > 1;
            
            while (checkNumberCards)
            {
                numberOfCards--;
                int randomIndex = i_RandomParameter.Next(numberOfCards + 1);
                
                MemoryCard tempCard = i_Cards[randomIndex];
                i_Cards[randomIndex] = i_Cards[numberOfCards];
                i_Cards[numberOfCards] = tempCard;
                checkNumberCards = numberOfCards > 1;
            }
        }

        private void placeCardsOnBoard(List<MemoryCard> i_Cards)
        {
            for(int row = 0; row < r_NumberOfRows; row++)
            {
                for (int column = 0; column < r_NumberOfColumns; column++)
                {
                    m_BoardGame[row, column] = i_Cards[row * r_NumberOfColumns + column];
                }
            }
        }

        public MemoryCard GetCard(int i_NumberRow, int i_NumberColumn)
        {
            return m_BoardGame[i_NumberRow, i_NumberColumn];
        }

        public bool CheckIfTheBoardComplete()
        {
            return m_NumberOfRevealedCards == r_NumberOfRows * r_NumberOfColumns;
        }

        public void OpenCard(int i_NumberRow, int i_NumberColumn)
        {
            m_BoardGame[i_NumberRow, i_NumberColumn].FlipCard();
            m_NumberOfRevealedCards++;
        }

        public void CloseCard(int i_NumberRow, int i_NumberColumn)
        {
            if (m_BoardGame[i_NumberRow, i_NumberColumn].IsCardRevealed)
            {
                m_BoardGame[i_NumberRow, i_NumberColumn].FlipCard();
                m_NumberOfRevealedCards--;
            }
        }
    }
}