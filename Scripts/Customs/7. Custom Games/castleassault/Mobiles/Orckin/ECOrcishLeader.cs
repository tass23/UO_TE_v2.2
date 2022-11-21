using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an orcish corpse" )]
	public class ECOrcishLeader : BaseCreature
	{
		private DateTime NextAbilityTime;

		private Point3D home;

		private bool manual;

		[Constructable]
		public ECOrcishLeader() : base( AIType.AI_Mage, FightMode.Closest, 15, 5, 0.1, 0.2 )
		{
			Init();

			manual = true;
		}

		public ECOrcishLeader(Point3D h) : base( AIType.AI_Mage, FightMode.Closest, 15, 5, 0.1, 0.2 )
		{
			Init();

			home = h;

			manual = false;
		}

		public void Init()
		{
			Name = "an orcish leader";
			Body = 138;
			BaseSoundID = 0x45A;

			Hue = 346;

			SetStr( 500 );
			SetDex( 200 );
			SetInt( 1000 );

			SetHits( 3600 );
			SetMana( 5000 );

			SetDamage( 20, 26 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 80, 100 );
			SetResistance( ResistanceType.Fire, 80, 100 );
			SetResistance( ResistanceType.Cold, 80, 90 );
			SetResistance( ResistanceType.Poison, 80, 100 );
			SetResistance( ResistanceType.Energy, 80, 100 );

			SetSkill( SkillName.EvalInt, 90.0, 110.0 );
			SetSkill( SkillName.Magery, 90.0, 110.0 );
			SetSkill( SkillName.MagicResist, 100.0, 120.0 );
			SetSkill( SkillName.Tactics, 80.0, 90.0 );
			SetSkill( SkillName.Wrestling, 100.1, 120.0 );

			Fame = 25000;
			Karma = -25000;

			switch ( Utility.Random( 10 ) )
			{
				case 0: PackItem( new Lockpick() );  break;
				case 1: PackItem( new MortarPestle() ); break;
				case 2: PackItem( new Bottle() ); break;
				case 3: PackItem( new RawRibs() ); break;
				case 4: PackItem( new Shovel() ); break;
                case 5: PackItem( new Lockpick()); break;
                case 6: PackItem(new BowOfTheJukaKing()); break;
                case 7: PackItem( new Bottle()); break;
                case 8: PackItem( new RawRibs()); break;
                case 9: PackItem(new JukaBow()); break;
			}

			PackItem( new RingmailChest() );

			if ( 0.3 > Utility.RandomDouble() )
				PackItem( Loot.RandomPossibleReagent() );

			if ( 0.2 > Utility.RandomDouble() )
				PackItem( new BolaBall() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( !Summoned && !NoKillAwards && (DemonKnight.GetArtifactChance( this ) > Utility.Random( 100000 )) )
			{
				Item i = DemonKnight.CreateRandomArtifact();

				if (i != null) c.AddItem( i );
			}
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }
		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override bool IsEnemy( Mobile m )
		{
			if ( m.Player && m.FindItemOnLayer( Layer.Helm ) is OrcishKinMask )
				return false;

			return base.IsEnemy( m );
		}

		public override void AggressiveAction( Mobile aggressor, bool criminal )
		{
			base.AggressiveAction( aggressor, criminal );

			Item item = aggressor.FindItemOnLayer( Layer.Helm );

			if ( item is OrcishKinMask )
			{
				AOS.Damage( aggressor, 50, 0, 100, 0, 0, 0 );
				item.Delete();
				aggressor.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
				aggressor.PlaySound( 0x307 );
			}
		}

		public bool UseBola()
		{
			if (Combatant == null) return false;

			Mobile to = (Mobile)Combatant;

			if (!to.Mounted)
			{
				return false;
			}

			if ( Core.AOS ) 
			{
				if ( 0.03 > Utility.RandomDouble() )
				{
					Bola temp = new Bola();

					temp.Location = to.Location;
					temp.Map = to.Map;
				}
			}

			to.Damage( 1, this );

			IMount mt = to.Mount;

			if ( mt != null ) mt.Rider = null;

			to.BeginAction( typeof( BaseMount ) );

			to.SendLocalizedMessage( 1040023 ); // You have been knocked off of your mount!

			to.EndAction( typeof( BaseMount ) );

			return true;
		}

		public override void OnThink()
		{
			if (manual)
			{
				home = new Point3D(Location);

				manual = false;				
			}
			else
			{
				double dist = GetDistanceToSqrt(home);

				if ( dist > 20 ) 
				{
					Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
					Effects.PlaySound( this, Map, 0x201 );
	
					Location = home;

					Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
					Effects.PlaySound( this, Map, 0x201 );
				}
			}

			if ( DateTime.Now >= NextAbilityTime )
			{
				if (!UseBola()) return;

				NextAbilityTime = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 20, 35 ) );
			}
		}

		public ECOrcishLeader( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( home );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			home = reader.ReadPoint3D();

			if (home.X == 0 && home.Y == 0 && home.Z == 0)
				manual = true;
			else
				manual = false;
		}
	}
}