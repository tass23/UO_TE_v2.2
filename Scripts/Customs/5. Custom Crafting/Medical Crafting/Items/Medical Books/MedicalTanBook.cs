using System;
using Server;

namespace Server.Items
{
	public class MedicalTanBook : BaseBook
	{
		[Constructable]
		public MedicalTanBook() : base( 0xFF0 )
		{
			Name = "Medical Tan Book";
			Weight = 1.0;
		}

		public MedicalTanBook( Serial serial ) : base( serial )
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