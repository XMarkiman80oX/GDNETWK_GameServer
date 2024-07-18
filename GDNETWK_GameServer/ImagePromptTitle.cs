using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDNETWK_GameServer
{
    class ImagePromptTitle
    {
        List<string[]> ImagePromptTitleList;
        public int imagePromptTitleCount = 0;
        public ImagePromptTitle()
        {
            ImagePromptTitleList = new List<string[]>();
            Setup();
        }

        public void AddImagePromptTitle(string newImagePromptTitle1, string newImagePromptTitle2, string newImagePromptTitle3, string newImagePromptTitle4, string newImagePromptTitle5)
        {
            string[] newImagePromptTitle = new string[5]{ newImagePromptTitle1, newImagePromptTitle2, newImagePromptTitle3, newImagePromptTitle4, newImagePromptTitle5 };
            ImagePromptTitleList.Add(newImagePromptTitle);
            imagePromptTitleCount++;
        }

        public void Setup()
        {   
            AddImagePromptTitle("Boxer Patrick", "Patrick Boxer", "Patrick Boxing", "Patrick Fighting", "Patrick beating up people");
            AddImagePromptTitle("Danny Phantom Super Saiyan", "Super Saiyan Danny Phantom", "Danny Phantom Super Saiyan Mode", "Super Saiyan Mode Danny Phantom", "Danny Phantom Dragon Ball");
        }
    }
}
