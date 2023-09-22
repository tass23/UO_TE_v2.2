/*Rescripted by_____
*                \_  \___ ___
*                / /\/ __/ _ \
*            /\/ /_| (_|  __/
*            \____/ \___\___|
* With Helpful Ideas from: GhostRiderGrey @ RunUO.com
*/
using System;
using Server;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.PartySystem;
using System.Collections.Generic;
using Server.Misc;
using Server.Engines.Plants;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
    public class TreasureMapChest : LockableContainer
    {
        public override int LabelNumber{ get{ return 3000541; } }

        public static Type[] Artifacts { get { return m_Artifacts; } }

        private static Type[] m_Artifacts = new Type[]
        {
            typeof( CandelabraOfSouls ), typeof( GoldBricks ), typeof( PhillipsWoodenSteed ),
            typeof( ArcticDeathDealer ), typeof( BlazeOfDeath ), typeof( BurglarsBandana ),
            typeof( CavortingClub ), typeof( DreadPirateHat ),
            typeof( EnchantedTitanLegBone ), typeof( GwennosHarp ), typeof( IolosLute ),
            typeof( LunaLance ), typeof( NightsKiss ), typeof( NoxRangersHeavyCrossbow ),
            typeof( PolarBearMask ), typeof( VioletCourage ), typeof( HeartOfTheLion ),
            typeof( ColdBlood ), typeof( AlchemistsBauble )
        };

        private int m_Level;
        private DateTime m_DeleteTime;
        private Timer m_Timer;
        private Mobile m_Owner;
        private bool m_Temporary;
		private bool IsThemed;
		private ChestThemeType m_type;
        private bool m_Spawn; // bool variable to stop the spawning of mobiles after it's been taken by a player.

        private List<Mobile> m_Guardians;

        [CommandProperty( AccessLevel.GameMaster )]
        public int Level{ get{ return m_Level; } set{ m_Level = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public Mobile Owner{ get{ return m_Owner; } set{ m_Owner = value; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public DateTime DeleteTime{ get{ return m_DeleteTime; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public bool Temporary{ get{ return m_Temporary; } set{ m_Temporary = value; } }

		//set theme type of chest
		[CommandProperty( AccessLevel.GameMaster )]
		public ChestThemeType Type{ get{ return m_type; } set{ m_type = value; } }

		//set if chest is themed or not
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Themed{ get{ return IsThemed; } set{ IsThemed = value; } }

        public List<Mobile> Guardians { get { return m_Guardians; } }

        [Constructable]
		public TreasureMapChest( int level ) : this( null, level, false, false, ChestThemeType.None )
        {
        }

		[Constructable]
		public TreasureMapChest( int level, ChestThemeType type ) : this( null, level, true, true, type )
        {
        }

		public TreasureMapChest( Mobile owner, int level, bool temporary, bool themed, ChestThemeType type ) : base( 0xE40 )
        {
            m_Owner = owner;
            m_Level = level;
			IsThemed = themed;
			m_type = type;
            m_DeleteTime = DateTime.Now + TimeSpan.FromSeconds( 60.0 );

            m_Temporary = temporary;
            m_Guardians = new List<Mobile>();

            m_Timer = new DeleteTimer( this, m_DeleteTime );
            m_Timer.Start();

            m_Spawn = true; //allow the spawning of gardians

            Fill( this, level, IsThemed, type );
			
			GumpID = 0x4A;
        }
		private static object[] m_Arguments = new object[1];

		//override fill function to keep fishing and other scripts using old fill function not needing updated.
		public static void Fill( LockableContainer cont, int level)
		{
			Fill( cont, level, false, ChestThemeType.None);
        }

        private static void GetRandomAOSStats( out int attributeCount, out int min, out int max )
        {
            int rnd = Utility.Random( 15 );

			if ( Core.SE )
			{
				if ( rnd < 1 )
				{
					attributeCount = Utility.RandomMinMax( 3, 5 );
					min = 50; max = 100;
				}
				else if ( rnd < 3 )
				{
					attributeCount = Utility.RandomMinMax( 2, 5 );
					min = 40; max = 80;
				}
				else if ( rnd < 6 )
				{
					attributeCount = Utility.RandomMinMax( 2, 4 );
					min = 30; max = 60;
				}
				else if ( rnd < 10 )
				{
					attributeCount = Utility.RandomMinMax( 1, 3 );
					min = 20; max = 40;
				}
				else
				{
					attributeCount = 1;
					min = 10; max = 20;
				}
			}
			else
			{
				if ( rnd < 1 )
				{
					attributeCount = Utility.RandomMinMax( 2, 5 );
					min = 20; max = 70;
				}
				else if ( rnd < 3 )
				{
					attributeCount = Utility.RandomMinMax( 2, 4 );
					min = 20; max = 50;
				}
				else if ( rnd < 6 )
				{
					attributeCount = Utility.RandomMinMax( 2, 3 );
					min = 20; max = 40;
				}
				else if ( rnd < 10 )
				{
					attributeCount = Utility.RandomMinMax( 1, 2 );
					min = 10; max = 30;
				}
				else
				{
					attributeCount = 1;
					min = 10; max = 20;
				}
			}
		}

		public static void Fill( LockableContainer cont, int level, bool IsThemed, ChestThemeType type )
        {
            cont.Movable = false;
            cont.Locked = true;
			int numberItems;

            if ( level == 0 )
            {
                cont.LockLevel = 0; // Can't be unlocked

                cont.DropItem( new Gold( Utility.RandomMinMax( 50, 100 ) ) );

                if ( Utility.RandomDouble() < 0.75 )
                    cont.DropItem( new TreasureMap( 0, Map.Trammel ) );
            }
            else
            {
                cont.TrapType = TrapType.ExplosionTrap;
                cont.TrapPower = level * 25;
                cont.TrapLevel = level;

                switch ( level )
                {
                    case 1: cont.RequiredSkill = 36; break;
                    case 2: cont.RequiredSkill = 76; break;
                    case 3: cont.RequiredSkill = 84; break;
                    case 4: cont.RequiredSkill = 92; break;
                    case 5: cont.RequiredSkill = 100; break;
                    case 6: cont.RequiredSkill = 100; break;
                }

                cont.LockLevel = cont.RequiredSkill - 10;
                cont.MaxLockLevel = cont.RequiredSkill + 40;

				AddThemeLoot(cont, level, type);

				cont.DropItem( new Gold( level * 500 ) );
				for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new Gold(50,100) );  
					}

                for ( int i = 0; i < level * 5; ++i )
                    cont.DropItem( Loot.RandomScroll( 0, 63, SpellbookType.Regular ) );

				if ( Core.SE)
				{
					switch ( level )
					{
						case 1: numberItems = 5; break;
						case 2: numberItems = 10; break;
						case 3: numberItems = 15; break;
						case 4: numberItems = 38; break;
						case 5: numberItems = 50; break;
						case 6: numberItems = 60; break;
						default: numberItems = 0; break;
					};
				}
				else
					numberItems = level * 6;
				
				for ( int i = 0; i < numberItems; ++i )
                {
                    Item item;

                    if ( Core.AOS )
                        item = Loot.RandomArmorOrShieldOrWeaponOrJewelry();
                    else
                        item = Loot.RandomArmorOrShieldOrWeapon();

                    if ( item is BaseWeapon )
                    {
                        BaseWeapon weapon = (BaseWeapon)item;

                        if ( Core.AOS )
                        {
                            int attributeCount;
                            int min, max;

                            GetRandomAOSStats( out attributeCount, out min, out max );

                            BaseRunicTool.ApplyAttributesTo( weapon, attributeCount, min, max );
                        }
                        else
                        {
                            weapon.DamageLevel = (WeaponDamageLevel)Utility.Random( 6 );
                            weapon.AccuracyLevel = (WeaponAccuracyLevel)Utility.Random( 6 );
                            weapon.DurabilityLevel = (WeaponDurabilityLevel)Utility.Random( 6 );
                        }

                        cont.DropItem( item );
                    }
                    else if ( item is BaseArmor )
                    {
                        BaseArmor armor = (BaseArmor)item;

                        if ( Core.AOS )
                        {
                            int attributeCount;
                            int min, max;

                            GetRandomAOSStats( out attributeCount, out min, out max );

                            BaseRunicTool.ApplyAttributesTo( armor, attributeCount, min, max );
                        }
                        else
                        {
                            armor.ProtectionLevel = (ArmorProtectionLevel)Utility.Random( 6 );
                            armor.Durability = (ArmorDurabilityLevel)Utility.Random( 6 );
                        }

                        cont.DropItem( item );
                    }
                    else if( item is BaseHat )
                    {
                        BaseHat hat = (BaseHat)item;

                        if( Core.AOS )
                        {
                            int attributeCount;
                            int min, max;

                            GetRandomAOSStats( out attributeCount, out min, out  max );

                            BaseRunicTool.ApplyAttributesTo( hat, attributeCount, min, max );
                        }

                        cont.DropItem( item );
                    }
                    else if( item is BaseJewel )
                    {
                        int attributeCount;
                        int min, max;

                        GetRandomAOSStats( out attributeCount, out min, out max );

                        BaseRunicTool.ApplyAttributesTo( (BaseJewel)item, attributeCount, min, max );

                        cont.DropItem( item );
                    }
                }
            }

            int reagents;
            if ( level == 0 )
                reagents = 12;
            else
                reagents = level * 3;

            for ( int i = 0; i < reagents; i++ )
            {
                Item item = Loot.RandomPossibleReagent();
                item.Amount = Utility.RandomMinMax( 40, 60 );
                cont.DropItem( item );
            }

            int gems;
            if ( level == 0 )
                gems = 2;
            else
                gems = level * 3;

            for ( int i = 0; i < gems; i++ )
            {
                Item item = Loot.RandomGem();
                cont.DropItem( item );
            }

            if ( level == 6 && Core.AOS )
                cont.DropItem( (Item)Activator.CreateInstance( m_Artifacts[Utility.Random(m_Artifacts.Length)] ) );
				if ( Utility.RandomDouble() < .25 )
					cont.DropItem( new RewardScroll() );
        }

		private static void AddThemeLoot (LockableContainer cont, int level, ChestThemeType type)
		{
			//Switch to add in theme treasures
			switch ( type )
			{
				case ChestThemeType.Solen:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new ChitanousStaff() );
					
					//Drop One Special Item
					switch (Utility.Random(2))
					{
						//case 0: cont.DropItem( new Seed(PlantType.HedgeShort,0,false) ); break;
						case 1: cont.DropItem( new WaterBucket() ); break;
					}
					
					//Drop Special Reagent
					for ( int i = 0; i < level * 3; ++i )
					{
						cont.DropItem (new GreenThorns() );  
					}
					for ( int i = 0; i < level * 3; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Brigand:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new BanditsBlade() );
					
					//Drop One Special Item
					switch (Utility.Random(2))
					{
						//case 0: cont.DropItem( new Brazier(true) ); break;
						case 1: cont.DropItem( new DecorativeBow(Utility.RandomMinMax(0,3)) ); break;
					}
					//Drop Special Reagent
					for ( int i = 0; i < level * 4; ++i )
					{
						cont.DropItem (new PowderOfTranslocation() );  
					}
					for ( int i = 0; i < level * 4; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Savage:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new OrnateRitualSpear() );
					
					//Drop One Special Item
					switch (Utility.Random(4))
					{
						case 0: cont.DropItem( new BrownBearRugEastDeed() ); break;
						case 1: cont.DropItem( new BrownBearRugSouthDeed() ); break;
						case 2: cont.DropItem( new BeefCarcass() ); break;
						case 3: cont.DropItem( new SheepCarcass() ); break;
					}
					
					//Drop Special Reagent
					for ( int i = 0; i < level * 4; ++i )
					{
						cont.DropItem (new OrangePetals() );  
					}
					for ( int i = 0; i < level * 4; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Undead:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new SoulReaver() );
				
					//Drop One Special Item
					switch (Utility.Random(5))
					{
						case 0: cont.DropItem( new GraveStone1() ); break;
						case 1: cont.DropItem( new GraveStone2() ); break;
						case 2: cont.DropItem( new GraveStone3() ); break;
						case 3: cont.DropItem( new GraveStone4() ); break;
						case 4: cont.DropItem( new BoneContainer(Utility.RandomMinMax(0,2)) ); break;
					}
					
					//Drop Special Reagent
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new Bone() );  
					}
					for ( int i = 0; i < level * 1; ++i )
					{
						cont.DropItem(new Moonstone(MoonstoneType.Felucca));
					}
					for ( int i = 0; i < level * 1; ++i )
					{
						cont.DropItem(new Moonstone(MoonstoneType.Trammel));
					}
					for ( int i = 0; i < level * 4; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Pirate:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new BucBow() );
					
					//Drop One Special Item
					switch (Utility.Random(5))
					{
						case 0: cont.DropItem( new Oars1() ); break;
						case 1: cont.DropItem( new Oars2() ); break;
						case 2: cont.DropItem( new GenieBottle(false) ); break;
						case 3: cont.DropItem( new Anchor() ); break;
						case 4: cont.DropItem( new Anchor() ); break;
					}
					
					//Drop Special Reagent
					for ( int i = 0; i < level * 1; ++i )
					{
						cont.DropItem (new MessageInABottle() );  
					}
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Dragon:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new DragClaw() );
					
					//Drop One Special Item
					switch (Utility.Random(5))
					{
						case 0: cont.DropItem( new HangingDragonChest() ); break;
						case 1: cont.DropItem( new HangingDragonLegs() ); break;
						case 2: cont.DropItem( new HangingDragonArms() ); break;
						case 3: cont.DropItem( new DragonHeadTrophy1() ); break;
						case 4: cont.DropItem( new DragonHeadTrophy2() ); break;
					}
					
					//Drop Special Reagent
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new DragonBlood() );  
					}
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Demon:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new BaneEnlighten() );
					
					//Drop One Special Item
					switch (Utility.Random(4))
					{
						case 0: cont.DropItem( new DemonSkull1() ); break;
						case 1: cont.DropItem( new DemonSkull2() ); break;
						case 2: cont.DropItem( new DemonSkull3() ); break;
						case 3: cont.DropItem( new DemonSkull4() ); break;
					}
					//Drop Special Reagent
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new DaemonBone() );  
					}
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Corpser:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new Nurture() );
					
					//Drop One Special Item
					switch (Utility.Random(5))
					{
						case 0: cont.DropItem( new Mushrooms1() ); break;
						case 1: cont.DropItem( new Mushrooms2() ); break;
						case 2: cont.DropItem( new FlaxFlower() ); break;
						case 3: cont.DropItem( new Mandrake() ); break;
						case 4: cont.DropItem( new Mandrake2() ); break;
					}
					//Drop Special Reagent
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new FertileDirt() );  
					}
					for ( int i = 0; i < level * 3; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Juka:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new JukaCrossbow() );
					
					//Drop One Special Item
					switch (Utility.Random(7))
					{
						case 0: cont.DropItem( new Cards6() ); break;
						case 1: cont.DropItem( new Cards7() ); break;
						case 2: cont.DropItem( new Cards8() ); break;
						case 3: cont.DropItem( new SkinnedRabbit() ); break;
						case 4: cont.DropItem( new RolledMap() ); break;
						case 5: cont.DropItem( new Brush() ); break;
						case 6: cont.DropItem( new Brush2() ); break;
					}
					//Drop Special Reagent
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new ArcaneGem() );  
					}
					for ( int i = 0; i < level * 4; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Spider:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new Reclusa() );
					
					//Drop One Special Item
					switch (Utility.Random(7))
					{
						case 0: cont.DropItem( new BottleSpider() ); break;
						case 1: cont.DropItem( new Bottle2() ); break;
						case 2: cont.DropItem( new Bottle3() ); break;
						case 3: cont.DropItem( new Bottle4() ); break;
						case 4: cont.DropItem( new SheetMusic() ); break;
						case 5: cont.DropItem( new SkullMug() ); break;
						case 6: cont.DropItem( new DeadBird() ); break;
					}
					
					//Drop Special Reagent
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new SpidersSilk() );  
					}
					for ( int i = 0; i < level * 4; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Reaper:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new ForestGuard() );
					
					//Drop One Special Item
					switch (Utility.Random(5))
					{
						case 0: cont.DropItem( new BirdsNest() ); break;
						case 1: cont.DropItem( new BirdsNest2() ); break;
						case 2: cont.DropItem( new ReaperStat() ); break;
						case 3: cont.DropItem( new ReaperStat1() ); break;
						case 4: cont.DropItem( new ReaperStat2() ); break;
					}
					
					//Drop Special Reagent
					/*for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new PetrafiedWood() );  
					}*/
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Fey:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new IrkBane() );
					
					//Drop One Special Item
					switch (Utility.Random(6))
					{
						case 0: cont.DropItem( new ArrowShaft() ); break;
						case 1: cont.DropItem( new ArrowShaft2() ); break;
						case 2: cont.DropItem( new Feathers() ); break;
						case 3: cont.DropItem( new Feathers2() ); break;
						case 4: cont.DropItem( new HoodHat() ); break;
						case 5: cont.DropItem( new HoodHat2() ); break;
					}
					
					//Drop Special Reagent
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new BlueDiamond() );  
					}
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Minotaur:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new Infamia() );
					
					//Drop One Special Item
					switch (Utility.Random(6))
					{
						case 0: cont.DropItem( new MinotaurShackles() ); break;
						case 1: cont.DropItem( new MinotaurShackles2() ); break;
						case 2: cont.DropItem( new MeatSkeleton() ); break;
						case 3: cont.DropItem( new MeatSkeleton2() ); break;
						case 4: cont.DropItem( new OpenBook() ); break;
						case 5: cont.DropItem( new OpenBook2() ); break;
					}
					
					//Drop Special Reagent
					/*for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new BlueDiamond() );  
					}*/
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.SkeletonDragon:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new Malati() );
					
					//Drop One Special Item
					switch (Utility.Random(8))
					{
						case 0: cont.DropItem( new SkellBlood() ); break;
						case 1: cont.DropItem( new SkellBlood2() ); break;
						case 2: cont.DropItem( new SkellBlood3() ); break;
						case 3: cont.DropItem( new Lockpicks3() ); break;
						case 4: cont.DropItem( new Lockpicks2() ); break;
						case 5: cont.DropItem( new EmptyToolBox() ); break;
						case 6: cont.DropItem( new TarotCards() ); break;
						case 7: cont.DropItem( new TarotCards2() ); break;
					}
					
					//Drop Special Reagent
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new Bone() );  
					}
					for ( int i = 0; i < level * 1; ++i )
					{
						cont.DropItem(new Moonstone(MoonstoneType.Felucca));
					}
					for ( int i = 0; i < level * 1; ++i )
					{
						cont.DropItem(new Moonstone(MoonstoneType.Trammel));
					}
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Titan:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new Pulp() );
					if ( Utility.RandomDouble() < 0.3 )
						cont.DropItem (new TreasureMap( 5, Map.Felucca) );
					if ( Utility.RandomDouble() < 0.3 )
						cont.DropItem (new TreasureMap( 5, Map.Felucca) );
					if ( Utility.RandomDouble() < 0.3 )
						cont.DropItem (new TreasureMap( 5, Map.Felucca) );   
					
					//Drop One Special Item
					switch (Utility.Random(6))
					{
						case 0: cont.DropItem( new BambooStool() ); break;
						case 1: cont.DropItem( new TapestryTitan() ); break;
						case 2: cont.DropItem( new TapestryTitan2() ); break;
						case 3: cont.DropItem( new WallMap() ); break;
						case 4: cont.DropItem( new WallMap2() ); break;
						case 5: cont.DropItem( new FruitBowlTitan() ); break;
					}
					
					//Drop Special Reagent
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Dead:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new DeathShadow() );
					
					//Drop One Special Item
					switch (Utility.Random(8))
					{
						case 0: cont.DropItem( new Blood4() ); break;
						case 1: cont.DropItem( new Blood5() ); break;
						case 2: cont.DropItem( new Blood6() ); break;
						case 3: cont.DropItem( new TwoPartBody() ); break;
						case 4: cont.DropItem( new TwoPartBody2() ); break;
						case 5: cont.DropItem( new TwoPartBody3() ); break;
						case 6: cont.DropItem( new PillowCorpse() ); break;
						case 7: cont.DropItem( new FoldedSheet() ); break;
					}
					
					//Drop Special Reagent
					for ( int i = 0; i < level * 5; ++i )
					{
						cont.DropItem (new Bone() );  
					}
					for ( int i = 0; i < level * 1; ++i )
					{
						cont.DropItem(new Moonstone(MoonstoneType.Felucca));
					}
					for ( int i = 0; i < level * 1; ++i )
					{
						cont.DropItem(new Moonstone(MoonstoneType.Trammel));
					}
					for ( int i = 0; i < level * 6; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Ice:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new Glacial2() );
					
					//Drop One Special Item
					switch (Utility.Random(10))
					{
						case 0: cont.DropItem( new Silverware() ); break;
						case 1: cont.DropItem( new Silverware2() ); break;
						case 2: cont.DropItem( new Silverware3() ); break;
						case 3: cont.DropItem( new Silverware4() ); break;
						case 4: cont.DropItem( new IceCrystals() ); break;
						case 5: cont.DropItem( new IceCrystals2() ); break;
						case 6: cont.DropItem( new PotOfWax() ); break;
						case 7: cont.DropItem( new PotOfWax2() ); break;
						case 8: cont.DropItem( new DippingSticks() ); break;
						case 9: cont.DropItem( new DippingSticks2() ); break;
					}
					
					//Drop Special Reagent
					
					for ( int i = 0; i < level * 6; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Shadow:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new MysticMayhem() );
					if ( Utility.RandomDouble() < 0.3 )
						cont.DropItem (new TreasureMap( 6, Map.Felucca) );
					if ( Utility.RandomDouble() < 0.3 )
						cont.DropItem (new TreasureMap( 6, Map.Felucca) );
					if ( Utility.RandomDouble() < 0.3 )
						cont.DropItem (new TreasureMap( 6, Map.Felucca) );    
					
					//Drop One Special Item
					switch (Utility.Random(6))
					{
						case 0: cont.DropItem( new EvilTotem() ); break;
						case 1: cont.DropItem( new EvilTotem2() ); break;
						case 2: cont.DropItem( new EvilTotem3() ); break;
						case 3: cont.DropItem( new EvilTotem4() ); break;
						case 4: cont.DropItem( new OpenBook5() ); break;
						case 5: cont.DropItem( new OpenBook4() ); break;
					}
					
					//Drop Special Reagent
					for ( int i = 0; i < level * 6; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Lich:
				{
					//Drop Special Weapon 50%
					if ( Utility.RandomDouble() < 0.3 )
					cont.DropItem(new Glacial2() );
					
					//Drop One Special Item
					switch (Utility.Random(8))
					{
						case 0: cont.DropItem( new GinsengDeco() ); break;
						case 1: cont.DropItem( new GinsengDeco2() ); break;
						case 2: cont.DropItem( new HourGlass() ); break;
						case 3: cont.DropItem( new GlowRune() ); break;
						case 4: cont.DropItem( new GlowRune2() ); break;
						case 5: cont.DropItem( new GlowRune3() ); break;
						case 6: cont.DropItem( new GlowRune4() ); break;
						case 7: cont.DropItem( new GlowRune5() ); break;
					}
					
					//Drop Special Reagent
					for ( int i = 0; i < level * 6; ++i )
					{
						cont.DropItem (new Bone() );  
					}
					for ( int i = 0; i < level * 1; ++i )
					{
						cont.DropItem(new Moonstone(MoonstoneType.Felucca));
					}
					for ( int i = 0; i < level * 1; ++i )
					{
						cont.DropItem(new Moonstone(MoonstoneType.Trammel));
					}
					for ( int i = 0; i < level * 6; ++i )
					{
						cont.DropItem (new Gold(50,250) );  
					}
					break;
				}
				case ChestThemeType.Lizardmen: 
				{
					if (Utility.RandomBool())
						cont.DropItem( new LizardmansStaff() ); 
					else
						cont.DropItem( new LizardmansMace() ); 
				}
					break;
				
				case ChestThemeType.Ettin:
				{
					cont.DropItem( new EttinHammer() ); 
				}
					break;
				
				case ChestThemeType.Ogre: 
				{
					cont.DropItem( new OgresClub() ); 
				}
					break;

				case ChestThemeType.Ophidian:
				{
					cont.DropItem( new OphidianBardiche() ); 
				}
					break;
				
				case ChestThemeType.Skeleton:
				{
					switch (Utility.Random(3))
					{
						case 0: cont.DropItem( new SkeletonScimitar() ); break;
						case 1: cont.DropItem( new SkeletonAxe() ); break;
						case 2: cont.DropItem( new BoneMageStaff() ); break;
					}
				}
					break;

				case ChestThemeType.Ratmen:
				{
					if (Utility.RandomBool())
						cont.DropItem( new RatmanSword() ); 
					else
						cont.DropItem( new RatmanAxe() ); 
				}
					break;

				case ChestThemeType.Orc:
				{
					switch (Utility.Random(3))
					{
						case 0: cont.DropItem( new OrcClub() ); break;
						case 1: cont.DropItem( new OrcMageStaff() ); break;
						case 2: cont.DropItem( new OrcLordBattleaxe() ); break;
					}
				}
					break;

				case ChestThemeType.Terathan:
				{
					switch (Utility.Random(3))
					{
						case 0: cont.DropItem( new TerathanStaff() ); break;
						case 1: cont.DropItem( new TerathanSpear() ); break;
						case 2: cont.DropItem( new TerathanMace() ); break;
					}
				}
					break;

				case ChestThemeType.FrostTroll:
				{
					switch (Utility.Random(3))
					{
						case 0: cont.DropItem( new FrostTrollClub() ); break;
						case 1: cont.DropItem( new TrollAxe() ); break;
						case 2: cont.DropItem( new TrollMaul() ); break;
					}
				}
					break;
			}
		}
		
		private static void ClearAmounts( int[] list )
		{
			for ( int i = 0; i < list.Length; ++i )
				list[i] = 0;
		}
		
        public override bool CheckLocked( Mobile from )
        {
            if ( !this.Locked )
                return false;

            if ( this.Level == 0 && from.AccessLevel < AccessLevel.GameMaster )
            {
                foreach ( Mobile m in this.Guardians )
                {
                    if ( m.Alive )
                    {
                        from.SendLocalizedMessage( 1046448 ); // You must first kill the guardians before you may open this chest.
                        return true;
                    }
                }

                LockPick( from );
                return false;
            }
            else
            {
                return base.CheckLocked( from );
            }
        }

        private List<Item> m_Lifted = new List<Item>();

        private bool CheckLoot( Mobile m, bool criminalAction )
        {
            if ( m_Temporary )
                return false;

            if ( m.AccessLevel >= AccessLevel.GameMaster || m_Owner == null || m == m_Owner )
                return true;

            Party p = Party.Get( m_Owner );

            if ( p != null && p.Contains( m ) )
                return true;

            Map map = this.Map;

            if ( map != null && (map.Rules & MapRules.HarmfulRestrictions) == 0 )
            {
                if ( criminalAction )
                    m.CriminalAction( true );
                else
                    m.SendLocalizedMessage( 1010630 ); // Taking someone else's treasure is a criminal offense!

                return true;
            }

            m.SendLocalizedMessage( 1010631 ); // You did not discover this chest!
            return false;
        }

        public override bool IsDecoContainer
        {
            get{ return false; }
        }

        public override bool CheckItemUse( Mobile from, Item item )
        {
            return CheckLoot( from, item != this ) && base.CheckItemUse( from, item );
        }

		public override bool CheckLift( Mobile from, Item item, ref LRReason reject )
		{
			return CheckLoot( from, true ) && base.CheckLift( from, item, ref reject );
		}

        public override void OnItemLifted( Mobile from, Item item )
        {
            if(m_Spawn) 
			{ //if it's allowed to spawn let it else don't.
                bool notYetLifted = !m_Lifted.Contains( item );

                from.RevealingAction();

                if ( notYetLifted )
                {
                    m_Lifted.Add( item );
					double chance = 0.1 * (Level * .5);				
					//Old Method
					/*if ( 0.1 >= Utility.RandomDouble() ) // 10% chance to spawn a new monster
						TreasureMap.Spawn( m_Level, GetWorldLocation(), Map, from, false );*/

					if (IsThemed == true && 0.1 >= Utility.RandomDouble() )
					{
						// 10% chance to spawn 2 monsters as is a Themed chest
						TreasureTheme.Spawn( m_Level, GetWorldLocation(), Map, from, IsThemed, m_type,false,false );
						TreasureTheme.Spawn( m_Level, GetWorldLocation(), Map, from, IsThemed, m_type,false,false );
					}
					else if (IsThemed == false && chance >= Utility.RandomDouble())
						// Otherwise spawn 1 monster
						TreasureTheme.Spawn( m_Level, GetWorldLocation(), Map, from, IsThemed, m_type,false,false );
                }
            }

            base.OnItemLifted( from, item );
        }

        public override void GetProperties( ObjectPropertyList list )
        {
			base.GetProperties( list );

			list.Add( "Level {0} Dug up by {1}", this.m_Level, m_Owner == null ? "Incognito" : m_Owner.Name );
            list.Add( " On {0}", m_DeleteTime );
        }
 		public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			if ( m.AccessLevel < AccessLevel.Player )
			{
				m.SendLocalizedMessage( 1048122, "", 0x8A5 ); // The chest refuses to be filled with treasure again.
				return false;
			}
 
			return base.CheckHold( m, item, message, checkItems, plusItems, plusWeight );
		}
		/*Treasure Map Chest Change*/
