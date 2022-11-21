using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class TheExpanseGlobe : SnowGlobe
	{
		[Constructable]
		public TheExpanseGlobe() 
		{
			Name = "A Snowy Scene of The Expanse"; 
		}

		public TheExpanseGlobe( Serial serial ) : base( serial )
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