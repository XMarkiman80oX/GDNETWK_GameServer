using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDNETWK_GameServer
{

    class RiddleGenerator
    {

        public List<KeyValuePair<string, string>> riddleBook;
        public int riddleCount = 0;

        public RiddleGenerator()
        {
            riddleBook = new List<KeyValuePair<string, string>>();
            Setup();
        }

        public void AddRiddle(string riddle, string answer)
        {
            riddleBook.Add(new KeyValuePair<string, string>(riddle, answer));
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

            AddRiddle(
                "What has to be broken before you can use it?",
                "Egg"
                );

            AddRiddle(
                "I'm tall when I'm young, and I'm short when I'm old. What am I?",
                "Candle"
                );

            AddRiddle(
                "What is full of holes but still holds water?",
                "Sponge"
                );

            AddRiddle(
                "What can you break, even if you never pick it up or touch it?",
                "Promise"
                );

            AddRiddle(
                "The more of this there is, the less you see. What is it?",
                "Darkness"
                );

            AddRiddle(
                "What is always in front of you but can't be seen?",
                "Future"
                );

            AddRiddle(
                "Where does today come before yesterday?",
                "Dictionary"
                );

            AddRiddle(
                "What has one eye, but can't see?",
                "Needle"
                );

            AddRiddle(
                "What has hands, but can't clap?",
                "Clock"
                );

            AddRiddle(
                "What has many teeth, but can't bite?",
                "Comb"
                );

            AddRiddle(
                "What five-letter word becomes shorter when you add two letters to it?",
                "Short"
                );

            AddRiddle(
                "What begins with an 'e' and only contains one letter?",
                "Envelope"
                );

            AddRiddle(
                "A word I know, six letters it contains, remove one letter and 12 remains. What is it?",
                "Dozens"
                );

            AddRiddle(
                "Forward I am heavy, but backward I am not. What am I?",
                "Not"
                );

            AddRiddle(
                "What is 3/7 chicken, 2/3 cat and 2/4 goat?",
                "Chicago"
                );

            AddRiddle(
                "What word is pronounced the same if you take away four of its five letters?",
                "Queue"
                );

            AddRiddle(
                "What is so fragile that saying its name breaks it?",
                "Silence"
                );

            AddRiddle(
                "If you drop me I'm sure to crack, but give me a smile and I'll always smile back. What am I?",
                "Mirror"
                );

            AddRiddle(
                "The more you take, the more you leave behind. What are they?",
                "Footsteps"
                );

            AddRiddle(
                "What kind of ship has two mates but no captain?",
                "Relationship"
                );
        }


    }
}
