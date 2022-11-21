using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a godly corpse" )]
	public class Gale : BaseCreature
	{
		[Constructable]
		public Gale() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Gale";
			Body = 279;
			Female = true;
			Hue = 1175;
			SetStr( 800 );
			SetDex( 60 );
			SetInt( 600 );
			SetHits( 2500 );
			SetDamage( 15, 20 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Physical, 50 );
			SetResistance( ResistanceType.Fire, 80 );
			SetResistance( ResistanceType.Cold, -50 );
			SetResistance( ResistanceType.Poison, 10 );
			SetResistance( ResistanceType.Energy, 80 );
			SetSkill( SkillName.MagicResist, 400.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 190.0 );
			SetSkill( SkillName.Anatomy, 190.0 );

			Fame = 500;
			Karma = -500;
			VirtualArmor = 40;			
		}

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Disarm;
		}

		public override void GenerateLoot()
		{
		}

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool AutoDispel{ get{ return true; } }

		public Gale( Serial serial ) : base( serial )
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