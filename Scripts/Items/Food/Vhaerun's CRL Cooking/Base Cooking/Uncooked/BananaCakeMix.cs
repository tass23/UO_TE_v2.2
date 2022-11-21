using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class BananaCakeMix : CookableFood
	{
		public override int LabelNumber{ get{ return 1041002; } }

		[Constructable]
		public BananaCakeMix() : base( 0x103F, 75 )
		{
			Name = "banana cake mix";
			Hue = 354;
		}

		public BananaCakeMix( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override Food Cook()
		{
			return new BananaCake();
		}

	}
}