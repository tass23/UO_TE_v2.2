using Server;
using System;
using System.Collections;
using Server.Multis;
using Server.Targeting;
using Server.Items;

namespace Server.Multis.Deeds
{

public class CraftedFieldStoneHouseDeed : HouseDeed
	{
		[Constructable]
		public CraftedFieldStoneHouseDeed() : base( 0x66, new Point3D( 0, 4, 0 ) )
		{
        Name = "a Crafted FieldStone House Deed";
		}

		public CraftedFieldStoneHouseDeed( Serial serial ) : base( serial )
		{
		}

		public override BaseHouse GetHouse( Mobile owner )
		{
			return new SmallOldHouse( owner, 0x66 );
		}

		public override int LabelNumber{ get{ return 1041212; } }
		public override Rectangle2D[] Area{ get{ return SmallOldHouse.AreaArray; } }

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
	}
}