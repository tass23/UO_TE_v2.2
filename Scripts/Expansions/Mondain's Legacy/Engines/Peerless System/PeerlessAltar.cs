using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Engines.PartySystem;

namespace Server.Items
{
	public abstract class PeerlessAltar : Container
	{
		public override bool IsPublicContainer{ get{ return true; } }
		public override bool IsDecoContainer{ get{ return false; } }
	
		public virtual TimeSpan TimeToSlay{ get{ return TimeSpan.Zero; } }
		public virtual TimeSpan DelayAfterBossSlain{ get{ return TimeSpan.FromMinutes( 15 ); } }
	
		public abstract int KeyCount{ get; }
		public abstract MasterKey MasterKey{ get; }
		
		public abstract Type[] Keys{ get; }
		public abstract BasePeerless Boss{ get; }
				
		private BasePeerless m_Peerless;
		private Point3D m_BossLocation;
		private Point3D m_TeleportDest;
		private Point3D m_ExitDest;
		private DateTime m_Deadline;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public BasePeerless Peerless
		{
			get{ return m_Peerless; }
			set{ m_Peerless = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D BossLocation
		{
			get{ return m_BossLocation; }
			set{ m_BossLocation = value; }
		}	
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D TeleportDest
		{
			get{ return m_TeleportDest; }
			set{ m_TeleportDest = value; }
		}		
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D ExitDest
		{
			get{ return m_ExitDest; }
			set{ m_ExitDest = value; }
		}		
		
		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime Deadline
		{
			get{ return m_Deadline; }
			set{ m_Deadline = value; }
		}	
					
		private List<Mobile> m_Fighters;
		private Dictionary<Mobile,List<Mobile>> m_Pets;
		private List<Item> m_MasterKeys;	
		
		public List<Mobile> Fighters
		{
			get{ return m_Fighters; }
		}
		
		public Dictionary<Mobile,List<Mobile>> Pets
		{
			get{ return m_Pets; }
		}
		
		public List<Item> MasterKeys
		{
			get{ return m_MasterKeys; }
		}	
		
		public bool Activated
		{
			get{ return ( m_Fighters.Count > 0 || Items.Count == Keys.Length ) ? true : false; }
		}
		
		public Mobile Summoner
		{
			get{ return m_Fighters[ 0 ]; }
		}
	
		public PeerlessAltar( int itemID ) : base( itemID )
		{
			Movable = false;
			
			m_Fighters = new List<Mobile>();
			m_Pets = new Dictionary<Mobile,List<Mobile>>();
			m_MasterKeys = new List<Item>();
		}
	
		public PeerlessAltar( Serial serial ) : base( serial )
		{
		}				
		
		public override bool OnDragDrop( Mobile from, Item dropped )
		{											
			if ( Activated )
			{					
				from.SendLocalizedMessage( 1075213 ); // The master of this realm has already been summoned and is engaged in combat.  Your opportunity will come after he has squashed the current batch of intruders!
				return false;
			}
			
			if ( !IsKey( dropped ) )
			{
				from.SendLocalizedMessage( 1072682 ); // This is not the proper key.
				return false;
			}	
			
			if ( Items.Count + 1 == Keys.Length )
			{
				from.SendLocalizedMessage( 1072680 ); // You have been given the key to the boss.
				
				for ( int i = 0; i < KeyCount; i ++ )
				{
					MasterKey key = MasterKey;
					
					if ( key != null )
					{
						key.Altar = this;
						
						if ( !from.AddToBackpack( key ) )
							key.MoveToWorld( from.Location, from.Map );
							
						m_MasterKeys.Add( key );
					}						
				}
				
				dropped.Delete();				
				ClearContainer();
				StopTimer();
			}
			else
				StartTimer( from );
							
			return base.OnDragDrop( from, dropped );
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( (int) from.AccessLevel > (int) AccessLevel.Player )
				base.OnDoubleClick( from );
		}
		
		public override bool CheckLift( Mobile from, Item item, ref LRReason reject )
		{
			if ( (int) from.AccessLevel > (int) AccessLevel.Player )
				return base.CheckLift( from, item, ref reject );
			else
				reject = LRReason.CannotLift;
				
			return false;
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

			// version 1
			writer.Write( (bool) ( m_Helpers != null ) );
			
			if ( m_Helpers != null )
				writer.WriteMobileList<BaseCreature>( m_Helpers );

			// version 0			
			writer.Write( (Mobile) m_Peerless );
			writer.Write( (Point3D) m_BossLocation );
			writer.Write( (Point3D) m_TeleportDest );
			writer.Write( (Point3D) m_ExitDest );
			
			writer.Write( (DateTime) m_Deadline );
			
			// serialize master keys						
			writer.WriteItemList( m_MasterKeys );
			
			// serialize fighters							
			writer.WriteMobileList( m_Fighters );
				
			// serialize pets
			writer.Write( (int) m_Pets.Count );
			
			foreach ( KeyValuePair<Mobile,List<Mobile>> pair in m_Pets )
			{
				writer.Write( (Mobile) pair.Key );
				
				writer.WriteMobileList( pair.Value );
			}
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			switch ( version )
			{
				case 2:
				case 1:
					if ( reader.ReadBool() )
						m_Helpers = reader.ReadStrongMobileList<BaseCreature>();
					goto case 0;
				case 0:
					m_Peerless = reader.ReadMobile() as BasePeerless;
					m_BossLocation = reader.ReadPoint3D();
					m_TeleportDest = reader.ReadPoint3D();
					m_ExitDest = reader.ReadPoint3D();
					
					m_Deadline = reader.ReadDateTime();
					
					// deserialize master keys
					m_MasterKeys = reader.ReadStrongItemList();		
						
					// deserialize fightes			
					m_Fighters = reader.ReadStrongMobileList();
						
					// deserialize pets
					m_Pets = new Dictionary<Mobile,List<Mobile>>();
					int count = reader.ReadInt();
					
					for ( int i = 0; i < count; i ++ )
						m_Pets.Add( reader.ReadMobile(), reader.ReadStrongMobileList() );
					
					if ( version < 2 )
						reader.ReadBool();
					
					break;
			}			
			
			FinishSequence();
		}
		
		public virtual bool IsKey( Item item )
		{
			if ( Keys == null || item == null )
				return false;
				
			bool isKey = false;
			
			// check if item is key	
			for ( int i = 0; i < Keys.Length && !isKey; i ++ )
				if ( Keys[ i ].IsAssignableFrom( item.GetType() ) )
					isKey = true;
			
			// check if item is already in container			
			for ( int i = 0; i < Items.Count && isKey; i ++ )
				if ( Items[ i ].GetType() == item.GetType() )
					return false;
					
			return isKey;
		}
		
		public virtual void ClearContainer()
		{
			while( Items.Count > 0 )
				Items[ 0 ].Delete();
		}
		
		private int toConfirm;
		
		public virtual void AddFighter( Mobile fighter, bool confirmed )
		{
			if ( confirmed )
				AddFighter( fighter );
			
			toConfirm -= 1;
			
			if ( toConfirm == 0 )
				BeginSequence( Summoner );
		}
		
		public virtual void AddFighter( Mobile fighter )
		{
			m_Fighters.Add( fighter );		
				
			foreach ( Mobile m in fighter.GetMobilesInRange( 5 ) )
			{
				if ( m is BaseCreature )
				{
					BaseCreature pet = (BaseCreature) m;
					
					if ( pet.Controlled && pet.ControlMaster == fighter )
					{
						if ( !m_Pets.ContainsKey( fighter ) )
							m_Pets.Add( fighter, new List<Mobile>() );
							
						m_Pets[ fighter ].Add( pet );
					}
				}				
			}
			
			if ( fighter.Mounted )
			{
				if ( !m_Pets.ContainsKey( fighter ) )
					m_Pets.Add( fighter, new List<Mobile>() );
				
				if ( fighter.Mount is Mobile )
					m_Pets[ fighter ].Add( (Mobile) fighter.Mount );						
			}
		}
		
		public virtual void SendConfirmations( Mobile from )
		{
			Party party = Party.Get( from );	
			
			if ( party != null )
			{
				toConfirm = 0;
				
				foreach( PartyMemberInfo info in party.Members )
				{
					Mobile m = info.Mobile;
				
					if ( m.InRange( from.Location, 5 ) && CanEnter( m ) )
					{
						if ( m == from )
							AddFighter( from );
						else
						{
							toConfirm += 1;						
							
							m.CloseGump( typeof( ConfirmEntranceGump ) );		
							m.SendGump( new ConfirmEntranceGump( this ) );				
						}		
					}
				}
			}
			else
			{					
				AddFighter( from );
				BeginSequence( Summoner );
			}
		}
		
		public virtual void BeginSequence( Mobile from )
		{							
			if ( m_Peerless == null )
			{
				// spawn boss
				m_Peerless = Boss;
					
				if ( m_Peerless != null )
				{
					m_Peerless.Home = m_BossLocation;
					m_Peerless.RangeHome = 4;
					m_Peerless.MoveToWorld( m_BossLocation, Map );
					m_Peerless.Altar = this;
				}
				else
					return;
						
				// set deadline								
				if ( m_Timer != null )
					m_Timer.Stop();
				
				if ( TimeToSlay != TimeSpan.Zero )
				{
					m_Deadline = DateTime.Now + TimeToSlay;
					m_Timer = Timer.DelayCall( TimeSpan.FromMinutes( 5 ), TimeSpan.FromMinutes( 5 ), new TimerCallback( DeadlineCheck ) );	
					m_Timer.Priority = TimerPriority.OneMinute;
				}
			}
				
			// teleport figters
			for ( int i = 0; i < m_Fighters.Count; i ++ )
			{
				Mobile fighter = m_Fighters[ i ];
				int counter = 1;
				
				if ( from.InRange( fighter.Location, 5 ) && CanEnter( fighter ) )
				{
					Timer.DelayCall( TimeSpan.FromSeconds( counter ), new TimerStateCallback( Enter_Callback ), fighter );
											
					counter += 1;
				}
			}		
		}
		
		private void Enter_Callback( object state )
		{
			if ( state is Mobile )
				Enter( (Mobile) state );
		}
		
		public virtual void Enter( Mobile fighter )
		{				
			if ( CanEnter( fighter ) )
			{
				// teleport party member's pets
				if ( m_Pets.ContainsKey( fighter ) )
				{
					for ( int i = 0; i < m_Pets[ fighter ].Count; i ++ )
					{
						BaseCreature pet = m_Pets[ fighter ][ i ] as BaseCreature;
						
						if ( pet != null && pet.Alive && pet.InRange( fighter.Location, 5 ) && !( pet is BaseMount && ((BaseMount)pet).Rider != null ) && CanEnter( pet ) )
						{							
							pet.FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
							pet.PlaySound( 0x1FE );
							pet.MoveToWorld( m_TeleportDest, Map );
						}
					}	
				}	
				
				// teleport party member
				fighter.FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
				fighter.PlaySound( 0x1FE );
				fighter.MoveToWorld( m_TeleportDest, Map );						
			}
		}
		
		public virtual bool CanEnter( Mobile fighter )
		{
			return true;
		}
		
		public virtual bool CanEnter( BaseCreature pet )
		{
			return true;
		}
		
		public virtual void FinishSequence()
		{						
			StopTimer();
			
			// delete perless
			if ( m_Peerless != null )
			{
				if ( m_Peerless.Corpse != null )
					m_Peerless.Corpse.Delete();
			
				m_Peerless.Delete();
			}
			
			// teleport pary to exit if not already there				
			for ( int i = m_Fighters.Count - 1; i >= 0; i -- )
				Exit( m_Fighters[ i ] );
			
			// delete master keys				
			for ( int i = m_MasterKeys.Count - 1; i >= 0; i -- ) 
				m_MasterKeys[ i ].Delete();
				
			m_MasterKeys.Clear();				
			m_Fighters.Clear();		
			m_Pets.Clear();		
			
			// delete any remaining helpers
			CleanupHelpers();
			
			// reset summoner, boss		
			m_Peerless = null;
		}		
		
		public virtual void Exit( Mobile fighter )
		{
			// teleport fighter
			if ( fighter.NetState == null )
				fighter.LogoutLocation = m_ExitDest;
			else
			{	
				fighter.FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
				fighter.PlaySound( 0x1FE );
				
				if ( this is CitadelAltar )
					fighter.MoveToWorld( m_ExitDest, Map.Tokuno );
				else
					fighter.MoveToWorld( m_ExitDest, Map );
			}
			
			// teleport his pets
			if ( m_Pets.ContainsKey( fighter ) )
			{
				for ( int i = 0; i < m_Pets[ fighter ].Count; i ++ )
				{
					BaseCreature pet = m_Pets[ fighter ][ i ] as BaseCreature;
					
					if ( pet != null && ( pet.Alive || pet.IsBonded ) && pet.Map != Map.Internal )
					{							
						if ( pet is BaseMount )
						{
							BaseMount mount = (BaseMount) pet;
						
							if ( mount.Rider != null && mount.Rider != fighter )
							{	
								mount.Rider.FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
								mount.Rider.PlaySound( 0x1FE );								
								
								if ( this is CitadelAltar )
									mount.Rider.MoveToWorld( m_ExitDest, Map.Tokuno );
								else
									mount.Rider.MoveToWorld( m_ExitDest, Map );
							
								continue;
							}
							else if ( mount.Rider != null )
								continue;
						}
						
						pet.FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
						pet.PlaySound( 0x1FE );									
								
						if ( this is CitadelAltar )
							pet.MoveToWorld( m_ExitDest, Map.Tokuno );
						else
							pet.MoveToWorld( m_ExitDest, Map );
					}
				}	
			}
			
			m_Fighters.Remove( fighter );
			m_Pets.Remove( fighter );
			
			fighter.SendLocalizedMessage( 1072677 ); // You have been transported out of this room.
			
			if ( m_Fighters.Count == 0 )
				FinishSequence();
		}
		
		public virtual void OnPeerlessDeath()
		{
			SendMessage( 1072681 ); // The master of this realm has been slain! You may only stay here so long.
			
			if ( DelayAfterBossSlain != null )
				SendMessage( 1075611, DelayAfterBossSlain.TotalSeconds ); // Time left: ~1_time~ seconds
				
			StopTimer();
			
			// delete master keys				
			for ( int i = m_MasterKeys.Count - 1; i >= 0; i -- ) 
				m_MasterKeys[ i ].Delete();
				
			m_MasterKeys.Clear();
			
			m_Timer = Timer.DelayCall( DelayAfterBossSlain, new TimerCallback( FinishSequence ) );
		}
		
		public virtual void SendMessage( int message )
		{
			for ( int i = 0; i < m_Fighters.Count; i ++ )
				m_Fighters[ i ].SendLocalizedMessage( message );
		}
		
		public virtual void SendMessage( int message, object param )
		{				
			for ( int i = 0; i < m_Fighters.Count; i ++ )
				m_Fighters[ i ].SendLocalizedMessage( message, param.ToString() );
		}
		
		private Timer m_Timer;				
		
		public virtual void StartTimer( Mobile from )
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = Timer.DelayCall( TimeSpan.FromMinutes( 1 ), new TimerStateCallback( KeyTimeout_Callback ), from );
		}
		
		public virtual void StopTimer()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;
		}
		
