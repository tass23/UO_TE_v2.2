using System;
using Server;

namespace Server.Items
{
	public class FetidEssenceStatuette : ShimmeringEffusionStatuette
	{
		public override int LabelNumber{ get{ return 1074504; } } // Fetid Essence Statuette
	
		[Constructable]
		public FetidEssenceStatuette() : base( 0x2D94 )
		{
			Weight = 1.0;			
		}

		public FetidEssenceStatuette( Serial serial ) : base( serial )
		{
		}

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

