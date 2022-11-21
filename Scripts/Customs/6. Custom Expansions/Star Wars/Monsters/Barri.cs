using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a Barri corpse" )]
	public class Barri : BaseCreature
	{
		[Constructable]
		public Barri() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Barri";
			Body = 21; 
            Hue = 1472;
			SetStr( 423, 506 );
			SetDex( 231, 258 );
			SetInt( 130, 135 );
			SetHits( 697, 715 );
			SetStam( 231, 258 );

			SetDamage( 16, 22 );
			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 45, 60 );
			SetResistance( ResistanceType.Cold, 85, 90 );
			SetResistance( ResistanceType.Poison, 100, 100 );
			SetResistance( ResistanceType.Energy, 35, 47 );
			SetSkill( SkillName.MagicResist, 95.4, 110.0 );
			SetSkill( SkillName.Tactics, 95.5, 115.0 );
			SetSkill( SkillName.Wrestling, 95.4, 110.0 );
			Tamable = false;
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if (Utility.RandomDouble() < 0.05)
			{
				switch ( Utility.Random( 45 ) )
				{
					case 0: c.DropItem( new AllyaExileDeed() ); break;
					case 1: c.DropItem( new AllyaRedemptionDeed() ); break;
					case 2: c.DropItem( new AnkarresDeed() ); break;
					case 3: c.DropItem( new BaasDeed() ); break;
					case 4: c.DropItem( new BarabDeed() ); break;
					case 5: c.DropItem( new BlackwingDeed() ); break;
					case 6: c.DropItem( new BondaraDeed() ); break;
					case 7: c.DropItem( new BondarDeed() ); break;
					case 8: c.DropItem( new DamindDeed() ); break;
					case 9: c.DropItem( new DODDeed() ); break;
					case 10: c.DropItem( new DragiteDeed() ); break;
					case 11: c.DropItem( new DurindfireDeed() ); break;
					case 12: c.DropItem( new EralamDeed() ); break;
					case 13: c.DropItem( new GreenAdeganDeed() ); break;
					case 14: c.DropItem( new HeartDeed() ); break;
					case 15: c.DropItem( new HurrikaineDeed() ); break;
					case 16: c.DropItem( new ImpactDeed() ); break;
					case 17: c.DropItem( new JenruaxDeed() ); break;
					case 18: c.DropItem( new KenobiDeed() ); break;
					case 19: c.DropItem( new KraytDeed() ); break;
					case 20: c.DropItem( new LambentDeed() ); break;
					case 21: c.DropItem( new LavaDeed() ); break;
					case 22: c.DropItem( new LignanDeed() ); break;
					case 23: c.DropItem( new LorridianDeed() ); break;
					case 24: c.DropItem( new MantleDeed() ); break;
					case 25: c.DropItem( new MeditationDeed() ); break;
					case 26: c.DropItem( new NextorDeed() ); break;
					case 27: c.DropItem( new PermafrostDeed() ); break;
					case 28: c.DropItem( new PhondDeed() ); break;
					case 29: c.DropItem( new QixoniDeed() ); break;
					case 30: c.DropItem( new RubatDeed() ); break;
					case 31: c.DropItem( new RuusanDeed() ); break;
					case 32: c.DropItem( new SapithDeed() ); break;
					case 33: c.DropItem( new SigilDeed() ); break;
					case 34: c.DropItem( new SolariDeed() ); break;
					case 35: c.DropItem( new StygiumDeed() ); break;
					case 36: c.DropItem( new SunriderDeed() ); break;
					case 37: c.DropItem( new SyntheticDeed() ); break;
					case 38: c.DropItem( new TyranusDeed() ); break;
					case 39: c.DropItem( new UlricRedemptionDeed() ); break;
					case 40: c.DropItem( new UltimaDeed() ); break;
					case 41: c.DropItem( new UpariDeed() ); break;
					case 42: c.DropItem( new VelmoriteDeed() ); break;
					case 43: c.DropItem( new VexxtalDeed() ); break;
					case 44: c.DropItem( new WinduDeed () ); break;
				}
			}
        }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}
		
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int Meat{ get{ return 6; } }
		public override int DragonBlood{ get{ return 6; } }
		public override int Hides{ get{ return 11; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        public override Poison HitPoison { get { return Poison.Lethal; } }

		public Barri( Serial serial ) : base( serial )
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