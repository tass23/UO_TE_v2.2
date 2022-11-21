using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class JarHoney : Item
	{
		[Constructable]
		public JarHoney() : this(1)
		{
		}

		[Constructable]
		public JarHoney( int amount ) : base( 0x9ec )
		{
			Stackable = true;
			Amount = amount;
		}

		public JarHoney( Serial serial ) : base( serial )
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

	}
}