		private void KeyTimeout_Callback( object state )
		{
			if ( state is Mobile )
				KeyTimeout( (Mobile) state );
		}
		
		public virtual void KeyTimeout( Mobile from )
		{				
			ClearContainer();
			
			from.SendLocalizedMessage( 1072679 ); // Your realm offering has reset. You will need to start over.
		}
		
		public virtual void DeadlineCheck()
		{
			if ( DateTime.Now > m_Deadline )
			{
				SendMessage( 1072258 ); // You failed to complete an objective in time!
				FinishSequence();
				return;
			}	
			
			TimeSpan timeLeft = m_Deadline - DateTime.Now;
			
			if ( timeLeft < TimeSpan.FromMinutes( 30 ) )
				SendMessage( 1075611, timeLeft.TotalSeconds );
				
			for ( int i = m_Fighters.Count - 1; i >= 0; i -- )
			{
				if ( m_Fighters[ i ] is PlayerMobile )
				{
					PlayerMobile player = (PlayerMobile) m_Fighters[ i ];
					
					if ( player.NetState == null )
					{					
						TimeSpan offline = DateTime.Now - player.LastOnline;
						
						if ( offline > TimeSpan.FromMinutes( 10 ) )
							Exit( player );
					}
				}
			}
		}
		
		#region Helpers
		private List<BaseCreature> m_Helpers = new List<BaseCreature>();

		public List<BaseCreature> Helpers
		{
			get{ return m_Helpers; }
		}
		
		public void AddHelper( BaseCreature helper )
		{			
			if ( helper != null && helper.Alive && !helper.Deleted )
				m_Helpers.Add( helper );
		}

		public bool AllHelpersDead()
		{
			for ( int i = m_Helpers.Count - 1; i >= 0; i-- )
			{
				BaseCreature c = m_Helpers[ i ];

				if ( c.Alive )
					return false;
			}

			return true;
		}
		
		public void CleanupHelpers()
		{
			for ( int i = m_Helpers.Count - 1; i >= 0 ; i -- )
			{
				BaseCreature c = m_Helpers[ i ];
				
				if ( c.Alive )
					c.Delete();
			}
			
			m_Helpers.Clear();
		}
		#endregion
	}
}