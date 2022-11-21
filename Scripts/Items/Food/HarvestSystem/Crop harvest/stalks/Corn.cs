using System;
using Server.Items;
using System.Collections;
using Server.Mobiles;

namespace Server.Items
{
	public class Corn : Item
	{
		[Constructable]
		public Corn() : this( 1 ){}

		[Constructable]
		public Corn( int amount ) : base( 0x0C81 )
		{
			Name = "An Ear of Corn";
			Stackable = true;
			Weight = 4.0;
			Amount = amount;
		}

		public override void OnDoubleClick( Mobile from )
		{
			ArrayList list = new ArrayList();

			foreach ( Item m in from.GetItemsInRange( 2 ) )
			{
				if ( m is FlourMillEastAddon ) list.Add( m );
				else if ( m is FlourMillSouthAddon ) list.Add( m );
				else if ( m.ItemID == 6434 ) list.Add( m );
			}

			if( IsChildOf( from.Backpack ) && Amount >= 4 && list.Count <= 0 )
			{
				from.SendLocalizedMessage( 1044491 );
			}

			else if( IsChildOf( from.Backpack ) && Amount >= 4 )
			{
				from.SendMessage( "You got a sack of cornflour." );
				from.AddToBackpack( new SackcornFlour() );
				Consume( 4 );
			}

			else if( IsChildOf( from.Backpack ) && Amount < 4 )
			{
				from.SendMessage( "You need more ears of corn." );
			}
			else from.SendLocalizedMessage( 1042001 );
		}

		public Corn( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}