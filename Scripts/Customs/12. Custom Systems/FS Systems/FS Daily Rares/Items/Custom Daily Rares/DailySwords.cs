using System;

namespace Server.Items
{
	public class DailySwords : BaseDailyRare
	{
		[Constructable]
		public DailySwords() : base( 0x291F )
		{
			Name = "cobra sword display";
		}

		public DailySwords( Serial serial ) : base( serial )
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