using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class SerpentsGlobe : SnowGlobe
	{
		[Constructable]
		public SerpentsGlobe() 
		{
			Name = "a Snowy Scene of Serpent's Hold"; 
		}

		public SerpentsGlobe( Serial serial ) : base( serial )
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