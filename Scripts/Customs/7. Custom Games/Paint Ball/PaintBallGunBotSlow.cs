using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class PaintBallGunBotSlow : PaintBallGunBase
	{
		public override int AosSpeed{ get{ return 15; } }

		public override int OldSpeed{ get{ return 15; } }

		[Constructable]
		public PaintBallGunBotSlow()
		{
			Name = "Paint Ball Gun (Bot Slow)";
		}

		public PaintBallGunBotSlow( Serial serial ) : base( serial )
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