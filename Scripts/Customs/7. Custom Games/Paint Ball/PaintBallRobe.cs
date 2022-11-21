using System;
using Server.Items;

namespace Server.Items
{
	public class PaintBallRobe : BaseOuterTorso
	{
		[Constructable]
		public PaintBallRobe() : this( 6 )
		{
		}

		[Constructable]
		public PaintBallRobe( int hue ) : base(7939, hue )
		{
			Name = "Undead Robe";
			Weight = 5.0;
		}

		public PaintBallRobe( Serial serial ) : base( serial )
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