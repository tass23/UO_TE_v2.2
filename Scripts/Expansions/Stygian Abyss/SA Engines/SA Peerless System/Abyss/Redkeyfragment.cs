using System;
using Server.Items;

namespace Server.Items
{
	public class Redkeyfragment : Item
	{
		public override int LabelNumber { get { return 1111647; } }

		[Constructable]
		public Redkeyfragment() : base(0x1012)
		{
			Movable = false;
			Hue = 0x8F;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("You make a copy of the key in your pack");
			
			RedKey redkey = new RedKey();
			if ( !from.AddToBackpack( redkey ) )
			redkey.Delete();
		}

		public Redkeyfragment( Serial serial ) : base( serial )
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