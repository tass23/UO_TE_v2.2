using System;
using Server;

namespace Server.Items
{

	public class GumballStand : Item
	{
		[Constructable]
		public GumballStand() : base( 11736 )
		{
			Name = "Artifact Gumball Machine Stand";
			Hue = 1589;
		}

		public GumballStand( Serial serial ) : base( serial )
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