/*        public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
*        {
*            if ( m.AccessLevel < AccessLevel.GameMaster )
*            {
*                m.SendLocalizedMessage( 1048122, "", 0x8A5 ); // The chest refuses to be filled with treasure again.
*                return false;
*            }
*
*            return base.CheckHold( m, item, message, checkItems, plusItems, plusWeight );
*        }
*/
        public TreasureMapChest( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 4 ); // version

			writer.Write(IsThemed);
			writer.Write((int)m_type);
            writer.Write( (bool)m_Spawn);
			
            writer.Write( m_Guardians, true );
            writer.Write( (bool) m_Temporary );

            writer.Write( m_Owner );

            writer.Write( (int) m_Level );
            writer.WriteDeltaTime( m_DeleteTime );
            writer.Write( m_Lifted, true );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            switch ( version )
            {
				case 4:
				{
					IsThemed = reader.ReadBool();
					m_type = (ChestThemeType)reader.ReadInt();
					goto case 3;
				}
                case 3:
                {
                    m_Spawn = reader.ReadBool();
                    goto case 2;
                }
                case 2:
                {
                    if(m_Spawn == null)
                    {
                        m_Spawn = false;
                    }
                    m_Guardians = reader.ReadStrongMobileList();
                    m_Temporary = reader.ReadBool();

                    goto case 1;
                }
                case 1:
                {
                    m_Owner = reader.ReadMobile();

                    goto case 0;
                }
                case 0:
                {
                    m_Level = reader.ReadInt();
                    m_DeleteTime = reader.ReadDeltaTime();
                    m_Lifted = reader.ReadStrongItemList();

                    if ( version < 2 )
                        m_Guardians = new List<Mobile>();

                    break;
                }
            }

            if ( !m_Temporary )
            {
                m_Timer = new DeleteTimer( this, m_DeleteTime );
                m_Timer.Start();
            }
            else
            {
                Delete();
            }
        }
		/*Treasure Map Chest Change*/
