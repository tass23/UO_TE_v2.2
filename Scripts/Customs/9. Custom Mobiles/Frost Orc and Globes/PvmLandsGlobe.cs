using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class PvmLandsGlobe : SnowGlobe
	{
		[Constructable]
		public PvmLandsGlobe() 
		{
			Weight = 1.0;
			Name = "a Snowy Scene of the Spider Wing in Death Maw";
		}

		public PvmLandsGlobe( Serial serial ) : base( serial )
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