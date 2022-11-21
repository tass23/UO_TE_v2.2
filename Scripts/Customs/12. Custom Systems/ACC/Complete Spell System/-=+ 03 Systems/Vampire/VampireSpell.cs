using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Spellweaving;

namespace Server.ACC.CSS.Systems.Vampire
{
	public abstract class VampireSpell : CSpell
	{
		public VampireSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
		{
		}
        
        public abstract SpellCircle Circle { get; }
        
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(3 * CastDelaySecondsPerTick); } }
        public override SkillName CastSkill { get { return SkillName.SpiritSpeak; } }
        public override SkillName DamageSkill { get { return SkillName.Focus; } }

        public override bool ClearHandsOnCast { get { return false; } }
		
		private int m_CastTimeFocusLevel;
		
		public virtual int FocusLevel
		{
			get { return m_CastTimeFocusLevel; }
		}

        public int ComputePowerValue(int div)
        {
            return ComputePowerValue(Caster, div);
        }

        public static int ComputePowerValue(Mobile from, int div)
        {
            if (from == null)
                return 0;

            int v = (int)Math.Sqrt(from.Karma + 20000 + (from.Skills.Focus.Fixed * 50));

            return v / div;
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

