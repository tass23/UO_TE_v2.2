using System;
using Server;

namespace Server.Items
{
	public class CityTrashCan : TrashBarrel
	{
		[Constructable]
		public CityTrashCan() : base()
		{
			Name = "trash barrel";
			Hue = Utility.RandomList( 1150, 1151, 1152, 1153, 1154, 1155, 1156, 1157, 1158, 1159, 1160, 1161 );
			Movable = true;
		}

		public CityTrashCan( Serial serial ) : base( serial )
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