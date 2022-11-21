#define ChatEnabled

using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using Server.Targeting;
using System.Collections;
using Server.Misc;
using Server.Commands;
using System.Collections.Generic;
using Server.Accounting;
using Server.Multis;

#if (ChatEnabled )
using Knives.Chat3;
#endif

namespace Server.Items
{
	public class JoinCityTarget : Target
	{
		private CityManagementStone m_Stone;

		public JoinCityTarget( CityManagementStone stone ) : base( 12, false, TargetFlags.None )
		{
			m_Stone = stone;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Mobile )
			{
				Mobile targ = (Mobile)targeted;
				if ( targ == m_Stone.Mayor )
				{
					from.SendMessage( "You are already a member of this city." );
				}
				else if ( m_Stone.Citizens.Count >= PlayerGovernmentSystem.MaxCitizensPerCity )
				{
					from.SendMessage( "This city has reached its maxium citizens limit." );
				}
				else if ( from.InRange( m_Stone.GetWorldLocation(), 5 ) )
				{
					bool isMember = false;

					foreach ( Mobile m in m_Stone.Citizens )
					{
						if ( targ == m )
							isMember = true;
					}

					if ( isMember == true )
						from.SendMessage( "They are already a member of this city." );
					else
						targ.SendGump( new AcceptJoinGump( m_Stone, from ) );
				}
				else
				{
					from.SendMessage( "You are too far away from the stone to contiune." );
				}
			}
		}
	}

	public class CitySponsorTarget : Target
	{
		private CityManagementStone m_Stone;

		public CitySponsorTarget( CityManagementStone stone ) : base( 12, false, TargetFlags.None )
		{
			m_Stone = stone;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Mobile )
			{
				Mobile targ = (Mobile)targeted;
				if ( targ == m_Stone.Mayor )
				{
					from.SendMessage( "That person is already a member of this city." );
				}
				else if ( m_Stone.Citizens.Count >= PlayerGovernmentSystem.MaxCitizensPerCity )
				{
					from.SendMessage( "This city has reached its maxium citizens limit." );
				}
				else if ( from.InRange( m_Stone.GetWorldLocation(), 5 ) )
				{
					bool isMember = false;

					foreach ( Mobile m in m_Stone.Citizens )
					{
						if ( targ == m )
							isMember = true;
					}

					if ( isMember == true )
						from.SendMessage( "That person is already a member of this city." );
					else
						targ.SendGump( new SponsorGump( m_Stone, from ) );
				}
				else
				{
					from.SendMessage( "You are too far away from the stone to continue." );
				}
			}
		}
	}

	public class CityManagementStone : Item
	{
		private bool regionchanged = false;
		public bool mayorchange = false;
		private DateTime m_Time;

		private Timer m_Timer;

		private Mobile m_Mayor;
		private PlayerCityRegion m_Region;
		private ArrayList m_Coords;
		private bool m_IsGuarded;
		private bool m_AllowHouses;
		private int m_Level = 1;
		private string m_CityName;
		private ArrayList m_toDelete;
		private ArrayList m_Members;
		private int m_TravelTax;
		private int m_IncomeTax;
		private int m_HousingTax;
		private int m_CityTreasury = PlayerGovernmentSystem.TreasuryAmount;
		private string m_CityRules = "none";
		private string m_CityWebURL = "http://";
		private Point2D m_Center;
		private Point3D m_MoongateLocation;
		private ArrayList m_Sponsored;
		private ArrayList m_isLockedDown;
		private int m_MaxDecore = PlayerGovernmentSystem.Level1LD;
		private int m_CurrentDecore;
		private bool m_IsRegistered;
		private ArrayList m_Banned;

		private ArrayList m_Waring;
		private ArrayList m_WarsDeclared;
		private ArrayList m_WarsInvited;

		private ArrayList m_Allegiances;
		private ArrayList m_AllegiancesDeclared;
		private ArrayList m_AllegiancesInvited;

		private bool m_HasBank;
		private bool m_HasTavern;
		private bool m_HasHealer;
		private bool m_HasMoongate;
		private bool m_HasStable;

		private ArrayList m_Gardens;
		private ArrayList m_Parks;

		private int m_ResFee;
		private int m_CorpseFee;
		
		private ArrayList m_Vendors;
		private CityResurrectionStone m_resstone;
		private Mobile m_AssistMayor;
		private CityVotingStone m_votingstone;
		
		private Rectangle3D[] m_RegionCoords;
		private bool m_HasMarket;
		private ArrayList m_Addons;
		
		public ArrayList AddOns
		{
			get{ return m_Addons; }
			set{ m_Addons = value; }
		}

		public bool HasMarket
		{
			get{ return m_HasMarket; }
			set{ m_HasMarket = value; }
		}
		public Rectangle3D[] RegionCoords
		{
			get{ return m_RegionCoords; }
			set{ m_RegionCoords = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Mayor
		{
			get{ return m_Mayor; }
			set{ m_Mayor = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile AssistMayor
		{
			get{ return m_AssistMayor; }
			set{ m_AssistMayor = value; }
		}

		public CityResurrectionStone ResStone
		{
			get{ return m_resstone; }
			set{ m_resstone = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public CityVotingStone VoteStone
		{
			get{ return m_votingstone; }
			set{ m_votingstone = value; }
		}
		
		public PlayerCityRegion PCRegion
		{
			get{ return m_Region; }
			set{ m_Region = value; }
		}

		public ArrayList Coords
		{
			get{ return m_Coords; }
			set{ m_Coords = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsGuarded
		{
			get{ return m_IsGuarded; }
			set{ m_IsGuarded = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowHousing
		{
			get{ return m_AllowHouses; }
			set{ m_AllowHouses = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string CityName
		{
			get{ return m_CityName; }
			set{ m_CityName = value; }
		}

		public ArrayList toDelete
		{
			get{ return m_toDelete; }
			set{ m_toDelete = value; }
		}

		public ArrayList Citizens
		{
			get{ return m_Members; }
			set{ m_Members = value; }
		}
		
		public ArrayList Vendors
		{
			get{ return m_Vendors; }
			set{ m_Vendors = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int TravelTax
		{
			get{ return m_TravelTax; }
			set{ m_TravelTax = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int IncomeTax
		{
			get{ return m_IncomeTax; }
			set{ m_IncomeTax = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HousingTax
		{
			get{ return m_HousingTax; }
			set{ m_HousingTax = value; }
		}

		public int CityTreasury
		{
			get{ return m_CityTreasury; }
			set{ m_CityTreasury = value; }
		}

		public string CityRules
		{
			get{ return m_CityRules; }
			set{ m_CityRules = value; }
		}

		public string CityWebURL
		{
			get{ return m_CityWebURL; }
			set{ m_CityWebURL = value; }
		}

		public Point2D Center
		{
			get{ return m_Center; }
			set{ m_Center = value; }
		}
		
		public Point3D MoongateLocation
		{
			get{ return m_MoongateLocation; }
			set{ m_MoongateLocation = value; }
		}

		public ArrayList Sponsored
		{
			get{ return m_Sponsored; }
			set{ m_Sponsored = value; }
		}

		public ArrayList isLockedDown
		{
			get{ return m_isLockedDown; }
			set{ m_isLockedDown = value; }
		}

		public int MaxDecore
		{
			get{ return m_MaxDecore; }
			set{ m_MaxDecore = value; }
		}

		public int CurrentDecore
		{
			get{ return m_CurrentDecore; }
			set{ m_CurrentDecore = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsRegistered
		{
			get{ return m_IsRegistered; }
			set{ m_IsRegistered = value; }
		}

		public ArrayList Banned
		{
			get{ return m_Banned; }
			set{ m_Banned = value; }
		}

		public ArrayList Waring
		{
			get{ return m_Waring; }
			set{ m_Waring = value; }
		}

		public ArrayList WarsDeclared
		{
			get{ return m_WarsDeclared; }
			set{ m_WarsDeclared = value; }
		}

		public ArrayList WarsInvited
		{
			get{ return m_WarsInvited; }
			set{ m_WarsInvited = value; }
		}

		public ArrayList Allegiances
		{
			get{ return m_Allegiances; }
			set{ m_Allegiances = value; }
		}

		public ArrayList AllegiancesDeclared
		{
			get{ return m_AllegiancesDeclared; }
			set{ m_AllegiancesDeclared = value; }
		}

		public ArrayList AllegiancesInvited
		{
			get{ return m_AllegiancesInvited; }
			set{ m_AllegiancesInvited = value; }
		}

		public bool HasBank
		{
			get{ return m_HasBank; }
			set{ m_HasBank = value; }
		}

		public bool HasTavern
		{
			get{ return m_HasTavern; }
			set{ m_HasTavern = value; }
		}

		public bool HasHealer
		{
			get{ return m_HasHealer; }
			set{ m_HasHealer = value; }
		}

		public bool HasMoongate
		{
			get{ return m_HasMoongate; }
			set{ m_HasMoongate = value; }
		}

		public bool HasStable
		{
			get{ return m_HasStable; }
			set{ m_HasStable = value; }
		}

		public ArrayList Gardens
		{
			get{ return m_Gardens; }
			set{ m_Gardens = value; }
		}

		public ArrayList Parks
		{
			get{ return m_Parks; }
			set{ m_Parks = value; }
		}

		public int ResFee
		{
			get{ return m_ResFee; }
			set{ m_ResFee = value; }
		}

		public int CorpseFee
		{
			get{ return m_CorpseFee; }
			set{ m_CorpseFee = value; }
		}

		public static void Initialize()
		{
			CommandSystem.Register( "CityUpdate", AccessLevel.Administrator, new CommandEventHandler( CityUpdate_OnCommand ) );
			
		}
		[Usage( "CityUpdate" )]
		[Description( "Forces City Update")]
		private static void CityUpdate_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.SendMessage( " Select the city stone you wish to have update" );
			from.Target = new CityUpdateTarget();
		}
		
		private class CityUpdateTarget : Target
		{
			
			
			public CityUpdateTarget() : base( 10, false, TargetFlags.None )
			{
				
			}
			protected override void OnTarget( Mobile from, object target )
			{
				CityManagementStone stone;
				
				if ( target is CityManagementStone )
				{
					stone = (CityManagementStone)target;
					stone.DoUpdate();
					stone.RestartTimer();
					
				}
			}
		}
		
		public CityManagementStone() : base( 3804 )
		{
			Movable = false;
			Name = "city management stone";

			m_Time = DateTime.Now + PlayerGovernmentSystem.StartUpdate;
			m_Timer = new CityUpdateTimer( m_Time, this );
			m_Timer.Start();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( from == m_Mayor || from.AccessLevel >= AccessLevel.GameMaster )
				{
					from.SendGump( new CityManagementGump( this, from ) );

					TimeSpan time = m_Time - DateTime.Now;

					if ( time.Days != 0 )
						from.SendMessage( 53, "The city will update again in {0} days, {1} hours, {2} minutes, and {3} seconds.", time.Days, time.Hours, time.Minutes, time.Seconds );
					else if ( time.Hours != 0 )
						from.SendMessage( 53, "The city will update again in {0} hours, {1} minutes, and {2} seconds.", time.Hours, time.Minutes, time.Seconds );
					else if ( time.Minutes != 0 )
						from.SendMessage( 53, "The city will update again in {0} minutes, and {1} seconds.", time.Minutes, time.Seconds );
					else if ( time.Seconds != 0 )
						from.SendMessage( 53, "The city will update again in {0} seconds.", time.Seconds );
				}
				else if ( this.Citizens.Contains( from ) )
				{
					from.SendGump( new CityCitizenGump( this, from ) );

					TimeSpan time = m_Time - DateTime.Now;

					if ( time.Days != 0 )
						from.SendMessage( 53, "The city will update again in {0} days, {1} hours, {2} minutes, and {3} seconds.", time.Days, time.Hours, time.Minutes, time.Seconds );
					else if ( time.Hours != 0 )
						from.SendMessage( 53, "The city will update again in {0} hours, {1} minutes, and {2} seconds.", time.Hours, time.Minutes, time.Seconds );
					else if ( time.Minutes != 0 )
						from.SendMessage( 53, "The city will update again in {0} minutes, and {1} seconds.", time.Minutes, time.Seconds );
					else if ( time.Seconds != 0 )
						from.SendMessage( 53, "The city will update again in {0} seconds.", time.Seconds );
				}
				else
				{
					from.SendMessage( "You are not a citizen of this city, You must see the mayor to join this city." );
				}
			}
			else
			{
				from.SendMessage( "You are too far away to access that." );
			}
		}

		public void DoJoin( Mobile mayor, Mobile from )
		{
			bool isBanned = false;

			foreach ( Mobile mob in this.Banned )
			{
				if ( mob == from )
				{
					isBanned = true;
				}
			}

			if ( isBanned == true ) //Pointless sence you cant place a house while banned. Nore can you join without house. But just in case :)
			{
				mayor.SendMessage( "You must lift the ban over them before you can add them to the city." );
				from.SendMessage( "You are banned from this city you cannot join." );
			}
			else if ( PlayerGovernmentSystem.CheckIfMayor( from ) )
			{
				from.SendMessage( "You are already a mayor of another city. You cannot join this city." );
				mayor.SendMessage( "That person is a mayor of another city, they cannot join." );
			}
			else if ( PlayerGovernmentSystem.CheckIfCitizen( from ) )
			{
				from.SendMessage( "You are already a member of another city. You must leave that city before joining this one." );
				mayor.SendMessage( "That person is already a member of another city." );
			}
			else if ( !PlayerGovernmentSystem.CheckIfHouseInCity( from, m_Region ) )
			{
				from.SendMessage( "You must own a house within city limits." );
				mayor.SendMessage( "That person must own a house within city limits." );
			}
			else if ( PlayerGovernmentSystem.AreThereVendors( from ) )
			{
				from.SendMessage( "You must remove your house vendors before you may join." );
				mayor.SendMessage( "They have existing vendors in their house and must remove them first." );
			}
			         
			else
			{
				from.SendMessage( "Welcome to {0}.", this.CityName );
				m_Members.Add( from );
				
				PlayerMobile pm = (PlayerMobile)from;
				
				pm.City = this;
				pm.CityTitle = "Citizen";
				pm.ShowCityTitle = true;

				foreach ( Mobile m in m_Members )
				{
					m.SendMessage( 53, "{0} has just joined the city.", from.Name );
					m.SendMessage( 53, "The city now has {0} members and is at level {1}.", m_Members.Count, this.Level );
					#if ( ChatEnabled )
					InternalChatMessage.SendInternalChatMsg( m, "City Join Message", String.Format( " {0} has joined the city!", from.RawName ));
					#endif
				}
			}
		}

		public void AdminDoJoin( Mobile admin, Mobile from )
		{
			bool isBanned = false;

			foreach ( Mobile mob in this.Banned )
			{
				if ( mob == from )
				{
					isBanned = true;
				}
			}

			if ( isBanned == true ) //Pointless sence you cant place a house while banned. Nore can you join without house. But just in case :)
			{
				admin.SendMessage( "User is banned from that city" );
			}
						
			else if ( PlayerGovernmentSystem.AreThereVendors( from ) )
			{
				admin.SendMessage( "User has a house in town with vendors that need to be removed first." );
			}
			
			else
			{
				m_Members.Add( from );
				
				PlayerMobile pm = (PlayerMobile)from;
				
				pm.City = this;
				pm.CityTitle = "Citizen";
				pm.ShowCityTitle = true;

				admin.SendMessage( "User has been added to {0}.", this.Name );
			}
		}
		
		public bool CalculateMaintenance()  
		{
			/*
			 * Maintenance Rules
			 *
			 * Every Member Costs 2 gold
			 * Every Lockdown Costs 3 gold
			 * Banks = 1000, Healers = 1000, Tavern = 3000, Moongate = 5000, Stable = 2000
			 * City Hall = 10000 x Level, Guards = 5000, Registered City = 1000
			 * Gardens = 2000
			 * Parks = 3000
			 * Markets = 1000
			 * Addons = 100 gold
			 */

			if ( this.Mayor == null )
				return false;

			int cost = 0;

			int hallCost = 5000 * this.Level;
			cost += hallCost;

			if ( this.Citizens.Count >= 1 )
			{
				int citizensCost = this.Citizens.Count * 2;
				cost += citizensCost;
			}

			if ( this.CurrentDecore >= 1 )
			{
				int decoreCost = this.CurrentDecore * 3;
				cost += decoreCost;
			}
			
			if ( this.AddOns.Count >= 1 )
			{
				int addoncost = this.AddOns.Count * 100;
				cost += addoncost;
			}

			if ( this.IsGuarded == true )
				cost += 1000;

			if ( this.IsRegistered == true )
				cost += 1000;

			if ( this.HasBank == true )
				cost += 1000;

			if ( this.HasTavern == true )
				cost += 1000;

			if ( this.HasHealer == true )
				cost += 1000;

			if ( this.HasMoongate == true )
				cost += 1000;

			if ( this.HasStable == true )
				cost += 1000;
			
			if ( this.HasMarket == true )
				cost += 1000;

			if ( this.Gardens.Count >= 1 )
			{
				int gardenCost = this.Gardens.Count * 1000;
				cost += gardenCost;
			}

			if ( this.Parks.Count >= 1 )
			{
				int parkCost = this.Parks.Count * 1000;
				cost += parkCost;
			}

			if ( this.CityTreasury >= cost )
			{
				this.Mayor.SendMessage( "Your cities maintenance has been paid, A total of {0} was taken out of your cites treasury you now have a total of {1} left in the city treasury.", cost, ( this.CityTreasury - cost ) );
				this.CityTreasury -= cost;
				#if (ChatEnabled)
				InternalChatMessage.SendInternalChatMsg( this.Mayor, "City System", String.Format( "City maintenance paid. {0} removed from treasury.  {1} remaning.", cost, ( this.CityTreasury - cost )) );
				#endif
				return false;
			}
			else
			{
				if ( m_Members != null )
				{
					foreach ( Mobile m in m_Members )
					{
						m.SendMessage( 53, "City Disbanded." );
						m.SendMessage( 53, "REASON: The city lacked the required funds in its treasury to pay its maintenance." );
						#if (ChatEnabled)
						InternalChatMessage.SendInternalChatMsg( m, "City Message", "City Disbanded due to lack of funds." );
						#endif
					}
				}

				if ( isLockedDown != null )
				{
					foreach( Item ld in isLockedDown )
					{
						ld.Movable = true;
					}
				}

				this.Delete();

				return true;
			}
		}

		public void PitchMembers()
		{
			ArrayList removelist = new ArrayList();
			
			foreach ( Mobile m in m_Members )
			{
				if ( m == null )
				{
					removelist.Add( m );
					continue;
				}

				if ( !PlayerGovernmentSystem.CheckIfHouseInCity( m, m_Region ) )
				{
					m.SendMessage( "You must own a house in the city, Your membership has been revoked." );
					#if (ChatEnabled)
					InternalChatMessage.SendInternalChatMsg( m, "City System", "Your membership in the city has been revoked since you house is no longer in the limits." );
					#endif
					removelist.Add( m );
					PlayerMobile pm = (PlayerMobile)m;
					pm.City = null;
					pm.CityTitle = null;
					pm.ShowCityTitle = false;
				}
			}
			
			if ( removelist.Count > 0 )
			{
				foreach ( Mobile m in removelist )
				{
					m_Members.Remove( m );
					if ( this.Mayor == m )
						this.Mayor = null;
					if ( this.AssistMayor == m )
						this.AssistMayor = null;
				}
			}
		}
		
		public bool CheckMayorExist( Mobile mayor )
		{
			try
			{
				Account a = (Account)mayor.Account;
				for ( int i = 0; i < a.Count; i++ )
				{
					if ( a[i] == mayor )
						return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}
		
		public bool CheckAsstMayorExist( Mobile astmayor )
		{
			if ( this.AssistMayor != null )
			{
			
					try
					{
						Account a = (Account)astmayor.Account;
						for ( int i = 0; i < a.Count; i++ )
						{
							if ( a[i] == astmayor )
								return true;
						}
						return false;
				
					}
					catch
					{
						return false;
					}
					
			}
			return false;		
		
		}
		
		public bool CheckVendorOwnerExist( Mobile owner, CityPlayerVendor vendor )
		{
			try
			{
				Account a = (Account)owner.Account;
				for ( int i = 0; i < a.Count; i++ )
				{
					if ( a[i] == vendor.Owner )
						return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}
				
		public void PickRandomNewMayor()
		{
			
			if ( m_Members.Count > 0 )
			{
				
				int index = Utility.Random( 0, m_Members.Count - 1 );
				object newmayor = m_Members[index];
				Mobile mob = (Mobile)newmayor;
				this.Mayor = mob;
				PlayerMobile mayor = (PlayerMobile)newmayor;
				mayor.CityTitle = "Mayor";
				this.Mayor.SendMessage( "You are the new mayor since the other has moved on! Because you were picked at random an election will take place in 24 hours!" );
				#if (ChatEnabled)
				InternalChatMessage.SendInternalChatMsg( this.Mayor, "Urgent City Message", "You are the new mayor since the other has moved on! Because you were picked at random an election will take place in 24 hours!");
				#endif				                                        
				foreach ( Mobile m in m_Members )
				{
					m.SendMessage( "The mayor has left the town and there is no Assistant Mayor, {0} has been randomly chosen.  Please elect a new mayor within 24 hours!", this.Mayor.Name );
					#if (ChatEnabled)
					InternalChatMessage.SendInternalChatMsg( m, "Urgent City Message", String.Format(" The mayor has left the town and there is no Assistant Mayor, {0} has been randomly chosen.  Please elect a new mayor within 24 hours!", this.Mayor.Name ));
					#endif
				}
				this.VoteStone.RestartTimer( TimeSpan.FromDays( 1.0 ) );
			}
		}
		
		public bool DeadMayor()
		{
			if ( ( this.Mayor == null || !CheckMayorExist( this.Mayor ) ) && ( m_AssistMayor == null || !CheckAsstMayorExist(this.AssistMayor ) ) ) 
			{
				PickRandomNewMayor();
				return true;
			}
						
			PlayerMobile incumbant = (PlayerMobile)this.Mayor;
			if ( incumbant.City == null || incumbant.City != this  )
			{
				if ( m_AssistMayor != null && CheckAsstMayorExist( m_AssistMayor ) )
				{
					this.Mayor = m_AssistMayor;
					PlayerMobile newmayor = (PlayerMobile)m_AssistMayor;
					newmayor.CityTitle = "Mayor";
					m_AssistMayor.SendMessage( "You are the new mayor since the other has moved on! Please remember to pick a new Assistant Mayor." );
					m_AssistMayor = null;
					#if (ChatEnabled)
					InternalChatMessage.SendInternalChatMsg( this.Mayor, "City System Message", "You are the new mayor since the other has moved on! Please remember to pick a new Assistant Mayor." );
					#endif
					return true;
					
				}
				else
				{
					PickRandomNewMayor();
						return true;
					
				}
			
			}
			else
				return false;
		}
	
		public void VerifyAddons()  
		{
			ArrayList removeme = new ArrayList();
			ArrayList unlockme = new ArrayList();
			
			if ( m_Addons != null )
			{
				for ( int i = 0; i < m_Addons.Count; i++ )
				{
					BaseAddon thing = (BaseAddon)m_Addons[i];
					Point3D p = new Point3D( thing.X, thing.Y, thing.Z );
					Map m = thing.Map;
					
					if ( Region.Find( p, m ) is PlayerCityRegion )
						continue;
					else
					{
						removeme.Add( m_Addons[i] );
						thing.Delete();
					}
					
				}
				if ( removeme.Count > 0 )
				{
					for ( int i = 0; i < removeme.Count; i++ )
					{
						m_Addons.Remove( removeme[i] );
					}
				}
			}
			
			if ( isLockedDown != null )
			{
				foreach ( Item item in isLockedDown )
				{
					Point3D p = new Point3D( item.X, item.Y, item.Z );
					Map m = item.Map;
					if ( Region.Find( p, m ) is PlayerCityRegion )
						continue;
					else
					{
						unlockme.Add( item );
						item.Movable = true;
					}
				}
				if ( unlockme.Count > 0 )
				{
					for ( int i = 0; i < unlockme.Count; i++ )
					{
						isLockedDown.Remove( unlockme[i] );
					}
				}
			}
		}
			
		public void UpdateCityStructures( int level )
		{
			
			List<CivicSign> killsign = new List<CivicSign>();
			switch ( level )
			{
				case 1:
				{
					for ( int i = 0; i < toDelete.Count; i++ )
					{
						if ( toDelete[i] is CivicSign )
						{
							CivicSign sign = toDelete[i] as CivicSign;
							if ( sign.Type == CivicSignType.Garden )
							{
								RecCivicSign recsign = (RecCivicSign) sign;
								if ( recsign.CityLevel > level )
									this.Gardens.Remove( sign );
								else
									continue;
							}

							if ( sign.Type == CivicSignType.Moongate )
								m_IsRegistered = false;
							if ( sign.Type == CivicSignType.Park )
								this.Parks.Remove( sign );
							if ( sign.Type == CivicSignType.Healer )
								this.ResStone = null;
							
							m_IsGuarded = false;
							killsign.Add( sign );
							sign.Delete();
						}
					}
					goto case 10;
				}
				case 2:
				{
					for ( int i = 0; i < toDelete.Count; i++ )
					{
						if ( toDelete[i] is CivicSign )
						{
							CivicSign sign = toDelete[i] as CivicSign;
							if ( sign.Type == CivicSignType.Bank )
								continue;
							
							if ( sign.Type == CivicSignType.Garden )
							{
								RecCivicSign recsign = (RecCivicSign) sign;
								if ( recsign.CityLevel > level )
									this.Gardens.Remove( sign );
								else
									continue;
							}
							
							if ( sign.Type == CivicSignType.Moongate )
								m_IsRegistered = false;
							if ( sign.Type == CivicSignType.Park )
								this.Parks.Remove( sign );
							if ( sign.Type == CivicSignType.Healer )
								this.ResStone = null;
							
							m_IsGuarded = false;
							killsign.Add( sign );
							sign.Delete();
						}
					}
					goto case 10;
				}
				case 3:
				{
					for ( int i = 0; i < toDelete.Count; i++ )
					{
						if ( toDelete[i] is CivicSign )
						{
							CivicSign sign = toDelete[i] as CivicSign;
							if ( sign.Type == CivicSignType.Bank || sign.Type == CivicSignType.Moongate || sign.Type == CivicSignType.Market ) 
								continue;
							
							if ( sign.Type == CivicSignType.Garden )
							{
								RecCivicSign recsign = (RecCivicSign) sign;
								if ( recsign.CityLevel > level )
									this.Gardens.Remove( sign );
								else
									continue;
							}
							
							if ( sign.Type == CivicSignType.Park )
								this.Parks.Remove( sign );
							if ( sign.Type == CivicSignType.Healer )
								this.ResStone = null;
							killsign.Add( sign );
							sign.Delete();
						}
					}
					goto case 10;
				}
				case 4:
				{
					for ( int i = 0; i < toDelete.Count; i++ )
					{
						if ( toDelete[i] is CivicSign )
						{
							CivicSign sign = toDelete[i] as CivicSign;
							
							if ( sign.Type == CivicSignType.Bank || sign.Type == CivicSignType.Moongate || sign.Type == CivicSignType.Healer || sign.Type == CivicSignType.Garden || sign.Type == CivicSignType.Market )
								continue;
							
							if ( sign.Type == CivicSignType.Park )
							{
								RecCivicSign recsign = (RecCivicSign) sign;
								if ( recsign.CityLevel > level )
									this.Parks.Remove( sign );
								else
									continue;
							}
							killsign.Add( sign );
							sign.Delete();
						}
					}
					goto case 10;
				}
				case 5:
				{
					for ( int i = 0; i < toDelete.Count; i++ )
					{
						if ( toDelete[i] is CivicSign )
						{
							CivicSign sign = toDelete[i] as CivicSign;
							
							if ( sign.Type == CivicSignType.Bank || sign.Type == CivicSignType.Moongate || sign.Type == CivicSignType.Healer || sign.Type == CivicSignType.Garden || sign.Type == CivicSignType.Stable || sign.Type == CivicSignType.Market )
								continue;
							
							if ( sign.Type == CivicSignType.Park )
							{
								RecCivicSign recsign = (RecCivicSign) sign;
								if ( recsign.CityLevel > level )
									this.Parks.Remove( sign );
								else
									continue;
							}
							killsign.Add( sign );
							sign.Delete();
						}
					}
					goto case 10;
				}
				case 6:
				{
					break;
				}
				case 10:
				{
					if ( killsign != null )
					{
						foreach ( CivicSign s in killsign )
						{
							toDelete.Remove( s );
						}
					}
					break;
				}
			}
		}
		
		public void CheckVendors( bool citydelete ) 
		{
			if ( m_Vendors != null ) //Set Vendors to delete after 2 days
			{
				ArrayList removelist = new ArrayList();
				foreach ( CityPlayerVendor vend in m_Vendors )
				{
					if ( vend == null || !CheckVendorOwnerExist( vend.Owner, vend ) )
					{
						removelist.Add( vend );
						continue;
					}
					else 
					{
						if ( vend is CityRentedVendor )
						{
							if ( vend.Region is CityMarketRegion && !citydelete )
								continue;
							else
							{
								vend.City = null;
								vend.Die = DateTime.Now + TimeSpan.FromDays( 2.0 );
								Timer t = new CityVendorDismiss( vend, vend.Die );
								Mobile owner = vend.Owner;
								owner.SendMessage( "Your vendor will delete in 2 days since your city is gone." );
								#if (ChatEnabled)
								if ( CheckVendorOwnerExist( vend.Owner, vend ) )
										InternalChatMessage.SendInternalChatMsg( owner, "City Message", " Your vendor will delete in 2 days because they are no longer in a town region." );
								#endif
								t.Start();
							}
						}
						
						else if (  ( vend.Region is PlayerCityRegion || vend.Region is CityMarketRegion || CheckIfVendorInTownhouse( vend ) ) && !citydelete )
							continue;
						else
						{
							vend.City = null;
							vend.Die = DateTime.Now + TimeSpan.FromDays( 2.0 );
							//vend.Die = DateTime.Now + TimeSpan.FromMinutes( 2.0 ); //For Testing
							Mobile owner = vend.Owner;
							Timer t = new CityVendorDismiss( vend, vend.Die );
							owner.SendMessage( "Your vendor will delete in 2 days since your city is gone." );
							#if (ChatEnabled)
							if ( CheckVendorOwnerExist( vend.Owner, vend ) )
									InternalChatMessage.SendInternalChatMsg( owner, "City Message", " Your vendor will delete in 2 days because they are no longer in a town region." );
							#endif
							t.Start();
						}
						
					}
				}
				
				if ( removelist.Count > 0 )
				{
					for ( int x = 0; x < removelist.Count; x++ )
					{
						CityPlayerVendor vendor = (CityPlayerVendor)removelist[x];
						if ( vendor != null )
							vendor.Dismiss();		
												
						m_Vendors.Remove( removelist[x] );
					}
				}
			}
		}
		
		public bool CheckIfVendorInTownhouse( CityPlayerVendor vend )
		{
			PlayerMobile pm = (PlayerMobile)vend.Owner;
			Region reg = vend.Region;
			bool hashouse = false;
			if ( reg is HouseRegion )
			{
				HouseRegion housr = (HouseRegion)reg;
				BaseHouse house = housr.House;
				hashouse = PlayerGovernmentSystem.CheckIfSpecificHouseInCity( house, this.PCRegion );
				if ( hashouse && ( pm.City == this ) && ( vend.City == this ) )
					return true;
				else
					return false;
			
			}
			else
				return false;
		}
		
		public void DoUpdate()
		{
			bool db1 = false;
			PitchMembers(); //Check to see if all members still own houses in the town. This fixes the enumeration crash.
			mayorchange = DeadMayor();
			
			if ( m_HasHealer )
			{
				if ( m_resstone != null )
					m_resstone.ClearGhosts();
			}
			
			foreach ( Mobile m in m_Members )
			{
				m.SendMessage( 53, "City update in progress..." );
			}

			int count = m_Members.Count;

			if ( count < PlayerGovernmentSystem.Level1 || this.Mayor == null ) 
			{
				db1 = true;
			}
			else if ( count < PlayerGovernmentSystem.Level2 )
			{
				if ( this.Level != 1 )
					regionchanged = true;

				this.Level = 1;
				int offset = PlayerGovernmentSystem.L1CLOffset;
				Point2D start = new Point2D( this.Center.X - offset, this.Center.Y - offset );
				Point2D end = new Point2D( this.Center.X + offset, this.Center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				this.MaxDecore = PlayerGovernmentSystem.Level1LD;
				this.Coords = cityCoords;

				m_RegionCoords = PlayerGovernmentSystem.FormatRegion( box );
				UpdateCityStructures( this.Level );
				UpdateRegion();
				CheckVendors( false );
				VerifyAddons();
			}
			else if ( count < PlayerGovernmentSystem.Level3 )
			{
				if ( this.Level != 2 )
					regionchanged = true;
				
				this.Level = 2;

				int offset = PlayerGovernmentSystem.L2CLOffset;
				Point2D start = new Point2D( this.Center.X - offset, this.Center.Y - offset );
				Point2D end = new Point2D( this.Center.X + offset, this.Center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				this.MaxDecore = PlayerGovernmentSystem.Level2LD;
				this.Coords = cityCoords;

				m_RegionCoords = PlayerGovernmentSystem.FormatRegion( box );
				UpdateCityStructures( this.Level );
				UpdateRegion();
				CheckVendors( false );
				VerifyAddons();
			}
			else if ( count < PlayerGovernmentSystem.Level4 )
			{
				if ( this.Level != 3 )
					regionchanged = true;
				
				this.Level = 3;

				int offset = PlayerGovernmentSystem.L3CLOffset;
				Point2D start = new Point2D( this.Center.X - offset, this.Center.Y - offset );
				Point2D end = new Point2D( this.Center.X + offset, this.Center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				this.MaxDecore = PlayerGovernmentSystem.Level3LD;
				this.Coords = cityCoords;

				m_RegionCoords = PlayerGovernmentSystem.FormatRegion( box );
				UpdateCityStructures( this.Level );
				UpdateRegion();
				CheckVendors( false );
				VerifyAddons();
			}
			else if ( count < PlayerGovernmentSystem.Level5 )
			{
				if ( this.Level != 4 )
					regionchanged = true;
				
				this.Level = 4;

				int offset = PlayerGovernmentSystem.L4CLOffset;
				Point2D start = new Point2D( this.Center.X - offset, this.Center.Y - offset );
				Point2D end = new Point2D( this.Center.X + offset, this.Center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				this.MaxDecore = PlayerGovernmentSystem.Level4LD;
				this.Coords = cityCoords;

				m_RegionCoords = PlayerGovernmentSystem.FormatRegion( box );
				UpdateCityStructures( this.Level );
				UpdateRegion();
				CheckVendors( false );
				VerifyAddons();
			}
			else if ( count < PlayerGovernmentSystem.Level6 )
			{
				if ( this.Level != 5 )
					regionchanged = true;
				
				this.Level = 5;

				int offset = PlayerGovernmentSystem.L5CLOffset;
				Point2D start = new Point2D( this.Center.X - offset, this.Center.Y - offset );
				Point2D end = new Point2D( this.Center.X + offset, this.Center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				this.MaxDecore = PlayerGovernmentSystem.Level5LD;
				this.Coords = cityCoords;

				m_RegionCoords = PlayerGovernmentSystem.FormatRegion( box );
				UpdateCityStructures( this.Level );
				UpdateRegion();
				CheckVendors( false );
				VerifyAddons();
			}
			else
			{
				if ( this.Level != 6 )
					regionchanged = true;
				
				this.Level = 6;

				int offset = PlayerGovernmentSystem.L6CLOffset;
				Point2D start = new Point2D( this.Center.X - offset, this.Center.Y - offset );
				Point2D end = new Point2D( this.Center.X + offset, this.Center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				this.MaxDecore = PlayerGovernmentSystem.Level6LD;
				this.Coords = cityCoords;

				m_RegionCoords = PlayerGovernmentSystem.FormatRegion( box );
				UpdateCityStructures( this.Level );
				UpdateRegion();
				CheckVendors( false );
				VerifyAddons();
			}
			
			if ( HasMarket )
			{
				foreach ( Item item in toDelete )
				{
					if ( item is CivicSign )
					{
						CivicSign sign = (CivicSign)item;
						if ( sign.Type == CivicSignType.Market )
						{
							CityLandLord lord = (CityLandLord)sign.LandlordRemove;
							lord.CreateRandomVendors( lord.Location, lord.Map );
						}
					}
				}
			}

			if ( CalculateMaintenance() )
			{
				if ( m_Members != null )
				{
					foreach ( Mobile m in m_Members )
					{
						m.SendMessage( 53, "City Update Complete..." );
						
					}
				}
			}
			else if ( db1 == true )
			{
				if ( m_Members != null )
				{
					foreach ( Mobile m in m_Members )
					{
						m.SendMessage( 53, "City Disbanded." );
						m.SendMessage( 53, "REASON: The city lacked the minumal amount of citizens." );
						m.SendMessage( 53, "City Update Complete..." );
						#if (ChatEnabled)
							InternalChatMessage.SendInternalChatMsg( m, "City Disbanded", "City disbanded due to lack of citizens" );
						#endif
					}
				}

				if ( isLockedDown != null )
				{
					foreach( Item ld in isLockedDown )
					{
						ld.Movable = true;
					}
				}

				this.Delete();
			}
			else
			{
				if ( IncomeTax >= 0 )
				{
					if ( m_Members != null && m_Vendors != null )
					{
						
						foreach ( Mobile m in m_Vendors )
						{
							if ( m is CityPlayerVendor )
							{
								CityPlayerVendor vend = (CityPlayerVendor)m;
								Mobile mob = vend.Owner;
								this.CityTreasury += vend.IncomeTax;
								mob.SendMessage( "Your income tax of {0} has been collected from your vendor.", vend.IncomeTax );
								#if (ChatEnabled)
									InternalChatMessage.SendInternalChatMsg( mob, "Taxes Collected", String.Format( "Your income tax of {0} has been collected from your vendor.", vend.IncomeTax ) );
								#endif
								vend.IncomeTax = 0;
							}
							
						}
					}
				}

				if ( HousingTax >= 0 )
				{
					if ( m_Members != null )
					{
						foreach ( Mobile m in m_Members )
						{
							
							if ( m_HousingTax > 0 )
								
								if ( Banker.Withdraw( m, m_HousingTax ) )
							{
								m.SendMessage( "Your property tax has been collected, {0} has been withdrawn from your bank account.", m_HousingTax );
								#if (ChatEnabled)
									InternalChatMessage.SendInternalChatMsg( m, "Taxes Collected", String.Format( "Your property tax has been collected, {0} has been withdrawn from your bank account.", m_HousingTax ) );
								#endif								
								m_CityTreasury += m_HousingTax;
							}
							else
							{
								m.SendMessage( "You lack the gold to pay your taxes." );
								#if (ChatEnabled)
									InternalChatMessage.SendInternalChatMsg( m, "You Owe Back Taxes", "You lack the gold to pay your taxes.");
								#endif
								PlayerMobile pm = (PlayerMobile)m;
								pm.OwesBackTaxes = true;
								pm.BackTaxesAmount += m_HousingTax;
							}
						}
					}
				}

				if ( m_CityTreasury <= 10000 )
				{
					if ( m_Members != null )
					{
						foreach ( Mobile m in m_Members )
						{
							m.SendMessage( 38, "WARNING: City treasury is low, There is now less than 10k gold in the treasury, The city will disband if there is not enough gold in the treasury to cover the maintenance cost before the next city update." );
							#if (ChatEnabled)
								InternalChatMessage.SendInternalChatMsg( m, "City Treasury", "The city treasury is low, it will disband after the next maintainance, please add money!" );
							#endif
						}
					}
				}

				if ( m_Members != null )
				{
					foreach ( Mobile m in m_Members )
					{
						m.SendMessage( 53, "The city has been updated, The city level is now {0}, with {1} citizens.", Level, Citizens.Count );
						m.SendMessage( 53, "City Update Complete..." );
					}
				}
			}
		}

		public CityManagementStone( Serial serial ) : base( serial )
		{
		}

		public override void OnDelete()
		{
			CheckVendors( true );  //Set Vendors to delete after 2 days.
			
			if( m_Region != null ) // Remove Region
				
				m_Region.Unregister();

			if ( toDelete != null ) // Delete all items needed
			{
				foreach( Item i in toDelete )
				{
					if ( i != null )
						i.Delete();
				}
			}
			
		
			if ( AddOns != null )
			{
				foreach( Item addon in AddOns )
				{
					if ( addon != null )
						addon.Delete();
				}
			}

			if ( isLockedDown != null )
			{
				foreach( Item ld in isLockedDown )
				{
					if ( ld != null )
						ld.Movable = true;
				}
			}

			if ( Citizens != null ) // Disband Citizens
			{
				foreach( Mobile mob in Citizens )
				{
					PlayerMobile pm = (PlayerMobile)mob;
					
					pm.City = null;
					pm.CityTitle = null;
					pm.ShowCityTitle = false;
				}
			}

			if ( Waring != null ) // Clear all cities waring this one.
			{
				foreach( Item item in Waring )
				{
					CityManagementStone stones = (CityManagementStone)item;

					stones.Waring.Remove( this );
				}
			}

			if ( WarsDeclared != null ) // Clear Wars Invited From This City
			{
				foreach( Item item in WarsDeclared )
				{
					CityManagementStone stones = (CityManagementStone)item;

					stones.WarsInvited.Remove( this );
				}
			}

			if ( WarsInvited != null ) // Clear Wars Declared To This City
			{
				foreach( Item item in WarsInvited )
				{
					CityManagementStone stones = (CityManagementStone)item;

					stones.WarsDeclared.Remove( this );
				}
			}

			if ( Allegiances != null ) // Clear all cities allied this one.
			{
				foreach( Item item in Allegiances )
				{
					CityManagementStone stones = (CityManagementStone)item;

					stones.Allegiances.Remove( this );
				}
			}

			if ( AllegiancesDeclared != null ) // Clear allies Invited From This City
			{
				foreach( Item item in AllegiancesDeclared )
				{
					CityManagementStone stones = (CityManagementStone)item;

					stones.AllegiancesInvited.Remove( this );
				}
			}

			if ( AllegiancesInvited != null ) // Clear allies Declared To This City
			{
				foreach( Item item in AllegiancesInvited )
				{
					CityManagementStone stones = (CityManagementStone)item;

					stones.AllegiancesDeclared.Remove( this );
				}
			}

			if ( m_Timer != null )
				m_Timer.Stop();

			base.OnDelete();
		}

		public void UpdateRegion()
		{
			if( m_Region != null )
				m_Region.Unregister();
			
			if ( regionchanged )
				m_Region = null;
			
			if( Coords != null && Coords.Count != 0 )
			{
				if( m_Region == null )
					m_Region = new PlayerCityRegion( this, this.Map, this.RegionCoords  );

				PlayerCityRegion reg = m_Region;
				this.PCRegion = reg;

				reg.Register();
			}
		}

		public void RestartTimer()
		{
			if ( m_Timer != null )
				m_Timer.Stop();
			m_Time = DateTime.Now + PlayerGovernmentSystem.CityUpdate;
			m_Timer = new CityUpdateTimer( m_Time, this );
			m_Timer.Start();
		}

		public static void WriteRect3DArray(GenericWriter writer, Rectangle3D[] ary)
		{
			if (ary == null)
			{
				writer.Write(0);
				return;
			}

			writer.Write(ary.Length);

			for (int i = 0; i < ary.Length; i++)
			{
				Rectangle3D rect = ((Rectangle3D)ary[i]);
				writer.Write((Point3D)rect.Start);
				writer.Write((Point3D)rect.End);
			}
			return;
		}
		
		public static Rectangle3D[] ReadRect3DArray(GenericReader reader)
		{
			int size = reader.ReadInt();
			List<Rectangle3D> newAry = new List<Rectangle3D>();

			for (int i = 0; i < size; i++)
			{
				Point3D start = reader.ReadPoint3D();
				Point3D end = reader.ReadPoint3D();
				newAry.Add(new Rectangle3D(start,end));
			}

			return newAry.ToArray();
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 6 ); // version
			
			UpdateRegion();
			
			//2.1.1
			writer.WriteItemList( m_Addons, true );
			// 2.1.0 Update
			writer.Write( m_HasMarket );

			//2.0 Update
			WriteRect3DArray(writer, m_RegionCoords);
			
			// 1.0 update
			writer.Write( m_votingstone );
			writer.Write( m_AssistMayor );
			
			// .9 Update
			writer.Write( m_resstone );
			
			// City Resurrection Stones Update
			writer.Write( m_ResFee );
			writer.Write( m_CorpseFee );

			// Release
			writer.WriteDeltaTime( m_Time );
			writer.Write( m_Mayor );
			WriteRect2DArray( writer, m_Coords );
			writer.Write( m_IsGuarded );
			writer.Write( m_AllowHouses );
			writer.Write( m_Level );
			writer.Write( m_CityName );
			writer.WriteItemList( m_toDelete, true );
			writer.WriteMobileList( m_Members, true );
			writer.Write( m_TravelTax );
			writer.Write( m_IncomeTax );
			writer.Write( m_HousingTax );
			writer.Write( m_CityTreasury );
			writer.Write( m_CityRules );
			writer.Write( m_CityWebURL );
			writer.Write( m_Center );
			writer.Write( m_MoongateLocation );
			writer.WriteMobileList( m_Sponsored, true );
			writer.WriteItemList( m_isLockedDown, true );
			writer.Write( m_MaxDecore );
			writer.Write( m_CurrentDecore );
			writer.Write( m_IsRegistered );
			writer.WriteMobileList( m_Banned, true );
			writer.WriteItemList( m_Waring, true );
			writer.WriteItemList( m_WarsDeclared, true );
			writer.WriteItemList( m_WarsInvited, true );
			writer.WriteItemList( m_Allegiances, true );
			writer.WriteItemList( m_AllegiancesDeclared, true );
			writer.WriteItemList( m_AllegiancesInvited, true );
			writer.Write( m_HasBank );
			writer.Write( m_HasTavern );
			writer.Write( m_HasHealer );
			writer.Write( m_HasMoongate );
			writer.Write( m_HasStable );
			writer.WriteItemList( m_Gardens, true );
			writer.WriteItemList( m_Parks, true );
			writer.WriteMobileList( m_Vendors, true );
		}

		public static void WriteRect2DArray( GenericWriter writer, ArrayList ary )
		{
			writer.Write( ary.Count );

			for( int i = 0; i < ary.Count; i++ )
			{
				writer.Write( (Rectangle2D)ary[i] );	//Rect2D
			}

			return;
		}

		public static ArrayList ReadRect2DArray( GenericReader reader )
		{
			int size = reader.ReadInt();
			ArrayList newAry = new ArrayList();

			for( int i = 0; i < size; i++ )
			{
				newAry.Add( reader.ReadRect2D() );
			}
			
			return newAry;
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 6:
				{
					m_Addons = reader.ReadItemList();
					goto case 5;
				}
				case 5:
				{
					
					m_HasMarket = reader.ReadBool();
					goto case 4;
				}
				case 4:
				{
					m_RegionCoords = ReadRect3DArray(reader);
					goto case 3;
				}
				case 3:
				{
					m_votingstone = (CityVotingStone)reader.ReadItem();
					m_AssistMayor = reader.ReadMobile();
					goto case 2;
				}
				case 2:
				{
					m_resstone = (CityResurrectionStone)reader.ReadItem();
					goto case 1;
				}					
				case 1: // City Resurrection Stone Update
				{
					m_ResFee = reader.ReadInt();
					m_CorpseFee = reader.ReadInt();
					goto case 0;
				}
				case 0: // Beta Release
				{
					m_Time = reader.ReadDeltaTime();
					m_Mayor = reader.ReadMobile();
					m_Coords = ReadRect2DArray( reader );
					m_IsGuarded = reader.ReadBool();
					m_AllowHouses = reader.ReadBool();
					m_Level = reader.ReadInt();
					m_CityName = reader.ReadString();
					m_toDelete = reader.ReadItemList();
					m_Members = reader.ReadMobileList();
					m_TravelTax = reader.ReadInt();
					m_IncomeTax = reader.ReadInt();
					m_HousingTax = reader.ReadInt();
					m_CityTreasury = reader.ReadInt();
					m_CityRules = reader.ReadString();
					m_CityWebURL = reader.ReadString();
					m_Center = reader.ReadPoint2D();
					m_MoongateLocation = reader.ReadPoint3D();
					m_Sponsored = reader.ReadMobileList();
					m_isLockedDown = reader.ReadItemList();
					m_MaxDecore = reader.ReadInt();
					m_CurrentDecore = reader.ReadInt();
					m_IsRegistered = reader.ReadBool();
					m_Banned = reader.ReadMobileList();
					m_Waring = reader.ReadItemList();
					m_WarsDeclared = reader.ReadItemList();
					m_WarsInvited = reader.ReadItemList();
					m_Allegiances = reader.ReadItemList();
					m_AllegiancesDeclared = reader.ReadItemList();
					m_AllegiancesInvited = reader.ReadItemList();
					m_HasBank = reader.ReadBool();
					m_HasTavern = reader.ReadBool();
					m_HasHealer = reader.ReadBool();
					m_HasMoongate = reader.ReadBool();
					m_HasStable = reader.ReadBool();
					m_Gardens = reader.ReadItemList();
					m_Parks = reader.ReadItemList();
					m_Vendors = reader.ReadMobileList();
					break;
				}
			}

			m_Timer = new CityUpdateTimer( m_Time, this );
			m_Timer.Start();

			UpdateRegion();
		}

		private class CityUpdateTimer : Timer
		{
			private CityManagementStone m_Stone;

			public CityUpdateTimer( DateTime end, CityManagementStone stone ) : base( end - DateTime.Now )
			{
				m_Stone = stone;
			}

			protected override void OnTick()
			{
				m_Stone.DoUpdate();
				m_Stone.RestartTimer();

				Stop();
			}
		}
	}
}