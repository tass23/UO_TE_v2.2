using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class EchoFieldsGlobe : SnowGlobe
	{
		[Constructable]
		public EchoFieldsGlobe() 
		{
			Name = "a Snowy Scene Of Echo Fields";
		}

		public EchoFieldsGlobe( Serial serial ) : base( serial )
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