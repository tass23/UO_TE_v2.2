using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a harpy corpse" )]
	public class DeathHarpy : BaseCreature
	{
		private DateTime m_NextAbility;

		[Constructable]
		public DeathHarpy() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Death Harpy";
			Body = 30;
			Hue = 1109;
			BaseSoundID = 402;

			SetStr( 200 );
			SetDex( 60 );
			SetInt( 100 );
			SetHits( 600 );
			SetDamage( 15, 20 );
			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 30 );
			SetResistance( ResistanceType.Fire, 50 );
			SetResistance( ResistanceType.Cold, 40 );
			SetResistance( ResistanceType.Poison, 30 );
			SetResistance( ResistanceType.Energy, 40 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
			SetSkill( SkillName.Anatomy, 120.0 );

			Fame = 5000;
			Karma = -5000;
			VirtualArmor = 40;
		}

		/*
		public override void OnThink()
		{
			if ( m_NextAbility < DateTime.Now && this.Combatant != null && this.Combatant.InRange( this.Location, 10 ))
			{
				this.PlaySound( 908 );
				AOS.Damage( this.Combatant, this, Utility.Random( 15, 20 ), true, 100, 0, 0, 0, 0 );
				this.Combatant.AddStatMod( new StatMod( StatType.Str, "Harpy Screech", -10, TimeSpan.FromSeconds( 15.0 ) ) );
				m_NextAbility = DateTime.Now + TimeSpan.FromSeconds( 45.0 );
			}
		}
		*/

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 1 );
		}

		public DeathHarpy( Serial serial ) : base( serial )
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