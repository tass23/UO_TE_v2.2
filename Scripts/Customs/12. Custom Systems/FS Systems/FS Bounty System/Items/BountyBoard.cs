using System; 
using Server; 
using Server.Gumps; 
using Server.Mobiles;
using System.Collections;

namespace Server.Items
{
	[FlipableAttribute( 7774, 7775 )]
	public class BountyBoard : Item 
	{
		[Constructable] 
		public BountyBoard() : base( 7774 ) 
		{ 
			Movable = false; 
			Name = "a bounty board";
		} 

		public override void OnDoubleClick( Mobile from ) 
		{
			from.SendGump( new BountyHunterGump( from, 0, null, null ) );
		}

		public BountyBoard( Serial serial ) : base( serial ) 
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