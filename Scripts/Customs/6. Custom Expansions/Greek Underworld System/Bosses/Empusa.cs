using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a daemon corpse" )]
	public class Empusa : BaseCreature
	{
		private DateTime m_NextAbility;
		private Mobile m_Empusa;
		[Constructable]
		public Empusa() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Empusa";
			Body = 15;
			Hue = 0;
			BaseSoundID = 278;
			SetStr( 800 );
			SetDex( 60 );
			SetInt( 100 );
			SetHits( 10000 );
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
		}

		public override void OnThink()
		{				
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
							this.Hits += AOS.Damage( m, this, Utility.Random( 1, 5 ), 0, 100, 0, 0, 0 );
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

		public Empusa( Serial serial ) : base( serial )
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