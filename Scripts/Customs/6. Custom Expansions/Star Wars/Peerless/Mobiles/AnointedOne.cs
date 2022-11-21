using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.ACC.CSS.Systems.DarkForce;
using Server.ACC.CSS.Systems.LightForce;

namespace Server.Mobiles
{
	[CorpseName( "a Disciple of Ragnos's corpse" )]
	public class AnointedOne : BaseCreature
	{
		[Constructable]
		public AnointedOne() : base( AIType.AI_Sith, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Disciple of Ragnos";
			Body = 400;
			BaseSoundID = 451;
			Hue = 971;
			SetStr( 95, 100 );
			SetDex( 250, 300 );
			SetInt( 175, 200 );
			SetHits( 592, 711 );

			SetDamage( 10, 17 );
			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Cold, 60 );
			SetResistance( ResistanceType.Physical, 85, 90 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 40, 50 );
			SetSkill( SkillName.Swords, 119.1, 120.0 );
			SetSkill( SkillName.Tactics, 119.1, 120.0 );
			SetSkill( SkillName.Anatomy, 119.1, 120.0 );
			SetSkill( SkillName.Focus, 119.1, 120.0 );
			SetSkill( SkillName.Meditation, 119.1, 120.0 );

			Fame = 300;
			Karma = -300;
			VirtualArmor = 40;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( Utility.RandomDouble() < 0.14 )
				c.DropItem( new TatteredSithCloak() );

			if ( Utility.RandomDouble() < 0.10 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new DarkPoisonFieldDisc() ); break;
					case 1: c.DropItem( new LightPoisonFieldDisc() ); break;
				}
			}
				
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
		
		public override bool BleedImmune{ get{ return true; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		public AnointedOne( Serial serial ) : base( serial )
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