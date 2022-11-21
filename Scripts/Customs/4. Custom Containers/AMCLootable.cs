using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.PartySystem;

namespace Server.Items
{
    public class AMCLootable : LockableContainer
    {
		public virtual double AMCLootableChance { get { return AMCLootable.LootChance; } }
		public const double LootChance = 0.9; // 90% chance to appear as loot

		private static int[] m_ItemIDs = new int[]
		{
			0x9AB, 0xE40, 0xE41, 0xE7C
		};
		
		public override int LabelNumber{ get{ return 1149868; } }
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

		public string m_Recovered;
        public int m_ArtifactLevel;
        private DateTime m_DeleteTime;
		private int m_Hue;
		public string m_Dname;

		[CommandProperty(AccessLevel.GameMaster)]
		public string Recovered
		{
			get { return m_Recovered; }
			set { m_Recovered = value; }
		}
		[CommandProperty(AccessLevel.GameMaster)]
		public string Dname
		{
			get { return m_Dname; }
			set
			{
				m_Dname = value;
				Name = m_Dname;
			}
		}
        [CommandProperty( AccessLevel.GameMaster )]
        public int ArtifactLevel{ get{ return m_ArtifactLevel; } set{ m_ArtifactLevel = value; InvalidateProperties(); } }

        [CommandProperty( AccessLevel.GameMaster )]
        public DateTime DeleteTime{ get{ return m_DeleteTime; } }

		[Constructable]
        public AMCLootable( int artifactlevel ) : base( Utility.RandomList( m_ItemIDs ) )
        {
            m_ArtifactLevel = artifactlevel;
			if ( m_Dname == null )
				m_Dname = NameList.RandomName( "daemon" );
			else
				m_Dname = "";
			m_DeleteTime = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			Hue = Utility.RandomList( m_Hues );
			Movable = true;
            Fill( this, artifactlevel );
        }
		
		private static int[] m_Hues = new int[]
		{
			1068,
			1070,
			1078,
			1151,
			1154,
			1155,
			1158,
			1159,
			1160,
			1165,
			1170,
			1195,
			1295,
			1398,
			1464,
			1573,
			1574
		};

		public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );
			
			//list.Add(1042971, this.Name);

			list.Add( " Artifact Map Chest: Level {0} stolen by {1}", this.m_ArtifactLevel, Dname );
            list.Add( " On {0}", m_DeleteTime );
        }

        private static void GetRandomAOSStats( out int attributeCount, out int min, out int max )
        {
            int rnd = Utility.Random( 15 );

            if ( rnd < 1 )
            {
                attributeCount = Utility.RandomMinMax( 2, 6 );
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

        public static void Fill( LockableContainer cont, int artifactlevel )
        {
            cont.Locked = true;

            if ( artifactlevel == 0 )
            {
                cont.LockLevel = 0; // Can't be unlocked

                if ( Utility.RandomDouble() < 0.75 )
                    cont.DropItem( new TreasureMap( 0, Map.Trammel ) );
            }
            else
            {
                cont.TrapType = TrapType.ExplosionTrap;
                cont.TrapPower = artifactlevel * 25;
                cont.TrapLevel = artifactlevel;

                switch ( artifactlevel )
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

                for ( int i = 0; i < artifactlevel * 3; ++i )
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

            int gems;
            if ( artifactlevel == 0 )
                gems = 2;
            else
                gems = artifactlevel * 3;

            for ( int i = 0; i < gems; i++ )
            {
                Item item = Loot.RandomGem();
                cont.DropItem( item );
            }

			if ( artifactlevel == 6 && Core.AOS )
                cont.DropItem( (Item)Activator.CreateInstance( m_Artifacts[Utility.Random(m_Artifacts.Length)] ) );
				
			if ( artifactlevel == 6 )
			{
				cont.DropItem( new BluePrint2() );
				cont.DropItem( new BluePrint() );
				cont.DropItem( new BluePrint() );
				cont.DropItem( new BluePrint() );
				if ( Utility.RandomDouble() < .15 )
				{
					cont.DropItem( new BluePrint() );
					cont.DropItem( new BluePrint() );
					cont.DropItem( new BluePrint() );
					cont.DropItem( new RewardScroll(1, 20) );
				}
			}
			else
			{
				cont.DropItem( new BluePrint2() );
				cont.DropItem( new BluePrint() );
				cont.DropItem( new BluePrint() );
			}
			cont.DropItem( new BankCheck( Utility.Random( 2500, 5000 )));
			cont.DropItem( new RewardScroll( artifactlevel * Utility.Random( 75, 100 )) );
			cont.DropItem( new BluePrint() );
			cont.DropItem( new ArtifactMap( artifactlevel + 1, ( Utility.RandomBool() ? Map.Tokuno : Map.Tokuno ) ) );
        }

        public override bool CheckLocked( Mobile from )
        {
            if ( !this.Locked )
                return false;

            if ( this.ArtifactLevel == 0 && from.AccessLevel < AccessLevel.GameMaster )
            {
                LockPick( from );
                return false;
            }
            else
            {
                return base.CheckLocked( from );
            }
        }

        public override bool IsDecoContainer
        {
            get{ return false; }
        }

        public AMCLootable( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 0 ); // version
			writer.Write(m_Dname);
            writer.Write( m_Recovered );

            writer.Write( (int) m_ArtifactLevel );
            writer.WriteDeltaTime( m_DeleteTime );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

			m_Dname = Utility.Intern( reader.ReadString() );
			m_Recovered = reader.ReadString();
            m_ArtifactLevel = reader.ReadInt();
            m_DeleteTime = reader.ReadDeltaTime();
        }
    }
}