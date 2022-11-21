using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class AnimatedWeaponSpell : MysticismSpell
	{
		// When this spell is invoked, a weapon is conjured and animated. This weapon attacks nearby foes. 
		// Shame you cannot target a weapon/armor and animate it Diablo II's Summon Steel Golem style, it would be retardly simple too, just equip the mobile with the item and mark it unmovable.

		public override int RequiredMana{ get{ return 11; } }
		public override double RequiredSkill{ get{ return 33.0; } }

		private static SpellInfo m_Info = new SpellInfo(
				"Animated Weapon", "In Jux Por Ylem",
				230,
				9022,
				Reagent.BlackPearl,
				Reagent.MandrakeRoot,
				Reagent.Nightshade,
				Reagent.Bone
			);
		
		public override SpellCircle Circle
    {
      get { return SpellCircle.Fourth; }
    }

		public AnimatedWeaponSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			if ( Caster.Followers + 4 > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				TimeSpan duration = TimeSpan.FromSeconds( (2 * Caster.Skills[SkillName.Focus].Fixed) / 5 );
				SpellHelper.Summon( new MysticismAnimatedWeapon(), Caster, 0x216, duration, false, false );
			}

			FinishSequence();
		}
	}
}