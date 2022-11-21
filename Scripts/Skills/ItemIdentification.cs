using System;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
	public class ItemIdentification
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.ItemID].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile from )
		{
			from.SendLocalizedMessage( 500343 ); // What do you wish to appraise and identify?
			from.Target = new InternalTarget();

			return TimeSpan.FromSeconds( 1.0 );
		}

		[PlayerVendorTarget]
		private class InternalTarget : Target
		{
			public InternalTarget() :  base ( 8, false, TargetFlags.None )
			{
				AllowNonlocal = true;
			}

			protected override void OnTarget( Mobile from, object o )
			{
                int oInt = 0;
                double oDo = 0;
                int oMods = 0;
                if (o is BaseWeapon || o is BaseArmor || o is BaseJewel || o is BaseHat)
				{
                    if (from.Skills[SkillName.ItemID].Base >= 100.0)
					{
						if ( o is BaseWeapon )
                        {
                            BaseWeapon w = o as BaseWeapon;
                            if (o is BaseRanged)
                            {
                                BaseRanged r = o as BaseRanged;
                                if (r.Velocity > 0) { oDo += (130 / 50) * r.Velocity; oMods += 1; }
                                if (r.Balanced == true) { oDo += 150; oMods += 1; }
                                if (w.Attributes.DefendChance > 0) { oDo += (130 / 25) * w.Attributes.DefendChance; oMods += 1; }
                                if (w.Attributes.AttackChance > 0) { oDo += (130 / 25) * w.Attributes.AttackChance; oMods += 1; }
                                if (w.Attributes.Luck > 0) { oDo += (100 / 120) * w.Attributes.Luck; oMods += 1; }
                                if (w.WeaponAttributes.ResistPhysicalBonus > 0) { oDo += (100 / 18) * w.WeaponAttributes.ResistPhysicalBonus; oMods += 1; }
                                if (w.WeaponAttributes.ResistFireBonus > 0) { oDo += (100 / 18) * w.WeaponAttributes.ResistFireBonus; oMods += 1; }
                                if (w.WeaponAttributes.ResistColdBonus > 0) { oDo += (100 / 18) * w.WeaponAttributes.ResistColdBonus; oMods += 1; }
                                if (w.WeaponAttributes.ResistPoisonBonus > 0) { oDo += (100 / 18) * w.WeaponAttributes.ResistPoisonBonus; oMods += 1; }
                                if (w.WeaponAttributes.ResistEnergyBonus > 0) { oDo += (100 / 18) * w.WeaponAttributes.ResistEnergyBonus; oMods += 1; }
                            }
                            else if(o is BaseMeleeWeapon)
                            {
                                if (w.Attributes.DefendChance > 0) { oDo += (130 / 15) * w.Attributes.DefendChance; oMods += 1; }
                                if (w.Attributes.AttackChance > 0) { oDo += (130 / 15) * w.Attributes.AttackChance; oMods += 1; }
                                if (w.Attributes.Luck > 0) { oDo += w.Attributes.Luck; oMods += 1; }
                                if (w.WeaponAttributes.ResistPhysicalBonus > 0) { oDo += (100 / 15) * w.WeaponAttributes.ResistPhysicalBonus; oMods += 1; }
                                if (w.WeaponAttributes.ResistFireBonus > 0) { oDo += (100 / 15) * w.WeaponAttributes.ResistFireBonus; oMods += 1; }
                                if (w.WeaponAttributes.ResistColdBonus > 0) { oDo += (100 / 15) * w.WeaponAttributes.ResistColdBonus; oMods += 1; }
                                if (w.WeaponAttributes.ResistPoisonBonus > 0) { oDo += (100 / 15) * w.WeaponAttributes.ResistPoisonBonus; oMods += 1; }
                                if (w.WeaponAttributes.ResistEnergyBonus > 0) { oDo += (100 / 15) * w.WeaponAttributes.ResistEnergyBonus; oMods += 1; }
                            }

                            if (w.Attributes.RegenHits > 0) { oDo += (50 * w.Attributes.RegenHits); oMods += 1; }
                            if (w.Attributes.RegenStam > 0) { oDo += (33.33 * w.Attributes.RegenStam); oMods += 1; }
                            if (w.Attributes.RegenMana > 0) { oDo += (50 * w.Attributes.RegenMana); oMods += 1; }
                            if (w.Attributes.BonusStr > 0) { oDo += (110 / 8) * w.Attributes.BonusStr; oMods += 1; }
                            if (w.Attributes.BonusDex > 0) { oDo += (110 / 8) * w.Attributes.BonusDex; oMods += 1; }
                            if (w.Attributes.BonusInt > 0) { oDo += (110 / 8) * w.Attributes.BonusInt; oMods += 1; }
                            if (w.Attributes.BonusHits > 0) { oDo += 22 * w.Attributes.BonusHits; oMods += 1; }
                            if (w.Attributes.BonusStam > 0) { oDo += (100 / 8) * w.Attributes.BonusStam; oMods += 1; }
                            if (w.Attributes.BonusMana > 0) { oDo += (110 / 8) * w.Attributes.BonusMana; oMods += 1; }
                            if (w.Attributes.WeaponDamage > 0) { oDo += (2 * w.Attributes.WeaponDamage); oMods += 1; }
                            if (w.Attributes.WeaponSpeed > 0) { oDo += (110 / 30) * w.Attributes.WeaponSpeed; oMods += 1; }
                            if (w.Attributes.SpellDamage > 0) { oDo += (100 / 12) * w.Attributes.SpellDamage; oMods += 1; }
                            if (w.Attributes.CastRecovery > 0) { oDo += (40 * w.Attributes.CastRecovery); oMods += 1; }
                            if (w.Attributes.LowerManaCost > 0) { oDo += (110 / 8) * w.Attributes.LowerManaCost; oMods += 1; }
                            if (w.Attributes.LowerRegCost > 0) { oDo += (5 * w.Attributes.LowerRegCost); oMods += 1; }
                            if (w.Attributes.ReflectPhysical > 0) { oDo += (100 / 15) * w.Attributes.ReflectPhysical; oMods += 1; }
                            if (w.Attributes.EnhancePotions > 0) { oDo += (4 * w.Attributes.EnhancePotions); oMods += 1; }
                            if (w.Attributes.SpellChanneling > 0)
                            {
                                oDo += 100; oMods += 1;
                                if (w.Attributes.CastSpeed == 0) { oDo += 140; oMods += 1; }
                                if (w.Attributes.CastSpeed == 1) { oDo += 280; oMods += 1; }
                            }
                            else if (w.Attributes.CastSpeed > 0) { oDo += (140 * w.Attributes.CastSpeed); oMods += 1; }
                            if (w.Attributes.NightSight > 0) { oDo += 50; oMods += 1; }

                            if (w.WeaponAttributes.LowerStatReq > 0) { oDo += w.WeaponAttributes.LowerStatReq; oMods += 1; }
                            if (w.WeaponAttributes.HitLeechHits > 0) { oDo += (110 / 50) * w.WeaponAttributes.HitLeechHits; oMods += 1; }
                            if (w.WeaponAttributes.HitLeechStam > 0) { oDo += 2 * w.WeaponAttributes.HitLeechStam; oMods += 1; }
                            if (w.WeaponAttributes.HitLeechMana > 0) { oDo += (110 / 50) * w.WeaponAttributes.HitLeechMana; oMods += 1; }
                            if (w.WeaponAttributes.HitLowerAttack > 0) { oDo += (110 / 50) * w.WeaponAttributes.HitLowerAttack; oMods += 1; }
                            if (w.WeaponAttributes.HitLowerDefend > 0) { oDo += (130 / 50) * w.WeaponAttributes.HitLowerDefend; oMods += 1; }
                            if (w.WeaponAttributes.HitColdArea > 0) { oDo += (2 * w.WeaponAttributes.HitColdArea); oMods += 1; }
                            if (w.WeaponAttributes.HitFireArea > 0) { oDo += (2 * w.WeaponAttributes.HitFireArea); oMods += 1; }
                            if (w.WeaponAttributes.HitPoisonArea > 0) { oDo += (2 * w.WeaponAttributes.HitPoisonArea); oMods += 1; }
                            if (w.WeaponAttributes.HitEnergyArea > 0) { oDo += (2 * w.WeaponAttributes.HitEnergyArea); oMods += 1; }
                            if (w.WeaponAttributes.HitPhysicalArea > 0) { oDo += (2 * w.WeaponAttributes.HitPhysicalArea); oMods += 1; }
                            if (w.WeaponAttributes.HitMagicArrow > 0) { oDo += 2.4 * w.WeaponAttributes.HitMagicArrow; oMods += 1; }
                            if (w.WeaponAttributes.HitHarm > 0) { oDo += (110 / 50) * w.WeaponAttributes.HitHarm; oMods += 1; }
                            if (w.WeaponAttributes.HitFireball > 0) { oDo += 2.4 * w.WeaponAttributes.HitFireball; oMods += 1; }
                            if (w.WeaponAttributes.HitLightning > 0) { oDo += 2.4 * w.WeaponAttributes.HitLightning; oMods += 1; }
                            if (w.WeaponAttributes.HitDispel > 0) { oDo += (2 * w.WeaponAttributes.HitDispel); oMods += 1; }
                            if (w.WeaponAttributes.UseBestSkill > 0) { oDo += 150; oMods += 1; }
                            if (w.WeaponAttributes.MageWeapon > 0) { oDo += (20 * w.WeaponAttributes.MageWeapon); oMods += 1; }
                            if (w.WeaponAttributes.DurabilityBonus > 0) { oDo += w.WeaponAttributes.DurabilityBonus; oMods += 1; }
                            if (w.Slayer == SlayerName.Silver || w.Slayer == SlayerName.Repond || w.Slayer == SlayerName.ReptilianDeath || w.Slayer == SlayerName.Exorcism || w.Slayer == SlayerName.ArachnidDoom || w.Slayer == SlayerName.ElementalBan || w.Slayer == SlayerName.Fey) { oDo += 130; oMods += 1; }
                            else if (w.Slayer != SlayerName.None) { oDo += 110; oMods += 1; }
                            if (w.Slayer2 == SlayerName.Silver || w.Slayer2 == SlayerName.Repond || w.Slayer2 == SlayerName.ReptilianDeath || w.Slayer2 == SlayerName.Exorcism || w.Slayer2 == SlayerName.ArachnidDoom || w.Slayer2 == SlayerName.ElementalBan || w.Slayer2 == SlayerName.Fey) { oDo += 130; oMods += 1; }
                            else if (w.Slayer2 != SlayerName.None) { oDo += 110; oMods += 1; }

                            if (w.SkillBonuses.GetBonus(0) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(0); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(1) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(1); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(2) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(2); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(3) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(3); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(4) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(4); oMods += 1; }
                            oDo = Math.Round(oDo, 1);
                            oInt += Convert.ToInt32(oDo);
                        }
                        if (o is BaseArmor)
                        {
                            BaseArmor w = o as BaseArmor;

                            if (w.Attributes.DefendChance > 0) { oDo += (130 / 15) * w.Attributes.DefendChance; oMods += 1; }
                            if (w.Attributes.AttackChance > 0) { oDo += (130 / 15) * w.Attributes.AttackChance; oMods += 1; }
                            if (w.Attributes.Luck > 0) { oDo += w.Attributes.Luck; oMods += 1; }
                            if (w.Attributes.RegenHits > 0) { oDo += (50 * w.Attributes.RegenHits); oMods += 1; }
                            if (w.Attributes.RegenStam > 0) { oDo += (33.33 * w.Attributes.RegenStam); oMods += 1; }
                            if (w.Attributes.RegenMana > 0) { oDo += (50 * w.Attributes.RegenMana); oMods += 1; }
                            if (w.Attributes.BonusStr > 0) { oDo += (110 / 8) * w.Attributes.BonusStr; oMods += 1; }
                            if (w.Attributes.BonusDex > 0) { oDo += (110 / 8) * w.Attributes.BonusDex; oMods += 1; }
                            if (w.Attributes.BonusInt > 0) { oDo += (110 / 8) * w.Attributes.BonusInt; oMods += 1; }
                            if (w.Attributes.BonusHits > 0) { oDo += 22 * w.Attributes.BonusHits; oMods += 1; }
                            if (w.Attributes.BonusStam > 0) { oDo += (100 / 8) * w.Attributes.BonusStam; oMods += 1; }
                            if (w.Attributes.BonusMana > 0) { oDo += (110 / 8) * w.Attributes.BonusMana; oMods += 1; }
                            if (w.Attributes.WeaponDamage > 0) { oDo += (2 * w.Attributes.WeaponDamage); oMods += 1; }
                            if (w.Attributes.WeaponSpeed > 0) { oDo += (110 / 30) * w.Attributes.WeaponSpeed; oMods += 1; }
                            if (w.Attributes.SpellDamage > 0) { oDo += (100 / 12) * w.Attributes.SpellDamage; oMods += 1; }
                            if (w.Attributes.CastRecovery > 0) { oDo += (40 * w.Attributes.CastRecovery); oMods += 1; }
                            if (w.Attributes.LowerManaCost > 0) { oDo += (110 / 8) * w.Attributes.LowerManaCost; oMods += 1; }
                            if (w.Attributes.LowerRegCost > 0) { oDo += (5 * w.Attributes.LowerRegCost); oMods += 1; }
                            if (w.Attributes.ReflectPhysical > 0) { oDo += (100 / 15) * w.Attributes.ReflectPhysical; oMods += 1; }
                            if (w.Attributes.EnhancePotions > 0) { oDo += (4 * w.Attributes.EnhancePotions); oMods += 1; }
                            if (w.Attributes.SpellChanneling > 0)
                            {
                                oDo += 100; oMods += 1;
                                if (w.Attributes.CastSpeed == 0) { oDo += 140; oMods += 1; }
                                if (w.Attributes.CastSpeed == 1) { oDo += 280; oMods += 1; }
                            }
                            else if (w.Attributes.CastSpeed > 0) { oDo += (140 * w.Attributes.CastSpeed); oMods += 1; }
                            if (w.Attributes.NightSight > 0) { oDo += 50; oMods += 1; }

                            if (w.ArmorAttributes.LowerStatReq > 0) { oDo += w.ArmorAttributes.LowerStatReq; oMods += 1; }
                            if (w.ArmorAttributes.MageArmor > 0) { oDo += 140; oMods += 1; }
                            if (w.ArmorAttributes.DurabilityBonus > 0) { oDo += w.ArmorAttributes.DurabilityBonus; oMods += 1; }

                            if (w.Quality != ArmorQuality.Exceptional)
                            {
                                if (w.PhysicalBonus > 0) { oDo += (100 / 15) * w.PhysicalBonus; oMods += 1; }
                                if (w.FireBonus > 0) { oDo += (100 / 15) * w.FireBonus; oMods += 1; }
                                if (w.ColdBonus > 0) { oDo += (100 / 15) * w.ColdBonus; oMods += 1; }
                                if (w.PoisonBonus > 0) { oDo += (100 / 15) * w.PoisonBonus; oMods += 1; }
                                if (w.EnergyBonus > 0) { oDo += (100 / 15) * w.EnergyBonus; oMods += 1; }
                            }

                             if (w.SkillBonuses.GetBonus(0) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(0); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(1) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(1); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(2) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(2); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(3) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(3); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(4) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(4); oMods += 1; }
                            oDo = Math.Round(oDo, 1);
                            oInt += Convert.ToInt32(oDo);
                        }

                        if (o is BaseJewel)
                        {
                            BaseJewel w = o as BaseJewel;

                            if (w.Attributes.DefendChance > 0) { oDo += (130 / 15) * w.Attributes.DefendChance; oMods += 1; }
                            if (w.Attributes.AttackChance > 0) { oDo += (130 / 15) * w.Attributes.AttackChance; oMods += 1; }
                            if (w.Attributes.Luck > 0) { oDo += w.Attributes.Luck; oMods += 1; }
                            if (w.Attributes.RegenHits > 0) { oDo += (50 * w.Attributes.RegenHits); oMods += 1; }
                            if (w.Attributes.RegenStam > 0) { oDo += (33.33 * w.Attributes.RegenStam); oMods += 1; }
                            if (w.Attributes.RegenMana > 0) { oDo += (50 * w.Attributes.RegenMana); oMods += 1; }
                            if (w.Attributes.BonusStr > 0) { oDo += (110 / 8) * w.Attributes.BonusStr; oMods += 1; }
                            if (w.Attributes.BonusDex > 0) { oDo += (110 / 8) * w.Attributes.BonusDex; oMods += 1; }
                            if (w.Attributes.BonusInt > 0) { oDo += (110 / 8) * w.Attributes.BonusInt; oMods += 1; }
                            if (w.Attributes.BonusHits > 0) { oDo += 22 * w.Attributes.BonusHits; oMods += 1; }
                            if (w.Attributes.BonusStam > 0) { oDo += (100 / 8) * w.Attributes.BonusStam; oMods += 1; }
                            if (w.Attributes.BonusMana > 0) { oDo += (110 / 8) * w.Attributes.BonusMana; oMods += 1; }
                            if (w.Attributes.WeaponDamage > 0) { oDo += (2 * w.Attributes.WeaponDamage); oMods += 1; }
                            if (w.Attributes.WeaponSpeed > 0) { oDo += (110 / 30) * w.Attributes.WeaponSpeed; oMods += 1; }
                            if (w.Attributes.SpellDamage > 0) { oDo += (100 / 12) * w.Attributes.SpellDamage; oMods += 1; }
                            if (w.Attributes.CastRecovery > 0) { oDo += (40 * w.Attributes.CastRecovery); oMods += 1; }
                            if (w.Attributes.LowerManaCost > 0) { oDo += (110 / 8) * w.Attributes.LowerManaCost; oMods += 1; }
                            if (w.Attributes.LowerRegCost > 0) { oDo += (5 * w.Attributes.LowerRegCost); oMods += 1; }
                            if (w.Attributes.ReflectPhysical > 0) { oDo += (100 / 15) * w.Attributes.ReflectPhysical; oMods += 1; }
                            if (w.Attributes.EnhancePotions > 0) { oDo += (4 * w.Attributes.EnhancePotions); oMods += 1; }
                            if (w.Attributes.SpellChanneling > 0)
                            {
                                oDo += 100; oMods += 1;
                                if (w.Attributes.CastSpeed == 0) { oDo += 140; oMods += 1; }
                                if (w.Attributes.CastSpeed == 1) { oDo += 280; oMods += 1; }
                            }
                            else if (w.Attributes.CastSpeed > 0) { oDo += (140 * w.Attributes.CastSpeed); oMods += 1; }
                            if (w.Attributes.NightSight > 0) { oDo += 50; oMods += 1; }

                            if (w.Resistances.Physical > 0) { oDo += (100 / 15) * w.Resistances.Physical; oMods += 1; }
                            if (w.Resistances.Fire > 0) { oDo += (100 / 15) * w.Resistances.Fire; oMods += 1; }
                            if (w.Resistances.Cold > 0) { oDo += (100 / 15) * w.Resistances.Cold; oMods += 1; }
                            if (w.Resistances.Poison > 0) { oDo += (100 / 15) * w.Resistances.Poison; oMods += 1; }
                            if (w.Resistances.Energy > 0) { oDo += (100 / 15) * w.Resistances.Energy; oMods += 1; }

                            if (w.SkillBonuses.GetBonus(0) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(0); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(1) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(1); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(2) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(2); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(3) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(3); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(4) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(4); oMods += 1; }
                            oDo = Math.Round(oDo, 1);
                            oInt += Convert.ToInt32(oDo);
                        }
                        if (o is BaseClothing)
                        {
                            BaseClothing w = o as BaseClothing;

                            if (w.Attributes.DefendChance > 0) { oDo += (130 / 15) * w.Attributes.DefendChance; oMods += 1; }
                            if (w.Attributes.AttackChance > 0) { oDo += (130 / 15) * w.Attributes.AttackChance; oMods += 1; }
                            if (w.Attributes.Luck > 0) { oDo += w.Attributes.Luck; oMods += 1; }
                            if (w.Attributes.RegenHits > 0) { oDo += (50 * w.Attributes.RegenHits); oMods += 1; }
                            if (w.Attributes.RegenStam > 0) { oDo += (33.33 * w.Attributes.RegenStam); oMods += 1; }
                            if (w.Attributes.RegenMana > 0) { oDo += (50 * w.Attributes.RegenMana); oMods += 1; }
                            if (w.Attributes.BonusStr > 0) { oDo += (110 / 8) * w.Attributes.BonusStr; oMods += 1; }
                            if (w.Attributes.BonusDex > 0) { oDo += (110 / 8) * w.Attributes.BonusDex; oMods += 1; }
                            if (w.Attributes.BonusInt > 0) { oDo += (110 / 8) * w.Attributes.BonusInt; oMods += 1; }
                            if (w.Attributes.BonusHits > 0) { oDo += 22 * w.Attributes.BonusHits; oMods += 1; }
                            if (w.Attributes.BonusStam > 0) { oDo += (100 / 8) * w.Attributes.BonusStam; oMods += 1; }
                            if (w.Attributes.BonusMana > 0) { oDo += (110 / 8) * w.Attributes.BonusMana; oMods += 1; }
                            if (w.Attributes.WeaponDamage > 0) { oDo += (2 * w.Attributes.WeaponDamage); oMods += 1; }
                            if (w.Attributes.WeaponSpeed > 0) { oDo += (110 / 30) * w.Attributes.WeaponSpeed; oMods += 1; }
                            if (w.Attributes.SpellDamage > 0) { oDo += (100 / 12) * w.Attributes.SpellDamage; oMods += 1; }
                            if (w.Attributes.CastRecovery > 0) { oDo += (40 * w.Attributes.CastRecovery); oMods += 1; }
                            if (w.Attributes.LowerManaCost > 0) { oDo += (110 / 8) * w.Attributes.LowerManaCost; oMods += 1; }
                            if (w.Attributes.LowerRegCost > 0) { oDo += (5 * w.Attributes.LowerRegCost); oMods += 1; }
                            if (w.Attributes.ReflectPhysical > 0) { oDo += (100 / 15) * w.Attributes.ReflectPhysical; oMods += 1; }
                            if (w.Attributes.EnhancePotions > 0) { oDo += (4 * w.Attributes.EnhancePotions); oMods += 1; }
                            if (w.Attributes.SpellChanneling > 0)
                            {
                                oDo += 100; oMods += 1;
                                if (w.Attributes.CastSpeed == 0) { oDo += 140; oMods += 1; }
                                if (w.Attributes.CastSpeed == 1) { oDo += 280; oMods += 1; }
                            }
                            else if (w.Attributes.CastSpeed > 0) { oDo += (140 * w.Attributes.CastSpeed); oMods += 1; }
                            if (w.Attributes.NightSight > 0) { oDo += 50; oMods += 1; }


                                if (w.Resistances.Physical > 0) { oDo += (100 / 15) * w.Resistances.Physical; oMods += 1; }
                                if (w.Resistances.Fire > 0) { oDo += (100 / 15) * w.Resistances.Fire; oMods += 1; }
                                if (w.Resistances.Cold > 0) { oDo += (100 / 15) * w.Resistances.Cold; oMods += 1; }
                                if (w.Resistances.Poison > 0) { oDo += (100 / 15) * w.Resistances.Poison; oMods += 1; }
                                if (w.Resistances.Energy > 0) { oDo += (100 / 15) * w.Resistances.Energy; oMods += 1; }

                            if (w.SkillBonuses.GetBonus(0) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(0); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(1) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(1); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(2) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(2); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(3) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(3); oMods += 1; }
                            if (w.SkillBonuses.GetBonus(4) > 0) { oDo += (140 / 15) * w.SkillBonuses.GetBonus(4); oMods += 1; }
                            oDo = Math.Round(oDo, 1);
                            oInt += Convert.ToInt32(oDo);
                        }

                        if (oInt > 0 && oInt <= 200)
                        {
                            from.LocalOverheadMessage(MessageType.Regular, 2499, true, "You conclude that item will magically unravel into: Magical Residue");
                            if (from.Skills[SkillName.Imbuing].Base >= 100.0)
                                from.LocalOverheadMessage(MessageType.Regular, 2499, true, String.Format("Item Intensity: {0}", oInt));
                        }

                        else if (oInt > 200 && oInt < 480)
                        {
                            if (from.Skills[SkillName.Imbuing].Base >= 45.0)
                            {
                                from.LocalOverheadMessage(MessageType.Regular, 2499, true, "You conclude that item will magically unravel into: Enchanted Essence");
                                if (from.Skills[SkillName.Imbuing].Base >= 100.0)
                                    from.LocalOverheadMessage(MessageType.Regular, 2499, true, String.Format("Item Intensity: {0}", oInt));
                            }
                            else
                            {
                                from.LocalOverheadMessage(MessageType.Regular, 2499, true, "Your Imbuing skill is not high enough to identify the imbuing ingredient.");
                            }
                        }
                        else if (oInt >= 480)
                        {
                            if (from.Skills[SkillName.Imbuing].Base >= 95.0)
                            {
                                from.LocalOverheadMessage(MessageType.Regular, 2499, true, "You conclude that item will magically unravel into: Relic Fragment");
                                if (from.Skills[SkillName.Imbuing].Base >= 100.0)
                                    from.LocalOverheadMessage(MessageType.Regular, 2499, true, String.Format("Item Intensity: {0}", oInt));
                            }
                            else
                            {
                                from.LocalOverheadMessage(MessageType.Regular, 2499, true, "Your Imbuing skill is not high enough to identify the imbuing ingredient.");
                            }
                        }
                        else
                        {
                            from.LocalOverheadMessage(MessageType.Regular, 2499, true, "You conclude that item cannot be magically unraveled. It appears to possess little to no magic.");
                        }
                    }
                    else
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 2499, true, "You are uncertain.. your Item Identification skill is not high enougth");
                    }
                }
                else if (o is Mobile)
                {
                    ((Mobile)o).OnSingleClick(from);
                }
                else
                {
                    from.LocalOverheadMessage(MessageType.Regular, 2499, true, "You conclude that item cannot be magically unraveled.");
                }
				Server.Engines.XmlSpawner2.XmlAttach.RevealAttachments(from, o);
            }
        }
    }
}