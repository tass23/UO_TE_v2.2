//################################################################
//By Rocko Wang
//www.medievaldream.com
//Thanks to "The world of dreams" and Drocket for the idea of this
//################################################################


using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using Server.Prompts;
using Server.Gumps;
using Server.Network;
using Server.Game.Arena;


namespace Server.Gumps
{
    public class ArenaScoreBoardGump : Gump
    {
        ArenaControlStone m_Arena;

        public ArenaScoreBoardGump(ArenaControlStone arena)
            : base(55, 65)
        {
            m_Arena = arena;

            Closable = true; Disposable = true; Dragable = true; Resizable = false;
            AddPage(0);
            AddBackground(0, 0, 340, 240, 9200);
            AddLabel(40, 10, 152, "Arena Score Board (Top 5)");

            if (m_Arena == null || m_Arena.Deleted || !m_Arena.Running)
            {
                AddLabel(60, 45, 400, "The Arena is under maintainance.");
                AddButton(280, 210, 2143, 2142, 2, GumpButtonType.Reply, 0);
                return;
            }

            int y = 45;
            AddLabel(60, y, 0, "Player Name");
            AddLabel(190, y, 0, "Score");

            Hashtable top5 = new Hashtable();
            for (int i = 0; i < Math.Min(5, m_Arena.ScoreTable.Count); i++)
            {
                y += 25;
                Mobile player=null;
                int score = -100000;
                foreach (DictionaryEntry de in m_Arena.ScoreTable)
                {
                    Mobile mob = (Mobile)de.Key;
                    if (top5.ContainsKey(mob))
                        continue;
                    if ((int)de.Value > score)
                    {
                        score = (int)de.Value;
                        player = mob;
                    }
                }
                top5.Add(player, score);

                int hue = 0x480;
                if (i == 0) hue = 1161;
                AddLabel(60, y, hue, player.Name);
                AddLabel(190, y, hue, score.ToString());
            }

            AddButton(280, 210, 2143, 2142, 2, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            switch (info.ButtonID)
            {
                default: { break; }
                case 1: { break; }
            }
        }
    }
}