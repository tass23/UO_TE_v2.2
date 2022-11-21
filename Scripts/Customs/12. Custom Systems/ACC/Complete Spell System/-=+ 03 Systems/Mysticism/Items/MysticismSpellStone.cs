using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;
using Server.Mobiles;
using Server.Spells;
using Server.ACC.CSS.Systems.Mysticism;

namespace Server.Items
{
	public class MysticismSpellStone : SpellScroll
	{
		private Mobile m_Caster;

		[Constructable]
		public MysticismSpellStone( Mobile caster, int spellid ) : base( spellid, 0x4079, 1 )
		{
			m_Caster = caster;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from != m_Caster )
			{
				from.SendLocalizedMessage( 500294 ); // You cannot use that.
				Delete();
			}
			else if ( SpellID >= 677  && SpellID <= 692 )
				base.OnDoubleClick( from );
			else
			{
				from.SendMessage( "There was no spell stored in that stone." );
				Delete();
			}
		}
		
		public override bool DropToWorld( Mobile from, Point3D p )
		{
			Delete();
			return false;
		}

		public override bool AllowSecureTrade( Mobile from, Mobile to, Mobile newOwner, bool accepted )
		{
			return false;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
		}

		private static Dictionary<int, string> m_SpellNames = new Dictionary<int, string>();

		public static void Configure()
		{
			m_SpellNames.Add( 677, "Nether Bolt" );
			m_SpellNames.Add( 678, "Healing Stone" );
			m_SpellNames.Add( 679, "Purge Magic" );
			m_SpellNames.Add( 680, "Enchant" );
			m_SpellNames.Add( 681, "Sleep" );
			m_SpellNames.Add( 682, "Eagle Strike" );
			m_SpellNames.Add( 683, "Animated Weapon" );
			m_SpellNames.Add( 684, "Stone Form" );
			m_SpellNames.Add( 685, "Spell Tigger" );
			m_SpellNames.Add( 686, "Mass Sleep" );
			m_SpellNames.Add( 687, "Cleansing Winds" );
			m_SpellNames.Add( 688, "Bombard" );
			m_SpellNames.Add( 689, "Spell Plague" );
			m_SpellNames.Add( 690, "Hail Storm" );
			m_SpellNames.Add( 691, "Nether Cyclone" );
			m_SpellNames.Add( 692, "Colossus" );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_SpellNames.ContainsKey( SpellID ) )
				list.Add( 1080166, m_SpellNames[SpellID] ); // Use: ~1_spellName~
		}

		public MysticismSpellStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( m_Caster );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Caster = reader.ReadMobile();
		}
	}
}
/*
		private Dictionary<int, string> m_SpellNames = new Dictionary<int, string>()
		{
			{ 677, "Nether Bolt" },
			{ 678, "Healing Stone" },
			{ 679, "Purge Magic" },
			{ 680, "Enchant" },
			{ 681, "Sleep" },
			{ 682, "Eagle Strike" },
			{ 683, "Animated Weapon" },
			{ 684, "Stone Form" },
			{ 685, "Spell Tigger" },
			{ 686, "Mass Sleep" },
			{ 687, "Cleansing Winds" },
			{ 688, "Bombard" },
			{ 689, "Spell Plague" },
			{ 690, "Hail Storm" },
			{ 691, "Nether Cyclone" },
			{ 692, "Colossus" }
		};
*/