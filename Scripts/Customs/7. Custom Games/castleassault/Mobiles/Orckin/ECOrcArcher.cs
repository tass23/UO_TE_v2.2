using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "an orcish corpse" )]
	public class ECOrcArcher : BaseCreature
	{
		private DateTime NextAbilityTime;

		private const int maxDist = 10;

		[Constructable]
		public ECOrcArcher() : base( AIType.AI_Melee, FightMode.Closest, 10, 6, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "orc" );
			Body = 17;
			BaseSoundID = 0x45A;

			Hue = 346;

			SetStr( 144, 180 );
			SetDex( 121, 157 );
			SetInt( 54, 90 );

			SetHits( 237, 258 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 40 );
			SetResistance( ResistanceType.Energy, 30, 50 );

			SetSkill( SkillName.Archery, 80.0, 110.0 );
			SetSkill( SkillName.MagicResist, 70.1, 95.0 );
			SetSkill( SkillName.Tactics, 75.1, 100.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );

			Fame = 2000;
			Karma = -2000;

			VirtualArmor = 34;

			AddItem( new RepeatingCrossbow() );

			PackItem( new Bolt( Utility.Random( 50, 120 ) ) );
			PackItem( new ThighBoots() );

			switch ( Utility.Random( 7 ) )
			{
				case 0: PackItem( new Ribs() ); break;
				case 1: PackItem( new Shaft() ); break;
				case 2: PackItem( new Candle() ); break;
                case 3: PackItem(new JukaBow()); break;
                case 4: PackItem(new Shaft()); break;
                case 5: PackItem(new Candle()); break;
                case 6: PackItem(new Shaft()); break;
			}

			if ( 0.01 > Utility.RandomDouble() )
				PackItem( new Bola() );

			if ( 0.2 > Utility.RandomDouble() )
				PackItem( new BolaBall() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
		public override int Meat{ get{ return 1; } }
		public override bool BardImmune{ get{ return true; } }

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

		public ECOrcArcher( Serial serial ) : base( serial )
		{
		}

		public void GetTarget()
		{
			if (Combatant != null) return;

			foreach ( Mobile m in this.GetMobilesInRange( maxDist ) )
			{
				if (m.Player && !m.Deleted && m.Alive && !m.Hidden && m.AccessLevel == AccessLevel.Player)
				{
					if ( m.FindItemOnLayer( Layer.Helm ) is OrcishKinMask )
						return;

					Combatant = m;
					FocusMob = m;

					return;
				}
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
			GetTarget();

			if ( DateTime.Now >= NextAbilityTime )
			{
				if (!UseBola()) return;

				NextAbilityTime = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 22, 25 ) );
			}
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
