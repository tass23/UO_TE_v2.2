using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a godly corpse" )]
	public class Hades : BaseCreature
	{
		private DateTime m_NextRaiseDead;
		private Mobile m_Persephone;
		private DateTime m_NextHeal;
		private ArrayList m_Erinyes;
		private DateTime m_DoSpawn;

		[Constructable]
		public Hades() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			m_Erinyes = new ArrayList();
			Name = "Hades";
			Title = "the Lord of the Underworld";
			Body = 400;
			Female = false;
			Hue = 2955;
			HairItemID = 8252;
			HairHue = 1175;
			SetStr( 800 );
			SetDex( 60 );
			SetInt( 600 );
			SetHits( 80000 );
			SetDamage( 15, 20 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 50 );
			SetResistance( ResistanceType.Fire, 80 );
			SetResistance( ResistanceType.Cold, -50 );
			SetResistance( ResistanceType.Poison, 10 );
			SetResistance( ResistanceType.Energy, 80 );
			SetSkill( SkillName.MagicResist, 400.0 );
			SetSkill( SkillName.Tactics, 180.0 );
			SetSkill( SkillName.Macing, 180.0 );
			SetSkill( SkillName.Anatomy, 180.0 );
			SetSkill( SkillName.Magery, 400.0 );
			SetSkill( SkillName.EvalInt, 400.0 );

			CloseHelm helm = new CloseHelm();
			helm.ItemID = 9865;
			helm.Hue = 1719;
			helm.Name = "Crown of Hades";
			helm.Movable = false;
			AddItem( helm );

			PlateChest chest = new PlateChest();
			chest.Hue = 1719;
			chest.Name = "Tunic of the Underworld";
			chest.Movable = false;
			chest.ItemID = 11017;
			AddItem( chest );

			ChainChest chest2 = new ChainChest();
			chest2.Hue = 1765;
			chest2.Layer = Layer.Shirt;
			chest2.Name = "Chainmail Undershirt";
			chest2.Movable = false;
			AddItem( chest2 );

			ChainLegs legs = new ChainLegs();
			legs.Hue = 1765;
			legs.Name = "Chainmail Leggings";
			legs.Movable = false;
			AddItem( legs );

			Cloak cloak = new Cloak( 1765 );
			cloak.Name = "Cloak of the Underworld";
			cloak.Movable = false;
			AddItem( cloak );

			Boots boots = new Boots( 1157 );
			boots.Movable = false;
			AddItem( boots );

			Scepter scepter = new Scepter();
			scepter.Name = "Scepter of Hades";
			scepter.Hue = 1719;
			scepter.Movable = false;
			AddItem( scepter );

			OrderShield shield = new OrderShield();
			shield.Hue = 1765;
			shield.Name = "Shield of the Underworld";
			shield.Movable = false;
			AddItem( shield );

			LeatherGloves gloves = new LeatherGloves();
			gloves.Hue = 1157;
			gloves.Movable = false;
			AddItem( gloves );

			Fame = 1000;
			Karma = -1000;
			VirtualArmor = 40;
			m_NextHeal = DateTime.Now + TimeSpan.FromMinutes( 1.0 );
			m_NextRaiseDead = DateTime.Now + TimeSpan.FromMinutes( 5.0 );
			m_DoSpawn = DateTime.Now + TimeSpan.FromSeconds( 2.0 );			
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
						m.SendMessage( 0, "Hades has fallen and you receive some of the loot." );
						m.AddToBackpack( new BankCheck( Utility.Random( 25000, 50000 )));
						int chance = Utility.Random( 1, 100 );

						if ( this.IsParagon == true )
							chance = Utility.Random( 1, 24 );

						if ( chance <= 4 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new UnderworldChest() );
						}
						else if ( chance <= 8 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new UnderworldCloak() );
						}
						else if ( chance <= 12 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new UnderworldShield() );
						}
						else if ( chance <= 16 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new HadesCrown() );
						}
						else if ( chance <= 20 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new HadesScepter() );
						}
						else if ( chance <= 25 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new TalismanOfTheUnderworld() );
						}
					}
				}
			}
			return base.OnBeforeDeath();
		}
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			c.DropItem( new RewardScroll( Utility.Random( 15, 30 )));
			if ( Utility.RandomDouble() < 0.25 )
			c.DropItem( new RewardScroll(5) );
			if ( Utility.RandomDouble() < 0.15 )
			c.DropItem( new RewardScroll(10) );
			if ( Utility.RandomDouble() < 0.10 )
			c.DropItem( new RewardScroll(15) );
			if ( Utility.RandomDouble() < 0.05 )
			c.DropItem( new RewardScroll(20) );
			if ( Utility.RandomDouble() < 0.01 )
			c.DropItem( new RewardScroll(100) );
		}
		public void DoSpawn()
		{
			Erinyes e1 = new Erinyes();
			m_Erinyes.Add( e1 );
			e1.MoveToWorld( this.Location, this.Map );
			Erinyes e2 = new Erinyes();
			m_Erinyes.Add( e2 );
			e2.MoveToWorld( this.Location, this.Map );
			Erinyes e3 = new Erinyes();
			m_Erinyes.Add( e3 );
			e3.MoveToWorld( this.Location, this.Map );
			Persephone per = new Persephone();
			m_Persephone = per;
			per.MoveToWorld( this.Location, this.Map );
		}

		public override void OnDamage( int amount, Mobile from, bool willkill )
		{
			if ( CheckErinyes() )
			{
				for( int i = 0; i < m_Erinyes.Count; i++ )
				{
					Mobile m = (Mobile)m_Erinyes[i];
					if ( m is Erinyes )
					{
						Erinyes e = m as Erinyes;
						e.Punish( from, amount );
					}
				}
			}

			base.OnDamage( amount, from, willkill );
		}

		public override void OnThink()
		{
			if ( m_DoSpawn < DateTime.Now )
			{
				DoSpawn();
				m_DoSpawn = DateTime.Now + TimeSpan.FromHours( 1000.0 );
			}

			if ( m_NextHeal < DateTime.Now && m_Persephone != null && m_Persephone is Persephone && m_Persephone.Alive && this.Hits < this.HitsMax )
			{
				Persephone per = m_Persephone as Persephone;
				per.DoHeal( this );
				m_NextHeal = DateTime.Now + TimeSpan.FromMinutes( 1.5 );
			}
			if ( this.Combatant != null )
			{
				if ( DateTime.Now > m_NextRaiseDead )
				{
					FallenHero spawn1 = new FallenHero();
					FallenHero spawn2 = new FallenHero();
					VengefulCorpse spawn3 = new VengefulCorpse();
					VengefulCorpse spawn4 = new VengefulCorpse();
					Cacodemon spawn5 = new Cacodemon();
					Cacodemon spawn6 = new Cacodemon();
					spawn1.MoveToWorld( this.Combatant.Location, this.Combatant.Map );
					spawn2.MoveToWorld( this.Combatant.Location, this.Combatant.Map );
					spawn3.MoveToWorld( this.Combatant.Location, this.Combatant.Map );
					spawn4.MoveToWorld( this.Combatant.Location, this.Combatant.Map );
					spawn5.MoveToWorld( this.Combatant.Location, this.Combatant.Map );
					spawn6.MoveToWorld( this.Combatant.Location, this.Combatant.Map );
					m_NextRaiseDead = DateTime.Now + TimeSpan.FromMinutes( 4.0 );
				}
			}							
		}

		public bool CheckErinyes()
		{
			int count = 0;
			if ( m_Erinyes != null && m_Erinyes.Count > 0 )
			{
				for( int i = 0; i < m_Erinyes.Count; i++ )
				{
					Mobile m = (Mobile)m_Erinyes[i];
					if ( m.Alive )
						count += 1;
				}
			}
			if ( count > 0 )
				return true;
			return false;
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

		public Hades( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.WriteMobileList( m_Erinyes );
			writer.Write( (DateTime) m_DoSpawn );
			writer.Write( (Mobile) m_Persephone );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Erinyes = reader.ReadMobileList();
			m_DoSpawn = reader.ReadDateTime();
			m_Persephone = reader.ReadMobile();
		}
	}
}