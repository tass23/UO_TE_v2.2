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
    public class StealthSpell : LightForceSpell
    {
        private static SpellInfo m_Info = new SpellInfo(
        "Stealth", "A Jedi uses the Force for knowledge and defense, never for attack.",
        //SpellCircle.Fourth,
        212,
        9041,
        Reagent.SpidersSilk,
        Reagent.Garlic,
        Reagent.BlackPearl
        );

		public override SpellCircle Circle
        {
            get { return SpellCircle.Sixth; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 52; } }
        public override int RequiredMana { get { return 20; } }

        private static Dictionary<Mobile, SkillMod> m_Table = new Dictionary<Mobile, SkillMod>();

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

        public StealthSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public static void Remove(Mobile m)
        {
            if (m_Table.ContainsKey(m))
            {
                m.RemoveSkillMod(m_Table[m]);
                m_Table.Remove(m);
            }

            m.EndAction(typeof(StealthSpell));
        }

        public override void OnCast()
        {
            Caster.Hidden = true;
            if (Caster.CanBeginAction(typeof(StealthSpell)))
            {
                Caster.BeginAction(typeof(StealthSpell));
                DefaultSkillMod mod = new DefaultSkillMod(SkillName.Focus, true, 50.0);
                m_Table.Add(Caster, mod);
                Caster.AddSkillMod(mod);

                new InternalTimer(Caster, DateTime.Now.AddSeconds(15*(Caster.Skills[DamageSkill].Base / 10))).Start();
            }
            else
                Caster.SendMessage("You are already in the shadows!");
        }

        private class InternalTimer : Timer
        {
            private Mobile m_Owner;
            private DateTime m_ExpiresAt;

            public InternalTimer(Mobile owner, DateTime expiresAt)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(15))
            {
                m_Owner = owner;
                m_ExpiresAt = expiresAt;
            }

            protected override void OnTick()
            {
                if (DateTime.Now >= m_ExpiresAt)
                {
                    StealthSpell.Remove(m_Owner);
                    Stop();
                }
            }
        }
    }
}