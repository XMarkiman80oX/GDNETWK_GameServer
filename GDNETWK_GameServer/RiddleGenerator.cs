using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDNETWK_GameServer
{

    class RiddleGenerator
    {

        public List<string> promptList;
        public static int promptCount = 0;

        public RiddleGenerator()
        {
            promptList = new List<string>();
            Setup();
        }

        public void AddPrompt(string prompt)
        {
            promptList.Add(prompt);
            promptCount++;

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
                );
        }


    }
}
