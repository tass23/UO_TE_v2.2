using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Items;
using Server.Engines.Craft;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
	public enum GemType
	{
		None,
		StarSapphire,
		Emerald,
		Sapphire,
		Ruby,
		Citrine,
		Amethyst,
		Tourmaline,
		Amber,
		Diamond
	}

	public abstract class BaseJewel : Item, ICraftable, ISetItem
	{
		private int m_MaxHitPoints;
		private int m_HitPoints;

		private AosAttributes m_AosAttributes;
		private AosElementAttributes m_AosResistances;
		private AosSkillBonuses m_AosSkillBonuses;
		#region SA
		private SAAbsorptionAttributes m_SAAbsorptionAttributes;
		#endregion
		private CraftResource m_Resource;
		private GemType m_GemType;

		#region Spellcrafting
        private int m_TimesCrafted;
		#endregion
		
        #region SF Imbuing
        private int m_TimesImbued;
		#endregion

		#region Personal Bless Deed
		private Mobile m_BlessedBy;

		[CommandProperty( AccessLevel.GameMaster )] 
		public Mobile BlessedBy
		{
			get { return m_BlessedBy; } 
			set { m_BlessedBy = value;InvalidateProperties();} 
		}
	
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{
			base.GetContextMenuEntries( from, list ); 
			XmlLevelItem levitem = XmlAttach.FindAttachment(this, typeof(XmlLevelItem)) as XmlLevelItem;

            if (levitem != null)
            {
                list.Add(new LevelInfoEntry(from, this, AttributeCategory.Melee));
            }
			if ( BlessedFor == from && BlessedBy == from && RootParent == from )
			{
				list.Add( new UnBlessEntry( from, this ) );
			}
		}
			
		private class UnBlessEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private BaseJewel m_Item;
			
			public UnBlessEntry( Mobile from, BaseJewel item ) : base( 6208, -1 )
			{
				m_From = from;
				m_Item = item;
			}

			public override void OnClick()
			{
				m_Item.BlessedFor = null;
				m_Item.BlessedBy = null;
				
				Container pack = m_From.Backpack;

				if ( pack != null )
				{
					pack.DropItem( new PersonalBlessDeed( m_From ) );
					m_From.SendLocalizedMessage( 1062200 ); // A personal bless deed has been placed in your backpack.
				}
			}
		}
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

		[CommandProperty( AccessLevel.Player )]
		public AosAttributes Attributes
		{
			get{ return m_AosAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosElementAttributes Resistances
		{
			get{ return m_AosResistances; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosSkillBonuses SkillBonuses
		{
			get{ return m_AosSkillBonuses; }
			set{}
		}

		#region SA
		[CommandProperty( AccessLevel.GameMaster )]
		public SAAbsorptionAttributes AbsorptionAttributes
		{
			get { return m_SAAbsorptionAttributes; }
			set { }
		}
		#endregion

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; Hue = CraftResources.GetHue( m_Resource ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public GemType GemType
		{
			get{ return m_GemType; }
			set{ m_GemType = value; InvalidateProperties(); }
		}

		#region Spellcrafting
		[CommandProperty( AccessLevel.GameMaster )]
		public int TimesCrafted{ get{ return m_TimesCrafted; } set{ m_TimesCrafted = value; InvalidateProperties(); } }
        #endregion
		
        #region SF Imbuing
		[CommandProperty( AccessLevel.GameMaster )]
		public int TimesImbued{ get{ return m_TimesImbued; } set{ m_TimesImbued = value; InvalidateProperties(); } }
        #endregion

		public override int PhysicalResistance{ get{ return m_AosResistances.Physical; } }
		public override int FireResistance{ get{ return m_AosResistances.Fire; } }
		public override int ColdResistance{ get{ return m_AosResistances.Cold; } }
		public override int PoisonResistance{ get{ return m_AosResistances.Poison; } }
		public override int EnergyResistance{ get{ return m_AosResistances.Energy; } }
		public virtual int BaseGemTypeNumber{ get{ return 0; } }

		public virtual int InitMinHits{ get{ return 0; } }
		public virtual int InitMaxHits{ get{ return 0; } }

		#region SA
		public virtual Race RequiredRace { get { return null; } }
		public virtual bool CanBeWornByGargoyles{ get{ return false; } }
		#endregion

		public override int LabelNumber
		{
			get
			{
				if ( m_GemType == GemType.None )
					return base.LabelNumber;

				return BaseGemTypeNumber + (int)m_GemType - 1;
			}
		}

		public override void OnAfterDuped( Item newItem )
		{
			BaseJewel jewel = newItem as BaseJewel;

			if ( jewel == null )
				return;

			jewel.m_AosAttributes = new AosAttributes( newItem, m_AosAttributes );
			jewel.m_AosResistances = new AosElementAttributes( newItem, m_AosResistances );
			jewel.m_AosSkillBonuses = new AosSkillBonuses( newItem, m_AosSkillBonuses );

			#region Mondain's Legacy
			jewel.m_SetAttributes = new AosAttributes( newItem, m_SetAttributes );
			jewel.m_SetSkillBonuses = new AosSkillBonuses( newItem, m_SetSkillBonuses );
			#endregion
		}

		public virtual int ArtifactRarity{ get{ return 0; } }
		
		#region Mondain's Legacy
		private Mobile m_Crafter;
		private ArmorQuality m_Quality;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public ArmorQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}
		#endregion

		public BaseJewel( int itemID, Layer layer ) : base( itemID )
		{
			m_AosAttributes = new AosAttributes( this );
			m_AosResistances = new AosElementAttributes( this );
			m_AosSkillBonuses = new AosSkillBonuses( this );
			m_Resource = CraftResource.Iron;
			m_GemType = GemType.None;

			Layer = layer;
			#region UO-The Expanse XmlSockets
			// Xml Spawner 3.26c XmlSockets - SOF
			// mod to randomly add sockets and socketability features to weapons. These settings will yield
			// 2% drop rate of socketed/socketable items
			// 0.1% chance of 5 sockets
			// 0.5% of 4 sockets
			// 3% chance of 3 sockets
			// 15% chance of 2 sockets
			// 50% chance of 1 socket
			// the remainder will be 0 socket (31.4% in this case)
			// uncomment the next line to prevent artifacts from being socketed
			// if(ArtifactRarity == 0)
			XmlSockets.ConfigureRandom(this, 2.0, 0.1, 0.5, 3.0, 15.0, 50.0);
			// Xml Spawner 3.26c XmlSockets - EOF
			#endregion

			m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax( InitMinHits, InitMaxHits );

			#region Mondain's Legacy Sets
			m_SetAttributes = new AosAttributes( this );
			m_SetSkillBonuses = new AosSkillBonuses( this );
			#endregion

            #region SA
			m_SAAbsorptionAttributes = new SAAbsorptionAttributes( this );
			#endregion
        }

		#region SA
        public override bool CanEquip(Mobile from)
        {
            if (BlessedBy != null && BlessedBy != from)
            {
                from.SendLocalizedMessage(1075277); // That item is blessed by another player.
                return false;
            }

			if ( from.AccessLevel < AccessLevel.GameMaster )
			{
				if ( from.Race == Race.Gargoyle && !CanBeWornByGargoyles )
				{
					from.SendLocalizedMessage( 1111708 ); // Gargoyles can't wear this.
					return false;
        }
				else if ( RequiredRace != null && from.Race != RequiredRace )
				{
					if( RequiredRace == Race.Elf )
						from.SendLocalizedMessage( 1072203 ); // Only Elves may use this.
					else if ( RequiredRace == Race.Gargoyle )
						from.SendLocalizedMessage( 1111707 ); // Only gargoyles can wear this.
					else
						from.SendMessage( "Only {0} may use this.", RequiredRace.PluralName );

					return false;
				}
			}
        
			return base.CanEquip( from );
		}
        #endregion

		public override void OnAdded( object parent )
		{
			if ( Core.AOS && parent is Mobile )
			{
				Mobile from = (Mobile)parent;

				m_AosSkillBonuses.AddTo( from );

				int strBonus = m_AosAttributes.BonusStr;
				int dexBonus = m_AosAttributes.BonusDex;
				int intBonus = m_AosAttributes.BonusInt;

				if ( strBonus != 0 || dexBonus != 0 || intBonus != 0 )
				{
					string modName = this.Serial.ToString();

					if ( strBonus != 0 )
						from.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

					if ( dexBonus != 0 )
						from.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

					if ( intBonus != 0 )
						from.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
				}

				from.CheckStatTimers();

				#region Mondain's Legacy Sets
				if ( IsSetItem )
				{
					m_SetEquipped = SetHelper.FullSetEquipped( from, SetID, Pieces );
				
					if ( m_SetEquipped )
					{
						m_LastEquipped = true;							
						SetHelper.AddSetBonus( from, SetID );
					}
				}
				#endregion
			}
		}

		public override void OnRemoved( object parent )
		{
			if ( Core.AOS && parent is Mobile )
			{
				Mobile from = (Mobile)parent;

				m_AosSkillBonuses.Remove();

				string modName = this.Serial.ToString();

				from.RemoveStatMod( modName + "Str" );
				from.RemoveStatMod( modName + "Dex" );
				from.RemoveStatMod( modName + "Int" );

				from.CheckStatTimers();

				#region Mondain's Legacy Sets
				if ( IsSetItem && m_SetEquipped )
					SetHelper.RemoveSetBonus( from, SetID, this );
				#endregion
			}
		}

		public BaseJewel( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			BaseJewel jew = this;

			int props = 0;
			foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			{
				if ( jew != null && jew.Attributes[ (AosAttribute)i ] > 0 ) ++props;
				if ( jew != null && jew.SetAttributes[ (AosAttribute)i ] > 0 ) ++props;
			}
			if ( jew != null ){ foreach( int i in Enum.GetValues(typeof( AosElementAttribute)) ) if ( jew.Resistances[ (AosElementAttribute)i ] > 0 ) ++props;}
			if(this.SkillBonuses.Skill_1_Value > 0) ++props;
			if(this.SkillBonuses.Skill_2_Value > 0) ++props;
			if(this.SkillBonuses.Skill_3_Value > 0) ++props;
			if(this.SkillBonuses.Skill_4_Value > 0) ++props;
			if(this.SkillBonuses.Skill_5_Value > 0) ++props;

			base.GetProperties( list );

			if( props <= 0  ) 		//  No color (Basic)
			{
				list.Add("<BASEFONT COLOR=#FFFFFF><BIG>[Common]</BIG><BASEFONT COLOR=#D3D3D3>");
			}
			else if( props >= 1 && props <= 2 ) 	// Green (Uncommon)
			{
				list.Add("<BASEFONT COLOR=#1EFF00><BIG>[Uncommon]</BIG><BASEFONT COLOR=#8FED82>");
			}
			else if( props >= 3 && props <= 4 ) 	// Blue (Rare)
			{
				list.Add("<BASEFONT COLOR=#0070FF><BIG>[Rare]</BIG><BASEFONT COLOR=#7CB3F8>");
			}
			else if( props >= 5 && props <= 6 ) 	// Purple (Epic)
			{
				list.Add("<BASEFONT COLOR=#A335EE><BIG>[Epic]</BIG><BASEFONT COLOR=#C58AED>");
			}
			else if( props >= 7 && props < 10 ) 	// Orange (Legendary)
			{
				list.Add("<BASEFONT COLOR=#FF8000><BIG>[Legendary]</BIG><BASEFONT COLOR=#FAC48E>");
			}
			else
			{
				list.Add("<BASEFONT COLOR=#FFFFFF><BIG>[Common]</BIG><BASEFONT COLOR=#D3D3D3>");
			}
			
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
			
			#region Mondain's Legacy
			if ( m_Quality == ArmorQuality.Exceptional )
				list.Add( 1063341 ); // exceptional
				
			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~
			#endregion

			#region Mondain's Legacy Sets
			if ( IsSetItem )
			{
				list.Add( 1080240, Pieces.ToString() ); // Part of a Jewelry Set (~1_val~ pieces)
					
				if ( m_SetEquipped )
				{
					list.Add( 1080241 ); // Full Jewelry Set Present					
					SetHelper.GetSetProperties( list, this );
				}
			}
			#endregion

			m_AosSkillBonuses.GetProperties( list );

			int prop;

			#region SA
			if ( RequiredRace == Race.Elf )
				list.Add( 1075086 ); // Elves Only
			else if ( RequiredRace == Race.Gargoyle )
				list.Add( 1111709 ); // Gargoyles Only
			#endregion

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

			if ( (prop = m_AosAttributes.Luck) != 0 )
				list.Add( 1060436, prop.ToString() ); // luck ~1_val~

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

			#region SA
			if ( (prop = m_SAAbsorptionAttributes.CastingFocus) != 0 )
				list.Add( 1113696, prop.ToString() ); // Casting Focus ~1_val~%

			if ( (prop = m_SAAbsorptionAttributes.EaterFire) != 0 )
				list.Add( 1113593, prop.ToString() ); // Fire Eater ~1_Val~%

			if ( (prop = m_SAAbsorptionAttributes.EaterCold) != 0 )
				list.Add( 1113594, prop.ToString() ); // Cold Eater ~1_Val~%

			if ( (prop = m_SAAbsorptionAttributes.EaterPoison) != 0 )
				list.Add( 1113595, prop.ToString() ); // Poison Eater ~1_Val~%

			if ( (prop = m_SAAbsorptionAttributes.EaterEnergy) != 0 )
				list.Add( 1113596, prop.ToString() ); // Energy Eater ~1_Val~%

			if ( (prop = m_SAAbsorptionAttributes.EaterKinetic) != 0 )
				list.Add( 1113597, prop.ToString() ); // Kinetic Eater ~1_Val~%

			if ( (prop = m_SAAbsorptionAttributes.EaterDamage) != 0 )
				list.Add( 1113598, prop.ToString() ); // Damage Eater ~1_Val~%

			if ( (prop = m_SAAbsorptionAttributes.ResonanceFire) != 0 )
				list.Add( 1113691, prop.ToString() ); // Fire Resonance ~1_val~%

			if ( (prop = m_SAAbsorptionAttributes.ResonanceCold) != 0 )
				list.Add( 1113692, prop.ToString() ); // Cold Resonance ~1_val~%

			if ( (prop = m_SAAbsorptionAttributes.ResonancePoison) != 0 )
				list.Add( 1113693, prop.ToString() ); // Poison Resonance ~1_val~%

			if ( (prop = m_SAAbsorptionAttributes.ResonanceEnergy) != 0 )
				list.Add( 1113694, prop.ToString() ); // Energy Resonance ~1_val~%

			if ( (prop = m_SAAbsorptionAttributes.ResonanceKinetic) != 0 )
				list.Add( 1113695, prop.ToString() ); // Kinetic Resonance ~1_val~%
			#endregion

			base.AddResistanceProperties( list );
			#region UO-The Expanse
			// mod to display attachment properties
        		Server.Engines.XmlSpawner2.XmlAttach.AddAttachmentProperties(this, list);
			#endregion
			if ( m_HitPoints >= 0 && m_MaxHitPoints > 0 )
				list.Add( 1060639, "{0}\t{1}", m_HitPoints, m_MaxHitPoints ); // durability ~1_val~ / ~2_val~
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 9 ); // version 8
			
			#region Spellcrafting
			writer.WriteEncodedInt( (int) m_TimesCrafted );
			#endregion
			
            #region SF Imbuing
			writer.WriteEncodedInt( (int) m_TimesImbued );
			#endregion
			
			m_SAAbsorptionAttributes.Serialize( writer );

			writer.WriteEncodedInt( (int) m_MaxHitPoints );
			writer.WriteEncodedInt( (int) m_HitPoints );

			writer.Write( (Mobile)m_BlessedBy ); // Personal Bless Deed

			#region Mondain's Legacy Sets version 4
			writer.Write( (bool) m_LastEquipped );
			writer.Write( (bool) m_SetEquipped );
			writer.WriteEncodedInt( (int) m_SetHue );

			m_SetAttributes.Serialize( writer );
			m_SetSkillBonuses.Serialize( writer );
			#endregion
			
			#region Mondain's Legacy version 3
			writer.Write( (Mobile) m_Crafter );
			writer.Write( (int) m_Quality );
			#endregion

			writer.WriteEncodedInt( (int) m_Resource );
			writer.WriteEncodedInt( (int) m_GemType );

			m_AosAttributes.Serialize( writer );
			m_AosResistances.Serialize( writer );
			m_AosSkillBonuses.Serialize( writer );
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
                       m_TimesCrafted = reader.ReadEncodedInt();
                       goto case 8;
                    }
                #endregion
                #region SF Imbuing
                case 8:
                    {
                       m_TimesImbued = reader.ReadEncodedInt();
                       goto case 7;
                    }
                #endregion
                case 7:
                    {
                        m_SAAbsorptionAttributes = new SAAbsorptionAttributes(this, reader);
                        goto case 6;
                    }
				case 6:
				{
                        if (version == 6)
                            m_SAAbsorptionAttributes = new SAAbsorptionAttributes(this);

					m_MaxHitPoints = reader.ReadEncodedInt();
					m_HitPoints = reader.ReadEncodedInt();

					goto case 5;
				}
				//personal bless deed
				case 5:
				{
					m_BlessedBy = reader.ReadMobile();
					goto case 4;
				}
				#region Mondain's Legacy Sets
				case 4:
				{
					m_LastEquipped = reader.ReadBool();
					m_SetEquipped = reader.ReadBool();
					m_SetHue = reader.ReadEncodedInt();

					m_SetAttributes = new AosAttributes( this, reader );
					m_SetSkillBonuses = new AosSkillBonuses( this, reader );

					goto case 3;
				}
				#endregion
				#region Mondain's Legacy
				case 3:
				{
					m_Crafter = reader.ReadMobile();
					m_Quality = (ArmorQuality) reader.ReadInt();
										
					goto case 2;
				}
				#endregion
				case 2:
				{
					m_Resource = (CraftResource)reader.ReadEncodedInt();
					m_GemType = (GemType)reader.ReadEncodedInt();

					goto case 1;
				}
				case 1:
				{
					m_AosAttributes = new AosAttributes( this, reader );
					m_AosResistances = new AosElementAttributes( this, reader );
					m_AosSkillBonuses = new AosSkillBonuses( this, reader );

					if ( Core.AOS && Parent is Mobile )
						m_AosSkillBonuses.AddTo( (Mobile)Parent );

					int strBonus = m_AosAttributes.BonusStr;
					int dexBonus = m_AosAttributes.BonusDex;
					int intBonus = m_AosAttributes.BonusInt;

					if ( Parent is Mobile && (strBonus != 0 || dexBonus != 0 || intBonus != 0) )
					{
						Mobile m = (Mobile)Parent;

						string modName = Serial.ToString();

						if ( strBonus != 0 )
							m.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

						if ( dexBonus != 0 )
							m.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

						if ( intBonus != 0 )
							m.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
					}

					if ( Parent is Mobile )
						((Mobile)Parent).CheckStatTimers();

					break;
				}
				case 0:
				{
					m_AosAttributes = new AosAttributes( this );
					m_AosResistances = new AosElementAttributes( this );
					m_AosSkillBonuses = new AosSkillBonuses( this );

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
			{
				m_Resource = CraftResource.Iron;
				m_GemType = GemType.None;
			}
		}
		#region ICraftable Members

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Type resourceType = typeRes;

			if ( resourceType == null )
				resourceType = craftItem.Resources.GetAt( 0 ).ItemType;

			#region Mondain's Legacy
			if ( !craftItem.ForceNonExceptional )
				Resource = CraftResources.GetFromType( resourceType );
			#endregion

			CraftContext context = craftSystem.GetContext( from );

			if ( context != null && context.DoNotColor )
				Hue = 0;
			else
			Hue = resHue;


			if ( 1 < craftItem.Resources.Count )
			{
				resourceType = craftItem.Resources.GetAt( 1 ).ItemType;

				if ( resourceType == typeof( StarSapphire ) )
					GemType = GemType.StarSapphire;
				else if ( resourceType == typeof( Emerald ) )
					GemType = GemType.Emerald;
				else if ( resourceType == typeof( Sapphire ) )
					GemType = GemType.Sapphire;
				else if ( resourceType == typeof( Ruby ) )
					GemType = GemType.Ruby;
				else if ( resourceType == typeof( Citrine ) )
					GemType = GemType.Citrine;
				else if ( resourceType == typeof( Amethyst ) )
					GemType = GemType.Amethyst;
				else if ( resourceType == typeof( Tourmaline ) )
					GemType = GemType.Tourmaline;
				else if ( resourceType == typeof( Amber ) )
					GemType = GemType.Amber;
				else if ( resourceType == typeof( Diamond ) )
					GemType = GemType.Diamond;
			}
			
			#region Mondain's Legacy
			m_Quality = (ArmorQuality) quality;
			
			if ( makersMark )
				m_Crafter = from;
			#endregion

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
		#endregion
	}
}