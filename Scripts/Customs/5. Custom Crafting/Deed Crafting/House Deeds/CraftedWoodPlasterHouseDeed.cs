using Server;
using System;
using System.Collections;
using Server.Multis;
using Server.Targeting;
using Server.Items;

namespace Server.Multis.Deeds
{

public class CraftedWoodPlasterHouseDeed : HouseDeed
	{
		[Constructable]
		public CraftedWoodPlasterHouseDeed() : base( 0x6C, new Point3D( 0, 4, 0 ) )
		{
          Name = "a Crafted WoodPlaster House Deed";
		}

		public CraftedWoodPlasterHouseDeed( Serial serial ) : base( serial )
		{
		}

		public override BaseHouse GetHouse( Mobile owner )
		{
			return new SmallOldHouse( owner, 0x6C );
		}

		public override int LabelNumber{ get{ return 1041215; } }
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