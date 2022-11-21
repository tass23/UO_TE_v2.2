using System;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;
using Server.Items;
using Server.Engines.VeteranRewards;
using Server.Engines.Craft;
using Server.Factions;
using Server.Network;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
	public enum ClothingQuality
	{
		Low,
		Regular,
		Exceptional
	}

	public interface IArcaneEquip
	{
		bool IsArcane{ get; }
		int CurArcaneCharges{ get; set; }
		int MaxArcaneCharges{ get; set; }
	}

	public abstract class BaseClothing : Item, IDyable, IScissorable, IFactionItem, ICraftable, IWearableDurability, ISetItem
	{
		#region Factions
		private FactionItem m_FactionState;

		public FactionItem FactionItemState
		{
			get{ return m_FactionState; }
			set
			{
				m_FactionState = value;

				if ( m_FactionState == null )
					Hue = 0;

				LootType = ( m_FactionState == null ? LootType.Regular : LootType.Blessed );
			}
		}
		#endregion

		public virtual bool CanFortify{ get{ return true; } }

		private int m_MaxHitPoints;
		private int m_HitPoints;
		private Mobile m_Crafter;
		private ClothingQuality m_Quality;
		private bool m_PlayerConstructed;
		protected CraftResource m_Resource;
		private int m_StrReq = -1;

		private AosAttributes m_AosAttributes;
		private AosArmorAttributes m_AosClothingAttributes;
		private AosSkillBonuses m_AosSkillBonuses;
		private AosElementAttributes m_AosResistances;

		#region Spellcrafting
		private int m_TimesCrafted;
        #endregion
		
        #region SF Imbuing
		private int m_TimesImbued;
        #endregion

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxHitPoints
		{
			get{ return m_MaxHitPoints; }
			set{ m_MaxHitPoints = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HitPoints
		{
			get 
			{
				return m_HitPoints;
			}
			set 
			{
				if ( value != m_HitPoints && MaxHitPoints > 0 )
				{
					m_HitPoints = value;

					if ( m_HitPoints < 0 )
						Delete();
					else if ( m_HitPoints > MaxHitPoints )
						m_HitPoints = MaxHitPoints;

					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int StrRequirement
		{
			get{ return ( m_StrReq == -1 ? (Core.AOS ? AosStrReq : OldStrReq) : m_StrReq ); }
			set{ m_StrReq = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ClothingQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PlayerConstructed
		{
			get{ return m_PlayerConstructed; }
			set{ m_PlayerConstructed = value; }
		}

		#region Spellcrafting
		[CommandProperty( AccessLevel.GameMaster )]
		public int TimesCrafted{ get{ return m_TimesCrafted; } set{ m_TimesCrafted = value; InvalidateProperties(); } }
        #endregion
		
        #region SF Imbuing
		[CommandProperty( AccessLevel.GameMaster )]
		public int TimesImbued{ get{ return m_TimesImbued; } set{ m_TimesImbued = value; InvalidateProperties(); } }
        #endregion

        #region Personal Bless Deed
        private Mobile m_BlessedBy;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile BlessedBy
        {
            get { return m_BlessedBy; }
            set { m_BlessedBy = value; InvalidateProperties(); }
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
			XmlLevelItem levitem = XmlAttach.FindAttachment(this, typeof(XmlLevelItem)) as XmlLevelItem;

            if (levitem != null)
            {
                list.Add(new LevelInfoEntry(from, this, AttributeCategory.Melee));
            }
			
            if (BlessedFor == from && BlessedBy == from && RootParent == from)
            {
                list.Add(new UnBlessEntry(from, this));
            }
        }

        private class UnBlessEntry : ContextMenuEntry
        {
            private Mobile m_From;
            private BaseClothing m_Item;

            public UnBlessEntry(Mobile from, BaseClothing item)
                : base(6208, -1)
            {
                m_From = from;
                m_Item = item; // BaseArmor, BaseWeapon or BaseClothing
            }

            public override void OnClick()
            {
                m_Item.BlessedFor = null;
                m_Item.BlessedBy = null;

                Container pack = m_From.Backpack;

                if (pack != null)
                {
                    pack.DropItem(new PersonalBlessDeed(m_From));
                    m_From.SendLocalizedMessage(1062200); // A personal bless deed has been placed in your backpack.
                }
            }
        }
        #endregion

		public virtual CraftResource DefaultResource{ get{ return CraftResource.None; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; Hue = CraftResources.GetHue( m_Resource ); InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosAttributes Attributes
		{
			get{ return m_AosAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosArmorAttributes ClothingAttributes
		{
			get{ return m_AosClothingAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosSkillBonuses SkillBonuses
		{
			get{ return m_AosSkillBonuses; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosElementAttributes Resistances
		{
			get{ return m_AosResistances; }
			set{}
		}

		public virtual int BasePhysicalResistance{ get{ return 0; } }
		public virtual int BaseFireResistance{ get{ return 0; } }
		public virtual int BaseColdResistance{ get{ return 0; } }
		public virtual int BasePoisonResistance{ get{ return 0; } }
		public virtual int BaseEnergyResistance{ get{ return 0; } }

		#region Mondain's Legacy Sets
		public override int PhysicalResistance{ get{ return BasePhysicalResistance + m_AosResistances.Physical + (m_SetEquipped ? m_SetPhysicalBonus : 0 ); } }
		public override int FireResistance{ get{ return BaseFireResistance + m_AosResistances.Fire + (m_SetEquipped ? m_SetFireBonus : 0 ); } }
		public override int ColdResistance{ get{ return BaseColdResistance + m_AosResistances.Cold + (m_SetEquipped ? m_SetColdBonus : 0 ); } }
		public override int PoisonResistance{ get{ return BasePoisonResistance + m_AosResistances.Poison + (m_SetEquipped ? m_SetPoisonBonus : 0 ); } }
		public override int EnergyResistance{ get{ return BaseEnergyResistance + m_AosResistances.Energy + (m_SetEquipped ? m_SetEnergyBonus : 0 ); } }
		#endregion

		public virtual int ArtifactRarity{ get{ return 0; } }

		public virtual int BaseStrBonus{ get{ return 0; } }
		public virtual int BaseDexBonus{ get{ return 0; } }
		public virtual int BaseIntBonus { get { return 0; } }

		public override bool AllowSecureTrade( Mobile from, Mobile to, Mobile newOwner, bool accepted )
		{
			if ( !Ethics.Ethic.CheckTrade( from, to, newOwner, this ) )
				return false;

			return base.AllowSecureTrade( from, to, newOwner, accepted );
		}

		public virtual Race RequiredRace { get { return null; } }

		#region SA
		public virtual bool CanBeWornByGargoyles{ get{ return false; } }
		#endregion

		public override bool CanEquip( Mobile from )
		{
			if ( !Ethics.Ethic.CheckEquip( from, this ) )
				return false;

			if( from.AccessLevel < AccessLevel.GameMaster )
			{
				#region SA
				if ( from.Race == Race.Gargoyle && !CanBeWornByGargoyles )
				{
					from.SendLocalizedMessage( 1111708 ); // Gargoyles can't wear this.
					return false;
				}
				#endregion
				else if( RequiredRace != null && from.Race != RequiredRace )
				{
					if( RequiredRace == Race.Elf )
						from.SendLocalizedMessage( 1072203 ); // Only Elves may use this.
					else if ( RequiredRace == Race.Gargoyle )
						from.SendLocalizedMessage( 1111707 ); // Only gargoyles can wear this.
					else
						from.SendMessage( "Only {0} may use this.", RequiredRace.PluralName );

					return false;
				}
				else if( !AllowMaleWearer && !from.Female )
				{
					if( AllowFemaleWearer )
						from.SendLocalizedMessage( 1010388 ); // Only females can wear this.
					else
						from.SendMessage( "You may not wear this." );

					return false;
				}
				else if( !AllowFemaleWearer && from.Female )
				{
					if( AllowMaleWearer )
						from.SendLocalizedMessage( 1063343 ); // Only males can wear this.
					else
						from.SendMessage( "You may not wear this." );

					return false;
                }
                #region Personal Bless Deed
                else if (BlessedBy != null && BlessedBy != from)
                {
                    from.SendLocalizedMessage(1075277); // That item is blessed by another player.

                    return false;
                }
                #endregion
                else
				{
					int strBonus = ComputeStatBonus( StatType.Str );
					int strReq = ComputeStatReq( StatType.Str );

					if( from.Str < strReq || (from.Str + strBonus) < 1 )
					{
						from.SendLocalizedMessage( 500213 ); // You are not strong enough to equip that.
						return false;
					}
				}
			}

			return base.CanEquip( from );
		}

		public virtual int AosStrReq{ get{ return 10; } }
		public virtual int OldStrReq{ get{ return 0; } }

		public virtual int InitMinHits{ get{ return 0; } }
		public virtual int InitMaxHits{ get{ return 0; } }

		public virtual bool AllowMaleWearer{ get{ return true; } }
		public virtual bool AllowFemaleWearer{ get{ return true; } }
		public virtual bool CanBeBlessed{ get{ return true; } }

		public int ComputeStatReq( StatType type )
		{
			int v;

			//if ( type == StatType.Str )
				v = StrRequirement;

			return AOS.Scale( v, 100 - GetLowerStatReq() );
		}

		public int ComputeStatBonus( StatType type )
		{
			if ( type == StatType.Str )
				return BaseStrBonus + Attributes.BonusStr;
			else if ( type == StatType.Dex )
				return BaseDexBonus + Attributes.BonusDex;
			else
				return BaseIntBonus + Attributes.BonusInt;
		}

		public virtual void AddStatBonuses( Mobile parent )
		{
			if ( parent == null )
				return;

			int strBonus = ComputeStatBonus( StatType.Str );
			int dexBonus = ComputeStatBonus( StatType.Dex );
			int intBonus = ComputeStatBonus( StatType.Int );

			if ( strBonus == 0 && dexBonus == 0 && intBonus == 0 )
				return;

			string modName = this.Serial.ToString();

			if ( strBonus != 0 )
				parent.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

			if ( dexBonus != 0 )
				parent.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

			if ( intBonus != 0 )
				parent.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
		}

		public static void ValidateMobile( Mobile m )
		{
			for ( int i = m.Items.Count - 1; i >= 0; --i )
			{
				if ( i >= m.Items.Count )
					continue;

				Item item = m.Items[i];

				if ( item is BaseClothing )
				{
					BaseClothing clothing = (BaseClothing)item;

					#region SA
					if ( m.Race == Race.Gargoyle && !clothing.CanBeWornByGargoyles )
					{
						m.SendLocalizedMessage( 1111708 ); // Gargoyles can't wear this.
						m.AddToBackpack( clothing );
					}
					#endregion
					if( clothing.RequiredRace != null && m.Race != clothing.RequiredRace )
					{
						if( clothing.RequiredRace == Race.Elf )
							m.SendLocalizedMessage( 1072203 ); // Only Elves may use this.
						#region SA
						else if ( clothing.RequiredRace == Race.Gargoyle )
							m.SendLocalizedMessage( 1111707 ); // Only gargoyles can wear this.
						#endregion
						else
							m.SendMessage( "Only {0} may use this.", clothing.RequiredRace.PluralName );

						m.AddToBackpack( clothing );
					}
					else if ( !clothing.AllowMaleWearer && !m.Female && m.AccessLevel < AccessLevel.GameMaster )
					{
						if ( clothing.AllowFemaleWearer )
							m.SendLocalizedMessage( 1010388 ); // Only females can wear this.
						else
							m.SendMessage( "You may not wear this." );

						m.AddToBackpack( clothing );
					}
					else if ( !clothing.AllowFemaleWearer && m.Female && m.AccessLevel < AccessLevel.GameMaster )
					{
						if ( clothing.AllowMaleWearer )
							m.SendLocalizedMessage( 1063343 ); // Only males can wear this.
						else
							m.SendMessage( "You may not wear this." );

						m.AddToBackpack( clothing );
					}
				}
			}
		}

		public int GetLowerStatReq()
		{
			if ( !Core.AOS )
				return 0;

			return m_AosClothingAttributes.LowerStatReq;
		}

		public override void OnAdded( object parent )
		{
			Mobile mob = parent as Mobile;

			if ( mob != null )
			{
				if ( Core.AOS )
					m_AosSkillBonuses.AddTo( mob );

				#region Mondain's Legacy Sets
				if ( IsSetItem )
				{
					m_SetEquipped = SetHelper.FullSetEquipped( mob, SetID, Pieces );

					if ( m_SetEquipped )
					{
						m_LastEquipped = true;
						SetHelper.AddSetBonus( mob, SetID );
					}
				}
				#endregion

				AddStatBonuses( mob );
				mob.CheckStatTimers();
			}

			base.OnAdded( parent );
		}

		public override void OnRemoved( object parent )
		{
			Mobile mob = parent as Mobile;

			if ( mob != null )
			{
				if ( Core.AOS )
					m_AosSkillBonuses.Remove();

				string modName = this.Serial.ToString();

				mob.RemoveStatMod( modName + "Str" );
				mob.RemoveStatMod( modName + "Dex" );
				mob.RemoveStatMod( modName + "Int" );

				mob.CheckStatTimers();

				#region Mondain's Legacy Sets
				if ( IsSetItem && m_SetEquipped )
					SetHelper.RemoveSetBonus( mob, SetID, this );
				#endregion
			}

			base.OnRemoved( parent );
		}

		public virtual int OnHit( BaseWeapon weapon, int damageTaken )
		{
			int Absorbed = Utility.RandomMinMax( 1, 4 );

			damageTaken -= Absorbed;

			if ( damageTaken < 0 ) 
				damageTaken = 0;

			if ( 25 > Utility.Random( 100 ) ) // 25% chance to lower durability
			{
				// Mondain's Legacy Sets
				if ( Core.AOS && m_AosClothingAttributes.SelfRepair + ( IsSetItem && m_SetEquipped ? m_SetSelfRepair : 0 ) > Utility.Random( 10 ) )
				{
					HitPoints += 2;
				}
				else
				{
					int wear;

					if ( weapon.Type == WeaponType.Bashing )
						wear = Absorbed / 2;
					else
						wear = Utility.Random( 2 );

					if ( wear > 0 && m_MaxHitPoints > 0 )
					{
						if ( m_HitPoints >= wear )
						{
							HitPoints -= wear;
							wear = 0;
						}
						else
						{
							wear -= HitPoints;
							HitPoints = 0;
						}

						if ( wear > 0 )
						{
							if ( m_MaxHitPoints > wear )
							{
								MaxHitPoints -= wear;

								if ( Parent is Mobile )
									((Mobile)Parent).LocalOverheadMessage( MessageType.Regular, 0x3B2, 1061121 ); // Your equipment is severely damaged.
							}
							else
							{
								Delete();
							}
						}
					}
				}
			}

			return damageTaken;
		}

		public BaseClothing( int itemID, Layer layer ) : this( itemID, layer, 0 )
		{
		}

		public BaseClothing( int itemID, Layer layer, int hue ) : base( itemID )
		{
			Layer = layer;
			Hue = hue;

			m_Resource = DefaultResource;
			m_Quality = ClothingQuality.Regular;

			m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax( InitMinHits, InitMaxHits );

			m_AosAttributes = new AosAttributes( this );
			m_AosClothingAttributes = new AosArmorAttributes( this );
			m_AosSkillBonuses = new AosSkillBonuses( this );
			m_AosResistances = new AosElementAttributes( this );

			#region Mondain's Legacy Sets
			m_SetAttributes = new AosAttributes( this );
			m_SetSkillBonuses = new AosSkillBonuses( this );
			#endregion
		}

		public override void OnAfterDuped( Item newItem )
		{
			BaseClothing clothing = newItem as BaseClothing;

			if ( clothing == null )
				return;

			clothing.m_AosAttributes = new AosAttributes( newItem, m_AosAttributes );
			clothing.m_AosResistances = new AosElementAttributes( newItem, m_AosResistances );
			clothing.m_AosSkillBonuses = new AosSkillBonuses( newItem, m_AosSkillBonuses );
			clothing.m_AosClothingAttributes = new AosArmorAttributes( newItem, m_AosClothingAttributes );

			#region Mondain's Legacy
			clothing.m_SetAttributes = new AosAttributes( newItem, m_SetAttributes );
			clothing.m_SetSkillBonuses = new AosSkillBonuses( newItem, m_SetSkillBonuses );
			#endregion
		}

		public BaseClothing( Serial serial ) : base( serial )
		{
		}

		public override bool AllowEquipedCast( Mobile from )
		{
			if ( base.AllowEquipedCast( from ) )
				return true;

			return ( m_AosAttributes.SpellChanneling != 0 );
		}

		public void UnscaleDurability()
		{
			int scale = 100 + m_AosClothingAttributes.DurabilityBonus;

			m_HitPoints = ( ( m_HitPoints * 100 ) + ( scale - 1 ) ) / scale;
			m_MaxHitPoints = ( ( m_MaxHitPoints * 100 ) + ( scale - 1 ) ) / scale;

			InvalidateProperties();
		}

		public void ScaleDurability()
		{
			int scale = 100 + m_AosClothingAttributes.DurabilityBonus;

			m_HitPoints = ( ( m_HitPoints * scale ) + 99 ) / 100;
			m_MaxHitPoints = ( ( m_MaxHitPoints * scale ) + 99 ) / 100;

			InvalidateProperties();
		}

		public override bool CheckPropertyConfliction( Mobile m )
		{
			if ( base.CheckPropertyConfliction( m ) )
				return true;

			if ( Layer == Layer.Pants )
				return ( m.FindItemOnLayer( Layer.InnerLegs ) != null );

			if ( Layer == Layer.Shirt )
				return ( m.FindItemOnLayer( Layer.InnerTorso ) != null );

			return false;
		}

		private string GetNameString()
		{
			string name = this.Name;

			if ( name == null )
				name = String.Format( "#{0}", LabelNumber );

			return name;
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			int oreType;

			switch ( m_Resource )
			{
				case CraftResource.DullCopper:		oreType = 1053108; break; // dull copper
				case CraftResource.ShadowIron:		oreType = 1053107; break; // shadow iron
				case CraftResource.Copper:			oreType = 1053106; break; // copper
				case CraftResource.Bronze:			oreType = 1053105; break; // bronze
				case CraftResource.Gold:			oreType = 1053104; break; // golden
				case CraftResource.Agapite:			oreType = 1053103; break; // agapite
				case CraftResource.Verite:			oreType = 1053102; break; // verite
				case CraftResource.Valorite:		oreType = 1053101; break; // valorite
				case CraftResource.SpinedLeather:	oreType = 1061118; break; // spined
				case CraftResource.HornedLeather:	oreType = 1061117; break; // horned
				case CraftResource.BarbedLeather:	oreType = 1061116; break; // barbed
				case CraftResource.RedScales:		oreType = 1060814; break; // red
				case CraftResource.YellowScales:	oreType = 1060818; break; // yellow
				case CraftResource.BlackScales:		oreType = 1060820; break; // black
				case CraftResource.GreenScales:		oreType = 1060819; break; // green
				case CraftResource.WhiteScales:		oreType = 1060821; break; // white
				case CraftResource.BlueScales:		oreType = 1060815; break; // blue
				default: oreType = 0; break;
			}
			if ( oreType != 0 )
				list.Add( 1053099, "#{0}\t{1}", oreType, GetNameString() ); // ~1_oretype~ ~2_armortype~
			else if ( Name == null )
				list.Add( LabelNumber );
			else
				list.Add( Name );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			#region Spellcrafting
			if( m_TimesCrafted > 0 )
				list.Add( "<BASEFONT COLOR=#1C7BFF>[Spellcrafted]<BASEFONT COLOR=#63A5FF>" );	//<BASEFONT COLOR=#63A5FF>
            #endregion
            #region SF Imbuing
			if( m_TimesImbued > 0 )
				list.Add( "<BASEFONT COLOR=#2BCF31>[Imbued]<BASEFONT COLOR=#92F0A0>" );	//<BASEFONT COLOR=#92F0A0>
            #endregion
			XmlLevelItem levitem = XmlAttach.FindAttachment(this, typeof(XmlLevelItem)) as XmlLevelItem;

			if (levitem != null)
			{
				/* Display level in the properties context menu.
				* Will display experience as well, if DisplayExpProp.
				* is set to true in LevelItemManager.cs */
				list.Add(1060658, "Level\t{0}", levitem.Level);
				if (LevelItems.DisplayExpProp)
					list.Add(1060659, "Experience\t{0}", levitem.Experience);
			}
			
			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~

			#region Factions
			if ( m_FactionState != null )
				list.Add( 1041350 ); // faction item
			#endregion

			#region Mondain's Legacy Sets
			if ( IsSetItem )
			{
				if ( MixedSet )
					list.Add( 1073491, Pieces.ToString() ); // Part of a Weapon/Armor Set (~1_val~ pieces)
				else
					list.Add( 1072376, Pieces.ToString() ); // Part of an Armor Set (~1_val~ pieces)

				if ( m_SetEquipped )
				{
					if ( MixedSet )
						list.Add( 1073492 ); // Full Weapon/Armor Set Present
					else
						list.Add( 1072377 ); // Full Armor Set Present

					GetSetProperties( list );
				}
			}
			#endregion

			if ( m_Quality == ClothingQuality.Exceptional )
				list.Add( 1060636 ); // exceptional

			if( RequiredRace == Race.Elf )
				list.Add( 1075086 ); // Elves Only
			#region SA
			else if ( RequiredRace == Race.Gargoyle )
				list.Add( 1111709 ); // Gargoyles Only
			#endregion

			if ( m_AosSkillBonuses != null )
				m_AosSkillBonuses.GetProperties( list );

			int prop;

			if ( (prop = ArtifactRarity) > 0 )
				list.Add( 1061078, prop.ToString() ); // artifact rarity ~1_val~

			if ( (prop = m_AosAttributes.WeaponDamage) != 0 )
				list.Add( 1060401, prop.ToString() ); // damage increase ~1_val~%

			if ( (prop = m_AosAttributes.DefendChance) != 0 )
				list.Add( 1060408, prop.ToString() ); // defense chance increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusDex) != 0 )
				list.Add( 1060409, prop.ToString() ); // dexterity bonus ~1_val~

			if ( (prop = m_AosAttributes.EnhancePotions) != 0 )
				list.Add( 1060411, prop.ToString() ); // enhance potions ~1_val~%

			if ( (prop = m_AosAttributes.CastRecovery) != 0 )
				list.Add( 1060412, prop.ToString() ); // faster cast recovery ~1_val~

			if ( (prop = m_AosAttributes.CastSpeed) != 0 )
				list.Add( 1060413, prop.ToString() ); // faster casting ~1_val~

			if ( (prop = m_AosAttributes.AttackChance) != 0 )
				list.Add( 1060415, prop.ToString() ); // hit chance increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusHits) != 0 )
				list.Add( 1060431, prop.ToString() ); // hit point increase ~1_val~

			if ( (prop = m_AosAttributes.BonusInt) != 0 )
				list.Add( 1060432, prop.ToString() ); // intelligence bonus ~1_val~

			if ( (prop = m_AosAttributes.LowerManaCost) != 0 )
				list.Add( 1060433, prop.ToString() ); // lower mana cost ~1_val~%

			if ( (prop = m_AosAttributes.LowerRegCost) != 0 )
				list.Add( 1060434, prop.ToString() ); // lower reagent cost ~1_val~%

			if ( (prop = m_AosClothingAttributes.LowerStatReq) != 0 )
				list.Add( 1060435, prop.ToString() ); // lower requirements ~1_val~%

			if ( (prop = m_AosAttributes.Luck) != 0 )
				list.Add( 1060436, prop.ToString() ); // luck ~1_val~

			if ( (prop = m_AosClothingAttributes.MageArmor) != 0 )
				list.Add( 1060437 ); // mage armor

			if ( (prop = m_AosAttributes.BonusMana) != 0 )
				list.Add( 1060439, prop.ToString() ); // mana increase ~1_val~

			if ( (prop = m_AosAttributes.RegenMana) != 0 )
				list.Add( 1060440, prop.ToString() ); // mana regeneration ~1_val~

			if ( (prop = m_AosAttributes.NightSight) != 0 )
				list.Add( 1060441 ); // night sight

			if ( (prop = m_AosAttributes.ReflectPhysical) != 0 )
				list.Add( 1060442, prop.ToString() ); // reflect physical damage ~1_val~%

			if ( (prop = m_AosAttributes.RegenStam) != 0 )
				list.Add( 1060443, prop.ToString() ); // stamina regeneration ~1_val~

			if ( (prop = m_AosAttributes.RegenHits) != 0 )
				list.Add( 1060444, prop.ToString() ); // hit point regeneration ~1_val~

			if ( (prop = m_AosClothingAttributes.SelfRepair) != 0 )
				list.Add( 1060450, prop.ToString() ); // self repair ~1_val~

			if ( (prop = m_AosAttributes.SpellChanneling) != 0 )
				list.Add( 1060482 ); // spell channeling

			if ( (prop = m_AosAttributes.SpellDamage) != 0 )
				list.Add( 1060483, prop.ToString() ); // spell damage increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusStam) != 0 )
				list.Add( 1060484, prop.ToString() ); // stamina increase ~1_val~

			if ( (prop = m_AosAttributes.BonusStr) != 0 )
				list.Add( 1060485, prop.ToString() ); // strength bonus ~1_val~

			if ( (prop = m_AosAttributes.WeaponSpeed) != 0 )
				list.Add( 1060486, prop.ToString() ); // swing speed increase ~1_val~%

			base.AddResistanceProperties( list );

			if ( (prop = m_AosClothingAttributes.DurabilityBonus) > 0 )
				list.Add( 1060410, prop.ToString() ); // durability ~1_val~%

			if ( (prop = ComputeStatReq( StatType.Str )) > 0 )
				list.Add( 1061170, prop.ToString() ); // strength requirement ~1_val~

			if ( m_HitPoints >= 0 && m_MaxHitPoints > 0 )
				list.Add( 1060639, "{0}\t{1}", m_HitPoints, m_MaxHitPoints ); // durability ~1_val~ / ~2_val~

			#region Mondain's Legacy Sets
			if ( IsSetItem && !m_SetEquipped )
			{
				list.Add( 1072378 ); // <br>Only when full set is present:				
				GetSetProperties( list );
			}
			#endregion
		}

		public override void OnSingleClick( Mobile from )
		{
			List<EquipInfoAttribute> attrs = new List<EquipInfoAttribute>();

			AddEquipInfoAttributes( from, attrs );

			int number;

			if ( Name == null )
			{
				number = LabelNumber;
			}
			else
			{
				this.LabelTo( from, Name );
				number = 1041000;
			}

			if ( attrs.Count == 0 && Crafter == null && Name != null )
				return;

			EquipmentInfo eqInfo = new EquipmentInfo( number, m_Crafter, false, attrs.ToArray() );

			from.Send( new DisplayEquipmentInfo( this, eqInfo ) );
		}

		public virtual void AddEquipInfoAttributes( Mobile from, List<EquipInfoAttribute> attrs )
		{
			if ( DisplayLootType )
			{
				if ( LootType == LootType.Blessed )
					attrs.Add( new EquipInfoAttribute( 1038021 ) ); // blessed
				else if ( LootType == LootType.Cursed )
					attrs.Add( new EquipInfoAttribute( 1049643 ) ); // cursed
			}

			#region Factions
			if ( m_FactionState != null )
				attrs.Add( new EquipInfoAttribute( 1041350 ) ); // faction item
			#endregion

			if ( m_Quality == ClothingQuality.Exceptional )
				attrs.Add( new EquipInfoAttribute( 1018305 - (int)m_Quality ) );
		}

		#region Serialization
		private static void SetSaveFlag( ref SaveFlag flags, SaveFlag toSet, bool setIf )
		{
			if ( setIf )
				flags |= toSet;
		}

		private static bool GetSaveFlag( SaveFlag flags, SaveFlag toGet )
		{
			return ( (flags & toGet) != 0 );
		}

		[Flags]
		private enum SaveFlag
		{
			None				= 0x00000000,
			Resource			= 0x00000001,
			Attributes			= 0x00000002,
			ClothingAttributes	= 0x00000004,
			SkillBonuses		= 0x00000008,
			Resistances			= 0x00000010,
			MaxHitPoints		= 0x00000020,
			HitPoints			= 0x00000040,
			PlayerConstructed	= 0x00000080,
			Crafter				= 0x00000100,
			Quality				= 0x00000200,
			StrReq				= 0x00000400,

            #region SF Imbuing
			TimesImbued			= 0x12000000,
            #endregion
			#region Spellcrafting
			TimesCrafted		= 0x13000000
            #endregion
		}

		#region Mondain's Legacy Sets		
		private static void SetSaveFlag( ref SetFlag flags, SetFlag toSet, bool setIf )
		{
			if ( setIf )
				flags |= toSet;
		}

		private static bool GetSaveFlag( SetFlag flags, SetFlag toGet )
		{
			return ( (flags & toGet) != 0 );
		}
		
		[Flags]
		private enum SetFlag
		{
			None				= 0x00000000,
			Attributes			= 0x00000001,
			ArmorAttributes		= 0x00000002,
			SkillBonuses		= 0x00000004,
			PhysicalBonus		= 0x00000008,
			FireBonus			= 0x00000010,
			ColdBonus			= 0x00000020,
			PoisonBonus			= 0x00000040,
			EnergyBonus			= 0x00000080,
			SetHue				= 0x00000100,
			LastEquipped		= 0x00000200,
			SetEquipped			= 0x00000400,
			SetSelfRepair		= 0x00000800,
		}
		#endregion

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 8 ); // version
			writer.Write((int)m_TimesImbued); // Imbuing

            writer.Write((Mobile)m_BlessedBy); // Personal Bless Deed

			#region Mondain's Legacy Sets version 6
			SetFlag sflags = SetFlag.None;
			
			SetSaveFlag( ref sflags, SetFlag.Attributes,		!m_SetAttributes.IsEmpty );
			SetSaveFlag( ref sflags, SetFlag.SkillBonuses,		!m_SetSkillBonuses.IsEmpty );
			SetSaveFlag( ref sflags, SetFlag.PhysicalBonus,		m_SetPhysicalBonus != 0 );
			SetSaveFlag( ref sflags, SetFlag.FireBonus,			m_SetFireBonus != 0 );
			SetSaveFlag( ref sflags, SetFlag.ColdBonus,			m_SetColdBonus != 0 );
			SetSaveFlag( ref sflags, SetFlag.PoisonBonus,		m_SetPoisonBonus != 0 );
			SetSaveFlag( ref sflags, SetFlag.EnergyBonus,		m_SetEnergyBonus != 0 );
			SetSaveFlag( ref sflags, SetFlag.SetHue,			m_SetHue != 0 );
			SetSaveFlag( ref sflags, SetFlag.LastEquipped,		m_LastEquipped );			
			SetSaveFlag( ref sflags, SetFlag.SetEquipped,		m_SetEquipped );		
			SetSaveFlag( ref sflags, SetFlag.SetSelfRepair,		m_SetSelfRepair != 0 );
			
			writer.WriteEncodedInt( (int) sflags );
			
			if ( GetSaveFlag( sflags, SetFlag.Attributes ) )
				m_SetAttributes.Serialize( writer );	

			if ( GetSaveFlag( sflags, SetFlag.SkillBonuses ) )
				m_SetSkillBonuses.Serialize( writer );

			if ( GetSaveFlag( sflags, SetFlag.PhysicalBonus ) )
				writer.WriteEncodedInt( (int) m_SetPhysicalBonus );

			if ( GetSaveFlag( sflags, SetFlag.FireBonus ) )
				writer.WriteEncodedInt( (int) m_SetFireBonus );

			if ( GetSaveFlag( sflags, SetFlag.ColdBonus ) )
				writer.WriteEncodedInt( (int) m_SetColdBonus );

			if ( GetSaveFlag( sflags, SetFlag.PoisonBonus ) )
				writer.WriteEncodedInt( (int) m_SetPoisonBonus );

			if ( GetSaveFlag( sflags, SetFlag.EnergyBonus ) )
				writer.WriteEncodedInt( (int) m_SetEnergyBonus );
				
			if ( GetSaveFlag( sflags, SetFlag.SetHue ) )
				writer.WriteEncodedInt( (int) m_SetHue );
				
			if ( GetSaveFlag( sflags, SetFlag.LastEquipped ) )
				writer.Write( (bool) m_LastEquipped );
				
			if ( GetSaveFlag( sflags, SetFlag.SetEquipped ) )
				writer.Write( (bool) m_SetEquipped );
				
			if ( GetSaveFlag( sflags, SetFlag.SetSelfRepair ) )
				writer.WriteEncodedInt( (int) m_SetSelfRepair );
			#endregion

			SaveFlag flags = SaveFlag.None;

			SetSaveFlag( ref flags, SaveFlag.Resource,			m_Resource != DefaultResource );
			SetSaveFlag( ref flags, SaveFlag.Attributes,		!m_AosAttributes.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.ClothingAttributes,!m_AosClothingAttributes.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.SkillBonuses,		!m_AosSkillBonuses.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.Resistances,		!m_AosResistances.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.MaxHitPoints,		m_MaxHitPoints != 0 );
			SetSaveFlag( ref flags, SaveFlag.HitPoints,			m_HitPoints != 0 );
			SetSaveFlag( ref flags, SaveFlag.PlayerConstructed,	m_PlayerConstructed != false );
			SetSaveFlag( ref flags, SaveFlag.Crafter,			m_Crafter != null );
			SetSaveFlag( ref flags, SaveFlag.Quality,			m_Quality != ClothingQuality.Regular );
			SetSaveFlag( ref flags, SaveFlag.StrReq,			m_StrReq != -1 );
            #region SF Imbuing
			SetSaveFlag( ref flags, SaveFlag.TimesImbued,		m_TimesImbued != 0 );
            #endregion
			#region Spellcrafting
			SetSaveFlag( ref flags, SaveFlag.TimesCrafted,		m_TimesCrafted != 0 );
            #endregion

			writer.WriteEncodedInt( (int) flags );

			if ( GetSaveFlag( flags, SaveFlag.Resource ) )
				writer.WriteEncodedInt( (int) m_Resource );

			if ( GetSaveFlag( flags, SaveFlag.Attributes ) )
				m_AosAttributes.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.ClothingAttributes ) )
				m_AosClothingAttributes.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.SkillBonuses ) )
				m_AosSkillBonuses.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.Resistances ) )
				m_AosResistances.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.MaxHitPoints ) )
				writer.WriteEncodedInt( (int) m_MaxHitPoints );

			if ( GetSaveFlag( flags, SaveFlag.HitPoints ) )
				writer.WriteEncodedInt( (int) m_HitPoints );

			if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
				writer.Write( (Mobile) m_Crafter );

			if ( GetSaveFlag( flags, SaveFlag.Quality ) )
				writer.WriteEncodedInt( (int) m_Quality );

			if ( GetSaveFlag( flags, SaveFlag.StrReq ) )
				writer.WriteEncodedInt( (int) m_StrReq );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
            {
				#region Spellcrafting
                case 9:
                    {
                       m_TimesCrafted = reader.ReadInt();
                       goto case 8;
                    }
                #endregion
                #region SF Imbuing
                case 8:
                    {
                       m_TimesImbued = reader.ReadInt();
                       goto case 7;
                    }
                #endregion
                //personal bless deed
                case 7:
                    {
                        m_BlessedBy = reader.ReadMobile();
                        goto case 6;
                    }
				case 6:
				{
					#region Mondain's Legacy Sets
					SetFlag sflags = (SetFlag) reader.ReadEncodedInt();
					
					if ( GetSaveFlag( sflags, SetFlag.Attributes ) )
						m_SetAttributes = new AosAttributes( this, reader );
					else
						m_SetAttributes = new AosAttributes( this );
					
					if ( GetSaveFlag( sflags, SetFlag.ArmorAttributes ) )
						m_SetSelfRepair = (new AosArmorAttributes( this, reader )).SelfRepair;
						
					if ( GetSaveFlag( sflags, SetFlag.SkillBonuses ) )
						m_SetSkillBonuses = new AosSkillBonuses( this, reader );
					else
						m_SetSkillBonuses =  new AosSkillBonuses( this );

					if ( GetSaveFlag( sflags, SetFlag.PhysicalBonus ) )
						m_SetPhysicalBonus = reader.ReadEncodedInt();

					if ( GetSaveFlag( sflags, SetFlag.FireBonus ) )
						m_SetFireBonus = reader.ReadEncodedInt();

					if ( GetSaveFlag( sflags, SetFlag.ColdBonus ) )
						m_SetColdBonus = reader.ReadEncodedInt();

					if ( GetSaveFlag( sflags, SetFlag.PoisonBonus ) )
						m_SetPoisonBonus = reader.ReadEncodedInt();

					if ( GetSaveFlag( sflags, SetFlag.EnergyBonus ) )
						m_SetEnergyBonus = reader.ReadEncodedInt();
						
					if ( GetSaveFlag( sflags, SetFlag.SetHue ) )
						m_SetHue = reader.ReadEncodedInt();
						
					if ( GetSaveFlag( sflags, SetFlag.LastEquipped ) )
						m_LastEquipped = reader.ReadBool();
						
					if ( GetSaveFlag( sflags, SetFlag.SetEquipped ) )
						m_SetEquipped = reader.ReadBool();
						
					if ( GetSaveFlag( sflags, SetFlag.SetSelfRepair ) )
						m_SetSelfRepair = reader.ReadEncodedInt();
					#endregion
					
					goto case 5;
				}
				case 5:
				{
					SaveFlag flags = (SaveFlag)reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.Resource ) )
						m_Resource = (CraftResource)reader.ReadEncodedInt();
					else
						m_Resource = DefaultResource;

					if ( GetSaveFlag( flags, SaveFlag.Attributes ) )
						m_AosAttributes = new AosAttributes( this, reader );
					else
						m_AosAttributes = new AosAttributes( this );

					if ( GetSaveFlag( flags, SaveFlag.ClothingAttributes ) )
						m_AosClothingAttributes = new AosArmorAttributes( this, reader );
					else
						m_AosClothingAttributes = new AosArmorAttributes( this );

					if ( GetSaveFlag( flags, SaveFlag.SkillBonuses ) )
						m_AosSkillBonuses = new AosSkillBonuses( this, reader );
					else
						m_AosSkillBonuses = new AosSkillBonuses( this );

					if ( GetSaveFlag( flags, SaveFlag.Resistances ) )
						m_AosResistances = new AosElementAttributes( this, reader );
					else
						m_AosResistances = new AosElementAttributes( this );

					if ( GetSaveFlag( flags, SaveFlag.MaxHitPoints ) )
						m_MaxHitPoints = reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.HitPoints ) )
						m_HitPoints = reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
						m_Crafter = reader.ReadMobile();

					if ( GetSaveFlag( flags, SaveFlag.Quality ) )
						m_Quality = (ClothingQuality)reader.ReadEncodedInt();
					else
						m_Quality = ClothingQuality.Regular;

					if ( GetSaveFlag( flags, SaveFlag.StrReq ) )
						m_StrReq = reader.ReadEncodedInt();
					else
						m_StrReq = -1;

					if ( GetSaveFlag( flags, SaveFlag.PlayerConstructed ) )
						m_PlayerConstructed = true;

					break;
				}
				case 4:
				{
					m_Resource = (CraftResource)reader.ReadInt();

					goto case 3;
				}
				case 3:
				{
					m_AosAttributes = new AosAttributes( this, reader );
					m_AosClothingAttributes = new AosArmorAttributes( this, reader );
					m_AosSkillBonuses = new AosSkillBonuses( this, reader );
					m_AosResistances = new AosElementAttributes( this, reader );

					goto case 2;
				}
				case 2:
				{
					m_PlayerConstructed = reader.ReadBool();
					goto case 1;
				}
				case 1:
				{
					m_Crafter = reader.ReadMobile();
					m_Quality = (ClothingQuality)reader.ReadInt();
					break;
				}
				case 0:
				{
					m_Crafter = null;
					m_Quality = ClothingQuality.Regular;
					break;
				}
			}
			
			#region Mondain's Legacy Sets
			if ( m_SetAttributes == null )
				m_SetAttributes = new AosAttributes( this );
				
			if ( m_SetSkillBonuses == null )
				m_SetSkillBonuses = new AosSkillBonuses( this );
			#endregion

			if ( version < 2 )
				m_PlayerConstructed = true; // we don't know, so, assume it's crafted

			if ( version < 3 )
			{
				m_AosAttributes = new AosAttributes( this );
				m_AosClothingAttributes = new AosArmorAttributes( this );
				m_AosSkillBonuses = new AosSkillBonuses( this );
				m_AosResistances = new AosElementAttributes( this );
			}

			if ( version < 4 )
				m_Resource = DefaultResource;

			if ( m_MaxHitPoints == 0 && m_HitPoints == 0 )
				m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax( InitMinHits, InitMaxHits );

			Mobile parent = Parent as Mobile;

			if ( parent != null )
			{
				if ( Core.AOS )
					m_AosSkillBonuses.AddTo( parent );

				AddStatBonuses( parent );
				parent.CheckStatTimers();
			}
		}
		#endregion

		public virtual bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public virtual bool Scissor( Mobile from, Scissors scissors )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
				return false;
			}

			if ( Ethics.Ethic.IsImbued( this ) )
			{
				from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
				return false;
			}

			CraftSystem system = DefTailoring.CraftSystem;

			CraftItem item = system.CraftItems.SearchFor( GetType() );

			if ( item != null && item.Resources.Count == 1 && item.Resources.GetAt( 0 ).Amount >= 2 )
			{
				try
				{
					Type resourceType = null;

					CraftResourceInfo info = CraftResources.GetInfo( m_Resource );

					if ( info != null && info.ResourceTypes.Length > 0 )
						resourceType = info.ResourceTypes[0];

					if ( resourceType == null )
						resourceType = item.Resources.GetAt( 0 ).ItemType;

					Item res = (Item)Activator.CreateInstance( resourceType );

					ScissorHelper( from, res, m_PlayerConstructed ? (item.Resources.GetAt( 0 ).Amount / 2) : 1 );

					res.LootType = LootType.Regular;

					return true;
				}
				catch
				{
				}
			}

			from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
			return false;
		}

		public void DistributeBonuses( int amount )
		{
			for ( int i = 0; i < amount; ++i )
			{
				switch ( Utility.Random( 5 ) )
				{
					case 0: ++m_AosResistances.Physical; break;
					case 1: ++m_AosResistances.Fire; break;
					case 2: ++m_AosResistances.Cold; break;
					case 3: ++m_AosResistances.Poison; break;
					case 4: ++m_AosResistances.Energy; break;
				}
			}

			InvalidateProperties();
		}

		#region ICraftable Members

		public virtual int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (ClothingQuality)quality;

			if ( makersMark )
				Crafter = from;

			#region Mondain's Legacy
			if ( !craftItem.ForceNonExceptional )
			{
				if ( DefaultResource != CraftResource.None )
				{
					Type resourceType = typeRes;

					if ( resourceType == null )
						resourceType = craftItem.Resources.GetAt( 0 ).ItemType;

					Resource = CraftResources.GetFromType( resourceType );
				}
				else
				{
					Hue = resHue;
				}
			}
			#endregion

			PlayerConstructed = true;

			CraftContext context = craftSystem.GetContext( from );

			if ( context != null && context.DoNotColor )
				Hue = 0;
			else
				Hue = resHue;


			return quality;
		}

		#endregion

		#region Mondain's Legacy Sets
		public override bool OnDragLift( Mobile from )
		{
			if ( Parent is Mobile && from == Parent )
			{
				if ( IsSetItem && m_SetEquipped )
					SetHelper.RemoveSetBonus( from, SetID, this );
			}

			return base.OnDragLift( from );
		}

		public virtual SetItem SetID{ get{ return SetItem.None; } }
		public virtual int Pieces{ get{ return 0; } }
		public virtual bool MixedSet{ get{ return false; } }

		public bool IsSetItem{ get{ return SetID == SetItem.None ? false : true; } }
		
		private int m_SetHue;
		private bool m_SetEquipped;
		private bool m_LastEquipped;

		[CommandProperty( AccessLevel.GameMaster )]
		public int SetHue
		{
			get{ return m_SetHue; }
			set{ m_SetHue = value; InvalidateProperties(); }
		}

		public bool SetEquipped
		{
			get{ return m_SetEquipped; }
			set{ m_SetEquipped = value; }
		}

		public bool LastEquipped
		{
			get{ return m_LastEquipped; }
			set{ m_LastEquipped = value; }
		}
		
		private AosAttributes m_SetAttributes;
		private AosSkillBonuses m_SetSkillBonuses;
		private int m_SetSelfRepair;

		[CommandProperty( AccessLevel.GameMaster )]
		public AosAttributes SetAttributes
		{
			get{ return m_SetAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosSkillBonuses SetSkillBonuses
		{
			get{ return m_SetSkillBonuses; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int SetSelfRepair
		{
			get{ return m_SetSelfRepair; }
			set{ m_SetSelfRepair = value; InvalidateProperties(); }
		}

		private int m_SetPhysicalBonus, m_SetFireBonus, m_SetColdBonus, m_SetPoisonBonus, m_SetEnergyBonus;

		[CommandProperty( AccessLevel.GameMaster )]
		public int SetPhysicalBonus
		{
			get{ return m_SetPhysicalBonus; }
			set{ m_SetPhysicalBonus = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int SetFireBonus
		{
			get{ return m_SetFireBonus; }
			set{ m_SetFireBonus = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int SetColdBonus
		{
			get{ return m_SetColdBonus; }
			set{ m_SetColdBonus = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int SetPoisonBonus
		{
			get{ return m_SetPoisonBonus; }
			set{ m_SetPoisonBonus = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int SetEnergyBonus
		{ 
			get{ return m_SetEnergyBonus; }
			set{ m_SetEnergyBonus = value; InvalidateProperties(); }
		}

		public virtual void GetSetProperties( ObjectPropertyList list )
		{
			int prop;

			if ( !m_SetEquipped )
			{
				if ( m_SetPhysicalBonus != 0 )
					list.Add( 1072382, m_SetPhysicalBonus.ToString() ); // physical resist +~1_val~%

				if ( m_SetFireBonus != 0 )
					list.Add( 1072383, m_SetFireBonus.ToString() ); // fire resist +~1_val~%

				if ( m_SetColdBonus != 0 )
					list.Add( 1072384, m_SetColdBonus.ToString() ); // cold resist +~1_val~%

				if ( m_SetPoisonBonus != 0 )
					list.Add( 1072385, m_SetPoisonBonus.ToString() ); // poison resist +~1_val~%

				if ( m_SetEnergyBonus != 0 )
					list.Add( 1072386, m_SetEnergyBonus.ToString() ); // energy resist +~1_val~%			
			}

			if ( (prop = m_SetSelfRepair) != 0 )
				list.Add( 1060450, prop.ToString() ); // self repair ~1_val~		

			SetHelper.GetSetProperties( list, this );
		}
		#endregion
	}
}