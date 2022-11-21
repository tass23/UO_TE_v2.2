using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class SithRevenant : BaseCreature
	{
		private DateTime m_NextAbility;
		[Constructable]
		public SithRevenant() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Sith Revenant";
			Body = 721;
			Hue = 1786;
			SetStr( 195, 200 );
			SetDex( 60, 75 );
			SetInt( 100, 125 );
			SetHits( 175, 200 );

			SetDamage( 15, 20 );
			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 80 );
			SetResistance( ResistanceType.Cold, 60 );
			SetResistance( ResistanceType.Poison, 60 );
			SetResistance( ResistanceType.Energy, 50 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
			SetSkill( SkillName.Anatomy, 120.0 );

			Fame = 500;
			Karma = -500;
			VirtualArmor = 40;
		}

		public override void OnThink()
		{
			if ( m_NextAbility < DateTime.Now )
			{
				this.Say( "*wails*" );
				this.PlaySound( 1157 );
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
								c.BeginFlee( TimeSpan.FromSeconds( 30.0 ));
						}
						m.AddStatMod( new StatMod( StatType.Str, "Curse of the Sith Revenant", -10, TimeSpan.FromSeconds( 30.0 ) ) );
					}
				}
				m_NextAbility = DateTime.Now + TimeSpan.FromMinutes( 5.0 );
			}
		}

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool AlwaysMurderer{ get{ return true; }}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager, 2 );
		}

		public SithRevenant( Serial serial ) : base( serial )
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