using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{
	public class ConfirmRewardGump : BaseConfirmGump
	{		
		public override int TitleNumber{ get{ return 1074974; } } // Confirm Selection
		public override int LabelNumber{ get{ return 1074975; } } // Are you sure you wish to select this?
	
		private IComunityCollection m_Collection;
		private Point3D m_Location;
		private CollectionItem m_Item;
		private int m_Hue;
		
		public ConfirmRewardGump( IComunityCollection collection, Point3D location, CollectionItem item ) : this( collection, location, item, 0 )
		{
		}
	
		public ConfirmRewardGump( IComunityCollection collection, Point3D location, CollectionItem item, int hue ) : base()
		{
			m_Collection = collection;
			m_Location = location;
			m_Item = item;
			m_Hue = hue;
			
			if ( m_Item != null )			
				AddItem( 150, 100, m_Item.ItemID, m_Item.Hue );
		}
		
		public override void Confirm( Mobile from )
		{			
			if ( m_Collection == null || !from.InRange( m_Location, 2 ) )
				return;
			
			if ( from is PlayerMobile )	
				m_Collection.Reward( (PlayerMobile) from, m_Item, m_Hue );
		}
	}
}