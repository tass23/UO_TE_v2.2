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
	public class SpellCraftJewel : BaseSpellCraft
	{
		[Constructable]
		public SpellCraftJewel() : this( 1, 0 )
		{
		}

		[Constructable]
		public SpellCraftJewel( int craft ) : this( 1, craft )
		{
		}

		[Constructable]
		public SpellCraftJewel( int amount, int craft ) : base( amount, craft )
		{
		}

		public SpellCraftJewel( Serial serial ) : base( serial )
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