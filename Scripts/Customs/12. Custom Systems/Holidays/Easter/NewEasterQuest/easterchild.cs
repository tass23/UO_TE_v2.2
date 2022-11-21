using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a child corpse!" )]
	public class easterchild : BaseCreature
	{
        public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public easterchild() : base(AIType.AI_Thief, FightMode.None, 10, 1, 0.4, 1.6 )
		{
			Name = "A Child";
			Body = 401;
            Hue = 1102;
			Female = true;
			CantWalk = false;
			
			Container pack = new Backpack();
			pack.Movable = false;
			AddItem( pack );

			Item PlainDress = new PlainDress();
			PlainDress.Hue = 1166;
			PlainDress.Movable = false;
			AddItem( PlainDress );
			
			Item Shoes = new Shoes();
			Shoes.Hue = 1166;
			Shoes.Movable = false;
			AddItem( Shoes );

            Item WideBrimHat = new WideBrimHat();
			WideBrimHat.Hue = 1166;
			WideBrimHat.Movable = false;
			AddItem( WideBrimHat );
			
			Item hair = new Item( 0x2046 );
			hair.Hue = 1519;
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );			

			Blessed = true;
			
		}

		public easterchild( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
	    { 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new easterchildEntry( from, this ) ); 
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

		public class easterchildEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public easterchildEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( easterchildquestGump ) ) )
					{
						mobile.SendGump( new easterchildquestGump( mobile ));
												
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
				if( dropped is eggeaster)
         		{
         			if(dropped.Amount!=1)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "That is not the item I asked for.", mobile.NetState );
         				return false;
         			}

					dropped.Delete(); 
					mobile.AddToBackpack( new chocolatedust() );

				
					return true;
         		}
				else if ( dropped is eggeaster)
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I did not ask for this item.", mobile.NetState );
     			}
			}
			return false;
		}
	}
}
