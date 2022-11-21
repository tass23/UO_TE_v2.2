using System;
using Server;
using Server.Items;

namespace Server.Targeting
{
	public class SpellWeavingWandTarget : Target
	{
		private BaseSpellWeavingWand m_Item;

		public SpellWeavingWandTarget( BaseSpellWeavingWand item ) : base( 6, false, TargetFlags.None )
		{
			m_Item = item;
		}

		private static int GetOffset( Mobile caster )
		{
			return 5 + (int)(caster.Skills[SkillName.Spellweaving].Value * 0.02 );
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			m_Item.DoSpellWeavingWandTarget( from, targeted );
		}
	}
}