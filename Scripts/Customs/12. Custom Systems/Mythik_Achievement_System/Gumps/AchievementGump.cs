using Server.Gumps;
using System.Collections.Generic;
using Server.Network;
using System.Linq;

namespace Scripts.Mythik.Systems.Achievements.Gumps
{
    class AchievementGump : Gump
    {
        private int m_curTotal;
        private Dictionary<int, AchieveData> m_curAchieves;

        public AchievementGump(Dictionary<int, AchieveData> achieves, int total,int category = 1) : base(25, 25)
        {
            m_curAchieves = achieves;
            m_curTotal = total;
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(80, 10, 660, 585, 5150);	//UO-The Expanse Tiled scroll
			this.AddImage(101, 40, 52);	//UO-The Expanse Ribbon
			this.AddImageTiled(320, 133, 5, 375, 2701);	//UO-The Expanse splitter
            //this.AddBackground(57, 92, 666, 478, 9250);
            //this.AddBackground(74, 104, 633, 453, 9250);
            //this.AddBackground(72, 104, 245, 453, 9270);
            //this.AddBackground(72, 34, 635, 53, 9270);
            //this.AddBackground(327, 5, 133, 30, 9200);
            this.AddLabel(150, 38, 2058, @"UO-The Expanse:");
			this.AddLabel(150, 76, 2058, @"an antiquary review");
			this.AddImage(559, 64, 57);	//UO-The Expanse tan arrowhead left
            this.AddLabel(610, 60, 1558, total + @" Points");
			this.AddImage(685, 64, 59);	//UO-The Expanse tan arrowhead right
            //this.AddBackground(341, 522, 353, 26, 9200);	//Page bar

            int cnt = 0;
            for(int i = 0;i < AchievementSystem.Categories.Count; i++)
            {
                int x = 102;
                int bgID = 9350;
                var cat = AchievementSystem.Categories[i];
                var reqCat = AchievementSystem.Categories.Where(c => c.ID == category).FirstOrDefault();

                if (cat.Parent != 0 && cat.ID != reqCat.ID && cat.Parent != reqCat.ID && cat.Parent != reqCat.Parent)
                    continue;
                if(cat.Parent != 0)
                    x += 20;
                if(cat.ID == category)
                    bgID = 9350;	//UO-The Expanse grey
				
				
                this.AddBackground(x, 123 + (cnt * 31), 18810 / x, 25, bgID);
                if (cat.ID == category) // selected
				{
					this.AddBackground(x, 123 + (cnt * 31), 18810 / x, 25, 9300);	//UO-The Expanse tan
					this.AddLabel(x + 32, 125 + (cnt * 31), 2058, cat.Name);
					this.AddImage(x - 31, 123 + (cnt * 31), 31);	//UO-The Expanse Red tab
					this.AddImage(x + 4, 129 + (cnt * 31), 2224);	//UO-The Expanse blue dot with arrow tip
				}
                else
				{
                    this.AddButton(x + 12, 129 + (cnt * 31), 1210, 1209, 5000 + cat.ID, GumpButtonType.Reply, 0);
					this.AddLabel(x + 32, 125 + (cnt * 31), 0, cat.Name);
				}
                cnt++;
            }
            cnt = 0; 
            foreach( var ac in AchievementSystem.Achievements)
            {
                
                if (ac.CategoryID == category)
                {
                    if(ac.PreReq != null)
                    {
                        if (!achieves.ContainsKey(ac.PreReq.ID))
                            continue;
                        if(achieves[ac.PreReq.ID].CompletedOn != null)
                            continue;

                    }
                    if (achieves.ContainsKey(ac.ID))
                    {
                        AddAchieve(ac, cnt, achieves[ac.ID]);
                    }
                    else
                    {
                        if (ac.HiddenTillComplete)
                            continue;
                        AddAchieve(ac, cnt,null);
                    }
                    cnt++;
                }                
            }
        }
        
        private void AddAchieve(BaseAchievement ac, int i, AchieveData achieveData)
        {
            int index = i % 4;
            if(index > 0)
            {
                this.AddButton(680, 540, 4005, 4006, 0, GumpButtonType.Page, (i / 4) + 1);
                AddPage((i / 4) + 1);
                this.AddLabel(525, 550, 1776, "Page " + ((i / 4) + 1));
                this.AddButton(367, 540, 4014, 4015, 0, GumpButtonType.Page, i/4);
				this.AddImageTiled(400, 542, 277, 11, 50);
            }
            int bg = 9350;
            if (achieveData != null && achieveData.CompletedOn != null)
                bg = 9300;
            this.AddBackground(368, 120 + (index * 104), 347, 100, bg);
            this.AddLabel(377, 122 + (index * 104), 2058, ac.Title);
            //if(ac.ItemIcon > 0)
                //this.AddItem(357, 147 + (index * 100), ac.ItemIcon);
			this.AddImageTiled(375, 195 + (index * 104), 100, 22, 10840);	//this.AddImageTiled(348, 208 + (index * 100), 95, 9, 9750);

            var step = 100.0 / ac.CompletionTotal;
            var progress = 0;
            if (achieveData != null && achieveData.CompletedOn != null)
                progress = achieveData.Progress;

            this.AddImageTiled(375, 195 + (index * 104), (int)(progress * step), 22, 10820);	//this.AddImageTiled(348, 208 + (index * 100), (int)(progress * step), 9, 9752);
            this.AddHtml(376, 140 + (index * 104), 272, 55,ac.Desc, (bool)true, (bool)false);
            if (achieveData != null && achieveData.CompletedOn != null)
                this.AddLabel(629, 122 + (index * 104), 1558, achieveData.CompletedOn.ToShortDateString());

            if(ac.CompletionTotal > 1)
                this.AddLabel(522, 196 + (index * 104), 1776, progress + @" / " + ac.CompletionTotal);

            this.AddImage(652, 145 + (index * 104), 51);	//this.AddBackground(632, 149 + (index * 100), 48, 48, 9200);
            this.AddLabel(676, 159 + (index * 104), 1558, ac.RewardPoints.ToString());
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            base.OnResponse(sender, info);
            if (info.ButtonID == 0)
                return;
            var btn = info.ButtonID - 5000;
            if (btn >= 0 && btn < AchievementSystem.Categories.Count)
                sender.Mobile.SendGump(new AchievementGump(m_curAchieves, m_curTotal, btn));
        }
    }
}