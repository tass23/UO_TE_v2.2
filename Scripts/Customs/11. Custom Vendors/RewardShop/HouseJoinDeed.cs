using System;
using Server.Network;
using Server.Prompts;
using Server.Items;

namespace Server.Items
{
	public class HouseJoinDeed : Item
	{
		public override string DefaultName
		{
			get { return "a house join deed"; }
		}

		[Constructable]
		public HouseJoinDeed() : base( 0x14ED )
		{
			base.Weight = 1.0;
			Hue = 1460;
		}

		public HouseJoinDeed( Serial serial ) : base( serial )
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


