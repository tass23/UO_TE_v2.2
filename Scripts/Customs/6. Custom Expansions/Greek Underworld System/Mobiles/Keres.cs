using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a daemonic corpse" )]
	public class Keres : BaseCreature
	{
		private DateTime m_NextAbility;

		[Constructable]
		public Keres() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Keres";
			Body = 149;
			Hue = 1766;
			BaseSoundID = 0x45A;

			SetStr( 200 );
			SetDex( 60 );
			SetInt( 100 );
			SetHits( 1000 );
			SetDamage( 15, 20 );
			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 60 );
			SetResistance( ResistanceType.Cold, -50 );
			SetResistance( ResistanceType.Poison, 60 );
			SetResistance( ResistanceType.Energy, 60 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
			SetSkill( SkillName.Anatomy, 120.0 );

			Fame = 5000;
			Karma = -5000;
			VirtualArmor = 40;
		}

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override void OnThink()
		{
			if ( m_NextAbility < DateTime.Now && this.Combatant != null && this.Combatant.InRange( this.Location, 5 ))
			{
				this.PlaySound( 481 );
				this.Combatant.ApplyPoison( this, Poison.GetPoison( 4 ));
				this.Combatant.AddStatMod( new StatMod( StatType.Str, "Keres Disease", -10, TimeSpan.FromMinutes( 5.0 ) ) );
				this.Combatant.Paralyzed = true;
				this.m_NextAbility = DateTime.Now + TimeSpan.FromMinutes( 2.0 );
			}
		}

		public override int GetAngerSound()
		{
			return 357;
		}

		public override int GetIdleSound()
		{
			return 358;
		}

		public override int GetAttackSound()
		{
			return 700;
		}

		public override int GetHurtSound()
		{
			return 697;
		}

		public override int GetDeathSound()
		{
			return 699;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
		}

		public Keres( Serial serial ) : base( serial )
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