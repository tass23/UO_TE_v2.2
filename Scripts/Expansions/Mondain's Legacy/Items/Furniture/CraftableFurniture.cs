using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public enum ItemQuality
	{
		Low,
		Normal,
		Exceptional,
	}

	public class CraftableFurniture : Item, ICraftable
	{
		public virtual bool ShowCraferName{ get{ return true; } }

		private Mobile m_Crafter;
		private CraftResource m_Resource;
		private ItemQuality m_Quality;

		[CommandProperty( AccessLevel.GameMaster )]
		public ItemQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource;	}
			set
			{
				if ( m_Resource != value )
				{
					m_Resource = value;
					Hue = CraftResources.GetHue( m_Resource );
					
					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}
	
		public CraftableFurniture( int itemID ) : base( itemID )
		{
		}

		public CraftableFurniture( Serial serial ) : base( serial )
		{
		}
		
		public override void AddWeightProperty( ObjectPropertyList list )
		{
			base.AddWeightProperty( list );

			if ( ShowCraferName && m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~

			if ( m_Quality == ItemQuality.Exceptional )
				list.Add( 1060636 ); // exceptional
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			CraftResourceInfo info = CraftResources.IsStandard( m_Resource ) ? null : CraftResources.GetInfo( m_Resource );

			if ( info != null && info.Number > 0 )
				list.Add( info.Number );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (Mobile) m_Crafter );
			writer.Write( (int) m_Resource );
			writer.Write( (int) m_Quality );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Crafter = reader.ReadMobile();
			m_Resource = (CraftResource) reader.ReadInt();
			m_Quality = (ItemQuality) reader.ReadInt();
		}

		#region ICraftable
		public virtual int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (ItemQuality) quality;

			if ( makersMark )
				Crafter = from;

			Type resourceType = typeRes;

			if ( resourceType == null )
				resourceType = craftItem.Resources.GetAt( 0 ).ItemType;

			Resource = CraftResources.GetFromType( resourceType );

			CraftContext context = craftSystem.GetContext( from );

			if ( context != null && context.DoNotColor )
				Hue = 0;

			return quality;
		}
		#endregion
	}
}
