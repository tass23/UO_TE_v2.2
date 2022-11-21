using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "the remains of a rising colossus" )]
	public class MysticismRisingColossus : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 125.0; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public MysticismRisingColossus() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.5 )
		{
			Name = "Rising Colossus";
			Body = 829;

			SetStr( 300 );
			SetDex( 300 );
			SetInt( 300 );

			SetDamage( 17, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 100, 105 );
			SetResistance( ResistanceType.Fire, 100, 105 );
			SetResistance( ResistanceType.Cold, 100, 105 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			VirtualArmor = 58;
			ControlSlots = 5;
		}

		public override int GetAttackSound()
		{
			return 0x627;
		}

		public override int GetHurtSound()
		{
			return 0x629;
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } } // Immune to poison?

		public MysticismRisingColossus( Serial serial ) : base( serial )
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