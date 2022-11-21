using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.SkillHandlers;
using Server.Engines.Craft;
using Server.Engines.XmlSpawner2;

namespace Server.Gumps
{
    public class MasterRewardsGump : Gump
    {
        public MasterRewardsGump( Mobile from ) : base( 20, 30 )
        {
			from.CloseGump(typeof(MasterRewardsGump));
			
            this.Closable=true;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddBackground(0, 0, 429, 244, 5150);
			AddHtml( 40, 20, 350, 50, @"Choose A Reward Point System To View Available Items", (bool)false, (bool)false);
			AddButton(20, 90, 4005, 4007, 0, GumpButtonType.Reply, 0);
			AddLabel(57, 92, 1067, @"Quest Rewards");
			AddButton(20, 136, 4005, 4007, 1, GumpButtonType.Reply, 0);
			AddLabel(57, 138, 1067, @"PvP Rewards");
			AddButton(20, 182, 4005, 4007, 2, GumpButtonType.Reply, 0);
			AddLabel(57, 184, 1067, @"Faction Rewards");
			AddLabel(298, 123, 1080, @"The Expanse");
			AddLabel(256, 143, 1080, @"Custom Reward Systems");
			AddImage(165, 80, 2376, 1153);
			AddImage(165, 126, 2377, 1153);
			AddImage(165, 172, 2374, 1153);
			AddImage(308, 168, 69, 1080);           
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            switch(info.ButtonID)
            {
                case 0:
				{
					from.SendGump( new QuestRewardGump( from, 0 ) );
					break;
				}
                case 1:
				{
					from.SendGump( new PointsRewardGump( from, 0 ) );
					break;
				}
                case 2:
				{
					from.SendGump( new MobFactionsRewardGump( from, 0 ) );
					break;
				}
            }
        }
    }

	public class MasterCraftGump : Gump
	{
		private Mobile m_From;
		public CraftSystem m_CraftSystem;
		public BaseTool m_Tool;

		private const int LabelHue = 0x480;
		private const int LabelColor = 0x7FFF;
		private const int FontColor = 0xFFFFFF;

		public MasterCraftGump( Mobile from ) : base( 40, 40 )
		{
			m_From = from;

			from.CloseGump( typeof( MasterCraftGump ) );
			from.CloseGump( typeof( CraftGump ) );
			from.CloseGump( typeof( CraftGumpItem ) );

			AddPage( 0 );

			AddBackground( 0, 0, 220, 250, 5054 );
			AddImageTiled( 10, 10, 200, 22, 2624 );		//Section for Gump Heading
			AddImageTiled( 10, 41, 200, 200, 2624 );	//Section for Categories
			AddAlphaRegion( 10, 10, 200, 230 );

			AddHtmlLocalized( 10, 12, 200, 20, 1044010, LabelColor, false, false ); //<CENTER>CATEGORIES</CENTER>

			AddButton( 15, 55, 4005, 4007, 1, GumpButtonType.Reply, 0 );	// Crafting Systems
			AddHtmlLocalized( 50, 58, 150, 18, 1044002, LabelColor, false, false ); //Blacksmithing
			AddButton( 15, 77, 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 50, 80, 150, 18, 1044005, LabelColor, false, false ); //Tailoring
			AddButton( 15, 99, 4005, 4007, 3, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 50, 102, 150, 18, 1044001, LabelColor, false, false ); //Alchemy
			AddButton( 15, 119, 4005, 4007, 4, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 50, 122, 150, 18, 1044004, LabelColor, false, false ); //Carpentry
			AddButton( 15, 139, 4005, 4007, 5, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 50, 142, 150, 18, 1044007, LabelColor, false, false ); //Tinkering
			AddButton( 15, 159, 4005, 4007, 6, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 50, 162, 150, 18, 1079588, LabelColor, false, false ); //Imbuing

			AddButton( 15, 200, 4017, 4019, 0, GumpButtonType.Reply, 0 ); //EXIT BUTTON
			AddHtmlLocalized( 50, 202, 150, 18, 1011441, LabelColor, false, false ); // EXIT
		}

		public override void OnResponse(NetState state, RelayInfo info)
        {
			Mobile m = state.Mobile;
			Container pack = m.Backpack;
			BaseTool tool = (BaseTool)pack.FindItemByType(typeof(BaseTool));

            if ( info.ButtonID <= 0 )
				return; // Canceled

			else if (info.ButtonID == 1)
			{
				CraftSystem system = DefBlacksmithy.CraftSystem;
				m.SendGump( new CraftGump( m, system, tool, null ) );
			}

			else if (info.ButtonID == 2)
			{
				CraftSystem system = DefTailoring.CraftSystem;
				m.SendGump( new CraftGump( m, system, tool, null ) );
			}

			else if (info.ButtonID == 3)
			{
				CraftSystem system = DefAlchemy.CraftSystem;
				m.SendGump( new CraftGump( m, system, tool, null ) );
			}

			else if (info.ButtonID == 4)
			{
				CraftSystem system = DefCarpentry.CraftSystem;
				m.SendGump( new CraftGump( m, system, tool, null ) );
			}

			else if (info.ButtonID == 5)
			{
				CraftSystem system = DefTinkering.CraftSystem;
				m.SendGump( new CraftGump( m, system, tool, null ) );
			}

			else if (info.ButtonID == 6)
			{
				m.SendGump(new ImbuingGump(m));
			}
        }
	}
}