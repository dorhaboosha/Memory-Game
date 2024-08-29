using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
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

    public struct MemoryCard
    {
        private readonly eCardSign r_CardSign;
        private bool m_IsCardRevealed;
        
        public MemoryCard(eCardSign i_CardSign, bool i_IsCardRevealed)
        {
            r_CardSign = i_CardSign;
            m_IsCardRevealed = i_IsCardRevealed;
        }
        
        public eCardSign CardSign
        {
            get
            {
                return r_CardSign;
            }
        }
        
        public bool IsCardRevealed
        {
            get
            {
                return m_IsCardRevealed;
            }
        }
        
        public void FlipCard()
        {
            m_IsCardRevealed = !m_IsCardRevealed;
        }
    }
}