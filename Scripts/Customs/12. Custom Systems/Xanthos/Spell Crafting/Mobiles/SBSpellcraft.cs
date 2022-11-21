#region AuthorHeader
//
//	SpellCrafting version 3.0, by Xanthos and TheOutkastDev
//
//  Based on original ideas and code by TheOutkastDev
//
#endregion AuthorHeader
using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.SpellCrafting.Items;

namespace Server.Mobiles
{
	public class SBSpellCraft : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSpellCraft()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "Strength Bonus Craft", typeof( BonusStrengthJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Dexterity Bonus Craft", typeof( BonusDexterityJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Intelligence Bonus Craft", typeof( BonusIntelligenceJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Point Bonus Craft", typeof( BonusHitsJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Stamina Bonus Craft", typeof( BonusStaminaJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Mana Bonus Craft", typeof( BonusManaJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Physical Resist Craft", typeof( PhysicalResistJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Fire Resist Craft", typeof( FireResistJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Cold Resist Craft", typeof( ColdResistJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Poison Resist Craft", typeof( PoisonResistJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Energy Resist Craft", typeof( EnergyResistJewel ), 250000, 5, 0x1EA7, 1161 ) );
				//Add( new GenericBuyInfo( "HP Regenation Craft", typeof( RegenerateHitsJewel ), 250000, 5, 0x1EA7, 1161 ) );
				//Add( new GenericBuyInfo( "Mana Regenation Craft", typeof( RegenerateManaJewel ), 250000, 5, 0x1EA7, 1161 ) );
				//Add( new GenericBuyInfo( "Stamina Regeneration Craft", typeof( RegenerateStaminaJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Faster Cast Recovery Craft", typeof( CastRecoveryJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Faster Cast Speed Craft", typeof( CastSpeedJewel ), 250000, 5, 0x1EA7, 1161 ) );
				//Add( new GenericBuyInfo( "Lower Mana Cost Craft", typeof( LowerManaCostJewel ), 250000, 5, 0x1EA7, 1161 ) );
				//Add( new GenericBuyInfo( "Lower Reagent Cost Craft", typeof( LowerReagentCostJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Mage Armor Craft", typeof( MageArmorJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Mage Weapon Craft", typeof( MageWeaponJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Spell Channeling Craft", typeof( SpellChannelingJewel ), 250000, 5, 0x1EA7, 1161 ) );
				//Add( new GenericBuyInfo( "Spell Damage Increase Craft", typeof( SpellDamageJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Cold Area Craft", typeof( HitColdAreaJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Energy Area Craft", typeof( HitEnergyAreaJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Fire Area Craft", typeof( HitFireAreaJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Physical Area Craft", typeof( HitPhysicalAreaJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Poison Area Craft", typeof( HitPoisonAreaJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Dispel Craft", typeof( HitDispelJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Fireball Craft", typeof( HitFireballJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Harm Craft", typeof( HitHarmJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Lightning Craft", typeof( HitLightningJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Magic Arrow Craft", typeof( HitMagicArrowJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Lower Attack Craft", typeof( HitLowerAttackJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Lower Defence Craft", typeof( HitLowerDefendJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Leech Hits Craft", typeof( HitLeechHitsJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Leech Mana Craft", typeof( HitLeechManaJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Hit Leech Stamina Craft", typeof( HitLeechStamJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Use Best Weapon Skill Craft", typeof( UseBestSkillJewel ), 250000, 5, 0x1EA7, 1161 ) );
				//Add( new GenericBuyInfo( "Weapon Damage Increase Craft", typeof( WeaponDamageJewel ), 250000, 5, 0x1EA7, 1161 ) );
				//Add( new GenericBuyInfo( "Swing Speed Increase Craft", typeof( WeaponSpeedJewel ), 250000, 5, 0x1EA7, 1161 ) );
				//Add( new GenericBuyInfo( "Hit Chance Increase Craft", typeof( AttackChanceJewel ), 250000, 5, 0x1EA7, 1161 ) );
				//Add( new GenericBuyInfo( "Defense Chance Increase Craft", typeof( DefendChanceJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Enhance Potions Craft", typeof( EnhancePotionsJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Lower Stat Requirements Craft", typeof( LowerStatRequirementsJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Luck Craft", typeof( LuckJewel ), 250000, 5, 0x1EA7, 1161 ) );
				//Add( new GenericBuyInfo( "Reflect Physical Craft", typeof( ReflectPhysicalJewel ), 250000, 5, 0x1EA7, 1161 ) );
				//Add( new GenericBuyInfo( "Self Repair Craft", typeof( SelfRepairJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Night Sight Craft", typeof( NightSightJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Slayer Craft", typeof( SlayerJewel ), 250000, 5, 0x1EA7, 1161 ) );
				Add( new GenericBuyInfo( "Durability Craft", typeof( DurabilityJewel ), 250000, 5, 0x1EA7, 1161 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BonusStrengthJewel ), 500 );
				Add( typeof( BonusDexterityJewel ), 500 );
				Add( typeof( BonusIntelligenceJewel ), 500 );
				Add( typeof( BonusHitsJewel ), 500 );
				Add( typeof( BonusStaminaJewel ), 500 );
				Add( typeof( BonusManaJewel ), 500 );
				Add( typeof( PhysicalResistJewel ), 500 );
				Add( typeof( FireResistJewel ), 500 );
				Add( typeof( ColdResistJewel ), 500 );
				Add( typeof( PoisonResistJewel ), 500 );
				Add( typeof( EnergyResistJewel ), 500 );
				Add( typeof( RegenerateHitsJewel ), 500 );
				Add( typeof( RegenerateManaJewel ), 500 );
				Add( typeof( RegenerateStaminaJewel ), 500 );
				Add( typeof( CastRecoveryJewel ), 500 );
				Add( typeof( CastSpeedJewel ), 500 );
				Add( typeof( LowerManaCostJewel ), 500 );
				Add( typeof( LowerReagentCostJewel ), 500 );
				Add( typeof( MageArmorJewel ), 500 );
				Add( typeof( MageWeaponJewel ), 500 );
				Add( typeof( SpellChannelingJewel ), 500 );
				Add( typeof( SpellDamageJewel ), 500 );
				Add( typeof( HitColdAreaJewel ), 500 );
				Add( typeof( HitEnergyAreaJewel ), 500 );
				Add( typeof( HitFireAreaJewel ), 500 );
				Add( typeof( HitPhysicalAreaJewel ), 500 );
				Add( typeof( HitPoisonAreaJewel ), 500 );
				Add( typeof( HitDispelJewel ), 500 );
				Add( typeof( HitFireballJewel ), 500 );
				Add( typeof( HitHarmJewel ), 500 );
				Add( typeof( HitLightningJewel ), 500 );
				Add( typeof( HitMagicArrowJewel ), 500 );
				Add( typeof( HitLowerAttackJewel ), 500 );
				Add( typeof( HitLowerDefendJewel ), 500 );
				Add( typeof( HitLeechHitsJewel ), 500 );
				Add( typeof( HitLeechManaJewel ), 500 );
				Add( typeof( HitLeechStamJewel ), 500 );
				Add( typeof( UseBestSkillJewel ), 500 );
				Add( typeof( WeaponDamageJewel ), 500 );
				Add( typeof( WeaponSpeedJewel ), 500 );
				Add( typeof( AttackChanceJewel ), 500 );
				Add( typeof( DefendChanceJewel ), 500 );
				Add( typeof( EnhancePotionsJewel ), 500 );
				Add( typeof( LowerStatRequirementsJewel ), 500 );
				Add( typeof( LuckJewel ), 500 );
				Add( typeof( ReflectPhysicalJewel ), 500 );
				Add( typeof( SelfRepairJewel ), 500 );
				Add( typeof( NightSightJewel ), 500 );
				Add( typeof( SlayerJewel ), 500 );
				Add( typeof( DurabilityJewel ), 500 );
			}
		}
	}
}