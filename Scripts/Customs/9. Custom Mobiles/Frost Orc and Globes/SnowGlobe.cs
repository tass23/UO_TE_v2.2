using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
	public class SnowGlobe : Item
	{
		public SnowGlobe() : base(0xE2E) 
		{
			Weight = 1.0;
			Hue = Utility.RandomList ( 1154, 1160, 1165, 1168, 1170, 1195 );
			LootType = LootType.Blessed; 
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 2 ) )
				from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				from.CloseGump( typeof( SnowGlobeGump ) );
				from.SendGump( new SnowGlobeGump( from, this ) );
		}
		
		public SnowGlobe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
namespace Server.Gumps
{
	public class SnowGlobeGump : Gump
    {
		private Mobile m_From;
		private SnowGlobe m_Globe;
		
		public SnowGlobeGump(Mobile from, SnowGlobe globe) : base( 0, 0 )
		{
			m_From = from;
			m_Globe = globe;
			
			AddPage(0);
			AddBackground(0, 0, 450, 300, 5054);
			AddImage(49, 96, 11414, 1152);
			AddHtml( 30, 14, 390, 76, "<BASEFONT COLOR=#1C7BFF>You stare into the globe and lose all sense of time. Seconds feel like hours and your eyelids are starting to feel heavy.", (bool)false, (bool)false);
			AddImage(15, 95, 50, 1264);
			AddImage(154, 95, 50, 1264);
			AddImage(293, 95, 50, 1264);
			
			if (Utility.RandomDouble() <= 0.10)
			{
				AddButton(49, 96, 11414, 11414, 1, GumpButtonType.Reply, 0);
				AddImage(49, 96, 11414, 1152);
			}
			if (Utility.RandomDouble() >= 0.05)
			{
				AddButton(49, 96, 11414, 11414, 2, GumpButtonType.Reply, 0);
				AddImage(49, 96, 11414, 1152);
			}
		}
		public override void OnResponse(NetState state, RelayInfo info) 
		{
			Mobile from = state.Mobile;

			switch (info.ButtonID)
			{
				case 0: 
				{
					break;
				}
				case 1: 
				{
					from.SendMessage( "For spending time staring into nothing, you have been given a Reward Scroll." ); 
					from.AddToBackpack( new RewardScroll() );
					break;
				}
				case 2: 
				{
					from.SendMessage( "For spending time staring into nothing, you have frozen stiff and been given a Reward Scroll for your suffering. The effects should wear off in a few seconds..." );
					from.FixedParticles( 0x37C4, 200, 100, 5052, 1264, 0, EffectLayer.LeftFoot );
					from.Freeze(TimeSpan.FromSeconds(5));				
					from.AddToBackpack( new RewardScroll() );
					break;
				}
			}
		}
    }
}