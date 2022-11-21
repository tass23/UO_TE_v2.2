using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;

namespace Server.Items
{
	public class DullCopperIngotStone : Item, ISecurable
	{
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}
		
		public override string DefaultName
		{
			get { return "a Dull Copper Ingot stone"; }
		}

		[Constructable]
		public DullCopperIngotStone() : base( 0xED4 )
		{
			Movable = false;
			Hue = 2419;
		}

		public override void OnDoubleClick( Mobile from )
		{
                  // Bag Cost---10000 Gold
		   	Item[] Token = from.Backpack.FindItemsByType( typeof( Gold ) );
		   	if ( from.Backpack.ConsumeTotal( typeof( Gold ), 100000 ) )
		{
         	BagOfDullCopperIngots BagOfDullCopperIngots = new BagOfDullCopperIngots(); // 3 places to change to matching bag name here
		   	from.AddToBackpack( BagOfDullCopperIngots );     // and 1 for the matching bag name here
			from.SendMessage( "100000 gold has been removed from your pack." );
		}
		   	else
		   	{
		   		from.SendMessage( "You do not have enough funds for that." );
		   	}
					
		}

		public DullCopperIngotStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			writer.Write( (int)m_Level );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			if ( version == 1 )
				m_Level = (SecureLevel)reader.ReadInt();
		}
	}
}