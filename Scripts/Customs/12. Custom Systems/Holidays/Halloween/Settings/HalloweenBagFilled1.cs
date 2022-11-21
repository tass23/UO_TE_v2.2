// Original Author Unknown
// Updated to be halloween 2007 by GreyWolf

using System;
using Server;
using Server.Items;

namespace Server.Items
{  
	public class HalloweenBagFilled1 : Bag
	{
		[Constructable]
		public HalloweenBagFilled1()
		{
			Name = "Have A Spooky Halloween insert_year";
			Hue = 1258;

			DropItem (new HalloweenLantern() );

			switch ( Utility.Random( 4 ) )
			{      	
				case 0: DropItem(new HalloweenCloak());
				break;

				case 1: DropItem(new HalloweenTunic());
				break;

				case 2: DropItem(new HalloweenDoublet());
				break;

				case 3: DropItem(new HalloweenBoots());
				break;
			}

			if ( 0.1 > Utility.RandomDouble() )
			{
				DropItem( new HalloweenOuiJaBoard() );
			}

		}

		[Constructable]
		public HalloweenBagFilled1(int amount)
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( "Halloween insert_year" );
		}

		public HalloweenBagFilled1(Serial serial) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version 
		}

		public override void Deserialize(GenericReader reader)
      	{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
