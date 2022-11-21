using System;
using Server;

namespace Server.Items
{
	public class MuseumRetrieval : BaseTalisman
	{
		[Constructable]
		public MuseumRetrieval() : base( 0x2F58 )
		{
			Name = "Museum Retrieval Specialist";
			Weight = 1.0;
			Hue = 0x28A;
		}
		
		public MuseumRetrieval( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}