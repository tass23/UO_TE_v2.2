using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a chimera corpse" )]
	public class Chimera : BaseCreature
	{
		[Constructable]
		public Chimera () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a chimera";
			Hue = 67;
			Body = 276;
			BaseSoundID = 362;

			SetStr( 401, 475 );
			SetDex( 133, 175 );
			SetInt( 101, 175 );

			SetHits( 275, 320 );

			SetDamage( 10, 19 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Poison, 10 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 50, 65 );
			SetResistance( ResistanceType.Cold, 40, 55 );
			SetResistance( ResistanceType.Poison, 20, 40 );
			SetResistance( ResistanceType.Energy, 30, 50 );

			SetSkill( SkillName.MagicResist, 75.1, 90.0 );
			SetSkill( SkillName.Tactics, 75.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );
			SetSkill( SkillName.Anatomy, 35.1, 60.0 );
			SetSkill( SkillName.Poisoning, 15.0, 25.0 );

			Fame = 8800;
			Karma = -8800;

			VirtualArmor = 44;

			PackItem( new DragonFeather( 20 ) );

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int TreasureMapLevel{ get{ return 3; } }
		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 12; } }
		public override HideType HideType{ get{ return HideType.Horned; } }
		public override int Scales{ get{ return 4; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Yellow; } }

		public Chimera( Serial serial ) : base( serial )
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