using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.SpellCrafting.Items;

namespace Server.Mobiles
{
	[CorpseName ( "Corpse of Balto ") ]
	public class Balto : Mobile
	{
		[Constructable]
		public Balto ()
		{
			Name = "Balto";
			Title = "Apprentice spell crafter";
			Body = 400;
			Hue = Utility.RandomSkinHue ();
			
			AddItem ( new Shoes ());
			AddItem ( new Robe (1160) );
			AddItem ( new GnarledStaff ());
			AddItem ( new WizardsHat ());
			
			Blessed = true;
			
			
		}
		public Balto ( Serial serial) : base ( serial )
		
		{
		}
		
		public override void GetContextMenuEntries ( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries (from, list);
			list.Add ( new BaltoEntry ( from, this ));
			
		}
		public override void Serialize ( GenericWriter writer )
		{
			base.Serialize (writer);
			writer.Write ( (int) 0);
		}
		public override void Deserialize ( GenericReader reader)
		{
			base.Deserialize ( reader);
			int version = reader.ReadInt();
		}
		
		public class BaltoEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public BaltoEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
				

                          if( !( m_Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;

				{
					if ( ! mobile.HasGump( typeof( BaltoGump ) ) )
					{
						mobile.SendGump( new BaltoGump( mobile ));
					}
				}
		
			}
		}
	}
}

