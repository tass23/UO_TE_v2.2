using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Chivalry;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class DarkStrangleSpell : DarkForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Force Choke", "A Sith eliminates everything in the way of their destiny.",
		209,
		9031,
		Reagent.DaemonBlood,
		Reagent.NoxCrystal
		);
		
		public override SpellCircle Circle { get { return SpellCircle.Seventh; } }

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3.0 ); } }
		public override double RequiredSkill{ get{ return 66; } }
		public override int RequiredMana{ get{ return 40; } }

		public DarkStrangleSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
			if ( CheckHSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				//SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );	//Irrelevent after AoS

				/* Temporarily chokes off the air suply of the target with poisonous fumes.
				 * The target is inflicted with poison damage over time.
				 * The amount of damage dealt each "hit" is based off of the caster's Spirit Speak skill and the Target's current Stamina.
				 * The less Stamina the target has, the more damage is done by Strangle.
				 * Duration of the effect is Spirit Speak skill level / 10 rounds, with a minimum number of 4 rounds.
				 * The first round of damage is dealt after 5 seconds, and every next round after that comes 1 second sooner than the one before, until there is only 1 second between rounds.
				 * The base damage of the effect lies between (Spirit Speak skill level / 10) - 2 and (Spirit Speak skill level / 10) + 1.
				 * Base damage is multiplied by the following formula: (3 - (target's current Stamina / target's maximum Stamina) * 2).
				 * Example:
				 * For a target at full Stamina the damage multiplier is 1,
				 * for a target at 50% Stamina the damage multiplier is 2 and
				 * for a target at 20% Stamina the damage multiplier is 2.6
				 */

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				double duration;
				
				if ( Core.AOS )
				{
					int secs = (int)((GetDamageSkill( Caster ) / 10) - (GetResistSkill( m ) / 10));
					
					if( !Core.SE )
						secs += 2;

					if ( !m.Player )
						secs *= 3;

					if ( secs < 0 )
						secs = 0;

					duration = secs;
				}
				else
				{
					// Algorithm: ((20% of magery) + 4) seconds [- 50% if resisted]

					duration = 4.0 + (Caster.Skills[SkillName.Meditation].Value * 0.2);

					if ( CheckResisted( m ) )
						duration *= 0.75;
				}

				if ( m is PlagueBeastLord )
				{
					( (PlagueBeastLord) m ).OnParalyzed( Caster );
					duration = 120;
				}
				m.Paralyze( TimeSpan.FromSeconds( duration ) );
				
				 if ( m.Spell != null )
					m.Spell.OnCasterHurt();
				m.PlaySound( 0x21F );
				m.FixedParticles( 0x36B0, 1, 9, 9911, 67, 5, EffectLayer.Head );
				m.FixedParticles( 0x372A, 1, 17, 9502, 1108, 4, (EffectLayer)255 );
				
				m.FixedParticles( 0x375A, 2, 10, 5027, 0x3D, 2, EffectLayer.Waist );

				{
					Point3D loc = new Point3D( m.X, m.Y, m.Z );

					Item item = new InternalItem( loc, Caster.Map, Caster );
				}

				if ( !m_Table.Contains( m ) )
				{
					Timer t = new InternalTimer( m, Caster );
					t.Start();

					m_Table[m] = t;
				}
			}

			FinishSequence();
		}

		private static Hashtable m_Table = new Hashtable();

		public static bool RemoveCurse( Mobile m )
		{
			Timer t = (Timer)m_Table[m];

			if ( t == null )
				return false;

			t.Stop();
			m.SendLocalizedMessage( 1061687 ); // You can breath normally again.

			m_Table.Remove( m );
			return true;
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Target, m_From;
			private double m_MinBaseDamage, m_MaxBaseDamage;

			private DateTime m_NextHit;
			private int m_HitDelay;

			private int m_Count, m_MaxCount;

			public InternalTimer( Mobile target, Mobile from ) : base( TimeSpan.FromSeconds( 0.1 ), TimeSpan.FromSeconds( 0.1 ) )
			{
				Priority = TimerPriority.FiftyMS;

				m_Target = target;
				m_From = from;

				double spiritLevel = from.Skills[SkillName.Meditation].Value / 7;

				m_MinBaseDamage = spiritLevel - 2;
				m_MaxBaseDamage = spiritLevel + 1;

				m_HitDelay = 5;
				m_NextHit = DateTime.Now + TimeSpan.FromSeconds( m_HitDelay );

				m_Count = (int)spiritLevel;

				if ( m_Count < 4 )
					m_Count = 4;

				m_MaxCount = m_Count;
			}

			protected override void OnTick()
			{
				if ( !m_Target.Alive )
				{
					m_Table.Remove( m_Target );
					Stop();
				}

				if ( !m_Target.Alive || DateTime.Now < m_NextHit )
					return;

				--m_Count;

				if ( m_HitDelay > 1 )
				{
					if ( m_MaxCount < 5 )
					{
						--m_HitDelay;
					}
					else
					{
						int delay = (int)(Math.Ceiling( (1.0 + (5 * m_Count)) / m_MaxCount ) );

						if ( delay <= 5 )
							m_HitDelay = delay;
						else
							m_HitDelay = 5;
					}
				}

				if ( m_Count == 0 )
				{
					m_Target.SendLocalizedMessage( 1061687 ); // You can breath normally again.
					m_Table.Remove( m_Target );
					Stop();
				}
				else
				{
					m_NextHit = DateTime.Now + TimeSpan.FromSeconds( m_HitDelay );

					double damage = m_MinBaseDamage + (Utility.RandomDouble() * (m_MaxBaseDamage - m_MinBaseDamage));

					damage *= (5 - (((double)m_Target.Stam / m_Target.StamMax) * 2));

					if ( damage < 3 )
						damage = 3;

					if ( !m_Target.Player )
						damage *= 1.75;

					AOS.Damage( m_Target, m_From, (int)damage, 0, 0, 0, 100, 0 );
				}
			}
		}

		private class InternalItem : Item
		{
			private Timer m_Timer;
			private DateTime m_End;

			public InternalItem( Point3D loc, Map map, Mobile caster ) : base( 0xC5F )
			{
				Visible = false;
				Movable = false;

				MoveToWorld( loc, map );

				if ( caster.InLOS( this ) )
					Visible = true;
				else
					Delete();

				if ( Deleted )
					return;

				m_Timer = new InternalTimer( this, TimeSpan.FromSeconds( 30.0 ) );
				m_Timer.Start();

				m_End = DateTime.Now + TimeSpan.FromSeconds( 30.0 );
			}

			public InternalItem( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );
				writer.Write( (int) 1 ); // version
				writer.Write( m_End - DateTime.Now );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );
				int version = reader.ReadInt();
				TimeSpan duration = reader.ReadTimeSpan();

				m_Timer = new InternalTimer( this, duration );
				m_Timer.Start();

				m_End = DateTime.Now + duration;
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if ( m_Timer != null )
					m_Timer.Stop();
			}

			private class InternalTimer : Timer
			{
				private InternalItem m_Item;

				public InternalTimer( InternalItem item, TimeSpan duration ) : base( duration )
				{
					m_Item = item;
				}

				protected override void OnTick()
				{
					m_Item.Delete();
				}
			}
		}
		
		public class InternalTarget : Target
		{
			private DarkStrangleSpell m_Owner;

			public InternalTarget( DarkStrangleSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile) o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}