using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a daemon corpse" )]
	public class Oneiri : BaseCreature
	{
		private DateTime m_NextAbility;

		[Constructable]
		public Oneiri() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an oneiri";
			Body = 4;
			this.Hue = 1000566;

			SetStr( 200 );
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

			Fame = 5000;
			Karma = -5000;
			VirtualArmor = 40;
		}

		public override void OnThink()
		{
			if ( m_NextAbility < DateTime.Now && this.Combatant != null )
			{
				if ( this.Combatant is BaseCreature )
				{
					BaseCreature c = this.Combatant as BaseCreature;
					c.BeginFlee( TimeSpan.FromSeconds( 30.0 ));
				}
				this.Combatant.AddStatMod( new StatMod( StatType.Str, "Curse of the Oneiroi", -20, TimeSpan.FromSeconds( 30.0 ) ) );
				m_NextAbility = DateTime.Now + TimeSpan.FromMinutes( 2.0 );
			}
		}

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool AlwaysMurderer{ get{ return true; }}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
		}

		public Oneiri( Serial serial ) : base( serial )
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