using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an ice minion corpse" )]
	public class IceMinion : BaseCreature
	{
		[Constructable]
		public IceMinion () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ice minion";
			Body = 776;
			BaseSoundID = 357;
			Hue = 1266;

			SetStr( 16, 40 );
			SetDex( 31, 60 );
			SetInt( 11, 25 );

			SetHits( 10, 24 );

			SetDamage( 2, 5 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 5, 10 );

			SetSkill( SkillName.MagicResist, 10.0 );
			SetSkill( SkillName.Tactics, 0.1, 15.0 );
			SetSkill( SkillName.Wrestling, 25.1, 35.0 );

			Fame = 500;
			Karma = -500;

			VirtualArmor = 18;

			AddItem( new LightSource() );

			PackItem( new Bone( 3 ) );
			// TODO: Body parts
		}

		public override int GetIdleSound()
		{
			return 338;
		}

		public override int GetAngerSound()
		{
			return 338;
		}

		public override int GetDeathSound()
		{
			return 338;
		}

		public override int GetAttackSound()
		{
			return 406;
		}

		public override int GetHurtSound()
		{
			return 194;
		}

		public IceMinion( Serial serial ) : base( serial )
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