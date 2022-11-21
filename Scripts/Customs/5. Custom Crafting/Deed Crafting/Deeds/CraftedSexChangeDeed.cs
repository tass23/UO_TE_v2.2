using System;
using Server.Mobiles;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server.Gumps;

namespace Server.Items
{
	public class CraftedSexChangeDeed : Item
	{
		[Constructable]
		public CraftedSexChangeDeed() : base( 0x14F0 )
		{
			base.Weight = 1.0;
			base.Name = "a sex change deed";
			base.LootType = LootType.Blessed;
		}
		
		public CraftedSexChangeDeed( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			else
				from.SendGump( new InternalGump( from, this ) );
		}
		
		private class InternalGump : Gump
		{
			private Mobile m_From;
			private CraftedSexChangeDeed m_Deed;
			
			public InternalGump( Mobile from, CraftedSexChangeDeed deed ) : base( 50, 50 )
			{
				m_From = from;
				m_Deed = deed;
				
				from.CloseGump( typeof( InternalGump ) );
				
				this.AddPage(0);
				this.AddBackground(1, 1, 229, 329, 5054);
				this.AddBackground(10, 10, 209, 312, 2620);
				this.AddBackground(65, 45, 146, 25, 3000);
				this.AddBackground(65, 85, 146, 25, 3000);
				this.AddLabel(70, 48, 53, "Change To Male");
				this.AddButton(30, 45, 4005, 4007, 2, GumpButtonType.Reply, 0);
				this.AddLabel(70, 87, 53, "Change To Female");
				this.AddButton(30, 85, 4005, 4007, 1, GumpButtonType.Reply, 0);
				this.AddImage(66, 84, 121);
				this.AddImage(-19, 73, 120);
				
				this.AddImage(100, 115, 113);
			}
			
			public override void OnResponse( NetState sender, RelayInfo info )
			{
				Mobile from = sender.Mobile;
				
				if ( m_Deed.Deleted )
					return;
				
				switch ( info.ButtonID )
				{
						case 0:
						break;
						
					case 1:
						
						Item beard = from.FindItemOnLayer( Layer.FacialHair );
						if ( from.BodyValue == 400 )
						{
							from.BodyValue = 401;
							from.SendMessage ( "You just changed your sex to female!" );
							from.Female = true;
							if ( !(beard == null) )
							{
								beard.Delete();
								from.SendMessage ( "Woman can't have a beard! Yeahp we live in a strange world" );
							}
							m_Deed.Delete();
						}
						break;
						
					case 2:
						
						if ( from.BodyValue == 401 )
						{
							from.BodyValue = 400;
							from.SendMessage ( "You just changed just sex to male!" );
							from.Female = false;
							m_Deed.Delete();
						}
						break;
				}
			}
		}
	}
}


