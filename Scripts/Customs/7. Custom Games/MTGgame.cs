using System;
using Server;
using Server.Network;

namespace Server.Items 
{
	[Flipable( 0x12AB, 0x12AC )]
	public class MTGGame : Item
	{
		private static string GetFortune()
		{
			switch ( Utility.Random( 22 ) )
			{
				default:
				case 0: return "Undead Warchief! 1/1.";
				case 1: return "Skyshroud Sentinel! 1/2.";
				case 2: return "Goblin Warchief! 2/2.";
				case 3: return "Thieving Magpie! 1/3."; 
				case 4: return "Daru Warchief! 1/1.";
				case 5: return "Sliver Overlord! 7/7.";
				case 6: return "Brontotherium! 5/3.";
				case 7: return "Vengeful Dead! 3/2";
				case 8: return "Skirk Volcanist! 3/1";
				case 9: return "Tidal Kraken! 6/6.";
				case 10: return "Dawn Elemental! 3/3.";
				case 11: return "Big Furry Monster! 99/99 Game Over.";
				case 12: return "Elvish Soultiller! 5/4.";
				case 13: return "Air Elemental! 4/4.";
				case 14: return "Skirk Commando! 2/1.";
				case 15: return "Ironfist Crusher! 2/4.";
				case 16: return "Noxious Ghoul! 3/3.";
				case 17: return "Kamahl Fist of Krosa! 4/3.";
				case 18: return "Raven Guild Master! 1/1";
				case 19: return "Flamestick Courier! 2/1.";
				case 20: return "Glimmering Angel! 2/2.";
				case 21: return "Zombie Cutthroat! 3/4.";
			}
		}
		
		[Constructable]
		public MTGGame() : base( 0x12AB )
		{
			Name = "Magic the Gathering deck";
		}
		
		public MTGGame( Serial serial ) : base( serial ) 
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
			switch ( ((Item)this).ItemID )
			{
				case 0x12AB: // Closed north
					if ( Utility.Random( 2 ) == 0 )
						((Item)this).ItemID = 0x12A5;
					else
						((Item)this).ItemID = 0x12A8;
					break;
				case 0x12AC: // Closed east
					if ( Utility.Random( 2 ) == 0 )
						((Item)this).ItemID = 0x12A6;
					else
						((Item)this).ItemID = 0x12A7;
					break;
				case 0x12A5: from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} draws {1}*", from.Name, GetFortune() ) ); break;
				case 0x12A6: from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} draws {1}*", from.Name, GetFortune() ) ); break;
				case 0x12A8: from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} draws {1}*", from.Name, GetFortune() ) ); break;
				case 0x12A7: from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} draws {1}*", from.Name, GetFortune() ) ); break;
			}
		}
		
		public override void OnAdded(object target)
		{
			switch ( ((Item)this).ItemID )
			{
				case 0x12A5: ((Item)this).ItemID = 0x12AB; break; // Open north
				case 0x12A6: ((Item)this).ItemID = 0x12AC; break; // Open east
				case 0x12A8: ((Item)this).ItemID = 0x12AB; break; // Open north
				case 0x12A7: ((Item)this).ItemID = 0x12AC; break; // Open east
			}
		}
	}
	
	[Flipable( 0x12AB, 0x12AC )]
	public class DecoMTGGame : Item
	{
		[Constructable]
		public DecoMTGGame() : base( 0x12AB )
		{
			Name = "tarot deck";
		}
		
		public DecoMTGGame( Serial serial ) : base( serial ) 
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
			switch ( ((Item)this).ItemID )
			{
				case 0x12AB: // Closed north
					if ( Utility.Random( 2 ) == 0 )
						((Item)this).ItemID = 0x12A5;
					else
						((Item)this).ItemID = 0x12A8;
					break;
				case 0x12AC: // Closed east
					if ( Utility.Random( 2 ) == 0 )
						((Item)this).ItemID = 0x12A6;
					else
						((Item)this).ItemID = 0x12A7;
					break;
				case 0x12A5: ((Item)this).ItemID = 0x12AB; break; // Open north
				case 0x12A6: ((Item)this).ItemID = 0x12AC; break; // Open east
				case 0x12A8: ((Item)this).ItemID = 0x12AB; break; // Open north
				case 0x12A7: ((Item)this).ItemID = 0x12AC; break; // Open east
			}
		}
	}
}