#region AuthorHeader
//
//	SpellCrafting version 3.0, by Xanthos and TheOutkastDev
//
//  Based on original ideas and code by TheOutkastDev
//
#endregion AuthorHeader
using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;
using Server.Engines.Craft;
using Server.SpellCrafting.Items;

namespace Server.SpellCrafting
{		
	public class CraftState 
	{
		public int Id;
		public BookOfSpellCrafts Book; 
		public CraftState( BookOfSpellCrafts book, int i  ) { Book = book; Id = i; }
	}

	public class SpellCraft
	{
		#region Arrays and Enums

		internal static string [] MessageList = 
		{
			"",
			"New properties cannot be applied to this item as it meets or exceeds the maximum number of properties allowed.",
			"You can only spellcraft items in your backpack.",
			"You cannot spellcraft artifact items.",
			"You can only spellcraft jewelry that are bracelets or rings.",

			"You don't have enough magic jewels to affix this craft's magic to the item.",
			"The magics erupt violently and the item is destroyed.",
			"The energy inside the jewel erupts violently, but the item remains intact.",
			"This craft may only be placed on",
			" armor",

			" shields",
			" weapons",
			" jewelry",
			" armor,",
			" shields,",

			" weapons,",
			" hats",
			" and shields",
			" and weapons",
			" and hats",

			" and jewelry",
			" and instruments",
			" and ",
			"You successfully apply the craft to the item.",
			"This item has been Imbued and cannot be used for Spellcrafting.",
			"This item has been Empowered and cannot be used for Spellcrafting."
		};

		public enum MsgNums
		{
			Unused = 0,
			MaxProperties,
			BackPack,
			Artifact,
			BraceletsAndRingsOnly,

			NotEnoughJewels,
			ItemDestoyed,
			ItemIntact,
			CraftRestriction,
			Armor,

			Shields,
			Weapons,
			Jewelry,
			ArmorComma,
			ShieldsComma,

			WeaponsComma,
			HatsComma,
			AndShields,
			AndWeapons,
			AndHats,

			AndJewelry,
			AndInstruments,
			And,
			Success,
			Imbued,
			Empowered
		};

		public static Type[] m_Loot = new Type[]
		{
			typeof( BonusStrengthJewel ),		typeof( BonusDexterityJewel ), 		typeof( BonusIntelligenceJewel ),
			typeof( BonusHitsJewel ),		typeof( BonusStaminaJewel ),		typeof( BonusManaJewel ),
			typeof( PhysicalResistJewel ),	typeof( FireResistJewel ),		typeof( ColdResistJewel ),
			typeof( PoisonResistJewel ),	typeof( EnergyResistJewel ),	typeof( RegenerateHitsJewel ),
			typeof( RegenerateManaJewel ),		typeof( RegenerateStaminaJewel ),		typeof( CastRecoveryJewel ),
			typeof( CastSpeedJewel ),		typeof( LowerManaCostJewel ),	typeof( LowerReagentCostJewel ),
			typeof( MageArmorJewel ),		typeof( MageWeaponJewel ),		typeof( SpellChannelingJewel ),
			typeof( SpellDamageJewel ),		typeof( HitColdAreaJewel ),		typeof( HitEnergyAreaJewel ),
			typeof( HitFireAreaJewel ),		typeof( HitPhysicalAreaJewel ),	typeof( HitPoisonAreaJewel ),
			typeof( HitDispelJewel ),		typeof( HitFireballJewel ),		typeof( HitHarmJewel ),
			typeof( HitLightningJewel ),	typeof( HitMagicArrowJewel ),	typeof( HitLowerAttackJewel ),
			typeof( HitLowerDefendJewel ),	typeof( HitLeechHitsJewel ),	typeof( HitLeechManaJewel ),
			typeof( HitLeechStamJewel ),	typeof( UseBestSkillJewel ),	typeof( WeaponDamageJewel ),
			typeof( WeaponSpeedJewel ),		typeof( AttackChanceJewel ),	typeof( DefendChanceJewel ),
			typeof( EnhancePotionsJewel ),	typeof( LowerStatRequirementsJewel ),	typeof( LuckJewel ),
			typeof( ReflectPhysicalJewel ),	typeof( SelfRepairJewel ),		typeof( NightSightJewel ),	
			typeof( SlayerJewel ),			typeof( DurabilityJewel ),

		};
		#endregion

