using Server;
using System;
using System.Collections;
using Server.Multis;
using Server.Targeting;
using Server.Items;

namespace Server.Multis.Deeds
{

public class CraftedThatchedRoofCottageDeed : HouseDeed
	{
		[Constructable]
		public CraftedThatchedRoofCottageDeed() : base( 0x6E, new Point3D( 0, 4, 0 ) )
		{
          Name = "a Crafted ThatchedRoof Cottage Deed";
		}

		public CraftedThatchedRoofCottageDeed( Serial serial ) : base( serial )
		{
		}

		public override BaseHouse GetHouse( Mobile owner )
		{
			return new SmallOldHouse( owner, 0x6E );
		}

		public override int LabelNumber{ get{ return 1041216; } }
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