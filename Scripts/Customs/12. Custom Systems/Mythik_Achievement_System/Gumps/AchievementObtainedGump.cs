using Server.Gumps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Mythik.Systems.Achievements.Gumps
{
    class AchievementObtainedGump : Gump
    {
        private BaseAchievement ach;

        public AchievementObtainedGump(BaseAchievement ach):base(470,389)
        {
            this.ach = ach;

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(31, 16, 363, 145, 5150);	//this.AddBackground(39, 38, 350, 100, 9250);
            this.AddAlphaRegion(48, 46, 332, 82);
            //if(ach.ItemIcon > 0)
                //this.AddItem(54, 53, ach.ItemIcon);
            this.AddLabel(62, 54, 2058, ach.Title);
            this.AddHtml(58, 72, 251, 46, ach.Desc, (bool)true, (bool)true);
            this.AddLabel(242, 54, 1558, @"COMPLETE");
            this.AddImage(314, 73, 51);	//this.AddBackground(320, 72, 44, 47, 9200);
            this.AddLabel(339, 87, 1558, ach.RewardPoints.ToString());
        }
    }
}