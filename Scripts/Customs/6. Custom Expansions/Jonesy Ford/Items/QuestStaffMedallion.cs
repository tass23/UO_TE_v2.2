using System;
using Server.Items;

namespace Server.Items
{
    public class QuestStaffMedallion : Item
	{

		[Constructable]
		public QuestStaffMedallion()
		{
			Hue = 1174;
			ItemID = 0x1085;
			Name = "Staff Medallion";

		}

		public QuestStaffMedallion( Serial serial ) : base( serial )
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
