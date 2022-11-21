using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class PaintBallGunSnipper : PaintBallGunBase
	{
		public override int AosSpeed{ get{ return 5; } }

		public override int OldSpeed{ get{ return 5; } }

		public override int DefMaxRange{ get{ return 15; } }
		public override float MlSpeed { get { return 4.5f; } }//For ML Change Speed Here


		[Constructable]
		public PaintBallGunSnipper()
		{
			Name = "Paint Ball Gun (Sniper)";
		}

		public PaintBallGunSnipper( Serial serial ) : base( serial )
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