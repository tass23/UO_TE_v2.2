using System;
using Server;

namespace Server.Items
{
	public class MedicalBrownBook : BaseBook
	{

		[Constructable]
		public MedicalBrownBook() : base( 0xFEF )
		{
			Name = "Medical Brown Book";
			Weight = 1.0;
		}

		public MedicalBrownBook( Serial serial ) : base( serial )
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