/*        public override void OnAfterDelete()
*        {
*            if ( m_Timer != null )
*                m_Timer.Stop();
*                m_Owner.SendMessage(38,"THE TIMER HAS STOPED");
*
*            m_Timer = null;
*
*            base.OnAfterDelete();
*        }
*/
        public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
        {
            base.GetContextMenuEntries( from, list );

            if ( from.Alive )
                list.Add( new RemoveEntry( from, this ) );
        }

        public void BeginRemove( Mobile from )
        {
            if ( !from.Alive )
                return;
            this.EndRemove( from );
            if ( m_Timer != null )
                m_Timer.Stop();
		/*Treasure Map Chest Change*/
                //m_Owner.SendMessage(38,"THE TIMER HAS STOPED");

            m_Timer = null;
		/*Treasure Map Chest Change*/
        //    from.CloseGump( typeof( RemoveGump ) );
        //    from.SendGump( new RemoveGump( from, this ) );
        }

        public void EndRemove( Mobile from )
        {
            if ( Deleted || from != m_Owner || !from.InRange( GetWorldLocation(), 3 ) )
                return;
		/*Treasure Map Chest Change*/
            //from.SendLocalizedMessage( 1048124, "", 0x8A5 ); // The old, rusted chest crumbles when you hit it.
            //this.Delete();
            from.SendMessage(0x8A5, "You unbind the old chest from the ground");
            this.Movable = true;

            this.m_Spawn = false; //the chest has been removed do not let any more monsters spawn.
        }
		/*Treasure Map Chest Change*/
