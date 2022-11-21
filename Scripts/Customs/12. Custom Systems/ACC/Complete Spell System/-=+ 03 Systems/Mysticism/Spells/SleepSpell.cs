using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class SleepSpell : MysticismSpell
	{
		// Hurls a magical boulder at the Target, dealing physical damage. 
		// This spell also has a chance to knockback and stun a player Target. 

		public override int RequiredMana{ get{ return 9; } }
		public override double RequiredSkill{ get{ return 20; } }

		private static SpellInfo m_Info = new SpellInfo(
				"Sleep", "In Zu",
				230,
				9022,
				Reagent.Bloodmoss,
				Reagent.Garlic,
				Reagent.SulfurousAsh,
				Reagent.DragonBlood
			);
		
		public override SpellCircle Circle
    {
      get { return SpellCircle.Fourth; }
    }

		public SleepSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new MysticismSpellTarget( this, TargetFlags.Harmful );
		}

		public override void OnTarget( Object o )
		{
			Mobile target = o as Mobile;

			if ( target == null )
			{
				return;
			}
			else if ( CheckHSequence( target ) )
			{
					target.DoSleep( TimeSpan.FromSeconds( 6 ) );
			}

			FinishSequence();
		}
	}
}