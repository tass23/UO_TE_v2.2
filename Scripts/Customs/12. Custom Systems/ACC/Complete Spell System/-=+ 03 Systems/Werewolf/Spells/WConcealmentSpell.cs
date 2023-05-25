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

namespace Server.ACC.CSS.Systems.Werewolf
{
	public class WConcealmentSpell : WerewolfSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Lurking", "*whispers*",
		//SpellCircle.Sixth,
		206,
		9002,
		false,
		Reagent.MandrakeRoot,
		Reagent.BlackPearl
		);

		public override SpellCircle Circle
        {
            get { return SpellCircle.Sixth; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 24; } }
        public override int RequiredMana { get { return 11; } }
		private bool speak;
		private static Dictionary<Mobile, SkillMod> m_Table = new Dictionary<Mobile, SkillMod>();
		
		public WConcealmentSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override bool CheckCast()
		{
			PlayerMobile pm = (PlayerMobile) Caster;
			if ( pm.Werewolf == 0 )
			{
				Caster.SendMessage( "Only a werewolf may attempt something like this." ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			if ( pm.Werewolf == 1 )
			{
				if ( pm.BodyMod != 0xE1 )
				{
					Caster.SendMessage( "You must be in Wolf form to conceal yourself." );
					return false;
				}
				else
				{
					Caster.SendMessage( "You have blended in to your surroundings." );
					return true;
				}
			}				
			else
			{
				Caster.CloseGump( typeof( WerewolfGump ) );
				Caster.SendGump( new WerewolfGump() );
				return true;
			}
		}

        public static void Remove(Mobile m)
        {
            if (m_Table.ContainsKey(m))
            {
                m.RemoveSkillMod(m_Table[m]);
                m_Table.Remove(m);
            }

            m.EndAction(typeof(WConcealmentSpell));
        }

        public override void OnCast()
        {
            Caster.Hidden = true;
            if (Caster.CanBeginAction(typeof(WConcealmentSpell)))
            {
                Caster.BeginAction(typeof(WConcealmentSpell));
                DefaultSkillMod mod = new DefaultSkillMod(SkillName.AnimalLore, true, 50.0);
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
                    WConcealmentSpell.Remove(m_Owner);
                    Stop();
                }
            }
        }
    }
}