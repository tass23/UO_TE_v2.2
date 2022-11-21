using System;
using Server;

namespace Server.Items
{
	public class MedicalBlueBook : Item
	{

		[Constructable]
		public MedicalBlueBook() : base( 0xFF2 )
		{
			Name = "Medical Blue Book";
			Weight = 1.0;
		}

		public MedicalBlueBook( Serial serial ) : base( serial )
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}
	}
}