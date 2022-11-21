using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
	public class CocoaBean : Food
	{
		[Constructable]
		public CocoaBean() : this( 1 )
		{
		}

		[Constructable]
		public CocoaBean( int amount ) : base( amount, 0x172A )
		{
			Weight = 0.5;
			FillFactor = 1;
			Hue = 0x47A;
			Name = "Cocoa Bean";
		}

		public CocoaBean( Serial serial ) : base( serial )
		{
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

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			from.Target = new InternalTarget( this );
		}

		private class InternalTarget : Target
		{
			private CocoaBean m_Item;

			public InternalTarget( CocoaBean item ) : base( 1, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted ) return;

				else if ( IsFlourMill(targeted) )
				{
					if(m_Item.Amount >= 30)
					{
						m_Item.Consume( 30 );
						from.SendMessage("You made a bag of cocoa.");
						from.AddToBackpack( new BagOfCocoa() );
					}
					else
						from.SendMessage("You don't have enough cocoa beans.");
				}
			}
		}

		public static bool IsFlourMill( object targeted )
		{
			int itemID;

			if ( targeted is Item )
				itemID = ((Item)targeted).ItemID & 0x3FFF;
			else if ( targeted is StaticTarget )
				itemID = ((StaticTarget)targeted).ItemID & 0x3FFF;
			else
				return false;

			if ( itemID >= 0x1883 && itemID <= 0x1893 )
				return true;
			else if ( itemID >= 0x1920 && itemID <= 0x1937 )
				return true;

			return false;
		}
	}
}