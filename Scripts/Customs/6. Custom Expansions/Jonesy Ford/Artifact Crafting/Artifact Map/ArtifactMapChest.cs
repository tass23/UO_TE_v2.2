using System;
using Server;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.PartySystem;
using System.Collections.Generic;

namespace Server.Items
{
    public class ArtifactMapChest : LockableContainer
    {
		public virtual double ArtifactMapChestChance { get { return ArtifactMapChest.LootChance; } }
		public const double LootChance = 0.9; // 90% chance to appear as loot
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

        private int m_ArtifactLevel;
        private DateTime m_DeleteTime;
        private Timer m_Timer;
        private Mobile m_Owner;
        private bool m_Temporary;
		private int m_Hue;
        private bool m_Spawn; // bool variable to stop the spawning of mobiles after it's been taken by a player.
        private List<Mobile> m_Guardians;

        [CommandProperty( AccessLevel.GameMaster )]
        public int ArtifactLevel{ get{ return m_ArtifactLevel; } set{ m_ArtifactLevel = value; } }
        [CommandProperty( AccessLevel.GameMaster )]
        public Mobile Owner{ get{ return m_Owner; } set{ m_Owner = value; } }
        [CommandProperty( AccessLevel.GameMaster )]
        public DateTime DeleteTime{ get{ return m_DeleteTime; } }
        [CommandProperty( AccessLevel.GameMaster )]
        public bool Temporary{ get{ return m_Temporary; } set{ m_Temporary = value; } }
        public List<Mobile> Guardians { get { return m_Guardians; } }

        [Constructable]
        public ArtifactMapChest( int level ) : this( null, level, false )
        {
        }

        public ArtifactMapChest( Mobile owner, int level, bool temporary ) : base( 0xE40 )
        {
            m_Owner = owner;
            m_ArtifactLevel = level;
            m_DeleteTime = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
            m_Temporary = temporary;
			Hue = Utility.RandomList( m_Hues );
            m_Guardians = new List<Mobile>();
            m_Timer = new DeleteTimer( this, m_DeleteTime );
            m_Timer.Start();
            m_Spawn = true; //allow the spawning of gardians
            Fill( this, level );
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

        public static void Fill( LockableContainer cont, int level )
        {
            cont.Movable = false;
            cont.Locked = true;

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
                cont.DropItem( new Gold( level * 1000 ) );

                for ( int i = 0; i < level * 5; ++i )
                    cont.DropItem( Loot.RandomScroll( 0, 63, SpellbookType.Regular ) );

                for ( int i = 0; i < level * 6; ++i )
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

				if ( Utility.RandomDouble() < .15 )
					cont.DropItem( new RewardScroll() );

			if ( level == 6 )
			{
				cont.DropItem( new BluePrint2() );
			}
			else
			{
				cont.DropItem( new BluePrint() );
			}

			cont.DropItem( new ArtifactMap( level + 1, ( Utility.RandomBool() ? Map.Tokuno : Map.Tokuno ) ) );
        }

        public override bool CheckLocked( Mobile from )
        {
            if ( !this.Locked )
                return false;

            if ( this.ArtifactLevel == 0 && from.AccessLevel < AccessLevel.GameMaster )
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

        public override void OnItemLifted( Mobile from, Item item )
        {
            if(m_Spawn) { //if it's allowed to spawn let it else don't.
                bool notYetLifted = !m_Lifted.Contains( item );
                from.RevealingAction();

                if ( notYetLifted )
                {
                    m_Lifted.Add( item );

                    if ( 0.1 >= Utility.RandomDouble() ) // 10% chance to spawn a new monster
                        ArtifactMap.Spawn( m_ArtifactLevel, GetWorldLocation(), Map, from, false );
                }
            }

            base.OnItemLifted( from, item );
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );
			list.Add( " Artifact Map: Level {0} Dug up by {1}", this.m_ArtifactLevel, m_Owner == null ? "Incognito" : m_Owner.Name );
            list.Add( " On {0}", m_DeleteTime );
        }

        public ArtifactMapChest( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int) 3 ); // version

            writer.Write( (bool)m_Spawn);
            writer.Write( m_Guardians, true );
            writer.Write( (bool) m_Temporary );
            writer.Write( m_Owner );
            writer.Write( (int) m_ArtifactLevel );
            writer.WriteDeltaTime( m_DeleteTime );
            writer.Write( m_Lifted, true );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();

            switch ( version )
            {
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
                    m_ArtifactLevel = reader.ReadInt();
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

            m_Timer = null;
        }

        public void EndRemove( Mobile from )
        {
            if ( Deleted || from != m_Owner || !from.InRange( GetWorldLocation(), 3 ) )
                return;

            from.SendMessage(0x8A5, "You unbind the old chest from the ground");
            this.Movable = true;

            this.m_Spawn = false; //the chest has been removed do not let any more monsters spawn.
        }

        public static bool InHouse( Mobile from )
        {
            BaseHouse house = BaseHouse.FindHouseAt( from );
            return ( house != null /*&& house.IsCoOwner( from )*/ );
        }

        private class RemoveEntry : ContextMenuEntry
        {
            private Mobile m_From;
            private ArtifactMapChest m_Chest;

            public RemoveEntry( Mobile from, ArtifactMapChest chest ) : base( 6149, 3 )
            {
                m_From = from;
                m_Chest = chest;
                Enabled = ( from == chest.Owner );
            }

            public override void OnClick()
            {
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
            }
        }
    }
}