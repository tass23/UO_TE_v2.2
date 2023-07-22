using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Multis;

namespace Server.Items
{
	public class YardItem : Item
	{
		public Mobile m_Placer;
		public int m_Value = 0;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Placer
		{
			get{ return m_Placer; }
			set{ m_Placer = value; }
		}
		
		[Constructable]
		public YardItem( Mobile from, int id, Point3D loc, int price)
		{
			m_Value = price;
			Movable = false;
			MoveToWorld( loc, from.Map );
			m_Placer = from;
			Name = from.Name + "'s Yard";
			ItemID = id;
			Light = LightType.Circle150;
		}

		public YardItem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_Placer );
			writer.Write( m_Value );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Placer = reader.ReadMobile();
			m_Value = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( m_Placer == null || m_Placer.Deleted )
			m_Placer = from;

			if( from.InRange( this.GetWorldLocation(), 10 ) )
			{
				if( m_Placer == null || from == m_Placer || from.AccessLevel >= AccessLevel.GameMaster )
				{
					Container c = m_Placer.Backpack;
					Gold t = new Gold( m_Value );
					if( c.TryDropItem( m_Placer, t, true ) )
					{
						this.Delete();
						m_Placer.SendMessage( "The item disolves and gives you a refund" );
					}
					else
					{
						t.Delete();
						m_Placer.SendMessage("For some reason, the refund didn't work! Please page a GM");
					}
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