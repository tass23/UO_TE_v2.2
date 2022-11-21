using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class TerathonKeepGlobe : SnowGlobe
	{
		[Constructable]
		public TerathonKeepGlobe() 
		{
			Name = "a Snowy Scene of Terathon Keep"; 
		}

		public TerathonKeepGlobe( Serial serial ) : base( serial )
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