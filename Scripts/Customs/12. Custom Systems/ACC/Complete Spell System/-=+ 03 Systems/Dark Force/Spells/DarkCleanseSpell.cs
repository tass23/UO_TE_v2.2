using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells.Necromancy;
using Server.Spells;
using Server.Spells.Fourth;
using Server.Gumps;
using Server.ACC.CSS.Systems.LightForce;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class DarkCleanseSpell : DarkForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Cleanse", "The Force shall free me.",
		//SpellCircle.Second,
		212,
		9041,
		Reagent.NoxCrystal,
		Reagent.DaemonBlood
		);

        public override SpellCircle Circle
        {
            get { return SpellCircle.Seventh; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
        public override double RequiredSkill { get { return 66; } }
        public override int RequiredMana { get { return 40; } }

		public DarkCleanseSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool CheckCast()
		{
			if ( Caster.Karma > 4999 )
			{
				Caster.SendMessage( "You lack the Sith power of the Force to cast this." ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			else
			{
				Caster.CloseGump( typeof( KarmaGump ) );
				Caster.SendGump( new KarmaGump( Caster ) );
				return true;
			}
		}
		
		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckBSequence( m, false ) )
			{
				SpellHelper.Turn( Caster, m );

				m.PlaySound( 0xF6 );
				m.PlaySound( 0x1F7 );
				m.FixedParticles( 0x3709, 1, 30, 9963, 13, 3, EffectLayer.Head );

				IEntity from = new Entity( Serial.Zero, new Point3D( m.X, m.Y, m.Z - 10 ), Caster.Map );
				IEntity to = new Entity( Serial.Zero, new Point3D( m.X, m.Y, m.Z + 50 ), Caster.Map );
				Effects.SendMovingParticles( from, to, 0x2255, 1, 0, false, false, 13, 3, 9501, 1, 0, EffectLayer.Head, 0x100 );

				StatMod mod;

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
				m.Sleep = false; //SA Mysticism Edit
				m.CurePoison( Caster );

				EvilOmenSpell.TryEndEffect( m );
				StrangleSpell.RemoveCurse( m );
				CorpseSkinSpell.RemoveCurse( m );
				CurseSpell.RemoveEffect( m );
				MortalStrike.EndWound( m );
				DarkCurseSpell.RemoveEffect( m );
				LightCurseSpell.RemoveEffect( m );
				if (Core.ML) { BloodOathSpell.RemoveCurse ( m ); }
				MindRotSpell.ClearMindRotScalar ( m );

				BuffInfo.RemoveBuff( m, BuffIcon.Clumsy );
				BuffInfo.RemoveBuff( m, BuffIcon.FeebleMind );
				BuffInfo.RemoveBuff( m, BuffIcon.Weaken );
				BuffInfo.RemoveBuff ( m, BuffIcon.Curse );
				BuffInfo.RemoveBuff( m, BuffIcon.MassCurse );
				BuffInfo.RemoveBuff( m, BuffIcon.MortalStrike );
				BuffInfo.RemoveBuff ( m, BuffIcon.Mindrot );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private DarkCleanseSpell m_Owner;

			public InternalTarget( DarkCleanseSpell owner ) : base( 12, false, TargetFlags.Beneficial )
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
