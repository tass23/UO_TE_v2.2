using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class BlackKnightStatuette : VesperCollectionStatuette
	{
		[Constructable]
		public BlackKnightStatuette() : base( 0x262B )
		{
			Name = "Black Knight Replica - Victory!";
			Hue = 963;
		}

		public BlackKnightStatuette( Serial serial ) : base( serial )
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