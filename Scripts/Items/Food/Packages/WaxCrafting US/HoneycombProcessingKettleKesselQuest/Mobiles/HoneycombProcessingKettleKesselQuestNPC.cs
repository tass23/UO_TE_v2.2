using System;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "Corpse of an old man" )]
	public class HoneycombProcessingKettleQuestNPC : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public HoneycombProcessingKettleQuestNPC()
		{
			Name = "Old Man";
			Body = 400;
			Hue = Utility.RandomSkinHue();

			LeatherArms LeatherArms = new LeatherArms();
			LeatherArms.Hue = 2418;
			AddItem( LeatherArms );
						
			LeatherGloves LeatherGloves = new LeatherGloves();
			LeatherGloves.Hue = 2418;
			AddItem( LeatherGloves );

			LeatherLegs LeatherLegs = new LeatherLegs();
			LeatherLegs.Hue = 2418;
			AddItem( LeatherLegs );
			
			LeatherChest LeatherChest = new LeatherChest();
			LeatherChest.Hue = 2418;
			AddItem( LeatherChest );

			LeatherGorget LeatherGorget = new LeatherGorget();
			LeatherGorget.Hue = 2418;
			AddItem( LeatherGorget );

                        int hairHue = 2406;

			switch ( Utility.Random( 1 ) )
			{
				case 0: AddItem( new PonyTail( hairHue ) ); break;
				case 1: AddItem( new Goatee( hairHue ) ); break;
			} 
			
			Blessed = true;
			
			}



		public HoneycombProcessingKettleQuestNPC( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new HoneycombProcessingKettleQuestNPCEntry( from, this ) ); 
	        } 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public class HoneycombProcessingKettleQuestNPCEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public HoneycombProcessingKettleQuestNPCEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( HoneycombProcessingKettleQuestGump ) ) )
					{
						mobile.SendGump( new HoneycombProcessingKettleQuestGump( mobile ));
												
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
				if( dropped is HoneyComb)
         		{
         			if(dropped.Amount!=5)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I need five honeycombs!!", mobile.NetState );
         				return false;
         			}

					dropped.Delete();
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Thank you! Here, theres your Honeycomb Processing Kettle!", mobile.NetState );
					mobile.AddToBackpack( new HoneycombProcessingKettle() );

				
					return true;
         		}
				else if ( dropped is HoneycombProcessingKettle)
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I dont want it back, you can keep it!", mobile.NetState );
         			return false;
				}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I didnt ask you for this stuff.", mobile.NetState );
     			}
			}
			return false;
		}
	}
}
