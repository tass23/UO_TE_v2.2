using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class HolyGrailDeco : BaseBeverage
	{
		public override int MaxQuantity{ get{ return 1; } }

		public override int ComputeItemID()
		{
			if ( ItemID == 0x099A || ItemID == 0x099A || ItemID == 0x099A || ItemID == 0x099A )
				return ItemID;

			return 0x099A;
		}

		[Constructable]
		public HolyGrailDeco( ) : base( 0x099A  )
		{
			Name = "The Holy Grail";
			Hue = 1711;
			Stackable = false;
			Weight = 1.0;
		}

		[Constructable]
		public HolyGrailDeco( BeverageType type ) : base( type )
		{
		}

		public HolyGrailDeco( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}