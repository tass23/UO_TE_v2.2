// 15AUG2007 written by RavonTUS
//
//   /\\           |\\  ||
//  /__\\  |\\ ||  | \\ ||  /\\  \ //
// /    \\ | \\||  |  \\||  \//  / \\ 
// Play at An Nox, the cure for the UO addiction
// http://annox.no-ip.com  // RavonTUS@Yahoo.com

using System;
using Server.Items;

namespace Server.Engines.Craft
{
    public class DefPillowCraft : CraftSystem
    {
        public override SkillName MainSkill
        {
            get { return SkillName.Tailoring; }
        }

        public override int GumpTitleNumber
        {
            get { return 0; } // <CENTER>TAILORING MENU</CENTER>
        }

        public override string GumpTitleString
        {
            get { return "<basefont color=#FFFFFF><CENTER>Pillow Crafting Menu</CENTER></basefont>"; }
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefPillowCraft();

                return m_CraftSystem;
            }
        }

        public override CraftECA ECA { get { return CraftECA.ChanceMinusSixtyToFourtyFive; } }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.5; // 50%
        }

        private DefPillowCraft()
            : base(1, 1, 1.25)// base( 1, 1, 4.5 )
        {
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
            from.PlaySound(0x248);
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (failed)
            {
                if (lostMaterial)
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                else
                    return 1044157; // You failed to create the item, but no materials were lost.
            }
            else
            {
                if (quality == 0)
                    return 502785; // You were barely able to make this item.  It's quality is below average.
                else if (makersMark && quality == 2)
                    return 1044156; // You create an exceptional quality item and affix your maker's mark.
                else if (quality == 2)
                    return 1044155; // You create an exceptional quality item.
                else
                    return 1044154; // You create the item.
            }
        }

        public override void InitCraftList()
        {
            int index = -1;

            //#region Pillow Crafting
            
			index = AddCraft(typeof(TasslePillow), "Pillows", "Tassle Pillow", 6.2, 31.2, typeof(Cloth), 1044286, 6, 1044287);
            AddRes(index, typeof(Cotton), "Cotton", 6, "You don't have any Cotton.");

            index = AddCraft(typeof(LargePillow), "Pillows", "Large Pillow", 6.2, 31.2, typeof(Cloth), 1044286, 12, 1044287);
            AddRes(index, typeof(Cotton), "Cotton", 12, "You don't have any Cotton.");

            index = AddCraft(typeof(RoundPillow), "Pillows", "Round Pillow", 6.2, 31.2, typeof(Cloth), 1044286, 10, 1044287);
            AddRes(index, typeof(Cotton), "Cotton", 10, "You don't have any Cotton.");

            index = AddCraft(typeof(MediumPillow), "Pillows", "Medium Pillow", 6.2, 31.2, typeof(Cloth), 1044286, 4, 1044287);
            AddRes(index, typeof(Cotton), "Cotton", 4, "You don't have any Cotton.");
			
            index = AddCraft(typeof(TaggedPillow), "Special Pillows", "Tagged Pillow", 6.2, 31.2, typeof(Cloth), 1044286, 10, 1044287);
            AddRes(index, typeof(Wool), "Wool", 5, "You don't have any wool.");
            //AddRes(index, typeof(Cloth), 1044286, 5, 1044287);

            index = AddCraft(typeof(ThrowingPillow), "Special Pillows", "Throwing Pillow", 6.2, 31.2, typeof(Cloth), 1044286, 10, 1044287);
            AddRes(index, typeof(Feather), "Feather", 20, "You don't have any feathers.");
			
            index = AddCraft(typeof(SmallPillow), "Pillows", "Small Pillow", 6.2, 31.2, typeof(Cloth), 1044286, 4, 1044287);
            AddRes(index, typeof(Cotton), "Cotton", 4, "You don't have any Cotton.");
			
            index = AddCraft(typeof(ShagPillow), "Pillows", "Shag Pillow", 6.2, 31.2, typeof(Cloth), 1044286, 4, 1044287);
            AddRes(index, typeof(Cotton), "Cotton", 4, "You don't have any Cotton.");

            //Reference
            //index = AddCraft(typeof(ArmysPaeonScroll), "Bard Scrolls", "Armys Paeon Scroll", 85.0, 100.0, typeof(BlankScroll), Quanity needed, "You don't have a scroll.");
            //AddRes(index, typeof(GraveDust), 1, "You don't have any grave dust.");
            //AddRes(index, typeof(DaemonBlood), 1, "You don't have any daemon blood.");

            //#endregion

            MarkOption = true;
            Repair = Core.AOS;
            CanEnhance = Core.AOS;
        }
    }
}