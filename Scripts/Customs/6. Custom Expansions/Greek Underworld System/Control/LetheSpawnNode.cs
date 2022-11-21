using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Engines.Underworld;

namespace Server.Items
{
	public class LetheSpawnNode : Item
	{
		private UnderworldSystem sys;
		private DateTime m_NextSpawn;
		private Mobile m_CurrentSpawn;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime NextSpawn
		{
			get{ return m_NextSpawn; }
			set{ m_NextSpawn = value; }
		}

		[Constructable]
		public LetheSpawnNode( UnderworldSystem s ) : base( 3699 )
		{
			Name = "Underworld Spawn Node";
			Movable = false;
			Visible = false;
			sys = s;
			new InternalTimer( this, sys ).Start();
		}

		public override void Delete()
		{
			if ( this.m_CurrentSpawn != null )
				this.m_CurrentSpawn.Delete();

			if ( sys.SpawnNodes != null && sys.SpawnNodes.Count > 0 )
				sys.SpawnNodes.Remove( this );

			base.Delete();
		}

		public bool CheckSpawn()
		{
			if ( this != null )
			{
				if ( this.m_CurrentSpawn == null && m_NextSpawn < DateTime.Now )
					return true;
				else if ( !this.m_CurrentSpawn.Alive && sys.CurrentStage == 1 )
				{
					this.m_CurrentSpawn.Delete();
					this.m_CurrentSpawn = null;
					sys.RegularKillCount += 1;
					sys.DoRandomDrops( this.Location );
					m_NextSpawn = DateTime.Now + TimeSpan.FromMinutes( 1.0 );
					return false;
				}
				else if ( !this.m_CurrentSpawn.Alive && sys.CurrentStage == 2 )
				{
					this.m_CurrentSpawn.Delete();
					this.m_CurrentSpawn = null;
					sys.DoRandomDrops( this.Location );
					m_NextSpawn = DateTime.Now + TimeSpan.FromMinutes( 1.0 );
					return false;
				}
				return false;
			}
			return false;
		}

		public void DoSpawn()
		{
			int chance = Utility.Random( 1, 100 );
			if ( chance < 70 )
			{
				Cacodemon minion = new Cacodemon();
				this.m_CurrentSpawn = minion;
				minion.Home = this.Location;
				minion.RangeHome = 2;
				minion.MoveToWorld( this.Location, this.Map );
			}
			else
			{
				Arae minion = new Arae();
				this.m_CurrentSpawn = minion;
				minion.Home = this.Location;
				minion.RangeHome = 2;
				minion.MoveToWorld( this.Location, this.Map );
			}
		}

		public LetheSpawnNode( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( (Mobile) m_CurrentSpawn );
			writer.Write( sys );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_CurrentSpawn = reader.ReadMobile();
			sys = reader.ReadItem() as UnderworldSystem;
			new InternalTimer( this, sys ).Start();
		}

		private class InternalTimer : Timer
		{
			private LetheSpawnNode node;
			private UnderworldSystem system;

			public InternalTimer( LetheSpawnNode n, UnderworldSystem sy ) : base( TimeSpan.FromSeconds( 1.0 ))
			{
				node = n;
				system = sy;
			}

			protected override void OnTick()
			{
				if ( node != null )
				{
					if ( node.NextSpawn < DateTime.Now && node.CheckSpawn() )
						node.DoSpawn();

					new InternalTimer( node, system ).Start();
				}
			}
		}
	}
}