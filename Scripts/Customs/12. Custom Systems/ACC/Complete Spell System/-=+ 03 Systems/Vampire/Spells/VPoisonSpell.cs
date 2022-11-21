using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Gumps;
using Server.Misc;

//First Circle: 4
//Second Circle: 6
//Third Circle: 9
//Fourth Circle: 11
//Fifth Circle: 14
//Sixth Circle: 20
//Seventh Circle: 40
//Eight Circle: 50

namespace Server.ACC.CSS.Systems.Vampire
{
    public class VPoisonSpell : VampireSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
        "Miasma", "otrava",
        //SpellCircle.Fourth,
        212,
        9041,
        Reagent.MandrakeRoot,
        Reagent.BlackPearl
        );

		public override SpellCircle Circle
        {
            get { return SpellCircle.Third; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 67; } }
        public override int RequiredMana { get { return 9; } }

        private static Dictionary<Mobile, SkillMod> m_Table = new Dictionary<Mobile, SkillMod>();
		
		public VPoisonSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override bool CheckCast()
		{
			PlayerMobile pm = (PlayerMobile) Caster;
			if ( pm.Vampire == 0 )
			{
				Caster.SendMessage( "Only a vampire may attempt something like this." ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			
			else
			{
				Caster.CloseGump( typeof( VampireGump ) );
				Caster.SendGump( new VampireGump() );
				return true;
			}
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				if ( m.Spell != null )
					m.Spell.OnCasterHurt();

				m.Paralyzed = false;
				m.Sleep = false; //SA Mysticism Edit

				if ( CheckResisted( m ) )
				{
					m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
				}
				else
				{
					int level;

					if ( Core.AOS )
					{
						if ( Caster.InRange( m, 2 ) )
						{
							int total = (Caster.Skills.Focus.Fixed + Caster.Skills.SpiritSpeak.Fixed) / 2;

							if ( total >= 1000 )
								level = 3;
							else if ( total > 850 )
								level = 2;
							else if ( total > 650 )
								level = 1;
							else
								level = 0;
						}
						else
						{
							level = 0;
						}
					}
					else
					{
						//double total = Caster.Skills[SkillName.Magery].Value + Caster.Skills[SkillName.Poisoning].Value;

						#region Dueling
						double total = Caster.Skills[SkillName.Focus].Value;

						if ( Caster is Mobiles.PlayerMobile )
						{
							Mobiles.PlayerMobile pm = (Mobiles.PlayerMobile)Caster;

							if ( pm.DuelContext != null && pm.DuelContext.Started && !pm.DuelContext.Finished && !pm.DuelContext.Ruleset.GetOption( "Skills", "Spirit Speak" ) )
							{
							}
							else
							{
								total += Caster.Skills[SkillName.SpiritSpeak].Value;
							}
						}
						else
						{
							total += Caster.Skills[SkillName.SpiritSpeak].Value;
						}
						#endregion

						double dist = Caster.GetDistanceToSqrt( m );

						if ( dist >= 3.0 )
							total -= (dist - 3.0) * 10.0;

						if ( total >= 200.0 && 1 > Utility.Random( 10 ) )
							level = 3;
						else if ( total > (Core.AOS ? 170.1 : 170.0) )
							level = 2;
						else if ( total > (Core.AOS ? 130.1 : 130.0) )
							level = 1;
						else
							level = 0;
					}

					m.ApplyPoison( Caster, Poison.GetPoison( level ) );
				}

				m.FixedParticles( 0x3728, 10, 15, 1569, 1569, 5021, EffectLayer.Waist );
				m.PlaySound( 0x1E6 );

				HarmfulSpell( m );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private VPoisonSpell m_Owner;

			public InternalTarget( VPoisonSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}