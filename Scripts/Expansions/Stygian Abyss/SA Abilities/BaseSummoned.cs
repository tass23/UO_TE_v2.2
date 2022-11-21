// Created by Peoharen for the Mobile Abilities Package.
using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a corpse" )]
	public class BaseSummoned : BaseCreature
	{
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }
		public override bool IsDispellable { get { return true; } }

		private DateTime m_DecayTime;
		public virtual TimeSpan m_Delay{ get{ return TimeSpan.FromMinutes( 2.0 ); } }

		public BaseSummoned( AIType aitype, FightMode fightmode, int spot, int meleerange, double passivespeed, double activespeed ) : base( aitype, fightmode, spot, meleerange, passivespeed, activespeed )
		{
			m_DecayTime = DateTime.Now + m_Delay;
		}

		public override void OnThink()
		{
			if ( DateTime.Now > m_DecayTime )
			{
				this.FixedParticles( 14120, 10, 15, 5012, EffectLayer.Waist );
				this.PlaySound( 510 );
				this.Delete();
			}
		}

		public BaseSummoned(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			m_DecayTime = DateTime.Now + TimeSpan.FromMinutes( 1.0 );
		}
	}
}