
using System;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 10302, 10303 )]
	public class DarkSushiTray : Food
	{
		[Constructable]
		public DarkSushiTray() : base( 1, Utility.RandomList( 10302, 10303 ) )
		{
			FillFactor = 5;
		}

		public DarkSushiTray( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}