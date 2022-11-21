using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class DirtyPlate : Item
	{
		[Constructable]
		public DirtyPlate() : base( 0x9AE )
		{
			this.Weight = 0.1;
		}

		public DirtyPlate( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable ) return;
			from.Target = new InternalTarget( this );
		}

		private class InternalTarget : Target
		{
			private DirtyPlate m_Item;

			public InternalTarget( DirtyPlate item ) : base( 1, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted ) return;

				if ( IsWaterSource(targeted) )
				{
					m_Item.Delete();
					from.PlaySound( 0x240 );
					from.AddToBackpack( new FoodPlate() );
					from.SendMessage("You washed the plate");
				}
			}

			public static bool IsWaterSource( object targeted )
			{
				int itemID;

				if ( targeted is Item ) itemID = ((Item)targeted).ItemID & 0x3FFF;
				else if ( targeted is StaticTarget ) itemID = ((StaticTarget)targeted).ItemID & 0x3FFF;
				else return false;
				if ( itemID >= 0xB41 && itemID <= 0xB44 ) return true;
				else if ( itemID == 0xE7B ) return true;
				else if ( itemID == 0x154D) return true;
				return false;
			}
		}
	}
}