		#region Public Methods

		public static string GetCraftName( int craftID )
		{
			string craft = "";

			switch ( craftID )
			{
				case 0: craft = "Strength Bonus" ; break;
				case 1: craft = "Dexterity Bonus"; break;
				case 2: craft = "Intelligence Bonus"; break;
				case 3: craft = "Hit Point Bonus"; break;
				case 4: craft = "Stamina Bonus"; break;
				case 5: craft = "Mana Bonus"; break;
				case 6: craft = "Physical Resist Bonus"; break;
				case 7: craft = "Fire Resist Bonus"; break;
				case 8: craft = "Cold Resist Bonus"; break;
				case 9: craft = "Poison Resist Bonus"; break;
				case 10: craft = "Energy Resist Bonus"; break;
				case 11: craft = "Hit Point Rengeration"; break;
				case 12: craft = "Mana Regeneration"; break;
				case 13: craft = "Stamina Regeneration"; break;
				case 14: craft = "Faster Cast Recovery"; break;
				case 15: craft = "Faster Cast Speed"; break;
				case 16: craft = "Lower Mana Cost"; break;
				case 17: craft = "Lower Reagent Cost"; break;
				case 18: craft = "Mage Armor"; break;
				case 19: craft = "Mage Weapon"; break;
				case 20: craft = "Spell Channeling"; break;
				case 21: craft = "Spell Damage Increase"; break;
				case 22: craft = "Hit Cold Area"; break;
				case 23: craft = "Hit Energy Area"; break;
				case 24: craft = "Hit Fire Area"; break;
				case 25: craft = "Hit Physical Area"; break;
				case 26: craft = "Hit Poison Area"; break;
				case 27: craft = "Hit Dispel"; break;
				case 28: craft = "Hit Fireball"; break;
				case 29: craft = "Hit Harm"; break;
				case 30: craft = "Hit Lightning"; break;
				case 31: craft = "Hit Magic Arrow"; break;
				case 32: craft = "Hit Lower Attack"; break;
				case 33: craft = "Hit Lower Defense"; break;
				case 34: craft = "Hit Life Leech"; break;
				case 35: craft = "Hit Mana Leech"; break;
				case 36: craft = "Hit Stamina Leech"; break;
				case 37: craft = "Use Best Weapon Skill"; break;
				case 38: craft = "Weapon Damage Increase"; break;
				case 39: craft = "Swing Speed Increase"; break;
				case 40: craft = "Hit Chance Increase"; break;
				case 41: craft = "Defense Chance Increase"; break;
				case 42: craft = "Enhance Potions"; break;
				case 43: craft = "Lower Stat Requirements"; break;
				case 44: craft = "Luck"; break;
				case 45: craft = "Reflect Physical"; break;
				case 46: craft = "Self Repair"; break;
				case 47: craft = "Night Sight"; break;
				case 48: craft = "Slayer"; break;
				case 49: craft = "Durability"; break;
			}
			return craft;
		}

		public static Item RandomCraft()
		{
			return Activator.CreateInstance( m_Loot[Utility.Random( m_Loot.Length )] ) as Item;
		}

		public static bool SufficientSkillToCraft( Mobile crafter )
		{
			return ( (int)(crafter.Skills[SkillName.Inscribe]).Value >= SpellCraftConfig.MinimumInscription
				&& (int)(crafter.Skills[SkillName.Alchemy]).Value >= SpellCraftConfig.MinimumAlchemy );
		}

		public static void MarkArmor( Mobile crafter, BaseArmor armor )
		{
			if ( false == armor.PlayerConstructed
				&& (crafter.Skills[SkillName.Inscribe]).Value == SpellCraftConfig.MaximumSkillValue
				&& (crafter.Skills[SkillName.Alchemy]).Value == SpellCraftConfig.MaximumSkillValue
				&& Utility.RandomDouble() < SpellCraftConfig.CraftersMarkChance )
			{
				armor.PlayerConstructed = true;
				armor.Crafter = crafter;
			}
		}

