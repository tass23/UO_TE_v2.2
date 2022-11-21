using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.ACC.CSS.Systems.Ancient;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.DarkForce
{
    public class DarkMindTrickSpell : DarkForceSpell
    {
        private Timer m_Timer;

        private static SpellInfo m_Info = new SpellInfo(
        "Mind Trick", "Through victory, my chains are broken.",
        218,
        9012,
        Reagent.NoxCrystal,
        Reagent.PigIron,
        Reagent.BatWing,
		Reagent.GraveDust
        );

        public override SpellCircle Circle
        {
            get { return SpellCircle.Fifth; }
        }
		
        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 38; } }
        public override int RequiredMana { get { return 14; } }

        public DarkMindTrickSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            if (CheckSequence())
                Caster.Target = new InternalTarget(this);
        }

		public override bool CheckCast()
		{
			if ( Caster.Karma > 5000 )
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
		
		public void Target( BaseCreature bc )
		{
			if ( !Caster.CanSee( bc.Location ) || !Caster.InLOS( bc ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( !IsValidTarget( bc ) )
			{
				Caster.SendMessage( "You cannot use your mind trick on that!" ); // You cannot charm that!
			}
			else if ( Caster.Followers + 4 > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1049607 ); // You have too many followers to control that creature.
			}
			else if ( bc.Allured )
			{
				Caster.SendLocalizedMessage( 1074380 ); // This humanoid is already controlled by someone else.				
			}			
			else if ( CheckSequence() )
			{
				int level = GetFocusLevel( Caster );
				double skill = Caster.Skills[ CastSkill ].Value;

				double chance = ( skill / 150.0 ) + ( level / 50.0 );

				if ( chance > Utility.RandomDouble() )
				{
					bc.ControlSlots = 4;				
					bc.Combatant = null;
						
					if ( Caster.Combatant == bc )
					{
						Caster.Combatant = null;
						Caster.Warmode = false;
					}
					
					if ( bc.SetControlMaster( Caster ) )
					{
						bc.PlaySound( 0x5C4 );
						bc.Allured = true;
						
						Container pack = bc.Backpack;

						if ( pack != null )
						{
							for ( int i = pack.Items.Count - 1; i >= 0; --i )
							{
								if ( i >= pack.Items.Count )
									continue;
			
								pack.Items[i].Delete();
							}
						}
						
						Caster.SendMessage( "Your Mind Trick was successful!" ); // You allure the humanoid to follow and protect you.
					}
				}
				else
				{
					bc.PlaySound( 0x5C5 );
					bc.ControlTarget = Caster;
					bc.ControlOrder = OrderType.Attack;
					bc.Combatant = Caster;

					Caster.SendMessage( "You have no chance of using your Mind Trick on that!" ); // The humanoid becomes enraged by your charming attempt and attacks you.
				}
			}

			FinishSequence();
		}
		
		public static bool IsValidTarget( BaseCreature bc )
		{
			if ( bc == null || bc.IsParagon || ( bc.Controlled && !bc.Allured ) || bc.Summoned )
				return false;
				
			SlayerEntry slayer = SlayerGroup.GetEntryByName( SlayerName.Repond );
			
			if ( slayer != null && slayer.Slays( bc ) )
				return true;
			
			return false;
		}

		public class InternalTarget : Target
		{
			private DarkMindTrickSpell m_Owner;

			public InternalTarget( DarkMindTrickSpell owner ) : base( 12, false, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile m, object o )
			{
				if ( o is BaseCreature )
				{
					m_Owner.Target( (BaseCreature) o );
				}
				else
				{
					m.SendMessage( "You cannot use your mind trick on that!" ); // You cannot charm that!
				}
			}

			protected override void OnTargetFinish( Mobile m )
			{
				m_Owner.FinishSequence();
			}
		}
    }
}
