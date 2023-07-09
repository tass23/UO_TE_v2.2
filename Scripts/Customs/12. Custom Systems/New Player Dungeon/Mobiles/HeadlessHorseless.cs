using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a headless and horseless corpse" )]
	public class HeadlessHorseless : BaseCreature
	{
		[Constructable]
		public HeadlessHorseless() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Headless and Horseless";
			Body = 31;
			Hue = 1205;
			BaseSoundID = 0x39D;

			SetStr( 26, 50 );
			SetDex( 36, 55 );
			SetInt( 16, 30 );

			SetHits( 16, 30 );

			SetDamage( 5, 8 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );

			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 25.1, 35.0 );
			SetSkill( SkillName.Wrestling, 25.1, 35.0 );

			Fame = 450;
			Karma = -450;

			VirtualArmor = 18;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
			// TODO: body parts
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Meat{ get{ return 1; } }

		public HeadlessHorseless( Serial serial ) : base( serial )
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