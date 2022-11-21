using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a godly corpse" )]
	public class Hecate : BaseCreature
	{
		private DateTime m_NextAreaBlast;
		private DateTime m_NextParalyze;
		private DateTime m_NextRaiseDead;
		private Mobile m_Melinoe;
		private Mobile m_Hekabe;
		private Mobile m_Gale;
		private DateTime m_DoSpawn;
		private DateTime m_NextHeal;

		[Constructable]
		public Hecate() : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "Hecate";
			Title = "the Lady of the Moon";
			Body = 401;
			Female = true;
			Hue = 2955;
			HairItemID = 8252;
			HairHue = 1150;
			SetStr( 800 );
			SetDex( 60 );
			SetInt( 600 );
			SetHits( 50000 );
			SetDamage( 15, 20 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 50 );
			SetResistance( ResistanceType.Fire, 80 );
			SetResistance( ResistanceType.Cold, -50 );
			SetResistance( ResistanceType.Poison, 10 );
			SetResistance( ResistanceType.Energy, 80 );
			SetSkill( SkillName.MagicResist, 400.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
			SetSkill( SkillName.Anatomy, 120.0 );
			SetSkill( SkillName.Magery, 400.0 );
			SetSkill( SkillName.EvalInt, 400.0 );

			AddItem( new Skirt( 1765 ));
			AddItem( new Doublet( 1765 ));
			AddItem( new ThighBoots( 1765 ));
			Torch torch = new Torch();
			torch.Movable = false;
			torch.Ignite();
			AddItem( torch );
			SpellweavingBook book = new SpellweavingBook();
			book.Hue = 1767;
			AddItem( book );
			Fame = 500;
			Karma = -500;
			VirtualArmor = 40;
			m_NextHeal = DateTime.Now + TimeSpan.FromMinutes( 2.0 );
			m_DoSpawn = DateTime.Now + TimeSpan.FromSeconds( 3.0 );			
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
						m.SendMessage( 0, "Hecate has fallen and you receive some of the loot." );
						m.AddToBackpack( new BankCheck( Utility.Random( 2500, 5000 )));
						int chance = Utility.Random( 1, 100 );

						if ( this.IsParagon == true )
							chance = Utility.Random( 1, 24 );

						if ( chance <= 13 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new TalismanOfTheMoon() );
						}
					}
				}
			}
			return base.OnBeforeDeath();
		}
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			base.OnDeath( c );
			c.DropItem( new RewardScroll( Utility.Random( 1, 10 )));
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
			Melinoe mel = new Melinoe();
			m_Melinoe = mel;
			mel.MoveToWorld( this.Location, this.Map );
			mel.Home = this.Location;
			mel.RangeHome = 8;

			Hekabe hek = new Hekabe();
			m_Hekabe = hek;
			hek.MoveToWorld( this.Location, this.Map );
			hek.Home = this.Location;
			hek.RangeHome = 8;	

			Gale gale = new Gale();
			m_Hekabe = hek;
			gale.MoveToWorld( this.Location, this.Map );
			gale.Home = this.Location;
			gale.RangeHome = 8;
		}

		public override void OnDamage( int amount, Mobile from, bool willkill )
		{
			if ( (m_Melinoe != null && m_Melinoe.Alive) || (m_Hekabe != null && m_Hekabe.Alive) || (m_Gale != null && m_Gale.Alive) )
			{
				this.Say( "You will never slay me while my MINIONS LIVE!" );
				return;
			}

			base.OnDamage( amount, from, willkill );
		}

		public override void OnThink()
		{
			if ( m_DoSpawn < DateTime.Now )
			{
				DoSpawn();
				m_DoSpawn = DateTime.Now + TimeSpan.FromMinutes( 2.0 );
			}

			if ( m_NextHeal < DateTime.Now && ((m_Melinoe != null && m_Melinoe.Alive) || (m_Hekabe != null && m_Hekabe.Alive) || (m_Gale != null && m_Gale.Alive)) )
			{
				this.Hits += 50000;
				this.Say( "You will never defeat me while my MINIONS LIVE!" );
				m_NextHeal = DateTime.Now + TimeSpan.FromMinutes( 3.0 );
			}
			if ( this.Combatant != null )
			{
				if ( m_NextAreaBlast < DateTime.Now )
				{
					ArrayList alist = new ArrayList();
					IPooledEnumerable eable = this.Map.GetMobilesInRange( this.Location, 7 );

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
								{
									AOS.Damage( c, this, Utility.Random( 25, 50 ), true, 100, 0, 0, 0, 0 );
									c.BoltEffect( 3 );
								}
							}
							else
							{
								AOS.Damage( m, this, Utility.Random( 25, 50 ), true, 100, 0, 0, 0, 0 );
								m.BoltEffect( 3 );
							}
						}
					}
					m_NextAreaBlast = DateTime.Now + TimeSpan.FromMinutes( 1.0 );
				}

				if ( m_NextParalyze < DateTime.Now )
				{
					this.Combatant.Paralyzed = true;
					this.Combatant.PlaySound( 516 );
					m_NextParalyze = DateTime.Now + TimeSpan.FromMinutes( 2.0 );
				}

				if ( m_NextRaiseDead < DateTime.Now && this.Hits < (this.HitsMaxSeed * 80) / 100 )
				{
					Lamiae vamp1 = new Lamiae();
					vamp1.MoveToWorld( this.Location, this.Map );
					m_NextRaiseDead = DateTime.Now + TimeSpan.FromMinutes( 4.0 );
				}
			}							
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

		public Hecate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( (Mobile)m_Melinoe );
			writer.Write( (Mobile)m_Hekabe );
			writer.Write( (Mobile)m_Gale );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Melinoe = reader.ReadMobile();
			m_Hekabe = reader.ReadMobile();
			m_Gale = reader.ReadMobile();
		}
	}
}