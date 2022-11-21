using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Factions;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
   	public class ShrinkItemX : Item
   	{
		private bool m_IsDeed = false;// Set True for all shrunken pets to be in deed form.
		private bool m_Disabled = false; //Set True to disable all newly made shrink items. (Use [shrinklockdown to disable all current shrinkitems)
		private BaseCreature m_Pet;
		private int m_PetHue = 0;
		private bool m_PetBonded;
		private Mobile m_PetOwner;
		private bool m_IsFemale;

		private string m_PetTitle;

		private Faction m_Faction;

		private DateTime m_BondingBegin;

		private int m_AbilityPoints;

		private bool m_BardingExceptional;
		private Mobile m_BardingCrafter;
		private int m_BardingHP;
		private bool m_HasBarding;
		private CraftResource m_BardingResource;

		private DateTime m_SheepWool;

		private int m_Exp;
		private int m_NextLevel;
		private int m_Level;
		private int m_MaxLevel;

		private bool m_AllowMating;

		private bool m_Evolves;
		private int m_Gen;

		private DateTime m_MatingDelay;

		private int m_Form1;
		private int m_Form2;
		private int m_Form3;
		private int m_Form4;
		private int m_Form5;
		private int m_Form6;
		private int m_Form7;
		private int m_Form8;
		private int m_Form9;

		private int m_Sound1;
		private int m_Sound2;
		private int m_Sound3;
		private int m_Sound4;
		private int m_Sound5;
		private int m_Sound6;
		private int m_Sound7;
		private int m_Sound8;
		private int m_Sound9;

		private bool m_UsesForm1;
		private bool m_UsesForm2;
		private bool m_UsesForm3;
		private bool m_UsesForm4;
		private bool m_UsesForm5;
		private bool m_UsesForm6;
		private bool m_UsesForm7;
		private bool m_UsesForm8;
		private bool m_UsesForm9;

		public bool m_F0;
		public bool m_F1;
		public bool m_F2;
		public bool m_F3;
		public bool m_F4;
		public bool m_F5;
		public bool m_F6;
		public bool m_F7;
		public bool m_F8;
		public bool m_F9;

		private int m_PetHitsNow;
		private int m_PetStamNow;
		private int m_PetManaNow;

		private int m_PetKP;
		private int m_PetStage;
		private bool m_PetHasEgg;
		private bool m_PetAllowMating;
		private bool m_PetPregnant;
		private bool m_PetS1;
		private bool m_PetS2;
		private bool m_PetS3;
		private bool m_PetS4;
		private bool m_PetS5;
		private bool m_PetS6;

		private int m_MountID;

		private Type m_PetType;
		private String m_PetTypeString;

		private bool m_Locked;
		private Double m_PetMinTame;
		private int m_PetControlSlots;
		private string m_PetName;

		private int m_PetMinDamage;
		private int m_PetMaxDamage;

		private int m_PetBody;
		private int m_PetSound;
		private int m_PetVArmor;

		private int m_PetStr;
		private int m_PetDex;
		private int m_PetInt;

		private int m_PetHits;
		private int m_PetStam;
		private int m_PetMana;

		private int m_PetPhysicalResist;
		private int m_PetColdResist;
		private int m_PetFireResist;
		private int m_PetPoisonResist;
		private int m_PetEnergyResist;

		private int m_PetPhysicalDamage;
		private int m_PetColdDamage;
		private int m_PetFireDamage;
		private int m_PetPoisonDamage;
		private int m_PetEnergyDamage;

		private Double m_PetWrestling;
		private Double m_PetTactics;
		private Double m_PetResist;
		private Double m_PetAnatomy;
		private Double m_PetPoisoning;
		private Double m_PetMagery;
		private Double m_PetEvalInt;
		private Double m_PetMed;

		private Double m_CapWrestling;
		private Double m_CapTactics;
		private Double m_CapResist;
		private Double m_CapAnatomy;
		private Double m_CapPoisoning;
		private Double m_CapMagery;
		private Double m_CapEvalInt;
		private Double m_CapMed;

		[CommandProperty( AccessLevel.Administrator )]
		public bool Disabled
		{
			get{ return m_Disabled; }
			set{ m_Disabled = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsDeed
		{
			get{ return m_IsDeed; }
			set
			{ 
				m_IsDeed = value;
				if ( m_IsDeed == true )
				{
					if ( m_Pet != null )
					{
						ItemID =  0x14EF;
						Name = "a pet deed";
					}
					else
					{
						ItemID = 0xFAA;
						Name = "Unlinked Shrink Item!";
					}
				}
				else
				{
					if ( m_Pet != null )
					{
						ItemID =  ShrinkTable.Lookup( m_Pet ); 
						Name = m_PetTypeString;
					}
					else
					{
						ItemID = 0xFAA;
						Name = "Unlinked Shrink Item!";
					}
				}
			}	
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public BaseCreature Pet
		{
			get{ return m_Pet; }
			set
			{ 
				m_Pet = value;
				if ( m_IsDeed != true )
				{
					ItemID =  ShrinkTable.Lookup( m_Pet ); 
					Name = m_PetTypeString;
				}
				else
				{
					ItemID =  0x14EF;
					Name = "a pet deed";
				}

				if ( m_Pet is SwampDragon )
				{
					SwampDragon sd = (SwampDragon)m_Pet;
					m_BardingExceptional = sd.BardingExceptional;
					m_BardingCrafter = sd.BardingCrafter;
					m_BardingHP = sd.BardingHP;
					m_HasBarding = sd.HasBarding;
					m_BardingResource = sd.BardingResource;
				}
				if ( m_Pet is FireSteed )
				{
					FireSteed fs = (FireSteed)m_Pet;
					m_BardingExceptional = fs.BardingExceptional;
					m_BardingCrafter = fs.BardingCrafter;
					m_BardingHP = fs.BardingHP;
					m_HasBarding = fs.HasBarding;
					m_BardingResource = fs.BardingResource;
				}
			
				if ( m_Pet is Sheep )
				{
					Sheep s = (Sheep)m_Pet;
					m_SheepWool = s.NextWoolTime;
				}

				if ( m_Pet is FactionWarHorse )
				{
					FactionWarHorse fwh = (FactionWarHorse)m_Pet;
					m_Faction = fwh.Faction;
				}

				if ( m_Pet is BaseMount )
				{	
					BaseMount mount = (BaseMount)m_Pet;
					m_MountID = mount.ItemID;
				}

				m_PetTitle = m_Pet.Title;
		
				m_BondingBegin = m_Pet.BondingBegin;

				m_Exp = m_Pet.Exp;
				m_NextLevel = m_Pet.NextLevel;
				m_Level = m_Pet.Level;
				m_MaxLevel = m_Pet.MaxLevel;
				m_AllowMating = m_Pet.AllowMating;
				m_Evolves = m_Pet.Evolves;
				m_Gen = m_Pet.Generation;
				this.MatingDelay = m_Pet.MatingDelay;
				m_Form1 = m_Pet.Form1;
				m_Form2 = m_Pet.Form2;
				m_Form3 = m_Pet.Form3;
				m_Form4 = m_Pet.Form4;
				m_Form5 = m_Pet.Form5;
				m_Form6 = m_Pet.Form6;
				m_Form7 = m_Pet.Form7;
				m_Form8 = m_Pet.Form8;
				m_Form9 = m_Pet.Form9;
				m_Sound1 = m_Pet.Sound1;
				m_Sound2 = m_Pet.Sound2;
				m_Sound3 = m_Pet.Sound3;
				m_Sound4 = m_Pet.Sound4;
				m_Sound5 = m_Pet.Sound5;
				m_Sound6 = m_Pet.Sound6;
				m_Sound7 = m_Pet.Sound7;
				m_Sound8 = m_Pet.Sound8;
				m_Sound9 = m_Pet.Sound9;
				m_UsesForm1 = m_Pet.UsesForm1;
				m_UsesForm2 = m_Pet.UsesForm2;
				m_UsesForm3 = m_Pet.UsesForm3;
				m_UsesForm4 = m_Pet.UsesForm4;
				m_UsesForm5 = m_Pet.UsesForm5;
				m_UsesForm6 = m_Pet.UsesForm6;
				m_UsesForm7 = m_Pet.UsesForm7;
				m_UsesForm8 = m_Pet.UsesForm8;
				m_UsesForm9 = m_Pet.UsesForm9;
				m_F0 = m_Pet.F0;
				m_F1 = m_Pet.F1;
				m_F2 = m_Pet.F2;
				m_F3 = m_Pet.F3;
				m_F4 = m_Pet.F4;
				m_F5 = m_Pet.F5;
				m_F6 = m_Pet.F6;
				m_F7 = m_Pet.F7;
				m_F8 = m_Pet.F8;
				m_F9 = m_Pet.F9;

				m_AbilityPoints = m_Pet.AbilityPoints;

				m_PetHitsNow = m_Pet.Hits;
				m_PetStamNow = m_Pet.Stam;
				m_PetManaNow = m_Pet.Mana;

				m_PetName = m_Pet.Name;
				m_PetHue = m_Pet.Hue;
				this.Hue = m_PetHue;
				m_PetBody = m_Pet.BodyValue;
				m_PetSound = m_Pet.BaseSoundID;
				m_PetVArmor = m_Pet.VirtualArmor;
				m_PetMinTame = m_Pet.MinTameSkill;
				m_PetControlSlots = m_Pet.ControlSlots;

				m_IsFemale = m_Pet.Female;

				m_PetMinDamage = m_Pet.DamageMin;
				m_PetMaxDamage = m_Pet.DamageMax;

				if ( m_Pet.IsBonded == true )
					m_PetBonded = true;
				else
					m_PetBonded = false;

				m_PetStr = m_Pet.RawStr;
				m_PetDex = m_Pet.RawDex;
				m_PetInt = m_Pet.RawInt;

				m_PetHits = m_Pet.HitsMax;
				m_PetStam = m_Pet.StamMax;
				m_PetMana = m_Pet.ManaMax;

				if ( m_Pet.PhysicalResistance <= m_Pet.PhysicalResistanceSeed )
					m_PetPhysicalResist = m_Pet.PhysicalResistanceSeed;
				else
					m_PetPhysicalResist = m_Pet.PhysicalResistance;

				if ( m_Pet.ColdResistance <= m_Pet.ColdResistSeed )
					m_PetColdResist = m_Pet.ColdResistSeed;
				else
					m_PetColdResist = m_Pet.ColdResistance;

				if ( m_Pet.FireResistance <= m_Pet.FireResistSeed )
					m_PetFireResist = m_Pet.FireResistSeed;
				else
					m_PetFireResist = m_Pet.FireResistance;

				if ( m_Pet.PoisonResistance <= m_Pet.PoisonResistSeed )
					m_PetPoisonResist = m_Pet.PoisonResistSeed;
				else
					m_PetPoisonResist = m_Pet.PoisonResistance;

				if ( m_Pet.EnergyResistance <= m_Pet.EnergyResistSeed )
					m_PetEnergyResist = m_Pet.EnergyResistSeed;
				else
					m_PetEnergyResist = m_Pet.EnergyResistance;

				m_PetPhysicalDamage = m_Pet.PhysicalDamage;
				m_PetColdDamage = m_Pet.ColdDamage;
				m_PetFireDamage = m_Pet.FireDamage;
				m_PetPoisonDamage = m_Pet.PoisonDamage;
				m_PetEnergyDamage = m_Pet.EnergyDamage;

				m_PetWrestling = m_Pet.Skills[SkillName.Wrestling].Base;
				m_PetTactics = m_Pet.Skills[SkillName.Tactics].Base;
				m_PetResist = m_Pet.Skills[SkillName.MagicResist].Base;
				m_PetAnatomy = m_Pet.Skills[SkillName.Anatomy].Base;
				m_PetPoisoning = m_Pet.Skills[SkillName.Poisoning].Base;
				m_PetMagery= m_Pet.Skills[SkillName.Magery].Base;
				m_PetEvalInt = m_Pet.Skills[SkillName.EvalInt].Base;
				m_PetMed = m_Pet.Skills[SkillName.Meditation].Base;

				m_CapWrestling = m_Pet.Skills[SkillName.Wrestling].Cap;
				m_CapTactics = m_Pet.Skills[SkillName.Tactics].Cap;
				m_CapResist = m_Pet.Skills[SkillName.MagicResist].Cap;
				m_CapAnatomy = m_Pet.Skills[SkillName.Anatomy].Cap;
				m_CapPoisoning = m_Pet.Skills[SkillName.Poisoning].Cap;
				m_CapMagery= m_Pet.Skills[SkillName.Magery].Cap;
				m_CapEvalInt = m_Pet.Skills[SkillName.EvalInt].Cap;
				m_CapMed = m_Pet.Skills[SkillName.Meditation].Cap;

			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PetHue
		{
			get{ return m_PetHue; }
			set
			{ 
				m_PetHue = value;
				Hue = m_PetHue; 
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Type MobType
		{
			get{ return m_PetType; }
			set
			{ 
				m_PetType = value;
				m_PetTypeString = m_PetType.Name; 
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public String MobTypeString
		{
			get{ return m_PetTypeString; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PetBonded
		{
			get{ return m_PetBonded; }
			set{ m_PetBonded = value; }	
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile PetOwner
		{
			get{ return m_PetOwner; }
			set{ m_PetOwner = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Lock
		{
			get{ return m_Locked; }
			set{ m_Locked = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PetBodyValue
		{
			get{ return m_PetBody; }
			set{ m_PetBody = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PetSoundID
		{
			get{ return m_PetSound; }
			set{ m_PetSound = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PetVirtualArmor
		{
			get{ return m_PetVArmor; }
			set{ m_PetVArmor = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Double PetMinTame
		{
			get{ return m_PetMinTame; }
			set{ m_PetMinTame = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int StatStr
		{
			get{ return m_PetStr; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int StatDex
		{
			get{ return m_PetDex; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int StatInt
		{
			get{ return m_PetInt; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int StatHits
		{
			get{ return m_PetHits; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int StatStam
		{
			get{ return m_PetStam; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int StatMana
		{
			get{ return m_PetMana; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int ResistPhysical
		{
			get{ return m_PetPhysicalResist; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int ResistCold
		{
			get{ return m_PetColdResist; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int ResistFire
		{
			get{ return m_PetFireResist; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int ResistPoison
		{
			get{ return m_PetPoisonResist; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int ResistEnergy
		{
			get{ return m_PetEnergyResist; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int DamageTypePhysical
		{
			get{ return m_PetPhysicalDamage; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int DamageTypeCold
		{
			get{ return m_PetColdDamage; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int DamageTypeFire
		{
			get{ return m_PetFireDamage; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int DamageTypePoison
		{
			get{ return m_PetPoisonDamage; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int DamageTypeEnergy
		{
			get{ return m_PetEnergyDamage; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Double SkillWrestling
		{
			get{ return m_PetWrestling; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Double SkillTactics
		{
			get{ return m_PetTactics; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Double SkillMagicResist
		{
			get{ return m_PetResist; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Double SkillAnatomy
		{
			get{ return m_PetAnatomy; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Double SkillPoisoning
		{
			get{ return m_PetPoisoning; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Double SkillMagery
		{
			get{ return m_PetMagery; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Double SkillEvalInt
		{
			get{ return m_PetEvalInt; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Double SkillMeditation
		{
			get{ return m_PetMed; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PetMinDamage
		{
			get{ return m_PetMinDamage; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PetMaxDamage
		{
			get{ return m_PetMaxDamage; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string PetName
		{
			get{ return m_PetName; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PetKP
		{
			get{ return m_PetKP; }
			set{ m_PetKP = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PetStage
		{
			get{ return m_PetStage; }
			set{ m_PetStage = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PetHasEgg
		{
			get{ return m_PetHasEgg; }
			set{ m_PetHasEgg = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PetPregnant
		{
			get{ return m_PetPregnant; }
			set{ m_PetPregnant = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PetAllowMating
		{
			get{ return m_PetAllowMating; }
			set{ m_PetAllowMating = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MountID
		{
			get{ return m_MountID; }
			set{ m_MountID = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PetHitsNow
		{
			get{ return m_PetHitsNow; }
			set{ m_PetHitsNow = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PetStamNow
		{
			get{ return m_PetStamNow; }
			set{ m_PetStamNow = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PetManaNow
		{
			get{ return m_PetManaNow; }
			set{ m_PetManaNow = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PetS1
		{
			get{ return m_PetS1; }
			set{ m_PetS1 = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PetS2
		{
			get{ return m_PetS2; }
			set{ m_PetS2 = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PetS3
		{
			get{ return m_PetS3; }
			set{ m_PetS3 = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PetS4
		{
			get{ return m_PetS4; }
			set{ m_PetS4 = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PetS5
		{
			get{ return m_PetS5; }
			set{ m_PetS5 = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PetS6
		{
			get{ return m_PetS6; }
			set{ m_PetS6 = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile BardingCrafter
		{
			get{ return m_BardingCrafter; }
			set{ m_BardingCrafter = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool BardingExceptional
		{
			get{ return m_BardingExceptional; }
			set{ m_BardingExceptional = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int BardingHP
		{
			get{ return m_BardingHP; }
			set{ m_BardingHP = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool HasBarding
		{
			get{ return m_HasBarding; }
			set{ m_HasBarding = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource BardingResource
		{
			get{ return m_BardingResource; }
			set{ m_BardingResource = value; InvalidateProperties(); }
		}

		public DateTime SheepWool
		{
			get{ return m_SheepWool; }
			set{ m_SheepWool = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public DateTime MatingDelay
		{
			get{ return m_MatingDelay; }
			set{ m_MatingDelay = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Generation
		{
			get{ return m_Gen; }
			set{ m_Gen = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Exp
		{
			get{ return m_Exp; }
			set{ m_Exp = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int NextLevel
		{
			get{ return m_NextLevel; }
			set{ m_NextLevel = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxLevel
		{
			get{ return m_MaxLevel; }
			set{ m_MaxLevel = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Evolves
		{
			get{ return m_Evolves; }
			set{ m_Evolves = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowMating
		{
			get{ return m_AllowMating; }
			set{ m_AllowMating = value; }
		}

		public int Form1
		{
			get{ return m_Form1; }
			set{ m_Form1 = value; }
		}

		public int Form2
		{
			get{ return m_Form2; }
			set{ m_Form2 = value; }
		}

		public int Form3
		{
			get{ return m_Form3; }
			set{ m_Form3 = value; }
		}

		public int Form4
		{
			get{ return m_Form4; }
			set{ m_Form4 = value; }
		}

		public int Form5
		{
			get{ return m_Form5; }
			set{ m_Form5 = value; }
		}

		public int Form6
		{
			get{ return m_Form6; }
			set{ m_Form6 = value; }
		}

		public int Form7
		{
			get{ return m_Form7; }
			set{ m_Form7 = value; }
		}

		public int Form8
		{
			get{ return m_Form8; }
			set{ m_Form8 = value; }
		}

		public int Form9
		{
			get{ return m_Form9; }
			set{ m_Form9 = value; }
		}

		public int Sound1
		{
			get{ return m_Sound1; }
			set{ m_Sound1 = value; }
		}

		public int Sound2
		{
			get{ return m_Sound2; }
			set{ m_Sound2 = value; }
		}

		public int Sound3
		{
			get{ return m_Sound3; }
			set{ m_Sound3 = value; }
		}

		public int Sound4
		{
			get{ return m_Sound4; }
			set{ m_Sound4 = value; }
		}

		public int Sound5
		{
			get{ return m_Sound5; }
			set{ m_Sound5 = value; }
		}

		public int Sound6
		{
			get{ return m_Sound6; }
			set{ m_Sound6 = value; }
		}

		public int Sound7
		{
			get{ return m_Sound7; }
			set{ m_Sound7 = value; }
		}

		public int Sound8
		{
			get{ return m_Sound8; }
			set{ m_Sound8 = value; }
		}

		public int Sound9
		{
			get{ return m_Sound9; }
			set{ m_Sound9 = value; }
		}

		public bool UsesForm1
		{
			get{ return m_UsesForm1; }
			set{ m_UsesForm1 = value; }
		}

		public bool UsesForm2
		{
			get{ return m_UsesForm2; }
			set{ m_UsesForm2 = value; }
		}

		public bool UsesForm3
		{
			get{ return m_UsesForm3; }
			set{ m_UsesForm3 = value; }
		}

		public bool UsesForm4
		{
			get{ return m_UsesForm4; }
			set{ m_UsesForm4 = value; }
		}

		public bool UsesForm5
		{
			get{ return m_UsesForm5; }
			set{ m_UsesForm5 = value; }
		}

		public bool UsesForm6
		{
			get{ return m_UsesForm6; }
			set{ m_UsesForm6 = value; }
		}

		public bool UsesForm7
		{
			get{ return m_UsesForm7; }
			set{ m_UsesForm7 = value; }
		}

		public bool UsesForm8
		{
			get{ return m_UsesForm8; }
			set{ m_UsesForm8 = value; }
		}

		public bool UsesForm9
		{
			get{ return m_UsesForm9; }
			set{ m_UsesForm9 = value; }
		}

		public bool F0
		{
			get{ return m_F0; }
			set{ m_F0 = value; }
		}

		public bool F1
		{
			get{ return m_F1; }
			set{ m_F1 = value; }
		}

		public bool F2
		{
			get{ return m_F2; }
			set{ m_F2 = value; }
		}

		public bool F3
		{
			get{ return m_F3; }
			set{ m_F3 = value; }
		}

		public bool F4
		{
			get{ return m_F4; }
			set{ m_F4 = value; }
		}

		public bool F5
		{
			get{ return m_F5; }
			set{ m_F5 = value; }
		}

		public bool F6
		{
			get{ return m_F6; }
			set{ m_F6 = value; }
		}

		public bool F7
		{
			get{ return m_F7; }
			set{ m_F7 = value; }
		}

		public bool F8
		{
			get{ return m_F8; }
			set{ m_F8 = value; }
		}

		public bool F9
		{
			get{ return m_F9; }
			set{ m_F9 = value; }
		}

		public int AbilityPoints
		{
			get{ return m_AbilityPoints; }
			set{ m_AbilityPoints = value; }
		}

		public DateTime BondingBegin
		{
			get{ return m_BondingBegin; }
			set{ m_BondingBegin = value; }
		}

		public Faction Factions
		{
			get{ return m_Faction; }
			set{ m_Faction = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public String PetTitle
		{
			get{ return m_PetTitle; }
			set{ m_PetTitle = value; }
		}

      		[Constructable]
      		public ShrinkItemX() : base()
      		{
			if ( m_Pet != null )
			{
				if ( m_IsDeed != false )
				{
					ItemID = 0x14EF;
					Name = "a pet deed";
				}
				else
				{
					ItemID = ShrinkTable.Lookup( m_Pet );
					Name = "a shrunken pet";
				}

         			Movable = true;
         			LootType=LootType.Blessed;
				Hue = m_PetHue;
			}
			else
			{
				ItemID = 0xFAA;
         			Movable = true;
         			Name = "Unlinked Shrink Item!";
         			LootType=LootType.Blessed;
			}

			Weight = 25.0;
      		}

      		public override void OnDoubleClick( Mobile from )
      		{

			bool notame = false;

			foreach ( string check in FSATS.NoTamingNeeded )
			{
  				if ( check == m_PetTypeString )
    					notame = true;
			}

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( m_PetTypeString == null || m_PetOwner == null || this.ItemID == 0xFAA )
			{
				from.SendMessage( 38, "You have encountered an error trying to reclaim your pet, Please contact a gamemaster and tell them about this error, It could still be possible to reclaim your pet." );
			}
			else if ( m_Locked != false && m_PetOwner != from )
			{
				from.SendMessage( "This is locked and only the owner can claim this pet while locked." );
				from.SendMessage( "This item is now being returned to its owner." );
				m_PetOwner.AddToBackpack( this );
				m_PetOwner.SendMessage( "You pet {0} has been returned to you because it was locked and {1} was trying to claim the pet.", m_PetTypeString, from.Name );
			}
			else if ( from.Skills[SkillName.AnimalTaming].Value < m_PetMinTame && notame != true )
			{
				from.SendMessage( "You do not have the required taming to control this pet.");
				from.SendMessage( "You must have {0} animal taming to reclaim this pet.", m_PetMinTame );
			}
			else if ( from.Followers + m_PetControlSlots > from.FollowersMax )
			{
				from.SendMessage( "You have to many followers to claim this pet." );
			}
			else if ( Server.Spells.SpellHelper.CheckCombat( from ) )
			{
				from.SendMessage( "You cannot reclaim your pet while your fighting." );
			}
			else if ( m_Disabled == true )
			{
				from.SendMessage( 54, "The server is on a shrinkitem lockdown. You cannot unshrink your pet at this time." );
			}
			else if ( from is PlayerMobile && m_PetTypeString != null && m_PetOwner != null )
			{
				if ( m_Pet != null )
				{
					IEntity p1a = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z ), from.Map );
					IEntity p2a = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z + 50 ), from.Map );

					Effects.SendMovingParticles( p1a, p2a, ShrinkTable.Lookup( m_PetBody ), 1, 0, true, false, 0, 3, 1153, 1, 0, EffectLayer.Head, 0x100 );
					from.PlaySound( 492 );

					m_Pet.ControlMaster = from;
					m_Pet.ControlTarget = from;
					m_Pet.ControlOrder = OrderType.Follow;

					if ( m_Pet.IsBonded == true && m_PetOwner != from )
						m_Pet.IsBonded = false;

					m_Pet.MoveToWorld( from.Location, from.Map );

					this.Delete();
				}
				else
				{
					BaseCreature pet = null;

					if( m_PetTypeString != null )
					{
						Type type = SpawnerType.GetType( m_PetTypeString );

						if( type != null )
						{				
							object o = Activator.CreateInstance( type );
        						pet = o as BaseCreature;
						}
					}

					if ( pet == null )
					{
						from.SendMessage( 38, "Pet could not be claimed due to an internal error. Please contact a member of the staff." );
					}
					else
					{
						pet.BondingBegin = m_BondingBegin;

						if ( pet is SwampDragon )
						{
							SwampDragon sd = (SwampDragon)pet;
							sd.BardingExceptional = sd.BardingExceptional;
							sd.BardingCrafter = sd.BardingCrafter;
							sd.BardingHP = sd.BardingHP;
							sd.HasBarding = sd.HasBarding;
							sd.BardingResource = sd.BardingResource;
						}
						if ( m_Pet is FireSteed )
						{
							FireSteed fs = (FireSteed)m_Pet;
							m_BardingExceptional = fs.BardingExceptional;
							m_BardingCrafter = fs.BardingCrafter;
							m_BardingHP = fs.BardingHP;
							m_HasBarding = fs.HasBarding;
							m_BardingResource = fs.BardingResource;
						}

						if ( pet is Sheep )
						{
							Sheep s = (Sheep)pet;
							s.NextWoolTime = m_SheepWool;
						}

						if ( pet is FactionWarHorse )
						{
							FactionWarHorse f = (FactionWarHorse)pet;
							f.Faction = m_Faction;
						}
		
						pet.Title = m_PetTitle;

						pet.AbilityPoints = m_AbilityPoints;

						pet.Exp = m_Exp;
						pet.NextLevel = m_NextLevel;
						pet.Level = m_Level;
						pet.MaxLevel = m_MaxLevel;
						pet.AllowMating = m_AllowMating;
						pet.Evolves = m_Evolves;
						pet.Generation = m_Gen;
						pet.MatingDelay = this.MatingDelay;
						pet.Form1 = m_Form1;
						pet.Form2 = m_Form2;
						pet.Form3 = m_Form3;
						pet.Form4 = m_Form4;
						pet.Form5 = m_Form5;
						pet.Form6 = m_Form6;
						pet.Form7 = m_Form7;
						pet.Form8 = m_Form8;
						pet.Form9 = m_Form9;
						pet.Sound1 = m_Sound1;
						pet.Sound2 = m_Sound2;
						pet.Sound3 = m_Sound3;
						pet.Sound4 = m_Sound4;
						pet.Sound5 = m_Sound5;
						pet.Sound6 = m_Sound6;
						pet.Sound7 = m_Sound7;
						pet.Sound8 = m_Sound8;
						pet.Sound9 = m_Sound9;
						pet.UsesForm1 = m_UsesForm1;
						pet.UsesForm2 = m_UsesForm2;
						pet.UsesForm3 = m_UsesForm3;
						pet.UsesForm4 = m_UsesForm4;
						pet.UsesForm5 = m_UsesForm5;
						pet.UsesForm6 = m_UsesForm6;
						pet.UsesForm7 = m_UsesForm7;
						pet.UsesForm8 = m_UsesForm8;
						pet.UsesForm9 = m_UsesForm9;
						pet.F0 = m_F0;
						pet.F1 = m_F1;
						pet.F2 = m_F2;
						pet.F3 = m_F3;
						pet.F4 = m_F4;
						pet.F5 = m_F5;
						pet.F6 = m_F6;
						pet.F7 = m_F7;
						pet.F8 = m_F8;
						pet.F9 = m_F9;

						if ( pet is BaseMount )
						{	
							BaseMount mount = pet as BaseMount;
							if ( m_MountID >= 0 )
								mount.ItemID = m_MountID;
						}	

						if ( m_PetBonded == true && m_PetOwner == from )
						{
							pet.IsBonded = true;
						}
						else if ( m_PetBonded == true && m_PetOwner != from )
						{
							from.SendMessage( "This pet has lost its bonded status since you are not the original owner." );
							pet.IsBonded = false;
						}
						else
						{
							pet.IsBonded = false;
						}

						pet.Name = m_PetName;

						pet.Female = m_IsFemale;
		
						if ( m_PetBody >= 0 )
							pet.BodyValue = m_PetBody;

						if ( m_PetHue >= 0 )
							pet.Hue = m_PetHue;

						if ( m_PetSound >= 0 )
							pet.BaseSoundID = m_PetSound;
	
						if ( m_PetVArmor >= 0 )
							pet.VirtualArmor = m_PetVArmor;

						if ( m_PetMinDamage >= 0 )
							pet.DamageMin = m_PetMinDamage;
		
						if ( m_PetMaxDamage >= 0 )
							pet.DamageMax = m_PetMaxDamage;
				
						//Start Pet Stats
				
						if ( m_PetStr >= 0 )
							pet.RawStr = m_PetStr;
				
						if ( m_PetDex >= 0 )
							pet.RawDex = m_PetDex;
		
						if ( m_PetInt >= 0 )
							pet.RawInt = m_PetInt;

						if ( m_PetHits >= 0 )
							pet.HitsMaxSeed = m_PetHits;
		
						if ( m_PetStam >= 0 )
							pet.StamMaxSeed = m_PetStam;

						if ( m_PetMana >= 0 )
							pet.ManaMaxSeed = m_PetMana;

						pet.Hits = m_PetHitsNow;
						pet.Stam = m_PetStamNow;
						pet.Mana = m_PetManaNow;

						// End Pet Stats
						// Start Resist Setting

						if ( m_PetPhysicalResist >= 0 )
							pet.PhysicalResistanceSeed = m_PetPhysicalResist;

						if ( m_PetColdResist >= 0 )
							pet.ColdResistSeed = m_PetColdResist;

						if ( m_PetFireResist >= 0 )
							pet.FireResistSeed = m_PetFireResist;

						if ( m_PetPoisonResist >= 0 )
							pet.PoisonResistSeed = m_PetPoisonResist;

						if ( m_PetEnergyResist >= 0 )
							pet.EnergyResistSeed = m_PetEnergyResist;
	
						//End Resist Setting
						//Start Damage Type Setting

						if ( m_PetPhysicalDamage >= 0 )
							pet.PhysicalDamage = m_PetPhysicalDamage;

						if ( m_PetColdDamage >= 0 )
							pet.ColdDamage = m_PetColdDamage;

						if ( m_PetFireDamage >= 0 )
							pet.FireDamage = m_PetFireDamage;
	
						if ( m_PetPoisonDamage >= 0 )
							pet.PoisonDamage = m_PetPoisonDamage;

						if ( m_PetEnergyDamage >= 0 )
							pet.EnergyDamage = m_PetEnergyDamage;

						//End Damage Type Setting
						//Start Setting Skills

						if ( m_PetWrestling >= 0 )
							pet.Skills[SkillName.Wrestling].Base = m_PetWrestling;

						if ( m_PetTactics >= 0 )
							pet.Skills[SkillName.Tactics].Base = m_PetTactics;

						if ( m_PetResist >= 0 )
							pet.Skills[SkillName.MagicResist].Base = m_PetResist;

						if ( m_PetAnatomy >= 0 )
							pet.Skills[SkillName.Anatomy].Base = m_PetAnatomy;
	
						if ( m_PetPoisoning >= 0 )
							pet.Skills[SkillName.Poisoning].Base = m_PetPoisoning;
	
						if ( m_PetMagery >= 0 )
							pet.Skills[SkillName.Magery].Base = m_PetMagery;

						if ( m_PetEvalInt >= 0 )
							pet.Skills[SkillName.EvalInt].Base = m_PetEvalInt;

						if ( m_PetMed >= 0 )
							pet.Skills[SkillName.Meditation].Base = m_PetMed;

						pet.Skills[SkillName.Wrestling].Cap = m_CapWrestling;

						pet.Skills[SkillName.Tactics].Cap = m_CapTactics;

						pet.Skills[SkillName.MagicResist].Cap = m_CapResist;

						pet.Skills[SkillName.Anatomy].Cap = m_CapAnatomy;
	
						pet.Skills[SkillName.Poisoning].Cap = m_CapPoisoning;
		
						pet.Skills[SkillName.Magery].Cap = m_CapMagery;

						pet.Skills[SkillName.EvalInt].Cap = m_CapEvalInt;

						pet.Skills[SkillName.Meditation].Cap = m_CapMed;

						//End Setting Skills

						if ( m_PetOwner == from )
						{
							from.SendMessage( "You have claimed your pet {0}.", m_PetName );
						}
						else if ( m_PetOwner != from )
						{
							from.SendMessage( "You have claimed a {0}, Named {1}.", m_PetTypeString, m_PetName );
							m_PetOwner.SendMessage( "{0} has claimed your old pet {1}.", from.Name, m_PetName );
						}

						IEntity p1 = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z ), from.Map );
						IEntity p2 = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z + 50 ), from.Map );

						Effects.SendMovingParticles( p1, p2, ShrinkTable.Lookup( m_PetBody ), 1, 0, true, false, 0, 3, 1153, 1, 0, EffectLayer.Head, 0x100 );
						from.PlaySound( 492 );

        					pet.Controlled = true;
        					pet.ControlMaster = from;
        					pet.Location = from.Location;
						pet.ControlOrder = OrderType.Follow;
						pet.ControlTarget = from;
        					pet.Map = from.Map;

						if ( pet.IsParagon )
							pet.IsParagon = false;

        					World.AddMobile( pet );

						bool packanimal = false;

						foreach ( string ispack in FSATS.PackAnimals )
						{
  							if ( ispack == m_PetTypeString )
    								packanimal = true;
						}

						if ( packanimal == true )
						{
						}
						else
						{
							ArrayList equipitems = new ArrayList( pet.Items );

							foreach (Item item in equipitems)
							{
								if ( ( item.Layer != Layer.Bank ) && ( item.Layer != Layer.Hair ) && ( item.Layer != Layer.FacialHair ) )
								{
									item.Delete();
								}
							}
						}

						this.Delete();
					}
				}
			}
      		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( from.Alive )
			{
				if ( m_Locked == false && m_PetOwner == from )
				{
					list.Add( new ContextMenus.LockShrinkItem( from, this ) );
				}
				else if ( m_Locked == true && m_PetOwner == from )
				{
					list.Add( new ContextMenus.UnLockShrinkItem( from, this ) );
				}
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			if ( m_PetTypeString != null )
			{
				if ( m_PetBonded != false )
					list.Add( 1049608 );
				
				if ( m_Locked == true )
				{
					list.Add( 1049644, "Locked" );
				}
				else
				{
					list.Add( 1049644, "Unlocked" );
				}

				string s = m_PetTypeString;

				int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);

				if( capsbreak > -1 )
				{
					string secondhalf = s.Substring( capsbreak );
 					string firsthalf = s.Substring(0, capsbreak );

					list.Add( 1060663, "Name\t{0} Breed: {1} {2}", m_PetName, firsthalf, secondhalf );
				}
				else
				{
					list.Add( 1060663, "Name\t{0} Breed: {1}", m_PetName, m_PetTypeString );
				}

				list.Add( 1061640, (m_PetOwner == null ) ? "nobody" : m_PetOwner.Name ); // Owner: ~1_OWNER~
				list.Add( 1060659, "Stats\tStrength {0}, Dexterity {1}, Intelligence {2}", m_PetStr, m_PetDex, m_PetInt );
				list.Add( 1060660, "Combat Skills\tWrestling {0}, Tactics {1}, Anatomy {2}, Poisoning {3}", m_PetWrestling, m_PetTactics, m_PetAnatomy, m_PetPoisoning );
				list.Add( 1060661, "Magic Skills\tMagery {0}, Eval Intel {1}, Magic Resist {2}, Meditation {3}", m_PetMagery, m_PetEvalInt, m_PetResist, m_PetMed );
				
				if ( m_Level != 0 )
					list.Add( 1060662, "Exp\t{0}, Level: {1}", m_Exp, m_Level );
			}
		}

      		public ShrinkItemX( Serial serial ) : base( serial )
      		{
      		}

      		public override void Serialize( GenericWriter writer )
      		{
        	 	base.Serialize( writer );

         		writer.Write( (int) 14 ); // version

			writer.Write( m_PetTitle );

			Faction.WriteReference( writer, m_Faction );

			writer.Write( m_Exp );

			writer.Write( m_NextLevel );

			writer.Write( m_Level );

			writer.Write( m_MaxLevel );

			writer.Write( m_AllowMating );

			writer.Write( m_Evolves );

			writer.Write( m_Gen );

			writer.WriteDeltaTime( m_MatingDelay );

			writer.Write( m_Form1 );

			writer.Write( m_Form2 );

			writer.Write( m_Form3 );

			writer.Write( m_Form4 );

			writer.Write( m_Form5 );

			writer.Write( m_Form6 );

			writer.Write( m_Form7 );

			writer.Write( m_Form8 );

			writer.Write( m_Form9 );

			writer.Write( m_Sound1 );

			writer.Write( m_Sound2 );

			writer.Write( m_Sound3 );

			writer.Write( m_Sound4 );

			writer.Write( m_Sound5 );

			writer.Write( m_Sound6 );

			writer.Write( m_Sound7 );

			writer.Write( m_Sound8 );

			writer.Write( m_Sound9 );

			writer.Write( m_UsesForm1 );

			writer.Write( m_UsesForm2 );

			writer.Write( m_UsesForm3 );

			writer.Write( m_UsesForm4 );

			writer.Write( m_UsesForm5 );

			writer.Write( m_UsesForm6 );

			writer.Write( m_UsesForm7 );

			writer.Write( m_UsesForm8 );

			writer.Write( m_UsesForm9 );

			writer.Write( m_F0 );

			writer.Write( m_F1 );

			writer.Write( m_F2 );

			writer.Write( m_F3 );

			writer.Write( m_F4 );

			writer.Write( m_F5 );

			writer.Write( m_F6 );

			writer.Write( m_F7 );

			writer.Write( m_F8 );

			writer.Write( m_F9 );

			writer.Write( (DateTime) m_BondingBegin );

			writer.Write( m_AbilityPoints );

			writer.Write( m_BardingExceptional );

			writer.Write( m_BardingCrafter );

			writer.Write( m_HasBarding );

			writer.Write( m_BardingHP );

			writer.Write( (int) m_BardingResource );

			writer.Write( m_CapWrestling );

			writer.Write( m_CapTactics );

			writer.Write( m_CapResist );

			writer.Write( m_CapAnatomy );

			writer.Write( m_CapPoisoning );

			writer.Write( m_CapMagery );

			writer.Write( m_CapEvalInt );

			writer.Write( m_CapMed );

			writer.Write( m_SheepWool );

			writer.Write( m_Disabled );

			writer.Write( m_PetS1 );

			writer.Write( m_PetS2 );

			writer.Write( m_PetS3 );

			writer.Write( m_PetS4 );

			writer.Write( m_PetS5 );

			writer.Write( m_PetS6 );

			writer.Write( m_IsFemale );

			writer.Write( m_PetHitsNow );

			writer.Write( m_PetStamNow );

			writer.Write( m_PetManaNow );

			writer.Write( m_MountID );

			writer.Write( m_PetKP );

			writer.Write( m_PetStage );

			writer.Write( m_PetHasEgg );

			writer.Write( m_PetAllowMating );

			writer.Write( m_PetPregnant );

			writer.Write( m_IsDeed );

			writer.Write( m_Pet );

			writer.Write( m_PetHue );

			writer.Write( m_PetBonded );

			writer.Write( m_PetOwner );

			writer.Write( m_PetTypeString );

			writer.Write( m_Locked );

			writer.Write( m_PetMinTame );

			writer.Write( m_PetControlSlots );

			writer.Write( m_PetName );

			writer.Write( m_PetMinDamage );

			writer.Write( m_PetMaxDamage );

			writer.Write( m_PetBody );

			writer.Write( m_PetSound );

			writer.Write( m_PetVArmor );

			writer.Write( m_PetStr );

			writer.Write( m_PetDex );

			writer.Write( m_PetInt );

			writer.Write( m_PetHits );

			writer.Write( m_PetStam );

			writer.Write( m_PetMana );

			writer.Write( m_PetPhysicalResist );

			writer.Write( m_PetColdResist );

			writer.Write( m_PetFireResist );

			writer.Write( m_PetPoisonResist );

			writer.Write( m_PetEnergyResist );

			writer.Write( m_PetPhysicalDamage );

			writer.Write( m_PetColdDamage );

			writer.Write( m_PetFireDamage );

			writer.Write( m_PetPoisonDamage );

			writer.Write( m_PetEnergyDamage );

			writer.Write( m_PetWrestling );

			writer.Write( m_PetTactics );

			writer.Write( m_PetResist );

			writer.Write( m_PetAnatomy );

			writer.Write( m_PetPoisoning );

			writer.Write( m_PetMagery );

			writer.Write( m_PetEvalInt );

			writer.Write( m_PetMed );
      		}

      		public override void Deserialize( GenericReader reader )
      		{
         		base.Deserialize( reader );

         		int version = reader.ReadInt();

			switch ( version )
			{
				case 14: // Fixed War Horese
				{
					m_PetTitle = reader.ReadString();
					goto case 13;
				}
				case 13: // Fixed War Horese
				{
					m_Faction = Faction.ReadReference( reader );
					goto case 12;
				}
				case 12: // Advanced Pet Addon
				{
					m_Exp = reader.ReadInt();
					m_NextLevel = reader.ReadInt();
					m_Level = reader.ReadInt();
					m_MaxLevel = reader.ReadInt();
					m_AllowMating = reader.ReadBool();
					m_Evolves = reader.ReadBool();
					m_Gen = reader.ReadInt();
					m_MatingDelay = reader.ReadDeltaTime();
					m_Form1 = reader.ReadInt();
					m_Form2 = reader.ReadInt();
					m_Form3 = reader.ReadInt();
					m_Form4 = reader.ReadInt();
					m_Form5 = reader.ReadInt();
					m_Form6 = reader.ReadInt();
					m_Form7 = reader.ReadInt();
					m_Form8 = reader.ReadInt();
					m_Form9 = reader.ReadInt();
					m_Sound1 = reader.ReadInt();
					m_Sound2 = reader.ReadInt();
					m_Sound3 = reader.ReadInt();
					m_Sound4 = reader.ReadInt();
					m_Sound5 = reader.ReadInt();
					m_Sound6 = reader.ReadInt();
					m_Sound7 = reader.ReadInt();
					m_Sound8 = reader.ReadInt();
					m_Sound9 = reader.ReadInt();
					m_UsesForm1 = reader.ReadBool();
					m_UsesForm2 = reader.ReadBool();
					m_UsesForm3 = reader.ReadBool();
					m_UsesForm4 = reader.ReadBool();
					m_UsesForm5 = reader.ReadBool();
					m_UsesForm6 = reader.ReadBool();
					m_UsesForm7 = reader.ReadBool();
					m_UsesForm8 = reader.ReadBool();
					m_UsesForm9 = reader.ReadBool();
					m_F0 = reader.ReadBool();
					m_F1 = reader.ReadBool();
					m_F2 = reader.ReadBool();
					m_F3 = reader.ReadBool();
					m_F4 = reader.ReadBool();
					m_F5 = reader.ReadBool();
					m_F6 = reader.ReadBool();
					m_F7 = reader.ReadBool();
					m_F8 = reader.ReadBool();
					m_F9 = reader.ReadBool();
					goto case 11;
				}
				case 11: // Fixed Pets Losing Bond Time
				{
					m_BondingBegin = reader.ReadDateTime();
					goto case 10;
				}
				case 10: // Ability Points Fix
				{
					m_AbilityPoints = reader.ReadInt();
					goto case 9;
				}
				case 9: // Swamp Dragon Armor Fix
				{
					m_BardingExceptional = reader.ReadBool();
					m_BardingCrafter = reader.ReadMobile();
					m_HasBarding = reader.ReadBool();
					m_BardingHP = reader.ReadInt();
					m_BardingResource = (CraftResource) reader.ReadInt();
					goto case 8;
				}
				case 8: // Pet Skill Cap Fix
				{
					m_CapWrestling = reader.ReadDouble();
					m_CapTactics = reader.ReadDouble();
					m_CapResist = reader.ReadDouble();
					m_CapAnatomy = reader.ReadDouble();
					m_CapPoisoning = reader.ReadDouble();
					m_CapMagery = reader.ReadDouble();
					m_CapEvalInt = reader.ReadDouble();
					m_CapMed = reader.ReadDouble();
					goto case 7;
				} 
				case 7: // Sheep Wool Fix
				{
					m_SheepWool = reader.ReadDeltaTime();
					goto case 6;
				}
				case 6: // Disable Command Addon
				{
					m_Disabled = reader.ReadBool();
					goto case 5;
				}
				case 5: // Evo Pet Fix
				{
					m_PetS1 = reader.ReadBool();
					m_PetS2 = reader.ReadBool();
					m_PetS3 = reader.ReadBool();
					m_PetS4 = reader.ReadBool();
					m_PetS5 = reader.ReadBool();
					m_PetS6 = reader.ReadBool();
					goto case 4;
				}
				case 4: // Evo Pet Fix
				{
					m_IsFemale = reader.ReadBool();
					goto case 3;
				}
				case 3: // Insta Heal On Shrink Fix
				{
					m_PetHitsNow = reader.ReadInt();
					m_PetStamNow = reader.ReadInt();
					m_PetManaNow = reader.ReadInt();
					goto case 2;
				}
				case 2: // Mount Fix
				{
					m_MountID = reader.ReadInt();
					goto case 1;
				}	
				case 1: // Evo Dragon Addon
				{
					m_PetKP = reader.ReadInt();
					m_PetStage = reader.ReadInt();
					m_PetHasEgg = reader.ReadBool();
					m_PetAllowMating = reader.ReadBool();
					m_PetPregnant = reader.ReadBool();
					goto case 0;
				}
				case 0: // Initial Release
				{
					m_IsDeed = reader.ReadBool();
					m_Pet = (BaseCreature)reader.ReadMobile();
					m_PetHue = reader.ReadInt();
					m_PetBonded = reader.ReadBool();
					m_PetOwner = reader.ReadMobile();
					m_PetTypeString = reader.ReadString();
					m_Locked = reader.ReadBool();
					m_PetMinTame = reader.ReadDouble();
					m_PetControlSlots = reader.ReadInt();
					m_PetName = reader.ReadString();
					m_PetMinDamage = reader.ReadInt();
					m_PetMaxDamage = reader.ReadInt();
					m_PetBody = reader.ReadInt();
					m_PetSound = reader.ReadInt();
					m_PetVArmor = reader.ReadInt();
					m_PetStr = reader.ReadInt();
					m_PetDex = reader.ReadInt();
					m_PetInt = reader.ReadInt();
					m_PetHits = reader.ReadInt();
					m_PetStam = reader.ReadInt();
					m_PetMana = reader.ReadInt();
					m_PetPhysicalResist = reader.ReadInt();
					m_PetColdResist = reader.ReadInt();
					m_PetFireResist = reader.ReadInt();
					m_PetPoisonResist = reader.ReadInt();
					m_PetEnergyResist = reader.ReadInt();
					m_PetPhysicalDamage = reader.ReadInt();
					m_PetColdDamage = reader.ReadInt();
					m_PetFireDamage = reader.ReadInt();
					m_PetPoisonDamage = reader.ReadInt();
					m_PetEnergyDamage = reader.ReadInt();
					m_PetWrestling = reader.ReadDouble();
					m_PetTactics = reader.ReadDouble();
					m_PetResist = reader.ReadDouble();
					m_PetAnatomy = reader.ReadDouble();
					m_PetPoisoning = reader.ReadDouble();
					m_PetMagery = reader.ReadDouble();
					m_PetEvalInt = reader.ReadDouble();
					m_PetMed = reader.ReadDouble();
					break;
				}
			}
      		}
   	}
}
