using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Targeting;

namespace Server.ACC.CSS.Systems.Mysticism
{
	public class NetherBoltSpell : MysticismSpell
	{
		// Fires a bolt of nether energy at the Target, dealing chaos damage.

		public override int RequiredMana{ get{ return 4; } }
		public override double RequiredSkill{ get{ return 0; } }

		private static SpellInfo m_Info = new SpellInfo(
				"Nether Bolt", "In Corp Ylem",
				230,
				9022,
				Reagent.BlackPearl,
				Reagent.SulfurousAsh
			);
		
		public override SpellCircle Circle
    {
      get { return SpellCircle.Fourth; }
    }

		public NetherBoltSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool DelayedDamage{ get{ return true; } }

		public override void OnCast()
		{
			Caster.Target = new MysticismSpellTarget( this, TargetFlags.Harmful );
		}

		public override void OnTarget( Object o )
		{
			Mobile target = o as Mobile;

			if ( target == null )
			{
				return;
			}
			else if ( CheckHSequence( target ) )
			{
				double damage = GetNewAosDamage( 10, 1, 4, target );
				int hue = 0;

				switch( Utility.Random( 5 ) )
				{
					case 0: { SpellHelper.Damage( this, target, damage, 100, 0, 0, 0, 0 ); hue = 1908; } break;
					case 1: { SpellHelper.Damage( this, target, damage, 0, 100, 0, 0, 0 ); hue = 1355; } break;
					case 2: { SpellHelper.Damage( this, target, damage, 0, 0, 100, 0, 0 ); hue = 1361; } break;
					case 3: { SpellHelper.Damage( this, target, damage, 0, 0, 0, 100, 0 ); hue = 1367; } break;
					default: { SpellHelper.Damage( this, target, damage, 0, 0, 0, 0, 100 ); hue = 1373; } break;
				}

				target.BoltEffect( hue );
				Caster.PlaySound( 0x654 );
			}

			FinishSequence();
		}
	}
}