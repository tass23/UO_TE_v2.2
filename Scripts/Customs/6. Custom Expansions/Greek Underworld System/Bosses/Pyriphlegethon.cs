using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a godly corpse" )]
	public class Pyriphlegethon : BaseCreature
	{
		private DateTime m_NextAbility;
		private Mobile m_Empusa;
		private DateTime m_ToSummon;

		[Constructable]
		public Pyriphlegethon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Pyriphlegethon";
			Body = 15;
			Hue = 1757;
			BaseSoundID = 278;
			SetStr( 800 );
			SetDex( 60 );
			SetInt( 100 );
			SetHits( 35000 );
			SetDamage( 15, 20 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 50 );
			SetResistance( ResistanceType.Fire, 80 );
			SetResistance( ResistanceType.Cold, -50 );
			SetResistance( ResistanceType.Poison, 10 );
			SetResistance( ResistanceType.Energy, 80 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
			SetSkill( SkillName.Anatomy, 120.0 );
			SetSkill( SkillName.Magery, 120.0 );
			SetSkill( SkillName.EvalInt, 120.0 );

			Fame = 500;
			Karma = -500;
			VirtualArmor = 40;
			m_ToSummon = DateTime.Now + TimeSpan.FromSeconds( 1.0 );
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
						m.SendMessage( 0, "Pyriphlegethon has fallen and you receive some of the loot." );
						m.AddToBackpack( new BankCheck( Utility.Random( 2500, 5000 )));
						int chance = Utility.Random( 1, 100 );

						if ( this.IsParagon == true )
							chance = Utility.Random( 1, 24 );

						if ( chance <= 15 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new TalismanOfFlame() );
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
		public override void OnThink()
		{
			if ( DateTime.Now > m_ToSummon && m_Empusa == null )
			{
				Empusa emp = new Empusa();
				m_Empusa = emp;
				emp.MoveToWorld( this.Location, this.Map );
				emp.Home = this.Location;
				emp.RangeHome = 10;
			}

			if ( m_Empusa != null && m_Empusa.Alive && this.Hits < this.HitsMax )
			{
				this.Say( "Empusa, restore me!" );
				int toHeal = this.HitsMax - this.Hits;
				AOS.Damage( m_Empusa, this, toHeal / 2, true, 100, 0, 0, 0, 0 );
				this.Hits = this.HitsMax;
			}
				
			if ( m_NextAbility < DateTime.Now && this.Str < 1600 )
			{
				ArrayList alist = new ArrayList();
				IPooledEnumerable eable = this.Map.GetMobilesInRange( this.Location, 5 );

				foreach( Mobile m in eable )
					alist.Add( m );

				eable.Free();

				if ( alist != null && alist.Count > 0 )
				{
					for( int i = 0; i < alist.Count; i++ )
					{
						Mobile m = (Mobile)alist[i];

						if ( m is BaseCreature )
						{
							BaseCreature c = m as BaseCreature;

							if ( c.ControlMaster != null )
								this.Hits += AOS.Damage( c, this, Utility.Random( 5, 10 ), 0, 100, 0, 0, 0 );
						}
						else if ( m is PlayerMobile )
							this.Hits += AOS.Damage( m, this, Utility.Random( 5, 10 ), 0, 100, 0, 0, 0 );
					}
				}
				m_NextAbility = DateTime.Now + TimeSpan.FromSeconds( 2.0 );
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 1 );
		}

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public Pyriphlegethon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( (Mobile) m_Empusa );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Empusa = reader.ReadMobile();
		}
	}
}