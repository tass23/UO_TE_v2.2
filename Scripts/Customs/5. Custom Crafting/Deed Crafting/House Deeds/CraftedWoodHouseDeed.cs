using Server;
using System;
using System.Collections;
using Server.Multis;
using Server.Targeting;
using Server.Items;

namespace Server.Multis.Deeds
{

public class CraftedWoodHouseDeed : HouseDeed
	{
		[Constructable]
		public CraftedWoodHouseDeed() : base( 0x6A, new Point3D( 0, 4, 0 ) )
		{
          Name = "a Crafted WoodHouse Deed";
		}

		public CraftedWoodHouseDeed( Serial serial ) : base( serial )
		{
		}

		public override BaseHouse GetHouse( Mobile owner )
		{
			return new SmallOldHouse( owner, 0x6A );
		}

		public override int LabelNumber{ get{ return 1041214; } }
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