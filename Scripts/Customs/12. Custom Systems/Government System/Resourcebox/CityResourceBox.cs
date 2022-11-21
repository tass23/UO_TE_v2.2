using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0xE41, 0xE40 )]
	public class CityResourceBox : ResourceBox
	{
		private CityManagementStone m_stone;
		
		[CommandProperty( AccessLevel.GameMaster)]
		public CityManagementStone Stone
		{
			get { return m_stone; }
			set { m_stone = value; }
		}
		
		[Constructable]
		public CityResourceBox() : base()
		{
			Movable = true;
			Weight = 100.0;
			Hue = 0x21;
			Name = "Town Resource Box";
		}
		
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( m_stone == null )
			{
				from.SendMessage( "There was an error, please contact a GM " );
				return false;
			}
			else
			{
				if ( PlayerGovernmentSystem.IsMemberOf( from, m_stone ) )
					return base.OnDragDrop( from, dropped );
				else
				{
					from.SendMessage( "Only citizens of the town may access this" );
					return false;
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_stone == null )
				from.SendMessage( "There was an error, please contact a GM " );
			else
			{
			
				if ( PlayerGovernmentSystem.IsMemberOf( from, m_stone ) )
					 base.OnDoubleClick( from );
				else
				{
					from.SendMessage( "Only citizens of the town may access this" );
					
				}
			}
		}
		
		public CityResourceBox( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
			
			writer.Write( (Item)m_stone );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			m_stone = (CityManagementStone)reader.ReadItem();
		}
	}
}