using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class LotusLakeGlobe : SnowGlobe
	{
		[Constructable]
		public LotusLakeGlobe() 
		{
			Name = "a Snowy Scene of Lotus Lake"; 
		}

		public LotusLakeGlobe( Serial serial ) : base( serial )
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