using System;
using Server.Network;

namespace Server.Items
{
	public class Prune : Food
	{
		[Constructable]
		public Prune() : this( 1 )
		{
		}

		[Constructable]
		public Prune( int amount ) : base( amount, 0xF2B )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
			this.Hue = 0x205;
			this.Name = "Prune";
			this.Stackable = true;
		}

		public Prune( Serial serial ) : base( serial )
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