using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Multis;
using Server.ContextMenus;

namespace Server.Items
{
	public class YardStair : Item
	{
		public Mobile m_Placer;
		public int m_Value = 0;
		public int ID1, ID2, ID3, ID4;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Placer
		{
			get{ return m_Placer; }
			set{ m_Placer = value; }
		}

		[Constructable]
		public YardStair( Mobile from, int id1, int id2, int id3, int id4, Point3D loc, int price)
		{
			ID1 = id1;
			ID2 = id2;
			ID3 = id3;
			ID4 = id4;

			m_Value = price;
			Movable = false;
			MoveToWorld( loc, from.Map );
			m_Placer = from;
			Name = from.Name + "'s Yard";
			ItemID = id1;
			Light = LightType.Circle150;
		}

		public YardStair( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_Placer );
			writer.Write( m_Value );
			writer.Write( ID1 );
			writer.Write( ID2 );
			writer.Write( ID3 );
			writer.Write( ID4 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Placer = reader.ReadMobile();
			m_Value = reader.ReadInt();
			ID1 = reader.ReadInt();
			ID2 = reader.ReadInt();
			ID3 = reader.ReadInt();
			ID4 = reader.ReadInt();
		}


		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			if( m_Placer == null || from == m_Placer || from.AccessLevel >= AccessLevel.GameMaster )
			{
				list.Add( new StairRefundEntry( from, this, m_Value ));
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from.InRange( this.GetWorldLocation(), 10 ) )
			{
				if( m_Placer == null || from == m_Placer || from.AccessLevel >= AccessLevel.GameMaster )
				{
					if( this.ItemID == ID1 ) this.ItemID = ID2;
					else if( this.ItemID == ID2 ) this.ItemID = ID3;
					else if( this.ItemID == ID3 ) this.ItemID = ID4;
					else if( this.ItemID == ID4 ) this.ItemID = ID1;
				}
				else
				{
					from.SendMessage( "Stay out of my yard!" );
				}
			}
			else
			{
				from.SendMessage( "The item is too far away" );
			}
		}
	}
}