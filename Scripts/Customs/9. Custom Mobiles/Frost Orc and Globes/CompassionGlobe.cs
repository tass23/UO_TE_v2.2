using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class CompassionGlobe : SnowGlobe
	{
		[Constructable]
		public CompassionGlobe() 
		{
			Name = "a Snowy Scene Of The Shrine of Compassion";
		}

		public CompassionGlobe( Serial serial ) : base( serial )
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