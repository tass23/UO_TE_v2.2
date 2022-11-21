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
	public class ReflectPhysicalJewel : BaseSpellCraft
	{
		[Constructable]
		public ReflectPhysicalJewel() : this( 1 )
		{
		}

		[Constructable]
		public ReflectPhysicalJewel( int amount ) : base( amount, 45 )
		{
		}

		public ReflectPhysicalJewel( Serial serial ) : base( serial )
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
			int version = reader.ReadInt(); // version
		}
	}
}
