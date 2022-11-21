using System;
using Server;
using Server.Network;
using Server.Regions;
using Server.Gumps;
using Server.Items;


namespace Server.Items
{
	public class Sheets : Item, IDyable
	{
		[Constructable]
		public Sheets() : this( 0 )
		{
		}

		[Constructable]
		public Sheets( int hue ) : base( Utility.RandomList( 2706, 2707 ) )
		{
			Weight = 1.0;
			Hue = hue;
			Stackable = false;
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public Sheets( Serial serial ) : base( serial )
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
                 