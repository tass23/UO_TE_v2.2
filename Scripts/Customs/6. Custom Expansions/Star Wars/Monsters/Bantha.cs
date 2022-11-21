using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using System.Collections;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a bantha corpse" )]
	public class Bantha: BaseCreature
	{
		private bool m_Stunning;

		[Constructable]
		public Bantha() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a bantha";
			Body = 715;
			Hue = 1772;
			SetStr( 696, 780 );
			SetDex( 268, 282 );
			SetInt( 216, 220 );
			SetHits( 1435, 1509 );
			SetStam( 268, 282 );
			SetMana( 116, 120 );

			SetDamage( 12, 18 );
			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 85, 95 );
			SetResistance( ResistanceType.Fire, 35, 40 );
			SetResistance( ResistanceType.Cold, 85, 95 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );
			SetSkill( SkillName.Anatomy, 86.6, 88.8 );
			SetSkill( SkillName.MagicResist, 69.7, 87.7 );
			SetSkill( SkillName.Tactics, 83.3, 88.8 );
			SetSkill( SkillName.Wrestling, 86.6, 87.9 );
			Tamable = false;
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if (Utility.RandomDouble() < 0.15)
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
		
		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 20; } }
        public override int DragonBlood { get { return 8; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }
		public override int GetIdleSound() { return 1507; } 
		public override int GetAngerSound() { return 1504; } 
		public override int GetHurtSound() { return 1506; } 
		public override int GetDeathSound()	{ return 1505; }

		public Bantha( Serial serial ) : base( serial )
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