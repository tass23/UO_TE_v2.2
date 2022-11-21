using System;
using Server.Items;

namespace Server.Items
{
    public class StaffMedallion : GoldNecklace
	{
		[Constructable]
		public StaffMedallion()
		{
			Hue = 1174;
			Name = "Staff Medallion";
			Attributes.SpellDamage = 10;
			Attributes.LowerManaCost = 7;
			Attributes.LowerRegCost = 15;
			Attributes.BonusMana = 50;
		}

		public StaffMedallion( Serial serial ) : base( serial )
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
	}
}