/*        private class RemoveGump : Gump
*        {
*            private Mobile m_From;
*            private TreasureMapChest m_Chest;
*            public RemoveGump( Mobile from, TreasureMapChest chest ) : base( 15, 15 )
*            {
*                m_From = from;
*                m_Chest = chest;
*
*                Closable = false;
*                Disposable = false;
*                AddPage( 0 );
*
*                AddBackground( 30, 0, 240, 240, 2620 );
*
*                AddHtmlLocalized( 45, 15, 200, 80, 1048125, 0xFFFFFF, false, false ); // When this treasure chest is removed, any items still inside of it will be lost.
*                AddHtmlLocalized( 45, 95, 200, 60, 1048126, 0xFFFFFF, false, false ); // Are you certain you're ready to remove this chest?
*
*                AddButton( 40, 153, 4005, 4007, 1, GumpButtonType.Reply, 0 );
*                AddHtmlLocalized( 75, 155, 180, 40, 1048127, 0xFFFFFF, false, false ); // Remove the Treasure Chest
*
*                AddButton( 40, 195, 4005, 4007, 2, GumpButtonType.Reply, 0 );
*                AddHtmlLocalized( 75, 197, 180, 35, 1006045, 0xFFFFFF, false, false ); // Cancel
*            }
*
*            public override void OnResponse( NetState sender, RelayInfo info )
*            {
*                if ( info.ButtonID == 1 )
*                    m_Chest.EndRemove( m_From );
*            }
*        }
*/
        public static bool InHouse( Mobile from )
        {
            BaseHouse house = BaseHouse.FindHouseAt( from );
		/*Treasure Map Chest Change*/
            return ( house != null /*&& house.IsCoOwner( from )*/ );
        }

        private class RemoveEntry : ContextMenuEntry
        {
            private Mobile m_From;
            private TreasureMapChest m_Chest;

            public RemoveEntry( Mobile from, TreasureMapChest chest ) : base( 6149, 3 )
            {
                m_From = from;
                m_Chest = chest;

                Enabled = ( from == chest.Owner );
            }

            public override void OnClick()
            {
		/*Treasure Map Chest Change*/
                //if ( InHouse( from ) )
                    //break;
                if ( m_Chest.Deleted || m_From != m_Chest.Owner || !m_From.CheckAlive() || InHouse( m_From ) )
                    return;

                m_Chest.BeginRemove( m_From );
            }
        }

        private class DeleteTimer : Timer
        {
            private Item m_Item;

            public DeleteTimer( Item item, DateTime time ) : base( time - DateTime.Now )
            {
                m_Item = item;
                Priority = TimerPriority.OneMinute;
            }
            protected override void OnTick()
            {
			/*Treasure Map Chest Change*/
            //    m_Item.Delete();
            }
        }
    }
}
