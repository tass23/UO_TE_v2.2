using System;
using System.Collections;
using Server.Network;
namespace Server.Items
{
	public class SlicedTurkey : Food
	{
		[Constructable]
		public SlicedTurkey() : this( 1 ) { }
		[Constructable]
		public SlicedTurkey( int amount ) : base( amount, 0x1E1F )
		{
			this.Weight = 0.2;
			this.FillFactor = 3;
			this.Name = "Sliced Turkey";
			this.Hue = 0x457;
			this.Stackable = true;
		}
		public SlicedTurkey( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}