using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class PaintBallGunBotMed : PaintBallGunBase
	{
		public override int AosSpeed{ get{ return 20; } }

		public override int OldSpeed{ get{ return 20; } }

		[Constructable]
		public PaintBallGunBotMed()
		{
			Name = "Paint Ball Gun (Bot Med)";
		}

		public PaintBallGunBotMed( Serial serial ) : base( serial )
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