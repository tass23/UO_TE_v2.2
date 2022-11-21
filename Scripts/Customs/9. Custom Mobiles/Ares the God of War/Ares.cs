using System;
using System.Collections;
using Server.Items;
 
namespace Server.Mobiles
{
    [CorpseName( "a godly corpse" )]
    public class Ares : BaseCreature
    {
        private DateTime _nextabil;
        private DateTime _nextspawn;
        private int _stage;
        private int _kills;
        private int _minibosskills;
        private ArrayList spawns;
        private Item _chair;
        private int _hits;
       
        [Constructable]
        public Ares() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
        {
            Female = false;
            _stage = 0;
            _kills = 0;
            _minibosskills = 0;
            spawns = new ArrayList();
            Name = "Ares";
            Body = 400;
 
            SetStr( 400 );
            SetDex( 104, 260 );
            SetInt( 91, 100 );
 
            SetHits( 100000 );
            _hits = Hits;
 
            SetDamage( 30, 50 );
 
            SetDamageType( ResistanceType.Physical, 100 );
 
            SetResistance( ResistanceType.Physical, 100 );
            SetResistance( ResistanceType.Fire, -10 );
            SetResistance( ResistanceType.Cold, -10 );
            SetResistance( ResistanceType.Poison, -10 );
            SetResistance( ResistanceType.Energy, -10 );
 
            SetSkill( SkillName.Anatomy, 300.0 );
            SetSkill( SkillName.Swords, 300.0 );
            SetSkill( SkillName.MagicResist, 50.3, 80.0 );
            SetSkill( SkillName.Tactics, 300.0 );
            SetSkill( SkillName.ArmsLore, 300.0 );
 
			Fame = 1000;
			Karma = -1000;
 
            VirtualArmor = 50;
            HairItemID = 8252;
            HairHue = 1174;
 
            AddItem( new BladeOfAres() );
            AddItem( new AresChest() );
            AddItem( new AresArms() );
            AddItem( new AresGloves() );
            AddItem( new AresCloak() );
            AddItem( new AresLegs() );
            AddItem( new AresBoots() );
            CantWalk = true;
        }
 
        public override bool AlwaysMurderer{ get{ return true; }}
 
        public override void OnThink()
        {
            if ( _stage == 0 )
                Stage1();
            if ( _nextabil <= DateTime.UtcNow )
            {
                _nextabil = DateTime.UtcNow + TimeSpan.FromSeconds(30.0);
                DoSpecialAttack();
            }
 
            if (_nextspawn <= DateTime.UtcNow && spawns.Count < 30)
            {
                _nextspawn = DateTime.UtcNow + TimeSpan.FromMinutes(1.0);
                DoSpawn();
            }
 
            if ( Hits > _hits )
                Hits = _hits;
 
            _hits = Hits;
 
            base.OnThink();
        }
 
        public void DoSpecialAttack()
        {
            ArrayList mlist = new ArrayList();
            IPooledEnumerable eable = Map.GetMobilesInRange( Location, 10 );
            foreach( Mobile m in eable )
                mlist.Add( m );
            eable.Free();
            if ( mlist != null && mlist.Count > 0 )
            {
                foreach (object t in mlist)
                {
                    Mobile m = (Mobile)t;
                    if ( m is PlayerMobile )
                    {
                        AOS.Damage( m, this, Utility.Random( 40, 60 ), 0, 100, 0, 0, 0 );
                        m.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot );
                        Say( "Ha ha ha!" );
                    }
                }
            }
        }
 
        public void AddKill()
        {
            if ( _kills < 100 )
            {
                _kills += 1;
                if ( _kills >= 100 )
                    Stage2();
            }
            else if ( _stage == 2 )
            {
                _minibosskills += 1;
                if ( _minibosskills >= 4 )
                    Stage3();
            }
        }
 
        public void DoSpawn()
        {
            if ( _stage == 1 && spawns.Count < 30 )
            {
                int chance = Utility.Random( 1, 3 );
                if ( chance == 1 )
                {
                    WarTroll troll = new WarTroll( this );
                    troll.Home = new Point3D( X, Y, Z - 20 );
                    troll.RangeHome = 30;
                    troll.MoveToWorld( troll.Home, Map );
                    spawns.Add(troll);
                }
                if ( chance == 2 )
                {
                    WarSpirit troll = new WarSpirit( this );
                    troll.Home = new Point3D( X, Y, Z - 20 );
                    troll.RangeHome = 30;
                    troll.MoveToWorld( troll.Home, Map );
                    spawns.Add(troll);
                }
                if ( chance == 3 )
                {
                    WarMonger troll = new WarMonger( this );
                    troll.Home = new Point3D( X, Y, Z - 20 );
                    troll.RangeHome = 30;
                    troll.MoveToWorld( troll.Home, Map );
                    spawns.Add(troll);
                }
            }
            else if ( _stage == 2 && spawns.Count < 4 )
            {
                WarMonger troll = new WarMonger( this );
                troll.Name = "Spawn of Ares";
                troll.HitsMaxSeed += 5000;
                troll.Hits += 5000;
                troll.DamageMin += 20;
                troll.DamageMax += 20;
                troll.Home = new Point3D( X, Y, Z - 20 );
                troll.RangeHome = 30;
                troll.MoveToWorld( troll.Home, Map );
                spawns.Add(troll);
            }              
        }
 
        public void Stage1()
        {
            Frozen = true;
            _stage = 1;
            StoneChair chair1 = new StoneChair();
            chair1.Movable = false;
            chair1.Hue = 2949;
            _chair = chair1;
            chair1.MoveToWorld( Location, Map );
            Z += 20;
            chair1.Z += 20;
            Blessed = true;
            DoSpawn();
            DoSpawn();
            DoSpawn();
            DoSpawn();
            DoSpawn();
            DoSpawn();
            DoSpawn();
            DoSpawn();
            DoSpawn();
        }
 
        public void Stage2()
        {
            _stage = 2;
            Say( "Come forth, my children!" );
            if ( spawns.Count > 0 )
            {
                foreach( Mobile m in spawns )
                    m.Delete();
            }
            DoSpawn();
        }
 
        public void Stage3()
        {
            Frozen = false;
            Blessed = false;
            _stage = 3;
            if ( _chair != null )
                _chair.Delete();
            CantWalk = false;
            Say( "FACE MY WRATH!" );
            new DescendTimer( this, 0 ).Start();
        }
           
 
        public override void GenerateLoot()
        {
            AddLoot( LootPack.SuperBoss, 3 );
            AddLoot( LootPack.Gems );
            AddLoot( LootPack.Gems );
            AddLoot( LootPack.Gems );
        }
 
        public Ares( Serial serial ) : base( serial )
        {
        }
 
        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int) 0 );
            writer.Write( (int) _stage );
            writer.Write( (int) _kills );
            writer.Write( (int) _minibosskills );
            writer.Write( (Item) _chair );
            writer.WriteMobileList(spawns, true);
 
        }
 
        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
            _stage = reader.ReadInt();
            _kills = reader.ReadInt();
            _minibosskills = reader.ReadInt();
            _chair = reader.ReadItem();
            spawns=reader.ReadMobileList();
 
 
        }
 
        private class DescendTimer : Timer
        {
            private Mobile m;
            private int count;
            public DescendTimer( Mobile from, int c ) : base( TimeSpan.FromSeconds( 0.1 ))
            {
                m = from;
                count = c;
            }
            protected override void OnTick()
            {
                if ( m != null && count < 20 )
                {
                    count += 1;
                    m.Z -= 1;
                    new DescendTimer( m, count ).Start();
                }
            }
        }
    }
}