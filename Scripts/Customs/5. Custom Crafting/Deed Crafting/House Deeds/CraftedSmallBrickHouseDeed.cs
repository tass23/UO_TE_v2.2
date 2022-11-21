using Server;
using System;
using System.Collections;
using Server.Multis;
using Server.Targeting;
using Server.Items;

namespace Server.Multis.Deeds
{

public class CraftedSmallBrickHouseDeed : HouseDeed
	{
		[Constructable]
		public CraftedSmallBrickHouseDeed() : base( 0x68, new Point3D( 0, 4, 0 ) )
		{
        Name = "a Crafted Small Brick House Deed";
		}

		public CraftedSmallBrickHouseDeed( Serial serial ) : base( serial )
		{
		}

		public override BaseHouse GetHouse( Mobile owner )
		{
			return new SmallOldHouse( owner, 0x68 );
		}

		public override int LabelNumber{ get{ return 1041213; } }
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