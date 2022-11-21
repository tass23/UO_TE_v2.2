using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using Server.Gumps;
using Server.Multis;
using Server.Engines.Help;
using Server.Engines.ConPVP;
using Server.ContextMenus;
using Server.Network;
using Server.Spells;
using Server.Spells.Fifth;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;
using Server.Spells.Bushido;
using Server.Targeting;
using Server.Engines.Quests;
using Server.Factions;
using Server.Regions;
using Server.Accounting;
using Server.Engines.CannedEvil;
using Server.Engines.Craft;
using Server.Spells.Spellweaving;
using Server.Engines.PartySystem;
using Server.Commands;
using Server.Engines.XmlSpawner2;

namespace Server.Mobiles
{
    #region Enums
    [Flags]
    public enum PlayerFlag // First 16 bits are reserved for default-distro use, start custom flags at 0x00010000
    {
        None 					= 0x00000000,
        Glassblowing 			= 0x00000001,
        Masonry 				= 0x00000002,
        SandMining 				= 0x00000004,
        StoneMining 			= 0x00000008,
        ToggleMiningStone 		= 0x00000010,
        KarmaLocked 			= 0x00000020,
        AutoRenewInsurance 		= 0x00000040,
        UseOwnFilter 			= 0x00000080,
        PublicMyRunUO 			= 0x00000100,
        PagingSquelched 		= 0x00000200,
        Young 					= 0x00000400,
        AcceptGuildInvites 		= 0x00000800,
        DisplayChampionTitle 	= 0x00001000,
        HasStatReward 			= 0x00002000,
		FireRockMining 			= 0x00100000,


        #region Mondain's Legacy
        Bedlam 					= 0x00010000,
        LibraryFriend 			= 0x00020000,
        Spellweaving 			= 0x00040000,
        #endregion

        #region SA Gemmining
        GemMining 				= 0x00080000,
        ToggleMiningGem 		= 0x00100000,
        BasketWeaving 			= 0x00200000,
        AbyssEntry 				= 0x00300000,
        #endregion
		
		#region Plant system
        ToggleClippings 		= 0x00400000,
        ToggleCutClippings 		= 0x00500000,
        ToggleCutReeds 			= 0x00600000, 
        #endregion
		
        #region SA Mechanical Life
        MechanicalLife          = 0x02000000,
        #endregion
    }

    public enum NpcGuild
    {
        None,
        MagesGuild,
        WarriorsGuild,
        ThievesGuild,
        RangersGuild,
        HealersGuild,
        MinersGuild,
        MerchantsGuild,
        TinkersGuild,
        TailorsGuild,
        FishermensGuild,
        BardsGuild,
        BlacksmithsGuild
    }

    public enum SolenFriendship
    {
        None,
        Red,
        Black
    }
/*GD13_HS_Start_1
	public enum HuntMode
	{
		None,
		Easy,
		Hard,
		Extreme,
		God
	}

	public enum HuntRank
	{
		None,
		Squire,
		Guardsman,
		Hunter,
		Stalker,
		Slayer,
		General,
		Elite,
		Master,
		GrandMaster,
		Elder,
		Legend,
		Diety,
		God
	}
	
	public enum HuntReward
	{
		None,
		Squire,
		Guardsman,
		Hunter,
		Stalker,
		Slayer,
		General,
		Elite,
		Master,
		GrandMaster,
		Elder,
		Legend,
		Diety,
		God
	}
/*GD13_HS_End_1*/
    #endregion

