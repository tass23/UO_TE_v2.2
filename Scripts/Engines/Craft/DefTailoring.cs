using System;
using Server.Items;

namespace Server.Engines.Craft
{
    public enum TailorRecipe
    {
        ElvenQuiver = 501,
        QuiverOfFire = 502,
        QuiverOfIce = 503,
        QuiverOfBlight = 504,
        QuiverOfLightning = 505,

        SongWovenMantle = 550,
        SpellWovenBritches = 551,
        StitchersMittens = 552,
    }

    public class DefTailoring : CraftSystem
    {
        public override SkillName MainSkill
        {
            get { return SkillName.Tailoring; }
        }

        public override int GumpTitleNumber
        {
            get { return 1044005; } // <CENTER>TAILORING MENU</CENTER>
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefTailoring();

                return m_CraftSystem;
            }
        }

        public override CraftECA ECA { get { return CraftECA.ChanceMinusSixtyToFourtyFive; } }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.5; // 50%
        }

		private DefTailoring()
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

        private static Type[] m_TailorColorables = new Type[]
			{
				typeof( GozaMatEastDeed ), typeof( GozaMatSouthDeed ),
				typeof( SquareGozaMatEastDeed ), typeof( SquareGozaMatSouthDeed ),
				typeof( BrocadeGozaMatEastDeed ), typeof( BrocadeGozaMatSouthDeed ),
				typeof( BrocadeSquareGozaMatEastDeed ), typeof( BrocadeSquareGozaMatSouthDeed ),
				typeof( PetMatDeed )
			};

       /* public override bool RetainsColorFrom(CraftItem item, Type type)
        {
            if (type != typeof(Cloth) && type != typeof(UncutCloth))
                return false;

            type = item.ItemType;

            bool contains = false;

            for (int i = 0; !contains && i < m_TailorColorables.Length; ++i)
                contains = (m_TailorColorables[i] == type);

            return contains;
        }*/

        public override bool RetainsColorFrom(CraftItem item, Type type)
        {
            if (type != typeof(Cloth) && type != typeof(UncutCloth))
                return false;

            type = item.ItemType;

            bool contains = false;

            for (int i = 0; !contains && i < m_TailorColorables.Length; ++i)
            {
                if (m_TailorColorables[i] == type)
                {
                    contains = true;
                    break;
                }
            }

            return contains;
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

            #region Hats
            AddCraft(typeof(SkullCap), 1011375, 1025444, 0.0, 25.0, typeof(Cloth), 1044286, 2, 1044287);
            AddCraft(typeof(Bandana), 1011375, 1025440, 0.0, 25.0, typeof(Cloth), 1044286, 2, 1044287);
            AddCraft(typeof(FloppyHat), 1011375, 1025907, 6.2, 31.2, typeof(Cloth), 1044286, 11, 1044287);
			AddCraft( typeof( Cap ), 1011375, 1025909, 6.2, 31.2, typeof( Cloth ), 1044286, 11, 1044287 );
            AddCraft(typeof(WideBrimHat), 1011375, 1025908, 6.2, 31.2, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(StrawHat), 1011375, 1025911, 6.2, 31.2, typeof(Cloth), 1044286, 10, 1044287);
            AddCraft(typeof(TallStrawHat), 1011375, 1025910, 6.7, 31.7, typeof(Cloth), 1044286, 13, 1044287);
            AddCraft(typeof(WizardsHat), 1011375, 1025912, 7.2, 32.2, typeof(Cloth), 1044286, 15, 1044287);
            AddCraft(typeof(Bonnet), 1011375, 1025913, 6.2, 31.2, typeof(Cloth), 1044286, 11, 1044287);
            AddCraft(typeof(FeatheredHat), 1011375, 1025914, 6.2, 31.2, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(TricorneHat), 1011375, 1025915, 6.2, 31.2, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(JesterHat), 1011375, 1025916, 7.2, 32.2, typeof(Cloth), 1044286, 15, 1044287);

            if (Core.AOS)
                AddCraft(typeof(FlowerGarland), 1011375, 1028965, 10.0, 35.0, typeof(Cloth), 1044286, 5, 1044287);

            if (Core.SE)
            {
                index = AddCraft(typeof(ClothNinjaHood), 1011375, 1030202, 80.0, 105.0, typeof(Cloth), 1044286, 13, 1044287);
                SetNeededExpansion(index, Expansion.SE);

                index = AddCraft(typeof(Kasa), 1011375, 1030211, 60.0, 85.0, typeof(Cloth), 1044286, 12, 1044287);
                SetNeededExpansion(index, Expansion.SE);
            }
            #endregion

            #region Shirts
            AddCraft(typeof(Doublet), 1015269, 1028059, 0, 25.0, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(Shirt), 1015269, 1025399, 20.7, 45.7, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(FancyShirt), 1015269, 1027933, 24.8, 49.8, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(Tunic), 1015269, 1028097, 00.0, 25.0, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(Surcoat), 1015269, 1028189, 8.2, 33.2, typeof(Cloth), 1044286, 14, 1044287);
            AddCraft(typeof(PlainDress), 1015269, 1027937, 12.4, 37.4, typeof(Cloth), 1044286, 10, 1044287);
            AddCraft(typeof(FancyDress), 1015269, 1027935, 33.1, 58.1, typeof(Cloth), 1044286, 12, 1044287);
            AddCraft(typeof(Cloak), 1015269, 1025397, 41.4, 66.4, typeof(Cloth), 1044286, 14, 1044287);
            AddCraft(typeof(Robe), 1015269, 1027939, 53.9, 78.9, typeof(Cloth), 1044286, 16, 1044287);
            AddCraft(typeof(JesterSuit), 1015269, 1028095, 8.2, 33.2, typeof(Cloth), 1044286, 24, 1044287);

            if (Core.AOS)
            {
                AddCraft(typeof(FurCape), 1015269, 1028969, 35.0, 60.0, typeof(Cloth), 1044286, 13, 1044287);
                AddCraft(typeof(GildedDress), 1015269, 1028973, 37.5, 62.5, typeof(Cloth), 1044286, 16, 1044287);
                AddCraft(typeof(FormalShirt), 1015269, 1028975, 26.0, 51.0, typeof(Cloth), 1044286, 16, 1044287);
            }

            if (Core.SE)
            {
                index = AddCraft(typeof(ClothNinjaJacket), 1015269, 1030207, 75.0, 100.0, typeof(Cloth), 1044286, 12, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(Kamishimo), 1015269, 1030212, 75.0, 100.0, typeof(Cloth), 1044286, 15, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(HakamaShita), 1015269, 1030215, 40.0, 65.0, typeof(Cloth), 1044286, 14, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(MaleKimono), 1015269, 1030189, 50.0, 75.0, typeof(Cloth), 1044286, 16, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(FemaleKimono), 1015269, 1030190, 50.0, 75.0, typeof(Cloth), 1044286, 16, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(JinBaori), 1015269, 1030220, 30.0, 55.0, typeof(Cloth), 1044286, 12, 1044287);
                SetNeededExpansion(index, Expansion.SE);
            }

            #region Mondain's Legacy
            if (Core.ML)
            {
                index = AddCraft(typeof(ElvenShirt), 1015269, 1032661, 80.0, 105.0, typeof(Cloth), 1044286, 10, 1044287);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(ElvenDarkShirt), 1015269, 1032662, 80.0, 105.0, typeof(Cloth), 1044286, 10, 1044287);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(MaleElvenRobe), 1015269, 1032659, 80.0, 105.0, typeof(Cloth), 1044286, 30, 1044287);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(FemaleElvenRobe), 1015269, 1032660, 80.0, 105.0, typeof(Cloth), 1044286, 30, 1044287);
                SetNeededExpansion(index, Expansion.ML);
            }
            #endregion

            // Pants
            #endregion

            #region Pants
            AddCraft(typeof(ShortPants), 1015279, 1025422, 24.8, 49.8, typeof(Cloth), 1044286, 6, 1044287);
            AddCraft(typeof(LongPants), 1015279, 1025433, 24.8, 49.8, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(Kilt), 1015279, 1025431, 20.7, 45.7, typeof(Cloth), 1044286, 8, 1044287);
            AddCraft(typeof(Skirt), 1015279, 1025398, 29.0, 54.0, typeof(Cloth), 1044286, 10, 1044287);

            if (Core.AOS)
                AddCraft(typeof(FurSarong), 1015279, 1028971, 35.0, 60.0, typeof(Cloth), 1044286, 12, 1044287);

            if (Core.SE)
            {
                index = AddCraft(typeof(Hakama), 1015279, 1030213, 50.0, 75.0, typeof(Cloth), 1044286, 16, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(TattsukeHakama), 1015279, 1030214, 50.0, 75.0, typeof(Cloth), 1044286, 16, 1044287);
                SetNeededExpansion(index, Expansion.SE);
            }

            #region Mondain's Legacy
            if (Core.ML)
            {
                index = AddCraft(typeof(ElvenPants), 1015279, 1032665, 80.0, 105.0, typeof(Cloth), 1044286, 12, 1044287);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(WoodlandBelt), 1015279, 1032639, 80.0, 105.0, typeof(Cloth), 1044286, 10, 1044287);
                SetNeededExpansion(index, Expansion.ML);
            }
            #endregion

            #endregion

            #region Misc
            AddCraft(typeof(BodySash), 1015283, 1025441, 4.1, 29.1, typeof(Cloth), 1044286, 4, 1044287);
            AddCraft(typeof(HalfApron), 1015283, 1025435, 20.7, 45.7, typeof(Cloth), 1044286, 6, 1044287);
            AddCraft(typeof(FullApron), 1015283, 1025437, 29.0, 54.0, typeof(Cloth), 1044286, 10, 1044287);

            if (Core.SE)
            {
                index = AddCraft(typeof(Obi), 1015283, 1030219, 20.0, 45.0, typeof(Cloth), 1044286, 6, 1044287);
                SetNeededExpansion(index, Expansion.SE);
            }

            if (Core.ML)
            {
                index = AddCraft(typeof(ElvenQuiver), 1015283, 1032657, 65.0, 115.0, typeof(Leather), 1044462, 28, 1044463);
                AddRecipe(index, (int)TailorRecipe.ElvenQuiver);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(QuiverOfFire), 1015283, 1073109, 65.0, 115.0, typeof(Leather), 1044462, 28, 1044463);
                AddRes(index, typeof(FireRuby), 1032695, 15, 1042081);
                AddRecipe(index, (int)TailorRecipe.QuiverOfFire);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(QuiverOfIce), 1015283, 1073110, 65.0, 115.0, typeof(Leather), 1044462, 28, 1044463);
                AddRes(index, typeof(WhitePearl), 1032694, 15, 1042081);
                AddRecipe(index, (int)TailorRecipe.QuiverOfIce);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(QuiverOfBlight), 1015283, 1073111, 65.0, 115.0, typeof(Leather), 1044462, 28, 1044463);
                AddRes(index, typeof(Blight), 1032675, 10, 1042081);
                AddRecipe(index, (int)TailorRecipe.QuiverOfBlight);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(QuiverOfLightning), 1015283, 1073112, 65.0, 115.0, typeof(Leather), 1044462, 28, 1044463);
                AddRes(index, typeof(Corruption), 1032676, 10, 1042081);
                AddRecipe(index, (int)TailorRecipe.QuiverOfLightning);
                SetNeededExpansion(index, Expansion.ML);

                #region Mondain's Legacy
                index = AddCraft(typeof(LeatherContainerEngraver), 1015283, 1072152, 75.0, 100.0, typeof(Bone), 1049064, 1, 1049063);
                AddRes(index, typeof(Leather), 1044462, 6, 1044463);
                AddRes(index, typeof(SpoolOfThread), 1073462, 2, 1073463);
                AddRes(index, typeof(Dyes), 1024009, 6, 1044253);
                SetNeededExpansion(index, Expansion.ML);
                #endregion
            }
            
            AddCraft(typeof(OilCloth), 1015283, 1041498, 74.6, 99.6, typeof(Cloth), 1044286, 1, 1044287);

            if (Core.SE)
            {
                index = AddCraft(typeof(GozaMatEastDeed), 1015283, 1030404, 55.0, 80.0, typeof(Cloth), 1044286, 25, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(GozaMatSouthDeed), 1015283, 1030405, 55.0, 80.0, typeof(Cloth), 1044286, 25, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(SquareGozaMatEastDeed), 1015283, 1030407, 55.0, 80.0, typeof(Cloth), 1044286, 25, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(SquareGozaMatSouthDeed), 1015283, 1030406, 55.0, 80.0, typeof(Cloth), 1044286, 25, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(BrocadeGozaMatEastDeed), 1015283, 1030408, 55.0, 80.0, typeof(Cloth), 1044286, 25, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(BrocadeGozaMatSouthDeed), 1015283, 1030409, 55.0, 80.0, typeof(Cloth), 1044286, 25, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(BrocadeSquareGozaMatEastDeed), 1015283, 1030411, 55.0, 80.0, typeof(Cloth), 1044286, 25, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(BrocadeSquareGozaMatSouthDeed), 1015283, 1030410, 55.0, 80.0, typeof(Cloth), 1044286, 25, 1044287);
                SetNeededExpansion(index, Expansion.SE);
            }

			#region UO-The Expanse
			index = AddCraft(typeof(PetMatDeed), 1015283, "pet mat", 55.0, 80.0, typeof(OilCloth), 1041498, 10, 1044287);
            AddRes(index, typeof(Leather), 1044462, 20, 1044463);
			SetNeededExpansion(index, Expansion.SE);
			#endregion
            #region SA
            /*if (Core.SA)
                {
                    AddCraft(typeof(GargishSash), 1015283, "gargish sash", 4.1, 29.1, typeof(Cloth), 1044286, 4, 1044287);
                    AddCraft(typeof(GargishHalfApron), 1015283, "gargish half apron", 20.7, 45.7, typeof(Cloth), 1044286, 6, 1044287);
                }
                  */
            #endregion
            #endregion

            #region Footwear

            #region Mondain's Legacy
            if (Core.ML)
            {
                index = AddCraft(typeof(ElvenBoots), 1015283, 1072902, 80.0, 105.0, typeof(Leather), 1044462, 15, 1044463);
                SetNeededExpansion(index, Expansion.ML);
            }
			#endregion

            if (Core.AOS)
                AddCraft(typeof(FurBoots), 1015288, 1028967, 50.0, 75.0, typeof(Cloth), 1044286, 12, 1044287);

            if (Core.SE)
            {
                index = AddCraft(typeof(NinjaTabi), 1015288, 1030210, 70.0, 95.0, typeof(Cloth), 1044286, 10, 1044287);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(SamuraiTabi), 1015288, 1030209, 20.0, 45.0, typeof(Cloth), 1044286, 6, 1044287);
                SetNeededExpansion(index, Expansion.SE);
            }

            AddCraft(typeof(Sandals), 1015288, 1025901, 12.4, 37.4, typeof(Leather), 1044462, 4, 1044463);
            AddCraft(typeof(Shoes), 1015288, 1025904, 16.5, 41.5, typeof(Leather), 1044462, 6, 1044463);
            AddCraft(typeof(Boots), 1015288, 1025899, 33.1, 58.1, typeof(Leather), 1044462, 8, 1044463);
            AddCraft(typeof(ThighBoots), 1015288, 1025906, 41.4, 66.4, typeof(Leather), 1044462, 10, 1044463);
            #region SA
            if (Core.SA)
            {
                AddCraft(typeof(LeatherTalons), 1015288, "gargish leather talons", 40.4, 65.4, typeof(Leather), 1044462, 6, 1044453);
            }
            #endregion
            #endregion

            #region Leather Armor

            #region Mondain's Legacy
            if (Core.ML)
            {
                index = AddCraft(typeof(SpellWovenBritches), 1015293, 1072929, 92.5, 117.5, typeof(Leather), 1044462, 15, 1044463);
                AddRes(index, typeof(EyeOfTheTravesty), 1032685, 1, 1044253);
                AddRes(index, typeof(Putrefication), 1032678, 10, 1044253);
                AddRes(index, typeof(Scourge), 1032677, 10, 1044253);
                AddRecipe(index, (int)TailorRecipe.SpellWovenBritches);
                ForceNonExceptional(index);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(SongWovenMantle), 1015293, 1072931, 92.5, 117.5, typeof(Leather), 1044462, 15, 1044463);
                AddRes(index, typeof(EyeOfTheTravesty), 1032685, 1, 1044253);
                AddRes(index, typeof(Blight), 1032675, 10, 1044253);
                AddRes(index, typeof(Muculent), 1032680, 10, 1044253);
                AddRecipe(index, (int)TailorRecipe.SongWovenMantle);
                ForceNonExceptional(index);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(StitchersMittens), 1015293, 1072932, 92.5, 117.5, typeof(Leather), 1044462, 15, 1044463);
                AddRes(index, typeof(CapturedEssence), 1032686, 1, 1044253);
                AddRes(index, typeof(Corruption), 1032676, 10, 1044253);
                AddRes(index, typeof(Taint), 1032679, 10, 1044253);
                AddRecipe(index, (int)TailorRecipe.StitchersMittens);
                ForceNonExceptional(index);
                SetNeededExpansion(index, Expansion.ML);
            }
            #endregion

            AddCraft(typeof(LeatherGorget), 1015293, 1025063, 53.9, 78.9, typeof(Leather), 1044462, 4, 1044463);
            AddCraft(typeof(LeatherCap), 1015293, 1027609, 6.2, 31.2, typeof(Leather), 1044462, 2, 1044463);
            AddCraft(typeof(LeatherGloves), 1015293, 1025062, 51.8, 76.8, typeof(Leather), 1044462, 3, 1044463);
            AddCraft(typeof(LeatherArms), 1015293, 1025061, 53.9, 78.9, typeof(Leather), 1044462, 4, 1044463);
            AddCraft(typeof(LeatherLegs), 1015293, 1025067, 66.3, 91.3, typeof(Leather), 1044462, 10, 1044463);
            AddCraft(typeof(LeatherChest), 1015293, 1025068, 70.5, 95.5, typeof(Leather), 1044462, 12, 1044463);

            if (Core.SE)
            {
                index = AddCraft(typeof(LeatherJingasa), 1015293, 1030177, 45.0, 70.0, typeof(Leather), 1044462, 4, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(LeatherMempo), 1015293, 1030181, 80.0, 105.0, typeof(Leather), 1044462, 8, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(LeatherDo), 1015293, 1030182, 75.0, 100.0, typeof(Leather), 1044462, 12, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(LeatherHiroSode), 1015293, 1030185, 55.0, 80.0, typeof(Leather), 1044462, 5, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(LeatherSuneate), 1015293, 1030193, 68.0, 93.0, typeof(Leather), 1044462, 12, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(LeatherHaidate), 1015293, 1030197, 68.0, 93.0, typeof(Leather), 1044462, 12, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(LeatherNinjaPants), 1015293, 1030204, 80.0, 105.0, typeof(Leather), 1044462, 13, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(LeatherNinjaJacket), 1015293, 1030206, 85.0, 110.0, typeof(Leather), 1044462, 13, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(LeatherNinjaBelt), 1015293, 1030203, 50.0, 75.0, typeof(Leather), 1044462, 5, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(LeatherNinjaMitts), 1015293, 1030205, 65.0, 90.0, typeof(Leather), 1044462, 12, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(LeatherNinjaHood), 1015293, 1030201, 90.0, 115.0, typeof(Leather), 1044462, 14, 1044463);
                SetNeededExpansion(index, Expansion.SE);
            }
            #region SA
            if (Core.SA)
            {
                AddCraft(typeof(GargishLeatherArms), 1015293, 1095327, 53.9, 78.9, typeof(Leather), 1044462, 8, 1044463);
                AddCraft(typeof(GargishLeatherChest), 1015293, 1095329, 70.5, 95.5, typeof(Leather), 1044462, 8, 1044463);
                AddCraft(typeof(GargishLeatherKilt), 1015293, 1095331, 58.0, 83.0, typeof(Leather), 1044462, 6, 1044463);
                AddCraft(typeof(GargishLeatherLegs), 1015293, 1095333, 66.3, 91.3, typeof(Leather), 1044462, 10, 1044463);
                AddCraft(typeof(GargishLeatherWingArmor), 1015293, "gargish leather wing armor", 65.0, 90.0, typeof(Leather), 1044462, 12, 1044463);
            }
            #endregion
            #region Mondain's Legacy
            if (Core.ML)
            {
                index = AddCraft(typeof(LeafChest), 1015293, 1032667, 75.0, 100.0, typeof(Leather), 1044462, 15, 1044463);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(LeafArms), 1015293, 1032670, 60.0, 85.0, typeof(Leather), 1044462, 12, 1044463);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(LeafGloves), 1015293, 1032668, 60.0, 85.0, typeof(Leather), 1044462, 10, 1044463);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(LeafLegs), 1015293, 1032671, 75.0, 100.0, typeof(Leather), 1044462, 15, 1044463);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(LeafGorget), 1015293, 1032669, 65.0, 90.0, typeof(Leather), 1044462, 12, 1044463);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(LeafTonlet), 1015293, 1032672, 70.0, 95.0, typeof(Leather), 1044462, 12, 1044463);
                SetNeededExpansion(index, Expansion.ML);
            }
            #endregion

            #endregion

            #region Studded Armor
            AddCraft(typeof(StuddedGorget), 1015300, 1025078, 78.8, 103.8, typeof(Leather), 1044462, 6, 1044463);
            AddCraft(typeof(StuddedGloves), 1015300, 1025077, 82.9, 107.9, typeof(Leather), 1044462, 8, 1044463);
            AddCraft(typeof(StuddedArms), 1015300, 1025076, 87.1, 112.1, typeof(Leather), 1044462, 10, 1044463);
            AddCraft(typeof(StuddedLegs), 1015300, 1025082, 91.2, 116.2, typeof(Leather), 1044462, 12, 1044463);
            AddCraft(typeof(StuddedChest), 1015300, 1025083, 94.0, 119.0, typeof(Leather), 1044462, 14, 1044463);

            if (Core.SE)
            {
                index = AddCraft(typeof(StuddedMempo), 1015300, 1030216, 80.0, 105.0, typeof(Leather), 1044462, 8, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(StuddedDo), 1015300, 1030183, 95.0, 120.0, typeof(Leather), 1044462, 14, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(StuddedHiroSode), 1015300, 1030186, 85.0, 110.0, typeof(Leather), 1044462, 8, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(StuddedSuneate), 1015300, 1030194, 92.0, 117.0, typeof(Leather), 1044462, 14, 1044463);
                SetNeededExpansion(index, Expansion.SE);
                index = AddCraft(typeof(StuddedHaidate), 1015300, 1030198, 92.0, 117.0, typeof(Leather), 1044462, 14, 1044463);
                SetNeededExpansion(index, Expansion.SE);
            }

            #region Mondain's Legacy
            if (Core.ML)
            {
                index = AddCraft(typeof(HideChest), 1015300, 1032651, 85.0, 110.0, typeof(Leather), 1044462, 15, 1044463);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(HidePauldrons), 1015300, 1032654, 75.0, 100.0, typeof(Leather), 1044462, 12, 1044463);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(HideGloves), 1015300, 1032652, 75.0, 100.0, typeof(Leather), 1044462, 10, 1044463);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(HidePants), 1015300, 1032655, 92.0, 117.0, typeof(Leather), 1044462, 15, 1044463);
                SetNeededExpansion(index, Expansion.ML);

                index = AddCraft(typeof(HideGorget), 1015300, 1032653, 90.0, 115.0, typeof(Leather), 1044462, 12, 1044463);
                SetNeededExpansion(index, Expansion.ML);
            }
            #endregion

            #endregion

            #region Female Armor
            AddCraft(typeof(LeatherShorts), 1015306, 1027168, 62.2, 87.2, typeof(Leather), 1044462, 8, 1044463);
            AddCraft(typeof(LeatherSkirt), 1015306, 1027176, 58.0, 83.0, typeof(Leather), 1044462, 6, 1044463);
            AddCraft(typeof(LeatherBustierArms), 1015306, 1027178, 58.0, 83.0, typeof(Leather), 1044462, 6, 1044463);
            AddCraft(typeof(StuddedBustierArms), 1015306, 1027180, 82.9, 107.9, typeof(Leather), 1044462, 8, 1044463);
            AddCraft(typeof(FemaleLeatherChest), 1015306, 1027174, 62.2, 87.2, typeof(Leather), 1044462, 8, 1044463);
            AddCraft(typeof(FemaleStuddedChest), 1015306, 1027170, 87.1, 112.1, typeof(Leather), 1044462, 10, 1044463);
            #endregion

            #region Bone Armor
            index = AddCraft(typeof(BoneHelm), 1049149, 1025206, 85.0, 110.0, typeof(Leather), 1044462, 4, 1044463);
            AddRes(index, typeof(Bone), 1049064, 2, 1049063);

            index = AddCraft(typeof(BoneGloves), 1049149, 1025205, 89.0, 114.0, typeof(Leather), 1044462, 6, 1044463);
            AddRes(index, typeof(Bone), 1049064, 2, 1049063);

            index = AddCraft(typeof(BoneArms), 1049149, 1025203, 92.0, 117.0, typeof(Leather), 1044462, 8, 1044463);
            AddRes(index, typeof(Bone), 1049064, 4, 1049063);

            index = AddCraft(typeof(BoneLegs), 1049149, 1025202, 95.0, 120.0, typeof(Leather), 1044462, 10, 1044463);
            AddRes(index, typeof(Bone), 1049064, 6, 1049063);

            index = AddCraft(typeof(BoneChest), 1049149, 1025199, 96.0, 121.0, typeof(Leather), 1044462, 12, 1044463);
            AddRes(index, typeof(Bone), 1049064, 10, 1049063);
            #endregion
            #region Cloth Armor
            if (Core.SA)
            {
                AddCraft(typeof(GargishClothArms), 1111748, 1021027, 53.9, 78.9, typeof(Cloth), 1044462, 8, 1044463);
                AddCraft(typeof(GargishClothChest), 1111748, 1021029, 6.2, 31.2, typeof(Cloth), 1044462, 8, 1044463);
                AddCraft(typeof(GargishClothKilt), 1111748, 1021031, 51.8, 76.8, typeof(Cloth), 1044462, 6, 1044463);
                AddCraft(typeof(GargishClothLegs), 1111748, 1021033, 53.9, 78.9, typeof(Cloth), 1044462, 10, 1044463);
                AddCraft(typeof(GargishClothWingArmor), 1111748, "gargish cloth wing armor", 66.3, 91.3, typeof(Cloth), 1044462, 12, 1044463);
            }
            #endregion
			
			#region Pillow Crafting
            index = AddCraft(typeof(TaggedPillow), "Pillows", "tagged pillow", 6.2, 31.2, typeof(Cloth), 1044286, 10, 1044287);
            AddRes(index, typeof(Wool), "Wool", 5, "You do not have enough Wool.");
			
            index = AddCraft(typeof(ThrowingPillow), "Pillows", "throwing pillow", 6.2, 31.2, typeof(Cloth), 1044286, 10, 1044287);
            AddRes(index, typeof(Feather), "Feather", 20, "You do not have enough Feathers.");
			
            index = AddCraft(typeof(LargePillow), "Pillows", "large pillow", 6.2, 31.2, typeof(Cloth), 1044286, 12, 1044287);
            AddRes(index, typeof(Cotton), "Cotton", 12, "You do not have enough Cotton.");
			
            index = AddCraft(typeof(MediumPillow), "Pillows", "medium pillow", 6.2, 31.2, typeof(Cloth), 1044286, 4, 1044287);
            AddRes(index, typeof(Cotton), "Cotton", 4, "You do not have enough Cotton.");
			
            index = AddCraft(typeof(SmallPillow), "Pillows", "small pillow", 6.2, 31.2, typeof(Cloth), 1044286, 4, 1044287);
            AddRes(index, typeof(Cotton), "Cotton", 4, "You do not have enough Cotton.");
			
			index = AddCraft(typeof(TasslePillow), "Pillows", "tassle pillow", 6.2, 31.2, typeof(Cloth), 1044286, 6, 1044287);
            AddRes(index, typeof(Cotton), "Cotton", 6, "You do not have enough Cotton.");
			
            index = AddCraft(typeof(ShagPillow), "Pillows", "shag pillow", 6.2, 31.2, typeof(Cloth), 1044286, 4, 1044287);
            AddRes(index, typeof(Cotton), "Cotton", 4, "You do not have enough Cotton.");
			
            index = AddCraft(typeof(RoundPillow), "Pillows", "round pillow", 6.2, 31.2, typeof(Cloth), 1044286, 10, 1044287);
            AddRes(index, typeof(Cotton), "Cotton", 10, "You do not have enough Cotton.");
			#endregion
			
			#region Carpet Weaving
			index = AddCraft( typeof( BlueRug2Deed ), 1076602, "blue rug", 105.0, 125.0, typeof( Wool ), "Wool", 24, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( BlueDecorativeRug2Deed ), 1076602, 1076589, 115.0, 130.5, typeof( Wool ), "Wool", 24, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( RedRug2Deed ), 1076602, "red rug", 105.0, 125.0, typeof( Wool ), "Wool", 24, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( RedPlainRug2Deed ), 1076602, 1076588, 107.0, 127.0, typeof( Wool ), "Wool", 24, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( CinnamonRug2Deed ), 1076602, "large cinnamon rug", 105.0, 125.0, typeof( Wool ), "Wool", 24, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( CinnamonFancyRug4Deed ), 1076602, 1076587, 110.2, 130.0, typeof( Wool ), "Wool", 24, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( CinnamonFancyRug2Deed ), 1076602, "cinnamon decorative rug", 115.0, 130.5, typeof( Wool ), "Wool", 24, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( CinnamonFancyRug3Deed ), 1076602, "cinnamon artisan rug", 115.0, 130.5, typeof( Wool ), "Wool", 24, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( GoldenRug2Deed ), 1076602, "large golden rug", 105.0, 125.0, typeof( Wool ), "Wool", 24, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( GoldenDecorativeRug2Deed ), 1076602, 1076586, 115.0, 130.5, typeof( Wool ), "Wool", 24, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 6, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( BlueRunnerNSDeed ), 1076602, "blue runner N/S", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( BlueRunnerEWDeed ), 1076602, "blue runner E/W", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( RedRunnerNSDeed ), 1076602, "red runner N/S", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( RedRunnerEWDeed ), 1076602, "red runner E/W", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( GoldenRunnerNSDeed ), 1076602, "golden runner N/S", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( GoldenRunnerEWDeed ), 1076602, "golden runner E/W", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( CinnamonRunnerNSDeed ), 1076602, "cinnamon runner N/S", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			
			index = AddCraft( typeof( CinnamonRunnerEWDeed ), 1076602, "cinnamon runner E/W", 105.0, 125.0, typeof( Wool ), "Wool", 12, "You do not have enough Wool." );
			AddRes( index, typeof( Dyes ), 1024009, 4, 1044253 );
			AddRes( index, typeof( Flax ), 1026809, 1, 1044253 );
			#endregion

            // Set the overridable material
            SetSubRes(typeof(Leather), 1049150);

            // Add every material you want the player to be able to choose from
            // This will override the overridable material
            AddSubRes(typeof(Leather), 1049150, 00.0, 1044462, 1049311);
            AddSubRes(typeof(SpinedLeather), 1049151, 65.0, 1044462, 1049311);
            AddSubRes(typeof(HornedLeather), 1049152, 80.0, 1044462, 1049311);
            AddSubRes(typeof(BarbedLeather), 1049153, 99.0, 1044462, 1049311);

            MarkOption = true;
                Repair = Core.ML;
                CanEnhance = Core.ML;
                //AlterItem = true;
        }
    }
}