using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Multis;
using Server.Targeting;

namespace Server.Items
{
	public enum TypeOfFountain
	{
		Sand,
		Stone,
	}

	public class YardFountain : Item
	{
		private ArrayList m_Components;
		private static Mobile m_Placer;
		private static int m_Value = 0;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Placer
		{
			get{ return m_Placer; }
			set{ m_Placer = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Price
		{
			get{ return m_Value; }
			set{ m_Value = value; }
		}

//PIECES\\
		private class Piece : Item
		{
			YardFountain IsPartOf;
			public Piece( int itemID, String name, YardFountain ThisFountain ) : base( itemID )
			{
				Movable = false;
				Name = name;
				IsPartOf=ThisFountain;
			}

			public Piece( Serial serial ) : base( serial )
			{
			}

			public override void OnAfterDelete()
			{
				if(IsPartOf != null)
					IsPartOf.OnAfterDelete();
				else
					base.OnAfterDelete();
			}

			public override void OnDoubleClick( Mobile from )
			{
				if(IsPartOf != null)
					IsPartOf.OnDoubleClick(from);
				else
					base.OnDoubleClick(from);
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );
				writer.Write( (int) 0 ); // version
				writer.Write( IsPartOf );
			}
			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );
				int version = reader.ReadInt();
				switch (version)
				{
					case 0:
					{
						IsPartOf = reader.ReadItem() as YardFountain;
						break;
					}
				}
			}

		}
//END PIECES\\


		public YardFountain( Mobile from, int price, TypeOfFountain type, Point3D loc )
		{
			m_Value = price;
			m_Placer = from;
			int id = 0;
			string name = "";
			name = from.Name + "'s Fountain";
			Name = name;
			Movable = false;
			MoveToWorld( loc, from.Map );

			m_Components = new ArrayList();

			switch ( type )
			{
				case TypeOfFountain.Stone:
				{
					id = 0x1731;
					ItemID = id+9;
				}
				break;
				case TypeOfFountain.Sand:
				{
					id = 0x19C3;
					ItemID = id+9;
				}
				break;
			}
			AddPiece( -2, +1, 0, id++, name, loc);
			AddPiece( -1, +1, 0, id++, name, loc);
			AddPiece( +0, +1, 0, id++, name, loc);
			AddPiece( +1, +1, 0, id++, name, loc);

			AddPiece( +1, +0, 0, id++, name, loc);
			AddPiece( +1, -1, 0, id++, name, loc);
			AddPiece( +1, -2, 0, id++, name, loc);

			AddPiece( +0, -2, 0, id++, name, loc);
			AddPiece( +0, -1, 0, id++, name, loc);
			//AddPiece( +0, +0, 0, id++, name, loc);
			id++;
			AddPiece( -1, +0, 0, id++, name, loc);
			AddPiece( -2, +0, 0, id++, name, loc);

			AddPiece( -2, -1, 0, id++, name, loc);
			AddPiece( -1, -1, 0, id++, name, loc);

			AddPiece( -1, -2, 0, id++, name, loc);
			AddPiece( -2, -2, 0, ++id, name, loc);

			m_Components.Add( this );
		}

		private void AddPiece( int x, int y, int z, int itemID, string name, Point3D loc)
		{
			PlaceAndAdd( x, y, z, new Piece( itemID, name, this), loc );
		}

		private void PlaceAndAdd( int x, int y, int z, Item item, Point3D loc )
		{
			item.MoveToWorld( new Point3D( loc.X + x, loc.Y + y, loc.Z + z ), m_Placer.Map );
			m_Components.Add( item );
		}


		public YardFountain( Serial serial ) : base( serial )
		{
		}



		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_Placer );
			writer.Write( m_Value );
			writer.WriteItemList( m_Components );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_Placer = reader.ReadMobile();
					m_Value = reader.ReadInt();
					m_Components = reader.ReadItemList();
					break;
				}
			}
		}

		public override void OnAfterDelete()
		{
			for ( int i = 0; i < m_Components.Count; ++i )
				((Item)m_Components[i]).Delete();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( from.InRange( this.GetWorldLocation(), 10 ) )
			{
				if( m_Placer == null || from == m_Placer || from.AccessLevel >= AccessLevel.GameMaster )
				{
					Container c = from.Backpack;
					Gold t = new Gold( m_Value );
					if( c.TryDropItem( from, t, true ) )
					{
						this.Delete();
						from.SendMessage( "The item disolves and gives you a refund" );
					}
					else
					{
						t.Delete();
						from.SendMessage("For some reason, the refund didn't work!  Please page a GM");
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
