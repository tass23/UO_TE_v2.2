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
	[CorpseName( "an abyssal infernal corpse" )]
	public class AbyssalInfernal2 : BaseDMChampion
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
		public AbyssalInfernal2() : base( AIType.AI_Mage )
		{
            Body = 713;// 730;
			Name = "The Abyssal Infernal";

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

        public void DrainLife()
        {
            if (this.Map == null)
                return;

            ArrayList list = new ArrayList();

            foreach (Mobile m in this.GetMobilesInRange(8))
            {
                if (m == this || !CanBeHarmful(m))
                    continue;

                if (m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team))
                    list.Add(m);
                else if (m.Player)
                    list.Add(m);
            }

            foreach (Mobile m in list)
            {
                DoHarmful(m);

                m.FixedParticles(0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist);
                m.PlaySound(0x231);////////////////

                m.SendMessage("You feel the life drain out of you!");

                int toDrain = Utility.RandomMinMax(25, 30);

                Hits += toDrain;
                m.Damage(toDrain, this);
            }
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (0.25 >= Utility.RandomDouble())
                DrainLife();
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            if (0.25 >= Utility.RandomDouble())
                DrainLife();
        }

        private DateTime m_Delay;

		public AbyssalInfernal2( Serial serial ) : base( serial )
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