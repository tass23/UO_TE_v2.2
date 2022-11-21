using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class UncookedFrenchBread : CookableFood
	{
		[Constructable]
		public UncookedFrenchBread() : base( 0x98C, 0 )
		{
			Hue = 51;
			Name = "uncooked french bread";
		}

		public UncookedFrenchBread( Serial serial ) : base( serial )
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
			return new FrenchBread();
		}
	}
}