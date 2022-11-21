using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class FanDancerDojoGlobe : SnowGlobe
	{
		[Constructable]
		public FanDancerDojoGlobe() 
		{
			Name = "a Snowy Scene Of Fan Dancer Dojo";
		}

		public FanDancerDojoGlobe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}