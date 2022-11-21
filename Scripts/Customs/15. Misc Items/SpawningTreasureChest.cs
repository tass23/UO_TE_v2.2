using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.SpellCrafting.Items;
using Server.SpellCrafting;

namespace Server.Items
{
	public class SpawningTreasureChest : WoodenChest
	{
		private DateTime m_DecayTime;
		private Timer m_DecayTimer;

		public virtual TimeSpan DecayDelay{ get{ return TimeSpan.FromMinutes( 30.0 ); } }

		[Constructable]
		public SpawningTreasureChest( int low, int high ) : this( Utility.RandomMinMax( low < 15 ? 15 : low, high < low ? low : high ) )
		{
		}

		[Constructable]
		public SpawningTreasureChest() : this( 400, 2000 )
		{
		}

		[Constructable]
		public SpawningTreasureChest( int amount ) : base()
		{
			RefreshDecay( true );
			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( CheckAddComponents ) );

			Weight = 5.0;
			Name = "treasure chest";
			Movable = false;

			switch ( Utility.Random( 3 ) )  // modify as necessary
			{
				case 0: TrapType = TrapType.None; break;
				case 1: TrapType = TrapType.None; break;
				case 2: TrapType = TrapType.ExplosionTrap; break;
			}

			TrapPower = amount < 100 ? Utility.RandomMinMax( amount - 15, amount ) : 100;
					
			Locked = true;
			LockLevel = Utility.RandomMinMax( 40, 100 );
			MaxLockLevel = amount;

		}

		public void CheckAddComponents()
		{
			if( Deleted )
				return;

			AddComponents();
		}

		public virtual void AddComponents()
		{
		}

		public virtual void RefreshDecay( bool setDecayTime )
		{
			if( Deleted )
				return;

			if( m_DecayTimer != null )
				m_DecayTimer.Stop();

			if( setDecayTime )
				m_DecayTime = DateTime.Now + DecayDelay;

			m_DecayTimer = Timer.DelayCall( DecayDelay, new TimerCallback( Delete ) );
		}

		public SpawningTreasureChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.WriteDeltaTime( m_DecayTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_DecayTime = reader.ReadDeltaTime();
					RefreshDecay( false );
					break;
				}
			}
		}
	}
}