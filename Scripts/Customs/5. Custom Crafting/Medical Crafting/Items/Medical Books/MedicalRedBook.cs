using System;
using Server;

namespace Server.Items
{
	public class MedicalRedBook : BaseBook
	{

		[Constructable]
		public MedicalRedBook() : base( 0xFF1 )
		{
			Name = "Medical Red Book";
			Weight = 1.0;
		}

		public MedicalRedBook( Serial serial ) : base( serial )
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