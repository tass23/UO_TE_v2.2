using Server;
using System;
using System.Collections;
using Server.Multis;
using Server.Targeting;
using Server.Items;

namespace Server.Multis.Deeds
{

public class CraftedBrickHouseDeed : HouseDeed
	{
		[Constructable]
		public CraftedBrickHouseDeed() : base( 0x74, new Point3D( -1, 7, 0 ) )
		{
        Name = "a Crafted BrickHouse Deed";
		}

		public CraftedBrickHouseDeed( Serial serial ) : base( serial )
		{
		}

		public override BaseHouse GetHouse( Mobile owner )
		{
			return new GuildHouse( owner );
		}

		public override int LabelNumber{ get{ return 1041219; } }
		public override Rectangle2D[] Area{ get{ return GuildHouse.AreaArray; } }

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