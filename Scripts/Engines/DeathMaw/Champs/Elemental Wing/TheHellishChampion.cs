using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Fourth;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using Server.Engines.DMChamps;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a hellish champion corpse" )]
	public class TheHellishChampion : BaseDMChampion
	{
        public override Type[] UniqueArtifacts{ get{ return new Type[] { typeof(DeathsHead), typeof(WallofHungryMouths), typeof(AbyssalBlade)}; } }

        public override Type[] SharedArtifacts{ get { return new Type[] { }; } }

        public override Type[] DecorationArtifacts{ get { return new Type[] { }; } }
		
        public override Type[] TokenArtifacts{ get { return new Type[] {
            typeof( DeathMawSpiderToken ),
			typeof( DeathMawUnholyToken ),
			typeof( DeathMawDragonToken ),
			typeof( DeathMawFeyToken ),
			typeof( DeathMawDaemonToken ) }; } }
		
        public override MonsterStatuetteType[] StatueTypes { get { return new MonsterStatuetteType[] { }; } }

		[Constructable]
		public TheHellishChampion() : base( AIType.AI_Mage )
		{
            Body = 713;// 730;
			Name = "The Hellish Champion";
			Hue = 1256;

			SetStr( 701, 900 );
			SetDex( 201, 350 );
			SetInt( 51, 100 );

			SetHits( 25000 );
			SetStam( 203, 650 );

			SetDamage( 28, 35 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Fire, 50 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Energy, 80, 90 );

			SetSkill( SkillName.Anatomy, 100.0 );
			SetSkill( SkillName.MagicResist, 140.2, 160.0 );
			SetSkill( SkillName.Tactics, 100.0 );

			Fame = 2250;
			Karma = -2250;

			VirtualArmor = 40;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 4 );
		}

        public override bool AlwaysMurderer { get { return true; } }
        public override bool BardImmune { get { return !Core.SE; } }
        public override bool Unprovokable { get { return Core.SE; } }
        public override bool Uncalmable { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override ScaleType ScaleType{ get{ return ScaleType.All; } }
		public override int Scales{ get{ return 20; } }


		public override int GetIdleSound() { return 1495; } 
		public override int GetAngerSound() { return 1492; } 
		public override int GetHurtSound() { return 1494; } 
		public override int GetDeathSound()	{ return 1493; }

        public void SpawnFollowers(Mobile target)
        {
            Map map = this.Map;

            if (map == null)
                return;

            int followers = 0;

            foreach (Mobile m in this.GetMobilesInRange(10))
            {
                if (m is EarthElemental || m is FireElemental || m is AirElemental || m is FireSteed || m is WaterElemental )
                    ++followers;
            }

            if ( Followers < 9)
            {
                PlaySound(0x218);//////////

                int newFollowers = Utility.RandomMinMax(1, 2);

                for (int i = 0; i < newFollowers; ++i)
                {
                    BaseCreature follower;

                    switch (Utility.Random(5))
                    {
                        default:
                        case 0: follower = new WaterElemental(); break;
                        case 1: follower = new EarthElemental(); break;
                        case 2: follower = new FireSteed(); break;
                        case 3: follower = new FireElemental(); break;
                        case 4: follower = new AirElemental(); break;
                    }

                    follower.Team = this.Team;

                    bool validLocation = false;
                    Point3D loc = this.Location;

                    for (int j = 0; !validLocation && j < 9; ++j)
                    {
                        int x = X + Utility.Random(3) - 1;
                        int y = Y + Utility.Random(3) - 1;
                        int z = map.GetAverageZ(x, y);

                        if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                            loc = new Point3D(x, y, Z);
                        else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                            loc = new Point3D(x, y, z);
                    }

                    follower.MoveToWorld(loc, map);
                    follower.Combatant = target;
                }
            }
        }

        public void DoSpecialAbility(Mobile target)
        {
            if (target == null || target.Deleted) //sanity
                return;        

            if (0.05 >= Utility.RandomDouble()) // 20% chance to more ratmen
                SpawnFollowers(target);           
        }

        private DateTime m_Delay;

        public override void OnActionCombat()
        {
            if (DateTime.Now > m_Delay)
            {

                Ability.CrimsonMeteor(this, 20);
                m_Delay = DateTime.Now + TimeSpan.FromSeconds(Utility.RandomMinMax(45, 55));
            }

            base.OnActionCombat();
        }

		public TheHellishChampion( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}