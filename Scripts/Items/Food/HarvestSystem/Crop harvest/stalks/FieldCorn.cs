using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class FieldCorn : Item
	{
		[Constructable]
		public FieldCorn() : this( 1 )
		{
		}

		[Constructable]
		public FieldCorn( int amount ) : base( 0xC81 )
		{
			Name = "Field Corn";
			Amount = amount;
			Weight = 3.0;
			Hue = 0x1C5;
			Stackable = true;
		}

		public FieldCorn( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable ) return;
			from.Target = new InternalTarget( this );
		}

		private class InternalTarget : Target
		{
			private FieldCorn m_Item;

			public InternalTarget( FieldCorn item ) : base( 1, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted ) return;

				else if ( IsFlourMill(targeted) )
				{
					if(m_Item.Amount >= 10)
					{
						m_Item.Consume( 10 );
						from.SendMessage("You made a bag of cornmeal");
						from.AddToBackpack( new BagOfCornmeal() );
					}
					else
						from.SendMessage("You don't have enough field corn.");
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