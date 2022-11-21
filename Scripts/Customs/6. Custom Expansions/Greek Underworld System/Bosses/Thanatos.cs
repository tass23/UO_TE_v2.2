using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a godly corpse" )]
	public class Thanatos : BaseCreature
	{
		private DateTime m_NextSummon;

		[Constructable]
		public Thanatos() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Thanatos";
			Title = "the Bringer of Death";
			Body = 400;
			Female = false;
			Hue = 2955;
			HairItemID = 8252;
			HairHue = 1175;
			FacialHairItemID = 8268;
			FacialHairHue = 1175;
			SetStr( 800 );
			SetDex( 60 );
			SetInt( 600 );
			SetHits( 30000 );
			SetDamage( 15, 20 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 60 );
			SetResistance( ResistanceType.Cold, 60 );
			SetResistance( ResistanceType.Poison, 60 );
			SetResistance( ResistanceType.Energy, 60 );
			SetSkill( SkillName.MagicResist, 400.0 );
			SetSkill( SkillName.Tactics, 300.0 );
			SetSkill( SkillName.Swords, 300.0 );
			SetSkill( SkillName.Anatomy, 300.0 );
			SetSkill( SkillName.Parry, 100.0 );
			SetSkill( SkillName.Magery, 100.0 );
			SetSkill( SkillName.EvalInt, 100.0 );

			AddItem( new Robe( 1175 ));
			AddItem( new Sandals( 1175 ));
			VikingSword sword = new VikingSword();
			sword.Hue = 2958;
			sword.Name = "Sword of Thanatos";
			sword.Movable = false;
			AddItem( sword );

			Fame = 500;
			Karma = -500;
			VirtualArmor = 40;
			m_NextSummon = DateTime.Now + TimeSpan.FromMinutes( 2.0 );
		}

		public override bool OnBeforeDeath()
		{
			ArrayList alist = new ArrayList();
			IPooledEnumerable eable = this.Map.GetMobilesInRange( this.Location, 30 );

			foreach( Mobile m in eable )
				alist.Add( m );

			if ( alist != null && alist.Count > 0 )
			{
				for( int i = 0; i < alist.Count; i++ )
				{
					Mobile m = (Mobile)alist[i];

					if ( m is PlayerMobile )
					{
						m.SendMessage( 0, "Thanatos has fallen and you receive some of the loot." );
						m.AddToBackpack( new BankCheck( Utility.Random( 2500, 5000 )));
						int chance = Utility.Random( 1, 100 );

						if ( this.IsParagon == true )
							chance = Utility.Random( 1, 24 );

						if ( chance <= 10 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new ThanatosRobe() );
						}
						else if ( chance <= 20 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new BladeOfThanatos() );
						}
						else if ( chance <= 25 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new TalismanOfShadows() );
						}
					}
				}
			}
			return base.OnBeforeDeath();
		}
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			c.DropItem( new RewardScroll( Utility.Random( 1, 5 )));
			if ( Utility.RandomDouble() < 0.25 )
			c.DropItem( new RewardScroll(5) );
			if ( Utility.RandomDouble() < 0.15 )
			c.DropItem( new RewardScroll(10) );
			if ( Utility.RandomDouble() < 0.10 )
			c.DropItem( new RewardScroll(15) );
			if ( Utility.RandomDouble() < 0.05 )
			c.DropItem( new RewardScroll(20) );
			if ( Utility.RandomDouble() < 0.01 )
			c.DropItem( new RewardScroll(25) );
		}

		public void DoSpawn()
		{
			Oneiri o1 = new Oneiri();
			o1.MoveToWorld( this.Location, this.Map );
			o1.Home = this.Location;
			o1.RangeHome = 10;
			m_NextSummon = DateTime.Now + TimeSpan.FromSeconds( 45.0 );
		}

		public override void OnThink()
		{
			if ( this.Combatant != null )
			{
				if ( this.Combatant.Hits < this.Combatant.HitsMax / 3 )
				{
					this.Combatant.Kill();
					this.Hits += 1000;
				}

				if ( m_NextSummon < DateTime.Now )
					DoSpawn();
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			this.Hits += 5;
			base.OnGaveMeleeAttack( defender );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 4 );
		}

		public override bool Uncalmable{ get{ return Core.SE; } }
		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool AutoDispel{ get{ return true; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		public Thanatos( Serial serial ) : base( serial )
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