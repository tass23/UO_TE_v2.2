using System;
using Server.Mobiles;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server.Gumps;

namespace Server.Items
{
	public class SailboatMembershipcard : Item
	{
		[Constructable]
		public SailboatMembershipcard() : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 1265;
			Name = "Sailboat Membership Card";
		}

		public SailboatMembershipcard( Serial serial ) : base( serial )
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