using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Engines.Harvest;
using Server.ContextMenus;

namespace Server.Items
{
	public abstract class BaseBoline : BaseMeleeWeapon, IUsesRemaining
	{
		public override int DefHitSound{ get{ return 0x23B; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override SkillName DefSkill{ get{ return SkillName.Swords; } }
		public override WeaponType DefType{ get{ return WeaponType.Slashing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		public virtual HarvestSystem HarvestSystem{ get{ return HerbGathering.System; } }

		private int m_UsesRemaining;
		private bool m_ShowUsesRemaining;

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool ShowUsesRemaining
		{
			get { return m_ShowUsesRemaining; }
			set { m_ShowUsesRemaining = value; InvalidateProperties(); }
		}

		public virtual int GetUsesScalar()
		{
			if ( Quality == WeaponQuality.Exceptional ) return 100;
			return 50;
		}

		public override void UnscaleDurability()
		{
			base.UnscaleDurability();
			int scale = GetUsesScalar();
			m_UsesRemaining = ((m_UsesRemaining * 100) + (scale - 1)) / scale;
			InvalidateProperties();
		}

		public override void ScaleDurability()
		{
			base.ScaleDurability();
			int scale = GetUsesScalar();
			m_UsesRemaining = ((m_UsesRemaining * scale) + 99) / 100;
			InvalidateProperties();
		}

		public BaseBoline( int itemID ) : this( itemID, 50 ) { }

		public BaseBoline( int itemID, int usesremaining ) : base( itemID )
		{
			m_UsesRemaining = usesremaining;
			ShowUsesRemaining = true;
			Hue = 88;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( HarvestSystem == null ) return;
			if ( IsChildOf( from.Backpack ) || Parent == from ) HarvestSystem.BeginHarvesting( from, this );
			else from.SendLocalizedMessage( 1042001 );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			if ( HarvestSystem != null ) BaseHarvestTool.AddContextMenuEntries( from, this, list, HarvestSystem );
		}

		public BaseBoline( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (bool) m_ShowUsesRemaining );
			writer.Write( (int) m_UsesRemaining );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_ShowUsesRemaining = reader.ReadBool();
			if( m_ShowUsesRemaining == false ) m_ShowUsesRemaining = true;
			m_UsesRemaining = reader.ReadInt();
		}
	}
}