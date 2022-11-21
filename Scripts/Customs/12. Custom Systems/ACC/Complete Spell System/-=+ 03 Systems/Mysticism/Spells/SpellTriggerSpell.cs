using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class SpellTriggerSpell : MysticismSpell
	{
		public override int RequiredMana{ get{ return 14; } }
		public override double RequiredSkill{ get{ return 45; } }

		private static SpellInfo m_Info = new SpellInfo(
				"Spell Trigger", "In Vas Ort Ex ",
				230,
				9022,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk,
				Reagent.DragonBlood
			);
		
		public override SpellCircle Circle
    {
      get { return SpellCircle.Fourth; }
    }

		public SpellTriggerSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			if ( Caster.HasGump( typeof( MysticismSpellTriggerGump ) ) )
					Caster.CloseGump( typeof( MysticismSpellTriggerGump ) );

			Caster.SendGump( new MysticismSpellTriggerGump( Caster ) );
		}
	}
}
/*




*/