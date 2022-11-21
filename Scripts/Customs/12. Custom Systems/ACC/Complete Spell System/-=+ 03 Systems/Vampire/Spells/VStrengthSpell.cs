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
    public class VStrengthSpell : VampireSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
        "Super Strength", "zor",
        //SpellCircle.Fourth,
        212,
        9041,
        Reagent.MandrakeRoot,
        Reagent.BlackPearl
        );

		public override SpellCircle Circle
        {
            get { return SpellCircle.Second; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 25; } }
        public override int RequiredMana { get { return 15; } }

        private static Dictionary<Mobile, SkillMod> m_Table = new Dictionary<Mobile, SkillMod>();
		
		public VStrengthSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
			else if ( CheckBSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				SpellHelper.AddStatBonus( Caster, m, StatType.Str );
				
				m.FixedParticles( 0x373A, 10, 15, 2023, 0, 5017, EffectLayer.Waist );
				m.PlaySound( 0x43D ); //Ooh sound

				int percentage = (int)(SpellHelper.GetOffsetScalar( Caster, m, false )*100);
				TimeSpan length = SpellHelper.GetDuration( Caster, m );
				
				BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.Strength, 1122600, length, m, percentage.ToString() ) );

			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private VStrengthSpell m_Owner;

			public InternalTarget( VStrengthSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Beneficial )
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