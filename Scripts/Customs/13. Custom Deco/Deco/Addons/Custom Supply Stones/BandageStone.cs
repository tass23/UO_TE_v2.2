using System;
using Server.Items;

namespace Server.Items
{
	public class BandageStone : Item
	{
		public override string DefaultName
		{
			get { return "a bandage stone"; }
		}

		[Constructable]
		public BandageStone() : base( 0xED4 )
		{
			Movable = false;
			Hue = 0x21;
		}

		public override void OnDoubleClick( Mobile from )
		{
                  // Bag Cost---2000 Gold
		   	Item[] Token = from.Backpack.FindItemsByType( typeof( Gold ) );
		   	if ( from.Backpack.ConsumeTotal( typeof( Gold ), 2000 ) )
		{
         	BagOfBandages BagOfBandages = new BagOfBandages(); // 3 places to change to matching bag name here
		   	from.AddToBackpack( BagOfBandages );     // and 1 for the matching bag name here
			from.SendMessage( "2000 gold has been removed from your pack." );
		}
		   	else
		   	{
		   		from.SendMessage( "You do not have enough funds for that." );
		   	}
					
		}

		public BandageStone( Serial serial ) : base( serial )
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