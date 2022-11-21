using System;
using Server.Items;

namespace Server.Items
{
	public class Bluekeyfragment : Item
	{
		public override int LabelNumber { get { return 1111646; } }

		[Constructable]
		public Bluekeyfragment() : base(0x1012)
		{
			Movable = false;
			Hue = 0x5D;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("You make a copy of the key in your pack");
			
			BlueKey bluekey = new BlueKey();
			if ( !from.AddToBackpack( bluekey ) )
			bluekey.Delete();
		}

		public Bluekeyfragment( Serial serial ) : base( serial )
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