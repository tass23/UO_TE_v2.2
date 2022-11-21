using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class CastleBritanniaGlobe : SnowGlobe
	{
		[Constructable]
		public CastleBritanniaGlobe()
		{
			Name = "a Snowy Scene Of Castle Britannia";
		}

		public CastleBritanniaGlobe( Serial serial ) : base( serial )
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