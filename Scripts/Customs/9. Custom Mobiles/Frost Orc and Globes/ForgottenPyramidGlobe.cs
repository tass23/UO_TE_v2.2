using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class ForgottenPyramidGlobe : SnowGlobe
	{
		[Constructable]
		public ForgottenPyramidGlobe() 
		{
			Name = "a Snowy Scene Of Forgotten Pyramid";
		}

		public ForgottenPyramidGlobe( Serial serial ) : base( serial )
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