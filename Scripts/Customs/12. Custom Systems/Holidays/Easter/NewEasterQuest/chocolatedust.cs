////////////////////////
//Created by Deikiell//
//////////////////////
using System;
using Server;

namespace Server.Items
{
	public class chocolatedust : Item
	{
		[Constructable]
		public chocolatedust() : base( 0x26B8 )
		{
			Weight = 1.0;
			Name = "Chocolate Dust";
			Hue = 1863;
		}

		public chocolatedust( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
		
		public override void OnDoubleClick( Mobile from ) 
		{ 
			switch ( Utility.Random( 4 ) )
			{
				default:
				case  0: from.SendMessage( "eeewww!" ); break;
				case  1: from.SendMessage( "it smells like chocolate!" ); break;
				case  2: from.SendMessage( "thats disgusting" ); break;
				case  3: from.SendMessage( "a pile of chewed chocolate" ); break;

			}
		}
	}	
}