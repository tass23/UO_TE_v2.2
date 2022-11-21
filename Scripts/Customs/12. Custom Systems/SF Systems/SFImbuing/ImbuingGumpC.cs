using System;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Server.Network;
using Server.Items;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.SkillHandlers
{
    public class ImbuingGumpC : Gump
    {
        private static int i_Mod;
        private static string s_Mod;
        private static object i_Item;
        private static int m_Intensity = 0;
        private static int modvalue;
        private static int i_Inc = 0;
        private static string m_Gem = "";
        private static int m_Gem_no = 0;
        private static string m_A = "";
        private static int m_A_no = 0;
        private static string m_B = "";
        private static int m_B_no = 0;
        private static double i_Success = 0;
        private static double i_Diff;
        private static string RepModName = "";
        private static int m_Imax;
        private static int s_Weight;
        private static int m_Desc;
        private static int IWmax;

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
                bool isQueensSForge = (item.ItemID >= 17015 && item.ItemID <= 17030); // TerMur QueensSoulForge (+5% bonus & easier unravels)
                bool isSForge = (item.ItemID >= 16995 && item.ItemID <= 17010); // Standard SoulForge

                if (isSForge || isQueensSForge)
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

        public ImbuingGumpC(Mobile from, object item, int mod, int value)
            : base(520, 340)
        {
            PlayerMobile m = from as PlayerMobile;
            i_Item = item;
            i_Mod = mod;
            modvalue = value;

            ImbuingGumpC.GetMaterials( mod );

            if (modvalue < i_Inc) { modvalue = i_Inc; }
            if (modvalue > m_Imax) { modvalue = m_Imax; }

            int i_Imbued = 0;
            if (item is BaseWeapon) { BaseWeapon Ti = item as BaseWeapon; i_Imbued = Ti.TimesImbued; }
            if (item is BaseArmor) { BaseArmor Ti = item as BaseArmor; i_Imbued = Ti.TimesImbued; }
            if (item is BaseJewel) { BaseJewel Ti = item as BaseJewel; i_Imbued = Ti.TimesImbued; }
            if (item is BaseClothing) { BaseClothing Ti = item as BaseClothing; i_Imbued = Ti.TimesImbued; }

            m_Gem_no = GetMGemNo(m_Imax, i_Inc, modvalue);
            m_A_no = GetMANo(m_Imax, i_Inc, modvalue);
            m_B_no = GetMBNo(m_Imax, i_Inc, modvalue);

            // ------------------------------ Gump Menu -------------------------------------------------------------
            AddPage(0);
            this.AddBackground(0, 0, 540, 450, 9270);
            this.AddAlphaRegion(17, 17, 503, 20);
            this.AddAlphaRegion(17, 45, 245, 140);
            this.AddAlphaRegion(275, 45, 245, 140);
            this.AddAlphaRegion(17, 195, 245, 140);
            this.AddAlphaRegion(275, 195, 245, 140);
            this.AddAlphaRegion(17, 345, 503, 60);
            this.AddAlphaRegion(17, 415, 503, 20);

            this.AddLabel(187, 18, 1359, "IMBUING CONFIRMATION");

            this.AddLabel(57, 49, 1359, "PROPERTY INFORMATION");

            AddHtmlLocalized(30, 80, 80, 17, 1114270, 0xFFFFFF, false, false);
            AddHtml(100, 80, 150, 17, String.Format("<BASEFONT COLOR=#FFFFFF> {0}", s_Mod), false, false);

            AddHtmlLocalized(30, 120, 80, 17, 1114272, 0xFFFFFF, false, false);
            if (s_Weight == 100) { AddHtml(100, 120, 80, 17, "<BASEFONT COLOR=#CCCCFF> 1.0x", false, false); }
            if (s_Weight == 110) { AddHtml(100, 120, 80, 17, "<BASEFONT COLOR=#CCCCFF> 1.1x", false, false); }
            if (s_Weight == 120) { AddHtml(100, 120, 80, 17, "<BASEFONT COLOR=#CCCCFF> 1.2x", false, false); }
            if (s_Weight == 130) { AddHtml(100, 120, 80, 17, "<BASEFONT COLOR=#CCCCFF> 1.3x", false, false); }
            if (s_Weight == 140) { AddHtml(100, 120, 80, 17, "<BASEFONT COLOR=#CCCCFF> 1.4x", false, false); }
            if (s_Weight == 150) { AddHtml(100, 120, 80, 17, "<BASEFONT COLOR=#CCCCFF> 1.5x", false, false); }

            double c_modv = modvalue;
            double c_max = m_Imax;
            double cur_int = (c_modv / c_max) * 100;
            cur_int = Math.Round(cur_int);
            m_Intensity = Convert.ToInt32(cur_int);

            AddHtmlLocalized(30, 140, 80, 17, 1114273, 0xFFFFFF, false, false);
            AddHtml(100, 140, 80, 17, String.Format("<BASEFONT COLOR=#CCCCFF> {0}%", m_Intensity), false, false);

            this.AddLabel(96, 199, 1359, "MATERIALS");

            AddHtml(40, 230, 180, 17, String.Format("<BASEFONT COLOR=#FFFFFF> {0}", m_A), false, false);
            AddHtml(210, 230, 40, 17, String.Format("<BASEFONT COLOR=#CCCCFF> {0}", m_A_no), false, false);

            AddHtml(40, 255, 180, 17, String.Format("<BASEFONT COLOR=#FFFFFF> {0}", m_Gem), false, false);
            AddHtml(210, 255, 40, 17, String.Format("<BASEFONT COLOR=#CCCCFF> {0}", m_Gem_no), false, false);
            if (m_B_no > 0)
            {
                AddHtml(40, 280, 180, 17, String.Format("<BASEFONT COLOR=#FFFFFF> {0}", m_B), false, false);
                AddHtml(210, 280, 40, 17, String.Format("<BASEFONT COLOR=#CCCCFF> {0}", m_B_no), false, false);
            }

            AddHtmlLocalized(290, 65, 215, 110, m_Desc, 0xFFFFFF, false, false); // = Mod Description

            this.AddLabel(365, 199, 1359, "RESULTS");

            RepModName = "";
            int i_TpropW = GetTotalWeight(i_Item); int i_TMods = GetTotalMods(i_Item);
            if (RepModName == "M'KAY!") { RepModName = s_Mod; }

            if (m_Imax <= 1) { m_Intensity = 100; }
            double c_i = m_Intensity;
            double c_w = s_Weight;

            double cur_wei = c_i * (c_w / 100);
            cur_wei = Math.Round(cur_wei);

            IWmax = 450;
            if (item is BaseWeapon)
            {
                BaseWeapon tit = item as BaseWeapon;
                if (tit.Quality == WeaponQuality.Exceptional)
                    IWmax = 500;
                else if (tit.Quality == WeaponQuality.Regular)
                    IWmax = 450;
                else
                    IWmax = 400;
            }
            else if (item is BaseArmor)
            {
                BaseArmor tit = item as BaseArmor;
                if (tit.Quality == ArmorQuality.Exceptional)
                    IWmax = 500;
                else if (tit.Quality == ArmorQuality.Regular)
                    IWmax = 450;
                else
                    IWmax = 400;
            }
            else if (item is BaseClothing)
            {
                BaseClothing tit = item as BaseClothing;
                if (tit.Quality == ClothingQuality.Exceptional)
                    IWmax = 500;
                else if (tit.Quality == ClothingQuality.Regular)
                    IWmax = 450;
                else
                    IWmax = 400;
            }
            else
            {
                BaseJewel tit = item as BaseJewel;
                if (tit.Quality == ArmorQuality.Exceptional)
                    IWmax = 500;
                else if (tit.Quality == ArmorQuality.Regular)
                    IWmax = 450;
                else
                    IWmax = 400;
            }

            AddHtmlLocalized(288, 230, 150, 17, 1113645, 0xFFFFFF, false, false);
            AddHtml(458, 230, 80, 17, String.Format("<BASEFONT COLOR=#CCFFCC> {0}/5", i_TMods + 1), false, false);
            AddHtmlLocalized(288, 250, 150, 17, 1113646, 0xFFFFFF, false, false);
            AddHtml(458, 250, 80, 17, String.Format("<BASEFONT COLOR=#CCFFCC> {0}/{1}", i_TpropW + Convert.ToInt32(cur_wei), IWmax), false, false);


            AddHtmlLocalized(288, 270, 150, 17, 1113647, 0xFFFFFF, false, false);
            AddHtml(458, 270, 80, 17, String.Format("<BASEFONT COLOR=#CCFFCC> {0}/20", i_Imbued), false, false);

            AddHtmlLocalized(30, 100, 80, 17, 1114271, 0xFFFFFF, false, false);  //  REPLACES ITEM?  NAME
            if (RepModName != "") { AddHtml(100, 100, 150, 17, String.Format("<BASEFONT COLOR=#FFFFFF> {0}", RepModName), false, false); }

            i_Diff = 0; i_Success = 0;

            double iBonus = m.Skills[SkillName.Imbuing].Base / 200;
            if (m.Race == Race.Gargoyle) { iBonus += 0.05; }
            if (IWmax > 450) { iBonus += 0.1; }
            if ( m.Imb_SFBonus > 0 )
                iBonus += m.Imb_SFBonus / 100;

            i_Diff = (((i_TpropW + cur_wei) / 4) * (1.65 - iBonus)) + ((i_TpropW + cur_wei) / 25);
            i_Success = (m.Skills[SkillName.Imbuing].Base - (i_Diff - 25)) * 1;
            double iX = (m.Skills[SkillName.Imbuing].Base - (i_Diff - 25)) * 100;

            if ((i_Diff - 25) > m.Skills[SkillName.Imbuing].Base) { iX = 0; i_Success = 0; }
            if (iX < 0.005) { iX = 0; i_Success = 0; }

            i_Success = Math.Round(i_Success, 2);

            AddHtmlLocalized(305, 300, 150, 17, 1044057, 0xFFFFFF, false, false);

            if (i_Success <= 1) { AddHtml(445, 300, 80, 17, "<BASEFONT COLOR=#FF5511>" + String.Format("{0}%", i_Success), false, false); }
            else if (i_Success > 1) { AddHtml(445, 300, 80, 17, "<BASEFONT COLOR=#EE6611>" + String.Format("{0}%", i_Success), false, false); }
            else if (i_Success > 10) { AddHtml(445, 300, 80, 17, "<BASEFONT COLOR=#DD7711>" + String.Format("{0}%", i_Success), false, false); }
            else if (i_Success > 20) { AddHtml(445, 300, 80, 17, "<BASEFONT COLOR=#CC8811>" + String.Format("{0}%", i_Success), false, false); }
            else if (i_Success > 30) { AddHtml(445, 300, 80, 17, "<BASEFONT COLOR=#BB9911>" + String.Format("{0}%", i_Success), false, false); }
            else if (i_Success > 40) { AddHtml(445, 300, 80, 17, "<BASEFONT COLOR=#AAAA11>" + String.Format("{0}%", i_Success), false, false); }
            else if (i_Success > 50) { AddHtml(445, 300, 80, 17, "<BASEFONT COLOR=#99BB11>" + String.Format("{0}%", i_Success), false, false); }
            else if (i_Success > 60) { AddHtml(445, 300, 80, 17, "<BASEFONT COLOR=#88CC11>" + String.Format("{0}%", i_Success), false, false); }
            else if (i_Success > 70) { AddHtml(445, 300, 80, 17, "<BASEFONT COLOR=#77DD11>" + String.Format("{0}%", i_Success), false, false); }
            else if (i_Success > 80) { AddHtml(445, 300, 80, 17, "<BASEFONT COLOR=#66EE11>" + String.Format("{0}%", i_Success), false, false); }
            else if (i_Success > 90) { AddHtml(445, 300, 80, 17, "<BASEFONT COLOR=#55FF11>" + String.Format("{0}%", i_Success), false, false); }

            if (m_Imax > 1)
            {
                if (modvalue <= 0) { modvalue = i_Inc; }
                AddHtmlLocalized(235, 350, 100, 17, 1062300, 0xFFFFFF, false, false);
                if (i_Mod != 41) { AddHtml(254, 374, 40, 17, String.Format("<BASEFONT COLOR=#CCCCFF> {0}%", modvalue), false, false); }
                else { AddHtml(254, 374, 40, 17, String.Format("<BASEFONT COLOR=#CCCCFF> -{0}", (30 - modvalue)), false, false); }

                AddButton(230, 376, 5223, 5223, 10051, GumpButtonType.Reply, 0); // To Minimum Value
                AddImageTiled(232, 377, 11, 15, 5603);
                AddButton(211, 376, 5223, 5223, 10052, GumpButtonType.Reply, 0); // Dec Value by %
                AddImageTiled(212, 376, 13, 15, 5603);
                AddButton(192, 376, 5223, 5223, 10053, GumpButtonType.Reply, 0); // Dec Value by 1
                AddImageTiled(190, 376, 17, 15, 5603);
                AddImageTiled(195, 376, 17, 15, 5603);

                AddButton(308, 376, 5224, 5224, 10054, GumpButtonType.Reply, 0); // To Maximum Value
                AddImageTiled(305, 377, 11, 15, 5601);
                AddImageTiled(308, 376, 7, 16, 5224);
                AddButton(327, 376, 5224, 5224, 10055, GumpButtonType.Reply, 0); // Inc Value by %
                AddImageTiled(325, 376, 11, 15, 5601);
                AddImageTiled(327, 376, 6, 16, 5224);
                AddButton(346, 376, 5224, 5224, 10056, GumpButtonType.Reply, 0); // Inc Value by 1
                AddImageTiled(345, 376, 17, 15, 5601);
                AddImageTiled(350, 376, 17, 15, 5601);
            }

            AddButton(19, 415, 4017, 4018, 10099, GumpButtonType.Reply, 0);
            this.AddLabel(58, 416, 1359, "CANCEL");

            AddButton(400, 415, 4017, 4018, 10100, GumpButtonType.Reply, 0);
            this.AddLabel(439, 416, 1359, "IMBUE ITEM");

        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
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
                case 10002:
                    {
                        break;
                    }
                case 10051:
                    {
                        if (modvalue > i_Inc) { modvalue -= i_Inc; }
                        from.SendGump(new ImbuingGumpC(from, i_Item, i_Mod, modvalue));
                        break;
                    }
                case 10052:
                    {
                        if (i_Mod == 42 || i_Mod == 24) { modvalue -= 20; }
                        if (i_Mod == 13 || i_Mod == 20 || i_Mod == 21) { modvalue -= 10; }
                        else { modvalue -= 5; }
                        from.SendGump(new ImbuingGumpC(from, i_Item, i_Mod, modvalue));
                        break;
                    }
                case 10053:
                    {
                        modvalue -= 100;
                        from.SendGump(new ImbuingGumpC(from, i_Item, i_Mod, modvalue));
                        break;
                    }
                case 10054:
                    {
                        modvalue += i_Inc;
                        from.SendGump(new ImbuingGumpC(from, i_Item, i_Mod, modvalue));
                        break;
                    }
                case 10055:
                    {
                        if (i_Mod == 42 || i_Mod == 24) { modvalue += 20; }
                        if (i_Mod == 13 || i_Mod == 20 || i_Mod == 21) { modvalue += 10; }
                        else { modvalue += 5; }
                        from.SendGump(new ImbuingGumpC(from, i_Item, i_Mod, modvalue));
                        break;
                    }
                case 10056:
                    {
                        modvalue += 100;
                        from.SendGump(new ImbuingGumpC(from, i_Item, i_Mod, modvalue));
                        break;
                    }

                case 10099: // - Cancel
                    {
                        break;
                    }
                case 10100:  // = Imbue the Item
                    {
                        ImbuingGumpC.ImbueItem(from, i_Item, i_Mod, modvalue );
                        from.SendGump(new ImbuingGump(from));
                        break;
                    }
                case 10011:  // = 
                    {
                        break;
                    }
            }
            return;
        }


        public int GetTotalWeight(object itw)
        {
            double oDo = 0;

            if (itw is BaseWeapon)
            {
                BaseWeapon i = itw as BaseWeapon;

                if (i.Attributes.DefendChance > 0)
                {
                    if (i_Mod != 1)
                    {
                        oDo += (8.6 * i.Attributes.DefendChance);
                    }
                    else
                    {
                        RepModName = "M'KAY!";
                    }
                }
                if (itw is BaseRanged)
                {
                    BaseRanged r = itw as BaseRanged;
                    if (r.Velocity > 0) { if (i_Mod != 60) { oDo += (130 / 50) * r.Velocity; } else { RepModName = "M'KAY!"; } }
                    if (r.Balanced == true) { if (i_Mod != 61) { oDo += 150; } else { RepModName = "M'KAY!"; } }
                }
                if (i.Attributes.AttackChance > 0) { if (i_Mod != 2) { oDo += (130 / 15) * i.Attributes.AttackChance; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.RegenHits > 0) { if (i_Mod != 3) { oDo += (50 * i.Attributes.RegenHits); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.RegenStam > 0) { if (i_Mod != 4) { oDo += (100 / 3) * i.Attributes.RegenStam; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.RegenMana > 0) { if (i_Mod != 5) { oDo += (50 * i.Attributes.RegenMana); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusStr > 0) { if (i_Mod != 6) { oDo += (110 / 8) * i.Attributes.BonusStr; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusDex > 0) { if (i_Mod != 7) { oDo += (110 / 8) * i.Attributes.BonusDex; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusInt > 0) { if (i_Mod != 8) { oDo += (110 / 8) * i.Attributes.BonusInt; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusHits > 0) { if (i_Mod != 9) { oDo += 22 * i.Attributes.BonusHits; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusStam > 0) { if (i_Mod != 10) { oDo += (100 / 8) * i.Attributes.BonusStam; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusMana > 0) { if (i_Mod != 11) { oDo += (110 / 8) * i.Attributes.BonusMana; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.WeaponDamage > 0) { if (i_Mod != 12) { oDo += (2 * i.Attributes.WeaponDamage); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.WeaponSpeed > 0) { if (i_Mod != 13) { oDo += (110 / 30) * i.Attributes.WeaponSpeed; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.SpellDamage > 0) { if (i_Mod != 14) { oDo += (100 / 12) * i.Attributes.SpellDamage; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.CastRecovery > 0) { if (i_Mod != 15) { oDo += (40 * i.Attributes.CastRecovery); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.LowerManaCost > 0) { if (i_Mod != 17) { oDo += (110 / 8) * i.Attributes.LowerManaCost; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.LowerRegCost > 0) { if (i_Mod != 18) { oDo += 5 * i.Attributes.LowerRegCost; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.ReflectPhysical > 0) { if (i_Mod != 19) { oDo += (100 / 15) * i.Attributes.ReflectPhysical; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.EnhancePotions > 0) { if (i_Mod != 20) { oDo += (4 * i.Attributes.EnhancePotions); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.Luck > 0) { if (i_Mod != 21) { oDo += i.Attributes.Luck; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.SpellChanneling > 0)
                {
                    if (i_Mod != 22) { oDo += 100; } else { RepModName = "M'KAY!"; }
                    if (i.Attributes.CastSpeed == 0) { if (i_Mod != 16) { oDo += 140; } else { RepModName = "M'KAY!"; } }
                    if (i.Attributes.CastSpeed == 1) { if (i_Mod != 16) { oDo += 280; } else { RepModName = "M'KAY!"; } }
                }
                else if (i.Attributes.CastSpeed > 0) { if (i_Mod != 16) { oDo += (140 * i.Attributes.CastSpeed); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.NightSight > 0) { if (i_Mod != 23) { oDo += 50; } else { RepModName = "M'KAY!"; } }

                if (i.SkillBonuses.GetBonus(0) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(0)); }
                if (i.SkillBonuses.GetBonus(1) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(1)); }
                if (i.SkillBonuses.GetBonus(2) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(2)); }
                if (i.SkillBonuses.GetBonus(3) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(3)); }
                if (i.SkillBonuses.GetBonus(4) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(4)); }

                if (i.WeaponAttributes.LowerStatReq > 0 && i_Mod != 24) { oDo += i.WeaponAttributes.LowerStatReq; }
                if (i.WeaponAttributes.HitLeechHits > 0 && i_Mod != 25) { oDo += ((110 / 50) * i.WeaponAttributes.HitLeechHits); }
                if (i.WeaponAttributes.HitLeechStam > 0 && i_Mod != 26) { oDo += (2 * i.WeaponAttributes.HitLeechStam); }
                if (i.WeaponAttributes.HitLeechMana > 0 && i_Mod != 27) { oDo += ((110 / 50) * i.WeaponAttributes.HitLeechMana); }
                if (i.WeaponAttributes.HitLowerAttack > 0 && i_Mod != 28) { oDo += ((110 / 50) * i.WeaponAttributes.HitLowerAttack); }
                if (i.WeaponAttributes.HitLowerDefend > 0 && i_Mod != 29) { oDo += ((130 / 50) * i.WeaponAttributes.HitLowerDefend); }
                if (i.WeaponAttributes.HitColdArea > 0 && i_Mod != 30) { oDo += (2 * i.WeaponAttributes.HitColdArea); }
                if (i.WeaponAttributes.HitFireArea > 0 && i_Mod != 31) { oDo += (2 * i.WeaponAttributes.HitFireArea); }
                if (i.WeaponAttributes.HitPoisonArea > 0 && i_Mod != 32) { oDo += (2 * i.WeaponAttributes.HitPoisonArea); }
                if (i.WeaponAttributes.HitEnergyArea > 0 && i_Mod != 33) { oDo += (2 * i.WeaponAttributes.HitEnergyArea); }
                if (i.WeaponAttributes.HitPhysicalArea > 0 && i_Mod != 34) { oDo += (2 * i.WeaponAttributes.HitPhysicalArea); }
                if (i.WeaponAttributes.HitMagicArrow > 0 && i_Mod != 35) { oDo += (2.4 * i.WeaponAttributes.HitMagicArrow); }
                if (i.WeaponAttributes.HitHarm > 0 && i_Mod != 36) { oDo += (2.2 * i.WeaponAttributes.HitHarm); }
                if (i.WeaponAttributes.HitFireball > 0 && i_Mod != 37) { oDo += (2.8 * i.WeaponAttributes.HitFireball); }
                if (i.WeaponAttributes.HitLightning > 0 && i_Mod != 38) { oDo += (2.8 * i.WeaponAttributes.HitLightning); }
                if (i.WeaponAttributes.HitDispel > 0 && i_Mod != 39) { oDo += (2 * i.WeaponAttributes.HitDispel); }
                if (i.WeaponAttributes.UseBestSkill > 0 && i_Mod != 40) { oDo += 150; }
                if (i.WeaponAttributes.MageWeapon > 0 && i_Mod != 41) { oDo += 20 * i.WeaponAttributes.MageWeapon; }
                if (i.WeaponAttributes.DurabilityBonus > 0 && i_Mod != 42) { oDo += i.WeaponAttributes.DurabilityBonus; }
                if (i.WeaponAttributes.ResistPhysicalBonus > 0 && i_Mod != 43) { oDo += ((100 / 15) * i.WeaponAttributes.ResistPhysicalBonus); }
                if (i.WeaponAttributes.ResistFireBonus > 0 && i_Mod != 44) { oDo += ((100 / 15) * i.WeaponAttributes.ResistFireBonus); }
                if (i.WeaponAttributes.ResistColdBonus > 0 && i_Mod != 45) { oDo += ((100 / 15) * i.WeaponAttributes.ResistColdBonus); }
                if (i.WeaponAttributes.ResistPoisonBonus > 0 && i_Mod != 46) { oDo += ((100 / 15) * i.WeaponAttributes.ResistPoisonBonus); }
                if (i.WeaponAttributes.ResistEnergyBonus > 0 && i_Mod != 47) { oDo += ((100 / 15) * i.WeaponAttributes.ResistEnergyBonus); }
                if (i.Slayer == SlayerName.Silver || i.Slayer == SlayerName.Repond || i.Slayer == SlayerName.ReptilianDeath || i.Slayer == SlayerName.Exorcism || i.Slayer == SlayerName.ArachnidDoom || i.Slayer == SlayerName.ElementalBan || i.Slayer == SlayerName.Fey) { oDo += 130; }
                else if (i.Slayer != SlayerName.None) { oDo += 110; }
                if (i.Slayer2 == SlayerName.Silver || i.Slayer2 == SlayerName.Repond || i.Slayer2 == SlayerName.ReptilianDeath || i.Slayer2 == SlayerName.Exorcism || i.Slayer2 == SlayerName.ArachnidDoom || i.Slayer2 == SlayerName.ElementalBan || i.Slayer2 == SlayerName.Fey) { oDo += 130; }
                else if (i.Slayer2 != SlayerName.None) { oDo += 110; }
            }

            else if (itw is BaseArmor)
            {
                BaseArmor i = itw as BaseArmor;

                if (i.Attributes.DefendChance > 0) { if (i_Mod != 1) { oDo += ((130 / 15) * i.Attributes.DefendChance); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.AttackChance > 0) { if (i_Mod != 2) { oDo += ((130 / 15) * i.Attributes.AttackChance); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.RegenHits > 0) { if (i_Mod != 3) { oDo += (50 * i.Attributes.RegenHits); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.RegenStam > 0) { if (i_Mod != 4) { oDo += ((100 / 3) * i.Attributes.RegenStam); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.RegenMana > 0) { if (i_Mod != 5) { oDo += (50 * i.Attributes.RegenMana); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusStr > 0) { if (i_Mod != 6) { oDo += ((110 / 8) * i.Attributes.BonusStr); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusDex > 0) { if (i_Mod != 7) { oDo += ((110 / 8) * i.Attributes.BonusDex); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusInt > 0) { if (i_Mod != 8) { oDo += ((110 / 8) * i.Attributes.BonusInt); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusHits > 0) { if (i_Mod != 9) { oDo += (22 * i.Attributes.BonusHits); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusStam > 0) { if (i_Mod != 10) { oDo += ((100 / 8) * i.Attributes.BonusStam); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusMana > 0) { if (i_Mod != 11) { oDo += ((110 / 8) * i.Attributes.BonusMana); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.WeaponDamage > 0) { if (i_Mod != 12) { oDo += (2 * i.Attributes.WeaponDamage); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.WeaponSpeed > 0) { if (i_Mod != 13) { oDo += ((110 / 30) * i.Attributes.WeaponSpeed); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.SpellDamage > 0) { if (i_Mod != 14) { oDo += ((100 / 12) * i.Attributes.SpellDamage); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.CastRecovery > 0) { if (i_Mod != 15) { oDo += (40 * i.Attributes.CastRecovery); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.LowerManaCost > 0) { if (i_Mod != 17) { oDo += ((110 / 8) * i.Attributes.LowerManaCost); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.LowerRegCost > 0) { if (i_Mod != 18) { oDo += (5 * i.Attributes.LowerRegCost); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.ReflectPhysical > 0) { if (i_Mod != 19) { oDo += ((100 / 15) * i.Attributes.ReflectPhysical); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.EnhancePotions > 0) { if (i_Mod != 20) { oDo += (4 * i.Attributes.EnhancePotions); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.Luck > 0) { if (i_Mod != 21) { oDo += i.Attributes.Luck; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.SpellChanneling > 0)
                {
                    if (i_Mod != 22) { oDo += 100; } else { RepModName = "M'KAY!"; }
                    if (i.Attributes.CastSpeed == 0) { if (i_Mod != 16) { oDo += 140; } else { RepModName = "M'KAY!"; } }
                    if (i.Attributes.CastSpeed == 1) { if (i_Mod != 16) { oDo += 280; } else { RepModName = "M'KAY!"; } }
                }
                else if (i.Attributes.CastSpeed > 0) { if (i_Mod != 16) { oDo += (140 * i.Attributes.CastSpeed); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.NightSight > 0) { if (i_Mod != 23) { oDo += 50; } else { RepModName = "M'KAY!"; } }

                if (i.SkillBonuses.GetBonus(0) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(0)); }
                if (i.SkillBonuses.GetBonus(1) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(1)); }
                if (i.SkillBonuses.GetBonus(2) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(2)); }
                if (i.SkillBonuses.GetBonus(3) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(3)); }
                if (i.SkillBonuses.GetBonus(4) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(4)); }


                if (i.ArmorAttributes.LowerStatReq > 0) { oDo += 100; }
                if (i.ArmorAttributes.MageArmor > 0) { oDo += 140; }
                if (i.ArmorAttributes.DurabilityBonus > 0) { oDo += i.ArmorAttributes.DurabilityBonus; }
                if (i.Quality != ArmorQuality.Exceptional)
                {
                    if (i.PhysicalBonus > 0) { oDo += ((100 / 15) * i.PhysicalBonus); }
                    if (i.FireBonus > 0) { oDo += ((100 / 15) * i.FireBonus); }
                    if (i.ColdBonus > 0) { oDo += ((100 / 15) * i.ColdBonus); }
                    if (i.PoisonBonus > 0) { oDo += ((100 / 15) * i.PoisonBonus); }
                    if (i.EnergyBonus > 0) { oDo += ((100 / 15) * i.EnergyBonus); }
                }
            }
            else if (itw is BaseJewel)
            {
                BaseJewel i = itw as BaseJewel;

                if (i.Attributes.DefendChance > 0) { if (i_Mod != 1) { oDo += ((130 / 15) * i.Attributes.DefendChance); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.AttackChance > 0) { if (i_Mod != 2) { oDo += ((130 / 15) * i.Attributes.AttackChance); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.RegenHits > 0) { if (i_Mod != 3) { oDo += (50 * i.Attributes.RegenHits); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.RegenStam > 0) { if (i_Mod != 4) { oDo += ((100 / 3) * i.Attributes.RegenStam); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.RegenMana > 0) { if (i_Mod != 5) { oDo += (50 * i.Attributes.RegenMana); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusStr > 0) { if (i_Mod != 6) { oDo += ((110 / 8) * i.Attributes.BonusStr); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusDex > 0) { if (i_Mod != 7) { oDo += ((110 / 8) * i.Attributes.BonusDex); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusInt > 0) { if (i_Mod != 8) { oDo += ((110 / 8) * i.Attributes.BonusInt); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusHits > 0) { if (i_Mod != 9) { oDo += (22 * i.Attributes.BonusHits); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusStam > 0) { if (i_Mod != 10) { oDo += ((100 / 8) * i.Attributes.BonusStam); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusMana > 0) { if (i_Mod != 11) { oDo += ((110 / 8) * i.Attributes.BonusMana); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.WeaponDamage > 0) { if (i_Mod != 12) { oDo += (2 * i.Attributes.WeaponDamage); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.WeaponSpeed > 0) { if (i_Mod != 13) { oDo += ((110 / 30) * i.Attributes.WeaponSpeed); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.SpellDamage > 0) { if (i_Mod != 14) { oDo += ((100 / 12) * i.Attributes.SpellDamage); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.CastRecovery > 0) { if (i_Mod != 15) { oDo += (40 * i.Attributes.CastRecovery); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.CastSpeed > 0) { if (i_Mod != 16) { oDo += (140 * i.Attributes.CastSpeed); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.LowerManaCost > 0) { if (i_Mod != 17) { oDo += ((110 / 8) * i.Attributes.LowerManaCost); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.LowerRegCost > 0) { if (i_Mod != 18) { oDo += (5 * i.Attributes.LowerRegCost); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.ReflectPhysical > 0) { if (i_Mod != 19) { oDo += ((100 / 15) * i.Attributes.ReflectPhysical); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.EnhancePotions > 0) { if (i_Mod != 20) { oDo += (4 * i.Attributes.EnhancePotions); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.Luck > 0) { if (i_Mod != 21) { oDo += i.Attributes.Luck; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.SpellChanneling > 0) { if (i_Mod != 22) { oDo += 100; } else { RepModName = "M'KAY!"; } }

                if (i.Attributes.NightSight > 0) { if (i_Mod != 23) { oDo += 50; } else { RepModName = "M'KAY!"; } }

                if (i.SkillBonuses.GetBonus(0) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(0)); }
                if (i.SkillBonuses.GetBonus(1) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(1)); }
                if (i.SkillBonuses.GetBonus(2) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(2)); }
                if (i.SkillBonuses.GetBonus(3) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(3)); }
                if (i.SkillBonuses.GetBonus(4) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(4)); }

                if (i.Resistances.Physical > 0) { oDo += ((100 / 15) * i.Resistances.Physical); }
                if (i.Resistances.Fire > 0) { oDo += ((100 / 15) * i.Resistances.Fire); }
                if (i.Resistances.Cold > 0) { oDo += ((100 / 15) * i.Resistances.Cold); }
                if (i.Resistances.Poison > 0) { oDo += ((100 / 15) * i.Resistances.Poison); }
                if (i.Resistances.Energy > 0) { oDo += ((100 / 15) * i.Resistances.Energy); }
            }
            else if (itw is BaseClothing)
            {
                BaseClothing i = itw as BaseClothing;

                if (i.Attributes.DefendChance > 0) { if (i_Mod != 1) { oDo += ((130 / 15) * i.Attributes.DefendChance); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.AttackChance > 0) { if (i_Mod != 2) { oDo += ((130 / 15) * i.Attributes.AttackChance); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.RegenHits > 0) { if (i_Mod != 3) { oDo += (50 * i.Attributes.RegenHits); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.RegenStam > 0) { if (i_Mod != 4) { oDo += ((100 / 3) * i.Attributes.RegenStam); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.RegenMana > 0) { if (i_Mod != 5) { oDo += (50 * i.Attributes.RegenMana); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusStr > 0) { if (i_Mod != 6) { oDo += ((110 / 8) * i.Attributes.BonusStr); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusDex > 0) { if (i_Mod != 7) { oDo += ((110 / 8) * i.Attributes.BonusDex); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusInt > 0) { if (i_Mod != 8) { oDo += ((110 / 8) * i.Attributes.BonusInt); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusHits > 0) { if (i_Mod != 9) { oDo += (22 * i.Attributes.BonusHits); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusStam > 0) { if (i_Mod != 10) { oDo += ((100 / 8) * i.Attributes.BonusStam); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.BonusMana > 0) { if (i_Mod != 11) { oDo += ((110 / 8) * i.Attributes.BonusMana); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.WeaponDamage > 0) { if (i_Mod != 12) { oDo += (2 * i.Attributes.WeaponDamage); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.WeaponSpeed > 0) { if (i_Mod != 13) { oDo += ((110 / 30) * i.Attributes.WeaponSpeed); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.SpellDamage > 0) { if (i_Mod != 14) { oDo += ((100 / 12) * i.Attributes.SpellDamage); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.CastRecovery > 0) { if (i_Mod != 15) { oDo += (40 * i.Attributes.CastRecovery); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.CastSpeed > 0) { if (i_Mod != 16) { oDo += (140 * i.Attributes.CastSpeed); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.LowerManaCost > 0) { if (i_Mod != 17) { oDo += ((110 / 8) * i.Attributes.LowerManaCost); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.LowerRegCost > 0) { if (i_Mod != 18) { oDo += (5 * i.Attributes.LowerRegCost); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.ReflectPhysical > 0) { if (i_Mod != 19) { oDo += ((100 / 15) * i.Attributes.ReflectPhysical); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.EnhancePotions > 0) { if (i_Mod != 20) { oDo += (4 * i.Attributes.EnhancePotions); } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.Luck > 0) { if (i_Mod != 21) { oDo += i.Attributes.Luck; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.SpellChanneling > 0) { if (i_Mod != 22) { oDo += 100; } else { RepModName = "M'KAY!"; } }
                if (i.Attributes.NightSight > 0) { if (i_Mod != 23) { oDo += 50; } else { RepModName = "M'KAY!"; } }

                if (i.SkillBonuses.GetBonus(0) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(0)); }
                if (i.SkillBonuses.GetBonus(1) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(1)); }
                if (i.SkillBonuses.GetBonus(2) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(2)); }
                if (i.SkillBonuses.GetBonus(3) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(3)); }
                if (i.SkillBonuses.GetBonus(4) > 0) { oDo += ((140 / 15) * i.SkillBonuses.GetBonus(4)); }
                if (i.Quality != ClothingQuality.Exceptional)
                {
                    if (i.Resistances.Physical > 0) { oDo += ((100 / 15) * i.Resistances.Physical); }
                    if (i.Resistances.Fire > 0) { oDo += ((100 / 15) * i.Resistances.Fire); }
                    if (i.Resistances.Cold > 0) { oDo += ((100 / 15) * i.Resistances.Cold); }
                    if (i.Resistances.Poison > 0) { oDo += ((100 / 15) * i.Resistances.Poison); }
                    if (i.Resistances.Energy > 0) { oDo += ((100 / 15) * i.Resistances.Energy); }
                }
            }

            oDo = Math.Round(oDo);
            int oInt = Convert.ToInt32(oDo);
            return oInt;

        }
        public int GetTotalMods(object itw)
        {
            int oMods = 0;

            if (itw is BaseWeapon)
            {
                BaseWeapon i = itw as BaseWeapon;

                if (itw is BaseRanged)
                {
                    BaseRanged r = itw as BaseRanged;
                    if (r.Velocity > 0) { oMods += 1; }
                    if (r.Balanced == true) { oMods += 1; }
                }
                if (i.Attributes.DefendChance > 0 && i_Mod != 1) { oMods += 1; }
                if (i.Attributes.AttackChance > 0 && i_Mod != 2) { oMods += 1; }
                if (i.Attributes.RegenHits > 0 && i_Mod != 3) { oMods += 1; }
                if (i.Attributes.RegenStam > 0 && i_Mod != 4) { oMods += 1; }
                if (i.Attributes.RegenMana > 0 && i_Mod != 5) { oMods += 1; }
                if (i.Attributes.BonusStr > 0 && i_Mod != 6) { oMods += 1; }
                if (i.Attributes.BonusDex > 0 && i_Mod != 7) { oMods += 1; }
                if (i.Attributes.BonusInt > 0 && i_Mod != 8) { oMods += 1; }
                if (i.Attributes.BonusHits > 0 && i_Mod != 9) { oMods += 1; }
                if (i.Attributes.BonusStam > 0 && i_Mod != 10) { oMods += 1; }
                if (i.Attributes.BonusMana > 0 && i_Mod != 11) { oMods += 1; }
                if (i.Attributes.WeaponDamage > 0 && i_Mod != 12) { oMods += 1; }
                if (i.Attributes.WeaponSpeed > 0 && i_Mod != 13) { oMods += 1; }
                if (i.Attributes.SpellDamage > 0 && i_Mod != 14) { oMods += 1; }
                if (i.Attributes.CastRecovery > 0 && i_Mod != 15) { oMods += 1; }
                if (i.Attributes.LowerManaCost > 0 && i_Mod != 17) { oMods += 1; }
                if (i.Attributes.LowerRegCost > 0 && i_Mod != 18) { oMods += 1; }
                if (i.Attributes.ReflectPhysical > 0 && i_Mod != 19) { oMods += 1; }
                if (i.Attributes.EnhancePotions > 0 && i_Mod != 20) { oMods += 1; }
                if (i.Attributes.Luck > 0 && i_Mod != 21) { oMods += 1; }
                if (i.Attributes.SpellChanneling > 0 && i_Mod != 22)
                {
                    oMods += 1;
                    if (i.Attributes.CastSpeed == 0 && i_Mod != 16) { oMods += 1; }
                    if (i.Attributes.CastSpeed == 1 && i_Mod != 16) { oMods += 1; }
                }
                else if (i.Attributes.CastSpeed > 0) { oMods += 1; }
                if (i.Attributes.NightSight > 0 && i_Mod != 23) { oMods += 1; }

                if (i.SkillBonuses.GetBonus(0) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(1) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(2) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(3) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(4) > 0) { oMods += 1; }

                if (i.WeaponAttributes.LowerStatReq > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitLeechHits > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitLeechStam > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitLeechMana > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitLowerAttack > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitLowerDefend > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitColdArea > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitFireArea > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitPoisonArea > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitEnergyArea > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitPhysicalArea > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitMagicArrow > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitHarm > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitFireball > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitLightning > 0) { oMods += 1; }
                if (i.WeaponAttributes.HitDispel > 0) { oMods += 1; }
                if (i.WeaponAttributes.UseBestSkill > 0) { oMods += 1; }
                if (i.WeaponAttributes.MageWeapon > 0) { oMods += 1; }
                if (i.WeaponAttributes.DurabilityBonus > 0) { oMods += 1; }
                if (i.WeaponAttributes.ResistPhysicalBonus > 0) { oMods += 1; }
                if (i.WeaponAttributes.ResistFireBonus > 0) { oMods += 1; }
                if (i.WeaponAttributes.ResistColdBonus > 0) { oMods += 1; }
                if (i.WeaponAttributes.ResistPoisonBonus > 0) { oMods += 1; }
                if (i.WeaponAttributes.ResistEnergyBonus > 0) { oMods += 1; }
                if (i.Slayer != SlayerName.None) { oMods += 1; }
                if (i.Slayer2 != SlayerName.None) { oMods += 1; }
            }

            if (i_Item is BaseArmor)
            {
                BaseArmor i = i_Item as BaseArmor;

                if (i.Attributes.DefendChance > 0 && i_Mod != 1) { oMods += 1; }
                if (i.Attributes.AttackChance > 0 && i_Mod != 2) { oMods += 1; }
                if (i.Attributes.RegenHits > 0 && i_Mod != 3) { oMods += 1; }
                if (i.Attributes.RegenStam > 0 && i_Mod != 4) { oMods += 1; }
                if (i.Attributes.RegenMana > 0 && i_Mod != 5) { oMods += 1; }
                if (i.Attributes.BonusStr > 0 && i_Mod != 6) { oMods += 1; }
                if (i.Attributes.BonusDex > 0 && i_Mod != 7) { oMods += 1; }
                if (i.Attributes.BonusInt > 0 && i_Mod != 8) { oMods += 1; }
                if (i.Attributes.BonusHits > 0 && i_Mod != 9) { oMods += 1; }
                if (i.Attributes.BonusStam > 0 && i_Mod != 10) { oMods += 1; }
                if (i.Attributes.BonusMana > 0 && i_Mod != 11) { oMods += 1; }
                if (i.Attributes.WeaponDamage > 0 && i_Mod != 12) { oMods += 1; }
                if (i.Attributes.WeaponSpeed > 0 && i_Mod != 13) { oMods += 1; }
                if (i.Attributes.SpellDamage > 0 && i_Mod != 14) { oMods += 1; }
                if (i.Attributes.CastRecovery > 0 && i_Mod != 15) { oMods += 1; }
                if (i.Attributes.LowerManaCost > 0 && i_Mod != 17) { oMods += 1; }
                if (i.Attributes.LowerRegCost > 0 && i_Mod != 18) { oMods += 1; }
                if (i.Attributes.ReflectPhysical > 0 && i_Mod != 19) { oMods += 1; }
                if (i.Attributes.EnhancePotions > 0 && i_Mod != 20) { oMods += 1; }
                if (i.Attributes.Luck > 0 && i_Mod != 21) { oMods += 1; }
                if (i.Attributes.SpellChanneling > 0 && i_Mod != 22)
                {
                    oMods += 1;
                    if (i.Attributes.CastSpeed == 0 && i_Mod != 16) { oMods += 1; }
                    if (i.Attributes.CastSpeed == 1 && i_Mod != 16) { oMods += 1; }
                }
                else if (i.Attributes.CastSpeed > 0) { oMods += 1; }
                if (i.Attributes.NightSight > 0 && i_Mod != 23) { oMods += 1; }

                if (i.SkillBonuses.GetBonus(0) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(1) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(2) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(3) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(4) > 0) { oMods += 1; }
                if (i.ArmorAttributes.LowerStatReq > 0) { oMods += 1; }
                if (i.ArmorAttributes.MageArmor > 0) { oMods += 1; }
                if (i.ArmorAttributes.DurabilityBonus > 0) { oMods += 1; }
                if (i.Quality != ArmorQuality.Exceptional)
                {
                    if (i.PhysicalBonus > 0) { oMods += 1; }
                    if (i.FireBonus > 0) { oMods += 1; }
                    if (i.ColdBonus > 0) { oMods += 1; }
                    if (i.PoisonBonus > 0) { oMods += 1; }
                    if (i.EnergyBonus > 0) { oMods += 1; }
                }
            }
            if (i_Item is BaseJewel)
            {
                BaseJewel i = i_Item as BaseJewel;

                if (i.Attributes.DefendChance > 0 && i_Mod != 1) { oMods += 1; }
                if (i.Attributes.AttackChance > 0 && i_Mod != 2) { oMods += 1; }
                if (i.Attributes.RegenHits > 0 && i_Mod != 3) { oMods += 1; }
                if (i.Attributes.RegenStam > 0 && i_Mod != 4) { oMods += 1; }
                if (i.Attributes.RegenMana > 0 && i_Mod != 5) { oMods += 1; }
                if (i.Attributes.BonusStr > 0 && i_Mod != 6) { oMods += 1; }
                if (i.Attributes.BonusDex > 0 && i_Mod != 7) { oMods += 1; }
                if (i.Attributes.BonusInt > 0 && i_Mod != 8) { oMods += 1; }
                if (i.Attributes.BonusHits > 0 && i_Mod != 9) { oMods += 1; }
                if (i.Attributes.BonusStam > 0 && i_Mod != 10) { oMods += 1; }
                if (i.Attributes.BonusMana > 0 && i_Mod != 11) { oMods += 1; }
                if (i.Attributes.WeaponDamage > 0 && i_Mod != 12) { oMods += 1; }
                if (i.Attributes.WeaponSpeed > 0 && i_Mod != 13) { oMods += 1; }
                if (i.Attributes.SpellDamage > 0 && i_Mod != 14) { oMods += 1; }
                if (i.Attributes.CastRecovery > 0 && i_Mod != 15) { oMods += 1; }
                if (i.Attributes.CastSpeed > 0 && i_Mod != 16) { oMods += 1; }
                if (i.Attributes.LowerManaCost > 0 && i_Mod != 17) { oMods += 1; }
                if (i.Attributes.LowerRegCost > 0 && i_Mod != 18) { oMods += 1; }
                if (i.Attributes.ReflectPhysical > 0 && i_Mod != 19) { oMods += 1; }
                if (i.Attributes.EnhancePotions > 0 && i_Mod != 20) { oMods += 1; }
                if (i.Attributes.Luck > 0 && i_Mod != 21) { oMods += 1; }
                if (i.Attributes.SpellChanneling > 0 && i_Mod != 22) { oMods += 1; }
                if (i.Attributes.NightSight > 0 && i_Mod != 23) { oMods += 1; }

                if (i.SkillBonuses.GetBonus(0) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(1) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(2) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(3) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(4) > 0) { oMods += 1; }

                if (i.Resistances.Physical > 0) { oMods += 1; }
                if (i.Resistances.Fire > 0) { oMods += 1; }
                if (i.Resistances.Cold > 0) { oMods += 1; }
                if (i.Resistances.Poison > 0) { oMods += 1; }
                if (i.Resistances.Energy > 0) { oMods += 1; }
            }
            if (i_Item is BaseClothing)
            {
                BaseClothing i = i_Item as BaseClothing;

                if (i.Attributes.DefendChance > 0 && i_Mod != 1) { oMods += 1; }
                if (i.Attributes.AttackChance > 0 && i_Mod != 2) { oMods += 1; }
                if (i.Attributes.RegenHits > 0 && i_Mod != 3) { oMods += 1; }
                if (i.Attributes.RegenStam > 0 && i_Mod != 4) { oMods += 1; }
                if (i.Attributes.RegenMana > 0 && i_Mod != 5) { oMods += 1; }
                if (i.Attributes.BonusStr > 0 && i_Mod != 6) { oMods += 1; }
                if (i.Attributes.BonusDex > 0 && i_Mod != 7) { oMods += 1; }
                if (i.Attributes.BonusInt > 0 && i_Mod != 8) { oMods += 1; }
                if (i.Attributes.BonusHits > 0 && i_Mod != 9) { oMods += 1; }
                if (i.Attributes.BonusStam > 0 && i_Mod != 10) { oMods += 1; }
                if (i.Attributes.BonusMana > 0 && i_Mod != 11) { oMods += 1; }
                if (i.Attributes.WeaponDamage > 0 && i_Mod != 12) { oMods += 1; }
                if (i.Attributes.WeaponSpeed > 0 && i_Mod != 13) { oMods += 1; }
                if (i.Attributes.SpellDamage > 0 && i_Mod != 14) { oMods += 1; }
                if (i.Attributes.CastRecovery > 0 && i_Mod != 15) { oMods += 1; }
                if (i.Attributes.CastSpeed > 0 && i_Mod != 16) { oMods += 1; }
                if (i.Attributes.LowerManaCost > 0 && i_Mod != 17) { oMods += 1; }
                if (i.Attributes.LowerRegCost > 0 && i_Mod != 18) { oMods += 1; }
                if (i.Attributes.ReflectPhysical > 0 && i_Mod != 19) { oMods += 1; }
                if (i.Attributes.EnhancePotions > 0 && i_Mod != 20) { oMods += 1; }
                if (i.Attributes.Luck > 0 && i_Mod != 21) { oMods += 1; }
                if (i.Attributes.SpellChanneling > 0 && i_Mod != 22) { oMods += 1; }
                if (i.Attributes.NightSight > 0 && i_Mod != 23) { oMods += 1; }

                if (i.SkillBonuses.GetBonus(0) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(1) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(2) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(3) > 0) { oMods += 1; }
                if (i.SkillBonuses.GetBonus(4) > 0) { oMods += 1; }
                if (i.Quality != ClothingQuality.Exceptional)
                {
                    if (i.Resistances.Physical > 0) { oMods += 1; }
                    if (i.Resistances.Fire > 0) { oMods += 1; }
                    if (i.Resistances.Cold > 0) { oMods += 1; }
                    if (i.Resistances.Poison > 0) { oMods += 1; }
                    if (i.Resistances.Energy > 0) { oMods += 1; }
                }
            }

            return oMods;
        }

        // === Imbue Item with selected Properties ===
        public static void ImbueItem(Mobile from, object i, int mod, int Mvalue)
        {
            PlayerMobile pm = from as PlayerMobile;
            i_Item = i;

            Type res_gem = null;
            Type res_a = null;
            Type res_b = null;

            // Bonuses
            double iBonus = from.Skills[SkillName.Imbuing].Base / 200;
            if (from.Race == Race.Gargoyle) { iBonus += 0.05; }
            if (IWmax > 450) { iBonus += 0.1; }
            if ( pm.Imb_SFBonus > 0 )
                iBonus += pm.Imb_SFBonus / 100;

            i_Mod = mod;
            ImbuingGumpC.GetMaterials( mod );

            if (m_Gem == "Diamond") { res_gem = typeof(Diamond); }
            if (m_Gem == "Emerald") { res_gem = typeof(Emerald); }
            if (m_Gem == "Ruby") { res_gem = typeof(Ruby); }
            if (m_Gem == "Citrine") { res_gem = typeof(Citrine); }
            if (m_Gem == "Tourmaline") { res_gem = typeof(Tourmaline); }
            if (m_Gem == "Amber") { res_gem = typeof(Amber); }
            if (m_Gem == "Amethyst") { res_gem = typeof(Amethyst); }
            if (m_Gem == "Sapphire") { res_gem = typeof(Sapphire); }
            if (m_Gem == "Star Sapphire") { res_gem = typeof(StarSapphire); }

            if (m_A == "Magical Residue") { res_a = typeof(MagicalResidue); }
            if (m_A == "Enchanted Essence") { res_a = typeof(EnchantEssence); }
            if (m_A == "Relic Fragment") { res_a = typeof(RelicFragment); }

            if (m_B == "Essence of Persistence") { res_b = typeof(EssencePersistence); }
            if (m_B == "Essence of Singularity") { res_b = typeof(EssenceSingularity); }
            if (m_B == "Essence of Precision") { res_b = typeof(EssencePrecision); }
            if (m_B == "Essence of Diligence") { res_b = typeof(EssenceDiligence); }
            if (m_B == "Essence of Achievement") { res_b = typeof(EssenceAchievement); }
            if (m_B == "Essence of Order") { res_b = typeof(EssenceOrder); }
            if (m_B == "Essence of Feeling") { res_b = typeof(EssenceFeeling); }
            if (m_B == "Essence of Passion") { res_b = typeof(EssencePassion); }
            if (m_B == "Essence of Direction") { res_b = typeof(EssenceDirection); }
            if (m_B == "Essence of Balance") { res_b = typeof(EssenceBalance); }
            if (m_B == "Essence of Control") { res_b = typeof(EssenceControl); }
            if (m_B == "Fire Ruby") { res_b = typeof(FireRuby); }
            if (m_B == "Blue Diamond") { res_b = typeof(BlueDiamond); }
            if (m_B == "Turquoise") { res_b = typeof(Turquoise); }
            if (m_B == "Delicate Scales") { res_b = typeof(DelicateScales); }
            if (m_B == "White Pearl") { res_b = typeof(WhitePearl); }
            if (m_B == "Luminescent Fungi") { res_b = typeof(LuminescentFungi); }
            if (m_B == "Parasitic Plant") { res_b = typeof(ParasiticPlant); }
            if (m_B == "Crystalline Blackrock") { res_b = typeof(CrystallineBlackrock); }
            if (m_B == "Vial of Vitriol") { res_b = typeof(VialOfVitriol); }
            if (m_B == "Spider Carapace") { res_b = typeof(SpiderCarapace); }
            if (m_B == "Daemon Claw") { res_b = typeof(DaemonClaw); }
            if (m_B == "Lava Serpent Crust") { res_b = typeof(LavaSerpentCrust); }
            if (m_B == "Goblin Blood") { res_b = typeof(GoblinBlood); }
            if (m_B == "Undying Flesh") { res_b = typeof(UndyingFlesh); }
            if (m_B == "Boura Pelt") { res_b = typeof(BouraPelt); }
            if (m_B == "Abyssal Cloth") { res_b = typeof(AbyssalCloth); }
            if (m_B == "Powdered Iron") { res_b = typeof(PowderedIron); }
            if (m_B == "Arcanic Rune Stone") { res_b = typeof(ArcanicRuneStone); }
            if (m_B == "Slith Tongue") { res_b = typeof(SlithTongue); }
            if (m_B == "Raptor Teeth") { res_b = typeof(RaptorTeeth); }
            if (m_B == "Void Orb") { res_b = typeof(VoidOrb); }
            if (m_B == "Elven Fletching") { res_b = typeof(ElvenFletchings); }
            if (m_B == "Bottle of Ichor") { res_b = typeof(BottleIchor); }
            if (m_B == "Silver Snake Skin") { res_b = typeof(SilverSnakeSkin); }
            if (m_B == "Chaga Mushroom") { res_b = typeof(ChagaMushroom); }
            if (m_B == "Crushed Glass") { res_b = typeof(CrushedGlass); }
            if (m_B == "Reflective Wolf Eye") { res_b = typeof(ReflectiveWolfEye); }
            if (m_B == "Faery Dust") { res_b = typeof(FaeryDust); }
            if (m_B == "Crystal Shards") { res_b = typeof(CrystalShards); }
            if (m_B == "Seed of Renewal") { res_b = typeof(SeedRenewal); }

            int Iref = 0;
            if (i_Item is BaseWeapon) { Iref = 1; }
            if (i_Item is BaseRanged) { Iref = 2; }
            if (i_Item is BaseArmor) { Iref = 3; }
            if (i_Item is BaseShield) { Iref = 4; }
            if (i_Item is BaseClothing) { Iref = 5; }
            if (i_Item is BaseJewel) { Iref = 6; }

            if (from.Backpack == null || from.Backpack.GetAmount(res_gem) < m_Gem_no || from.Backpack.GetAmount(res_a) < m_A_no || from.Backpack.GetAmount(res_b) < m_B_no)
            {
                from.SendLocalizedMessage(1079773);

                if (from.Backpack.GetAmount(res_gem) < m_Gem_no)
                {
                    from.SendMessage(String.Format("You need more {0}", m_Gem));
                }
                if (from.Backpack.GetAmount(res_a) < m_A_no)
                {
                    from.SendMessage(String.Format("You need more {0}", m_A));
                }
                if (from.Backpack.GetAmount(res_b) < m_B_no)
                {
                    from.SendMessage(String.Format("You need more {0}", m_B));
                }
            }
            else
            {
                from.Backpack.ConsumeTotal(res_gem, m_Gem_no);
                from.Backpack.ConsumeTotal(res_a, m_A_no);
                from.Backpack.ConsumeTotal(res_b, m_B_no);

                pm.ImbLast_Item = i_Item;
                pm.ImbLast_Iref = Iref;
                pm.ImbLast_Mod = i_Mod;
                pm.ImbLast_ModInt = modvalue;

                bool m_Success = false;
                m_Success = from.CheckSkill(SkillName.Imbuing, i_Diff - 25, i_Diff + 25);

                if (m_Success)
                {
                    from.SendLocalizedMessage(1079775); // Success
                    from.PlaySound(0x5D1);
                    Effects.SendLocationParticles(
                    EffectItem.Create(from.Location, from.Map, EffectItem.DefaultDuration), 0x373A,
                          10, 30, 0, 4, 0, 0);
                    if (i_Item is BaseWeapon)
                    {
                        BaseWeapon it = i_Item as BaseWeapon;
                        it.TimesImbued += 1;

                        if (i_Mod == 1) { it.Attributes.DefendChance = modvalue; }
                        if (i_Mod == 2) { it.Attributes.AttackChance = modvalue; }
                        if (i_Mod == 3) { it.Attributes.RegenHits = modvalue; }
                        if (i_Mod == 4) { it.Attributes.RegenStam = modvalue; }
                        if (i_Mod == 5) { it.Attributes.RegenMana = modvalue; }
                        if (i_Mod == 6) { it.Attributes.BonusStr = modvalue; }
                        if (i_Mod == 7) { it.Attributes.BonusDex = modvalue; }
                        if (i_Mod == 8) { it.Attributes.BonusInt = modvalue; }
                        if (i_Mod == 9) { it.Attributes.BonusHits = modvalue; }
                        if (i_Mod == 10) { it.Attributes.BonusStam = modvalue; }
                        if (i_Mod == 11) { it.Attributes.BonusMana = modvalue; }
                        if (i_Mod == 12) { it.Attributes.WeaponDamage = modvalue; }
                        if (i_Mod == 13) { it.Attributes.WeaponSpeed = modvalue; }
                        if (i_Mod == 14) { it.Attributes.SpellDamage = modvalue; }
                        if (i_Mod == 16)
                        {
                            if (it.Attributes.CastSpeed < 0) { it.Attributes.CastSpeed = 0; }
                            else if (it.Attributes.CastSpeed == 0 || it.Attributes.CastSpeed == 1) { it.Attributes.CastSpeed = 1; }
                        }
                        if (i_Mod == 17) { it.Attributes.LowerManaCost = modvalue; }
                        if (i_Mod == 18) { it.Attributes.LowerRegCost = modvalue; }
                        if (i_Mod == 19) { it.Attributes.ReflectPhysical = modvalue; }
                        if (i_Mod == 20) { it.Attributes.EnhancePotions = modvalue; }
                        if (i_Mod == 21) { it.Attributes.Luck = modvalue; }
                        if (i_Mod == 22)
                        {
                            it.Attributes.SpellChanneling = 1;
                            it.Attributes.CastSpeed -= 1;
                        }
                        if (i_Mod == 23) { it.Attributes.NightSight = 1; }

                        if (i_Mod == 25) { it.WeaponAttributes.HitLeechHits = modvalue; }
                        if (i_Mod == 26) { it.WeaponAttributes.HitLeechStam = modvalue; }
                        if (i_Mod == 27) { it.WeaponAttributes.HitLeechMana = modvalue; }
                        if (i_Mod == 28) { it.WeaponAttributes.HitLowerAttack = modvalue; }
                        if (i_Mod == 29) { it.WeaponAttributes.HitLowerDefend = modvalue; }
                        if (i_Mod == 30) { it.WeaponAttributes.HitPhysicalArea = modvalue; }
                        if (i_Mod == 31) { it.WeaponAttributes.HitFireArea = modvalue; }
                        if (i_Mod == 32) { it.WeaponAttributes.HitColdArea = modvalue; }
                        if (i_Mod == 33) { it.WeaponAttributes.HitPoisonArea = modvalue; }
                        if (i_Mod == 34) { it.WeaponAttributes.HitEnergyArea = modvalue; }
                        if (i_Mod == 35) { it.WeaponAttributes.HitMagicArrow = modvalue; }
                        if (i_Mod == 36) { it.WeaponAttributes.HitHarm = modvalue; }
                        if (i_Mod == 37) { it.WeaponAttributes.HitFireball = modvalue; }
                        if (i_Mod == 38) { it.WeaponAttributes.HitLightning = modvalue; }
                        if (i_Mod == 39) { it.WeaponAttributes.HitDispel = modvalue; }
                        if (i_Mod == 40) { it.WeaponAttributes.UseBestSkill = 1; }
                        if (i_Mod == 41) { it.WeaponAttributes.MageWeapon = modvalue; }

                        if (i_Mod == 51) { it.WeaponAttributes.ResistPhysicalBonus = modvalue; }
                        if (i_Mod == 52) { it.WeaponAttributes.ResistFireBonus = modvalue; }
                        if (i_Mod == 53) { it.WeaponAttributes.ResistColdBonus = modvalue; }
                        if (i_Mod == 54) { it.WeaponAttributes.ResistPoisonBonus = modvalue; }
                        if (i_Mod == 55) { it.WeaponAttributes.ResistEnergyBonus = modvalue; }

                        if (i_Mod == 60) { BaseRanged rg = it as BaseRanged; rg.Velocity = modvalue; }
                        if (i_Mod == 61) { BaseRanged rg = it as BaseRanged; rg.Balanced = true; }

                        if (i_Mod == 101) { it.Slayer = SlayerName.OrcSlaying; }
                        if (i_Mod == 102) { it.Slayer = SlayerName.TrollSlaughter; }
                        if (i_Mod == 103) { it.Slayer = SlayerName.OgreTrashing; }
                        if (i_Mod == 104) { it.Slayer = SlayerName.DragonSlaying; }
                        if (i_Mod == 105) { it.Slayer = SlayerName.Terathan; }
                        if (i_Mod == 106) { it.Slayer = SlayerName.SnakesBane; }
                        if (i_Mod == 107) { it.Slayer = SlayerName.LizardmanSlaughter; }
                        //if (i_Mod == 108) { it.Slayer = SlayerName.DaemonDismissal; }
                        if (i_Mod == 109) { it.Slayer = SlayerName.GargoylesFoe; }
                        //if (i_Mod == 110) { it.Slayer = SlayerName.BalronDamnation; }
                        if (i_Mod == 111) { it.Slayer = SlayerName.Ophidian; }
                        if (i_Mod == 112) { it.Slayer = SlayerName.SpidersDeath; }
                        if (i_Mod == 113) { it.Slayer = SlayerName.ScorpionsBane; }
                        if (i_Mod == 114) { it.Slayer = SlayerName.FlameDousing; }
                        if (i_Mod == 115) { it.Slayer = SlayerName.WaterDissipation; }
                        if (i_Mod == 116) { it.Slayer = SlayerName.Vacuum; }
                        if (i_Mod == 117) { it.Slayer = SlayerName.ElementalHealth; }
                        if (i_Mod == 118) { it.Slayer = SlayerName.EarthShatter; }
                        if (i_Mod == 119) { it.Slayer = SlayerName.BloodDrinking; }
                        if (i_Mod == 120) { it.Slayer = SlayerName.SummerWind; }
                        if (i_Mod == 121) { it.Slayer = SlayerName.Silver; }
                        if (i_Mod == 122) { it.Slayer = SlayerName.Repond; }
                        if (i_Mod == 123) { it.Slayer = SlayerName.ReptilianDeath; }
                        if (i_Mod == 124) { it.Slayer = SlayerName.Exorcism; }
                        if (i_Mod == 125) { it.Slayer = SlayerName.ArachnidDoom; }
                        if (i_Mod == 126) { it.Slayer = SlayerName.ElementalBan; }
                    }
                    else if (Iref == 3)
                    {
                        BaseArmor it = i_Item as BaseArmor;
                        it.TimesImbued += 1;

                        if (i_Mod == 3) { it.Attributes.RegenHits = modvalue; }
                        if (i_Mod == 4) { it.Attributes.RegenStam = modvalue; }
                        if (i_Mod == 5) { it.Attributes.RegenMana = modvalue; }

                        if (i_Mod == 9) { it.Attributes.BonusHits = modvalue; }
                        if (i_Mod == 10) { it.Attributes.BonusStam = modvalue; }
                        if (i_Mod == 11) { it.Attributes.BonusMana = modvalue; }
                        if (i_Mod == 17) { it.Attributes.LowerManaCost = modvalue; }
                        if (i_Mod == 18) { it.Attributes.LowerRegCost = modvalue; }
                        if (i_Mod == 19) { it.Attributes.ReflectPhysical = modvalue; }
                        if (i_Mod == 21) { it.Attributes.Luck = modvalue; }
                        if (i_Mod == 23) { it.Attributes.NightSight = 1; }
                        if (i_Mod == 22)
                        {
                            it.Attributes.SpellChanneling = 1;
                            if (it.Attributes.CastSpeed == 0) { it.Attributes.CastSpeed = -1; }
                            if (it.Attributes.CastSpeed == 1) { it.Attributes.CastSpeed = 0; }
                        }
                        if (i_Mod == 16)
                        {
                            if (it.Attributes.CastSpeed < 0) { it.Attributes.CastSpeed = 0; }
                            else if (it.Attributes.CastSpeed == 0 || it.Attributes.CastSpeed == 1) { it.Attributes.CastSpeed = 1; }
                        }
                    }
                    else if (Iref == 4)
                    {
                        BaseShield it = i_Item as BaseShield;
                        it.TimesImbued += 1;

                        if (i_Mod == 1) { it.Attributes.DefendChance = modvalue; }
                        if (i_Mod == 2) { it.Attributes.AttackChance = modvalue; }
                        if (i_Mod == 19) { it.Attributes.ReflectPhysical = modvalue; }
                        if (i_Mod == 16)
                        {
                            if (it.Attributes.CastSpeed < 0) { it.Attributes.CastSpeed = 0; }
                            else if (it.Attributes.CastSpeed == 0 || it.Attributes.CastSpeed == 1) { it.Attributes.CastSpeed = 1; }
                        }
                        if (i_Mod == 22)
                        {
                            it.Attributes.SpellChanneling = 1;
                            if (it.Attributes.CastSpeed == 0) { it.Attributes.CastSpeed = -1; }
                            if (it.Attributes.CastSpeed == 1) { it.Attributes.CastSpeed = 0; }
                        }
                        if (i_Mod == 24) { it.ArmorAttributes.LowerStatReq = modvalue; }
                        if (i_Mod == 42) { it.ArmorAttributes.DurabilityBonus = modvalue; }

                    }
                    else if (i_Item is BaseClothing)
                    {
                        BaseClothing it = i_Item as BaseClothing;
                        it.TimesImbued += 1;

                        if (i_Mod == 3) { it.Attributes.RegenHits = modvalue; }
                        if (i_Mod == 4) { it.Attributes.RegenStam = modvalue; }
                        if (i_Mod == 5) { it.Attributes.RegenMana = modvalue; }

                        if (i_Mod == 9) { it.Attributes.BonusHits = modvalue; }
                        if (i_Mod == 10) { it.Attributes.BonusStam = modvalue; }
                        if (i_Mod == 11) { it.Attributes.BonusMana = modvalue; }
                        if (i_Mod == 17) { it.Attributes.LowerManaCost = modvalue; }
                        if (i_Mod == 18) { it.Attributes.LowerRegCost = modvalue; }
                        if (i_Mod == 19) { it.Attributes.ReflectPhysical = modvalue; }
                        if (i_Mod == 21) { it.Attributes.Luck = modvalue; }
                        if (i_Mod == 23) { it.Attributes.NightSight = 1; }
                    }
                    else if (i_Item is BaseJewel)
                    {
                        BaseJewel it = i_Item as BaseJewel;
                        it.TimesImbued += 1;

                        if (i_Mod == 1) { it.Attributes.DefendChance = modvalue; }
                        if (i_Mod == 2) { it.Attributes.AttackChance = modvalue; }
                        if (i_Mod == 6) { it.Attributes.BonusStr = modvalue; }
                        if (i_Mod == 7) { it.Attributes.BonusDex = modvalue; }
                        if (i_Mod == 8) { it.Attributes.BonusInt = modvalue; }
                        if (i_Mod == 12) { it.Attributes.WeaponDamage = modvalue; }
                        if (i_Mod == 14) { it.Attributes.SpellDamage = modvalue; }
                        if (i_Mod == 15) { it.Attributes.CastRecovery = modvalue; }
                        if (i_Mod == 16) { it.Attributes.CastSpeed = 1; }
                        if (i_Mod == 17) { it.Attributes.LowerManaCost = modvalue; }
                        if (i_Mod == 18) { it.Attributes.LowerRegCost = modvalue; }
                        if (i_Mod == 20) { it.Attributes.EnhancePotions = modvalue; }
                        if (i_Mod == 21) { it.Attributes.Luck = modvalue; }
                        if (i_Mod == 23) { it.Attributes.NightSight = 1; }

                        if (i_Mod == 51) { it.Resistances.Physical = modvalue; }
                        if (i_Mod == 52) { it.Resistances.Fire = modvalue; }
                        if (i_Mod == 53) { it.Resistances.Cold = modvalue; }
                        if (i_Mod == 54) { it.Resistances.Poison = modvalue; }
                        if (i_Mod == 55) { it.Resistances.Energy = modvalue; }

                        if (i_Mod == 151) { it.SkillBonuses.SetSkill(0, SkillName.Fencing); it.SkillBonuses.SetBonus(0, modvalue); }
                        if (i_Mod == 152) { it.SkillBonuses.SetSkill(0, SkillName.Macing); it.SkillBonuses.SetBonus(0, modvalue); }
                        if (i_Mod == 153) { it.SkillBonuses.SetSkill(0, SkillName.Swords); it.SkillBonuses.SetBonus(0, modvalue); }
                        if (i_Mod == 154) { it.SkillBonuses.SetSkill(0, SkillName.Musicianship); it.SkillBonuses.SetBonus(0, modvalue); }
                        if (i_Mod == 155) { it.SkillBonuses.SetSkill(0, SkillName.Magery); it.SkillBonuses.SetBonus(0, modvalue); }

                        if (i_Mod == 156) { it.SkillBonuses.SetSkill(1, SkillName.Wrestling); it.SkillBonuses.SetBonus(1, modvalue); }
                        if (i_Mod == 157) { it.SkillBonuses.SetSkill(1, SkillName.AnimalTaming); it.SkillBonuses.SetBonus(1, modvalue); }
                        if (i_Mod == 158) { it.SkillBonuses.SetSkill(1, SkillName.SpiritSpeak); it.SkillBonuses.SetBonus(1, modvalue); }
                        if (i_Mod == 159) { it.SkillBonuses.SetSkill(1, SkillName.Tactics); it.SkillBonuses.SetBonus(1, modvalue); }
                        if (i_Mod == 160) { it.SkillBonuses.SetSkill(1, SkillName.Provocation); it.SkillBonuses.SetBonus(1, modvalue); }

                        if (i_Mod == 161) { it.SkillBonuses.SetSkill(2, SkillName.Focus); it.SkillBonuses.SetBonus(2, modvalue); }
                        if (i_Mod == 162) { it.SkillBonuses.SetSkill(2, SkillName.Parry); it.SkillBonuses.SetBonus(2, modvalue); }
                        if (i_Mod == 163) { it.SkillBonuses.SetSkill(2, SkillName.Stealth); it.SkillBonuses.SetBonus(2, modvalue); }
                        if (i_Mod == 164) { it.SkillBonuses.SetSkill(2, SkillName.Meditation); it.SkillBonuses.SetBonus(2, modvalue); }
                        if (i_Mod == 165) { it.SkillBonuses.SetSkill(2, SkillName.AnimalLore); it.SkillBonuses.SetBonus(2, modvalue); }
                        if (i_Mod == 166) { it.SkillBonuses.SetSkill(2, SkillName.Discordance); it.SkillBonuses.SetBonus(2, modvalue); }

                        if (i_Mod == 167) { it.SkillBonuses.SetSkill(3, SkillName.Bushido); it.SkillBonuses.SetBonus(3, modvalue); }
                        if (i_Mod == 168) { it.SkillBonuses.SetSkill(3, SkillName.Necromancy); it.SkillBonuses.SetBonus(3, modvalue); }
                        if (i_Mod == 169) { it.SkillBonuses.SetSkill(3, SkillName.Veterinary); it.SkillBonuses.SetBonus(3, modvalue); }
                        if (i_Mod == 170) { it.SkillBonuses.SetSkill(3, SkillName.Stealing); it.SkillBonuses.SetBonus(3, modvalue); }
                        if (i_Mod == 171) { it.SkillBonuses.SetSkill(3, SkillName.EvalInt); it.SkillBonuses.SetBonus(3, modvalue); }
                        if (i_Mod == 172) { it.SkillBonuses.SetSkill(3, SkillName.Anatomy); it.SkillBonuses.SetBonus(3, modvalue); }

                        if (i_Mod == 173) { it.SkillBonuses.SetSkill(4, SkillName.Peacemaking); it.SkillBonuses.SetBonus(4, modvalue); }
                        if (i_Mod == 174) { it.SkillBonuses.SetSkill(4, SkillName.Ninjitsu); it.SkillBonuses.SetBonus(4, modvalue); }
                        if (i_Mod == 175) { it.SkillBonuses.SetSkill(4, SkillName.Chivalry); it.SkillBonuses.SetBonus(4, modvalue); }
                        if (i_Mod == 176) { it.SkillBonuses.SetSkill(4, SkillName.Archery); it.SkillBonuses.SetBonus(4, modvalue); }
                        if (i_Mod == 177) { it.SkillBonuses.SetSkill(4, SkillName.MagicResist); it.SkillBonuses.SetBonus(4, modvalue); }
                        if (i_Mod == 178) { it.SkillBonuses.SetSkill(4, SkillName.Healing); it.SkillBonuses.SetBonus(4, modvalue); }
                    }
                }
                else
                {
                    from.SendLocalizedMessage(1079774); // Fail
                    from.PlaySound(0x1E5);
                }
            }
        }

        public static void GetMaterials( int Mod )
        {
            // ------------------------------ Sort Mod Data -------------------------------------------------------------
            if (Mod == 1) { s_Mod = "Defense Chance Increase"; s_Weight = 130; m_Gem = "Tourmaline"; m_A = "Relic Fragment"; m_B = "Essence of Singularity"; m_Imax = 15; i_Inc = 1; m_Desc = 1111947; }
            if (Mod == 2) { s_Mod = "Hit Chance Increase"; s_Weight = 130; m_Gem = "Amber"; m_A = "Relic Fragment"; m_B = "Essence of Precision"; m_Imax = 15; i_Inc = 1; m_Desc = 1111958; }
            if (Mod == 3) { s_Mod = "Regen Hitpoints"; s_Weight = 100; m_Gem = "Tourmaline"; m_A = "Enchanted Essence"; m_B = "Seed of Renewal"; m_Imax = 2; i_Inc = 1; m_Desc = 1111994; }
            if (Mod == 4) { s_Mod = "Regen Stamina"; s_Weight = 100; m_Gem = "Diamond"; m_A = "Enchanted Essence"; m_B = "Seed of Renewal"; m_Imax = 3; i_Inc = 1; m_Desc = 1112043; }
            if (Mod == 5) { s_Mod = "Regen Mana"; s_Weight = 100; m_Gem = "Sapphire"; m_A = "Enchanted Essence"; m_B = "Seed of Renewal"; m_Imax = 2; i_Inc = 1; m_Desc = 1112003; }
            if (Mod == 6) { s_Mod = "Strength Bonus"; s_Weight = 110; m_Gem = "Diamond"; m_A = "Enchanted Essence"; m_B = "Fire Ruby"; m_Imax = 8; i_Inc = 1; m_Desc = 1112044; }
            if (Mod == 7) { s_Mod = "Dexterity Bonus"; s_Weight = 110; m_Gem = "Ruby"; m_A = "Enchanted Essence"; m_B = "Blue Diamond"; m_Imax = 8; i_Inc = 1; m_Desc = 1111948; }
            if (Mod == 8) { s_Mod = "Intelligence Bonus"; s_Weight = 110; m_Gem = "Tourmaline"; m_A = "Enchanted Essence"; m_B = "Turquoise"; m_Imax = 8; i_Inc = 1; m_Desc = 1111995; }
            if (Mod == 9) { s_Mod = "Hitpoint Increase"; s_Weight = 110; m_Gem = "Ruby"; m_A = "Enchanted Essence"; m_B = "Luminescent Fungi"; m_Imax = 5; i_Inc = 1; m_Desc = 1111993; }
            if (Mod == 10) { s_Mod = "Stamina Increase"; s_Weight = 100; m_Gem = "Diamond"; m_A = "Enchanted Essence"; m_B = "Luminescent Fungi"; m_Imax = 8; i_Inc = 1; m_Desc = 1112042; }
            if (Mod == 11) { s_Mod = "Mana Increase"; s_Weight = 110; m_Gem = "Sapphire"; m_A = "Enchanted Essence"; m_B = "Luminescent Fungi"; m_Imax = 8; i_Inc = 1; m_Desc = 1112002; }
            if (Mod == 12) { s_Mod = "Damage Increase"; s_Weight = 100; m_Gem = "Citrine"; m_A = "Enchanted Essence"; m_B = "Crystal Shards"; m_Imax = 50; i_Inc = 1; m_Desc = 1112005; }
            if (Mod == 13) { s_Mod = "Swing Speed Increase"; s_Weight = 110; m_Gem = "Tourmaline"; m_A = "Relic Fragment"; m_B = "Essence of Control"; m_Imax = 40; i_Inc = 5; m_Desc = 1112045; }
            if (Mod == 14) { s_Mod = "Spell Damage Increase"; s_Weight = 100; m_Gem = "Emerald"; m_A = "Enchanted Essence"; m_B = "Crystal Shards"; m_Imax = 12; i_Inc = 1; m_Desc = 1112041; }
            if (Mod == 15) { s_Mod = "Faster Cast Recovery"; s_Weight = 120; m_Gem = "Amethyst"; m_A = "Relic Fragment"; m_B = "Essence of Diligence"; m_Imax = 3; i_Inc = 1; m_Desc = 1111952; }
            if (Mod == 16) { s_Mod = "Faster Casting"; s_Weight = 140; m_Gem = "Ruby"; m_A = "Relic Fragment"; m_B = "Essence of Achievement"; m_Imax = 1; i_Inc = 0; m_Desc = 1111951; }
            if (Mod == 17) { s_Mod = "Lower Mana Cost"; s_Weight = 110; m_Gem = "Tourmaline"; m_A = "Relic Fragment"; m_B = "Essence of Order"; m_Imax = 8; i_Inc = 1; m_Desc = 1111996; }
            if (Mod == 18) { s_Mod = "Lower Reagent Cost"; s_Weight = 100; m_Gem = "Amber"; m_A = "Magical Residue"; m_B = "Faery Dust"; m_Imax = 20; i_Inc = 1; m_Desc = 1111997; }
            if (Mod == 19) { s_Mod = "Reflect Physical Damage"; s_Weight = 100; m_Gem = "Citrine"; m_A = "Magical Residue"; m_B = "Reflective Wolf Eye"; m_Imax = 15; i_Inc = 1; m_Desc = 1112006; }
            if (Mod == 20) { s_Mod = "Enhance Potions"; s_Weight = 100; m_Gem = "Citrine"; m_A = "Enchanted Essence"; m_B = "Crushed Glass"; m_Imax = 30; i_Inc = 5; m_Desc = 1111950; }
            if (Mod == 21) { s_Mod = "Luck"; s_Weight = 100; m_Gem = "Citrine"; m_A = "Magical Residue"; m_B = "Chaga Mushroom"; m_Imax = 100; i_Inc = 1; m_Desc = 1111999; }
            if (Mod == 22) { s_Mod = "Spell Channeling"; s_Weight = 100; m_Gem = "Diamond"; m_A = "Magical Residue"; m_B = "Silver Snake Skin"; m_Imax = 1; i_Inc = 0; m_Desc = 1112040; }
            if (Mod == 23) { s_Mod = "Night Sight"; s_Weight = 50; m_Gem = "Tourmaline"; m_A = "Magical Residue"; m_B = "Bottle of Ichor"; m_Imax = 1; i_Inc = 0; m_Desc = 1112004; }

            if (Mod == 24) { s_Mod = "Lower Requirements"; s_Weight = 100; m_Gem = "Amethyst"; m_A = "Enchanted Essence"; m_B = "Elven Fletching"; m_Imax = 100; i_Inc = 10; m_Desc = 1111998; }
            if (Mod == 25) { s_Mod = "Hit Life Leech"; s_Weight = 110; m_Gem = "Ruby"; m_A = "Magical Residue"; m_B = "Void Orb"; m_Imax = 50; i_Inc = 2; m_Desc = 1111964; }
            if (Mod == 26) { s_Mod = "Hit Stamina Leech"; s_Weight = 110; m_Gem = "Diamond"; m_A = "Magical Residue"; m_B = "Void Orb"; m_Imax = 50; i_Inc = 2; m_Desc = 1111992; }
            if (Mod == 27) { s_Mod = "Hit Mana Leech"; s_Weight = 100; m_Gem = "Sapphire"; m_A = "Magical Residue"; m_B = "Void Orb"; m_Imax = 50; i_Inc = 2; m_Desc = 1111967; }
            if (Mod == 28) { s_Mod = "Hit Lower Attack"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Enchanted Essence"; m_B = "Parasitic Plant"; m_Imax = 50; i_Inc = 2; m_Desc = 1111965; }
            if (Mod == 29) { s_Mod = "Hit Lower Defense"; s_Weight = 130; m_Gem = "Tourmaline"; m_A = "Enchanted Essence"; m_B = "Parasitic Plant"; m_Imax = 50; i_Inc = 2; m_Desc = 1111966; }
            if (Mod == 30) { s_Mod = "Hit Physical Area"; s_Weight = 100; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "Raptor Teeth"; m_Imax = 50; i_Inc = 2; m_Desc = 1111956; }
            if (Mod == 31) { s_Mod = "Hit Fire Area"; s_Weight = 100; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "Raptor Teeth"; m_Imax = 50; i_Inc = 2; m_Desc = 1111955; }
            if (Mod == 32) { s_Mod = "Hit Cold Area"; s_Weight = 100; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "Raptor Teeth"; m_Imax = 50; i_Inc = 2; m_Desc = 1111953; }
            if (Mod == 33) { s_Mod = "Hit Poison Area"; s_Weight = 100; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "Raptor Teeth"; m_Imax = 50; i_Inc = 2; m_Desc = 1111957; }
            if (Mod == 34) { s_Mod = "Hit Energy Area"; s_Weight = 100; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "Raptor Teeth"; m_Imax = 50; i_Inc = 2; m_Desc = 1111954; }
            if (Mod == 35) { s_Mod = "Hit Magic Arrow"; s_Weight = 120; m_Gem = "Amber"; m_A = "Relic Fragment"; m_B = "Essence of Feeling"; m_Imax = 50; i_Inc = 2; m_Desc = 1111963; }
            if (Mod == 36) { s_Mod = "Hit Harm"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Enchanted Essence"; m_B = "Parasitic Plant"; m_Imax = 50; i_Inc = 2; m_Desc = 1111961; }
            if (Mod == 37) { s_Mod = "Hit Fireball"; s_Weight = 140; m_Gem = "Ruby"; m_A = "Enchanted Essence"; m_B = "Fire Ruby"; m_Imax = 50; i_Inc = 2; m_Desc = 1111960; }
            if (Mod == 38) { s_Mod = "Hit Lightning"; s_Weight = 140; m_Gem = "Amethyst"; m_A = "Relic Fragment"; m_B = "Essence of Passion"; m_Imax = 50; i_Inc = 2; m_Desc = 1111962; }
            if (Mod == 39) { s_Mod = "Hit Dispel"; s_Weight = 100; m_Gem = "Amber"; m_A = "Magical Residue"; m_B = "Slith Tongue"; m_Imax = 50; i_Inc = 2; m_Desc = 1111959; }
            if (Mod == 40) { s_Mod = "Use Best Weapon Skill"; s_Weight = 150; m_Gem = "Amber"; m_A = "Enchanted Essence"; m_B = "Delicate Scales"; m_Imax = 1; i_Inc = 0; m_Desc = 1111946; }
            if (Mod == 41) { s_Mod = "Mage Weapon"; s_Weight = 100; m_Gem = "Emerald"; m_A = "Enchanted Essence"; m_B = "Arcanic Rune Stone"; m_Imax = 10; i_Inc = 1; m_Desc = 1112001; }
            if (Mod == 42) { s_Mod = "Durability"; s_Weight = 100; m_Gem = "Diamond"; m_A = "Enchanted Essence"; m_B = "Powdered Iron"; m_Imax = 100; i_Inc = 10; m_Desc = 1111949; }

            if (Mod == 49) { s_Mod = "Mage Armor"; s_Weight = 100; m_Gem = "Diamond"; m_A = "Enchanted Essence"; m_B = "Abyssal Cloth"; m_Imax = 1; i_Inc = 0; m_Desc = 1112000; }

            if (Mod == 51) { s_Mod = "Physical Resist Bonus"; s_Weight = 100; m_Gem = "Diamond"; m_A = "Magical Residue"; m_B = "Boura Pelt"; m_Imax = 15; i_Inc = 1; m_Desc = 1112010; }
            if (Mod == 52) { s_Mod = "Fire Resist Bonus"; s_Weight = 100; m_Gem = "Ruby"; m_A = "Magical Residue"; m_B = "Boura Pelt"; m_Imax = 15; i_Inc = 1; m_Desc = 1112009; }
            if (Mod == 53) { s_Mod = "Cold Resist Bonus"; s_Weight = 100; m_Gem = "Sapphire"; m_A = "Magical Residue"; m_B = "Boura Pelt"; m_Imax = 15; i_Inc = 1; m_Desc = 1112007; }
            if (Mod == 54) { s_Mod = "Poison Resist Bonus"; s_Weight = 100; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "Boura Pelt"; m_Imax = 15; i_Inc = 1; m_Desc = 1112011; }
            if (Mod == 55) { s_Mod = "Energy Resist Bonus"; s_Weight = 100; m_Gem = "Amethyst"; m_A = "Magical Residue"; m_B = "Boura Pelt"; m_Imax = 15; i_Inc = 1; m_Desc = 1112008; }

            if (Mod == 60) { s_Mod = "Velocity"; s_Weight = 150; m_Gem = "Tourmaline"; m_A = "Relic Fragment"; m_B = "Essence of Direction"; m_Imax = 50; i_Inc = 2; m_Desc = 1112048; }
            if (Mod == 61) { s_Mod = "Balanced"; s_Weight = 100; m_Gem = "Amber"; m_A = "Relic Fragment"; m_B = "Essence of Balance"; m_Imax = 1; i_Inc = 0; m_Desc = 1112047; }

            if (Mod == 101) { s_Mod = "Orc Slaying"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111977; }
            if (Mod == 102) { s_Mod = "Troll Slaughter"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111990; }
            if (Mod == 103) { s_Mod = "Ogre Trashing"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111975; }
            if (Mod == 104) { s_Mod = "Dragon Slaying"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111970; }
            if (Mod == 105) { s_Mod = "Terathan"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111989; }
            if (Mod == 106) { s_Mod = "Snakes Bane"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111980; }
            if (Mod == 107) { s_Mod = "Lizardman Slaughter"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111973; }
            //if (Mod == 108) { s_Mod = "Daemon Dismissal"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1112001; }
            if (Mod == 109) { s_Mod = "Gargoyles Foe"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111973; }
            //if (Mod == 110) { s_Mod = "Balron Damnation"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1112001; }
            if (Mod == 111) { s_Mod = "Ophidian"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111976; }
            if (Mod == 112) { s_Mod = "Spiders Death"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111982; }
            if (Mod == 113) { s_Mod = "Scorpions Bane"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111979; }
            if (Mod == 114) { s_Mod = "Flame Dousing"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111972; }
            if (Mod == 115) { s_Mod = "Water Dissipation"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111991; }
            if (Mod == 116) { s_Mod = "Vacuum"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111968; }
            if (Mod == 117) { s_Mod = "Elemental Health"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111978; }
            if (Mod == 118) { s_Mod = "Earth Shatter"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111971; }
            if (Mod == 119) { s_Mod = "Blood Drinking"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111969; }
            if (Mod == 120) { s_Mod = "Summer Wind"; s_Weight = 110; m_Gem = "Emerald"; m_A = "Magical Residue"; m_B = "White Pearl"; m_Imax = 1; i_Inc = 0; m_Desc = 1111981; }

            if (Mod == 121) { s_Mod = "Silver"; s_Weight = 130; m_Gem = "Ruby"; m_A = "Relic Fragment"; m_B = "Undying Flesh"; m_Imax = 1; i_Inc = 0; m_Desc = 1111988; }
            if (Mod == 122) { s_Mod = "Repond"; s_Weight = 130; m_Gem = "Ruby"; m_A = "Relic Fragment"; m_B = "Goblin Blood"; m_Imax = 1; i_Inc = 0; m_Desc = 1111986; }
            if (Mod == 123) { s_Mod = "Reptilian Death"; s_Weight = 130; m_Gem = "Ruby"; m_A = "Relic Fragment"; m_B = "Lava Serpent Crust"; m_Imax = 1; i_Inc = 0; m_Desc = 1111987; }
            if (Mod == 124) { s_Mod = "Exorcism"; s_Weight = 130; m_Gem = "Ruby"; m_A = "Relic Fragment"; m_B = "Daemon Claw"; m_Imax = 1; i_Inc = 0; m_Desc = 1111984; }
            if (Mod == 125) { s_Mod = "Arachnid Doom"; s_Weight = 130; m_Gem = "Ruby"; m_A = "Relic Fragment"; m_B = "Spider Carapace"; m_Imax = 1; i_Inc = 0; m_Desc = 1111983; }
            if (Mod == 126) { s_Mod = "Elemental Ban"; s_Weight = 130; m_Gem = "Ruby"; m_A = "Relic Fragment"; m_B = "Vial of Vitriol"; m_Imax = 1; i_Inc = 0; m_Desc = 1111985; }

            if (Mod == 151) { s_Mod = "Fencing"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112012; }
            if (Mod == 152) { s_Mod = "Mace Fighting"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112013; }
            if (Mod == 153) { s_Mod = "Swordsmanship"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112016; }
            if (Mod == 154) { s_Mod = "Musicianship"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112015; }
            if (Mod == 155) { s_Mod = "Magery"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112014; }

            if (Mod == 156) { s_Mod = "Wrestling"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112021; }
            if (Mod == 157) { s_Mod = "Animal Taming"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112017; }
            if (Mod == 158) { s_Mod = "Spirit Speak"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112019; }
            if (Mod == 159) { s_Mod = "Tactics"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112020; }
            if (Mod == 160) { s_Mod = "Provocation"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 11120018; }

            if (Mod == 161) { s_Mod = "Focus"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112024; }
            if (Mod == 162) { s_Mod = "Parrying"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112026; }
            if (Mod == 163) { s_Mod = "Stealth"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112027; }
            if (Mod == 164) { s_Mod = "Meditation"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112025; }
            if (Mod == 165) { s_Mod = "Animal Lore"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112022; }
            if (Mod == 166) { s_Mod = "Discordance"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112023; }

            if (Mod == 167) { s_Mod = "Bushido"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112029; }
            if (Mod == 168) { s_Mod = "Necromancy"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112031; }
            if (Mod == 169) { s_Mod = "Veterinary"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112033; }
            if (Mod == 170) { s_Mod = "Stealing"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112032; }
            if (Mod == 171) { s_Mod = "Evaluating Intelligence"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112030; }
            if (Mod == 172) { s_Mod = "Anatomy"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112028; }

            if (Mod == 173) { s_Mod = "Peacemaking"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112038; }
            if (Mod == 174) { s_Mod = "Ninjitsu"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112037; }
            if (Mod == 175) { s_Mod = "Chivalry"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112035; }
            if (Mod == 176) { s_Mod = "Archery"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112034; }
            if (Mod == 177) { s_Mod = "Resisting Spells"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112039; }
            if (Mod == 178) { s_Mod = "Healing"; s_Weight = 140; m_Gem = "Star Sapphire"; m_A = "Enchanted Essence"; m_B = "Crystalline Blackrock"; m_Imax = 15; i_Inc = 1; m_Desc = 1112036; }

            return;
        }

        public int GetMGemNo(int Max, int Inc, double Mv)
        {
            double mno = 0;
            if (Max == 100) { mno = Mv / 10; }
            if (Max == 50) { mno = 1 * (Mv / 5); }
            if (Max == 40 && Inc == 5) { mno = 1 * (Mv / 4); }
            if (Max == 30 && Inc == 5) { mno = 1 * (Mv / 3); }
            if (Max == 20 && Inc == 1) { mno = 1 * (Mv / 2); }
            if (Max == 15 && Inc == 1) { mno = 1 * (Mv / 1.5); }
            if (Max == 12 && Inc == 1) { mno = Mv; }
            if (Max == 10 && Inc == 1) { mno = Mv; }
            if (Max == 8 && Inc == 1) { mno = Mv; }
            if (Max == 5 && Inc == 1) { mno = Mv * 2; }
            if (Max == 3 && Inc == 1) { mno = Mv * 3; }
            if (Max == 2 && Inc == 1) { mno = Mv * 5; }
            if (i_Mod == 16 || i_Mod == 22 || i_Mod == 23 || i_Mod == 40 || i_Mod == 41 || i_Mod == 49) { mno = 10; }
            if (i_Mod >= 100 && i_Mod <= 126) { mno = 10; }

            if (mno < 1) { mno = 1; }
            mno = Math.Round(mno);
            int oInt = Convert.ToInt32(mno);
            return oInt;
        }
        public int GetMANo(int Max, int Inc, double Mv)
        {
            double mno = 0;
            if (Max == 100) { mno = 1 * (Mv / 20); }
            if (Max == 50) { mno = 1 * (Mv / 10); }
            if (Max == 40 && Inc == 5) { mno = 1 * (Mv / 8); }
            if (Max == 30 && Inc == 5) { mno = 1 * (Mv / 6); }
            if (Max == 20 && Inc == 1) { mno = 1 * (Mv / 4); }
            if (Max == 15 && Inc == 1) { mno = 1 * (Mv / 3); }
            if (Max == 12 && Inc == 1) { mno = 1 * (Mv / 2.4); }
            if (Max == 10 && Inc == 1) { mno = 1 * (Mv / 2); }
            if (Max == 8 && Inc == 1) { mno = Mv * 0.625; }
            if (Max == 5 && Inc == 1) { mno = Mv; }
            if (Max == 3 && Inc == 1) { mno = Mv * 1.6; }
            if (Max == 2 && Inc == 1) { mno = Mv * 2.5; }
            if (i_Mod == 16 || i_Mod == 22 || i_Mod == 23 || i_Mod == 40 || i_Mod == 41 || i_Mod == 49) { mno = 5; }
            if (i_Mod >= 100 && i_Mod <= 126) { mno = 5; }
            if (mno < 1) { mno = 1; }
            mno = Math.Round(mno);
            int oInt = Convert.ToInt32(mno);
            return oInt;
        }
        public int GetMBNo(int Max, int Inc, double Mv)
        {
            double mno = 0;
            if (Max == 100) { mno = (Mv - 90); }
            if (Max == 50) { mno = (Mv - 45) * 2; }
            if (Max == 40 && Inc == 5)
            {
                if (Mv == 30) { mno = 3; }
                if (Mv == 35) { mno = 6; }
                if (Mv == 40) { mno = 10; }
            }
            if (Max == 30 && Inc == 5)
            {
                if (Mv == 30) { mno = 4; }
            }
            if (Max == 20 && Inc == 1) { mno = (Mv - 18) * 5; }
            if (Max == 15 && Inc == 1) { mno = (Mv - 12) * 3.3; }
            if (Max == 12 && Inc == 1) { mno = (Mv - 10) * 5; }
            if (Max == 10 && Inc == 1) { mno = (Mv - 8) * 5; }
            if (Max == 8 && Inc == 1) { mno = (Mv - 7) * 10; }
            if (Max == 5 && Inc == 1) { mno = (Mv - 4) * 10; }
            if (Max == 3 && Inc == 1) { mno = (Mv - 2) * 10; }
            if (Max == 2 && Inc == 1) { mno = (Mv - 1) * 10; }
            if (i_Mod == 16 || i_Mod == 22 || i_Mod == 23 || i_Mod == 40 || i_Mod == 41 || i_Mod == 49) { mno = 10; }
            if (i_Mod >= 100 && i_Mod <= 126) { mno = 10; }

            if (mno < 0) { mno = 0; }
            mno = Math.Round(mno);
            int oInt = Convert.ToInt32(mno);
            return oInt;
        }
    }                
}