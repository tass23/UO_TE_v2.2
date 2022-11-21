using System;
using System.Drawing;
using System.Drawing.Imaging;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server
{
	public class CollectionItem 
	{
		private Type m_Type;
			
		private int m_ItemID;
		private int m_X;
		private int m_Y;
		private int m_Width;
		private int m_Height;		
		
		private int m_Tooltip;
		private int m_Hue;
		private double m_Points;
		
		public Type Type{ get{ return m_Type; } }
		
		// image info
		public int ItemID{ get{ return m_ItemID; } }
		public int X{ get{ return m_X; } }
		public int Y{ get{ return m_Y; } }
		public int Width{ get{ return m_Width; } }
		public int Height{ get{ return m_Height; } }
		
		
		public int Tooltip{ get{ return m_Tooltip; } }
		public int Hue{ get{ return m_Hue; } }
		public double Points{ get{ return m_Points; } }
		
		public CollectionItem( Type type, int itemID, int tooltip, int hue, double points )
		{
			m_Type = type;
			m_ItemID = itemID;
			m_Tooltip = tooltip;
			m_Hue = hue;
			m_Points = points;
			
			int mx, my;			
			mx = my = 0;
			
			Item.Measure( Item.GetBitmap( m_ItemID ), out m_X, out m_Y, out mx, out my );
			
			m_Width = mx - m_X;
			m_Height = my - m_Y;
		}
		
		public virtual bool Validate( PlayerMobile from, Item item )
		{
			return true;
		}
		
		public virtual void OnGiveReward( PlayerMobile to, IComunityCollection collection, int hue )
		{
		}
	}
	
	public class CollectionHuedItem : CollectionItem
	{
		private int[] m_Hues;
		
		public int[] Hues{ get{ return m_Hues; } }
		
		public CollectionHuedItem( Type type, int itemID, int tooltip, int hue, double points, int[] hues ) : base( type, itemID, tooltip, hue, points )
		{
			m_Hues = hues;
		}
	}
	
	public class CollectionTitle : CollectionItem
	{	
		private object m_Title;
		
		public object Title{ get{ return m_Title; } }
	
		public CollectionTitle( object title, int tooltip, double points ) : base( null, 0xFF1, tooltip, 0x0, points )
		{
			m_Title = title;
		}
		
		public override void OnGiveReward( PlayerMobile to, IComunityCollection collection, int hue )
		{
			if ( to.AddCollectionTitle( m_Title ) )
			{
				if ( m_Title is int )
					to.SendLocalizedMessage( 1073625, "#" + (int) m_Title ); // The title "~1_TITLE~" has been bestowed upon you. 
				else if ( m_Title is string )
					to.SendLocalizedMessage( 1073625, (string) m_Title ); // The title "~1_TITLE~" has been bestowed upon you. 
					
				to.AddCollectionPoints( collection.CollectionID, (int) Points * -1 );				
			}
			else
				to.SendLocalizedMessage( 1073626 ); // You already have that title!
		}
	}
	
	public class CollectionTreasureMap : CollectionItem
	{
		private int m_Level;
		
		public int Level{ get{ return m_Level; } }
		
		public CollectionTreasureMap( int level, int tooltip, double points ) : base( typeof( TreasureMap ), 0x14EB, tooltip, 0x0, points )
		{
			m_Level = level;
		}
		
		public override bool Validate( PlayerMobile from, Item item )
		{
			TreasureMap map = (TreasureMap) item;
			
			if ( map.Level == m_Level )
				return true;
			
			return false;
		}
	}
	
	public class CollectionSpellbook : CollectionItem
	{
		private SpellbookType m_Type;
		
		public SpellbookType SpellbookType{ get{ return m_Type; } }
		
		public CollectionSpellbook( SpellbookType type, int itemID, int tooltip, double points ) : base( typeof( Spellbook ), itemID, tooltip, 0x0, points )
		{
			m_Type = type;
		}
		
		public override bool Validate( PlayerMobile from, Item item )
		{
			Spellbook spellbook = (Spellbook) item;
			
			if ( spellbook.SpellbookType == m_Type && spellbook.Content == 0 )
				return true;
			
			return false;
		}
	}
}