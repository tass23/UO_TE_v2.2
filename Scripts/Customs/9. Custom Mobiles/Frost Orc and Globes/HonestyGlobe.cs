using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class HonestyGlobe : SnowGlobe
	{
		[Constructable]
		public HonestyGlobe() 
		{
			Name = "a Snowy Scene Of the Shrine of Honesty";
		}

		public HonestyGlobe( Serial serial ) : base( serial )
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