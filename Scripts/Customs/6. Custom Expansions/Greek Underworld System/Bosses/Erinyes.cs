using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a daemon corpse" )]
	public class Erinyes : BaseCreature
	{
		private DateTime m_NextAbility;

		[Constructable]
		public Erinyes() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an Erinyes";
			Body = 149;
			this.Hue = 1175;
			SetStr( 1000 );
			SetDex( 60 );
			SetInt( 100 );
			SetHits( 5000 );
			SetDamage( 15, 20 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 80 );
			SetResistance( ResistanceType.Cold, 60 );
			SetResistance( ResistanceType.Poison, 60 );
			SetResistance( ResistanceType.Energy, -50 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 300.0 );
			SetSkill( SkillName.Wrestling, 300.0 );
			SetSkill( SkillName.Anatomy, 120.0 );

			Fame = 500;
			Karma = -500;
			VirtualArmor = 40;
		}

		public void Punish( Mobile toPunish, int damage )
		{
			if ( damage < 20 )
			{
				this.Say( "Die, worm!" );
				AOS.Damage( toPunish, this, damage, 100, 0, 0, 0, 0 );
			}
			else
			{
				this.Say( "Retribution is at hand, ", toPunish.Name.ToString());
				AOS.Damage( toPunish, this, damage, true, 100, 0, 0, 0, 0 );
				this.Combatant = toPunish;
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

		public Erinyes( Serial serial ) : base( serial )
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