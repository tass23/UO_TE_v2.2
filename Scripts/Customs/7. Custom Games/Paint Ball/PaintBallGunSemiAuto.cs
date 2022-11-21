using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class PaintBallGunSemiAuto : PaintBallGunBase
	{
		public override int AosSpeed{ get{ return 45; } }

		public override int OldSpeed{ get{ return 45; } }

		public override int DefMaxRange{ get{ return 6; } }
		public override float MlSpeed { get { return 4.5f; } }//For ML Change Speed Here


		[Constructable]
		public PaintBallGunSemiAuto()
		{
			Name = "Paint Ball Gun (SemiAuto)";
		}

		public PaintBallGunSemiAuto( Serial serial ) : base( serial )
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