using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_the_trolls
{
    class Program
    {
        static void Main(string[] args)
        {
			Game game = new Game();
			game.RunningGame();


			Console.ReadKey();
        }
    }
}

/*
 * TODO Phrase 3
 * Let's add some trolls. Place trolls at random spots and let them navigate to you character. You can avoid the trolls by pushing blocks.
 * The trolls should move a block when you move a block, so it is turnbased.
 * 
 * TODO Phrase 4
 * Generate your own maze
 * */
