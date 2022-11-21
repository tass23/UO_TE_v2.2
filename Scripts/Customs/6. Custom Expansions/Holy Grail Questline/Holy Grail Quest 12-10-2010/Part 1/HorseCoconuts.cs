/*
 * Created by SharpDevelop.
 * User: Shazzy
 * Date: 7/5/2010
 * Time: 9:10 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System; 
using Server; 

namespace Server.Items 
{ 
	public class HorseCoconuts : Item
	{ 
		[Constructable]
		public HorseCoconuts() : base( 0x1725 ) 
		{
			Weight = 1.0; 
			Name = "Coconuts of Clapping"; 
		} 

		public HorseCoconuts( Serial serial ) : base( serial ) 
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
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				return;
			}
			else
			{	
				switch ( Utility.Random( 6 ) )
				{
					default:
					case  0: from.PlaySound( 871 ); break;
					case  1: from.PlaySound( 169 ); break;
					case  2: from.PlaySound( 170 ); break;
					case  3: from.PlaySound( 874 ); break;
					case  4: from.PlaySound( 1213 ); break;
					case  5: from.PlaySound( 1212 ); break;
				}
			}
		}
	} 
}