using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public enum eWhoPlay
    {
        Person,
        Computer
    }

    public struct Player
    {
        private readonly string r_Name;
        private int m_Score;
        private readonly eWhoPlay r_WhoPlay;

        public Player(string i_name, eWhoPlay i_PlayerType)
        {
            r_Name = i_name;
            m_Score = 0;
            r_WhoPlay = i_PlayerType;
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public string WhoPlay
        {
            get
            {
                return r_WhoPlay.ToString();
            }
        }
    }
}