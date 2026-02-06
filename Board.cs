using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MemoryGame.MemoryCard;

namespace MemoryGame
{
    /// <summary>
    /// Represents the game board for a Memory card game. Manages a grid of paired cards,
    /// their placement, shuffling, and tracking of revealed cards.
    /// </summary>
    public class Board
    {
        private readonly int r_NumberOfRows;
        private readonly int r_NumberOfColumns;
        private MemoryCard[,] m_BoardGame;
        private int m_NumberOfRevealedCards;

        /// <summary>
        /// Initializes a new board with the specified dimensions and creates paired cards.
        /// </summary>
        /// <param name="i_NumberOfRows">Number of rows in the board.</param>
        /// <param name="i_NumberOfColumns">Number of columns in the board.</param>
        public Board(int i_NumberOfRows, int i_NumberOfColumns)
        {
            r_NumberOfRows = i_NumberOfRows;
            r_NumberOfColumns = i_NumberOfColumns;
            m_BoardGame = new MemoryCard[r_NumberOfRows, r_NumberOfColumns];
            m_NumberOfRevealedCards = 0;
            
            intializeBorad();
        }

        /// <summary>Gets the number of rows in the board.</summary>
        public int NumberOfRows
        {
            get 
            {
                return r_NumberOfRows;
            }
        }

        /// <summary>Gets the number of columns in the board.</summary>
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

        /// <summary>Gets the card at the specified position.</summary>
        /// <param name="i_NumberRow">Row index (0-based).</param>
        /// <param name="i_NumberColumn">Column index (0-based).</param>
        /// <returns>The <see cref="MemoryCard"/> at the given coordinates.</returns>
        public MemoryCard GetCard(int i_NumberRow, int i_NumberColumn)
        {
            return m_BoardGame[i_NumberRow, i_NumberColumn];
        }

        /// <summary>Checks whether all cards on the board have been revealed.</summary>
        /// <returns>True if every card is revealed; otherwise false.</returns>
        public bool CheckIfTheBoardComplete()
        {
            return m_NumberOfRevealedCards == r_NumberOfRows * r_NumberOfColumns;
        }

        /// <summary>Reveals (flips) the card at the specified position.</summary>
        /// <param name="i_NumberRow">Row index (0-based).</param>
        /// <param name="i_NumberColumn">Column index (0-based).</param>
        public void OpenCard(int i_NumberRow, int i_NumberColumn)
        {
            MemoryCard card = m_BoardGame[i_NumberRow, i_NumberColumn];
            if (!card.IsCardRevealed)
            {
                card.FlipCard();
                m_BoardGame[i_NumberRow, i_NumberColumn] = card;
                m_NumberOfRevealedCards++;
            }
        }

        /// <summary>Hides (flips back) the card at the specified position if it is currently revealed.</summary>
        /// <param name="i_NumberRow">Row index (0-based).</param>
        /// <param name="i_NumberColumn">Column index (0-based).</param>
        public void CloseCard(int i_NumberRow, int i_NumberColumn)
        {
            MemoryCard card = m_BoardGame[i_NumberRow, i_NumberColumn];
            if (card.IsCardRevealed)
            {
                card.FlipCard();
                m_BoardGame[i_NumberRow, i_NumberColumn] = card;
                m_NumberOfRevealedCards--;
            }
        }
    }
}