using System;
using Server;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x1B19, 0x1B1A )]
	public class BoneShards : Item, IScissorable
	{
		[Constructable]
		public BoneShards( ) : base( 0x1B19 + Utility.Random( 2 ) )
		{
			Stackable = false;
			Weight = 3.0;
		}

		public BoneShards( Serial serial ) : base( serial )
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

			base.ScissorHelper( from, new Bone(), 3 );
			from.PlaySound( 0x21B );

			return false;
		}
	}
}
