using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a lamiae corpse" )]
	public class Lamiae : BaseCreature
	{
		private DateTime m_NextAbility;
		private DateTime m_NextSong;

		[Constructable]
		public Lamiae() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Lamiae";
			Body = 401;
			Female = true;
			this.Hue = 2955;
			HairItemID = 8252;
			HairHue = 1175;

			SetStr( 600 );
			SetDex( 60 );
			SetInt( 100 );
			SetHits( 2000 );
			SetDamage( 15, 20 );
			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 80 );
			SetResistance( ResistanceType.Cold, 60 );
			SetResistance( ResistanceType.Poison, 60 );
			SetResistance( ResistanceType.Energy, -50 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
			SetSkill( SkillName.Anatomy, 120.0 );

			FemaleLeatherChest chest = new FemaleLeatherChest();
			chest.Hue = 1175;
			AddItem( chest );

			LeatherLegs legs = new LeatherLegs();
			legs.Hue = 1175;
			AddItem( legs );

			AddItem( new Sandals( 1175 ));

			Longsword sword = new Longsword();
			sword.WeaponAttributes.HitLeechHits = 35;
			sword.Hue = 1175;
			sword.Movable = false;
			AddItem( sword );

			Fame = 5000;
			Karma = -5000;
			VirtualArmor = 40;
			m_NextSong = DateTime.Now + TimeSpan.FromSeconds( Utility.Random( 60, 90 ));
		}

		public override bool OnBeforeDeath()
		{
			ArrayList alist = new ArrayList();
			IPooledEnumerable eable = this.Map.GetMobilesInRange( this.Location, 10 );

			foreach( Mobile m in eable )
				alist.Add( m );

			if ( alist != null && alist.Count > 0 )
			{
				for( int i = 0; i < alist.Count; i++ )
				{
					Mobile m = (Mobile)alist[i];
					if ( m is PlayerMobile )
					{
						int chance = Utility.Random( 1, 100 );
						if ( this.IsParagon == true )
							chance = Utility.Random( 1, 24 );

						if ( chance <= 24 )
						{
							m.SendMessage( 1150, "You recieve a rare Artifact!" );
							m.AddToBackpack( new LamiaeRing() );
						}
					}
				}
			}
			return base.OnBeforeDeath();
		}

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override void OnThink()
		{
			if ( m_NextAbility < DateTime.Now && this.Combatant != null )
			{
				this.PlaySound( 794 );
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
								this.Hits += AOS.Damage( c, this, Utility.Random( 30, 40 ), 0, 0, 0, 100, 0 );
						}
						else if ( m is PlayerMobile )
							this.Hits += AOS.Damage( m, this, Utility.Random( 30, 40 ), 0, 0, 0, 100, 0 );
					}
				}
				this.Say( "*Laughs*" );
				m_NextAbility = DateTime.Now + TimeSpan.FromSeconds( 15.0 );
			}

			else if ( m_NextSong < DateTime.Now )
			{
				this.Say( "*Sings*" );
				this.PlaySound( 247 );
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
								c.Paralyzed = true;
						}
						else if ( m is PlayerMobile )
							m.Paralyzed = true;
					}
				}
				m_NextSong = DateTime.Now + TimeSpan.FromSeconds( 60.0 );
			}
		}

		public override bool AlwaysMurderer{ get{ return true; }}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
		}

		public Lamiae( Serial serial ) : base( serial )
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