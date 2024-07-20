using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDNETWK_GameServer
{

    class RiddleGenerator
    {

<<<<<<< Updated upstream
        public List<KeyValuePair<string, string>> riddleBook;
        public int riddleCount = 0;

        public RiddleGenerator()
        {
            riddleBook = new List<KeyValuePair<string, string>>();
=======
        public List<string> promptList;
        public static int promptCount = 0;

        public RiddleGenerator()
        {
            promptList = new List<string>();
>>>>>>> Stashed changes
            Setup();
        }

        public void AddPrompt(string prompt)
        {
<<<<<<< Updated upstream
            riddleBook.Add(new KeyValuePair<string, string>(riddle, answer));
            riddleCount++;
=======
            promptList.Add(prompt);
            promptCount++;
>>>>>>> Stashed changes

        }

        public void Setup()
        {
            AddPrompt(
                "Need a life hack. If I don't have a can opener, what can I use?"
                );

            AddPrompt(
                "Whats the best equipment for a zombie apocalypse?"
                );

            AddPrompt(
                "Robots sound SUPER sketchy. Imagine what would be the worst part about having a robot takeover..."
                );

<<<<<<< Updated upstream
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
=======
            AddPrompt(
                "Imagine making your own Starbucks drink. What would you call it?"
                );

            AddPrompt(
                "Opinions on democracy... GO!!!"
                );

            AddPrompt(
                "YO GIVE ME UR TAKES! During every full moon, what would you turn into?"
                );

            AddPrompt(
                "How to guaranteed get booted from an NBA game"
                );

            AddPrompt(
                "How did spongebob get a job at the krusty krab"
                );

            AddPrompt(
                "I can't wait to spend the rest of my days doing our favorite thing..."
                );

            AddPrompt(
                "Can't weekends be longer? :("
                );

            AddPrompt(
                "What's your biggest fear?"
                );

            AddPrompt(
                "Where can you find the most chefs?"
                );

            AddPrompt(
                "What has many teeth, but can't bite?"
                );

            AddPrompt(
                "What five-letter word becomes shorter when you add two letters to it?"
                );

            AddPrompt(
                "What's your drunk habit"
                );

            AddPrompt(
                "What's much more crazier than facebook"
                );

            AddPrompt(
                "If you could become one person, dead or alive, for a day, who would it be?"
                );

            AddPrompt(
                "Akward as hell when you see something you weren't supposed to see"
                );

            AddPrompt(
                "Feeling so bored... how's yall days going?"
                );

            AddPrompt(
                "Lebron James or Michael Jordan?"
                );

            AddPrompt(
                "Relationship Status: "
                );

            AddPrompt(
                "The more you take, the more you leave behind. What are they?"
                );

            AddPrompt(
                "What kind of ship has two mates but no captain?"
>>>>>>> Stashed changes
                );
        }


    }
}
