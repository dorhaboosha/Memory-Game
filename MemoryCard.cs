using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    /// <summary>Represents the letter sign displayed on a memory card (A through R).</summary>
    public enum eCardSign
    {
        A = 'A',
        B = 'B',
        C = 'C',
        D = 'D',
        E = 'E',
        F = 'F',
        G = 'G',
        H = 'H',
        I = 'I',
        J = 'J',
        K = 'K',
        L = 'L',
        M = 'M',
        N = 'N',
        O = 'O',
        P = 'P',
        Q = 'Q',
        R = 'R'
    }

    /// <summary>
    /// Represents a single card in the Memory game. Each card has a sign and can be revealed or hidden.
    /// </summary>
    public struct MemoryCard
    {
        private readonly eCardSign r_CardSign;
        private bool m_IsCardRevealed;
        
        /// <summary>
        /// Initializes a new card with the specified sign and visibility state.
        /// </summary>
        /// <param name="i_CardSign">The letter sign displayed when the card is revealed.</param>
        /// <param name="i_IsCardRevealed">True if the card is face-up; false if face-down.</param>
        public MemoryCard(eCardSign i_CardSign, bool i_IsCardRevealed)
        {
            r_CardSign = i_CardSign;
            m_IsCardRevealed = i_IsCardRevealed;
        }
        
        /// <summary>Gets the letter sign displayed when the card is revealed.</summary>
        public eCardSign CardSign
        {
            get
            {
                return r_CardSign;
            }
        }
        
        /// <summary>Gets whether the card is currently face-up (revealed) or face-down (hidden).</summary>
        public bool IsCardRevealed
        {
            get
            {
                return m_IsCardRevealed;
            }
        }
        
        /// <summary>Toggles the card between revealed and hidden states.</summary>
        public void FlipCard()
        {
            m_IsCardRevealed = !m_IsCardRevealed;
        }
    }
}