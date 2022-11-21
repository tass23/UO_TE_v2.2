/* Created by Hammerhand */

using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
    public class DefFireRockCraft : CraftSystem
    {
        public override SkillName MainSkill
        {
            get { return SkillName.Blacksmith; }
        }

        public override string GumpTitleString
        {
            get { return "<basefont color=#FFFFFF><CENTER>FIREROCK CRAFTING MENU</CENTER></basefont>"; }
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefFireRockCraft();

                return m_CraftSystem;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.5; // 50%
        }

        private DefFireRockCraft()
            : base(1, 1, 1.25)// base( 1, 1, 3.0 )
        {
        }

        private static Type typeofAnvil = typeof(AnvilAttribute);
        private static Type typeofForge = typeof(ForgeAttribute);

        public static void CheckAnvilAndForge(Mobile from, int range, out bool anvil, out bool forge)
        {
            anvil = false;
            forge = false;

            Map map = from.Map;

            if (map == null)
                return;

            IPooledEnumerable eable = map.GetItemsInRange(from.Location, range);

            foreach (Item item in eable)
            {
                Type type = item.GetType();

                bool isAnvil = (type.IsDefined(typeofAnvil, false) || item.ItemID == 4015 || item.ItemID == 4016 || item.ItemID == 0x2DD5 || item.ItemID == 0x2DD6);
                bool isForge = (type.IsDefined(typeofForge, false) || item.ItemID == 4017 || (item.ItemID >= 6522 && item.ItemID <= 6569) || item.ItemID == 0x2DD8);

                if (isAnvil || isForge)
                {
                    if ((from.Z + 16) < item.Z || (item.Z + 16) < from.Z || !from.InLOS(item))
                        continue;

                    anvil = anvil || isAnvil;
                    forge = forge || isForge;

                    if (anvil && forge)
                        break;
                }
            }

            eable.Free();

            for (int x = -range; (!anvil || !forge) && x <= range; ++x)
            {
                for (int y = -range; (!anvil || !forge) && y <= range; ++y)
                {
                    StaticTile[] tiles = map.Tiles.GetStaticTiles(from.X + x, from.Y + y, true);

                    for (int i = 0; (!anvil || !forge) && i < tiles.Length; ++i)
                    {
                        int id = tiles[i].ID & 0x3FFF;

                        bool isAnvil = (id == 4015 || id == 4016 || id == 0x2DD5 || id == 0x2DD6);
                        bool isForge = (id == 4017 || (id >= 6522 && id <= 6569) || id == 0x2DD8);

                        if (isAnvil || isForge)
                        {
                            if ((from.Z + 16) < tiles[i].Z || (tiles[i].Z + 16) < from.Z || !from.InLOS(new Point3D(from.X + x, from.Y + y, tiles[i].Z + (tiles[i].Height / 2) + 1)))
                                continue;

                            anvil = anvil || isAnvil;
                            forge = forge || isForge;
                        }
                    }
                }
            }
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckTool(tool, from))
                return 1048146; // If you have a tool equipped, you must use that tool.
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            bool anvil, forge;
            CheckAnvilAndForge(from, 2, out anvil, out forge);

            if (anvil && forge)
                return 0;

            return 1044267; // You must be near an anvil and a forge to smith items.
        }

        public override void PlayCraftEffect(Mobile from)
        {
            // no animation, instant sound
            //if ( from.Body.Type == BodyType.Human && !from.Mounted )
            //	from.Animate( 9, 5, 1, true, false, 0 );
            //new InternalTimer( from ).Start();

            from.PlaySound(0x2A);
        }

        // Delay to synchronize the sound with the hit on the anvil
        private class InternalTimer : Timer
        {
            private Mobile m_From;

            public InternalTimer(Mobile from)
                : base(TimeSpan.FromSeconds(0.7))
            {
                m_From = from;
            }

            protected override void OnTick()
            {
                m_From.PlaySound(0x2A);
            }
        }


        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool 

            if (failed)
            {
                if (lostMaterial)
                    from.SendMessage("You swing too hard and molten material flies in every direction!");
                from.Damage(Utility.Random(10, 35));
                return 0; // if not 0 work use this and double message return 1044043;
            }
                else 
            {                                                                                         
                if  (quality == 0)
                    return 502785; // You were barely able to make this item.  It's quality is below average.
                if  (makersMark && quality == 2)
                    from.SendMessage("You carefully shape the molten material into a new item.");
                    return 0; // if not 0 work use this and double message return 1044156;
                if  (quality == 2)
                    from.SendMessage("You carefully shape the molten material.");
                    return 0; // if not 0 work use this and double message return 1044155;              
            }
        }
        public override void InitCraftList()
        {
            int index = -1;
                #region Armor

            index = AddCraft(typeof(FemaleFireRockChest), "Armor", "FemaleFireRockChest", 96.0, 115.0, typeof(LargeFireRock), "LargeFireRock", 2, "You dont have enough FireRock");
            AddRes(index, typeof(SmallFireRock), "SmallFireRock", 5, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 4, 1044240 );
            index = AddCraft(typeof(FireRockChest), "Armor", "FireRockChest", 96.0, 115.0, typeof(LargeFireRock), "LargeFireRock", 3, "You dont have enough FireRock");
            AddRes(index, typeof(SmallFireRock), "SmallFireRock", 5, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 4, 1044240 );
            index = AddCraft(typeof(FireRockArms), "Armor", "FireRockArms", 85.0, 110.0, typeof(LargeFireRock), "LargeFireRock", 2, "You dont have enough FireRock");
            AddRes(index, typeof(SmallFireRock), "SmallFireRock", 3, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 2, 1044240 );
            index = AddCraft(typeof(FireRockLegs), "Armor", "FireRockLegs", 85.0, 110.0, typeof(LargeFireRock), "LargeFireRock", 4, "You dont have enough FireRock");
            AddRes(index, typeof(SmallFireRock), "SmallFireRock", 2, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 3, 1044240 );
            index = AddCraft(typeof(FireRockGloves), "Armor", "FireRockGloves", 80.0, 105.0, typeof(LargeFireRock), "LargeFireRock", 1, "You dont have enough FireRock");
            AddRes(index, typeof(SmallFireRock), "SmallFireRock", 2, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 1, 1044240 );
            index = AddCraft(typeof(FireRockGorget), "Armor", "FireRockGorget", 80.0, 105.0, typeof(LargeFireRock), "LargeFireRock", 1, "You dont have enough FireRock");
            AddRes(index, typeof(SmallFireRock), "SmallFireRock", 2, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 1, 1044240 );
            AddCraft(typeof(FireRockHelm), "Armor", "FireRockHelm", 75.0, 95.0, typeof(LargeFireRock), "LargeFireRock", 2, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 1, 1044240 );
            index = AddCraft(typeof(FlameShield), "Armor", "FlameShield", 75.0, 95.0, typeof(LargeFireRock), "LargeFireRock", 5, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 4, 1044240 );
            AddRes(index, typeof(SmallFireRock), "SmallFireRock", 3, "You dont have enough FireRock");
            index = AddCraft(typeof(CrystalineFireChest), "Armor", "CrystalineFireChest", 97.0, 115.0, typeof(CrystalineFire), "CrystalineFire", 10, "You dont have enough CrystalineFire");
			AddRes( index, typeof( FireRuby ), 1032695, 4, 1044240 );
            AddRes(index, typeof(SmallFireRock), "SmallFireRock", 5, "You dont have enough FireRock");
            index = AddCraft(typeof(CrystalineFireArms), "Armor", "CrystalineFireArms", 86.0, 110.0, typeof(CrystalineFire), "CrystalineFire", 5, "You dont have enough CrystalineFire");
			AddRes( index, typeof( FireRuby ), 1032695, 2, 1044240 );
            AddRes(index, typeof(SmallFireRock), "SmallFireRock", 3, "You dont have enough FireRock");
            index = AddCraft(typeof(CrystalineFireLegs), "Armor", "CrystalineFireLegs", 86.0, 110.0, typeof(CrystalineFire), "CrystalineFire", 6, "You dont have enough CrystalineFire");
			AddRes( index, typeof( FireRuby ), 1032695, 3, 1044240 );
            AddRes(index, typeof(SmallFireRock), "SmallFireRock", 4, "You dont have enough FireRock");
            index = AddCraft(typeof(CrystalineFireGloves), "Armor", "CrystalineFireGloves", 82.0, 105.0, typeof(CrystalineFire), "CrystalineFire", 4, "You dont have enough CrystalineFire");
			AddRes( index, typeof( FireRuby ), 1032695, 1, 1044240 );
            AddRes(index, typeof(SmallFireRock), "SmallFireRock", 2, "You dont have enough FireRock");
            AddCraft(typeof(CrystalineFireHelm), "Armor", "CrystalineFireHelm", 80.0, 95.0, typeof(CrystalineFire), "CrystalineFire", 3, "You dont have enough CrystalineFire");
			AddRes( index, typeof( FireRuby ), 1032695, 1, 1044240 );

               #endregion

                #region Weapon

            AddCraft(typeof(Flame), "Weapon", "Flame", 75.0, 85.0, typeof(SmallFireRock), "SmallFireRock", 4, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 3, 1044240 );
            AddCraft(typeof(FlowingFire), "Weapon", "FlowingFire", 75.0, 85.0, typeof(SmallFireRock), "SmallFireRock", 4, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 3, 1044240 );
            AddCraft(typeof(CrashAndBurn), "Weapon", "CrashAndBurn", 85.0, 95.0, typeof(SmallFireRock), "SmallFireRock", 3, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 3, 1044240 );
            AddCraft(typeof(Blaze), "Weapon", "Blaze", 85.0, 95.0, typeof(SmallFireRock), "SmallFireRock", 6, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 4, 1044240 );
            AddCraft(typeof(DoubleFlame), "Weapon", "DoubleFlame", 75.0, 85.0, typeof(SmallFireRock), "SmallFireRock", 4, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 3, 1044240 );
            AddCraft(typeof(Inferno), "Weapon", "Inferno", 95.0, 105.0, typeof(SmallFireRock), "SmallFireRock", 7, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 5, 1044240 );
            AddCraft(typeof(FlameThrower), "Weapon", "FlameThrower", 75.0, 85.0, typeof(SmallFireRock), "SmallFireRock", 4, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 3, 1044240 );
            AddCraft(typeof(FireStorm), "Weapon", "FireStorm", 85.0, 95.0, typeof(SmallFireRock), "SmallFireRock", 6, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 4, 1044240 );

            #endregion

                #region Special
            index = AddCraft(typeof(RadiantFire), "Special", "RadiantFire", 85.0, 95.0, typeof(LargeFireRock), "LargeFireRock", 3, "You dont have enough FireRock");
            AddRes(index, typeof(CrystalineFire), "CrystalineFire", 1, "You dont have enough FireCrystal");
			AddRes( index, typeof( FireRuby ), 1032695, 4, 1044240 );
          //  SetUseAllRes(index, true);

            index = AddCraft(typeof(LuminousFlames), "Special", "LuminousFlames", 85.0, 95.0, typeof(LargeFireRock), "LargeFireRock", 2, "You dont have enough FireRock");
            AddRes(index, typeof(CrystalineFire), "CrystalineFire", 1, "You dont have enough FireCrystal");
			AddRes( index, typeof( FireRuby ), 1032695, 3, 1044240 );
          //  SetUseAllRes(index, true);
            #endregion

                #region Item
            index = AddCraft(typeof(FlamingArrow), "Item", "FlamingArrow", 50.0, 75.0, typeof(SmallFireRock), "SmallFireRock", 2, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 2, 1044240 );
            AddRes(index, typeof(PigIron), "PigIron", 1, "You dont have enough PigIron");
            SetUseAllRes(index, true);

            index = AddCraft(typeof(FireBolt), "Item", "FireBolt", 50.0, 75.0, typeof(SmallFireRock), "SmallFireRock", 2, "You dont have enough FireRock");
			AddRes( index, typeof( FireRuby ), 1032695, 2, 1044240 );
            AddRes(index, typeof(PigIron), "PigIron", 1, "You dont have enough PigIron");
            SetUseAllRes(index, true);
            AddCraft(typeof(TallDragonBrazierAddonDeed), "Item", "TallDragonBrazierAddonDeed", 85.0, 95.0, typeof(SmallFireRock), "SmallFireRock", 15, "You dont have enough FireRock");
            AddCraft(typeof(HorseBardingNorthAddonDeed), "Item", "HorseBardingNorthAddonDeed", 85.0, 95.0, typeof(SmallFireRock), "SmallFireRock", 15, "You dont have enough FireRock");
            AddCraft(typeof(HorseBardingWestAddonDeed), "Item", "HorseBardingWestAddonDeed", 85.0, 95.0, typeof(SmallFireRock), "SmallFireRock", 15, "You dont have enough FireRock");
            AddCraft(typeof(LamppostAddonDeed), "Item", "LamppostAddonDeed", 85.0, 95.0, typeof(SmallFireRock), "SmallFireRock", 15, "You dont have enough FireRock");
            AddCraft(typeof(LamppostFancyAddonDeed), "Item", "LamppostFancyAddonDeed", 85.0, 95.0, typeof(SmallFireRock), "SmallFireRock", 15, "You dont have enough FireRock");
            AddCraft(typeof(BBQEastAddonDeed), "Item", "BBQEastAddonDeed", 95.0, 105.0, typeof(LargeFireRock), "LargeFireRock", 15, "You dont have enough FireRock");
            AddCraft(typeof(BBQSouthAddonDeed), "Item", "BBQSouthAddonDeed", 95.0, 105.0, typeof(LargeFireRock), "LargeFireRock", 15, "You dont have enough FireRock");
            AddCraft(typeof(FireSteedBardingDeed), "Item", "FireSteedBardingDeed", 85.0, 95.0, typeof(SmallFireRock), "SmallFireRock", 10, "You dont have enough FireRock");
               #endregion

            MarkOption = true;
                Repair = Core.AOS;
            }
        }

	}

    