		public static void MarkWeapon( Mobile crafter, BaseWeapon weapon )
		{
			if ( false == weapon.PlayerConstructed
				&& (crafter.Skills[SkillName.Inscribe]).Value == SpellCraftConfig.MaximumSkillValue
				&& (crafter.Skills[SkillName.Alchemy]).Value == SpellCraftConfig.MaximumSkillValue
				&& Utility.RandomDouble() < SpellCraftConfig.CraftersMarkChance )
			{
				weapon.PlayerConstructed = true;
				weapon.Crafter = crafter;
			}
		}

		public static void MarkHat( Mobile crafter, BaseHat hat )
		{
			if ( false == hat.PlayerConstructed
				&& (crafter.Skills[SkillName.Inscribe]).Value == SpellCraftConfig.MaximumSkillValue
				&& (crafter.Skills[SkillName.Alchemy]).Value == SpellCraftConfig.MaximumSkillValue
				&& Utility.RandomDouble() < SpellCraftConfig.CraftersMarkChance )
			{
				hat.PlayerConstructed = true;
				hat.Crafter = crafter;
			}
		}

		public static void MarkInstrument( Mobile crafter, BaseInstrument instrument )
		{
			if ( null == instrument.Crafter
				&& (crafter.Skills[SkillName.Inscribe]).Value == SpellCraftConfig.MaximumSkillValue
				&& (crafter.Skills[SkillName.Alchemy]).Value == SpellCraftConfig.MaximumSkillValue
				&& Utility.RandomDouble() < SpellCraftConfig.CraftersMarkChance )
			{
				instrument.Crafter = crafter;
			}
		}

