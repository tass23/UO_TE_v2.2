using System;
using Server;

namespace Server.Items
{
	public class HoneyComb : Item
	{
		[Constructable]
		public HoneyComb() : this( 1 )
		{
		}

		[Constructable]
		public HoneyComb( int amount ) : base( 0x1762 )
		{
			Name = "HoneyComb";
			Stackable = true;
			Weight = 2.0;
			Amount = amount;
			Hue = 1177;
		}

		public HoneyComb( Serial serial ) : base( serial )
		{
		}

		/*
		public override Item Dupe( int amount )
		{
			return base.Dupe( new HoneyComb( amount ), amount );
		}
		*/
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