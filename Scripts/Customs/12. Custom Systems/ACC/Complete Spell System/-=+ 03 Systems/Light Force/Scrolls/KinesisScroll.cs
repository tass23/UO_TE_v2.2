using System;
using Server;
using Server.Items;

namespace Server.ACC.CSS.Systems.LightForce
{
	public class KinesisDisc : CSpellScroll
	{
		[Constructable]
		public KinesisDisc() : base( typeof( KinesisSpell ), 0x01CB )
		{
			Name = "Force Kinesis";
			Hue = 1185;
			Stackable = false;
		}

		public KinesisDisc( Serial serial ) : base( serial )
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
