using System;

namespace Server.Items
{
	public class DailyMeatPie : BaseDailyRareFood
	{
		[Constructable]
		public DailyMeatPie() : base( 0x1041 )
		{
			Name = "tasty meat pie";
			FillFactor = 5;
			Stackable = false;
		}

		public DailyMeatPie( Serial serial ) : base( serial )
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