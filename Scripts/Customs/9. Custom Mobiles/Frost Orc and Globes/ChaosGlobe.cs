using System;
using Server;
using Server.Mobiles;
using Server.Gumps;
namespace Server.Items
{
	public class ChaosGlobe : SnowGlobe
	{
		[Constructable]
		public ChaosGlobe() 
		{
			Name = "a Snowy Scene Of The Chaos Shrine";
		}

		public ChaosGlobe( Serial serial ) : base( serial )
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