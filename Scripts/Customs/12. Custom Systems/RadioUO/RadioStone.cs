using System; 
using Server.Gumps; 
using Server.Mobiles; 
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;
using Server.ContextMenus;
using Server;
using Server.Misc; 

namespace Server.Items 
{ 
   public class RadioStone : Item, ISecurable
	{
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}
		
		[Constructable] 
		public RadioStone() : base( 3805 ) 
		{ 
			Movable = true; 
			Hue = 1577; 
			Name = "UO-The Expanse Radio stone"; 
		} 

		public override void OnDoubleClick( Mobile from ) 
		{ 
			from.SendGump( new UOTERadioGump( from ));
		} 

		public RadioStone( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version
			writer.Write( (int)m_Level );
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt();
			m_Level = (SecureLevel)reader.ReadInt();
		} 
	}
	public class RadioStoneDeed : Item 
	{
		[Constructable]
		public RadioStoneDeed() : this( 1 )
		{
			ItemID = 5360;
			Movable = true;
			Hue = 1577;
			Name = "UO-The Expanse Radio Stone Deed";
		}
		
		 public override void OnDoubleClick( Mobile from )
      	{
       		from.AddToBackpack( new RadioStone() ); 
       		this.Delete();
        }

		[Constructable]
		public RadioStoneDeed( int amount ) 
        {
		}		

		public RadioStoneDeed( Serial serial ) : base( serial ) 
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