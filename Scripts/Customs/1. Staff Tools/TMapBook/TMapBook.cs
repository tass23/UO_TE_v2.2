  //
 //  Written by Haazen Dec 2005
//
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Multis;
using Server.Engines.Craft;
using Server.ContextMenus;
using Server.Prompts;

namespace Server.Items
{
	public class TMapBook : Item
	{
		private ArrayList m_Entries;
		private int m_DefaultIndex;


		[Constructable]
		public TMapBook( ) : base( 8788 )
		{
			Weight = ( 1.0 );
			LootType = LootType.Blessed;
			Hue = 2101;
			Name = "A Treasure Map Book";

			Layer = Layer.OneHanded;
			m_Entries = new ArrayList();
			m_DefaultIndex = -1;

		}


		public ArrayList Entries
		{
			get
			{
				return m_Entries;
			}
		}

		public TMapBookEntry Default
		{
			get
			{
				if ( m_DefaultIndex >= 0 && m_DefaultIndex < m_Entries.Count )
					return (TMapBookEntry)m_Entries[m_DefaultIndex];

				return null;
			}
			set
			{
				if ( value == null )
					m_DefaultIndex = -1;
				else
					m_DefaultIndex = m_Entries.IndexOf( value );
			}
		}

		public TMapBook( Serial serial ) : base( serial )
		{
		}

		public override bool AllowEquipedCast( Mobile from )
		{
			return true;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( from.CheckAlive() && IsChildOf( from.Backpack ) )
				list.Add( new NameBookEntry( from, this ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			writer.Write( m_Entries.Count );


			for ( int i = 0; i < m_Entries.Count; ++i )
				((TMapBookEntry)m_Entries[i]).Serialize( writer );
			writer.Write( m_DefaultIndex );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			LootType = LootType.Blessed;

			if( Core.SE && Weight == 3.0 )
				Weight = 1.0;

			int version = reader.ReadInt();

			int count = reader.ReadInt();

			m_Entries = new ArrayList( count );

			for ( int i = 0; i < count; ++i )
				m_Entries.Add( new TMapBookEntry( reader ) );

			m_DefaultIndex = reader.ReadInt();

		}

		public void DropTMap( Mobile from, TMapBookEntry e, int index )
		{
			if ( m_DefaultIndex == index )
				m_DefaultIndex = -1;

			m_Entries.RemoveAt( index );

			TreasureMap tmap = new TreasureMap( e.Level, e.Map );

			tmap.Decoder = e.Decoder;
			tmap.ChestLocation = e.Location;
			tmap.ChestMap = e.Map;
			tmap.Bounds = e.Bounds;

			tmap.ClearPins();
			tmap.AddWorldPin( e.Location.X, e.Location.Y );

			from.AddToBackpack( tmap );

			from.SendMessage( "You have removed the Treasure Map" );


		}
///////
		public void ViewMap( Mobile from, TMapBookEntry e, int index )
		{
			if ( e.Location.X > 5115 )
			{
				from.CloseGump( typeof( SecretMapGump ) );
				from.SendGump( new SecretMapGump( from ) );
				return;
			}
			if ( m_DefaultIndex == index )
				m_DefaultIndex = -1;

			from.CloseGump( typeof( MapDisplayGump ) );
			from.SendGump( new MapDisplayGump( from, e.Location.X, e.Location.Y ) );



		}

////////
	/*	public bool IsOpen( Mobile toCheck )
		{
			NetState ns = toCheck.NetState;

			if ( ns == null )
				return false;

			List<Gump> gumps = ns.Gumps;

			for ( int i = 0; i < gumps.Count; ++i )
			{
				if ( gumps[i] is TMapBookGump )
				{
					TMapBookGump gump = (TMapBookGump)gumps[i];

					if ( gump.Book == this )
						return true;
				}
			}

			return false;
		} */

		public override bool DisplayLootType{ get{ return Core.AOS; } }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( GetWorldLocation(), 1 ) )
			{
				from.CloseGump( typeof( TMapBookGump ) );
				from.SendGump( new TMapBookGump( from, this ) );
			}
		}

