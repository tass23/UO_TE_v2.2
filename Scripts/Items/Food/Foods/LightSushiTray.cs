
using System;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 10304, 10305 )]
	public class LightSushiTray : Food
	{
		[Constructable]
		public LightSushiTray() : base( 1, Utility.RandomList( 10304, 10305 ) )
		{
			FillFactor = 5;
		}

		public LightSushiTray( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}