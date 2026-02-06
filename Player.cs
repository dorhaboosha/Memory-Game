using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    /// <summary>Indicates whether a player is human or computer-controlled.</summary>
    public enum eWhoPlay
    {
        Person,
        Computer
    }

    /// <summary>
    /// Represents a player in the Memory game. Holds the player's name, score, and type (human or computer).
    /// </summary>
    public struct Player
    {
        private readonly string r_Name;
        private int m_Score;
        private readonly eWhoPlay r_WhoPlay;

        /// <summary>
        /// Initializes a new player with the specified name and type.
        /// </summary>
        /// <param name="i_name">The player's display name.</param>
        /// <param name="i_PlayerType">Whether the player is human or computer.</param>
        public Player(string i_name, eWhoPlay i_PlayerType)
        {
            r_Name = i_name;
            m_Score = 0;
            r_WhoPlay = i_PlayerType;
        }

        /// <summary>Gets or sets the player's score (number of matched pairs).</summary>
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

        /// <summary>Gets the player's display name.</summary>
        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        /// <summary>Gets the player type as a string ("Person" or "Computer").</summary>
        public string WhoPlay
        {
            get
            {
                return r_WhoPlay.ToString();
            }
        }
    }
}