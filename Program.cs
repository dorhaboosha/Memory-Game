using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class Program
    {
        public static void Main() 
        {
            GameManager memoryGame = new GameManager();
            
            memoryGame.StartGame();
        }
    }
}