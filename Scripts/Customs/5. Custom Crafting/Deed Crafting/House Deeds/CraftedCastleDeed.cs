using Server;
using System;
using System.Collections;
using Server.Multis;
using Server.Targeting;
using Server.Items;

namespace Server.Multis.Deeds
{

public class CraftedCastleDeed : HouseDeed
	{
		[Constructable]
		public CraftedCastleDeed() : base( 0x7E, new Point3D( 0, 16, 0 ) )
		{
        Name = "a Crafted Castle Deed";
		}

		public CraftedCastleDeed( Serial serial ) : base( serial )
		{
		}

		public override BaseHouse GetHouse( Mobile owner )
		{
			return new Castle( owner );
		}

		public override int LabelNumber{ get{ return 1041224; } }
		public override Rectangle2D[] Area{ get{ return Castle.AreaArray; } }

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