		// Apply AoS Armor Attributes

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseArmor armor, AosAttribute attribute, int minimum, int maximum )
		{
			CheckItem( from, armor, ( armor.Attributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, armor, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( armor.Attributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum );
			MarkArmor( from, armor );
		}

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseArmor armor, AosArmorAttribute attribute, int minimum, int maximum )
		{
			CheckItem( from, armor, ( armor.ArmorAttributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, armor, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			SpellCraft.ApplyAttribute( armor.ArmorAttributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum );
			MarkArmor( from, armor );
		}

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseArmor armor, AosArmorAttribute attribute, int minimum, int maximum, int scale )
		{
			CheckItem( from, armor, ( armor.ArmorAttributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, armor, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( armor.ArmorAttributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum, scale );
			MarkArmor( from, armor );
		}

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseShield shield, AosAttribute attribute, int minimum, int maximum, int scale )
		{
			CheckItem( from, shield, ( shield.Attributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, shield, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( shield.Attributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum, scale );
			MarkArmor( from, shield );
		}

		public static void ApplyResistance( Mobile from, BookOfSpellCrafts book, int craftId, BaseArmor armor, ResistanceType resistance, int minimum, int maximum )
		{
			CheckItem( from, armor, false );
			UseMagicJewels( from, book, armor, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyResistance( armor, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, resistance, minimum, maximum );
			MarkArmor( from, armor );
		}

		// Apply AoS Weapons Attributes

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseWeapon weapon, AosAttribute attribute, int minimum, int maximum )
		{
			CheckItem( from, weapon, ( weapon.Attributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, weapon, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( weapon.Attributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum );
			MarkWeapon( from, weapon );
		}

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseWeapon weapon, AosAttribute attribute, int minimum, int maximum, int scale )
		{
			CheckItem( from, weapon, ( weapon.Attributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, weapon, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( weapon.Attributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum, scale );
			MarkWeapon( from, weapon );
		}

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseWeapon weapon, AosWeaponAttribute attribute, int minimum, int maximum )
		{
			CheckItem( from, weapon, ( weapon.WeaponAttributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, weapon, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( weapon.WeaponAttributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum );
			MarkWeapon( from, weapon );
		}

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseWeapon weapon, AosWeaponAttribute attribute, int minimum, int maximum, int scale )
		{
			CheckItem( from, weapon, ( weapon.WeaponAttributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, weapon, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( weapon.WeaponAttributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum, scale );
			MarkWeapon( from, weapon );
		}

		// Apply AoS Hat Attributes

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseHat hat, AosAttribute attribute, int minimum, int maximum )
		{
			CheckItem( from, hat, ( hat.Attributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, hat, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( hat.Attributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum );
			MarkHat( from, hat );
		}

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseHat hat, AosArmorAttribute attribute, int minimum, int maximum )
		{
			CheckItem( from, hat, ( hat.ClothingAttributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, hat, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			SpellCraft.ApplyAttribute( hat.ClothingAttributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum );
			MarkHat( from, hat );
		}

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseHat hat, AosArmorAttribute attribute, int minimum, int maximum, int scale )
		{
			CheckItem( from, hat, ( hat.ClothingAttributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, hat, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( hat.ClothingAttributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum, scale );
			MarkHat( from, hat );
		}

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseHat hat, AosElementAttribute attribute, int minimum, int maximum )
		{
			CheckItem( from, hat, ( hat.Resistances[ attribute ] == 0 ) );
			UseMagicJewels( from, book, hat, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( hat.Resistances, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum );
			MarkHat( from, hat );
		}
		
		// Apply AoS Jewelry Attributes

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseJewel jewel, AosAttribute attribute, int minimum, int maximum )
		{
			CheckItem( from, jewel, ( jewel.Attributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, jewel, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( jewel.Attributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum );
		}

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseJewel jewel, AosAttribute attribute, int minimum, int maximum, int scale )
		{
			CheckItem( from, jewel, ( jewel.Attributes[ attribute ] == 0 ) );
			UseMagicJewels( from, book, jewel, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( jewel.Attributes, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum, scale );
		}

		public static void ApplyAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseJewel jewel, AosElementAttribute attribute, int minimum, int maximum )
		{
			CheckItem( from, jewel, ( jewel.Resistances[ attribute ] == 0 ) );
			UseMagicJewels( from, book, jewel, SpellCraftConfig.MagicJewelRequirements[craftId] );
			int scalar = ComputeSkillScalar( from );
			ApplyAttribute( jewel.Resistances, SpellCraftConfig.MinimumIntensity * scalar, SpellCraftConfig.MaximumIntensity * scalar, attribute, minimum, maximum );
		}

		public static void ApplySlayerAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseWeapon weapon )
		{
			CheckItem( from, weapon, ( weapon.Slayer == 0 ) );
			UseMagicJewels( from, book, weapon, SpellCraftConfig.MagicJewelRequirements[craftId] );
			do 
			{
				weapon.Slayer = GetRandomSlayer( ComputeSkillScalar( from ) );
			}
			while ( weapon.Slayer == SlayerName.None );
			MarkWeapon( from, weapon );
		}

		public static void ApplySlayerAttribute( Mobile from, BookOfSpellCrafts book, int craftId, BaseInstrument instrument )
		{
			CheckItem( from, instrument );
			UseMagicJewels( from, book, instrument, SpellCraftConfig.MagicJewelRequirements[craftId] );
			do 
			{
				instrument.Slayer = GetRandomSlayer( ComputeSkillScalar( from ) );
			}
			while ( instrument.Slayer == SlayerName.None );
			MarkInstrument( from, instrument );
		}

		public static void AdjustFastCast( BaseShield shield )
		{
			AdjustFastCast( shield.Attributes );
		}

		public static void AdjustFastCast( BaseWeapon weapon )
		{
			AdjustFastCast( weapon.Attributes );
		}

		public static string AssembleMessage( params SpellCraft.MsgNums[] msgIds )
		{
			string message = SpellCraft.MessageList[ (int)(SpellCraft.MsgNums.CraftRestriction) ];

			for( int i = 0; i < msgIds.Length; i++ )
			{
				int id = (int)msgIds[i];

				message = message + ( ( id < 0 || id >= SpellCraft.MessageList.Length ) ? "" : SpellCraft.MessageList[ id ] );
			}
			message += ".";

			return message;
		}


		#endregion Public Methods

		#region Private Methods

		private static int ComputeSkillScalar( Mobile from )
		{
			int skillValue = (int)(from.Skills[SkillName.Inscribe].Value + from.Skills[SkillName.Alchemy].Value);

			return (int)( Math.Min( skillValue, SpellCraftConfig.MaximumSkillValue ) / ( SpellCraftConfig.MaximumSkillValue / 10 ));
		}

		private static void AdjustFastCast( AosAttributes attrs )
		{
			if ( Utility.RandomDouble() < SpellCraftConfig.ScPenaltyChance )
			{
				// If the item has 0 or less fc pin the value to 0, otherwise subtract -1 from the fc
				attrs[AosAttribute.CastSpeed] = attrs[AosAttribute.CastSpeed] <= 0 ? -1 : attrs[AosAttribute.CastSpeed] - 1;
			}
		}

		private static int Scale( int min, int max, int low, int high )
		{
			int difference = high - low;

			if ( difference == 1 )
				return Utility.RandomMinMax( low, high );
			else
			{
				return low + (int)(Math.Round( ((Double)( difference * Utility.RandomMinMax( min, max ) )) / 100 ));
			}
		}

		private static void ApplyAttribute( AosAttributes attrs, int min, int max, AosAttribute attr, int low, int high )
		{
			attrs[attr] = Scale( min, max, low, high );
		}

		private static void ApplyAttribute( AosAttributes attrs, int min, int max, AosAttribute attr, int low, int high, int scale )
		{
			attrs[attr] = Scale( min, max, low / scale, high / scale ) * scale;
		}

		private static void ApplyAttribute( AosArmorAttributes attrs, int min, int max, AosArmorAttribute attr, int low, int high )
		{
			attrs[attr] = Scale( min, max, low, high );
		}

		private static void ApplyAttribute( AosArmorAttributes attrs, int min, int max, AosArmorAttribute attr, int low, int high, int scale )
		{
			attrs[attr] = Scale( min, max, low / scale, high / scale ) * scale;
		}

		private static void ApplyAttribute( AosWeaponAttributes attrs, int min, int max, AosWeaponAttribute attr, int low, int high )
		{
			attrs[attr] = Scale( min, max, low, high );
		}

		private static void ApplyAttribute( AosWeaponAttributes attrs, int min, int max, AosWeaponAttribute attr, int low, int high, int scale )
		{
			attrs[attr] = Scale( min, max, low / scale, high / scale ) * scale;
		}

		private static void ApplyAttribute( AosElementAttributes attrs, int min, int max, AosElementAttribute attr, int low, int high )
		{
			attrs[attr] = Scale( min, max, low, high );
		}

		private static void ApplyResistance( BaseArmor armor, int min, int max, ResistanceType res, int low, int high )
		{
			switch ( res )
			{
				case ResistanceType.Physical: armor.PhysicalBonus = Scale( min, max, low, high ); break;
				case ResistanceType.Fire: armor.FireBonus = Scale( min, max, low, high ); break;
				case ResistanceType.Cold: armor.ColdBonus = Scale( min, max, low, high ); break;
				case ResistanceType.Poison: armor.PoisonBonus = Scale( min, max, low, high ); break;
				case ResistanceType.Energy: armor.EnergyBonus = Scale( min, max, low, high ); break;
			}
		}

		private static void CheckItem( Mobile m, BaseArmor armor, bool addingAttribute )
		{
			if ( !((Item)armor).IsChildOf( m ) )
				throw new SpellCraftException( MsgNums.BackPack );
			
			if ( !SpellCraftConfig.ArtifactCraftable && armor.ArtifactRarity > 0 )
				throw new SpellCraftException( MsgNums.Artifact );

			if ( addingAttribute )
				CheckMaxAttributes( armor );
			
			#region SF Imbuing
			if ( (armor.TimesImbued >= 1 ))
				throw new SpellCraftException( MsgNums.Imbued );
			#endregion
		}

		private static void CheckItem( Mobile m, BaseWeapon weapon, bool addingAttribute )
		{
			if ( !((Item)weapon).IsChildOf( m ) )
				throw new SpellCraftException( MsgNums.BackPack );
			
			if ( !SpellCraftConfig.ArtifactCraftable && weapon.ArtifactRarity > 0 )
				throw new SpellCraftException( MsgNums.Artifact );

			if ( addingAttribute )
				CheckMaxAttributes( weapon );
			
			#region SF Imbuing
			if ( (weapon.TimesImbued >= 1 ))
				throw new SpellCraftException( MsgNums.Imbued );
			#endregion
			
			#region Lightsaber Crafting
			if ( (weapon.TimesEmpowered >= 1 ))
				throw new SpellCraftException( MsgNums.Empowered );
			#endregion
		}

		private static void CheckItem( Mobile m, BaseHat hat, bool addingAttribute )
		{
			if ( !((Item)hat).IsChildOf( m ) )
				throw new SpellCraftException( MsgNums.BackPack );
			
			if ( !SpellCraftConfig.ArtifactCraftable && hat.ArtifactRarity > 0 )
				throw new SpellCraftException( MsgNums.Artifact );

			if ( addingAttribute )
				CheckMaxAttributes( hat );
		}

		private static void CheckItem( Mobile m, BaseJewel jewel, bool addingAttribute )
		{
			if ( !((Item)jewel).IsChildOf( m ) )
				throw new SpellCraftException( MsgNums.BackPack );
			
			if ( !SpellCraftConfig.ArtifactCraftable && jewel.ArtifactRarity > 0 )
				throw new SpellCraftException( MsgNums.Artifact );

			if ( !( jewel is BaseBracelet || jewel is BaseRing ) && SpellCraftConfig.BraceletsRingsOnly )
				throw new SpellCraftException( MsgNums.BraceletsAndRingsOnly );

			if ( addingAttribute )
				CheckMaxAttributes( jewel );
		}

		private static void CheckItem( Mobile m, BaseInstrument instrument )
		{
			if ( !((Item)instrument).IsChildOf( m ) )
				throw new SpellCraftException( MsgNums.BackPack );
			
			if ( !SpellCraftConfig.ArtifactCraftable && ( instrument is GwennosHarp || instrument is IolosLute ) )
				throw new SpellCraftException( MsgNums.Artifact );
		}
		
		private static void CheckMaxAttributes( BaseArmor armor )
		{
			int props = 0;

			foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
				if ( armor.Attributes[ (AosAttribute)i ] > 0 ) ++props;

			foreach( int i in Enum.GetValues(typeof( AosArmorAttribute ) ) )
				if ( (AosArmorAttribute)i != AosArmorAttribute.LowerStatReq && armor.ArmorAttributes[ (AosArmorAttribute)i ] > 0 ) ++props;

			if ( props >= SpellCraftConfig.MaxPropsAllowed )
				throw new SpellCraftException( MsgNums.MaxProperties );
		}

		private static void CheckMaxAttributes( BaseWeapon weapon )
		{
			int props = 0;

			foreach( int i in Enum.GetValues(typeof( AosAttribute)) )
				if ( weapon.Attributes[ (AosAttribute)i ] > 0 ) ++props;

			foreach( int i in Enum.GetValues(typeof( AosWeaponAttribute)) )
				if ( (AosWeaponAttribute)i != AosWeaponAttribute.LowerStatReq && weapon.WeaponAttributes[ (AosWeaponAttribute)i ] > 0 ) ++props;

			if ( props >= SpellCraftConfig.MaxPropsAllowed )
				throw new SpellCraftException( MsgNums.MaxProperties );
		}

		private static void CheckMaxAttributes( BaseHat hat )
		{
			int props = 0;

			foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
				if ( hat.Attributes[ (AosAttribute)i ] > 0 ) ++props;

			foreach( int i in Enum.GetValues(typeof( AosArmorAttribute ) ) )
				if ( (AosArmorAttribute)i != AosArmorAttribute.LowerStatReq && hat.ClothingAttributes[ (AosArmorAttribute)i ] > 0 ) ++props;

			if ( props >= SpellCraftConfig.MaxPropsAllowed )
				throw new SpellCraftException( MsgNums.MaxProperties );
		}

		private static void CheckMaxAttributes( BaseJewel jewel )
		{
			int props = 0;
		
			foreach( int i in Enum.GetValues(typeof( AosAttribute)) )
				if ( jewel.Attributes[ (AosAttribute)i ] > 0 ) ++props;

			foreach( int i in Enum.GetValues(typeof( AosElementAttribute)) )
				if ( jewel.Resistances[ (AosElementAttribute)i ] > 0 ) ++props;

			if ( props >= SpellCraftConfig.MaxPropsAllowed )
				throw new SpellCraftException( MsgNums.MaxProperties );
		}

		private static void UseMagicJewels( Mobile from, BookOfSpellCrafts book, Item item, int amount )
		{
			// Consume the jewel possibly destroying the item and/or causing damage to the spell caster.

			if ( SpellCraftConfig.UseCharges && book.Charges >= amount )
			{
				book.Charges -= amount;
			}
			else if ( !from.Backpack.ConsumeTotal( typeof( MagicJewel ), amount ) )
				throw new SpellCraftException( MsgNums.NotEnoughJewels );
					
			if ( Utility.RandomDouble() < SpellCraftConfig.ExplodeChance )
			{
				from.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
				from.PlaySound( 0x307 );
				from.Damage( Utility.RandomMinMax( SpellCraftConfig.ExplodeMinDmg, SpellCraftConfig.ExplodeMaxDmg ) );

				if ( Utility.RandomDouble() < SpellCraftConfig.DestroyChance )
				{
					item.Delete();
					throw new SpellCraftException( MsgNums.ItemDestoyed );
				}
				throw new SpellCraftException( MsgNums.ItemIntact );
			}

			// Success effects
			#region Spellcrafting
			if ((Item)item is BaseWeapon)
			{	
				((BaseWeapon)item).TimesCrafted +=1;
			}
			if ((Item)item is BaseArmor)
			{	
				((BaseArmor)item).TimesCrafted +=1;
			}
			if ((Item)item is BaseJewel)
			{	
				((BaseJewel)item).TimesCrafted +=1;
			}
			if ((Item)item is BaseClothing)
			{	
				((BaseClothing)item).TimesCrafted +=1;
			}
			if ((Item)item is BaseInstrument)
			{	
				((BaseInstrument)item).TimesCrafted +=1;
			}
			#endregion
			from.PlaySound( 0x387 );
			from.FixedParticles( 0x36E4, 20, 10, 5044, EffectLayer.Head );
			from.PlaySound( 0x1E5 );
			from.SendMessage( MessageList[ (int)(MsgNums.Success) ] );
		}

		private static SlayerName GetRandomSlayer( int scalar )
		{
			SlayerGroup[] groups = SlayerGroup.Groups;

			if ( groups.Length == 0 )
				return SlayerName.None;

			SlayerGroup group = groups[Utility.Random( groups.Length -1 )]; //-1 To Exclude the Fey Slayer which appears ONLY on a certain artifact.
			SlayerEntry entry;

			if ( scalar > Utility.Random( 1, 100 ) ) // Chance to do super slayer
			{
				entry = group.Super;
			}
			else
			{
				SlayerEntry[] entries = group.Entries;

				if ( entries.Length == 0 )
					return SlayerName.None;

				entry = entries[Utility.Random( entries.Length )];
			}

			return entry.Name;
		}
		
		#endregion Private Methods
	}

	#region Exceptions

	public class SpellCraftException : Exception
	{
		private int m_MessageId;

		public SpellCraftException( SpellCraft.MsgNums msgId ) : base( "" )
		{
			m_MessageId = (int)msgId;
		}

		public override string ToString()
		{
			return ( ( m_MessageId < 0 || m_MessageId >= SpellCraft.MessageList.Length )
				? "" : SpellCraft.MessageList[ m_MessageId ] );
		}
	}

	#endregion

}