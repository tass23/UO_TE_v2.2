using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a godly corpse" )]
	public class Persephone : BaseCreature
	{
		private DateTime m_NextHeal;

		[Constructable]
		public Persephone() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Persephone";
			Title = "the Queen of the Underworld";
			Body = 401;
			Female = true;
			Hue = 2955;
			HairItemID = 8252;
			HairHue = 1818;
			SetStr( 800 );
			SetDex( 60 );
			SetInt( 600 );
			SetHits( 20000 );
			SetMana( 500 );
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
			SetSkill( SkillName.Magery, 200.0 );
			SetSkill( SkillName.EvalInt, 200.0 );

			CloseHelm helm = new CloseHelm();
			helm.ItemID = 11118;
			helm.Name = "Persephone's Crown";
			AddItem( helm );
			helm.Movable = false;

			AddItem( new GildedDress( 1175 ));
			AddItem( new Sandals( 1175 ));
			Torch torch = new Torch();
			torch.Ignite();
			AddItem( torch );
			torch.Movable = false;

			AssassinSpike dagger = new AssassinSpike();
			dagger.Hue = 2101;
			dagger.Name = "Persephone's Thorn";
			this.AddItem( dagger );
			dagger.Movable = false;

			Fame = 1000;
			Karma = -1000;
			VirtualArmor = 40;		
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
						m.SendMessage( 0, "Persephone has fallen and you receive some of the loot." );
						m.AddToBackpack( new BankCheck( Utility.Random( 2500, 5000 )));
						int chance = Utility.Random( 1, 100 );

						if ( this.IsParagon == true )
							chance = Utility.Random( 1, 24 );

						if ( chance <= 15 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new PersephoneCrown() );
						}
						else if ( chance <= 25 )
						{
							m.SendMessage( 1150, "You receive a rare Artifact!" );
							m.AddToBackpack( new PersephoneThorn() );
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
		public void DoHeal( Mobile toHeal )
		{
			if ( this.Mana > 50 && m_NextHeal < DateTime.Now && toHeal.YellowHealthbar == false )
			{
				this.Mana -= 50;
				this.Say( "Be restored, my beloved." );
				toHeal.Hits += 5000;
				m_NextHeal = DateTime.Now + TimeSpan.FromSeconds( 10.0 );
			}
			else if ( this.Hits > 5000 && m_NextHeal < DateTime.Now && toHeal.YellowHealthbar == false )
			{
				this.Say( "Use my life force to keep yourself strong, my beloved." );
				AOS.Damage( this, this, 5000, true, 100, 0, 0, 0, 0 );
				toHeal.Hits += 5000;
				m_NextHeal = DateTime.Now + TimeSpan.FromSeconds( 10.0 );
			}
			else if (  m_NextHeal < DateTime.Now && toHeal.YellowHealthbar == false )
			{
				this.Say( "Take my life so that you may live!" );
				toHeal.Hits += 10000;
				this.Kill();
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

		public Persephone( Serial serial ) : base( serial )
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