    public class PlayerMobile : Mobile, IHonorTarget
    {
		#region Custom BODs
		private DateTime m_NextFletcherBulkOrder;
		private DateTime m_NextCarpenterBulkOrder;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan NextFletcherBulkOrder
		{
			get
			{
				TimeSpan ts = m_NextFletcherBulkOrder - DateTime.UtcNow;

				if ( ts < TimeSpan.Zero )
					ts = TimeSpan.Zero;

				return ts;
			}
			set
			{
				try{ m_NextFletcherBulkOrder = DateTime.UtcNow + value; }
				catch{}
			}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan NextCarpenterBulkOrder
		{
			get
			{
				TimeSpan ts = m_NextCarpenterBulkOrder - DateTime.UtcNow;

				if ( ts < TimeSpan.Zero )
					ts = TimeSpan.Zero;

				return ts;
			}
			set
			{
				try{ m_NextCarpenterBulkOrder = DateTime.UtcNow + value; }
				catch{}
			}
		}
		#endregion
		#region FS:ATS Edtis
		private DateTime m_NextTamingBulkOrder;
		private bool m_Bioenginer;

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan NextTamingBulkOrder
		{
			get
			{
				TimeSpan ts = m_NextTamingBulkOrder - DateTime.Now;

				if ( ts < TimeSpan.Zero )
					ts = TimeSpan.Zero;

				return ts;
			}
			set
			{
				try{ m_NextTamingBulkOrder = DateTime.Now + value; }
				catch{}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Bioenginer
		{
			get{ return m_Bioenginer; }
			set{ m_Bioenginer = value; }
		}
		#endregion
	
        #region SA
        public override void ToggleFlying()
        {
            if (Race != Race.Gargoyle)
                return;
            else if (Flying)
            {
                Freeze(TimeSpan.FromSeconds(1));
                Animate(61, 10, 1, true, false, 0);
                Flying = false;
                BuffInfo.RemoveBuff( this, BuffIcon.Fly );
                SendMessage("You have landed.");

                BaseMount.Dismount(this);
                return;
            }

            BlockMountType type = BaseMount.GetMountPrevention(this);

            if (!Alive)
                SendLocalizedMessage(1113082); // You may not fly while dead.
            else if (IsBodyMod && !(BodyMod == 666 || BodyMod == 667))
                SendLocalizedMessage(1112453); // You can't fly in your current form!
            else if (type != BlockMountType.None)
            {
                switch (type)
                {
					case BlockMountType.Dazed: SendLocalizedMessage( 1112457 ); break; // You are still too dazed to fly.
					case BlockMountType.BolaRecovery: SendLocalizedMessage(1112455); break; // You cannot fly while recovering from a bola throw.
					case BlockMountType.DismountRecovery: SendLocalizedMessage(1112456); break; // You cannot fly while recovering from a dismount maneuver.
                }
                return;
            }
            else if (Hits < 25) // TODO confirm
                SendLocalizedMessage(1112454); // You must heal before flying.
            else
            {
				if ( !Flying )
				{
					// No message?

                	if (Spell is FlySpell)
                	{
                	    FlySpell spell = (FlySpell)Spell;
               	 	    spell.Stop();
                	}
	                new FlySpell(this).Cast();
    	        }
				else
				{
					Flying = false;
					BuffInfo.RemoveBuff( this, BuffIcon.Fly );
				}
			}
        }
        #endregion

        private class CountAndTimeStamp
        {
            private int m_Count;
            private DateTime m_Stamp;

            public CountAndTimeStamp()
            {
            }

            public DateTime TimeStamp
			{
				get
				{
					return m_Stamp;
				}
			}
            public int Count
            {
                get
				{
					return m_Count;
				}
                set
				{
					m_Count = value;
					m_Stamp = DateTime.Now;
				}
            }
        }
/*GD13_HS_Start_2
		#region hunting
		private HuntMode m_HuntMode = HuntMode.None;
		private HuntRank m_HuntRank = HuntRank.None;
		private HuntReward m_HuntReward = HuntReward.None;
		private bool m_Hunting = false;
		private int m_EasyKills = 0;
		private int m_HardKills = 0;
		private int m_ExtremeKills = 0;
		private int m_GodKills = 0;

		[CommandProperty( AccessLevel.Administrator )]
		public HuntMode HuntingMode
		{
			get { return m_HuntMode; }
			set { m_HuntMode = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public HuntRank HuntingRank
		{
			get { return m_HuntRank; }
			set { m_HuntRank = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public HuntReward HuntingReward
		{
			get { return m_HuntReward; }
			set { m_HuntReward = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool Hunting
		{
			get{ return m_Hunting; }
			set{ m_Hunting = value; }
		}
		
		[CommandProperty( AccessLevel.Administrator )]
		public int EasyKills
		{
			get { return m_EasyKills; }
			set { m_EasyKills = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int HardKills
		{
			get { return m_HardKills; }
			set { m_HardKills = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int ExtremeKills
		{
			get { return m_ExtremeKills; }
			set { m_ExtremeKills = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int GodKills
		{
			get { return m_GodKills; }
			set { m_GodKills = value; }
		}
		#endregion
/*GD13_HS_End_2*/
        private DesignContext m_DesignContext;
        private NpcGuild m_NpcGuild;
        private DateTime m_NpcGuildJoinTime;
        private DateTime m_NextBODTurnInTime;
        private TimeSpan m_NpcGuildGameTime;
        private PlayerFlag m_Flags;
        private int m_StepsTaken;
        private int m_Profession;
		
		#region Player Government
		private CityManagementStone m_City;
		private string m_CityTitle;
		private bool m_ShowCityTitle;
		private bool m_OwesBackTaxes;
		private int m_BackTaxesAmount;

		[CommandProperty( AccessLevel.GameMaster )]
		public CityManagementStone City
		{
			get{ return m_City; }
			set{ m_City = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string CityTitle
		{
			get{ return m_CityTitle; }
			set{ m_CityTitle = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool ShowCityTitle
		{
			get{ return m_ShowCityTitle; }
			set{ m_ShowCityTitle = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool OwesBackTaxes
		{
			get{ return m_OwesBackTaxes; }
			set{ m_OwesBackTaxes = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int BackTaxesAmount
		{
			get{ return m_BackTaxesAmount; }
			set{ m_BackTaxesAmount = value; }
		}
		#endregion
//-----------------------------------------------LIFE STONE SYSTEM PART 1----------------------------------------------------------------------------
		public Point3D LifeStone;
		public bool Bound = false;
		public Map LifeStoneMap;
//-----------------------------------------------LIFE STONE SYSTEM PART 1----------------------------------------------------------------------------

//Freyd's Vampire System
        private int m_Vampire;
        [CommandProperty(AccessLevel.GameMaster)]
        public int Vampire { get { return m_Vampire; } set { m_Vampire = value; } }   

        private int m_VampireBited;
        [CommandProperty(AccessLevel.GameMaster)]
        public int VampireBited { get { return m_VampireBited; } set { m_VampireBited = value; } }

        private DateTime m_VampireBiteTime;
        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan VampireBiteTime
        {
            get
            {
                TimeSpan ts = m_VampireBiteTime - DateTime.Now;

                if (ts < TimeSpan.Zero)
                    ts = TimeSpan.Zero;

                return ts;
            }
            set
            {
                try { m_VampireBiteTime = DateTime.Now + value; }
                catch { }
            }
        }          
        private int m_Werewolves;
        [CommandProperty(AccessLevel.GameMaster)]
        public int Werewolf { get { return m_Werewolves; } set { m_Werewolves = value; } }   

        private int m_WerewolfBited;
        [CommandProperty(AccessLevel.GameMaster)]
        public int WerewolfBited { get { return m_WerewolfBited; } set { m_WerewolfBited = value; } }

        private DateTime m_WerewolfBiteTime;
        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan WerewolfBiteTime
        {
            get
            {
                TimeSpan ts = m_WerewolfBiteTime - DateTime.Now;

                if (ts < TimeSpan.Zero)
                    ts = TimeSpan.Zero;

                return ts;
            }
            set
            {
                try { m_WerewolfBiteTime = DateTime.Now + value; }
                catch { }
            }
        }		
//Freyd's Vampire System

        private bool m_IsStealthing; // IsStealthing should be moved to Server.Mobiles
		private bool m_IgnoreMobiles; // IgnoreMobiles should be moved to Server.Mobiles
 	 	private int m_NonAutoreinsuredItems; // number of items that could not be automaitically reinsured because gold in bank was not enough
		private bool m_NinjaWepCooldown;
		/* 
		 * a value of zero means, that the mobile is not executing the spell. Otherwise,
		 * the value should match the BaseMana required 
		*/
		private int m_ExecutesLightningStrike; // move to Server.Mobiles??

        private DateTime m_LastOnline;
        private Server.Guilds.RankDefinition m_GuildRank;

        private int m_GuildMessageHue, m_AllianceMessageHue;

        private List<Mobile> m_AutoStabled;
        private List<Mobile> m_AllFollowers;
		private List<Mobile> m_RecentlyReported;

        #region Shadows Edge - Automated Server Staff

        private DateTime m_PromoGiftLast;
        private DateTime m_LastTimePaged;
        
        #endregion Edited By: A.A.R
        #region Guantlet Points
        private double m_GauntletPoints;

        [CommandProperty(AccessLevel.Administrator)]
        public double GauntletPoints
        {
            get { return m_GauntletPoints; }
            set { m_GauntletPoints = value; }
        } 
        #endregion

        #region Getters & Setters

		public List<Mobile> RecentlyReported
		{
			get
			{
				return m_RecentlyReported;
			}
			set
			{
				m_RecentlyReported = value;
			}
		}

        public List<Mobile> AutoStabled
		{
			get
			{
				return m_AutoStabled;
			}
		}

		public bool NinjaWepCooldown
		{
			get
			{
				return m_NinjaWepCooldown;
			}
			set
			{
				m_NinjaWepCooldown = value;
			}
		}

        public List<Mobile> AllFollowers
        {
            get
            {
                if (m_AllFollowers == null)
					m_AllFollowers = new List<Mobile>();
                return m_AllFollowers;
            }
        }

        public Server.Guilds.RankDefinition GuildRank
        {
            get
            {
                if (this.AccessLevel >= AccessLevel.GameMaster)
                    return Server.Guilds.RankDefinition.Leader;
                else
                    return m_GuildRank;
            }
            set
			{
				m_GuildRank = value;
			}
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int GuildMessageHue
        {
            get
			{
				return m_GuildMessageHue;
			}
            set
			{
				m_GuildMessageHue = value;
			}
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int AllianceMessageHue
        {
            get
			{
				return m_AllianceMessageHue;
			}
            set
			{
				m_AllianceMessageHue = value;
			}
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Profession
        {
            get
			{
				return m_Profession;
			}
            set
			{
				m_Profession = value;
			}
        }

        public int StepsTaken
        {
            get
			{
				return m_StepsTaken;
			}
            set
			{
				m_StepsTaken = value;
			}
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsStealthing // IsStealthing should be moved to Server.Mobiles
        {
            get
			{
				return m_IsStealthing;
			}
            set
			{
				m_IsStealthing = value;
			}
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public bool IgnoreMobiles // IgnoreMobiles should be moved to Server.Mobiles
		{
			get
			{
				return m_IgnoreMobiles;
			}
			set
			{
				if( m_IgnoreMobiles != value )
				{
					m_IgnoreMobiles = value;
					Delta( MobileDelta.Flags );
				}
			}
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
        public NpcGuild NpcGuild
        {
            get
			{
				return m_NpcGuild;
			}
            set
			{
				m_NpcGuild = value;
			}
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime NpcGuildJoinTime
        {
            get
			{
				return m_NpcGuildJoinTime;
			}
            set
			{
				m_NpcGuildJoinTime = value;
			}
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime NextBODTurnInTime
        {
            get
			{
				return m_NextBODTurnInTime;
			}
            set
			{
				m_NextBODTurnInTime = value;
			}
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastOnline
        {
            get
			{
				return m_LastOnline;
			}
            set
			{
				m_LastOnline = value;
			}
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NpcGuildGameTime
        {
            get
			{
				return m_NpcGuildGameTime;
			}
            set
			{
				m_NpcGuildGameTime = value;
			}
        }

        #region Shadows Edge - Automated Server Staff

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime PromoGiftLast
        {
            get { return m_PromoGiftLast; }
            set { m_PromoGiftLast = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastTimePaged
        {
            get { return m_LastTimePaged; }
            set { m_LastTimePaged = value; }
        }

        #endregion Edited By: A.A.R

        private int m_ToTItemsTurnedIn;

        [CommandProperty(AccessLevel.GameMaster)]
        public int ToTItemsTurnedIn
        {
            get
			{
				return m_ToTItemsTurnedIn;
			}
            set
			{
				m_ToTItemsTurnedIn = value;
			}
        }

        private int m_ToTTotalMonsterFame;

        [CommandProperty(AccessLevel.GameMaster)]
        public int ToTTotalMonsterFame
        {
            get
			{
				return m_ToTTotalMonsterFame;
			}
            set
			{
				m_ToTTotalMonsterFame = value;
			}
        }

        public int ExecutesLightningStrike
        {
            get { return m_ExecutesLightningStrike; }
            set { m_ExecutesLightningStrike = value; }
        }

        private int m_VASTotalMonsterFame;

        [CommandProperty(AccessLevel.GameMaster)]
        public int VASTotalMonsterFame
        {
            get { return m_VASTotalMonsterFame; }
            set { m_VASTotalMonsterFame = value; }
        }
        #endregion
        #region QueensLoyaltySystem              // Queens Loyalty System/////////////////////////////////////////////////////////////

        private long m_LevelExp;         // Experience Needed for next Experience Level

        [CommandProperty(AccessLevel.Owner)]
        public long LevelExp
        {
            get { return m_LevelExp; }
            set { m_LevelExp = value; InvalidateProperties(); }
        }

        private long m_Exp;              // Experience at the current Experience Level
        [CommandProperty(AccessLevel.GameMaster)]
        public long Exp
        {
            get { return m_Exp; }
            set { m_Exp = value; InvalidateProperties(); }
        }

        private int m_Level;            // Experience Level
        [CommandProperty(AccessLevel.GameMaster)]
        public int Level
        {
            get { return m_Level; }
            set { m_Level = value; InvalidateProperties(); }
        }

        public string m_ExpTitle;       // Title based on both levels
        [CommandProperty(AccessLevel.Owner)]
        public string ExpTitle
        {
            get { return m_ExpTitle; }
            set { m_ExpTitle = value; InvalidateProperties(); }
        }

        #endregion
		private DateTime m_LastEmote;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime LastEmote
		{
			get{ return m_LastEmote; }
			set{ m_LastEmote = value; }
		}
		
        #region PlayerFlags
        public PlayerFlag Flags
        {
            get { return m_Flags; }
            set { m_Flags = value; }
        }

		[CommandProperty(AccessLevel.GameMaster)]
		public bool FireRockMining
		{
			get { return GetFlag(PlayerFlag.FireRockMining); }
			set { SetFlag(PlayerFlag.FireRockMining, value); }
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PagingSquelched
        {
            get { return GetFlag(PlayerFlag.PagingSquelched); }
            set { SetFlag(PlayerFlag.PagingSquelched, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Glassblowing
        {
            get { return GetFlag(PlayerFlag.Glassblowing); }
            set { SetFlag(PlayerFlag.Glassblowing, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Masonry
        {
            get { return GetFlag(PlayerFlag.Masonry); }
            set { SetFlag(PlayerFlag.Masonry, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SandMining
        {
            get { return GetFlag(PlayerFlag.SandMining); }
            set { SetFlag(PlayerFlag.SandMining, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool StoneMining
        {
            get { return GetFlag(PlayerFlag.StoneMining); }
            set { SetFlag(PlayerFlag.StoneMining, value); }
        }
    
        [CommandProperty(AccessLevel.GameMaster)]
        public bool GemMining
        {
            get { return GetFlag(PlayerFlag.GemMining); }
            set { SetFlag(PlayerFlag.GemMining, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool BasketWeaving
        {
            get { return GetFlag(PlayerFlag.BasketWeaving); }
            set { SetFlag(PlayerFlag.BasketWeaving, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ToggleMiningStone
        {
            get { return GetFlag(PlayerFlag.ToggleMiningStone); }
            set { SetFlag(PlayerFlag.ToggleMiningStone, value); }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public bool AbyssEntry
        {
            get { return GetFlag(PlayerFlag.AbyssEntry); }
            set { SetFlag(PlayerFlag.AbyssEntry, value); }
        }
		
        #region Sutek Quest system

        [CommandProperty(AccessLevel.GameMaster)]
        public bool MechanicalLife
        {
            get { return GetFlag(PlayerFlag.MechanicalLife); }
            set { SetFlag(PlayerFlag.MechanicalLife, value); }
        }

        #endregion
		
        #region Plant system
        
        [CommandProperty(AccessLevel.GameMaster)]
        public bool ToggleClippings
        {
            get { return GetFlag(PlayerFlag.ToggleClippings); }
            set { SetFlag(PlayerFlag.ToggleClippings, value); }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public bool ToggleCutReeds
        {
            get { return GetFlag(PlayerFlag.ToggleCutReeds); }
            set { SetFlag(PlayerFlag.ToggleCutReeds, value); }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public bool ToggleCutClippings
        {
            get { return GetFlag(PlayerFlag.ToggleCutClippings); }
            set { SetFlag(PlayerFlag.ToggleCutClippings, value); }
        }
    
        [CommandProperty(AccessLevel.GameMaster)]
        public bool ToggleMiningGem
        {
            get { return GetFlag(PlayerFlag.ToggleMiningGem); }
            set { SetFlag(PlayerFlag.ToggleMiningGem, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool KarmaLocked
        {
            get { return GetFlag(PlayerFlag.KarmaLocked); }
            set { SetFlag(PlayerFlag.KarmaLocked, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AutoRenewInsurance
        {
            get { return GetFlag(PlayerFlag.AutoRenewInsurance); }
            set { SetFlag(PlayerFlag.AutoRenewInsurance, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool UseOwnFilter
        {
            get { return GetFlag(PlayerFlag.UseOwnFilter); }
            set { SetFlag(PlayerFlag.UseOwnFilter, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PublicMyRunUO
        {
            get { return GetFlag(PlayerFlag.PublicMyRunUO); }
            set { SetFlag(PlayerFlag.PublicMyRunUO, value); InvalidateMyRunUO(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AcceptGuildInvites
        {
            get { return GetFlag(PlayerFlag.AcceptGuildInvites); }
            set { SetFlag(PlayerFlag.AcceptGuildInvites, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool HasStatReward
        {
            get { return GetFlag(PlayerFlag.HasStatReward); }
            set { SetFlag(PlayerFlag.HasStatReward, value); }
        }
        #endregion

        #region Auto Arrow Recovery
        private Dictionary<Type, int> m_RecoverableAmmo = new Dictionary<Type, int>();

        public Dictionary<Type, int> RecoverableAmmo
        {
            get { return m_RecoverableAmmo; }
        }

        public void RecoverAmmo()
        {
            if (Core.SE && Alive)
            {
                foreach (KeyValuePair<Type, int> kvp in m_RecoverableAmmo)
                {
                    if (kvp.Value > 0)
                    {
                        Item ammo = null;

                        try
                        {
                            ammo = Activator.CreateInstance(kvp.Key) as Item;
                        }
                        catch
                        {
                        }

                        if (ammo != null)
                        {
                            string name = ammo.Name;
                            ammo.Amount = kvp.Value;

                            if (name == null)
                            {
                                if (ammo is Arrow)
                                    name = "arrow";
                                else if (ammo is Bolt)
                                    name = "bolt";
                            }

                            if (name != null && ammo.Amount > 1)
                                name = String.Format("{0}s", name);

                            if (name == null)
                                name = String.Format("#{0}", ammo.LabelNumber);

                            PlaceInBackpack(ammo);
                            SendLocalizedMessage(1073504, String.Format("{0}\t{1}", ammo.Amount, name)); // You recover ~1_NUM~ ~2_AMMO~.
                        }
                    }
                }

                m_RecoverableAmmo.Clear();
            }
        }
        #endregion

        #region SA
		private DateTime m_SSNextSeed;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime SSNextSeed
		{
			get{ return m_SSNextSeed; }
			set{ m_SSNextSeed = value; }
		}

		private DateTime m_SSSeedExpire;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime SSSeedExpire
		{
			get{ return m_SSSeedExpire; }
			set{ m_SSSeedExpire = value; }
		}

        private Point3D m_SSSeedLocation;

		public Point3D SSSeedLocation
		{
			get{ return m_SSSeedLocation; }
			set{ m_SSSeedLocation = value; }
		}

        private Map m_SSSeedMap;

		public Map SSSeedMap
		{
			get{ return m_SSSeedMap; }
			set{ m_SSSeedMap = value; }
		}
        #endregion

        private DateTime m_AnkhNextUse;

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime AnkhNextUse
        {
            get { return m_AnkhNextUse; }
            set { m_AnkhNextUse = value; }
        }

        #region Mondain's Legacy
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Bedlam
        {
            get { return GetFlag(PlayerFlag.Bedlam); }
            set { SetFlag(PlayerFlag.Bedlam, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool LibraryFriend
        {
            get { return GetFlag(PlayerFlag.LibraryFriend); }
            set { SetFlag(PlayerFlag.LibraryFriend, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Spellweaving
        {
            get { return GetFlag(PlayerFlag.Spellweaving); }
            set { SetFlag(PlayerFlag.Spellweaving, value); }
        }
        #endregion

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan DisguiseTimeLeft
		{
			get{ return DisguiseTimers.TimeRemaining( this ); }
		}

        #region Scroll of Alacrity
        private DateTime m_AcceleratedStart;

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime AcceleratedStart
        {
            get { return m_AcceleratedStart; }
            set { m_AcceleratedStart = value; }
        }

        private SkillName m_AcceleratedSkill;

        [CommandProperty(AccessLevel.GameMaster)]
        public SkillName AcceleratedSkill
        {
            get { return m_AcceleratedSkill; }
            set { m_AcceleratedSkill = value; }
        }
        #endregion

        public static Direction GetDirection4(Point3D from, Point3D to)
        {
            int dx = from.X - to.X;
            int dy = from.Y - to.Y;

            int rx = dx - dy;
            int ry = dx + dy;

            Direction ret;

            if (rx >= 0 && ry >= 0)
                ret = Direction.West;
            else if (rx >= 0 && ry < 0)
                ret = Direction.South;
            else if (rx < 0 && ry < 0)
                ret = Direction.East;
            else
                ret = Direction.North;

            return ret;
        }

        public override bool OnDroppedItemToWorld(Item item, Point3D location)
        {
            if (!base.OnDroppedItemToWorld(item, location))
                return false;

            IPooledEnumerable mobiles = Map.GetMobilesInRange(location, 0);

            foreach (Mobile m in mobiles)
            {
                if (m.Z >= location.Z && m.Z < location.Z + 16)
                {
                    mobiles.Free();
                    return false;
                }
            }

            mobiles.Free();

            BounceInfo bi = item.GetBounce();

            if (bi != null)
            {
                Type type = item.GetType();

                if (type.IsDefined(typeof(FurnitureAttribute), true) || type.IsDefined(typeof(DynamicFlipingAttribute), true))
                {
                    object[] objs = type.GetCustomAttributes(typeof(FlipableAttribute), true);

                    if (objs != null && objs.Length > 0)
                    {
                        FlipableAttribute fp = objs[0] as FlipableAttribute;

                        if (fp != null)
                        {
                            int[] itemIDs = fp.ItemIDs;

                            Point3D oldWorldLoc = bi.m_WorldLoc;
                            Point3D newWorldLoc = location;

                            if (oldWorldLoc.X != newWorldLoc.X || oldWorldLoc.Y != newWorldLoc.Y)
                            {
                                Direction dir = GetDirection4(oldWorldLoc, newWorldLoc);

                                if (itemIDs.Length == 2)
                                {
                                    switch (dir)
                                    {
                                        case Direction.North:
                                        case Direction.South: item.ItemID = itemIDs[0]; break;
                                        case Direction.East:
                                        case Direction.West: item.ItemID = itemIDs[1]; break;
                                    }
                                }
                                else if (itemIDs.Length == 4)
                                {
                                    switch (dir)
                                    {
                                        case Direction.South: item.ItemID = itemIDs[0]; break;
                                        case Direction.East: item.ItemID = itemIDs[1]; break;
                                        case Direction.North: item.ItemID = itemIDs[2]; break;
                                        case Direction.West: item.ItemID = itemIDs[3]; break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

		public override int GetPacketFlags()
		{
			int flags = base.GetPacketFlags();
			
			if ( m_IgnoreMobiles )
				flags |= 0x10;
			
			return flags;
		}
		
		public override int GetOldPacketFlags()
		{
			int flags = base.GetOldPacketFlags();
			
			if ( m_IgnoreMobiles )
				flags |= 0x10;
			
			return flags;
		}

        public bool GetFlag(PlayerFlag flag)
        {
            return ((m_Flags & flag) != 0);
        }

        public void SetFlag(PlayerFlag flag, bool value)
        {
            if (value)
                m_Flags |= flag;
            else
                m_Flags &= ~flag;
        }

        public DesignContext DesignContext
        {
            get { return m_DesignContext; }
            set { m_DesignContext = value; }
        }

        public static void Initialize()
        {
            if (FastwalkPrevention)
                PacketHandlers.RegisterThrottler(0x02, new ThrottlePacketCallback(MovementThrottle_Callback));

            EventSink.Login += new LoginEventHandler(OnLogin);
            EventSink.Logout += new LogoutEventHandler(OnLogout);
            EventSink.Connected += new ConnectedEventHandler(EventSink_Connected);
            EventSink.Disconnected += new DisconnectedEventHandler(EventSink_Disconnected);

            if (Core.SE)
            {
                Timer.DelayCall(TimeSpan.Zero, new TimerCallback(CheckPets));
            }
        }

        private static void CheckPets()
        {
            foreach (Mobile m in World.Mobiles.Values)
            {
                if (m is PlayerMobile)
                {
                    PlayerMobile pm = (PlayerMobile)m;

                    if (((!pm.Mounted || (pm.Mount != null && pm.Mount is EtherealMount)) && (pm.AllFollowers.Count > pm.AutoStabled.Count)) ||
                        (pm.Mounted && (pm.AllFollowers.Count > (pm.AutoStabled.Count + 1))))
                    {
                        pm.AutoStablePets(); /* autostable checks summons, et al: no need here */
                    }
                }
            }
        }

        public override void OnSkillInvalidated(Skill skill)
        {
            if (Core.AOS && skill.SkillName == SkillName.MagicResist)
                UpdateResistances();
        }

        public override int GetMaxResistance(ResistanceType type)
        {
            if (AccessLevel > AccessLevel.Player)
                return int.MaxValue;

            int max = base.GetMaxResistance(type);

            if (type != ResistanceType.Physical && 60 < max && Spells.Fourth.CurseSpell.UnderEffect(this))
                max = 60;

            if (Core.ML && this.Race == Race.Elf && type == ResistanceType.Energy)
                max += 5; //Intended to go after the 60 max from curse
//Freyd's Vampire System
            if (this.Vampire != 0 && 45 < max && type == ResistanceType.Fire)
                max = 45;
				
            if (this.Werewolf != 0 && 45 < max && type == ResistanceType.Energy)
                max = 45;
//Freyd's Vampire System


            return max;
        }

        protected override void OnRaceChange(Race oldRace)
        {
            ValidateEquipment();
            UpdateResistances();
        }

        public override int MaxWeight { get { return (((Core.ML && this.Race == Race.Human) ? 100 : 40) + (int)(3.5 * this.Str)); } }

        private int m_LastGlobalLight = -1, m_LastPersonalLight = -1;

        public override void OnNetStateChanged()
        {
            m_LastGlobalLight = -1;
            m_LastPersonalLight = -1;
        }

        public override void ComputeBaseLightLevels(out int global, out int personal)
        {
            global = LightCycle.ComputeLevelFor(this);

            bool racialNightSight = (Core.ML && this.Race == Race.Elf);

            if (this.LightLevel < 11 && (AosAttributes.GetValue(this, AosAttribute.NightSight) > 0 || racialNightSight))
                personal = 11;
            else
                personal = this.LightLevel;
        }

        public override void CheckLightLevels(bool forceResend)
        {
            NetState ns = this.NetState;

            if (ns == null)
                return;

            int global, personal;

            ComputeLightLevels(out global, out personal);

            if (!forceResend)
                forceResend = (global != m_LastGlobalLight || personal != m_LastPersonalLight);

            if (!forceResend)
                return;

            m_LastGlobalLight = global;
            m_LastPersonalLight = personal;

            ns.Send(GlobalLightLevel.Instantiate(global));
            ns.Send(new PersonalLightLevel(this, personal));
        }

        public override int GetMinResistance(ResistanceType type)
        {
            int magicResist = (int)(Skills[SkillName.MagicResist].Value * 10);
            int min = int.MinValue;

            if (magicResist >= 1000)
                min = 40 + ((magicResist - 1000) / 50);
            else if (magicResist >= 400)
                min = (magicResist - 400) / 15;

            if (min > MaxPlayerResistance)
                min = MaxPlayerResistance;

            int baseMin = base.GetMinResistance(type);

            if (min < baseMin)
                min = baseMin;

            return min;
        }

		public override void OnManaChange(int oldValue)
		{
			base.OnManaChange(oldValue);
			if (m_ExecutesLightningStrike > 0)
			{
				if (Mana < m_ExecutesLightningStrike)
				{
					LightningStrike.ClearCurrentMove(this);
				}
			}
		}

        private static void OnLogin(LoginEventArgs e)
        {
            Mobile from = e.Mobile;
			//Time System Adjust
			//TimeSystem.System.CheckSeason();
//Freyd's Vampire System
            PlayerMobile vampire = e.Mobile as PlayerMobile;
            if (vampire.Vampire > 0 && VampireSystem.Vampires)
            {
                vampire.Title = "the Vampire";
                vampire.AddStatMod(new StatMod(StatType.Str, "Vampire Str Bonus", VampireSystem.VampireStrBonus, TimeSpan.Zero));
                vampire.AddStatMod(new StatMod(StatType.Dex, "Vampire Dex Bonus", VampireSystem.VampireDexBonus, TimeSpan.Zero));
                vampire.AddStatMod(new StatMod(StatType.Int, "Vampire Int Bonus", VampireSystem.VampireIntBonus, TimeSpan.Zero));                
            }
            if (vampire.VampireBited == 1)
                vampire.VampireBited = 0;
            if (vampire.Vampire > 0 && vampire.Alive && vampire.VampireBiteTime == TimeSpan.Zero)
                new VampireTimer(vampire, 120.0).Start();
		if (vampire.Vampire > 0) vampire.SendGump( new VampireGump() );  

            PlayerMobile werewolf = e.Mobile as PlayerMobile;
            if (vampire.Werewolf > 0 && VampireSystem.Werewolves)
            {
                vampire.Title = "the Werewolf";
                vampire.AddStatMod(new StatMod(StatType.Str, "Werewolf Str Bonus", VampireSystem.WerewolfStr, TimeSpan.Zero));
                vampire.AddStatMod(new StatMod(StatType.Dex, "Werewolf Dex Bonus", VampireSystem.WerewolfDex, TimeSpan.Zero));
                vampire.AddStatMod(new StatMod(StatType.Int, "Werewolf Int Bonus", VampireSystem.WerewolfInt, TimeSpan.Zero));                
            }
            if (vampire.WerewolfBited == 1)
                vampire.WerewolfBited = 0;
            if (vampire.Werewolf > 0 && vampire.Alive && vampire.WerewolfBiteTime == TimeSpan.Zero)
                new WerewolfTimer(vampire, 120.0).Start();
		if (vampire.Werewolf > 0) vampire.SendGump( new WerewolfGump() );		
//Freyd's Vampire System

// Star Wars NotoGump
/*
		if (e.Mobile.Title == "the Jedi" | e.Mobile.Title == "the Sith Apprentice" | e.Mobile.Title == "the Jedi Exile")
			e.Mobile.SendGump( new NotoGump( from ) );
*/
// Star Wars NotoGump


            /////////////            
            #region - Party Dungeon System Edits -
                
            if (from.AccessLevel == AccessLevel.Player)
            {
                Region reg = Region.Find(from.Location, from.Map);

                if (reg is GroupDungeonRegion)
                {
                    GroupDungeonRegion dreg = (GroupDungeonRegion)reg;

                    //dungeon full so kick
                    if ( dreg.CountPlayers() > dreg.Stone.MaxPlayers)
                    {
                        from.SendMessage(34, "{0} is full right now. You are being teleported out.", dreg.Stone.DungeonName);
                        Timer.DelayCall(TimeSpan.FromSeconds(5), new TimerStateCallback(Server.Regions.GroupDungeonRegion.KickCallBack), new object[] { from, dreg.Stone });
                    }
                    //dungeon empty so kick to allow for reset
                    else if (dreg.CountPlayers() <= 1)
                    {
                        from.SendMessage(34, "You have logged into an empty dungeon. You are being teleported out.", dreg.Stone.DungeonName);
                        Timer.DelayCall(TimeSpan.FromSeconds(5), new TimerStateCallback(Server.Regions.GroupDungeonRegion.KickCallBack), new object[] { from, dreg.Stone });
                    }
                    
                        //not in the current party so kick

                    else
                    {
                        bool isinparty = false;
                        PlayerMobile pm = (PlayerMobile)from;
                        Server.Engines.PartySystem.Party p = Server.Engines.PartySystem.Party.Get(pm);

                        if (p != null)
                        {
                            foreach (Mobile mobs in dreg.Stone.GetMobilesInRange(dreg.Stone.Size))
                            {
                                for (int i = 0; i < p.Members.Count; i++)
                                {
                                    Server.Engines.PartySystem.PartyMemberInfo pmem = (Server.Engines.PartySystem.PartyMemberInfo)p.Members[i];
                                    if (pmem.Mobile == mobs)
                                        isinparty = true;
                                }
                            }
                        }
                        
                        if (!isinparty)
                        {
                            from.SendMessage(34, "You must join the party inside to enter. You are being teleported out.");
                            Timer.DelayCall(TimeSpan.FromSeconds(5), new TimerStateCallback(Server.Regions.GroupDungeonRegion.KickCallBack), new object[] { from, dreg.Stone });
                        }
                        else
                            reg.OnLocationChanged(from, from.Location); //set up afk timer for this player
                    }
                }
            }

            #endregion 
            /////////////

			
            CheckAtrophies(from);

            if (AccountHandler.LockdownLevel > AccessLevel.Player)
            {
                string notice;

                Accounting.Account acct = from.Account as Accounting.Account;

                if (acct == null || !acct.HasAccess(from.NetState))
                {
                    if (from.AccessLevel == AccessLevel.Player)
                        notice = "The server is currently under lockdown. No players are allowed to log in at this time.";
                    else
                        notice = "The server is currently under lockdown. You do not have sufficient access level to connect.";

                    Timer.DelayCall(TimeSpan.FromSeconds(1.0), new TimerStateCallback(Disconnect), from);
                }
                else if (from.AccessLevel >= AccessLevel.Administrator)
                {
                    notice = "The server is currently under lockdown. As you are an administrator, you may change this from the [Admin gump.";
                }
                else
                {
                    notice = "The server is currently under lockdown. You have sufficient access level to connect.";
                }

                from.SendGump(new NoticeGump(1060637, 30720, notice, 0xFFC000, 300, 140, null, null));
                return;
            }

            if (from is PlayerMobile)
                ((PlayerMobile)from).ClaimAutoStabledPets();
        }
        private bool m_NoDeltaRecursion;

        public void ValidateEquipment()
        {
            if (m_NoDeltaRecursion || Map == null || Map == Map.Internal)
                return;

            if (this.Items == null)
                return;

            m_NoDeltaRecursion = true;
            Timer.DelayCall(TimeSpan.Zero, new TimerCallback(ValidateEquipment_Sandbox));
        }

        private void ValidateEquipment_Sandbox()
        {
            try
            {
                if (Map == null || Map == Map.Internal)
                    return;

                List<Item> items = this.Items;

                if (items == null)
                    return;

                bool moved = false;

                int str = this.Str;
                int dex = this.Dex;
                int intel = this.Int;

                #region Factions
                int factionItemCount = 0;
                #endregion

                Mobile from = this;

                #region Ethics
                Ethics.Ethic ethic = Ethics.Ethic.Find(from);
                #endregion

                for (int i = items.Count - 1; i >= 0; --i)
                {
                    if (i >= items.Count)
                        continue;

                    Item item = items[i];

                    #region Ethics
                    if ((item.SavedFlags & 0x100) != 0)
                    {
                        if (item.Hue != Ethics.Ethic.Hero.Definition.PrimaryHue)
                        {
                            item.SavedFlags &= ~0x100;
                        }
                        else if (ethic != Ethics.Ethic.Hero)
                        {
                            from.AddToBackpack(item);
                            moved = true;
                            continue;
                        }
                    }
                    else if ((item.SavedFlags & 0x200) != 0)
                    {
                        if (item.Hue != Ethics.Ethic.Evil.Definition.PrimaryHue)
                        {
                            item.SavedFlags &= ~0x200;
                        }
                        else if (ethic != Ethics.Ethic.Evil)
                        {
                            from.AddToBackpack(item);
                            moved = true;
                            continue;
                        }
                    }
                    #endregion

                    if (item is BaseWeapon)
                    {
                        BaseWeapon weapon = (BaseWeapon)item;

                        bool drop = false;

                        if (dex < weapon.DexRequirement)
                            drop = true;
                        else if (str < AOS.Scale(weapon.StrRequirement, 100 - weapon.GetLowerStatReq()))
                            drop = true;
                        else if (intel < weapon.IntRequirement)
                            drop = true;
                        else if (weapon.RequiredRace != null && weapon.RequiredRace != this.Race)
                            drop = true;

                        if (drop)
                        {
                            string name = weapon.Name;

                            if (name == null)
                                name = String.Format("#{0}", weapon.LabelNumber);

                            from.SendLocalizedMessage(1062001, name); // You can no longer wield your ~1_WEAPON~
                            from.AddToBackpack(weapon);
                            moved = true;
                        }
                    }
                    else if (item is BaseArmor)
                    {
                        BaseArmor armor = (BaseArmor)item;

                        bool drop = false;

                        if (!armor.AllowMaleWearer && !from.Female && from.AccessLevel < AccessLevel.GameMaster)
                        {
                            drop = true;
                        }
                        else if (!armor.AllowFemaleWearer && from.Female && from.AccessLevel < AccessLevel.GameMaster)
                        {
                            drop = true;
                        }
                        else if (armor.RequiredRace != null && armor.RequiredRace != this.Race)
                        {
                            drop = true;
                        }
                        else
                        {
                            int strBonus = armor.ComputeStatBonus(StatType.Str), strReq = armor.ComputeStatReq(StatType.Str);
                            int dexBonus = armor.ComputeStatBonus(StatType.Dex), dexReq = armor.ComputeStatReq(StatType.Dex);
                            int intBonus = armor.ComputeStatBonus(StatType.Int), intReq = armor.ComputeStatReq(StatType.Int);

                            if (dex < dexReq || (dex + dexBonus) < 1)
                                drop = true;
                            else if (str < strReq || (str + strBonus) < 1)
                                drop = true;
                            else if (intel < intReq || (intel + intBonus) < 1)
                                drop = true;
                        }

                        if (drop)
                        {
                            string name = armor.Name;

                            if (name == null)
                                name = String.Format("#{0}", armor.LabelNumber);

                            if (armor is BaseShield)
                                from.SendLocalizedMessage(1062003, name); // You can no longer equip your ~1_SHIELD~
                            else
                                from.SendLocalizedMessage(1062002, name); // You can no longer wear your ~1_ARMOR~

                            from.AddToBackpack(armor);
                            moved = true;
                        }
                    }
                    else if (item is BaseClothing)
                    {
                        BaseClothing clothing = (BaseClothing)item;

                        bool drop = false;

                        if (!clothing.AllowMaleWearer && !from.Female && from.AccessLevel < AccessLevel.GameMaster)
                        {
                            drop = true;
                        }
                        else if (!clothing.AllowFemaleWearer && from.Female && from.AccessLevel < AccessLevel.GameMaster)
                        {
                            drop = true;
                        }
                        else if (clothing.RequiredRace != null && clothing.RequiredRace != this.Race)
                        {
                            drop = true;
                        }
                        else
                        {
                            int strBonus = clothing.ComputeStatBonus(StatType.Str);
                            int strReq = clothing.ComputeStatReq(StatType.Str);

                            if (str < strReq || (str + strBonus) < 1)
                                drop = true;
                        }

                        if (drop)
                        {
                            string name = clothing.Name;

                            if (name == null)
                                name = String.Format("#{0}", clothing.LabelNumber);

                            from.SendLocalizedMessage(1062002, name); // You can no longer wear your ~1_ARMOR~

                            from.AddToBackpack(clothing);
                            moved = true;
                        }
                    }

                    FactionItem factionItem = FactionItem.Find(item);

                    if (factionItem != null)
                    {
                        bool drop = false;

                        Faction ourFaction = Faction.Find(this);

                        if (ourFaction == null || ourFaction != factionItem.Faction)
                            drop = true;
                        else if (++factionItemCount > FactionItem.GetMaxWearables(this))
                            drop = true;

                        if (drop)
                        {
                            from.AddToBackpack(item);
                            moved = true;
                        }
                    }
                }

                if (moved)
                    from.SendLocalizedMessage(500647); // Some equipment has been moved to your backpack.
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                m_NoDeltaRecursion = false;
            }
        }

        public override void Delta(MobileDelta flag)
        {
            base.Delta(flag);

            if ((flag & MobileDelta.Stat) != 0)
                ValidateEquipment();

            if ((flag & (MobileDelta.Name | MobileDelta.Hue)) != 0)
                InvalidateMyRunUO();
        }

        private static void Disconnect(object state)
        {
            NetState ns = ((Mobile)state).NetState;

            if (ns != null)
                ns.Dispose();
        }

        private static void OnLogout(LogoutEventArgs e)
        {
//Freyd's Vampire System
            Mobile from = e.Mobile;
			PlayerMobile pm = e.Mobile as PlayerMobile;
			if (pm.Vampire > 0)
			{
				pm.BodyMod = 0;
				pm.NameMod = null;
				pm.Title = null;
				pm.RemoveStatMod("Vampire Str Bonus");
				pm.RemoveStatMod("Vampire Dex Bonus");
				pm.RemoveStatMod("Vampire Int Bonus");
            if (!VampireSystem.Vampires)            
					pm.Vampire = 0;
			}

			if (pm.Werewolf > 0)
			{
				pm.BodyMod = 0;
				pm.NameMod = null;
				pm.Title = null;
				pm.RemoveStatMod("Werewolf Str Bonus");
				pm.RemoveStatMod("Werewolf Dex Bonus");
				pm.RemoveStatMod("Werewolf Int Bonus");     
				if (!VampireSystem.Werewolves)
					pm.Werewolf = 0;
			}
//Freyd's Vampire System

            if (e.Mobile is PlayerMobile)
                ((PlayerMobile)e.Mobile).AutoStablePets();

			#region Scroll of ALacrity
			if(((PlayerMobile)e.Mobile).AcceleratedStart > DateTime.Now)
			{
				((PlayerMobile)e.Mobile).AcceleratedStart = DateTime.Now;
				Server.Items.ScrollofAlacrity.AlacrityEnd((PlayerMobile)e.Mobile);
			}
			#endregion
        }

        private static void EventSink_Connected(ConnectedEventArgs e)
        {
            PlayerMobile pm = e.Mobile as PlayerMobile;

            if (pm != null)
            {
                pm.m_SessionStart = DateTime.Now;

                if (pm.m_Quest != null)
                    pm.m_Quest.StartTimer();

                #region Mondain's Legacy
                QuestHelper.StartTimer(pm);
                #endregion

                pm.BedrollLogout = false;
                pm.LastOnline = DateTime.Now;
            }

            DisguiseTimers.StartTimer(e.Mobile);

            Timer.DelayCall(TimeSpan.Zero, new TimerStateCallback(ClearSpecialMovesCallback), e.Mobile);
        }

        private static void ClearSpecialMovesCallback(object state)
        {
            Mobile from = (Mobile)state;

            SpecialMove.ClearAllMoves(from);
        }

        private static void EventSink_Disconnected(DisconnectedEventArgs e)
        {
            Mobile from = e.Mobile;
            DesignContext context = DesignContext.Find(from);

            if (context != null)
            {
                /* Client disconnected
                 *  - Remove design context
                 *  - Eject all from house
                 *  - Restore relocated entities
                 */

                // Remove design context
                DesignContext.Remove(from);

                // Eject all from house
                from.RevealingAction();

                foreach (Item item in context.Foundation.GetItems())
                    item.Location = context.Foundation.BanLocation;

                foreach (Mobile mobile in context.Foundation.GetMobiles())
                    mobile.Location = context.Foundation.BanLocation;

                // Restore relocated entities
                context.Foundation.RestoreRelocatedEntities();
            }

            PlayerMobile pm = e.Mobile as PlayerMobile;

            if (pm != null)
            {
                pm.m_GameTime += (DateTime.Now - pm.m_SessionStart);

                if (pm.m_Quest != null)
                    pm.m_Quest.StopTimer();

                #region Mondain's Legacy
                QuestHelper.StopTimer(pm);
                #endregion

                pm.m_SpeechLog = null;
                pm.LastOnline = DateTime.Now;
            }

            DisguiseTimers.StopTimer(from);
        }

        public override void RevealingAction()
        {
            if (m_DesignContext != null)
                return;

            Spells.Sixth.InvisibilitySpell.RemoveTimer(this);

            base.RevealingAction();

            m_IsStealthing = false; // IsStealthing should be moved to Server.Mobiles
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override bool Hidden
        {
            get
            {
                return base.Hidden;
            }
            set
            {
                base.Hidden = value;

                RemoveBuff(BuffIcon.Invisibility);	//Always remove, default to the hiding icon EXCEPT in the invis spell where it's explicitly set

                if (!Hidden)
                {
                    RemoveBuff(BuffIcon.HidingAndOrStealth);
                }
                else// if( !InvisibilitySpell.HasTimer( this ) )
                {
                    BuffInfo.AddBuff(this, new BuffInfo(BuffIcon.HidingAndOrStealth, 1075655));	//Hidden/Stealthing & You Are Hidden
                }
            }
        }

        public override void OnSubItemAdded(Item item)
        {
            if (AccessLevel < AccessLevel.GameMaster && item.IsChildOf(this.Backpack))
            {
                int maxWeight = WeightOverloading.GetMaxWeight(this);
                int curWeight = Mobile.BodyWeight + this.TotalWeight;

                if (curWeight > maxWeight)
                    this.SendLocalizedMessage(1019035, true, String.Format(" : {0} / {1}", curWeight, maxWeight));
            }
        }

        public override bool CanBeHarmful(Mobile target, bool message, bool ignoreOurBlessedness)
        {
            if (m_DesignContext != null || (target is PlayerMobile && ((PlayerMobile)target).m_DesignContext != null))
                return false;

            #region Mondain's Legacy
            if (Peaced)
            {
                // TODO message
                return false;
            }
            #endregion

            if ((target is BaseVendor && ((BaseVendor)target).IsInvulnerable) || target is PlayerVendor || target is TownCrier)
            {
                if (message)
                {
                    if (target.Title == null)
                        SendMessage("{0} the vendor cannot be harmed.", target.Name);
                    else
                        SendMessage("{0} {1} cannot be harmed.", target.Name, target.Title);
                }

                return false;
            }

            return base.CanBeHarmful(target, message, ignoreOurBlessedness);
        }

        public override bool CanBeBeneficial(Mobile target, bool message, bool allowDead)
        {
            if (m_DesignContext != null || (target is PlayerMobile && ((PlayerMobile)target).m_DesignContext != null))
                return false;

            return base.CanBeBeneficial(target, message, allowDead);
        }

        public override bool CheckContextMenuDisplay(IEntity target)
        {
            return (m_DesignContext == null);
        }

        public override void OnItemAdded(Item item)
        {
            base.OnItemAdded(item);

            if (item is BaseArmor || item is BaseWeapon)
            {
                Hits = Hits; Stam = Stam; Mana = Mana;
            }

            if (this.NetState != null)
                CheckLightLevels(false);

            InvalidateMyRunUO();
        }

        public override void OnItemRemoved(Item item)
        {
            base.OnItemRemoved(item);

            if (item is BaseArmor || item is BaseWeapon)
            {
                Hits = Hits; Stam = Stam; Mana = Mana;
            }

            if (this.NetState != null)
                CheckLightLevels(false);

            InvalidateMyRunUO();
        }

        public override double ArmorRating
        {
            get
            {
                //BaseArmor ar;
                double rating = 0.0;

                AddArmorRating(ref rating, NeckArmor);
                AddArmorRating(ref rating, HandArmor);
                AddArmorRating(ref rating, HeadArmor);
                AddArmorRating(ref rating, ArmsArmor);
                AddArmorRating(ref rating, LegsArmor);
                AddArmorRating(ref rating, ChestArmor);
                AddArmorRating(ref rating, ShieldArmor);

                return VirtualArmor + VirtualArmorMod + rating;
            }
        }

        private void AddArmorRating(ref double rating, Item armor)
        {
            BaseArmor ar = armor as BaseArmor;

            if (ar != null && (!Core.AOS || ar.ArmorAttributes.MageArmor == 0))
                rating += ar.ArmorRatingScaled;
        }

        #region [Stats]Max
        [CommandProperty(AccessLevel.GameMaster)]
        public override int HitsMax
        {
            get
            {
                int strBase;
                int strOffs = GetStatOffset(StatType.Str);

                if (Core.AOS)
                {
                    strBase = this.Str;	//this.Str already includes GetStatOffset/str
                    strOffs = AosAttributes.GetValue(this, AosAttribute.BonusHits);

                    if (Core.ML && strOffs > 25 && AccessLevel <= AccessLevel.Player)
                        strOffs = 25;

                    if (AnimalForm.UnderTransformation(this, typeof(BakeKitsune)) || AnimalForm.UnderTransformation(this, typeof(GreyWolf)))
                        strOffs += 20;
                }
                else
                {
                    strBase = this.RawStr;
                }

                return (strBase / 2) + 50 + strOffs;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override int StamMax
        {
            get { return base.StamMax + AosAttributes.GetValue(this, AosAttribute.BonusStam); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override int ManaMax
        {
            get { return base.ManaMax + AosAttributes.GetValue(this, AosAttribute.BonusMana) + ((Core.ML && Race == Race.Elf) ? 20 : 0); }
        }
        #endregion

        #region Stat Getters/Setters

        [CommandProperty(AccessLevel.GameMaster)]
        public override int Str
        {
            get
            {
                if (Core.ML && this.AccessLevel == AccessLevel.Player)
                    return Math.Min(base.Str, 350);

                return base.Str;
            }
            set
            {
                base.Str = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override int Int
        {
            get
            {
                if (Core.ML && this.AccessLevel == AccessLevel.Player)
                    return Math.Min(base.Int, 350);

                return base.Int;
            }
            set
            {
                base.Int = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override int Dex
        {
            get
            {
                if (Core.ML && this.AccessLevel == AccessLevel.Player)
                    return Math.Min(base.Dex, 350);

                return base.Dex;
            }
            set
            {
                base.Dex = value;
            }
        }

        #endregion

        public override bool Move(Direction d)
        {
            NetState ns = this.NetState;

            if (ns != null)
            {
				if (HasGump(typeof(ResurrectGump)))
				{
					if (Alive)
					{
                        CloseGump(typeof(ResurrectGump));
					}
					else
					{
                        SendLocalizedMessage(500111); // You are frozen and cannot move.
                        return false;
                    }
                }
            }

            TimeSpan speed = ComputeMovementSpeed(d);

            bool res;

            if (!Alive)
                Server.Movement.MovementImpl.IgnoreMovableImpassables = true;

            res = base.Move(d);

            Server.Movement.MovementImpl.IgnoreMovableImpassables = false;

            if (!res)
                return false;

            m_NextMovementTime += speed;

            return true;
        }

        public override bool CheckMovement(Direction d, out int newZ)
        {
            DesignContext context = m_DesignContext;

            if (context == null)
                return base.CheckMovement(d, out newZ);

            HouseFoundation foundation = context.Foundation;

            newZ = foundation.Z + HouseFoundation.GetLevelZ(context.Level, context.Foundation);

            int newX = this.X, newY = this.Y;
            Movement.Movement.Offset(d, ref newX, ref newY);

            int startX = foundation.X + foundation.Components.Min.X + 1;
            int startY = foundation.Y + foundation.Components.Min.Y + 1;
            int endX = startX + foundation.Components.Width - 1;
            int endY = startY + foundation.Components.Height - 2;

            return (newX >= startX && newY >= startY && newX < endX && newY < endY && Map == foundation.Map);
        }

        public override bool AllowItemUse(Item item)
        {
			#region Dueling
			if ( m_DuelContext != null && !m_DuelContext.AllowItemUse( this, item ) )
				return false;
			#endregion

            return DesignContext.Check(this);
        }

        public SkillName[] AnimalFormRestrictedSkills { get { return m_AnimalFormRestrictedSkills; } }

        private SkillName[] m_AnimalFormRestrictedSkills = new SkillName[]
		{
			SkillName.ArmsLore,	SkillName.Begging, SkillName.Discordance, SkillName.Forensics,
			SkillName.Inscribe, SkillName.ItemID, SkillName.Meditation, SkillName.Peacemaking,
			SkillName.Provocation, SkillName.RemoveTrap, SkillName.SpiritSpeak, SkillName.Stealing,	
			SkillName.TasteID
		};

        public override bool AllowSkillUse(SkillName skill)
        {
            if (AnimalForm.UnderTransformation(this))
            {
                for (int i = 0; i < m_AnimalFormRestrictedSkills.Length; i++)
                {
                    if (m_AnimalFormRestrictedSkills[i] == skill)
                    {
                        #region Mondain's Legacy
                        AnimalFormContext context = AnimalForm.GetContext(this);

                        if (skill == SkillName.Stealing && context.StealingBonus)
                            continue;
                        #endregion

                        SendLocalizedMessage(1070771); // You cannot use that skill in this form.
                        return false;
                    }
                }
            }

			#region Dueling
			if ( m_DuelContext != null && !m_DuelContext.AllowSkillUse( this, skill ) )
				return false;
			#endregion

            return DesignContext.Check(this);
        }

        private bool m_LastProtectedMessage;
        private int m_NextProtectionCheck = 10;

        public virtual void RecheckTownProtection()
        {
            m_NextProtectionCheck = 10;

            Regions.GuardedRegion reg = (Regions.GuardedRegion)this.Region.GetRegion(typeof(Regions.GuardedRegion));
            bool isProtected = (reg != null && !reg.IsDisabled());

            if (isProtected != m_LastProtectedMessage)
            {
                if (isProtected)
                    SendLocalizedMessage(500112); // You are now under the protection of the town guards.
                else
                    SendLocalizedMessage(500113); // You have left the protection of the town guards.

                m_LastProtectedMessage = isProtected;
            }
        }

        public override void MoveToWorld(Point3D loc, Map map)
        {
            base.MoveToWorld(loc, map);

            RecheckTownProtection();
        }

        public override void SetLocation(Point3D loc, bool isTeleport)
        {
            if (!isTeleport && AccessLevel == AccessLevel.Player)
            {
                // moving, not teleporting
                int zDrop = (this.Location.Z - loc.Z);

                if (zDrop > 20) // we fell more than one story
                    Hits -= ((zDrop / 20) * 10) - 5; // deal some damage; does not kill, disrupt, etc
            }

            base.SetLocation(loc, isTeleport);

            if (isTeleport || --m_NextProtectionCheck == 0)
                RecheckTownProtection();
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            if (from == this)
            {
                if (m_Quest != null)
                    m_Quest.GetContextMenuEntries(list);

                if (Alive && InsuranceEnabled)
                {
                    list.Add(new CallbackEntry(6201, new ContextCallback(ToggleItemInsurance)));

                    if (AutoRenewInsurance)
                        list.Add(new CallbackEntry(6202, new ContextCallback(CancelRenewInventoryInsurance)));
                    else
                        list.Add(new CallbackEntry(6200, new ContextCallback(AutoRenewInventoryInsurance)));
                }

                BaseHouse house = BaseHouse.FindHouseAt(this);

                if (house != null)
                {
                    if (Alive && house.InternalizedVendors.Count > 0 && house.IsOwner(this))
                        list.Add(new CallbackEntry(6204, new ContextCallback(GetVendor)));

					if ( house.IsAosRules && !Region.IsPartOf( typeof( Engines.ConPVP.SafeZone ) ) ) // Dueling
                        list.Add(new CallbackEntry(6207, new ContextCallback(LeaveHouse)));
                }

                if (m_JusticeProtectors.Count > 0)
                    list.Add(new CallbackEntry(6157, new ContextCallback(CancelProtection)));

                if (Alive)
                    list.Add(new CallbackEntry(6210, new ContextCallback(ToggleChampionTitleDisplay)));
				#region SFTotals
                if (Alive)
                    list.Add(new CallbackEntry( 1114262, new ContextCallback(TotalsMenu)));
				#endregion
                #region Mondain's Legacy
                if (Alive)
                {
                    QuestHelper.GetContextMenuEntries(list);

                    if (m_CollectionTitles.Count > 0)
                        list.Add(new CallbackEntry(6229, new ContextCallback(ShowChangeTitle)));
                }
                #endregion
            }
            if (from != this)
            {
                if (Alive && Core.Expansion >= Expansion.AOS)
				{
					Party theirParty = from.Party as Party;
					Party ourParty = this.Party as Party;

					if (theirParty == null && ourParty == null)
					{
                    list.Add(new AddToPartyEntry(from, this));
					}
					else if (theirParty != null && theirParty.Leader == from)
					{
						if (ourParty == null)
						{
							list.Add( new AddToPartyEntry( from, this ) );
						}
						else if (ourParty == theirParty)
						{
							list.Add( new RemoveFromPartyEntry( from, this ) );
						}
					}
				}

                BaseHouse curhouse = BaseHouse.FindHouseAt(this);

                if (curhouse != null)
                {
                    if (Alive && Core.Expansion >= Expansion.AOS && curhouse.IsAosRules && curhouse.IsFriend(from))
                        list.Add(new EjectPlayerEntry(from, this));
                }
            }
        }

        private void CancelProtection()
        {
            for (int i = 0; i < m_JusticeProtectors.Count; ++i)
            {
                Mobile prot = m_JusticeProtectors[i];

                string args = String.Format("{0}\t{1}", this.Name, prot.Name);

                prot.SendLocalizedMessage(1049371, args); // The protective relationship between ~1_PLAYER1~ and ~2_PLAYER2~ has been ended.
                this.SendLocalizedMessage(1049371, args); // The protective relationship between ~1_PLAYER1~ and ~2_PLAYER2~ has been ended.
            }

            m_JusticeProtectors.Clear();
        }
		#region SFTotals
        private void TotalsMenu()
        {
            if (!CheckAlive())
                return;

            this.SendGump( new SFTotalsGump(this) );
        }
		#endregion

        #region Insurance

        private void ToggleItemInsurance()
        {
            if (!CheckAlive())
                return;

            BeginTarget(-1, false, TargetFlags.None, new TargetCallback(ToggleItemInsurance_Callback));
            SendLocalizedMessage(1060868); // Target the item you wish to toggle insurance status on <ESC> to cancel
        }

        private bool CanInsure(Item item)
        {
            #region Mondain's Legacy
            if (item is BaseQuiver && item.LootType == LootType.Regular)
                return true;
            #endregion

			if ( (( item is Container) && !(item is BaseQuiver)) || item is BagOfSending || item is KeyRing )
                return false;

            if ((item is Spellbook && item.LootType == LootType.Blessed) || item is Runebook || item is PotionKeg || item is Sigil)
                return false;

            if (item.Stackable)
                return false;

            if (item.LootType == LootType.Cursed)
                return false;

            if (item.ItemID == 0x204E) // death shroud
                return false;

            return true;
        }

        private void ToggleItemInsurance_Callback(Mobile from, object obj)
        {
            if (!CheckAlive())
                return;

            Item item = obj as Item;

            if (item == null || !item.IsChildOf(this))
            {
                BeginTarget(-1, false, TargetFlags.None, new TargetCallback(ToggleItemInsurance_Callback));
                SendLocalizedMessage(1060871, "", 0x23); // You can only insure items that you have equipped or that are in your backpack
            }
            else if (item.Insured)
            {
                item.Insured = false;

                SendLocalizedMessage(1060874, "", 0x35); // You cancel the insurance on the item

                BeginTarget(-1, false, TargetFlags.None, new TargetCallback(ToggleItemInsurance_Callback));
                SendLocalizedMessage(1060868, "", 0x23); // Target the item you wish to toggle insurance status on <ESC> to cancel
            }
            else if (!CanInsure(item))
            {
                BeginTarget(-1, false, TargetFlags.None, new TargetCallback(ToggleItemInsurance_Callback));
                SendLocalizedMessage(1060869, "", 0x23); // You cannot insure that
            }
            else if (item.LootType == LootType.Blessed || item.LootType == LootType.Newbied || item.BlessedFor == from)
            {
                BeginTarget(-1, false, TargetFlags.None, new TargetCallback(ToggleItemInsurance_Callback));
                SendLocalizedMessage(1060870, "", 0x23); // That item is blessed and does not need to be insured
                SendLocalizedMessage(1060869, "", 0x23); // You cannot insure that
            }
            else
            {
                if (!item.PayedInsurance)
                {
                    if (Banker.Withdraw(from, 600))
                    {
                        SendLocalizedMessage(1060398, "600"); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
                        item.PayedInsurance = true;
                    }
                    else
                    {
                        SendLocalizedMessage(1061079, "", 0x23); // You lack the funds to purchase the insurance
                        return;
                    }
                }

                item.Insured = true;

                SendLocalizedMessage(1060873, "", 0x23); // You have insured the item

                BeginTarget(-1, false, TargetFlags.None, new TargetCallback(ToggleItemInsurance_Callback));
                SendLocalizedMessage(1060868, "", 0x23); // Target the item you wish to toggle insurance status on <ESC> to cancel
            }
        }

        private void AutoRenewInventoryInsurance()
        {
            if (!CheckAlive())
                return;

            SendLocalizedMessage(1060881, "", 0x23); // You have selected to automatically reinsure all insured items upon death
            AutoRenewInsurance = true;
        }

        private void CancelRenewInventoryInsurance()
        {
            if (!CheckAlive())
                return;

            if (Core.SE)
            {
                if (!HasGump(typeof(CancelRenewInventoryInsuranceGump)))
                    SendGump(new CancelRenewInventoryInsuranceGump(this));
            }
            else
            {
                SendLocalizedMessage(1061075, "", 0x23); // You have cancelled automatically reinsuring all insured items upon death
                AutoRenewInsurance = false;
            }
        }

        private class CancelRenewInventoryInsuranceGump : Gump
        {
            private PlayerMobile m_Player;

            public CancelRenewInventoryInsuranceGump(PlayerMobile player)
                : base(250, 200)
            {
                m_Player = player;

                AddBackground(0, 0, 240, 142, 0x13BE);
                AddImageTiled(6, 6, 228, 100, 0xA40);
                AddImageTiled(6, 116, 228, 20, 0xA40);
                AddAlphaRegion(6, 6, 228, 142);

                AddHtmlLocalized(8, 8, 228, 100, 1071021, 0x7FFF, false, false); // You are about to disable inventory insurance auto-renewal.

                AddButton(6, 116, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
                AddHtmlLocalized(40, 118, 450, 20, 1060051, 0x7FFF, false, false); // CANCEL

                AddButton(114, 116, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0);
                AddHtmlLocalized(148, 118, 450, 20, 1071022, 0x7FFF, false, false); // DISABLE IT!
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (!m_Player.CheckAlive())
                    return;

                if (info.ButtonID == 1)
                {
                    m_Player.SendLocalizedMessage(1061075, "", 0x23); // You have cancelled automatically reinsuring all insured items upon death
                    m_Player.AutoRenewInsurance = false;
                }
                else
                {
                    m_Player.SendLocalizedMessage(1042021); // Cancelled.
                }
            }
        }

        #endregion

        private void GetVendor()
        {
            BaseHouse house = BaseHouse.FindHouseAt(this);

            if (CheckAlive() && house != null && house.IsOwner(this) && house.InternalizedVendors.Count > 0)
            {
                CloseGump(typeof(ReclaimVendorGump));
                SendGump(new ReclaimVendorGump(house));
            }
        }

        private void LeaveHouse()
        {
            BaseHouse house = BaseHouse.FindHouseAt(this);

            if (house != null)
                this.Location = house.BanLocation;
        }

        private delegate void ContextCallback();

        private class CallbackEntry : ContextMenuEntry
        {
            private ContextCallback m_Callback;

            public CallbackEntry(int number, ContextCallback callback)
                : this(number, -1, callback)
            {
            }

            public CallbackEntry(int number, int range, ContextCallback callback)
                : base(number, range)
            {
                m_Callback = callback;
            }

            public override void OnClick()
            {
                if (m_Callback != null)
                    m_Callback();
            }
        }

        public override void DisruptiveAction()
        {
            if (Meditating)
            {
                RemoveBuff(BuffIcon.ActiveMeditation);
            }

            base.DisruptiveAction();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (this == from && !Warmode)
            {
                IMount mount = Mount;

                if (mount != null && !DesignContext.Check(this))
                    return;
            }

            base.OnDoubleClick(from);
        }

        public override void DisplayPaperdollTo(Mobile to)
        {
            if (DesignContext.Check(this))
                base.DisplayPaperdollTo(to);
        }

        private static bool m_NoRecursion;

        public override bool CheckEquip(Item item)
        {
            if (!base.CheckEquip(item))
                return false;

			#region Dueling
			if ( m_DuelContext != null && !m_DuelContext.AllowItemEquip( this, item ) )
				return false;
			#endregion

            #region Factions
            FactionItem factionItem = FactionItem.Find(item);

            if (factionItem != null)
            {
                Faction faction = Faction.Find(this);

                if (faction == null)
                {
                    SendLocalizedMessage(1010371); // You cannot equip a faction item!
                    return false;
                }
                else if (faction != factionItem.Faction)
                {
                    SendLocalizedMessage(1010372); // You cannot equip an opposing faction's item!
                    return false;
                }
                else
                {
                    int maxWearables = FactionItem.GetMaxWearables(this);

                    for (int i = 0; i < Items.Count; ++i)
                    {
                        Item equiped = Items[i];

                        if (item != equiped && FactionItem.Find(equiped) != null)
                        {
                            if (--maxWearables == 0)
                            {
                                SendLocalizedMessage(1010373); // You do not have enough rank to equip more faction items!
                                return false;
                            }
                        }
                    }
                }
            }
            #endregion

            if (this.AccessLevel < AccessLevel.GameMaster && item.Layer != Layer.Mount && this.HasTrade)
            {
                BounceInfo bounce = item.GetBounce();

                if (bounce != null)
                {
                    if (bounce.m_Parent is Item)
                    {
                        Item parent = (Item)bounce.m_Parent;

                        if (parent == this.Backpack || parent.IsChildOf(this.Backpack))
                            return true;
                    }
                    else if (bounce.m_Parent == this)
                    {
                        return true;
                    }
                }

                SendLocalizedMessage(1004042); // You can only equip what you are already carrying while you have a trade pending.
                return false;
            }

            return true;
        }

        public override bool CheckTrade(Mobile to, Item item, SecureTradeContainer cont, bool message, bool checkItems, int plusItems, int plusWeight)
        {
            int msgNum = 0;

            if (cont == null)
            {
                if (to.Holding != null)
                    msgNum = 1062727; // You cannot trade with someone who is dragging something.
                else if (this.HasTrade)
                    msgNum = 1062781; // You are already trading with someone else!
                else if (to.HasTrade)
                    msgNum = 1062779; // That person is already involved in a trade
            }

            if (msgNum == 0)
            {
                if (cont != null)
                {
                    plusItems += cont.TotalItems;
                    plusWeight += cont.TotalWeight;
                }

                if (this.Backpack == null || !this.Backpack.CheckHold(this, item, false, checkItems, plusItems, plusWeight))
                    msgNum = 1004040; // You would not be able to hold this if the trade failed.
                else if (to.Backpack == null || !to.Backpack.CheckHold(to, item, false, checkItems, plusItems, plusWeight))
                    msgNum = 1004039; // The recipient of this trade would not be able to carry this.
                else
                    msgNum = CheckContentForTrade(item);
            }

            if (msgNum != 0)
            {
                if (message)
                    this.SendLocalizedMessage(msgNum);

                return false;
            }

            return true;
        }

        private static int CheckContentForTrade(Item item)
        {
            if (item is TrapableContainer && ((TrapableContainer)item).TrapType != TrapType.None)
                return 1004044; // You may not trade trapped items.

            if (SkillHandlers.StolenItem.IsStolen(item))
                return 1004043; // You may not trade recently stolen items.

            if (item is Container)
            {
                foreach (Item subItem in item.Items)
                {
                    int msg = CheckContentForTrade(subItem);

                    if (msg != 0)
                        return msg;
                }
            }

            return 0;
        }

        public override bool CheckNonlocalDrop(Mobile from, Item item, Item target)
        {
            if (!base.CheckNonlocalDrop(from, item, target))
                return false;

            if (from.AccessLevel >= AccessLevel.GameMaster)
                return true;

            Container pack = this.Backpack;
            if (from == this && this.HasTrade && (target == pack || target.IsChildOf(pack)))
            {
                BounceInfo bounce = item.GetBounce();

                if (bounce != null && bounce.m_Parent is Item)
                {
                    Item parent = (Item)bounce.m_Parent;

                    if (parent == pack || parent.IsChildOf(pack))
                        return true;
                }

                SendLocalizedMessage(1004041); // You can't do that while you have a trade pending.
                return false;
            }

            return true;
        }

        protected override void OnLocationChange(Point3D oldLocation)
        {
            CheckLightLevels(false);

			#region Dueling
			if ( m_DuelContext != null )
				m_DuelContext.OnLocationChanged( this );
			#endregion

            DesignContext context = m_DesignContext;

            if (context == null || m_NoRecursion)
                return;

            m_NoRecursion = true;

            HouseFoundation foundation = context.Foundation;

            int newX = this.X, newY = this.Y;
            int newZ = foundation.Z + HouseFoundation.GetLevelZ(context.Level, context.Foundation);

            int startX = foundation.X + foundation.Components.Min.X + 1;
            int startY = foundation.Y + foundation.Components.Min.Y + 1;
            int endX = startX + foundation.Components.Width - 1;
            int endY = startY + foundation.Components.Height - 2;

            if (newX >= startX && newY >= startY && newX < endX && newY < endY && Map == foundation.Map)
            {
                if (Z != newZ)
                    Location = new Point3D(X, Y, newZ);

                m_NoRecursion = false;
                return;
            }

            Location = new Point3D(foundation.X, foundation.Y, newZ);
            Map = foundation.Map;

            m_NoRecursion = false;
        }

        public override bool OnMoveOver(Mobile m)
        {
            if (m is BaseCreature && !((BaseCreature)m).Controlled)
				return ( !Alive || !m.Alive || IsDeadBondedPet || m.IsDeadBondedPet ) || ( Hidden && AccessLevel > AccessLevel.Player );

			#region Dueling
			if ( Region.IsPartOf( typeof( Engines.ConPVP.SafeZone ) ) && m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile) m;

				if ( pm.DuelContext == null || pm.DuelPlayer == null || !pm.DuelContext.Started || pm.DuelContext.Finished || pm.DuelPlayer.Eliminated )
					return true;
			}
			#endregion

            return base.OnMoveOver(m);
        }

        public override bool CheckShove(Mobile shoved)
        {
			if( m_IgnoreMobiles || TransformationSpellHelper.UnderTransformation( shoved, typeof( WraithFormSpell ) ) )
                return true;
            else
                return base.CheckShove(shoved);
        }

        protected override void OnMapChange(Map oldMap)
        {
            if ((Map != Faction.Facet && oldMap == Faction.Facet) || (Map == Faction.Facet && oldMap != Faction.Facet))
                InvalidateProperties();

			#region Dueling
			if ( m_DuelContext != null )
				m_DuelContext.OnMapChanged( this );
			#endregion

            DesignContext context = m_DesignContext;

            if (context == null || m_NoRecursion)
                return;

            m_NoRecursion = true;

            HouseFoundation foundation = context.Foundation;

            if (Map != foundation.Map)
                Map = foundation.Map;

            m_NoRecursion = false;
        }

        public override void OnBeneficialAction(Mobile target, bool isCriminal)
        {
            if (m_SentHonorContext != null)
                m_SentHonorContext.OnSourceBeneficialAction(target);

            base.OnBeneficialAction(target, isCriminal);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
/*GD13_HS_Start_3
			BaseCreature bc = from as BaseCreature;

			if ( bc != null && Hunting)
				//return;
			{
				if ( HuntingMode == HuntMode.Easy && ( bc.IsHard || bc.IsExtreme || bc.IsGod ) )
					Combatant = null;
				if ( HuntingMode == HuntMode.Hard && ( bc.IsEasy || bc.IsExtreme || bc.IsGod ) )
					Combatant = null;
				if ( HuntingMode == HuntMode.Extreme && ( bc.IsEasy || bc.IsHard || bc.IsGod ) )
					Combatant = null;
				if ( HuntingMode == HuntMode.God && ( bc.IsEasy || bc.IsHard || bc.IsExtreme ) )
					Combatant = null;
					
				if ( bc != null && Hunting == true && (!bc.Controlled == true || !bc.IsBonded == true) && !( bc.IsEasy || bc.IsHard || bc.IsExtreme || bc.IsGod ) && bc.Karma <= -200 )
				{
					if ( HuntingMode == HuntMode.Easy && Easy.CheckConvert( bc ))
					{
						bc.IsEasy = true;
						bc.Hits = Hits * 2;
						bc.Tamable = false;
						
						if ( HitsMax * 2 > 65000)
							Hits = 65000;
						else 
							Hits = HitsMax * 2;
					}
					else if ( HuntingMode == HuntMode.Hard && Hard.CheckConvert( bc ))
					{
						bc.IsHard = true;
						bc.Hits = Hits * 3;
						bc.Tamable = false;
						
						if ( HitsMax * 3 > 65000)
							Hits = 65000;
						else 
							Hits = HitsMax * 3;
					}
					else if ( HuntingMode == HuntMode.Extreme && Extreme.CheckConvert( bc ))
					{
						bc.IsExtreme = true;
						bc.Hits = Hits * 4;
						bc.Tamable = false;
						
						if ( HitsMax * 4 > 65000)
							Hits = 65000;
						else 
							Hits = HitsMax * 4;
					}
					else if ( HuntingMode == HuntMode.God && God.CheckConvert( bc ))
					{
						bc.IsGod = true;
						bc.Hits = Hits * 5;
						bc.Tamable =  false;
						
						if ( HitsMax * 5 > 65000)
							Hits = 65000;
						else 
							Hits = HitsMax * 5;
					}
					else
						return;
				}
			}
/*GD13_HS_End_3*/
            int disruptThreshold;

            if (!Core.AOS)
                disruptThreshold = 0;
            else if (from != null && from.Player)
                disruptThreshold = 18;
            else
                disruptThreshold = 25;

            if (amount > disruptThreshold)
            {
                BandageContext c = BandageContext.GetContext(this);

                if (c != null)
                    c.Slip();
            }

            if (Confidence.IsRegenerating(this))
                Confidence.StopRegenerating(this);

            WeightOverloading.FatigueOnDamage(this, amount);

            if (m_ReceivedHonorContext != null)
                m_ReceivedHonorContext.OnTargetDamaged(from, amount);
            if (m_SentHonorContext != null)
                m_SentHonorContext.OnSourceDamaged(from, amount);

            if (willKill && from is PlayerMobile)
                Timer.DelayCall(TimeSpan.FromSeconds(10), new TimerCallback(((PlayerMobile)from).RecoverAmmo));

            #region Mondain's Legacy
            if (InvisibilityPotion.HasTimer(this))
                InvisibilityPotion.Iterrupt(this);
            #endregion

            base.OnDamage(amount, from, willKill);
        }

        public override void Resurrect()
        {
            bool wasAlive = this.Alive;

            base.Resurrect();
//Freyd's Vampire System
            PlayerMobile vampire = this as PlayerMobile;
            if (vampire.VampireBited == 1)
                vampire.VampireBited = 0;
            if (vampire.Vampire == 1 && vampire.VampireBiteTime == TimeSpan.Zero)
                new VampireTimer(vampire, 120.0).Start();
				
            PlayerMobile werewolf = this as PlayerMobile;
            if (vampire.WerewolfBited == 1)
                vampire.WerewolfBited = 0;
            if (vampire.Werewolf == 1 && vampire.WerewolfBiteTime == TimeSpan.Zero)
                new WerewolfTimer(vampire, 120.0).Start();
//Freyd's Vampire System


            if (this.Alive && !wasAlive)
            {
				// Staff now res to full health
			if ( this.AccessLevel > AccessLevel.Player )
			{
				//BodyValue = 987; // Lets them walk though doors instead of having to open them.
				Hits = HitsMax;
				Mana = ManaMax;
				Stam = StamMax;
			}

				#region Scroll of Alacrity
				if ( this.AcceleratedStart > DateTime.Now )
				{
					BuffInfo.AddBuff( this, new BuffInfo( BuffIcon.ArcaneEmpowerment, 1078511, 1078512, this.AcceleratedSkill.ToString() ) );
				}
				#endregion
            }
        }

        public override double RacialSkillBonus
        {
            get
            {
                if (Core.ML && this.Race == Race.Human)
                    return 20.0;

                return 0;
            }
        }

        public override void OnWarmodeChanged()
        {
            if (!Warmode)
                Timer.DelayCall(TimeSpan.FromSeconds(10), new TimerCallback(RecoverAmmo));
        }

        private Mobile m_InsuranceAward;
        private int m_InsuranceCost;
        private int m_InsuranceBonus;

		private List<Item> m_EquipSnapshot;

		public List<Item> EquipSnapshot
		{
			get { return m_EquipSnapshot; }
		}

		private bool FindItems_Callback(Item item)
		{
			if (!item.Deleted && (item.LootType == LootType.Blessed || item.Insured == true))
			{
				if (this.Backpack != item.ParentEntity)
				{
					return true;
				}
			}
			return false;
		}

        public override bool OnBeforeDeath()
        {
			NetState state = NetState;

			if (state != null)
				state.CancelAllTrades();

			DropHolding();

			if (Backpack != null && !Backpack.Deleted)
			{
				List<Item> ilist = Backpack.FindItemsByType<Item>(FindItems_Callback);

				for (int i = 0; i < ilist.Count; i++)
				{
					Backpack.AddItem(ilist[i]);
				}
			}

			m_EquipSnapshot = new List<Item>( this.Items );

 	 	 	m_NonAutoreinsuredItems = 0;
            m_InsuranceCost = 0;
            m_InsuranceAward = base.FindMostRecentDamager(false);

            if (m_InsuranceAward is BaseCreature)
            {
                Mobile master = ((BaseCreature)m_InsuranceAward).GetMaster();

                if (master != null)
                    m_InsuranceAward = master;
            }

            if (m_InsuranceAward != null && (!m_InsuranceAward.Player || m_InsuranceAward == this))
                m_InsuranceAward = null;

            if (m_InsuranceAward is PlayerMobile)
                ((PlayerMobile)m_InsuranceAward).m_InsuranceBonus = 0;

            if (m_ReceivedHonorContext != null)
                m_ReceivedHonorContext.OnTargetKilled();
            if (m_SentHonorContext != null)
                m_SentHonorContext.OnSourceKilled();

            RecoverAmmo();

            return base.OnBeforeDeath();
        }
		
		public override bool RetainPackLocsOnDeath { get { return true; } }
		
        private bool CheckInsuranceOnDeath(Item item)
        {
            if (InsuranceEnabled && item.Insured)
            {
				#region Dueling
				if ( m_DuelPlayer != null && m_DuelContext != null && m_DuelContext.Registered && m_DuelContext.Started && !m_DuelPlayer.Eliminated )
					return true;
				#endregion
                // XmlPoints mod to support overriding insurance fees/awards during challenge games
                if(XmlPoints.InsuranceIsFree(this, m_InsuranceAward))
                {
                    item.PayedInsurance = true;
                    return true;
                }
				
                if (AutoRenewInsurance)
                {
                    int cost = (m_InsuranceAward == null ? 600 : 300);

                    if (Banker.Withdraw(this, cost))
                    {
                        m_InsuranceCost += cost;
                        item.PayedInsurance = true;
 	 	 	 	 	 	SendLocalizedMessage(1060398, cost.ToString()); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
                    }
                    else
                    {
                        SendLocalizedMessage(1061079, "", 0x23); // You lack the funds to purchase the insurance
                        item.PayedInsurance = false;
                        item.Insured = false;
 	 	 	 	 	 	m_NonAutoreinsuredItems++;
                    }
                }
                else
                {
                    item.PayedInsurance = false;
                    item.Insured = false;
                }

                if (m_InsuranceAward != null)
                {
                    if (Banker.Deposit(m_InsuranceAward, 300))
                    {
                        if (m_InsuranceAward is PlayerMobile)
                            ((PlayerMobile)m_InsuranceAward).m_InsuranceBonus += 300;
                    }
                }

                return true;
            }

            return false;
        }

        public override DeathMoveResult GetParentMoveResultFor(Item item)
        {
            if (!this.Young) 
                if (CheckInsuranceOnDeath(item)) 
                    return DeathMoveResult.RemainEquiped;

            DeathMoveResult res = base.GetParentMoveResultFor(item); 
            { 

                if (res == DeathMoveResult.MoveToCorpse && item.Movable && this.Young) 
                    res = DeathMoveResult.RemainEquiped; 

                if (res == DeathMoveResult.MoveToBackpack && !this.Young) 
                    res = DeathMoveResult.RemainEquiped; 

                return res; 
            }
        }

        public override DeathMoveResult GetInventoryMoveResultFor(Item item) 
        { 
            if (!this.Young) 
                if (CheckInsuranceOnDeath(item)) 
                    return DeathMoveResult.MoveToBackpack; 

            DeathMoveResult res = base.GetInventoryMoveResultFor(item); 

            if (res == DeathMoveResult.MoveToCorpse && item.Movable && this.Young) 
                res = DeathMoveResult.MoveToBackpack; 

            if (res == DeathMoveResult.MoveToBackpack && !this.Young) 
                res = DeathMoveResult.RemainEquiped; 


            return res; 
        }

        public override void OnDeath(Container c)
        {
 	 	 	if (m_NonAutoreinsuredItems > 0)
 	 	 	{
				SendLocalizedMessage(1061115);
 	 	 	}

            base.OnDeath(c);
			m_EquipSnapshot = null;
//-----------------------------------------------LIFE STONE SYSTEM PART 2----------------------------------------------------------------------------
				if( Bound )
				SendGump(new LifeStoneReturnGump( this ) );
//-----------------------------------------------LIFE STONE SYSTEM PART 2----------------------------------------------------------------------------

            HueMod = -1;
            NameMod = null;
            SavagePaintExpiration = TimeSpan.Zero;

            SetHairMods(-1, -1);

            PolymorphSpell.StopTimer(this);
            IncognitoSpell.StopTimer(this);
            DisguiseTimers.RemoveTimer(this);

            EndAction(typeof(PolymorphSpell));
            EndAction(typeof(IncognitoSpell));

            MeerMage.StopEffect(this, false);
            
            #region SA
            if ( Flying == true )
            {
            	Flying = false;
                BuffInfo.RemoveBuff( this, BuffIcon.Fly );
            }
            #endregion
            
            SkillHandlers.StolenItem.ReturnOnDeath(this, c);

            if (m_PermaFlags.Count > 0)
            {
                m_PermaFlags.Clear();

                if (c is Corpse)
                    ((Corpse)c).Criminal = true;

                if (SkillHandlers.Stealing.ClassicMode)
                    Criminal = true;
            }

            if (this.Kills >= 5 && DateTime.Now >= m_NextJustAward)
            {
                Mobile m = FindMostRecentDamager(false);

                if (m is BaseCreature)
                    m = ((BaseCreature)m).GetMaster();

                if (m != null && m is PlayerMobile && m != this)
                {
                    bool gainedPath = false;

                    int pointsToGain = 0;

                    pointsToGain += (int)Math.Sqrt(this.GameTime.TotalSeconds * 4);
                    pointsToGain *= 5;
                    pointsToGain += (int)Math.Pow(this.Skills.Total / 250, 2);

                    if (VirtueHelper.Award(m, VirtueName.Justice, pointsToGain, ref gainedPath))
                    {
                        if (gainedPath)
                            m.SendLocalizedMessage(1049367); // You have gained a path in Justice!
                        else
                            m.SendLocalizedMessage(1049363); // You have gained in Justice.

                        m.FixedParticles(0x375A, 9, 20, 5027, EffectLayer.Waist);
                        m.PlaySound(0x1F7);

                        m_NextJustAward = DateTime.Now + TimeSpan.FromMinutes(pointsToGain / 3);
                    }
                }
            }

            if (m_InsuranceCost > 0)
                SendLocalizedMessage(1060398, m_InsuranceCost.ToString()); // ~1_AMOUNT~ gold has been withdrawn from your bank box.

            if (m_InsuranceAward is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)m_InsuranceAward;

                if (pm.m_InsuranceBonus > 0)
                    pm.SendLocalizedMessage(1060397, pm.m_InsuranceBonus.ToString()); // ~1_AMOUNT~ gold has been deposited into your bank box.
            }

            Mobile killer = this.FindMostRecentDamager(true);
//Freyd's Vampire System
		if (killer is PlayerMobile)
		{
			PlayerMobile pm = (PlayerMobile) killer;
			if (pm.Vampire < 1 && pm.Werewolf < 1)
			{
            	if (this.VampireBited == 1)
            		this.VampireBited = 0;
			}
		}
		if (killer is BaseVampire)
		{
			BaseVampire vamp = (BaseVampire) killer;
			vamp.Rank += 1;
		}
		if (killer is PlayerMobile)
		{
			PlayerMobile pm = (PlayerMobile) killer;
			if (pm.Werewolf < 1 && pm.Vampire < 1)
			{
            	if (this.WerewolfBited == 1)
            		this.WerewolfBited = 0;
			}
		}
		if (killer is BaseWerewolf)
		{
			BaseWerewolf wolf = (BaseWerewolf) killer;
			wolf.Rank += 1;
		}
//Freyd's Vampire System
            #region QueensLoyaltySystem              // Queens Loyalty System

            if (killer is PlayerMobile)
            {
                m_Exp -= (long)(m_LevelExp / 100);
                if (m_Exp < 0)
                {
                    if (m_Level == 0) m_Exp = 0;
                    else
                    {
                        m_Exp += (long)(m_LevelExp / 1.4);
                        m_Level -= 1;
                        this.SendMessage("Due to your death you have lost a level of Loyalty to the Queen");
                    }
                }
            }
            else
            {
                m_Exp -= (long)(m_LevelExp / 50);
                if (m_Exp < 0)
                {
                    if (m_Level == 0) this.Exp = 0;
                    else
                    {
                        m_Exp += (long)(m_LevelExp / 1.4);
                        m_Level -= 1;
                        this.SendMessage("Due to your death you have lost a level of Loyalty to the Queen");
                    }
                }
            }

            #endregion                          // End Queen's Loyalty System


            if (killer is BaseCreature)
            {
                BaseCreature bc = (BaseCreature)killer;

                Mobile master = bc.GetMaster();
                if (master != null)
                    killer = master;
            }

			if ( this.Young && m_DuelContext == null )
            {
                if (YoungDeathTeleport())
                    Timer.DelayCall(TimeSpan.FromSeconds(2.5), new TimerCallback(SendYoungDeathNotice));
            }
            
			if ( m_DuelContext == null || !m_DuelContext.Registered || !m_DuelContext.Started || m_DuelPlayer == null || m_DuelPlayer.Eliminated )
            // block faction skill loss during challenge games
			if(!XmlPoints.AreChallengers(this, killer))
				Faction.HandleDeath( this, killer );

            Server.Guilds.Guild.HandleDeath(this, killer);

			#region Dueling
			if ( m_DuelContext != null )
				m_DuelContext.OnDeath( this, c );
			#endregion

            if (m_BuffTable != null)
            {
                List<BuffInfo> list = new List<BuffInfo>();

                foreach (BuffInfo buff in m_BuffTable.Values)
                {
                    if (!buff.RetainThroughDeath)
                    {
                        list.Add(buff);
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    RemoveBuff(list[i]);
                }
            }

            #region SA
            if ( Region.IsPartOf("Abyss") && SSSeedExpire > DateTime.Now )
                SendGump( new ResurrectGump( this, ResurrectMessage.SilverSapling ) );
            #endregion
        }

        private List<Mobile> m_PermaFlags;
        private List<Mobile> m_VisList;
        private Hashtable m_AntiMacroTable;
        private TimeSpan m_GameTime;
        private TimeSpan m_ShortTermElapse;
        private TimeSpan m_LongTermElapse;
        private DateTime m_SessionStart;
        private DateTime m_LastEscortTime;
        private DateTime m_LastPetBallTime;
        private DateTime m_NextSmithBulkOrder;
        private DateTime m_NextTailorBulkOrder;

        #region SF Imbuing
        public object ImbLast_Item;
        public int ImbLast_Iref;
        public int ImbLast_Mod;
        public int ImbLast_ModInt;
        public int Imb_SFBonus;
        #endregion

        private DateTime m_SavagePaintExpiration;
        private SkillName m_Learning = (SkillName)(-1);

        public SkillName Learning
        {
            get { return m_Learning; }
            set { m_Learning = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan SavagePaintExpiration
        {
            get
            {
                TimeSpan ts = m_SavagePaintExpiration - DateTime.Now;

                if (ts < TimeSpan.Zero)
                    ts = TimeSpan.Zero;

                return ts;
            }
            set
            {
                m_SavagePaintExpiration = DateTime.Now + value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextSmithBulkOrder
        {
            get
            {
                TimeSpan ts = m_NextSmithBulkOrder - DateTime.Now;

                if (ts < TimeSpan.Zero)
                    ts = TimeSpan.Zero;

                return ts;
            }
            set
            {
                try { m_NextSmithBulkOrder = DateTime.Now + value; }
                catch { }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextTailorBulkOrder
        {
            get
            {
                TimeSpan ts = m_NextTailorBulkOrder - DateTime.Now;

                if (ts < TimeSpan.Zero)
                    ts = TimeSpan.Zero;

                return ts;
            }
            set
            {
                try { m_NextTailorBulkOrder = DateTime.Now + value; }
                catch { }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastEscortTime
        {
            get { return m_LastEscortTime; }
            set { m_LastEscortTime = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastPetBallTime
        {
            get { return m_LastPetBallTime; }
            set { m_LastPetBallTime = value; }
        }

        public PlayerMobile()
        {
            m_AutoStabled = new List<Mobile>();

            #region Mondain's Legacy
            m_Quests = new List<BaseQuest>();
            m_Chains = new Dictionary<QuestChain, BaseChain>();
            m_DoneQuests = new List<QuestRestartInfo>();
            m_Collections = new Dictionary<Collection, int>();
            m_CollectionTitles = new List<object>();

            m_Peaced = DateTime.Now;
            #endregion

            m_VisList = new List<Mobile>();
            m_PermaFlags = new List<Mobile>();
            m_AntiMacroTable = new Hashtable();
			m_RecentlyReported = new List<Mobile>();

            m_BOBFilter = new Engines.BulkOrders.BOBFilter();
			/*
			#region Custom BODs
			m_FletcherBOBFilter = new Engines.BulkOrders.FletcherBOBFilter();
			m_CarpenterBOBFilter = new Engines.BulkOrders.CarpenterBOBFilter();
			#endregion
			*/
			#region FS:ATS Edits
			m_TamingBOBFilter = new Engines.BulkOrders.TamingBOBFilter();
			#endregion

            m_GameTime = TimeSpan.Zero;
            m_ShortTermElapse = TimeSpan.FromHours(8.0);
            m_LongTermElapse = TimeSpan.FromHours(40.0);

            m_JusticeProtectors = new List<Mobile>();
            m_GuildRank = Guilds.RankDefinition.Lowest;

            m_ChampionTitles = new ChampionTitleInfo();

            InvalidateMyRunUO();
        }

        public override bool MutateSpeech(List<Mobile> hears, ref string text, ref object context)
        {
            if (Alive)
                return false;

            if (Core.ML && Skills[SkillName.SpiritSpeak].Value >= 100.0)
                return false;

            if (Core.AOS)
            {
                for (int i = 0; i < hears.Count; ++i)
                {
                    Mobile m = hears[i];

                    if (m != this && m.Skills[SkillName.SpiritSpeak].Value >= 100.0)
                        return false;
                }
            }

            return base.MutateSpeech(hears, ref text, ref context);
        }

        public override void DoSpeech(string text, int[] keywords, MessageType type, int hue)
        {
            if (Guilds.Guild.NewGuildSystem && (type == MessageType.Guild || type == MessageType.Alliance))
            {
                Guilds.Guild g = this.Guild as Guilds.Guild;
                if (g == null)
                {
                    SendLocalizedMessage(1063142); // You are not in a guild!
                }
                else if (type == MessageType.Alliance)
                {
                    if (g.Alliance != null && g.Alliance.IsMember(g))
                    {
                        //g.Alliance.AllianceTextMessage( hue, "[Alliance][{0}]: {1}", this.Name, text );
                        g.Alliance.AllianceChat(this, text);
                        SendToStaffMessage(this, "[Alliance]: {0}", text);

                        m_AllianceMessageHue = hue;
                    }
                    else
                    {
                        SendLocalizedMessage(1071020); // You are not in an alliance!
                    }
                }
                else	//Type == MessageType.Guild
                {
                    m_GuildMessageHue = hue;

                    g.GuildChat(this, text);
                    SendToStaffMessage(this, "[Guild]: {0}", text);
                }
            }
            else
            {
                base.DoSpeech(text, keywords, type, hue);
            }
        }

        private static void SendToStaffMessage(Mobile from, string text)
        {
            Packet p = null;

            foreach (NetState ns in from.GetClientsInRange(8))
            {
                Mobile mob = ns.Mobile;

                if (mob != null && mob.AccessLevel >= AccessLevel.GameMaster && mob.AccessLevel > from.AccessLevel)
                {
                    if (p == null)
                        p = Packet.Acquire(new UnicodeMessage(from.Serial, from.Body, MessageType.Regular, from.SpeechHue, 3, from.Language, from.Name, text));

                    ns.Send(p);
                }
            }

            Packet.Release(p);
        }

        private static void SendToStaffMessage(Mobile from, string format, params object[] args)
        {
            SendToStaffMessage(from, String.Format(format, args));
        }

        public override void Damage(int amount, Mobile from)
        {
/*GD13_HS_Start_4
			BaseCreature bc = from as BaseCreature;

			if ( this == null )
				return;

			if ( bc != null && Hunting )
			{

				if (( bc.IsEasy || bc.IsHard || bc.IsExtreme || bc.IsGod ) && bc.Combatant != this )
				{
					amount = (int)( amount -= amount );
					bc.Damage( amount, bc );
				}
			}
/*GD13_HS_End_4*/
			if (Spells.Necromancy.EvilOmenSpell.TryEndEffect(this))
                amount = (int)(amount * 1.25);

            Mobile oath = Spells.Necromancy.BloodOathSpell.GetBloodOath(from);

            /* Per EA's UO Herald Pub48 (ML): 
             * ((resist spellsx10)/20 + 10=percentage of damage resisted)
             */

            if (oath == this)
            {
                amount = (int)(amount * 1.1);

                if (amount > 35 && from is PlayerMobile)  /* capped @ 35, seems no expansion */
                {
                    amount = 35;
                }

                if (Core.ML)
                {
                    from.Damage((int)(amount * (1 - (((from.Skills.MagicResist.Value * .5) + 10) / 100))), this);
                }
                else
                {
                    from.Damage(amount, this);
            }
            }

            if (from != null && Talisman is BaseTalisman)
            {
                BaseTalisman talisman = (BaseTalisman)Talisman;

                if (talisman.Protection != null && talisman.Protection.Type != null)
                {
                    Type type = talisman.Protection.Type;

                    if (type == from.GetType())
                        amount *= 1 - (int)(((double)talisman.Protection.Amount) / 100);
                }
            }

            base.Damage(amount, from);
        }

        #region Poison

        public override ApplyPoisonResult ApplyPoison(Mobile from, Poison poison)
        {
            if (!Alive)
                return ApplyPoisonResult.Immune;

			if (Spells.Necromancy.EvilOmenSpell.TryEndEffect(this))
                poison = PoisonImpl.IncreaseLevel(poison);

            ApplyPoisonResult result = base.ApplyPoison(from, poison);

            if (from != null && result == ApplyPoisonResult.Poisoned && PoisonTimer is PoisonImpl.PoisonTimer)
                (PoisonTimer as PoisonImpl.PoisonTimer).From = from;

            return result;
        }

        public override bool CheckPoisonImmunity(Mobile from, Poison poison)
        {
			if ( this.Young && (DuelContext == null || !DuelContext.Started || DuelContext.Finished) )
                return true;

            return base.CheckPoisonImmunity(from, poison);
        }

        public override void OnPoisonImmunity(Mobile from, Poison poison)
        {
			if ( this.Young && (DuelContext == null || !DuelContext.Started || DuelContext.Finished) )
                SendLocalizedMessage(502808); // You would have been poisoned, were you not new to the land of Britannia. Be careful in the future.
            else
                base.OnPoisonImmunity(from, poison);
        }

        #endregion

        public PlayerMobile(Serial s)
            : base(s)
        {
            m_VisList = new List<Mobile>();
            m_AntiMacroTable = new Hashtable();
            InvalidateMyRunUO();
        }

        public List<Mobile> VisibilityList
        {
            get { return m_VisList; }
        }

        public List<Mobile> PermaFlags
        {
            get { return m_PermaFlags; }
        }

        public override int Luck { get { return AosAttributes.GetValue(this, AosAttribute.Luck); } }

        public override bool IsHarmfulCriminal(Mobile target)
        {
            if (SkillHandlers.Stealing.ClassicMode && target is PlayerMobile && ((PlayerMobile)target).m_PermaFlags.Count > 0)
            {
                int noto = Notoriety.Compute(this, target);

                if (noto == Notoriety.Innocent)
                    target.Delta(MobileDelta.Noto);

                return false;
            }

            if (target is BaseCreature && ((BaseCreature)target).InitialInnocent && !((BaseCreature)target).Controlled)
                return false;

            if (Core.ML && target is BaseCreature && ((BaseCreature)target).Controlled && this == ((BaseCreature)target).ControlMaster)
                return false;

            return base.IsHarmfulCriminal(target);
        }

        public bool AntiMacroCheck(Skill skill, object obj)
        {
            if (obj == null || m_AntiMacroTable == null || this.AccessLevel != AccessLevel.Player)
                return true;

            Hashtable tbl = (Hashtable)m_AntiMacroTable[skill];
            if (tbl == null)
                m_AntiMacroTable[skill] = tbl = new Hashtable();

            CountAndTimeStamp count = (CountAndTimeStamp)tbl[obj];
            if (count != null)
            {
                if (count.TimeStamp + SkillCheck.AntiMacroExpire <= DateTime.Now)
                {
                    count.Count = 1;
                    return true;
                }
                else
                {
                    ++count.Count;
                    if (count.Count <= SkillCheck.Allowance)
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                tbl[obj] = count = new CountAndTimeStamp();
                count.Count = 1;

                return true;
            }
        }

        private void RevertHair()
        {
            SetHairMods(-1, -1);
        }

        private Engines.BulkOrders.BOBFilter m_BOBFilter;
		/*
		#region Custom BODs
		private Engines.BulkOrders.FletcherBOBFilter m_FletcherBOBFilter;
		private Engines.BulkOrders.CarpenterBOBFilter m_CarpenterBOBFilter;
		
		public Engines.BulkOrders.FletcherBOBFilter FletcherBOBFilter
		{
			get{ return m_FletcherBOBFilter; }
		}
		public Engines.BulkOrders.CarpenterBOBFilter CarpenterBOBFilter
		{
			get{ return m_CarpenterBOBFilter; }
		}
		#endregion
		*/

		#region FS:ATS Edits
		private Engines.BulkOrders.TamingBOBFilter m_TamingBOBFilter;
		#endregion

        public Engines.BulkOrders.BOBFilter BOBFilter
        {
            get { return m_BOBFilter; }
        }

		#region FS:ATS Edits
		public Engines.BulkOrders.TamingBOBFilter TamingBOBFilter
		{
			get{ return m_TamingBOBFilter; }
		}
		#endregion

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
				#region Custom BODs
				case 41:
				{
					NextFletcherBulkOrder = reader.ReadTimeSpan();
					NextCarpenterBulkOrder = reader.ReadTimeSpan();
					goto case 40;
				}
				#endregion
				#region Player Government
				case 40:
				{
					m_City = (CityManagementStone)reader.ReadItem();
					m_CityTitle = reader.ReadString();
					m_ShowCityTitle = reader.ReadBool();
					m_OwesBackTaxes = reader.ReadBool();
					m_BackTaxesAmount = reader.ReadInt();
					goto case 39;
				}
				#endregion
				
                #region Guantlet Points
                case 39:
                    {
                        m_GauntletPoints = reader.ReadDouble();

                        goto case 38;
                    } 
                #endregion
                case 38:
                #region SA
                {
                    m_SSNextSeed = reader.ReadDateTime();
                    m_SSSeedExpire = reader.ReadDateTime();
                    m_SSSeedLocation = reader.ReadPoint3D();
                    m_SSSeedMap = reader.ReadMap();
                    goto case 37;
                }
                #endregion

                #region QueensLoyaltySystem   // Queens Loyalty System
                case 37:
                {
                    m_LevelExp = reader.ReadLong();
                    m_Exp = reader.ReadLong();
                    m_Level = reader.ReadInt();

                    m_ExpTitle = reader.ReadString();
                    goto case 36;
                }
                #endregion
				case 36:
				{
					LifeStone = (Point3D)reader.ReadPoint3D();
					Bound = (bool)reader.ReadBool();
					LifeStoneMap = (Map)reader.ReadMap();
					goto case 35;
				}
				case 35:
				{
					m_TamingBOBFilter = new Engines.BulkOrders.TamingBOBFilter( reader );
					goto case 34;
				}
				case 34:
				{
					m_Bioenginer = reader.ReadBool();
					NextTamingBulkOrder = reader.ReadTimeSpan();
					goto case 33;
				}
				case 33:
				{
                    m_Werewolves = reader.ReadInt();                    
                    m_WerewolfBited = reader.ReadInt();                    
                    WerewolfBiteTime = reader.ReadTimeSpan();
					
					goto case 32;
				}
/*GD13_HS_Start_5
				case 33:
				{
					m_HuntMode = (HuntMode)reader.ReadInt();
					m_HuntRank = (HuntRank)reader.ReadInt();
					m_HuntReward = (HuntReward)reader.ReadInt();
					m_Hunting = (bool)reader.ReadBool();
					m_EasyKills = reader.ReadInt();
					m_HardKills = reader.ReadInt();
					m_ExtremeKills = reader.ReadInt();
					m_GodKills = reader.ReadInt();
		
					goto case 32;
				}	
/*GD13_HS_End_5*/
				case 32:
				{
 //Freyd's Vampire System
                    m_Vampire = reader.ReadInt();                    
                    m_VampireBited = reader.ReadInt();                    
                    VampireBiteTime = reader.ReadTimeSpan();
 //Freyd's Vampire System
					goto case 31;
				}

                #region Shadows Edge - Automated Server Staff

                case 31:
                    {
                        m_PromoGiftLast = reader.ReadDateTime();
                        goto case 30;
                    }
                              
                case 30:
                    {
                        m_LastTimePaged = reader.ReadDateTime();
                        goto case 29;
                    }

                #endregion Edited By: A.A.R

                case 29:
                    {
                        m_VASTotalMonsterFame = reader.ReadInt();
                        goto case 28;
                    }
                case 28:
                    {
                        m_AutoStabled = reader.ReadStrongMobileList();
						
                        goto case 27;
                    }
                case 27:
                    {
                        m_AnkhNextUse = reader.ReadDateTime();

                        goto case 26;
                    }
                case 26:
                    {
                        #region Mondain's Legacy
                        m_Quests = QuestReader.Quests(reader, this);
                        m_Chains = QuestReader.Chains(reader);

                        m_Collections = new Dictionary<Collection, int>();
                        m_CollectionTitles = new List<object>();

                        for (int i = reader.ReadInt(); i > 0; i--)
                            m_Collections.Add((Collection)reader.ReadInt(), reader.ReadInt());

                        for (int i = reader.ReadInt(); i > 0; i--)
                            m_CollectionTitles.Add(QuestReader.Object(reader));

                        m_SelectedTitle = reader.ReadInt();
                        m_Peaced = reader.ReadDateTime();
                        #endregion

                        goto case 25;
                    }
                case 25:
                    {
                        int recipeCount = reader.ReadInt();

                        if (recipeCount > 0)
                        {
                            m_AcquiredRecipes = new Dictionary<int, bool>();

                            for (int i = 0; i < recipeCount; i++)
                            {
                                int r = reader.ReadInt();
                                if (reader.ReadBool())	//Don't add in recipies which we haven't gotten or have been removed
                                    m_AcquiredRecipes.Add(r, true);
                            }
                        }
                        goto case 24;
                    }
                case 24:
                    {
                        m_LastHonorLoss = reader.ReadDeltaTime();
                        goto case 23;
                    }
                case 23:
                    {
                        m_ChampionTitles = new ChampionTitleInfo(reader);
                        goto case 22;
                    }
                case 22:
                    {
                        m_LastValorLoss = reader.ReadDateTime();
                        goto case 21;
                    }
                case 21:
                    {
                        m_ToTItemsTurnedIn = reader.ReadEncodedInt();
                        m_ToTTotalMonsterFame = reader.ReadInt();
                        goto case 20;
                    }
                case 20:
                    {
                        m_AllianceMessageHue = reader.ReadEncodedInt();
                        m_GuildMessageHue = reader.ReadEncodedInt();

                        goto case 19;
                    }
                case 19:
                    {
                        int rank = reader.ReadEncodedInt();
                        int maxRank = Guilds.RankDefinition.Ranks.Length - 1;
                        if (rank > maxRank)
                            rank = maxRank;

                        m_GuildRank = Guilds.RankDefinition.Ranks[rank];
                        m_LastOnline = reader.ReadDateTime();
                        goto case 18;
                    }
                case 18:
                    {
                        m_SolenFriendship = (SolenFriendship)reader.ReadEncodedInt();

                        goto case 17;
                    }
                case 17: // changed how DoneQuests is serialized
                case 16:
                    {
                        m_Quest = QuestSerializer.DeserializeQuest(reader);

                        if (m_Quest != null)
                            m_Quest.From = this;

                        int count = reader.ReadEncodedInt();

                        if (count > 0)
                        {
                            m_DoneQuests = new List<QuestRestartInfo>();

                            for (int i = 0; i < count; ++i)
                            {
                                Type questType = QuestSerializer.ReadType(QuestSystem.QuestTypes, reader);
                                DateTime restartTime;

                                if (version < 17)
                                    restartTime = DateTime.MaxValue;
                                else
                                    restartTime = reader.ReadDateTime();

                                m_DoneQuests.Add(new QuestRestartInfo(questType, restartTime));
                            }
                        }

                        m_Profession = reader.ReadEncodedInt();
                        goto case 15;
                    }
                case 15:
                    {
                        m_LastCompassionLoss = reader.ReadDeltaTime();
                        goto case 14;
                    }
                case 14:
                    {
                        m_CompassionGains = reader.ReadEncodedInt();

                        if (m_CompassionGains > 0)
                            m_NextCompassionDay = reader.ReadDeltaTime();

                        goto case 13;
                    }
                case 13: // just removed m_PayedInsurance list
                case 12:
                    {
                        m_BOBFilter = new Engines.BulkOrders.BOBFilter(reader);
                        goto case 11;
                    }
                case 11:
                    {
                        if (version < 13)
                        {
                            List<Item> payed = reader.ReadStrongItemList();

                            for (int i = 0; i < payed.Count; ++i)
                                payed[i].PayedInsurance = true;
                        }

                        goto case 10;
                    }
                case 10:
                    {
                        if (reader.ReadBool())
                        {
                            m_HairModID = reader.ReadInt();
                            m_HairModHue = reader.ReadInt();
                            m_BeardModID = reader.ReadInt();
                            m_BeardModHue = reader.ReadInt();
                        }

                        goto case 9;
                    }
                case 9:
                    {
                        SavagePaintExpiration = reader.ReadTimeSpan();

                        if (SavagePaintExpiration > TimeSpan.Zero)
                        {
                            BodyMod = (Female ? 184 : 183);
                            HueMod = 0;
                        }

                        goto case 8;
                    }
                case 8:
                    {
                        m_NpcGuild = (NpcGuild)reader.ReadInt();
                        m_NpcGuildJoinTime = reader.ReadDateTime();
                        m_NpcGuildGameTime = reader.ReadTimeSpan();
                        goto case 7;
                    }
                case 7:
                    {
                        m_PermaFlags = reader.ReadStrongMobileList();
                        goto case 6;
                    }
                case 6:
                    {
                        NextTailorBulkOrder = reader.ReadTimeSpan();
                        goto case 5;
                    }
                case 5:
                    {
                        NextSmithBulkOrder = reader.ReadTimeSpan();
                        goto case 4;
                    }
                case 4:
                    {
                        m_LastJusticeLoss = reader.ReadDeltaTime();
                        m_JusticeProtectors = reader.ReadStrongMobileList();
                        goto case 3;
                    }
                case 3:
                    {
                        m_LastSacrificeGain = reader.ReadDeltaTime();
                        m_LastSacrificeLoss = reader.ReadDeltaTime();
                        m_AvailableResurrects = reader.ReadInt();
                        goto case 2;
                    }
                case 2:
                    {
                        m_Flags = (PlayerFlag)reader.ReadInt();
                        goto case 1;
                    }
                case 1:
                    {
                        m_LongTermElapse = reader.ReadTimeSpan();
                        m_ShortTermElapse = reader.ReadTimeSpan();
                        m_GameTime = reader.ReadTimeSpan();
                        goto case 0;
                    }
                case 0:
                    {
                        if (version < 28)
                            m_AutoStabled = new List<Mobile>();
                    #region SA
					if( version < 37 )
                    {
                        m_SSNextSeed =  m_SSSeedExpire = DateTime.Now;
                        m_SSSeedLocation = Point3D.Zero;
                    }
                    #endregion
                        break;
                    }
            }

			if (m_RecentlyReported == null)
				m_RecentlyReported = new List<Mobile>();

            #region QueensLoyaltySystem      // Queens Loyalty System

            if (version < 31)
            {
                m_LevelExp = 1000;
                m_Exp = -1000;
                m_Level = 0;

                m_ExpTitle = "TerMur-guest";
            }

            #endregion                  // End of Queens Loyalty System
            #region Mondain's Legacy
            if (m_Quests == null)
                m_Quests = new List<BaseQuest>();

            if (m_Chains == null)
                m_Chains = new Dictionary<QuestChain, BaseChain>();

            if (m_DoneQuests == null)
                m_DoneQuests = new List<QuestRestartInfo>();

            if (m_Collections == null)
                m_Collections = new Dictionary<Collection, int>();

            if (m_CollectionTitles == null)
                m_CollectionTitles = new List<object>();
            #endregion

            // Professions weren't verified on 1.0 RC0
            if (!CharacterCreation.VerifyProfession(m_Profession))
                m_Profession = 0;

            if (m_PermaFlags == null)
                m_PermaFlags = new List<Mobile>();

            if (m_JusticeProtectors == null)
                m_JusticeProtectors = new List<Mobile>();

            if (m_BOBFilter == null)
                m_BOBFilter = new Engines.BulkOrders.BOBFilter();
			
			#region FS:ATS
			if ( m_TamingBOBFilter == null )
				m_TamingBOBFilter = new Engines.BulkOrders.TamingBOBFilter();
			#endregion
            if (m_GuildRank == null)
                m_GuildRank = Guilds.RankDefinition.Member;	//Default to member if going from older verstion to new version (only time it should be null)

            if (m_LastOnline == DateTime.MinValue && Account != null)
                m_LastOnline = ((Account)Account).LastLogin;

            if (m_ChampionTitles == null)
                m_ChampionTitles = new ChampionTitleInfo();

			if ( AccessLevel > AccessLevel.Player )
				m_IgnoreMobiles = true;

            List<Mobile> list = this.Stabled;

            for (int i = 0; i < list.Count; ++i)
            {
                BaseCreature bc = list[i] as BaseCreature;

                if (bc != null)
                    bc.IsStabled = true;
            }

            CheckAtrophies(this);

            if (Hidden)	//Hiding is the only buff where it has an effect that's serialized.
                AddBuff(new BuffInfo(BuffIcon.HidingAndOrStealth, 1075655));
        }

        public override void Serialize(GenericWriter writer)
        {
            //cleanup our anti-macro table 
            foreach (Hashtable t in m_AntiMacroTable.Values)
            {
                ArrayList remove = new ArrayList();
                foreach (CountAndTimeStamp time in t.Values)
                {
                    if (time.TimeStamp + SkillCheck.AntiMacroExpire <= DateTime.Now)
                        remove.Add(time);
                }

                for (int i = 0; i < remove.Count; ++i)
                    t.Remove(remove[i]);
            }

			CheckKillDecay();

            CheckAtrophies(this);

            base.Serialize(writer);

			writer.Write( (int) 41 ); // version old 40
			#region Custom BODs
			writer.Write(NextFletcherBulkOrder);
            writer.Write(NextCarpenterBulkOrder);
			#endregion
			#region Player Government
			writer.Write( m_City );
			writer.Write( m_CityTitle );
			writer.Write( m_ShowCityTitle );
			writer.Write( m_OwesBackTaxes );
			writer.Write( m_BackTaxesAmount );
			#endregion
            #region Guantlet Points
            writer.Write(m_GauntletPoints); 
            #endregion
            #region SA
            writer.Write( (DateTime) m_SSNextSeed );
            writer.Write( (DateTime) m_SSSeedExpire );
            writer.Write( (Point3D) m_SSSeedLocation );
            writer.Write( (Map) m_SSSeedMap );
            #endregion

            #region QueensLoyaltySystem              // Queens Loyalty System

            writer.Write((long)m_LevelExp);
            writer.Write((long)m_Exp);
            writer.Write((int)m_Level);

            writer.Write((string)m_ExpTitle);
            #endregion
//-----------------------------------------------LIFE STONE SYSTEM PART 4----------------------------------------------------------------------------
			writer.Write( (Point3D) LifeStone );
			writer.Write( (bool) Bound );
			writer.Write( (Map) LifeStoneMap );
//-----------------------------------------------LIFE STONE SYSTEM PART 4----------------------------------------------------------------------------

			m_TamingBOBFilter.Serialize( writer );

			writer.Write( m_Bioenginer );

			writer.Write( NextTamingBulkOrder );

 			writer.Write((int)m_Werewolves);
			writer.Write((int)m_WerewolfBited);
			writer.Write(WerewolfBiteTime);
/*GD13_HS_Start_6
			writer.Write( (int)m_HuntMode );
			writer.Write( (int)m_HuntRank );
			writer.Write( (int)m_HuntReward );
			writer.Write( (bool)m_Hunting );
			writer.Write( (int)m_EasyKills );
			writer.Write( (int)m_HardKills );
			writer.Write( (int)m_ExtremeKills );
			writer.Write( (int)m_GodKills );
GD13_HS_End_6*/
//Freyd's Vampire System
 			writer.Write((int)m_Vampire);
			writer.Write((int)m_VampireBited);
			writer.Write(VampireBiteTime);
//Freyd's Vampire System

			#region Edited By: A.A.R
            writer.Write((DateTime)m_PromoGiftLast);

            writer.Write((DateTime)m_LastTimePaged);

            #endregion Edited By: A.A.R


            writer.Write(m_VASTotalMonsterFame);	//This ain't going to be a small #.

            writer.Write(m_AutoStabled, true);

            #region Veteran Rewards
            writer.Write((DateTime)m_AnkhNextUse);
            #endregion

            #region Mondain's Legacy version 26
            QuestWriter.Quests(writer, m_Quests);
            QuestWriter.Chains(writer, m_Chains);

            if (m_Collections == null)
            {
                writer.Write((int)0);
            }
            else
            {
                writer.Write((int)m_Collections.Count);

                foreach (KeyValuePair<Collection, int> pair in m_Collections)
                {
                    writer.Write((int)pair.Key);
                    writer.Write((int)pair.Value);
                }
            }

            if (m_CollectionTitles == null)
            {
                writer.Write((int)0);
            }
            else
            {
                writer.Write((int)m_CollectionTitles.Count);

                for (int i = 0; i < m_CollectionTitles.Count; i++)
                    QuestWriter.Object(writer, m_CollectionTitles[i]);
            }

            writer.Write((int)m_SelectedTitle);
            writer.Write((DateTime)m_Peaced);
            #endregion

			// Version 28
			
			
            if (m_AcquiredRecipes == null)
            {
                writer.Write((int)0);
            }
            else
            {
                writer.Write(m_AcquiredRecipes.Count);

                foreach (KeyValuePair<int, bool> kvp in m_AcquiredRecipes)
                {
                    writer.Write(kvp.Key);
                    writer.Write(kvp.Value);
                }
            }

            writer.WriteDeltaTime(m_LastHonorLoss);

            ChampionTitleInfo.Serialize(writer, m_ChampionTitles);

            writer.Write(m_LastValorLoss);
            writer.WriteEncodedInt(m_ToTItemsTurnedIn);
            writer.Write(m_ToTTotalMonsterFame);	//This ain't going to be a small #.

            writer.WriteEncodedInt(m_AllianceMessageHue);
            writer.WriteEncodedInt(m_GuildMessageHue);

            writer.WriteEncodedInt(m_GuildRank.Rank);
            writer.Write(m_LastOnline);

            writer.WriteEncodedInt((int)m_SolenFriendship);

            QuestSerializer.Serialize(m_Quest, writer);

            if (m_DoneQuests == null)
            {
                writer.WriteEncodedInt((int)0);
            }
            else
            {
                writer.WriteEncodedInt((int)m_DoneQuests.Count);

                for (int i = 0; i < m_DoneQuests.Count; ++i)
                {
                    QuestRestartInfo restartInfo = m_DoneQuests[i];

                    QuestSerializer.Write((Type)restartInfo.QuestType, QuestSystem.QuestTypes, writer);
                    writer.Write((DateTime)restartInfo.RestartTime);
                }
            }

            writer.WriteEncodedInt((int)m_Profession);

            writer.WriteDeltaTime(m_LastCompassionLoss);

            writer.WriteEncodedInt(m_CompassionGains);

            if (m_CompassionGains > 0)
                writer.WriteDeltaTime(m_NextCompassionDay);

            m_BOBFilter.Serialize(writer);

            bool useMods = (m_HairModID != -1 || m_BeardModID != -1);

            writer.Write(useMods);

            if (useMods)
            {
                writer.Write((int)m_HairModID);
                writer.Write((int)m_HairModHue);
                writer.Write((int)m_BeardModID);
                writer.Write((int)m_BeardModHue);
            }

            writer.Write(SavagePaintExpiration);

            writer.Write((int)m_NpcGuild);
            writer.Write((DateTime)m_NpcGuildJoinTime);
            writer.Write((TimeSpan)m_NpcGuildGameTime);

            writer.Write(m_PermaFlags, true);

            writer.Write(NextTailorBulkOrder);

            writer.Write(NextSmithBulkOrder);

            writer.WriteDeltaTime(m_LastJusticeLoss);
            writer.Write(m_JusticeProtectors, true);

            writer.WriteDeltaTime(m_LastSacrificeGain);
            writer.WriteDeltaTime(m_LastSacrificeLoss);
            writer.Write(m_AvailableResurrects);

            writer.Write((int)m_Flags);

            writer.Write(m_LongTermElapse);
            writer.Write(m_ShortTermElapse);
            writer.Write(this.GameTime);
        }
		
        public static void CheckAtrophies(Mobile m)
        {
            SacrificeVirtue.CheckAtrophy(m);
            JusticeVirtue.CheckAtrophy(m);
            CompassionVirtue.CheckAtrophy(m);
            ValorVirtue.CheckAtrophy(m);
            HonorVirtue.CheckAtrophy(m);

            if (m is PlayerMobile)
                ChampionTitleInfo.CheckAtrophy((PlayerMobile)m);
        }

		public void CheckKillDecay()
		{
			if (m_ShortTermElapse < this.GameTime)
			{
				m_ShortTermElapse += TimeSpan.FromHours(8);
				if (ShortTermMurders > 0)
					--ShortTermMurders;
			}

			if (m_LongTermElapse < this.GameTime)
			{
				m_LongTermElapse += TimeSpan.FromHours(40);
				if (Kills > 0)
					--Kills;
			}
		}

        public void ResetKillTime()
        {
            m_ShortTermElapse = this.GameTime + TimeSpan.FromHours(8);
            m_LongTermElapse = this.GameTime + TimeSpan.FromHours(40);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime SessionStart
        {
            get { return m_SessionStart; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan GameTime
        {
            get
            {
                if (NetState != null)
                    return m_GameTime + (DateTime.Now - m_SessionStart);
                else
                    return m_GameTime;
            }
        }

        public override bool CanSee(Mobile m)
        {
            if (m is CharacterStatue)
                ((CharacterStatue)m).OnRequestedAnimation(this);

            if (m is PlayerMobile && ((PlayerMobile)m).m_VisList.Contains(this))
                return true;

			if ( m_DuelContext != null && m_DuelPlayer != null && !m_DuelContext.Finished && m_DuelContext.m_Tournament != null && !m_DuelPlayer.Eliminated )
			{
				Mobile owner = m;

				if ( owner is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)owner;

					Mobile master = bc.GetMaster();

					if( master != null )
						owner = master;
				}

				if ( m.AccessLevel == AccessLevel.Player && owner is PlayerMobile && ((PlayerMobile)owner).DuelContext != m_DuelContext )
					return false;
			}

            return base.CanSee(m);
        }

        public override bool CanSee(Item item)
        {
            if (m_DesignContext != null && m_DesignContext.Foundation.IsHiddenToCustomizer(item))
                return false;

            return base.CanSee(item);
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            Faction faction = Faction.Find(this);

            if (faction != null)
                faction.RemoveMember(this);

            BaseHouse.HandleDeletion(this);

            DisguiseTimers.RemoveTimer(this);
        }

        public override bool NewGuildDisplay { get { return Server.Guilds.Guild.NewGuildSystem; } }
		
        public delegate void PlayerPropertiesEventHandler(PlayerPropertiesEventArgs e);
        public static event PlayerPropertiesEventHandler PlayerProperties;
		#region XMLPoints PvP
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties(list);
			XmlTitle.AddTitles(this, list);

			XmlPoints a = (XmlPoints)XmlAttach.FindAttachment(this, typeof(XmlPoints));

			if (a != null)
			{
				list.Add(1070722, "Kills {0} / Deaths {1} : Rank={2}", a.Kills, a.Deaths, a.Rank);
			}
			#region Player Government
			if ( m_ShowCityTitle == true && m_City != null )
			{
				list.Add( 1060659, "{0}\t{1}", m_City.CityName, m_CityTitle );
			}
			#endregion
		}
		#endregion
        public class PlayerPropertiesEventArgs : EventArgs
        {
                public PlayerMobile Player = null;
                public ObjectPropertyList PropertyList = null;

                public PlayerPropertiesEventArgs(PlayerMobile player, ObjectPropertyList list)
                {
                        Player = player;
                        PropertyList = list;
                }
        }
        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            #region Mondain's Legacy
            if (m_CollectionTitles != null && m_SelectedTitle > -1)
            {
                if (m_SelectedTitle < m_CollectionTitles.Count)
                {
                    if (m_CollectionTitles[m_SelectedTitle] is int)
                        list.Add((int)m_CollectionTitles[m_SelectedTitle]);
                    else if (m_CollectionTitles[m_SelectedTitle] is string)
                        list.Add(1049644, (string)m_CollectionTitles[m_SelectedTitle]);
                }
            }
            #endregion

            if (Map == Faction.Facet)
            {
                PlayerState pl = PlayerState.Find(this);

                if (pl != null)
                {
                    Faction faction = pl.Faction;

                    if (faction.Commander == this)
                        list.Add(1042733, faction.Definition.PropName); // Commanding Lord of the ~1_FACTION_NAME~
                    else if (pl.Sheriff != null)
                        list.Add(1042734, "{0}\t{1}", pl.Sheriff.Definition.FriendlyName, faction.Definition.PropName); // The Sheriff of  ~1_CITY~, ~2_FACTION_NAME~
                    else if (pl.Finance != null)
                        list.Add(1042735, "{0}\t{1}", pl.Finance.Definition.FriendlyName, faction.Definition.PropName); // The Finance Minister of ~1_CITY~, ~2_FACTION_NAME~
                    else if (pl.MerchantTitle != MerchantTitle.None)
                        list.Add(1060776, "{0}\t{1}", MerchantTitles.GetInfo(pl.MerchantTitle).Title, faction.Definition.PropName); // ~1_val~, ~2_val~
                    else
                        list.Add(1060776, "{0}\t{1}", pl.Rank.Title, faction.Definition.PropName); // ~1_val~, ~2_val~
                }
            }

            if (Core.ML)
            {
                for (int i = AllFollowers.Count - 1; i >= 0; i--)
                {
                    BaseCreature c = AllFollowers[i] as BaseCreature;

                    if (c != null && c.ControlOrder == OrderType.Guard)
                    {
                        list.Add(501129); // guarded
                        break;
                    }
                }
            }
            #region QueensLoyaltySystem              // Queens Loyalty System

            if (m_Exp >= m_LevelExp)
            {
                while (m_Exp >= m_LevelExp)
                {
                    m_Exp -= m_LevelExp;
                    m_Level += 1;
                    m_LevelExp = (long)(1000 * (Math.Pow(1.4, m_Level)));
                }
            }

            if (m_Exp < 0)
            {
                while (m_Exp < 0)
                {
                    if (m_Level == 0) m_Exp = 0;
                    else
                    {
                        m_LevelExp = (long)(1000 * (Math.Pow(1.4, m_Level - 1)));
                        m_Exp += (long)(m_LevelExp);
                        m_Level -= 1;
                    }
                }
			}

            m_LevelExp = (long)(1000 * (Math.Pow(1.4, m_Level)));

            if (AccessLevel == AccessLevel.Player || AccessLevel == AccessLevel.Counselor)
            {
                if (m_Level == 0) m_ExpTitle = "TerMur-guest";
                else if (m_Level >= 1 && m_Level <= 5) m_ExpTitle = "Friend of TerMur";
                else if (m_Level >= 6 && m_Level <= 10) m_ExpTitle = "Friend of TerMur";
                else if (m_Level >= 11 && m_Level <= 15) m_ExpTitle = "Friend of TerMur";
                else if (m_Level >= 16 && m_Level <= 20) m_ExpTitle = "Friend of TerMur";
                else if (m_Level >= 21 && m_Level <= 25) m_ExpTitle = "Friend of TerMur";
                else if (m_Level >= 26 && m_Level <= 30) m_ExpTitle = "A Citizen of TerMur";
                else if (m_Level >= 31 && m_Level <= 35) m_ExpTitle = "A Citizen of TerMur";
                else if (m_Level >= 36 && m_Level <= 40) m_ExpTitle = "A Citizen of TerMur";
                else if (m_Level >= 41 && m_Level <= 45) m_ExpTitle = "A Citizen of TerMur";
                else if (m_Level >= 46 && m_Level <= 50) m_ExpTitle = "A Citizen of TerMur";
                else if (m_Level >= 51 && m_Level <= 60) m_ExpTitle = "A Citizen of TerMur";
                else if (m_Level >= 61 && m_Level <= 70) m_ExpTitle = "A Noble of Termur";
                else if (m_Level >= 71 && m_Level <= 80) m_ExpTitle = "A Noble of Termur";
                else if (m_Level >= 80 && m_Level <= 100) m_ExpTitle = "A Noble of Termur";
                else if (m_Level >= 101) m_ExpTitle = "A Noble of Termur";
            }
            /*
			else
            {
                m_ExpTitle = "Staff: ";
                if (AccessLevel == AccessLevel.GameMaster) m_ExpTitle += "GameMaster";
                else if (AccessLevel == AccessLevel.Seer) m_ExpTitle += "Seer";
                else if (AccessLevel == AccessLevel.Administrator) m_ExpTitle += "Administrator";
                else if (AccessLevel == AccessLevel.Developer) m_ExpTitle += "Developer";
                else if (AccessLevel == AccessLevel.Owner) m_ExpTitle += "Owner";
            }
			*/

			// Xml spawner 3.26c QueensLoyaltyTitle - SOF
			XmlData QueenTitle = (XmlData)XmlAttach.FindAttachment(this, typeof(XmlData), "QueenTitle");

			if (QueenTitle != null && QueenTitle.Data == "True")
			{
				return;
			}
			else 
			{ 
				list.Add(String.Concat("Queens Loyalty Level: ", String.Format("<BASEFONT COLOR={0}>{1}", "#FF0000", m_Level), "  ", String.Format("<BASEFONT COLOR={0}>{1}", "#000FFF", (int)(100 * m_Exp / m_LevelExp)), " %  ", String.Format("<BASEFONT COLOR={0}>{1}", "#0FFF00", m_ExpTitle))); InvalidateMyRunUO();
			}  
			// Xml Spawner 3.26c QueensLoyaltyTitle - EOF
            #endregion
			
			#region AccessLevel
			if (AccessLevel > AccessLevel.Player)
  			{
  				string color = "";
  				switch ( AccessLevel )
  				{
 		 		case AccessLevel.Counselor: color = "#00BFFF"; break; //Deep Sky Blue
 		 		case AccessLevel.GameMaster: color = "#FF0000"; break; //Red
 		 		case AccessLevel.Seer: color = "#00FF00"; break; //Green
 		 		case AccessLevel.Administrator: color = "#FFD700"; break; //Gold
	 		    case AccessLevel.Developer: color = "#FFD700"; break; //Gold
	 		    case AccessLevel.Owner: color = "#FFD700"; break; //Gold
  				}
					list.Add( 1060658, "{0}\t{1}", "Staff", String.Format("<BASEFONT COLOR={0}>{1}", color, AccessLevel) );
  			}
			#endregion

            if (PlayerProperties != null)
            PlayerProperties(new PlayerPropertiesEventArgs(this, list));
        }

        public override void OnSingleClick(Mobile from)
        {
            if (Map == Faction.Facet)
            {
                PlayerState pl = PlayerState.Find(this);

                if (pl != null)
                {
                    string text;
                    bool ascii = false;

                    Faction faction = pl.Faction;

                    if (faction.Commander == this)
                        text = String.Concat(this.Female ? "(Commanding Lady of the " : "(Commanding Lord of the ", faction.Definition.FriendlyName, ")");
                    else if (pl.Sheriff != null)
                        text = String.Concat("(The Sheriff of ", pl.Sheriff.Definition.FriendlyName, ", ", faction.Definition.FriendlyName, ")");
                    else if (pl.Finance != null)
                        text = String.Concat("(The Finance Minister of ", pl.Finance.Definition.FriendlyName, ", ", faction.Definition.FriendlyName, ")");
                    else
                    {
                        ascii = true;

                        if (pl.MerchantTitle != MerchantTitle.None)
                            text = String.Concat("(", MerchantTitles.GetInfo(pl.MerchantTitle).Title.String, ", ", faction.Definition.FriendlyName, ")");
                        else
                            text = String.Concat("(", pl.Rank.Title.String, ", ", faction.Definition.FriendlyName, ")");
                    }

                    int hue = (Faction.Find(from) == faction ? 98 : 38);

                    PrivateOverheadMessage(MessageType.Label, hue, ascii, text, from.NetState);
                }
            }

            base.OnSingleClick(from);
        }

        protected override bool OnMove(Direction d)
        {
            if (!Core.SE)
                return base.OnMove(d);

            if (AccessLevel != AccessLevel.Player)
                return true;

            if (Hidden && DesignContext.Find(this) == null)	//Hidden & NOT customizing a house
            {
                if (!Mounted && Skills.Stealth.Value >= 25.0)
                {
                    bool running = (d & Direction.Running) != 0;

                    if (running)
                    {
                        if ((AllowedStealthSteps -= 2) <= 0)
                            RevealingAction();
                    }
                    else if (AllowedStealthSteps-- <= 0)
                    {
                        Server.SkillHandlers.Stealth.OnUse(this);
                    }
                }
                else
                {
                    RevealingAction();
                }
            }

            #region Mondain's Legacy
            if (InvisibilityPotion.HasTimer(this))
                InvisibilityPotion.Iterrupt(this);
            #endregion

            return true;
        }

        private bool m_BedrollLogout;

        public bool BedrollLogout
        {
            get { return m_BedrollLogout; }
            set { m_BedrollLogout = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override bool Paralyzed
        {
            get
            {
                return base.Paralyzed;
            }
            set
            {
                base.Paralyzed = value;

                if (value)
                    AddBuff(new BuffInfo(BuffIcon.Paralyze, 1075827));	//Paralyze/You are frozen and can not move
                else
                    RemoveBuff(BuffIcon.Paralyze);
            }
        }
        
        #region SA Mysticism
        [CommandProperty(AccessLevel.GameMaster)]
        public override bool Sleep
        {
            get
            {
                return base.Sleep;
            }
            set
            {
                base.Sleep = value;

                if (value)
                    AddBuff(new BuffInfo(BuffIcon.Sleep,  1080139));	//Paralyze/You are frozen and can not move
                else
                    RemoveBuff(BuffIcon.Sleep);
            }
        }
		#endregion

        #region Ethics
        private Ethics.Player m_EthicPlayer;

        [CommandProperty(AccessLevel.GameMaster)]
        public Ethics.Player EthicPlayer
        {
            get { return m_EthicPlayer; }
            set { m_EthicPlayer = value; }
        }
        #endregion

        #region Factions
        private PlayerState m_FactionPlayerState;

        public PlayerState FactionPlayerState
        {
            get { return m_FactionPlayerState; }
            set { m_FactionPlayerState = value; }
        }
        #endregion

		#region Dueling
		private Engines.ConPVP.DuelContext m_DuelContext;
		private Engines.ConPVP.DuelPlayer m_DuelPlayer;

		public Engines.ConPVP.DuelContext DuelContext
		{
			get{ return m_DuelContext; }
		}

		public Engines.ConPVP.DuelPlayer DuelPlayer
		{
			get{ return m_DuelPlayer; }
			set
			{
				bool wasInTourny = ( m_DuelContext != null && !m_DuelContext.Finished && m_DuelContext.m_Tournament != null );

				m_DuelPlayer = value;

				if ( m_DuelPlayer == null )
					m_DuelContext = null;
				else
					m_DuelContext = m_DuelPlayer.Participant.Context;

				bool isInTourny = ( m_DuelContext != null && !m_DuelContext.Finished && m_DuelContext.m_Tournament != null );

				if ( wasInTourny != isInTourny )
					SendEverything();
			}
		}
		#endregion

        #region Quests
        private QuestSystem m_Quest;
        private List<QuestRestartInfo> m_DoneQuests;
        private SolenFriendship m_SolenFriendship;

        public QuestSystem Quest
        {
            get { return m_Quest; }
            set { m_Quest = value; }
        }

        public List<QuestRestartInfo> DoneQuests
        {
            get { return m_DoneQuests; }
            set { m_DoneQuests = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public SolenFriendship SolenFriendship
        {
            get { return m_SolenFriendship; }
            set { m_SolenFriendship = value; }
        }
        #endregion

        #region Mondain's Legacy
        private List<BaseQuest> m_Quests;
        private Dictionary<QuestChain, BaseChain> m_Chains;

        public List<BaseQuest> Quests
        {
            get { return m_Quests; }
        }

        public Dictionary<QuestChain, BaseChain> Chains
        {
            get { return m_Chains; }
        }

        private DateTime m_Peaced;

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime PeacedUntil
        {
            get { return m_Peaced; }
            set { m_Peaced = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Peaced
        {
            get
            {
                if (m_Peaced > DateTime.Now)
                    return true;

                return false;
            }
        }

        private Dictionary<Collection, int> m_Collections;
        private List<object> m_CollectionTitles;
        private int m_SelectedTitle;

        public Dictionary<Collection, int> Collections
        {
            get { return m_Collections; }
        }

        public List<object> CollectionTitles
        {
            get { return m_CollectionTitles; }
        }

        public int GetCollectionPoints(Collection collection)
        {
            if (m_Collections == null)
                m_Collections = new Dictionary<Collection, int>();

            int points = 0;

            if (m_Collections.ContainsKey(collection))
                m_Collections.TryGetValue(collection, out points);

            return points;
        }

        public void AddCollectionPoints(Collection collection, int points)
        {
            if (m_Collections == null)
                m_Collections = new Dictionary<Collection, int>();

            if (m_Collections.ContainsKey(collection))
                m_Collections[collection] += points;
            else
                m_Collections.Add(collection, points);
        }

        public void SelectCollectionTitle(int num)
        {
            if (num == -1)
            {
                m_SelectedTitle = num;
                SendLocalizedMessage(1074010); // You elect to hide your Reward Title.
            }
            else if (num < m_CollectionTitles.Count && num >= -1)
            {
                if (m_SelectedTitle != num)
                {
                    m_SelectedTitle = num;

                    if (m_CollectionTitles[num] is int)
                        SendLocalizedMessage(1074008, "#" + (int)m_CollectionTitles[num]); // You change your Reward Title to "~1_TITLE~".	
                    else if (m_CollectionTitles[num] is string)
                        SendLocalizedMessage(1074008, (string)m_CollectionTitles[num]); // You change your Reward Title to "~1_TITLE~".	
                }
                else
                    SendLocalizedMessage(1074009); // You decide to leave your title as it is.
            }

            InvalidateProperties();
        }

        public bool AddCollectionTitle(object title)
        {
            if (m_CollectionTitles == null)
                m_CollectionTitles = new List<object>();

            if (title != null && !m_CollectionTitles.Contains(title))
            {
                m_CollectionTitles.Add(title);
                m_SelectedTitle = m_CollectionTitles.Count - 1;
                InvalidateProperties();
                return true;
            }

            return false;
        }

        public void ShowChangeTitle()
        {
            SendGump(new SelectTitleGump(this, m_SelectedTitle));
        }
        #endregion

        #region MyRunUO Invalidation
        private bool m_ChangedMyRunUO;

        public bool ChangedMyRunUO
        {
            get { return m_ChangedMyRunUO; }
            set { m_ChangedMyRunUO = value; }
        }

        public void InvalidateMyRunUO()
        {
            if (!Deleted && !m_ChangedMyRunUO)
            {
                m_ChangedMyRunUO = true;
                Engines.MyRunUO.MyRunUO.QueueMobileUpdate(this);
            }
        }

        public override void OnKillsChange(int oldValue)
        {
            if (this.Young && this.Kills > oldValue)
            {
                Account acc = this.Account as Account;

                if (acc != null)
                    acc.RemoveYoungStatus(0);
            }

            InvalidateMyRunUO();
        }

        public override void OnGenderChanged(bool oldFemale)
        {
            InvalidateMyRunUO();
        }

        public override void OnGuildChange(Server.Guilds.BaseGuild oldGuild)
        {
            InvalidateMyRunUO();
        }

        public override void OnGuildTitleChange(string oldTitle)
        {
            InvalidateMyRunUO();
        }

        public override void OnKarmaChange(int oldValue)
        {
            InvalidateMyRunUO();
        }

        public override void OnFameChange(int oldValue)
        {
            InvalidateMyRunUO();
        }

        public override void OnSkillChange(SkillName skill, double oldBase)
        {
            if (this.Young && this.SkillsTotal >= 4500)
            {
                Account acc = this.Account as Account;

                if (acc != null)
                    acc.RemoveYoungStatus(1019036); // You have successfully obtained a respectable skill level, and have outgrown your status as a young player!
            }

            InvalidateMyRunUO();
        }

        public override void OnAccessLevelChanged(AccessLevel oldLevel)
        {
			if ( AccessLevel == AccessLevel.Player )
				IgnoreMobiles = false;
			else
				IgnoreMobiles = true;
			
            InvalidateMyRunUO();
        }

        public override void OnRawStatChange(StatType stat, int oldValue)
        {
            InvalidateMyRunUO();
        }

        public override void OnDelete()
        {
            if (m_ReceivedHonorContext != null)
                m_ReceivedHonorContext.Cancel();
            if (m_SentHonorContext != null)
                m_SentHonorContext.Cancel();

            InvalidateMyRunUO();
        }

        #endregion

        #region Fastwalk Prevention
        private static bool FastwalkPrevention = true; // Is fastwalk prevention enabled?
        private static TimeSpan FastwalkThreshold = TimeSpan.FromSeconds(0.4); // Fastwalk prevention will become active after 0.4 seconds

        private DateTime m_NextMovementTime;

		public virtual bool UsesFastwalkPrevention { get { return (AccessLevel < AccessLevel.Counselor) & !Flying; } }

        public override TimeSpan ComputeMovementSpeed(Direction dir, bool checkTurning)
        {
            if (checkTurning && (dir & Direction.Mask) != (this.Direction & Direction.Mask))
				return Mobile.RunMount;	// We are NOT actually moving (just a direction change)

            TransformContext context = TransformationSpellHelper.GetContext(this);

            if (context != null && context.Type == typeof(ReaperFormSpell))
                return Mobile.WalkFoot;

            bool running = ((dir & Direction.Running) != 0);

            bool onHorse = (this.Mount != null);

            AnimalFormContext animalContext = AnimalForm.GetContext(this);

            if (onHorse || (animalContext != null && animalContext.SpeedBoost))
                return (running ? Mobile.RunMount : Mobile.WalkMount);

            return (running ? Mobile.RunFoot : Mobile.WalkFoot);
        }

        public static bool MovementThrottle_Callback(NetState ns)
        {
            PlayerMobile pm = ns.Mobile as PlayerMobile;

            if (pm == null || !pm.UsesFastwalkPrevention)
                return true;

            if (pm.m_NextMovementTime == DateTime.MinValue)
            {
                // has not yet moved
                pm.m_NextMovementTime = DateTime.Now;
                return true;
            }

            TimeSpan ts = pm.m_NextMovementTime - DateTime.Now;

            if (ts < TimeSpan.Zero)
            {
                // been a while since we've last moved
                pm.m_NextMovementTime = DateTime.Now;
                return true;
            }

            return (ts < FastwalkThreshold);
        }

        #endregion

        #region Enemy of One
        private Type m_EnemyOfOneType;
        private bool m_WaitingForEnemy;

        public Type EnemyOfOneType
        {
            get { return m_EnemyOfOneType; }
            set
            {
                Type oldType = m_EnemyOfOneType;
                Type newType = value;

                if (oldType == newType)
                    return;

                m_EnemyOfOneType = value;

                DeltaEnemies(oldType, newType);
            }
        }

        public bool WaitingForEnemy
        {
            get { return m_WaitingForEnemy; }
            set { m_WaitingForEnemy = value; }
        }

        private void DeltaEnemies(Type oldType, Type newType)
        {
            foreach (Mobile m in this.GetMobilesInRange(18))
            {
                Type t = m.GetType();

				if (t == oldType || t == newType)
				{
                    NetState ns = this.NetState;

					if (ns != null)
					{
						if (ns.StygianAbyss)
						{
                            ns.Send(new MobileMoving(m, Notoriety.Compute(this, m)));
						}
						else
						{
                            ns.Send(new MobileMovingOld(m, Notoriety.Compute(this, m)));
                        }
                    }
                }
            }
        }

        #endregion

        #region Hair and beard mods
        private int m_HairModID = -1, m_HairModHue;
        private int m_BeardModID = -1, m_BeardModHue;

        public void SetHairMods(int hairID, int beardID)
        {
            if (hairID == -1)
                InternalRestoreHair(true, ref m_HairModID, ref m_HairModHue);
            else if (hairID != -2)
                InternalChangeHair(true, hairID, ref m_HairModID, ref m_HairModHue);

            if (beardID == -1)
                InternalRestoreHair(false, ref m_BeardModID, ref m_BeardModHue);
            else if (beardID != -2)
                InternalChangeHair(false, beardID, ref m_BeardModID, ref m_BeardModHue);
        }

        private void CreateHair(bool hair, int id, int hue)
        {
            if (hair)
            {
                //TODO Verification?
                HairItemID = id;
                HairHue = hue;
            }
            else
            {
                FacialHairItemID = id;
                FacialHairHue = hue;
            }
        }

        private void InternalRestoreHair(bool hair, ref int id, ref int hue)
        {
            if (id == -1)
                return;

            if (hair)
                HairItemID = 0;
            else
                FacialHairItemID = 0;

            //if( id != 0 )
            CreateHair(hair, id, hue);

            id = -1;
            hue = 0;
        }

        private void InternalChangeHair(bool hair, int id, ref int storeID, ref int storeHue)
        {
            if (storeID == -1)
            {
                storeID = hair ? HairItemID : FacialHairItemID;
                storeHue = hair ? HairHue : FacialHairHue;
            }
            CreateHair(hair, id, 0);
        }

        #endregion

        #region Virtues
        private DateTime m_LastSacrificeGain;
        private DateTime m_LastSacrificeLoss;
        private int m_AvailableResurrects;

        public DateTime LastSacrificeGain { get { return m_LastSacrificeGain; } set { m_LastSacrificeGain = value; } }
        public DateTime LastSacrificeLoss { get { return m_LastSacrificeLoss; } set { m_LastSacrificeLoss = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int AvailableResurrects { get { return m_AvailableResurrects; } set { m_AvailableResurrects = value; } }

        private DateTime m_NextJustAward;
        private DateTime m_LastJusticeLoss;
        private List<Mobile> m_JusticeProtectors;

        public DateTime LastJusticeLoss { get { return m_LastJusticeLoss; } set { m_LastJusticeLoss = value; } }
        public List<Mobile> JusticeProtectors { get { return m_JusticeProtectors; } set { m_JusticeProtectors = value; } }

        private DateTime m_LastCompassionLoss;
        private DateTime m_NextCompassionDay;
        private int m_CompassionGains;

        public DateTime LastCompassionLoss { get { return m_LastCompassionLoss; } set { m_LastCompassionLoss = value; } }
        public DateTime NextCompassionDay { get { return m_NextCompassionDay; } set { m_NextCompassionDay = value; } }
        public int CompassionGains { get { return m_CompassionGains; } set { m_CompassionGains = value; } }

        private DateTime m_LastValorLoss;

        public DateTime LastValorLoss { get { return m_LastValorLoss; } set { m_LastValorLoss = value; } }

        private DateTime m_LastHonorLoss;
        private DateTime m_LastHonorUse;
        private bool m_HonorActive;
        private HonorContext m_ReceivedHonorContext;
        private HonorContext m_SentHonorContext;
		public DateTime m_hontime;

        public DateTime LastHonorLoss { get { return m_LastHonorLoss; } set { m_LastHonorLoss = value; } }
        public DateTime LastHonorUse { get { return m_LastHonorUse; } set { m_LastHonorUse = value; } }
        public bool HonorActive { get { return m_HonorActive; } set { m_HonorActive = value; } }
        public HonorContext ReceivedHonorContext { get { return m_ReceivedHonorContext; } set { m_ReceivedHonorContext = value; } }
        public HonorContext SentHonorContext { get { return m_SentHonorContext; } set { m_SentHonorContext = value; } }
        #endregion

        #region Young system
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Young
        {
            get { return GetFlag(PlayerFlag.Young); }
            set { SetFlag(PlayerFlag.Young, value); InvalidateProperties(); }
        }

        public override string ApplyNameSuffix(string suffix)
        {
            if (Young)
            {
                if (suffix.Length == 0)
                    suffix = "(Young)";
                else
                    suffix = String.Concat(suffix, " (Young)");
            }

            #region Ethics
            if (m_EthicPlayer != null)
            {
                if (suffix.Length == 0)
                    suffix = m_EthicPlayer.Ethic.Definition.Adjunct.String;
                else
                    suffix = String.Concat(suffix, " ", m_EthicPlayer.Ethic.Definition.Adjunct.String);
            }
            #endregion

            if (Core.ML && this.Map == Faction.Facet)
            {
                Faction faction = Faction.Find(this);

                if (faction != null)
                {
                    string adjunct = String.Format("[{0}]", faction.Definition.Abbreviation);
                    if (suffix.Length == 0)
                        suffix = adjunct;
                    else
                        suffix = String.Concat(suffix, " ", adjunct);
                }
            }

            return base.ApplyNameSuffix(suffix);
        }

        public override TimeSpan GetLogoutDelay()
        {
            if (Young || BedrollLogout || TestCenter.Enabled)
                return TimeSpan.Zero;

            return base.GetLogoutDelay();
        }

        private DateTime m_LastYoungMessage = DateTime.MinValue;

        public bool CheckYoungProtection(Mobile from)
        {
            if (!this.Young)
                return false;

            if (Region.IsPartOf(typeof(DungeonRegion)))
				return false;

            if (from is BaseCreature && ((BaseCreature)from).IgnoreYoungProtection)
                return false;

            if (this.Quest != null && this.Quest.IgnoreYoungProtection(from))
                return false;

            if (DateTime.Now - m_LastYoungMessage > TimeSpan.FromMinutes(1.0))
            {
                m_LastYoungMessage = DateTime.Now;
                SendLocalizedMessage(1019067); // A monster looks at you menacingly but does not attack.  You would be under attack now if not for your status as a new citizen of Britannia.
            }

            return true;
        }

        private DateTime m_LastYoungHeal = DateTime.MinValue;

        public bool CheckYoungHealTime()
        {
            if (DateTime.Now - m_LastYoungHeal > TimeSpan.FromMinutes(5.0))
            {
                m_LastYoungHeal = DateTime.Now;
                return true;
            }

            return false;
        }

        private static Point3D[] m_TrammelDeathDestinations = new Point3D[]
			{
				new Point3D( 1481, 1612, 20 ),
				new Point3D( 2708, 2153,  0 ),
				new Point3D( 2249, 1230,  0 ),
				new Point3D( 5197, 3994, 37 ),
				new Point3D( 1412, 3793,  0 ),
				new Point3D( 3688, 2232, 20 ),
				new Point3D( 2578,  604,  0 ),
				new Point3D( 4397, 1089,  0 ),
				new Point3D( 5741, 3218, -2 ),
				new Point3D( 2996, 3441, 15 ),
				new Point3D(  624, 2225,  0 ),
				new Point3D( 1916, 2814,  0 ),
				new Point3D( 2929,  854,  0 ),
				new Point3D(  545,  967,  0 ),
			//	new Point3D( 3665, 2587,  0 )	// Old Haven, no healer here anymore
				new Point3D( 3469, 2559, 36 )	// New Haven
			};

        private static Point3D[] m_IlshenarDeathDestinations = new Point3D[]
			{
				new Point3D( 1216,  468, -13 ),
				new Point3D(  723, 1367, -60 ),
				new Point3D(  745,  725, -28 ),
				new Point3D(  281, 1017,   0 ),
				new Point3D(  986, 1011, -32 ),
				new Point3D( 1175, 1287, -30 ),
				new Point3D( 1533, 1341,  -3 ),
				new Point3D(  529,  217, -44 ),
				new Point3D( 1722,  219,  96 )
			};

        private static Point3D[] m_MalasDeathDestinations = new Point3D[]
			{
				new Point3D( 2079, 1376, -70 ),
				new Point3D(  944,  519, -71 )
			};

        private static Point3D[] m_TokunoDeathDestinations = new Point3D[]
			{
				new Point3D( 1166,  801, 27 ),
				new Point3D(  782, 1228, 25 ),
				new Point3D(  268,  624, 15 )
			};

        public bool YoungDeathTeleport()
        {
            if (this.Region.IsPartOf(typeof(Jail))
                || this.Region.IsPartOf("Samurai start location")
                || this.Region.IsPartOf("Ninja start location")
                || this.Region.IsPartOf("Ninja cave"))
                return false;

            Point3D loc;
            Map map;

            DungeonRegion dungeon = (DungeonRegion)this.Region.GetRegion(typeof(DungeonRegion));
            if (dungeon != null && dungeon.EntranceLocation != Point3D.Zero)
            {
                loc = dungeon.EntranceLocation;
                map = dungeon.EntranceMap;
            }
            else
            {
                loc = this.Location;
                map = this.Map;
            }

            Point3D[] list;

            if (map == Map.Trammel)
                list = m_TrammelDeathDestinations;
            else if (map == Map.Ilshenar)
                list = m_IlshenarDeathDestinations;
            else if (map == Map.Malas)
                list = m_MalasDeathDestinations;
            else if (map == Map.Tokuno)
                list = m_TokunoDeathDestinations;
            else
                return false;

            Point3D dest = Point3D.Zero;
            int sqDistance = int.MaxValue;

            for (int i = 0; i < list.Length; i++)
            {
                Point3D curDest = list[i];

                int width = loc.X - curDest.X;
                int height = loc.Y - curDest.Y;
                int curSqDistance = width * width + height * height;

                if (curSqDistance < sqDistance)
                {
                    dest = curDest;
                    sqDistance = curSqDistance;
                }
            }

            this.MoveToWorld(dest, map);
            return true;
        }

        private void SendYoungDeathNotice()
        {
            this.SendGump(new YoungDeathNotice());
        }

        #endregion

        #region Speech log
        private SpeechLog m_SpeechLog;

        public SpeechLog SpeechLog { get { return m_SpeechLog; } }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (SpeechLog.Enabled && this.NetState != null)
            {
                if (m_SpeechLog == null)
                    m_SpeechLog = new SpeechLog();

                m_SpeechLog.Add(e.Mobile, e.Speech);
            }
        }

        #endregion

        #region Champion Titles
        [CommandProperty(AccessLevel.GameMaster)]
        public bool DisplayChampionTitle
        {
            get { return GetFlag(PlayerFlag.DisplayChampionTitle); }
            set { SetFlag(PlayerFlag.DisplayChampionTitle, value); }
        }

        private ChampionTitleInfo m_ChampionTitles;

        [CommandProperty(AccessLevel.GameMaster)]
        public ChampionTitleInfo ChampionTitles { get { return m_ChampionTitles; } set { } }

        private void ToggleChampionTitleDisplay()
        {
            if (!CheckAlive())
                return;

            if (DisplayChampionTitle)
                SendLocalizedMessage(1062419, "", 0x23); // You have chosen to hide your monster kill title.
            else
                SendLocalizedMessage(1062418, "", 0x23); // You have chosen to display your monster kill title.

            DisplayChampionTitle = !DisplayChampionTitle;
        }

        [PropertyObject]
        public class ChampionTitleInfo
        {
            public static TimeSpan LossDelay = TimeSpan.FromDays(1.0);
            public const int LossAmount = 90;

            private class TitleInfo
            {
                private int m_Value;
                private DateTime m_LastDecay;

                public int Value { get { return m_Value; } set { m_Value = value; } }
                public DateTime LastDecay { get { return m_LastDecay; } set { m_LastDecay = value; } }

                public TitleInfo()
                {
                }

                public TitleInfo(GenericReader reader)
                {
                    int version = reader.ReadEncodedInt();

                    switch (version)
                    {
                        case 0:
                            {
                                m_Value = reader.ReadEncodedInt();
                                m_LastDecay = reader.ReadDateTime();
                                break;
                            }
                    }
                }

                public static void Serialize(GenericWriter writer, TitleInfo info)
                {
                    writer.WriteEncodedInt((int)0); // version

                    writer.WriteEncodedInt(info.m_Value);
                    writer.Write(info.m_LastDecay);
                }
			}

            private TitleInfo[] m_Values;

            private int m_Harrower;	//Harrower titles do NOT decay

            public int GetValue(ChampionSpawnType type)
            {
                return GetValue((int)type);
            }

            public void SetValue(ChampionSpawnType type, int value)
            {
                SetValue((int)type, value);
            }

            public void Award(ChampionSpawnType type, int value)
            {
                Award((int)type, value);
            }

            public int GetValue(int index)
            {
                if (m_Values == null || index < 0 || index >= m_Values.Length)
                    return 0;

                if (m_Values[index] == null)
                    m_Values[index] = new TitleInfo();

                return m_Values[index].Value;
            }

            public DateTime GetLastDecay(int index)
            {
                if (m_Values == null || index < 0 || index >= m_Values.Length)
                    return DateTime.MinValue;

                if (m_Values[index] == null)
                    m_Values[index] = new TitleInfo();

                return m_Values[index].LastDecay;
            }

            public void SetValue(int index, int value)
            {
                if (m_Values == null)
                    m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

                if (value < 0)
                    value = 0;

                if (index < 0 || index >= m_Values.Length)
                    return;

                if (m_Values[index] == null)
                    m_Values[index] = new TitleInfo();

                m_Values[index].Value = value;
            }

            public void Award(int index, int value)
            {
                if (m_Values == null)
                    m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

                if (index < 0 || index >= m_Values.Length || value <= 0)
                    return;

                if (m_Values[index] == null)
                    m_Values[index] = new TitleInfo();

                m_Values[index].Value += value;
            }

            public void Atrophy(int index, int value)
            {
                if (m_Values == null)
                    m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

                if (index < 0 || index >= m_Values.Length || value <= 0)
                    return;

                if (m_Values[index] == null)
                    m_Values[index] = new TitleInfo();

                int before = m_Values[index].Value;

                if ((m_Values[index].Value - value) < 0)
                    m_Values[index].Value = 0;
                else
                    m_Values[index].Value -= value;

                if (before != m_Values[index].Value)
                    m_Values[index].LastDecay = DateTime.Now;
            }

            public override string ToString()
            {
                return "...";
            }

            [CommandProperty(AccessLevel.GameMaster)]
			public int Pestilence { get { return GetValue(ChampionSpawnType.Pestilence); } set { SetValue(ChampionSpawnType.Pestilence, value); } }

			[CommandProperty( AccessLevel.GameMaster )]
            public int Abyss { get { return GetValue(ChampionSpawnType.Abyss); } set { SetValue(ChampionSpawnType.Abyss, value); } }

            [CommandProperty(AccessLevel.GameMaster)]
            public int Arachnid { get { return GetValue(ChampionSpawnType.Arachnid); } set { SetValue(ChampionSpawnType.Arachnid, value); } }

            [CommandProperty(AccessLevel.GameMaster)]
            public int ColdBlood { get { return GetValue(ChampionSpawnType.ColdBlood); } set { SetValue(ChampionSpawnType.ColdBlood, value); } }

            [CommandProperty(AccessLevel.GameMaster)]
            public int ForestLord { get { return GetValue(ChampionSpawnType.ForestLord); } set { SetValue(ChampionSpawnType.ForestLord, value); } }

            [CommandProperty(AccessLevel.GameMaster)]
            public int SleepingDragon { get { return GetValue(ChampionSpawnType.SleepingDragon); } set { SetValue(ChampionSpawnType.SleepingDragon, value); } }

            [CommandProperty(AccessLevel.GameMaster)]
            public int UnholyTerror { get { return GetValue(ChampionSpawnType.UnholyTerror); } set { SetValue(ChampionSpawnType.UnholyTerror, value); } }

            [CommandProperty(AccessLevel.GameMaster)]
            public int VerminHorde { get { return GetValue(ChampionSpawnType.VerminHorde); } set { SetValue(ChampionSpawnType.VerminHorde, value); } }

            [CommandProperty(AccessLevel.GameMaster)]
            public int Harrower { get { return m_Harrower; } set { m_Harrower = value; } }

            #region Mondain's Legacy Peerless Champion
            [CommandProperty(AccessLevel.GameMaster)]
            public int Glade { get { return GetValue(ChampionSpawnType.Glade); } set { SetValue(ChampionSpawnType.Glade, value); } }

            [CommandProperty(AccessLevel.GameMaster)]
            public int Corrupt { get { return GetValue(ChampionSpawnType.Corrupt); } set { SetValue(ChampionSpawnType.Corrupt, value); } }
            #endregion

            public ChampionTitleInfo()
            {
            }

            public ChampionTitleInfo(GenericReader reader)
            {
                int version = reader.ReadEncodedInt();

                switch (version)
                {
                    case 0:
                        {
                            m_Harrower = reader.ReadEncodedInt();

                            int length = reader.ReadEncodedInt();
                            m_Values = new TitleInfo[length];

                            for (int i = 0; i < length; i++)
                            {
                                m_Values[i] = new TitleInfo(reader);
                            }

                            if (m_Values.Length != ChampionSpawnInfo.Table.Length)
                            {
                                TitleInfo[] oldValues = m_Values;
                                m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

                                for (int i = 0; i < m_Values.Length && i < oldValues.Length; i++)
                                {
                                    m_Values[i] = oldValues[i];
                                }
                            }
                            break;
                        }
                }
            }

            public static void Serialize(GenericWriter writer, ChampionTitleInfo titles)
            {
                writer.WriteEncodedInt((int)0); // version

                writer.WriteEncodedInt(titles.m_Harrower);

                int length = titles.m_Values.Length;
                writer.WriteEncodedInt(length);

                for (int i = 0; i < length; i++)
                {
                    if (titles.m_Values[i] == null)
                        titles.m_Values[i] = new TitleInfo();

                    TitleInfo.Serialize(writer, titles.m_Values[i]);
                }
            }

            public static void CheckAtrophy(PlayerMobile pm)
            {
                ChampionTitleInfo t = pm.m_ChampionTitles;
                if (t == null)
                    return;

                if (t.m_Values == null)
                    t.m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

                for (int i = 0; i < t.m_Values.Length; i++)
                {
                    if ((t.GetLastDecay(i) + LossDelay) < DateTime.Now)
                    {
                        t.Atrophy(i, LossAmount);
                    }
                }
            }

            public static void AwardHarrowerTitle(PlayerMobile pm)	//Called when killing a harrower.  Will give a minimum of 1 point.
            {
                ChampionTitleInfo t = pm.m_ChampionTitles;
                if (t == null)
                    return;

                if (t.m_Values == null)
                    t.m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

                int count = 1;

                for (int i = 0; i < t.m_Values.Length; i++)
                {
                    if (t.m_Values[i].Value > 900)
                        count++;
                }

                t.m_Harrower = Math.Max(count, t.m_Harrower);	//Harrower titles never decay.
            }
        }

        #endregion

        #region Recipes

        private Dictionary<int, bool> m_AcquiredRecipes;

        public virtual bool HasRecipe(Recipe r)
        {
            if (r == null)
                return false;

            return HasRecipe(r.ID);
        }

        public virtual bool HasRecipe(int recipeID)
        {
            if (m_AcquiredRecipes != null && m_AcquiredRecipes.ContainsKey(recipeID))
                return m_AcquiredRecipes[recipeID];

            return false;
        }

        public virtual void AcquireRecipe(Recipe r)
        {
            if (r != null)
                AcquireRecipe(r.ID);
        }

        public virtual void AcquireRecipe(int recipeID)
        {
            if (m_AcquiredRecipes == null)
                m_AcquiredRecipes = new Dictionary<int, bool>();

            m_AcquiredRecipes[recipeID] = true;
        }

        public virtual void ResetRecipes()
        {
            m_AcquiredRecipes = null;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int KnownRecipes
        {
            get
            {
                if (m_AcquiredRecipes == null)
                    return 0;

                return m_AcquiredRecipes.Count;
            }
        }

        #endregion

        #region Buff Icons

        public void ResendBuffs()
        {
            if (!BuffInfo.Enabled || m_BuffTable == null)
                return;

            NetState state = this.NetState;

			if( state != null && state.BuffIcon )
            {
                foreach (BuffInfo info in m_BuffTable.Values)
                {
                    state.Send(new AddBuffPacket(this, info));
                }
            }
        }

        private Dictionary<BuffIcon, BuffInfo> m_BuffTable;

        public void AddBuff(BuffInfo b)
        {
            if (!BuffInfo.Enabled || b == null)
                return;

            RemoveBuff(b);	//Check & subsequently remove the old one.

            if (m_BuffTable == null)
                m_BuffTable = new Dictionary<BuffIcon, BuffInfo>();

            m_BuffTable.Add(b.ID, b);

            NetState state = this.NetState;

			if( state != null && state.BuffIcon )
            {
                state.Send(new AddBuffPacket(this, b));
            }
        }

        public void RemoveBuff(BuffInfo b)
        {
            if (b == null)
                return;

            RemoveBuff(b.ID);
        }

        public void RemoveBuff(BuffIcon b)
        {
            if (m_BuffTable == null || !m_BuffTable.ContainsKey(b))
                return;

            BuffInfo info = m_BuffTable[b];

            if (info.Timer != null && info.Timer.Running)
                info.Timer.Stop();

            m_BuffTable.Remove(b);

            NetState state = this.NetState;

			if( state != null && state.BuffIcon )
            {
                state.Send(new RemoveBuffPacket(this, b));
            }

            if (m_BuffTable.Count <= 0)
                m_BuffTable = null;
        }

        #endregion
        #region XML PVP Dismount Pet     
        public void DismountAndStable()
        {
            BaseCreature bc = Mount as BaseCreature;
 
            if (Mount != null)
            {
                Mount.Rider = null;
            }
            
			if (bc != null)
			{
                bc.ControlTarget = null;
                bc.ControlOrder = OrderType.Stay;
                bc.Internalize();
                bc.SetControlMaster(null);
                bc.SummonMaster = null;
                bc.IsStabled = true;
 
                Stabled.Add(bc);
                m_AutoStabled.Add(bc);
 
                this.SendMessage("Your Mount has been Stabled !."); 
			}
        } 
 
        public void RetrivePet()
        {   
            if (m_AutoStabled.Count < 1)
                 return;
 
            for ( int k = 0; k < m_AutoStabled.Count; ++k )
            {
				BaseCreature bc = (BaseCreature)m_AutoStabled[k];
 
				if ( Stabled.Contains( bc ) )
				{
					bc.ControlTarget = null;
					bc.ControlOrder = OrderType.Follow;
					bc.SetControlMaster(this);
					bc.SummonMaster = null;
	 
					if (bc.Summoned)
						bc.SummonMaster = this;
						bc.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully happy
						bc.MoveToWorld(Location, Map);
						bc.IsStabled = false;
						if (m_AutoStabled.Contains(bc))
							m_AutoStabled.Remove(bc);
	 
						this.SendMessage("Your Mount return to You !.");
				}                       
            }   
            m_AutoStabled.Clear();                 
        } 
        #endregion
        public void AutoStablePets()
        {
            if (Core.SE && AllFollowers.Count > 0)
            {
                for (int i = m_AllFollowers.Count - 1; i >= 0; --i)
                {
                    BaseCreature pet = AllFollowers[i] as BaseCreature;

					if (pet == null || pet.ControlMaster == null)
                        continue;

					if (pet.Summoned)
					{
						if (pet.Map != Map)
						{
							pet.PlaySound(pet.GetAngerSound());
							Timer.DelayCall(TimeSpan.Zero, new TimerCallback(pet.Delete));
						}
						continue;
					}

                    if (pet is IMount && ((IMount)pet).Rider != null)
                        continue;

                    if ((pet is PackLlama || pet is PackHorse || pet is Beetle || pet is HordeMinionFamiliar) && (pet.Backpack != null && pet.Backpack.Items.Count > 0))
                        continue;

                    if (pet is BaseEscortable)
                        continue;

                    pet.ControlTarget = null;
                    pet.ControlOrder = OrderType.Stay;
                    pet.Internalize();

                    pet.SetControlMaster(null);
                    pet.SummonMaster = null;

                    pet.IsStabled = true;

                    pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully happy

                    Stabled.Add(pet);
                    m_AutoStabled.Add(pet);
                }
            }
        }

        public void ClaimAutoStabledPets()
        {
            if (!Core.SE || m_AutoStabled.Count <= 0)
                return;

            if (!Alive)
            {
                SendLocalizedMessage(1076251); // Your pet was unable to join you while you are a ghost.  Please re-login once you have ressurected to claim your pets.				
                return;
            }

            for (int i = m_AutoStabled.Count - 1; i >= 0; --i)
            {
                BaseCreature pet = m_AutoStabled[i] as BaseCreature;

                if (pet == null || pet.Deleted)
                {
                    pet.IsStabled = false;

                    if (Stabled.Contains(pet))
                        Stabled.Remove(pet);

                    continue;
                }

                if ((Followers + pet.ControlSlots) <= FollowersMax)
                {
                    pet.SetControlMaster(this);

                    if (pet.Summoned)
                        pet.SummonMaster = this;

                    pet.ControlTarget = this;
                    pet.ControlOrder = OrderType.Follow;

                    pet.MoveToWorld(Location, Map);

                    pet.IsStabled = false;

                    pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully Happy

                    if (Stabled.Contains(pet))
                        Stabled.Remove(pet);
                }
                else
                {
					SendLocalizedMessage(1049612, pet.Name); // ~1_NAME~ remained in the stables because you have too many followers.
                }
            }

            m_AutoStabled.Clear();
        }
    }
	#endregion
}