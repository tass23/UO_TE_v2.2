using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BlackKnightHelm : NorseHelm
	{
		[Constructable]
		public BlackKnightHelm() : base()
		{
			Name = "The Black Knight's Helm";
			Hue = 1175;
		}

		public BlackKnightHelm( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}