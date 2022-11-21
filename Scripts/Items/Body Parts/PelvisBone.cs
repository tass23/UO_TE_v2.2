using System;
using Server;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x1B1B, 0x1B1C )]
	public class PelvisBone : Item, IScissorable
	{
		[Constructable]
		public PelvisBone() : base( 0x1B1B + Utility.Random( 2 ) )
		{
			Stackable = false;
			Weight = 1.0;
		}

		public PelvisBone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) )
				return false;

			base.ScissorHelper( from, new Bone(), 1 );
			from.PlaySound( 0x21B );

			return false;
		}
	}
}
