using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Spells;
using Server.Gumps;

//First Circle: 4
//Second Circle: 6
//Third Circle: 9
//Fourth Circle: 11
//Fifth Circle: 14
//Sixth Circle: 20
//Seventh Circle: 40
//Eight Circle: 50

namespace Server.ACC.CSS.Systems.LightForce
{
	public class ForceTidesSpell : LightForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
			"Force Tides", "A Jedi's defense can provide an offense.",
			233,
			9012,
			false,
			Reagent.Bloodmoss,
			Reagent.Ginseng,
			Reagent.MandrakeRoot,
			Reagent.SulfurousAsh
		);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 80; } }
        public override int RequiredMana { get { return 50; } }
		
		public ForceTidesSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool DelayedDamage{ get{ return !Core.AOS; } }

		public override void OnCast()
		{
			Ability.ForceWave( Caster );
			
			if ( SpellHelper.CheckTown( Caster, Caster ) && CheckSequence() )
			{				
				List<Mobile> targets = new List<Mobile>();

				Map map = Caster.Map;

				if ( map != null )
					foreach ( Mobile m in Caster.GetMobilesInRange( 1 + (int)(Caster.Skills[SkillName.Meditation].Value / 15.0) ) )
						if ( Caster != m && SpellHelper.ValidIndirectTarget( Caster, m ) && Caster.CanBeHarmful( m, false ) && (!Core.AOS || Caster.InLOS( m )) )
							targets.Add( m );

                Caster.PlaySound(0x220);

				for ( int i = 0; i < targets.Count; ++i )
				{
					Mobile m = targets[i];

					int damage;

					if ( Core.AOS )
					{
						damage = m.Hits / 2;

						if ( !m.Player )
							damage = Math.Max( Math.Min( damage, 200 ), 100 );
							damage += Utility.RandomMinMax( 0, 100 );

					}
					else
					{
						damage = (m.Hits * 8) / 5;

						if ( !m.Player && damage < 50 )
							damage = 50;
						else if ( damage > 75 )
							damage = 75;
					}

					Caster.DoHarmful( m );
					SpellHelper.Damage( TimeSpan.Zero, m, Caster, damage, 100, 0, 0, 0, 0 );
				}
			}

			FinishSequence();
		}
		
		public override bool CheckCast()
		{
			if ( Caster.Karma < 5000 )
			{
				Caster.SendMessage( "You lack the Jedi power of the Force to cast this." ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			else
			{
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return true;
			}
		}		
	}
}