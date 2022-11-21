using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;

namespace Server.Spells.Mystic
{
	public abstract class MysticSpell : Spell
	{
		public abstract int RequiredMana{ get; }
		public abstract double RequiredSkill{ get; }

		public MysticSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
		{
		}

		#region BaseSpellOverrides
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
		public override double CastDelayFastScalar{ get{ return 1.0; } }

		public override SkillName CastSkill{ get{ return SkillName.Mysticism; } }
		public override SkillName DamageSkill{ get{ return SkillName.Imbuing; } }

		public override void GetCastSkills( out double min, out double max )
		{
			min = RequiredSkill;
			max = RequiredSkill + 40.0;
		}

		public override int GetMana()
		{
			return RequiredMana;
		}

		public override TimeSpan GetCastRecovery()
		{
			if ( Scroll is SpellStone )
				return TimeSpan.Zero;

			return TimeSpan.FromSeconds( 0.75 );
		}

		public override TimeSpan GetCastDelay()
		{
			if ( Scroll is SpellStone )
				return TimeSpan.Zero;

			return TimeSpan.FromSeconds( 1.5 ); // TODO Delays
		}

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			return true;
		}

		public override void OnCast()
		{
			Caster.Target = new MysticSpellTarget( this, TargetFlags.None );
		}
		#endregion

		public virtual void OnTarget( Object o )
		{
		}

		// Ever wondered why in the hell RunUO coded a new target class for every spell?
		public class MysticSpellTarget : Target
		{
			private MysticSpell m_Owner;

			public MysticSpell Owner
			{
				get{ return m_Owner; }
				set{ m_Owner = value; }
			}

			public MysticSpellTarget( MysticSpell owner, TargetFlags flags ) : this( owner, false, flags )
			{
			}

			public MysticSpellTarget( MysticSpell owner, bool allowland, TargetFlags flags ) : base( 12, allowland, flags )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o == null ) 
					return;

				if ( !from.CanSee( o ) )
					from.SendLocalizedMessage( 500237 ); // Target can not be seen.
				else
				{
					SpellHelper.Turn( from, o );
					m_Owner.OnTarget( o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

	}
}
/*




*/