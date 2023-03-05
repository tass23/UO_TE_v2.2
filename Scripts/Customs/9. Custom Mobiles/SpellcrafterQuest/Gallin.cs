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
	[CorpseName ( "the corpse of Gallin" )]
	public class Gallin : Mobile
	{
		//public virtual bool IsInvulnerable ( get ( return true; ) )
		[Constructable]
		public Gallin ()
		{
			Name = "Gallin";
			Title = "the Arch Spell Crafter";
			Body = 400;
			Hue = Utility.RandomSkinHue ();
			CantWalk = true;
			AddItem ( new Shoes () );
			AddItem ( new Robe(1376) );
			AddItem ( new StaffOfPower ());
			int hairHue = 2122;
			
			switch (Utility.Random (1) )
			{
				case 0: AddItem (new LongHair ( hairHue ) ) ;
				break;				
			}
			
			Blessed = true;
		}
		
		public Gallin ( Serial serial) : base ( serial )
		{
		}
		
		public override void GetContextMenuEntries ( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries (from, list );
			list.Add ( new GallinEntry ( from, this ) ) ;
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
		
		public class GallinEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public GallinEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( GallinGump ) ) )
					{
						mobile.SendGump( new GallinGump( mobile ));
					}
				}
			}
		}
		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
         	Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;

			if ( mobile != null)
			{
				if( dropped is RareSCJewel )
         		{
         			if(dropped.Amount!=1)
         			{
						this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Here is your rewared!", mobile.NetState );
         				return false;
         			}

					dropped.Delete();
					mobile.AddToBackpack( new BookOfSpellCrafts() );
					mobile.AddToBackpack( new MagicJewel() );
					mobile.AddToBackpack( new MagicJewel() );
					mobile.AddToBackpack( new TreasureMap( 5, Map.Trammel ) );
					mobile.AddToBackpack( new ParrotItem() );
					mobile.AddToBackpack( new BankCheck(5000) );
					mobile.SendMessage( "Here are some rewards for your effort." );				
					return true;         		
				}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Why would I want that?", mobile.NetState );
     			}
			}
			return false;
		}
	}
}