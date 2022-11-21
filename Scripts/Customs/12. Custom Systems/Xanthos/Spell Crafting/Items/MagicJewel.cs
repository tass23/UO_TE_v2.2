#region AuthorHeader
//
//	SpellCrafting version 3.0, by Xanthos and TheOutkastDev
//
//  Based on original ideas and code by TheOutkastDev
//
#endregion AuthorHeader
using System;
using Server;

namespace Server.SpellCrafting.Items
{
	public class MagicJewel : Item
	{
		[Constructable]
		public MagicJewel() : this( 1 )
		{
		}

		[Constructable]
		public MagicJewel( int amount ) : base( 0xF13 )
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
			Hue = 1159;
			Name = "Magic Jewel";
		}

		public MagicJewel( Serial serial ) : base( serial )
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