	/*	public override Item Dupe( int amount )
		{
			TMapBook book = new TMapBook();
			book.m_DefaultIndex = m_DefaultIndex;
			book.LootType = this.LootType;

			for( int i = 0; i < m_Entries.Count; i++ )
			{
				TMapBookEntry entry = m_Entries[i] as TMapBookEntry;
				
				book.m_Entries.Add( new TMapBookEntry( entry.Level, entry.Decoder, entry.Map, entry.Location, entry.Bounds ) ); 
			}

			return base.Dupe( book, amount );
		} */

		public bool CheckAccess( Mobile m )
		{
			return true;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is TreasureMap )
			{
				if ( !CheckAccess( from ) )
				{
					from.SendLocalizedMessage( 502413 ); // That cannot be done while the book is locked down.
				}
				/* else if ( IsOpen( from ) )
				{
					from.SendLocalizedMessage( 1005571 ); // You cannot place objects in the book while viewing the contents.
				} */
				else if ( m_Entries.Count < 32 )
				{
					TreasureMap tmap = (TreasureMap)dropped;
					if ( tmap.Completed )
					{
						from.SendMessage( "This map is completed and can not be stored in this book" );
						InvalidateProperties();
						dropped.Delete();
						return false;
					}
					if ( tmap.ChestMap != null ) 
					{
						m_Entries.Add( new TMapBookEntry( tmap.Level, tmap.Decoder, tmap.ChestMap, tmap.ChestLocation,  tmap.Bounds ) );
						InvalidateProperties();
						dropped.Delete();
						from.Send( new PlaySound( 0x42, GetWorldLocation() ) );
						return true;
					}
					else
					{
						from.SendMessage( "This map is invalid" );
					}
				}
				else
				{
					from.SendMessage( "This TMap Book is full" );
				}
			}

			return false;
		}

		private class NameBookEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private TMapBook m_Book;

			public NameBookEntry( Mobile from, TMapBook book ) : base( 6216 )
			{
				m_From = from;
				m_Book = book;
			}

			public override void OnClick()
			{
				if ( m_From.CheckAlive() && m_Book.IsChildOf( m_From.Backpack ) )
				{
					m_From.Prompt = new NameBookPrompt( m_Book );
					m_From.SendLocalizedMessage( 1062479 ); // Type in the new name of the book:
				}
			}
		}

		private class NameBookPrompt : Prompt
		{
			private TMapBook m_Book;

			public NameBookPrompt( TMapBook book )
			{
				m_Book = book;
			}

			public override void OnResponse( Mobile from, string text )
			{
				if ( text.Length > 40 )
					text = text.Substring( 0, 40 );

				if ( from.CheckAlive() && m_Book.IsChildOf( from.Backpack ) )
				{
					m_Book.Name = Utility.FixHtml( text.Trim() );

					from.SendMessage( "This SOS Book name has been changed" );
				}
			}

			public override void OnCancel( Mobile from )
			{
			}
		}
	}

	public class TMapBookEntry
	{
		private int m_Level;
		private Mobile m_Decoder;
		private Map m_Map;
		private Point2D m_Location;
		private Rectangle2D m_Bounds;

		public int Level
		{ get{ return m_Level; } }

		public Mobile Decoder
		{ get{ return m_Decoder; } }

		public Map Map
		{ get{ return m_Map; } }

		public Point2D Location
		{ get{ return m_Location; } }

		public Rectangle2D Bounds
		{ get{ return m_Bounds; } }


		public TMapBookEntry( int level, Mobile decoder, Map map, Point2D loc, Rectangle2D bounds ) 
		{
			m_Level = level;
			m_Decoder = decoder;
			m_Map = map;
			m_Location = loc;
			m_Bounds = bounds;
		}

		public TMapBookEntry( GenericReader reader )
		{
			int version = reader.ReadByte();
			m_Level = reader.ReadInt();
			m_Decoder = reader.ReadMobile();
			m_Map = reader.ReadMap();
			m_Location = reader.ReadPoint2D();
			m_Bounds = reader.ReadRect2D();
		}

		public void Serialize( GenericWriter writer )
		{
			writer.Write( (byte) 0 ); // version
			writer.Write( m_Level );
			writer.Write( m_Decoder );
			writer.Write( m_Map );
			writer.Write( m_Location );
			writer.Write( m_Bounds );
		}
	}
}