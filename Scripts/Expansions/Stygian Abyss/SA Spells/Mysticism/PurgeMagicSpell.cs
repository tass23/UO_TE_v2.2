using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;

namespace Server.Spells.Mystic
{
	public class PurgeMagicSpell : MysticSpell
	{
		// Attempts to remove a beneficial ward from the Target, chosen randomly.

		public override int RequiredMana{ get{ return 6; } }
		public override double RequiredSkill{ get{ return 8; } }

		private static SpellInfo m_Info = new SpellInfo(
				"Purge", "An Ort Sanct ",
				230,
				9022,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh,
				Reagent.FertileDirt
			);

		public PurgeMagicSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new MysticSpellTarget( this, TargetFlags.Harmful );
		}

		public override void OnTarget( Object o )
		{
			Caster.PlaySound( 0x656 );
		}
	}
}
/*




*/