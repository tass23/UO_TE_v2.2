using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Spells;
using Server.Gumps;

namespace Server.ACC.CSS.Systems.DarkForce
{
	public class ForceDestructionSpell : DarkForceSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
			"Force Destruction", "The Dark Side magnifies my power!",
			203,
			9031,
			Reagent.NoxCrystal,
			Reagent.GraveDust,
			Reagent.PigIron
		);

        public override SpellCircle Circle
        {
            get { return SpellCircle.Eighth; }
        }
		
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.5 ); } }
		public override double RequiredSkill{ get{ return 80; } }
		public override int RequiredMana{ get{ return 50; } }

		public ForceDestructionSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
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
		
		public override bool DelayedDamage{ get{ return false; } }

		public override void OnCast()
		{
			Ability.SithMeteor( Caster, 10 );
			if ( CheckSequence() )
			{
				/* Creates a withering frost around the Caster,
				 * which deals Cold Damage to all valid targets in a radius of 5 tiles.
				 */

				Map map = Caster.Map;

				if ( map != null )
				{
					List<Mobile> targets = new List<Mobile>();

					foreach ( Mobile m in Caster.GetMobilesInRange( Core.ML ? 4 : 5 ) )
						if ( Caster != m && Caster.InLOS( m ) && SpellHelper.ValidIndirectTarget( Caster, m ) && Caster.CanBeHarmful( m, false ) )
							targets.Add( m );

					Effects.PlaySound( Caster.Location, map, 0x11E );
					Effects.PlaySound( Caster.Location, map, 0x15F );
					Effects.SendLocationParticles( EffectItem.Create( Caster.Location, map, EffectItem.DefaultDuration ), 0x3798, 1, 40, 97, 3, 9917, 0 );

					for ( int i = 0; i < targets.Count; ++i )
					{
						Mobile m = targets[i];

						Caster.DoHarmful( m );
						m.FixedParticles( 0x374A, 1, 10, 9502, 97, 3, (EffectLayer)255 );

						double damage = Utility.RandomMinMax( 30, 35 );

						damage *= (350 + (m.Karma / 100) + (GetDamageSkill( Caster ) * 12));
						damage /= 2000;

						int sdiBonus = AosAttributes.GetValue( Caster, AosAttribute.SpellDamage );
						
						// PvP spell damage increase cap of 15% from an item’s magic property in Publish 33(SE)
						if ( Core.SE && m.Player && Caster.Player && sdiBonus > 15 )
							sdiBonus = 15;
						
						damage *= ( 100 + sdiBonus );
						damage /= 100;

						// TODO: cap?
						//if ( damage > 40 )
						//	damage = 40;

						SpellHelper.Damage( this, m, damage, 0, 0, 100, 0, 0 );
					}
				}
			}

			FinishSequence();
		}
	}
}