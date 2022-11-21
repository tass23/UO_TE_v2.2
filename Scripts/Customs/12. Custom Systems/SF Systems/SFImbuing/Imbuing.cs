using System;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.SkillHandlers
{
    public class Imbuing
    {

        public static void Initialize()
        {
            SkillInfo.Table[(int)SkillName.Imbuing].Callback = new SkillUseCallback(OnUse);
        }

        public static TimeSpan OnUse(Mobile from)
        {
            if (!from.Alive)
            {
                from.SendMessage(2499, "It occurs to you that you cannot do much imbuing when you are dead...");
            }
            else
            {
                from.SendGump(new ImbuingGump(from));
            }

            return TimeSpan.FromSeconds(1.0);
        }
    }

    public class ImbuingGump : Gump
    {
        public static void CheckSoulForge(Mobile from, int range, out bool sforge)
        {
            sforge = false;
            PlayerMobile m = from as PlayerMobile;

            Map map = from.Map;

            if (map == null)
                return;

            IPooledEnumerable eable = map.GetItemsInRange(from.Location, 1);

            foreach (Item item in eable)
            {
                bool isQueensSForge = (item.ItemID >= 17015 && item.ItemID <= 17030); // TerMur SoulForge (+5% bonus & easier unravels)
                bool isSForge = (item.ItemID >= 16995 && item.ItemID <= 17010); // Standard SoulForge
				bool isGSForge = (item.ItemID >= 17607 && item.ItemID <= 17610); // Gargoyle Mini SoulForge

                if (isSForge || isQueensSForge || isGSForge)
                {
                    if ((from.Z + 16) < item.Z || (item.Z + 16) < from.Z || !from.InLOS(item))
                        continue;

                    sforge = true;

                    m.Imb_SFBonus = 0;

                    if (isQueensSForge)
                    {
                        m.Imb_SFBonus = 5;
                    }

                    if (sforge)
                        break;
                }
            }
        }

        public ImbuingGump(Mobile from)
            : base(520, 340)
        {
            Mobile m = from;
            AddPage(0);
            this.AddBackground(0, 0, 540, 340, 9270);
            this.AddAlphaRegion(17, 17, 486, 20);
            this.AddAlphaRegion(17, 45, 486, 247);
            this.AddAlphaRegion(17, 299, 486, 25);
            this.AddLabel(221, 18, 1359, "IMBUING MENU");

            AddButton(25, 66, 4017, 4018, 10005, GumpButtonType.Reply, 0);
            AddHtml(66, 68, 430, 18, "<BASEFONT COLOR=#FFFFFF>Imbue Item - Adds or modifies an item property on an item", false, false);
            AddButton(25, 95, 4017, 4018, 10006, GumpButtonType.Reply, 0);
            AddHtml(66, 97, 430, 18, "<BASEFONT COLOR=#FFFFFF>Reimbue Last - Repeats the last imbuing attempt", false, false);
            AddButton(25, 124, 4017, 4018, 10007, GumpButtonType.Reply, 0);
            AddHtml(66, 126, 430, 18, "<BASEFONT COLOR=#FFFFFF>Imbue Last Item - Auto targets the last imbued item", false, false);
            AddButton(25, 153, 4017, 4018, 10008, GumpButtonType.Reply, 0);
            AddHtml(66, 155, 430, 18, "<BASEFONT COLOR=#FFFFFF>Imbue Last Property - Imbues a new item with the last property", false, false);

            AddButton(25, 184, 4017, 4018, 10010, GumpButtonType.Reply, 0);
            AddHtml(66, 186, 430, 18, "<BASEFONT COLOR=#FFFFFF>Unravel Item - Extracts magical ingredients from an item, destroying it", false, false);
            AddButton(25, 213, 4017, 4018, 10011, GumpButtonType.Reply, 0);
            AddHtml(66, 215, 430, 18, "<BASEFONT COLOR=#FFFFFF>Unravel Container - Unravels all items in a container", false, false);

            AddButton(25, 242, 4017, 4018, 10008, GumpButtonType.Reply, 0);
            AddHtml(66, 244, 430, 18, "<BASEFONT COLOR=#FFFFFF>Soul Reinforcement - Fortify a cursed artifact", false, false);

            AddButton(19, 301, 4017, 4018, 10002, GumpButtonType.Reply, 0);
            this.AddLabel(58, 302, 1359, "CANCEL");

        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
            PlayerMobile pm = from as PlayerMobile;

            int buttonNum = 0;

            if (info.ButtonID > 0 && info.ButtonID < 10000)
                buttonNum = 1;
            else if (info.ButtonID > 20004)
                buttonNum = 30000;
            else
                buttonNum = info.ButtonID;

            switch (buttonNum)
            {
                case 0:
                    {
                        //Close
                        break;
                    }
                case 1:
                    {
                        break;
                    }
                case 10000:
                    {
                        break;
                    }
                case 10001:
                    {
                        break;
                    }
                case 10002:  // = Cancel button
                    {
                        break;
                    }
                case 10005:  // = Imbue Item
                    {
                        from.CloseGump(typeof(ImbuingGump));
                        bool sforge = false;
                        CheckSoulForge(from, 1, out sforge);

                        if (sforge != true)
                        {
                            from.SendLocalizedMessage(1079787);
                            break;
                        }
                        from.SendLocalizedMessage(1079589);
                        from.Target = new InternalTargetC();

                        break;
                    }
                case 10006:  // = ReImbue Last ( Mod & Item )
                    {
                        Item it = pm.ImbLast_Item as Item;

                        from.CloseGump(typeof(ImbuingGump));
                        bool sforge = false;
                        CheckSoulForge(from, 1, out sforge);

                        if (sforge != true)
                        {
                            from.SendLocalizedMessage(1079787);
                            break;
                        }

                        if (pm.ImbLast_Item == null || pm.ImbLast_Mod == 0 || pm.ImbLast_ModInt == 0)
                        {
                            from.SendLocalizedMessage(1113572);
                            break;
                        }

                        if (pm.ImbLast_Item is BaseWeapon) { BaseWeapon Ti = pm.ImbLast_Item as BaseWeapon; if (Ti.TimesImbued >= 20) { from.SendMessage("This item has been modified too many times and cannot be imbued any further."); break; } }
						#region Lightsaber Crafting
						if (pm.ImbLast_Item is BaseWeapon) { BaseWeapon Te = pm.ImbLast_Item as BaseWeapon; if (Te.TimesEmpowered >= 1) { from.SendMessage("This item has been Empowered and cannot be imbued."); break; } }
						#endregion
						#region Spellcrafting
						if (pm.ImbLast_Item is BaseWeapon) { BaseWeapon Tc = pm.ImbLast_Item as BaseWeapon; if (Tc.TimesCrafted >= 1) { from.SendMessage("This item has been Spellcrafted and cannot be imbued."); break; } }
						#endregion
                        if (pm.ImbLast_Item is BaseArmor) { BaseArmor Ti = pm.ImbLast_Item as BaseArmor; if (Ti.TimesImbued >= 20) { from.SendMessage("This item has been modified too many times and cannot be imbued any further."); break; } }
						#region Spellcrafting
						if (pm.ImbLast_Item is BaseArmor) { BaseArmor Tc = pm.ImbLast_Item as BaseArmor; if (Tc.TimesCrafted >= 1) { from.SendMessage("This item has been Spellcrafted and cannot be imbued."); break; } }
						#endregion
                        if (pm.ImbLast_Item is BaseJewel) { BaseJewel Ti = pm.ImbLast_Item as BaseJewel; if (Ti.TimesImbued >= 20) { from.SendMessage("This item has been modified too many times and cannot be imbued any further."); break; } }
						#region Spellcrafting
						if (pm.ImbLast_Item is BaseJewel) { BaseJewel Tc = pm.ImbLast_Item as BaseJewel; if (Tc.TimesCrafted >= 1) { from.SendMessage("This item has been Spellcrafted and cannot be imbued."); break; } }
						#endregion
                        if (pm.ImbLast_Item is BaseClothing) { BaseClothing Ti = pm.ImbLast_Item as BaseClothing; if (Ti.TimesImbued >= 20) { from.SendMessage("This item has been modified too many times and cannot be imbued any further."); break; } }
						#region Spellcrafting
						if (pm.ImbLast_Item is BaseClothing) { BaseClothing Tc = pm.ImbLast_Item as BaseClothing; if (Tc.TimesCrafted >= 1) { from.SendMessage("This item has been Spellcrafted and cannot be imbued."); break; } }
						#endregion

                        if ( it.LootType == LootType.Blessed)
                        {
                            from.SendLocalizedMessage(1080444);
                            break;
                        }

                        else
                        {
                            ImbuingGumpC.ImbueItem(from, pm.ImbLast_Item, pm.ImbLast_Mod, pm.ImbLast_ModInt);
                            from.SendGump(new ImbuingGump(from));
                        }

                        break;
                    }
                  
                case 10007:  // = Imbue Last ( Select Last imbued Item )
                    {
                        from.CloseGump(typeof(ImbuingGump));
                        bool sforge = false;
                        CheckSoulForge(from, 1, out sforge);

                        if (sforge != true)
                        {
                            from.SendLocalizedMessage(1079787);
                            break;
                        }

                        if ( pm.ImbLast_Item == null )
                            from.SendLocalizedMessage(1113572);
                        else
                            ImbuingGump.ImbueStep1(from, pm.ImbLast_Item);

                        break;
                    }
                case 10008:  // = Imbue Last Mod( To target Item )
                    {
                        from.CloseGump(typeof(ImbuingGump));
                        bool sforge = false;
                        CheckSoulForge(from, 1, out sforge);

                        if (sforge != true)
                        {
                            from.SendLocalizedMessage(1079787);
                            break;
                        }

                        if (pm.ImbLast_Mod == 0 || pm.ImbLast_ModInt == 0)
                        {
                            from.SendLocalizedMessage(1113572);
                            break;
                        }
                        else
                            ImbuingGump.ImbueLastProp(from, pm.ImbLast_Mod, pm.ImbLast_ModInt);

                        break;
                    }
                case 10010:  // = Unravel Item
                    {
                        from.CloseGump(typeof(ImbuingGump));
                        bool sforge = false;
                        CheckSoulForge(from, 1, out sforge);

                        if (sforge != true)
                        {
                            from.SendLocalizedMessage(1080433);
                            break;
                        }

                        from.SendLocalizedMessage(1080422); // What item do you wish to unravel?
                        from.Target = new InternalTargetA();
                        break;
                    }
                case 10011:  // = Unravel Container
                    {
                        from.CloseGump(typeof(ImbuingGump));
                        bool sforge = false;
                        CheckSoulForge(from, 1, out sforge);

                        if (sforge != true)
                        {
                            from.SendLocalizedMessage(1080433);
                            break;
                        }
                        from.SendMessage("What Container do you wish to unravel the contents of?");

                        from.Target = new InternalTargetB();
                        break;
                    }
            }
            return;
        }

        private class InternalTargetA : Target
        {
            public bool m_Success;

            public InternalTargetA()
                : base(8, false, TargetFlags.None)
            {
                AllowNonlocal = true;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                Mobile m_mob = from;
                PlayerMobile pm = from as PlayerMobile;

                Item m_obj = o as Item;
                double oDo = 0;
                int oMods = 0;
                int oIntense = 0;

                if (o is BaseWeapon || o is BaseArmor || o is BaseJewel || o is BaseClothing)
                {
                    if (m_obj.RootParent == m_mob)
                    {
                        if (o is BaseWeapon)
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
                            else if (o is BaseMeleeWeapon)
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

                            oDo = Math.Round(oDo);
                            oIntense = Convert.ToInt32(oDo);

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

                            oDo = Math.Round(oDo);
                            oIntense = Convert.ToInt32(oDo);
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

                            oDo = Math.Round(oDo);
                            oIntense = Convert.ToInt32(oDo);
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

                            oDo = Math.Round(oDo);
                            oIntense = Convert.ToInt32(oDo);
                        }

                        int URavBonus = pm.Imb_SFBonus;
                        if (oIntense > 0)
                        {
                            m_Success = false;
                            if (oIntense <= (200 - URavBonus)) { m_Success = m_mob.CheckSkill(SkillName.Imbuing, 0.0, 45.0); }
                            if (oIntense > (200 - URavBonus) && oIntense < (480 - URavBonus))
                            {
                                if (m_mob.Skills[SkillName.Imbuing].Base >= 45.0)
                                {
                                    m_Success = m_mob.CheckSkill(SkillName.Imbuing, 45.0, 95.0);
                                }
                                else
                                {
                                    m_mob.SendLocalizedMessage(1080434);
                                    return;
                                }
                            }
                            if (oIntense >= (480 - URavBonus))
                            {
                                if (m_mob.Skills[SkillName.Imbuing].Base >= 45.0)
                                {
                                    m_Success = m_mob.CheckSkill(SkillName.Imbuing, 95.0, 120.0);
                                }
                                else
                                {
                                    m_mob.SendLocalizedMessage(1080434);
                                    return;
                                }
                            }

                            if (!m_Success)
                            {
                                Effects.PlaySound(m_mob.Location, m_mob.Map, 0x3BF);
                                m_mob.SendLocalizedMessage(1080428); // Fail
                                return;
                            }
                            else
                            {

                                if (oIntense <= (200 - URavBonus)) { m_obj.Delete(); m_mob.AddToBackpack(new MagicalResidue()); }
                                if (oIntense > (200 - URavBonus) && oIntense < (480 - URavBonus)) { m_obj.Delete(); m_mob.AddToBackpack(new EnchantEssence()); }
                                if (oIntense >= (480 - URavBonus)) { m_obj.Delete(); m_mob.AddToBackpack(new RelicFragment()); }

                                Effects.PlaySound(m_mob.Location, m_mob.Map, 0x1ED);
                                Effects.SendLocationParticles( EffectItem.Create(m_mob.Location, m_mob.Map, EffectItem.DefaultDuration), 0x373A, 10, 30, 0, 4, 0, 0);
                                m_mob.SendLocalizedMessage(1080429); // Unravelled :P

                                return;
                            }
                        }
                        else
                        {
                            m_mob.SendLocalizedMessage(1080437);
                            return;
                        }
                    }
                    else
                    {
                        m_mob.SendLocalizedMessage(1080424);
                        return;
                    }
                }
                else
                {
                    m_mob.SendLocalizedMessage(1080425);
                    return;
                }
               // return;
            }
        }


        private class InternalTargetB : Target
        {
            public bool m_Success = false;

            public InternalTargetB()
                : base(8, false, TargetFlags.None)
            {
                AllowNonlocal = true;
            }
            protected override void OnTarget(Mobile from, object o)
            {
                double oDo = 0;
                int oMods = 0;
                int oIntense = 0;
                Mobile m_mob = from;
                PlayerMobile pm = from as PlayerMobile;

                if (o is BaseContainer)
                {
                    Item m_obj = o as Item;

                    if (m_obj.RootParent == m_mob)
                    {
                        BaseContainer sBag = o as BaseContainer;

                        object[] stuffs = sBag.FindItemsByType(typeof(object));

                        foreach (object item in stuffs)
                        {
                            oDo = 0;
                            oMods = 0;
                            oIntense = 0;

                            if (item is BaseWeapon)
                            {
                                BaseWeapon w = item as BaseWeapon;
                                if (item is BaseRanged)
                                {
                                    BaseRanged r = item as BaseRanged;
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
                                else if (item is BaseMeleeWeapon)
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

                                oDo = Math.Round(oDo);
                                oIntense = Convert.ToInt32(oDo);

                            }

                            if (item is BaseArmor)
                            {
                                BaseArmor w = item as BaseArmor;

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

                                oDo = Math.Round(oDo);
                                oIntense = Convert.ToInt32(oDo);
                            }

                            if (item is BaseJewel)
                            {
                                BaseJewel w = item as BaseJewel;

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

                                oDo = Math.Round(oDo);
                                oIntense = Convert.ToInt32(oDo);
                            }
                            if (item is BaseClothing)
                            {
                                BaseClothing w = item as BaseClothing;

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

                                oDo = Math.Round(oDo);
                                oIntense = Convert.ToInt32(oDo);
                            }

                            int URavBonus = pm.Imb_SFBonus;

                            if (oIntense > 0)
                            {
                                m_Success = false;
                                if (oIntense <= (200 - URavBonus)) { m_Success = m_mob.CheckSkill(SkillName.Imbuing, 0.0, 45.0); }
                                if (oIntense > (200 - URavBonus) && oIntense < (480 - URavBonus))
                                {
                                    if (m_mob.Skills[SkillName.Imbuing].Base >= 45.0)
                                    {
                                        m_Success = m_mob.CheckSkill(SkillName.Imbuing, 45.0, 95.0);
                                    }
                                    else
                                    {
                                        m_mob.SendLocalizedMessage(1080434);
                                        return;
                                    }
                                }
                                if (oIntense >= (480 - URavBonus))
                                {
                                    if (m_mob.Skills[SkillName.Imbuing].Base >= 45.0)
                                    {
                                        m_Success = m_mob.CheckSkill(SkillName.Imbuing, 95.0, 120.0);
                                    }
                                    else
                                    {
                                        m_mob.SendLocalizedMessage(1080434);
                                        return;
                                    }
                                }

                                if (!m_Success)
                                {
                                    Effects.PlaySound(m_mob.Location, m_mob.Map, 0x3BF);
                                    m_mob.SendLocalizedMessage(1080428); // Fail
                                    return;
                                }
                                else
                                {
                                    Item obj = item as Item;
                                    if (oIntense <= (200 - URavBonus)) { obj.Delete(); m_mob.AddToBackpack(new MagicalResidue()); }
                                    if (oIntense > (200 - URavBonus) && oIntense < (480 - URavBonus)) { obj.Delete(); m_mob.AddToBackpack(new EnchantEssence()); }
                                    if (oIntense >= (480 - URavBonus)) { obj.Delete(); m_mob.AddToBackpack(new RelicFragment()); }

                                    Effects.PlaySound(m_mob.Location, m_mob.Map, 0x1ED);
                                    Effects.SendLocationParticles(
                                    EffectItem.Create(m_mob.Location, m_mob.Map, EffectItem.DefaultDuration), 0x373A, 10, 30, 0, 4, 0, 0);
                                    m_mob.SendLocalizedMessage(1080429); // Unravelled :P
                                    oDo = 0; oIntense = 0;
                                }
                            }
                            else
                            {
                                m_mob.SendLocalizedMessage(1080437);
                            }
                        }
                    }
                    else
                    {
                        m_mob.SendMessage(2499, "You must target a container you are holding..");
                        return;
                    }
                }
                else
                {
                    m_mob.SendMessage(2499, "That is not a container..");
                    return;
                }
                return;
            }
        } 

        private class InternalTargetC : Target
        {
            public InternalTargetC()
                : base(8, false, TargetFlags.None)
            {
                AllowNonlocal = true;
            }


            protected override void OnTarget(Mobile from, object o)
            {
                int Irf = ImbuingGump.GetItemRef( o );
                PlayerMobile pm = from as PlayerMobile;
                if (Irf > 0)
                {
                    pm.ImbLast_Item = o;
                    pm.ImbLast_Iref = Irf;
                }
                else
                {
                    from.SendLocalizedMessage(1079576);
                    return;
                }

                ImbuingGump.ImbueStep1(from, o);
                return;
            }
        }

        public static int GetItemRef(object o)
        {
            int Ir = 0;
            if (o is BaseWeapon) { Ir = 1; }
            if (o is BaseRanged) { Ir = 2; }
            if (o is BaseArmor) { Ir = 3; }
            if (o is BaseShield) { Ir = 4; }
            if (o is BaseClothing) { Ir = 5; }
            if (o is BaseJewel) { Ir = 6; }

            return Ir;
        }

        // === Choose Target and Check ===
        public static void ImbueStep1(Mobile from, object o)
        {
            Item it = o as Item;

            if (o is BaseWeapon) { BaseWeapon Ti = o as BaseWeapon; if (Ti.TimesImbued >= 20) { from.SendMessage("This item has been modified too many times and cannot be imbued any further."); return; } }
			#region Lightsaber Crafting
			if (o is BaseWeapon) { BaseWeapon Te = o as BaseWeapon; if (Te.TimesEmpowered >= 1) { from.SendMessage("This item has been Empowered and cannot be imbued."); return; } }
			#endregion
			#region Spellcrafting
			if (o is BaseWeapon) { BaseWeapon Tc = o as BaseWeapon; if (Tc.TimesCrafted >= 1) { from.SendMessage("This item has been Spellcrafted and cannot be imbued."); return; } }
			#endregion
            if (o is BaseArmor) { BaseArmor Ti = o as BaseArmor; if (Ti.TimesImbued >= 20) { from.SendMessage("This item has been modified too many times and cannot be imbued any further."); return; } }
			#region Spellcrafting
			if (o is BaseArmor) { BaseArmor Tc = o as BaseArmor; if (Tc.TimesCrafted >= 1) { from.SendMessage("This item has been Spellcrafted and cannot be imbued."); return; } }
			#endregion
            if (o is BaseJewel) { BaseJewel Ti = o as BaseJewel; if (Ti.TimesImbued >= 20) { from.SendMessage("This item has been modified too many times and cannot be imbued any further."); return; } }
			#region Spellcrafting
			if (o is BaseJewel) { BaseJewel Ti = o as BaseJewel; if (Ti.TimesCrafted >= 20) { from.SendMessage("This item has been Spellcrafted and cannot be imbued."); return; } }
			#endregion
            if (o is BaseHat) { BaseClothing Ti = o as BaseClothing; if (Ti.TimesImbued >= 20) { from.SendMessage("This item has been modified too many times and cannot be imbued any further."); return; } }

            if (it.LootType == LootType.Blessed)
            {
                from.SendLocalizedMessage(1080444);
                return;
            }

            from.SendGump(new ImbuingGumpB(from, o));
            return;
        }             

        // === Imbue Target with Last Prop ===
        public static void ImbueLastProp(Mobile from, int Mod, int Mint)
        {
            from.Target = new InternalTargetD();

            return;
        }

        private class InternalTargetD : Target
        {
            public InternalTargetD()
                : base(8, false, TargetFlags.None)
            {
                AllowNonlocal = true;
            }
            protected override void OnTarget(Mobile from, object o)
            {
                PlayerMobile pm = from as PlayerMobile;
                int Imod = pm.ImbLast_Mod;
                Item it = o as Item;

                if (o is BaseWeapon) { BaseWeapon Ti = o as BaseWeapon; if (Ti.TimesImbued >= 20) { from.SendMessage("This item has been modified too many times and cannot be imbued any further."); return; } }
				#region Lightsaber Crafting
				if (o is BaseWeapon) { BaseWeapon Te = o as BaseWeapon; if (Te.TimesEmpowered >= 1) { from.SendMessage("This item has been Empowered and cannot be imbued."); return; } }
				#endregion
				#region Spellcrafting
				if (o is BaseWeapon) { BaseWeapon Tc = o as BaseWeapon; if (Tc.TimesCrafted >= 1) { from.SendMessage("This item has been Spellcrafted and cannot be imbued."); return; } }
				#endregion
                if (o is BaseArmor) { BaseArmor Ti = o as BaseArmor; if (Ti.TimesImbued >= 20) { from.SendMessage("This item has been modified too many times and cannot be imbued any further."); return; } }
				#region Spellcrafting
				if (o is BaseArmor) { BaseArmor Tc = o as BaseArmor; if (Tc.TimesCrafted >= 1) { from.SendMessage("This item has been Spellcrafted and cannot be imbued."); return; } }
				#endregion
                if (o is BaseJewel) { BaseJewel Ti = o as BaseJewel; if (Ti.TimesImbued >= 20) { from.SendMessage("This item has been modified too many times and cannot be imbued any further."); return; } }
				#region Spellcrafting
				if (o is BaseJewel) { BaseJewel Tc = o as BaseJewel; if (Tc.TimesCrafted >= 1) { from.SendMessage("This item has been Spellcrafted and cannot be imbued."); return; } }
				#endregion
                if (o is BaseClothing) { BaseClothing Ti = o as BaseClothing; if (Ti.TimesImbued >= 20) { from.SendMessage("This item has been modified too many times and cannot be imbued any further."); return; } }
				#region Spellcrafting
				if (o is BaseClothing) { BaseClothing Tc = o as BaseClothing; if (Tc.TimesCrafted >= 1) { from.SendMessage("This item has been Spellcrafted and cannot be imbued."); return; } }
				#endregion

                if (it.LootType == LootType.Blessed)
                {
                    from.SendLocalizedMessage(1080444);
                    return;
                }

                if (o is BaseMeleeWeapon)
                {
                    if (Imod == 1 || Imod == 2 || Imod == 12 || Imod == 13 || Imod == 16 || Imod == 21 || Imod == 22 || (Imod >= 25 && Imod <= 41) || Imod >= 101)
                    {
                        ImbuingGumpC.ImbueItem(from, o, Imod, pm.ImbLast_ModInt);
                        return;
                    }
                }
                else if (o is BaseRanged)
                {
                    if (Imod == 1 || Imod == 2 || Imod == 12 || Imod == 13 || Imod == 16 || Imod == 21 || Imod == 22 || Imod == 60 || Imod == 61 || (Imod >= 25 && Imod <= 41) || Imod >= 101)
                    {
                        ImbuingGumpC.ImbueItem(from, o, Imod, pm.ImbLast_ModInt);
                        return;
                    }
                }
                else if (o is BaseShield)
                {
                    if (Imod == 1 || Imod == 2 || Imod == 19 || Imod == 16 || Imod == 22 || Imod == 24 || Imod == 42)
                    {
                        ImbuingGumpC.ImbueItem(from, o, Imod, pm.ImbLast_ModInt);
                        return;
                    }
                }
                else if (o is BaseArmor)
                {
                    if (Imod == 3 || Imod == 4 || Imod == 5 || Imod == 9 || Imod == 10 || Imod == 11 || Imod == 21 || Imod == 23 || (Imod >= 17 && Imod <= 19))
                    {
                        ImbuingGumpC.ImbueItem(from, o, Imod, pm.ImbLast_ModInt);
                        return;
                    }
                }
                else if (o is BaseClothing)
                {
                    if (Imod == 3 || Imod == 4 || Imod == 5 || Imod == 9 || Imod == 10 || Imod == 11 || Imod == 21 || Imod == 23 || (Imod >= 17 && Imod <= 19))
                    {
                        ImbuingGumpC.ImbueItem(from, o, Imod, pm.ImbLast_ModInt);
                        return;
                    }
                }
                else if (o is BaseJewel)
                {
                    if (Imod == 1 || Imod == 2 || Imod == 6 || Imod == 7 || Imod == 8 || Imod == 12 || Imod == 10 || Imod == 11 || Imod == 20 || Imod == 21 || Imod == 23 || Imod == 21 || (Imod >= 14 && Imod <= 18) || (Imod >= 51 && Imod <= 55) || Imod >= 151)
                    {
                        ImbuingGumpC.ImbueItem(from, o, Imod, pm.ImbLast_ModInt);
                        return;
                    }
                }
                else
                    from.SendMessage("The selected item cannot be Imbued with the last Property..");

                return;
            }
        }

    }
}        
