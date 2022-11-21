using System;
using Server;
using Server.Items;
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Engines.Craft
{
    public enum ForgingRecipes
    {
    }

    public class DefForging : CraftSystem
    {
        #region Mondain's Legacy
        public override CraftECA ECA { get { return CraftECA.ChanceMinusSixtyToFourtyFive; } }
        #endregion

        public override SkillName MainSkill
        {
            get { return SkillName.Tinkering; }
        }

		public override int GumpTitleNumber
		{
			get { return 0; } // Use String
		}
		
        public override string GumpTitleString
        {
            get { return "<basefont color=#FFFFFF><CENTER>LIGHTSABER CRAFTING</CENTER></basefont>"; } // <CENTER>TINKERING MENU</CENTER>
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefForging();

                return m_CraftSystem;
            }
        }

        private DefForging(): base(1, 1, 1.25)// base( 1, 1, 3.0 )
        {
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.5; // 0%
        }

		private static Type typeofLightsaberForge = typeof( LightsaberForgeAttribute );

		public static void CheckLightsaberForge( Mobile from, int range, out bool lightsaberforge )
		{
			lightsaberforge = false;
			Map map = from.Map;

			if ( map == null )
				return;

			IPooledEnumerable eable = map.GetItemsInRange( from.Location, range );

			foreach ( Item item in eable )
			{
				Type type = item.GetType();
				bool isLightsaberForge = ( type.IsDefined( typeofLightsaberForge, false ) || item.ItemID == 10989 || item.ItemID == 10973);

				if ( isLightsaberForge )
				{
					if ( (from.Z + 16) < item.Z || (item.Z + 16) < from.Z || !from.InLOS( item ) )
						continue;

					lightsaberforge = lightsaberforge || isLightsaberForge;

					if ( lightsaberforge )
						break;
				}
			}

			eable.Free();

			for ( int x = -range; (!lightsaberforge) && x <= range; ++x )
			{
				for ( int y = -range; (!lightsaberforge) && y <= range; ++y )
				{
					StaticTile[] tiles = map.Tiles.GetStaticTiles( from.X+x, from.Y+y, true );

					for ( int i = 0; (!lightsaberforge) && i < tiles.Length; ++i )
					{
						int id = tiles[i].ID & 0x7FFF;

						bool isLightsaberForge = ( id == 10989 || id == 10973 );

						if ( isLightsaberForge )
						{
							if ( (from.Z + 16) < tiles[i].Z || (tiles[i].Z + 16) < from.Z || !from.InLOS( new Point3D( from.X+x, from.Y+y, tiles[i].Z + (tiles[i].Height/2) + 1 ) ) )
								continue;

							lightsaberforge = lightsaberforge || isLightsaberForge;
						}
					}
				}
			}
		}
		
        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
				
			bool lightsaberforge;
			CheckLightsaberForge( from, 2, out lightsaberforge );
            
            if ( lightsaberforge )
				return 0;

            return 1060620;// ("You must be near a Lightsaber Forge to use this tool!"); Is what we want it to return, but can't substitue a string for an int. Working on this issue.
        }
		
        private static Type[] m_ForgingColorables = new Type[]
		{
			typeof( Lightsaber )
		};

        public override bool RetainsColorFrom(CraftItem item, Type type)
        {
            if (!type.IsSubclassOf(typeof(BaseFocusingCrystal)))
                return false;

            type = item.ItemType;
            bool contains = false;

            for (int i = 0; !contains && i < m_ForgingColorables.Length; ++i)
                contains = (m_ForgingColorables[i] == type);

            return contains;
        }

        public override void PlayCraftEffect(Mobile from)
        {
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

            /*
			#region Tools
				AddCraft(typeof(LightsaberHiltMold), 1044046, "Lightsaber Hilt Mold", 5.0, 85.0, typeof(IronIngot), 1044036, 5, 1044037);
            #endregion
			*/			
            #region Parts
				AddCraft(typeof(LightsaberHilt), 1044047, "Lightsaber Hilt", 5.0, 93.0, typeof(IronIngot), 1044036, 10, 1044037);
				AddCraft(typeof(PowerCell), 1044047, "Diatium Power Cell", 5.0, 91.0, typeof(IronIngot), 1044036, 15, 1044037);
				AddCraft(typeof(StabilizingRing), 1044047, "Magnetic Stabilizing Ring", 5.0, 92.0, typeof(IronIngot), 1044036, 5, 1044037);
            #endregion

            #region Lightside Blue Crystal Lightsabers
                index = AddCraft(typeof(AnkarresLightsaber), "Lightside crystals", "Ankarres Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Blue1FocusingCrystal), "Ankarres Sapphire", 1, "You need an Ankarres Sapphire to craft this.");
				
                index = AddCraft(typeof(KenobiLegacyLightsaber), "Lightside crystals", "Kenobi's Legacy Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Blue3FocusingCrystal), "Kenobi's Legacy", 1, "You need a Kenobi's Legacy crystal to craft this.");
				
                index = AddCraft(typeof(KraytDragonLightsaber), "Lightside crystals", "Krayt dragon pearl Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Blue4FocusingCrystal), "Krayt Dragon Pearl", 1, "You need a Krayt dragon pearl to craft this.");
				
                index = AddCraft(typeof(UpariLightsaber), "Lightside crystals", "Upari Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Blue6FocusingCrystal), "Upari Crystal", 1, "You need an Upari crystal to craft this.");
				
                index = AddCraft(typeof(AdeganLightsaber), "Lightside crystals", "Green Adegan Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Green1FocusingCrystal), "Green Adegan Crystal", 1, "You need a Green Adegan crystal to craft this.");
				
				index = AddCraft(typeof(BondaraFollyLightsaber), "Lightside crystals", "Bondara's Folly Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Green3FocusingCrystal), "Bondara's Folly", 1, "You need a Bondara's Folly crystal to craft this.");
				
				index = AddCraft(typeof(DagobahLightsaber), "Lightside crystals", "Dawn of Dagobah Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Green4FocusingCrystal), "Dawn of Dagobah", 1, "You need a Dawn of Dagobah crystal to craft this.");
				
				index = AddCraft(typeof(SunriderLightsaber), "Lightside crystals", "Sunrider's Destiny Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Green5FocusingCrystal), "Sunrider's Destiny", 1, "You need a Sunrider's Destiny crystal to craft this.");
				
				index = AddCraft(typeof(DragiteLightsaber), "Lightside crystals", "Yellow Dragite Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Yellow1FocusingCrystal), "Yellow Dragite Crystal", 1, "You need a Yellow Dragite crystal to craft this.");
				
				index = AddCraft(typeof(GuardianLightsaber), "Lightside crystals", "Heart of the Guardian Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Yellow2FocusingCrystal), "Heart of the Guardian", 1, "You need a Heart of the Guardian crystal to craft this.");
				
				index = AddCraft(typeof(DurindfireLightsaber), "Lightside crystals", "Durindfire Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(White2FocusingCrystal), "Durindfire Crystal", 1, "You need a Durindfire crystal to craft this.");
				
				index = AddCraft(typeof(JenruaxLightsaber), "Lightside crystals", "Jenruax Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(White5FocusingCrystal), "Jenruax Crystal", 1, "You need a Jenruax crystal to craft this.");

				index = AddCraft(typeof(UltimaLightsaber), "Lightside crystals", "Ultima-pearl Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(White8FocusingCrystal), "Ultima-Pearl", 1, "You need an Ultima-pearl to craft this.");
				
				index = AddCraft(typeof(QixoniLightsaber), "Lightside crystals", "Qixoni Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Red5FocusingCrystal), "Qixoni Crystal", 1, "You need a Qixoni crystal to craft this.");
				
				index = AddCraft(typeof(LorridianLightsaber), "Lightside crystals", "Lorridian gemstone Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Pink2FocusingCrystal), "Lorridian Gemstone", 1, "You need a Lorridian gemstone to craft this.");
				
				index = AddCraft(typeof(RuusanLightsaber), "Lightside crystals", "Ruusan Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Pink3FocusingCrystal), "Ruusan Crystal", 1, "You need a Ruusan crystal to craft this.");
				
				index = AddCraft(typeof(HurrikaineLightsaber), "Lightside crystals", "Hurrikaine Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Purple1FocusingCrystal), "Hurrikaine Crystal", 1, "You need a Hurrikaine crystal to craft this.");

				index = AddCraft(typeof(WinduLightsaber), "Lightside crystals", "Windu's Guile Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Purple2FocusingCrystal), "Windu's Guile", 1, "You need a Windu's Guile crystal to craft this.");
				
                index = AddCraft(typeof(LambentLightsaber), "Lightside crystals", "Lambent Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Orange1FocusingCrystal), "Lambent Crystal", 1, "You need a Lambent crystal to craft this.");
				
				index = AddCraft(typeof(SolariLightsaber), "Lightside crystals", "Solari Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Orange3FocusingCrystal), "Solari Crystal", 1, "You need a Solari crystal to craft this.");
				
				index = AddCraft(typeof(MantleLightsaber), "Lightside crystals", "Mantle of the Force Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Cyan1FocusingCrystal), "Mantle of the Force", 1, "You need a Mantle of the Force crystal to craft this.");

				index = AddCraft(typeof(MeditationLightsaber), "Lightside crystals", "Meditation Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Cyan2FocusingCrystal), "Meditation Crystal", 1, "You need a Meditation crystal to craft this.");
			#endregion
			
			#region Exile Focusing crystal Lightsabers				
                index = AddCraft(typeof(BaasWisdomLightsaber), "Exile crystals", "Baas' Wisdom Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Blue2FocusingCrystal), "Baas' Wisdom Crystal", 1, "You need a Baas' Wisdom crystal to craft this.");
				
                index = AddCraft(typeof(PermafrostLightsaber), "Exile crystals", "Permafrost Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Blue5FocusingCrystal), "Permafrost Crystal", 1, "You need a Permafrost crystal to craft this.");
				
				index = AddCraft(typeof(AllyaRedemptionLightsaber), "Exile crystals", "Allya's Redemption Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Green2FocusingCrystal), "Allya's Redemption", 1, "You need an Allya's Redemption crystal to craft this.");
				
				index = AddCraft(typeof(ImpactLightsaber), "Exile crystals", "Impact Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Yellow3FocusingCrystal), "Impact Crystal", 1, "You need an Impact crystal to craft this.");
				
				index = AddCraft(typeof(BarabLightsaber), "Exile crystals", "Barab ore Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(White1FocusingCrystal), "Barab Ore", 1, "You need some Barab ore to craft this.");
				
				index = AddCraft(typeof(RubatLightsaber), "Exile crystals", "Rubat Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(White6FocusingCrystal), "Rubat Crystal", 1, "You need a Rubat crystal to craft this.");
		
				index = AddCraft(typeof(AllyaExileLightsaber), "Exile crystals", "Allya's Exile Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Red2FocusingCrystal), "Allya's Exile", 1, "You need an Allya's Exile crystal to craft this.");
				
				index = AddCraft(typeof(VelmoriteLightsaber), "Exile crystals", "Velmorite Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Orange4FocusingCrystal), "Velmorite Crystal", 1, "You need a Velmorite crystal to craft this.");
				
				index = AddCraft(typeof(UlricRedemptionLightsaber), "Exile crystals", "Ulric's Redemption Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Brown1FocusingCrystal), "Ulric's Redemption", 1, "You need an Ulric's Redemption crystal to craft this.");
			#endregion
			
			#region Darkside Focusing Crystal Lightsabers
				index = AddCraft(typeof(EralamLightsaber), "Darkside crystals", "Eralam Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(White3FocusingCrystal), "Eralam Crystal", 1, "You need an Eralam crystal to craft this.");
				
				index = AddCraft(typeof(NextorLightsaber), "Darkside crystals", "Nextor Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(White4FocusingCrystal), "Nextor Crystal", 1, "You need a Nextor crystal to craft this.");
				
				index = AddCraft(typeof(SapithLightsaber), "Darkside crystals", "Sapith Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(White7FocusingCrystal), "Sapith Crystal", 1, "You need a Sapith crystal to craft this.");
				
				index = AddCraft(typeof(BlackwingLightsaber), "Darkside crystals", "Blackwing Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Gray1FocusingCrystal), "Blackwing Crystal", 1, "You need a Blackwing crystal to craft this.");
				
				index = AddCraft(typeof(LignanLightsaber), "Darkside crystals", "Lignan Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Gray2FocusingCrystal), "Lignan Crystal", 1, "You need a Lignan crystal to craft this.");
				
				index = AddCraft(typeof(StygiumLightsaber), "Darkside crystals", "Stygium Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Gray3FocusingCrystal), "Stygium Crystal", 1, "You need a Stygium crystal to craft this.");
				
				index = AddCraft(typeof(BondarLightsaber), "Darkside crystals", "Bondar Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Red1FocusingCrystal), "Bondar Crystal", 1, "You need a Bondar crystal to craft this.");
				
				index = AddCraft(typeof(TyranusLightsaber), "Darkside crystals", "Cunning of Tyranus Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Red3FocusingCrystal), "Cunning of Tyranus", 1, "You need a Cunning of Tyranus crystal to craft this.");
				
				index = AddCraft(typeof(PhondLightsaber), "Darkside crystals", "Phond Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Red4FocusingCrystal), "Phond Crystal", 1, "You need a Phond crystal to craft this.");
				
				index = AddCraft(typeof(SigilLightsaber), "Darkside crystals", "Sigil Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Red6FocusingCrystal), "Sigil Crystal", 1, "You need a Sigil crystal to craft this.");
				
				index = AddCraft(typeof(SyntheticLightsaber), "Darkside crystals", "Synthetic Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Red7FocusingCrystal), "Synthetic Crystal", 1, "You need a Synthetic crystal to craft this.");
				
				index = AddCraft(typeof(DamindLightsaber), "Darkside crystals", "Damind Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Pink1FocusingCrystal), "Damind Crystal", 1, "You need a Damind cystal to craft this.");
				
				index = AddCraft(typeof(LavaLightsaber), "Darkside crystals", "Lava Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Orange2FocusingCrystal), "Lava Crystal", 1, "You need a Lava crystal to craft this.");
				
				index = AddCraft(typeof(VexxtalLightsaber), "Darkside crystals", "Vexxtal Lightsaber", 110.0, 120.0, typeof(LightsaberHilt), "Lightsaber Hilt", 1, "You need a lightsaber hilt to craft this.");
                AddRes(index, typeof(PowerCell), "Diatium Power Cell", 1, "You need a diatium power cell to craft this.");
                AddRes(index, typeof(StabilizingRing), "Magnetic Stabilizing Ring", 1, "You need a magnetic stabilizing ring.");
				AddRes(index, typeof(Brown2FocusingCrystal), "Vexxtal Crystal", 1, "You need a Vexxtal crystal to craft this.");
			#endregion

            // Set the overridable material
            SetSubRes(typeof(IronIngot), 1044022);

            // Add every material you want the player to be able to choose from
            // This will override the overridable material
            AddSubRes(typeof(IronIngot), 1044022, 00.0, 1044036, 1044267);
            AddSubRes(typeof(DullCopperIngot), 1044023, 65.0, 1044036, 1044268);
            AddSubRes(typeof(GoldIngot), 1044027, 85.0, 1044036, 1044268);

            MarkOption = true;
            //Repair = true;
            //CanEnhance = Core.AOS;
        }
    }
	
	public class LightsaberForgeAttribute : Attribute
	{
		public LightsaberForgeAttribute()
		{
		}
	}
}