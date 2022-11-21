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
	public class PhysicalResistJewel : BaseSpellCraft
	{
		[Constructable]
		public PhysicalResistJewel() : this( 1 )
		{
		}

		[Constructable]
		public PhysicalResistJewel( int amount ) : base( amount, 6 )
		{
		}

		public PhysicalResistJewel( Serial serial ) : base( serial )
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
