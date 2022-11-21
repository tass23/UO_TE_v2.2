using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Fourth;
using Server.Spells.Necromancy;
using Server.Targeting;

namespace Server.Spells.Mystic
{
	public class CleansingWindsSpell : MysticSpell
	{
		// Soothing winds attempt to neutralize poisons, lift curses, and heal a valid Target. 

		public override int RequiredMana{ get{ return 20; } }
		public override double RequiredSkill{ get{ return 58; } }

		private static SpellInfo m_Info = new SpellInfo(
				"Cleansing Winds", "In Vas Mani Hur",
				230,
				9022,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.MandrakeRoot,
				Reagent.DragonBlood
			);

		public CleansingWindsSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new MysticSpellTarget( this, TargetFlags.Beneficial );
		}

		public override void OnTarget( Object o )
		{
			IPoint3D p = o as IPoint3D;

			if ( p == null )
				return;

			List<Mobile> targets = new List<Mobile>();
			StatMod mod;

			foreach ( Mobile mob in Caster.Map.GetMobilesInRange( new Point3D( p ), 3 ) )
			{
				if ( mob == null )
					continue;

				if ( Caster is PlayerMobile )
					if ( Caster.CanBeBeneficial( mob, false ) )
						targets.Add( mob );
			}

			Mobile m;
			int toheal = (int)(Caster.Skills[SkillName.Mysticism].Value * 0.1);
			Caster.PlaySound( 0x64D );

			for ( int i = 0; i < targets.Count; i++ )
			{
				m = targets[i];

				if ( !m.Alive )
					continue;

				m.Heal( toheal + Utility.RandomMinMax( 1, 5 ) );

				mod = m.GetStatMod( "[Magic] Str Offset" );
				if ( mod != null && mod.Offset < 0 )
					m.RemoveStatMod( "[Magic] Str Offset" );

				mod = m.GetStatMod( "[Magic] Dex Offset" );
				if ( mod != null && mod.Offset < 0 )
					m.RemoveStatMod( "[Magic] Dex Offset" );

				mod = m.GetStatMod( "[Magic] Int Offset" );
				if ( mod != null && mod.Offset < 0 )
					m.RemoveStatMod( "[Magic] Int Offset" );

				m.Paralyzed = false;
				m.Sleep = false; // SA Mysticism Edit
				m.CurePoison( Caster );
				EvilOmenSpell.TryEndEffect( m );
				StrangleSpell.RemoveCurse( m );
				CorpseSkinSpell.RemoveCurse( m );
				CurseSpell.RemoveEffect( m );
				MortalStrike.EndWound( m );

				if ( Core.ML)
					BloodOathSpell.RemoveCurse ( m );

				MindRotSpell.ClearMindRotScalar ( m );

				BuffInfo.RemoveBuff( m, BuffIcon.Clumsy );
				BuffInfo.RemoveBuff( m, BuffIcon.FeebleMind );
				BuffInfo.RemoveBuff( m, BuffIcon.Weaken );
				BuffInfo.RemoveBuff ( m, BuffIcon.Curse );
				BuffInfo.RemoveBuff( m, BuffIcon.MassCurse );
				BuffInfo.RemoveBuff( m, BuffIcon.MortalStrike );
				BuffInfo.RemoveBuff ( m, BuffIcon.Mindrot );
			}

		}
	}
}
/*




*/