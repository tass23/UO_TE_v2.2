using System;
using Server;

namespace Server.Items
{
	public class DMLegs : Item
	{
		public override double DefaultWeight
		{
			get { return 50; }
		}

		[Constructable]
		public DMLegs() : base( 0x1CC1 )
		{
			Name = "Darth Maul's legs";
		}

		public DMLegs( Serial serial ) : base( serial )
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