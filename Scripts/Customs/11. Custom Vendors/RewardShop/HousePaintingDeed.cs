using System;
using Server.Network;
using Server.Prompts;
using Server.Items;

namespace Server.Items
{
	public class HousePaintingDeed : Item
	{
		public override string DefaultName
		{
			get { return "a house painting deed"; }
		}

		[Constructable]
		public HousePaintingDeed() : base( 0x14ED )
		{
			base.Weight = 1.0;
			Hue = 1772;
		}

		public HousePaintingDeed( Serial serial ) : base( serial )
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


