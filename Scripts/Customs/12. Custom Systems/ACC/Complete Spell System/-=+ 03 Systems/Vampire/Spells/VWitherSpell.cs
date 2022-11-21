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

namespace Server.ACC.CSS.Systems.Vampire
{
	public class VWitherSpell : VampireSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		"Vampiric Presence", "vampiric prezenta",
		//SpellCircle.Sixth,
		206,
		9002,
		false,
		Reagent.Bloodmoss,
		Reagent.Nightshade
		);

		public override SpellCircle Circle
        {
            get { return SpellCircle.Sixth; }
        }

        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
        public override double RequiredSkill { get { return 99; } }
        public override int RequiredMana { get { return 20; } }
		
		public VWitherSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

		public override bool DelayedDamage{ get{ return false; } }

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				/* Creates a withering frost around the Caster,
				 * which deals Cold Damage to all valid targets in a radius of 5 tiles.
				 */

				Map map = Caster.Map;

				if ( map != null )
				{
					List<Mobile> targets = new List<Mobile>();

					BaseCreature cbc = Caster as BaseCreature;
					bool isMonster = ( cbc != null && !cbc.Controlled && !cbc.Summoned );

					foreach ( Mobile m in Caster.GetMobilesInRange( Core.ML ? 4 : 5 ) )
					{
						if( Caster != m && Caster.InLOS( m ) && ( isMonster || SpellHelper.ValidIndirectTarget( Caster, m ) ) && Caster.CanBeHarmful( m, false ) )
						{
							if ( isMonster )
							{
								if ( m is BaseCreature )
								{
									BaseCreature bc = (BaseCreature)m;

									if ( !bc.Controlled && !bc.Summoned && bc.Team == cbc.Team )
										continue;
								}
								else if ( !m.Player )
								{
									continue;
								}
							}

							targets.Add( m );
						}
					}

					Effects.PlaySound( Caster.Location, map, 0x203 );
					Effects.PlaySound( Caster.Location, map, 0x108 );
					Effects.SendLocationParticles( EffectItem.Create( Caster.Location, map, EffectItem.DefaultDuration ), 0x3728, 1, 40, 97, 38, 9917, 0 );

					for ( int i = 0; i < targets.Count; ++i )
					{
						Mobile m = targets[i];

						Caster.DoHarmful( m );
						m.FixedParticles( 0x374A, 1, 15, 9502, 38, 3, (EffectLayer)255 );

						double damage = Utility.RandomMinMax( 30, 35 );

						damage *= (300 + (m.Karma / 100) + (GetDamageSkill( Caster ) * 10));
						damage /= 1000;

						int sdiBonus = AosAttributes.GetValue( Caster, AosAttribute.SpellDamage );
						
						// PvP spell damage increase cap of 15% from an item’s magic property in Publish 33(SE)
						if ( Core.SE && m.Player && Caster.Player && sdiBonus > 15 )
							sdiBonus = 15;
						
						damage *= ( 100 + sdiBonus );
						damage /= 100;

						// TODO: cap?
						//if ( damage > 40 )
						//	damage = 40;

						SpellHelper.Damage( this, m, damage, 0, 0, 0, 0, 100 );
					}
				}
			}

			FinishSequence();
		}
	}
}