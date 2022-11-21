using Server;
using System;
using Server.Items;
using Server.Spells;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using Server.Spells.Seventh;
using Server.Spells.Fourth;
using Server.Spells.Sixth;

namespace Server.Regions
{
	public class CityLockDownTarget : Target
	{
		private CityManagementStone m_Stone;

		public CityLockDownTarget( CityManagementStone stone ) : base( 12, false, TargetFlags.None )
		{
			m_Stone = stone;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Item )
			{
				Item item = (Item)targeted;
				
				if ( !PlayerGovernmentSystem.IsIteminCity( from, item ) )
				{
				    	from.SendMessage( "You may only lock down things in your city limits!" );
				    	return;
				}
				
				if ( item is ResourceBox )
				{
					if ( item is CityResourceBox )
					{
						if ( !PlayerGovernmentSystem.IsIteminCity( from, item ) )
						    {
						    	from.SendMessage( "You may only lock this down in the city proper!" );
						    	return;
						    }
					}
					
					else
					{
						from.SendMessage( "You may only lock this down in your house" );
						return;
					}
				}
				ArrayList decore = m_Stone.isLockedDown;

				if ( m_Stone.CurrentDecore == m_Stone.MaxDecore )
				{
					from.SendMessage( "You cannot secure anymore items in this city." );
				}
				else if ( item.Movable == true )
				{
					if ( decore == null )
					{
						m_Stone.isLockedDown = new ArrayList();
						decore = m_Stone.isLockedDown;
					}

					item.Movable = false;
					decore.Add( item );
					m_Stone.CurrentDecore += 1;
					from.SendMessage( "You secure the item." );
				}
				else
				{
					from.SendMessage( "That item cannot be locked down because it is already not movable." );
				}
			}
		}
	}

	public class CityReleaseTarget : Target
	{
		private CityManagementStone m_Stone;

		public CityReleaseTarget( CityManagementStone stone ) : base( 12, false, TargetFlags.None )
		{
			m_Stone = stone;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Item )
			{
				Item item = (Item)targeted;
				ArrayList decore = m_Stone.isLockedDown;

				if ( decore == null )
				{
					m_Stone.isLockedDown = new ArrayList();
					decore = m_Stone.isLockedDown;
				}

				bool isLockdown = false;

				foreach ( Item items in decore )
				{
					if ( item == items )
						isLockdown = true;
				}
				
				if ( isLockdown == false )
				{
					from.SendMessage( "That item was not locked down." );
				}
				else if ( item.Movable == false )
				{
					item.Movable = true;
					decore.Remove( item );
					m_Stone.CurrentDecore -= 1;
					from.SendMessage( "You have released the item." );
				}
				else
				{
					from.SendMessage( "That item is already released." );
				}
			}
		}
	}

	public class CityBanTarget : Target
	{
		private CityManagementStone m_Stone;

		public CityBanTarget( CityManagementStone stone ) : base( 12, false, TargetFlags.None )
		{
			m_Stone = stone;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Mobile )
			{
				Mobile mob = (Mobile)targeted;
				ArrayList banned = m_Stone.Banned;

				if ( PlayerGovernmentSystem.CheckIfHouseInCity( mob, m_Stone.PCRegion ) )
				{
					from.SendMessage( "You cannot ban someone who owns a house in the city." );
				}
				else if ( mob == from )
				{
					from.SendMessage( "You cant ban yourself." );
				}
				else if ( PlayerGovernmentSystem.MaxBannedPerCity == 0 )
				{
					from.SendMessage( "City banning has been disabled." );
				}
				else if ( banned.Count >= PlayerGovernmentSystem.MaxBannedPerCity )
				{
					from.SendMessage( "You cannot ban anymore players from this city, The ban list is full." );
				}
				else if ( mob.AccessLevel == AccessLevel.Player )
				{
					from.SendMessage( "You have banned them from the city." );
					mob.SendMessage( "You have been banned from this city." );
					banned.Add( mob );
					// TODO: Set up a ban location.
				}
				else
				{
					from.SendMessage( "You cant ban a staff member." );
				}
			}
		}
	}

	public class PlayerCityRegion : GuardedRegion
	{
		private CityManagementStone m_Stone;
		
		

		public CityManagementStone Stone
		{
			get{ return m_Stone; }
		}
		
		public static string PickRegionName()  //Dissallows duplicate region names
		{
			string name = "PlayerCity";
			int rndm = Utility.Random( 100000 );
			name += rndm.ToString();
			return name;
		
			
		}

		public PlayerCityRegion( CityManagementStone stone, Map map, Rectangle3D[] area ) : base( PickRegionName(), map, DefaultPriority, area )
		{
			//LoadFromXml = false;

			m_Stone = stone;
			
			
		}

		public override bool IsDisabled()
		{
			if ( m_Stone.IsGuarded == false )
				return !Disabled;
			else
				return Disabled;
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			foreach ( Mobile mob in m_Stone.Banned )
			{
				if ( mob == from )
				{
					from.SendMessage( "You cant place a house in a city your banned from." );
					return false;
				}
			}

			return m_Stone.AllowHousing;
		}

		public override bool AllowSpawn()
		{
			//return false;
			return true;
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !from.Alive )
				return;

			else if ( e.HasKeyword( 0x23 ) ) // I wish to lock this down
			{
				if ( from == m_Stone.Mayor )
				{
					from.Target = new CityLockDownTarget( m_Stone );
					from.SendMessage( "What would you like to secure?" );
				}
			}
			else if ( e.HasKeyword( 0x24 ) ) // I wish to release this
			{
				if ( from == m_Stone.Mayor )
				{
					from.Target = new CityReleaseTarget( m_Stone );
					from.SendMessage( "What would you like to release?" );
				}
			}
			else if ( e.HasKeyword( 0x34 ) ) // I ban thee
			{
				if ( from == m_Stone.Mayor )
				{
					from.Target = new CityBanTarget( m_Stone );
					from.SendMessage( "Who do you wish to ban?" );
				}
			}
			
			base.OnSpeech( e );
		}

		public override void OnExit( Mobile m )
		{
						
			
			if ( m_Stone != null )
			{
				 if ( m_Stone.Level == 1 )
					m.SendMessage( "You have left the {0} of {1}.", PlayerGovernmentSystem.Title1, m_Stone.CityName );
				else if ( m_Stone.Level == 2 )
					m.SendMessage( "You have left the {0} of {1}.", PlayerGovernmentSystem.Title2, m_Stone.CityName );
				else if ( m_Stone.Level == 3 )
					m.SendMessage( "You have left the {0} of {1}.", PlayerGovernmentSystem.Title3, m_Stone.CityName );
				else if ( m_Stone.Level == 4 )
					m.SendMessage( "You have left the {0} of {1}.", PlayerGovernmentSystem.Title4, m_Stone.CityName );
				else if ( m_Stone.Level == 5 )
					m.SendMessage( "You have left the {0} of {1}.", PlayerGovernmentSystem.Title5, m_Stone.CityName );
				else if ( m_Stone.Level == 6 )
					m.SendMessage( "You have left the {0} of {1}.", PlayerGovernmentSystem.Title6, m_Stone.CityName );
				else
					m.SendMessage( "You have left {0}.", m_Stone.CityName );
			}
			else
			{
				m.SendMessage( 38, "ERROR: City Not Linked To City Hall, Contact Staff." );
			}

			base.OnExit( m );

		}

		public override void OnEnter( Mobile m )
		{
			bool isBanned = false;

			if ( m_Stone != null && m_Stone.Banned != null )
			{
				foreach ( Mobile mob in m_Stone.Banned )
				{
					if ( m == mob )
						isBanned = true;
				}

				if ( isBanned == true )
				{
					m.SendMessage( 38, "You are banned from this city, You will be attackable by any of this cities citizens while in city limits." );
				}
			}

			if ( m_Stone != null )
			{
				if ( m_Stone.Level == 1 )
					m.SendMessage( "You have entered the {0} of {1}.", PlayerGovernmentSystem.Title1, m_Stone.CityName );
				else if ( m_Stone.Level == 2 )
					m.SendMessage( "You have entered the {0} of {1}.", PlayerGovernmentSystem.Title2, m_Stone.CityName );
				else if ( m_Stone.Level == 3 )
					m.SendMessage( "You have entered the {0} of {1}.", PlayerGovernmentSystem.Title3, m_Stone.CityName );
				else if ( m_Stone.Level == 4 )
					m.SendMessage( "You have entered the {0} of {1}.", PlayerGovernmentSystem.Title4, m_Stone.CityName );
				else if ( m_Stone.Level == 5 )
					m.SendMessage( "You have entered the {0} of {1}.", PlayerGovernmentSystem.Title5, m_Stone.CityName );
				else if ( m_Stone.Level == 6 )
					m.SendMessage( "You have entered the {0} of {1}.", PlayerGovernmentSystem.Title6, m_Stone.CityName );
				else
					m.SendMessage( "You have entered {0}.", m_Stone.CityName );
			}
			else
			{
				m.SendMessage( 38, "ERROR: City Not Linked To City Hall, Contact Staff." );
			}

			base.OnEnter( m );
		}
		
			public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
			if ( ( s is GateTravelSpell || s is RecallSpell ) && m.AccessLevel == AccessLevel.Player )
			{
				m.SendMessage( "You cannot cast that spell here." );
				return false;
			}
			else
			{
				return base.OnBeginSpellCast( m, s );
			}
		}
	}
}
