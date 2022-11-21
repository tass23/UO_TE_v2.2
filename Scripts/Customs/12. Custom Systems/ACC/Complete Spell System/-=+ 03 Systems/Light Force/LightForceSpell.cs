using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Spellweaving;

namespace Server.ACC.CSS.Systems.LightForce
{
	public abstract class LightForceSpell : CSpell
	{
		public LightForceSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
		{
		}
        
        public abstract SpellCircle Circle { get; }
        
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(3 * CastDelaySecondsPerTick); } }
        public override SkillName CastSkill { get { return SkillName.Focus; } }
        public override SkillName DamageSkill { get { return SkillName.Meditation; } }

        public override bool ClearHandsOnCast { get { return true; } }
		
		private int m_CastTimeFocusLevel;
		
		public virtual int FocusLevel
		{
			get { return m_CastTimeFocusLevel; }
		}
		
		public static int GetFocusLevel( Mobile from )
		{
			ArcaneFocus focus = FindArcaneFocus( from );

			if( focus == null || focus.Deleted )
				return 0;

			return focus.StrengthBonus;
		}

		public static ArcaneFocus FindArcaneFocus( Mobile from )
		{
			if( from == null || from.Backpack == null )
				return null;

			if ( from.Holding is ArcaneFocus )
				return (ArcaneFocus)from.Holding;

			return from.Backpack.FindItemByType<ArcaneFocus>();
		}

        public int ComputePowerValue(int div)
        {
            return ComputePowerValue(Caster, div);
        }

        public static int ComputePowerValue(Mobile from, int div)
        {
            if (from == null)
                return 0;

            int v = (int)Math.Sqrt(from.Karma + 20000 + (from.Skills.Meditation.Fixed * 50));

            return v / div;
        }
		
		public override int ComputeKarmaAward()
		{
			//TODO: Verify this formula being that Necro spells don't HAVE a circle.

			//return -(70 + (10 * (int)Circle));

			return (40 + (int)(10 * (CastDelayBase.TotalSeconds / CastDelaySecondsPerTick)));
		}
		
        public virtual bool CheckResisted(Mobile target)
        {
            double n = GetResistPercent(target);

            n /= 100.0;

            if (n <= 0.0)
                return false;

            if (n >= 1.0)
                return true;

            int maxSkill = (1 + (int)Circle) * 10;
            //int maxSkill = 40;
            maxSkill += (1 + ((int)Circle / 6)) * 25;
            //maxSkill += (1 + (4 / 6)) * 25;

            if (target.Skills[SkillName.MagicResist].Value < maxSkill)
                target.CheckSkill(SkillName.MagicResist, 0.0, 120.0);

            return (n >= Utility.RandomDouble());
        }

        public virtual double GetResistPercent(Mobile target)
        {
            return GetResistPercentForCircle(target, Circle);
            //return GetResistPercentForCircle(target, SpellCircle.Fourth);
        }

        public virtual double GetResistPercentForCircle(Mobile target, SpellCircle circle)
        {
            double firstPercent = target.Skills[SkillName.MagicResist].Value / 5.0;
            double secondPercent = target.Skills[SkillName.MagicResist].Value - (((Caster.Skills[CastSkill].Value - 20.0) / 5.0) + (1 + (int)circle) * 5.0);

            return (firstPercent > secondPercent ? firstPercent : secondPercent) / 2.0; // Seems should be about half of what stratics says.
        }
		
		public override void GetCastSkills( out double min, out double max )
		{
			min = RequiredSkill;
			max = RequiredSkill;
		}

		public override int GetMana()
		{
			return RequiredMana;
		}

		public override TimeSpan GetCastDelay()
		{
			return TimeSpan.FromSeconds( CastDelay );
		}
	}
}

