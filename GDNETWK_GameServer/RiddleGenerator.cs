using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDNETWK_GameServer
{

    class RiddleGenerator
    {
        public static Dictionary<string, string> riddleBook;
        public int riddleCount = 0;

        public RiddleGenerator()
        {
            Setup();
        }

        public void AddRiddle(string riddle, string answer)
        {
            riddleBook.Add(riddle, answer);
            riddleCount++;
        }

        public void Setup()
        {
            AddRiddle(
                "David's father has three sons: Snap, Crackle, and _____?",
                "David"
                );

            AddRiddle(
                "What gets bigger the more you take away",
                "Hole"
                );

            AddRiddle(
                "What has a head and a tail, but no body?",
                "Coin"
                );

        }


    }
}
