using System;

namespace Server.Items
{
	public class EmptyWoodenBowl : Item
	{
		[Constructable]
		public EmptyWoodenBowl() : base( 0x15F8 ) { }

		public EmptyWoodenBowl( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class EmptyPewterBowl : Item
	{
		[Constructable]
		public EmptyPewterBowl() : base( 0x15FD ) { }

		public EmptyPewterBowl( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}