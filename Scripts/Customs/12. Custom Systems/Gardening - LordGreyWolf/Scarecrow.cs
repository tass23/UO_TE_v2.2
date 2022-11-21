using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[FlipableAttribute( 7732, 7733 )]
	public class Scarecrow : Item
	{
		[Constructable]
		public Scarecrow() : base( 7732 )
		{
			Name = "a Scarecrow";
			Weight = 30.0;
		}

		public Scarecrow( Serial serial ) : base( serial ) { }
		public override bool IsAccessibleTo( Mobile check )
		{
				return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage(" ");
			from.SendMessage("The scarecrow creakily rotates its wooden neck and glares at you.");
			from.SendMessage(" ");
			from.SendMessage("Straw sifts out of its collar and the crudely painted mouth yawns open to shriek deprecations into the open sky.");
			from.SendMessage(" ");
			from.SendMessage("..... Any crows that might be near by leave with the utmost haste.");
			from.SendMessage(" ");
		}

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}
