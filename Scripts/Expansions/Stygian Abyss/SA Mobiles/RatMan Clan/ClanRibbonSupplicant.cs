using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a clan ribbon supplicant corpse" )]
	public class ClanRibbonSupplicant : BaseCreature
	{
		//public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Ratman; } }

		[Constructable]
		public ClanRibbonSupplicant() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Clan Ribbon Supplicant";
			Body = 42;
            Hue = 2952;
			BaseSoundID = 437;

			SetStr( 170, 180 );
			SetDex( 100, 120 );
			SetInt( 200, 210 );

			SetHits( 120, 140 );

			SetDamage( 7, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 60 );
			SetResistance( ResistanceType.Fire, 30, 35 );
			SetResistance( ResistanceType.Cold, 80, 85 );
			SetResistance( ResistanceType.Poison, 45, 50 );
			SetResistance( ResistanceType.Energy, 25, 30 );

			SetSkill( SkillName.MagicResist, 78.5, 80.0 );
			SetSkill( SkillName.Tactics, 62.1, 65.0 );
			SetSkill( SkillName.Wrestling, 56.5, 60.0 );

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 48;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 3 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Hides{ get{ return 8; } }
		public override HideType HideType{ get{ return HideType.Spined; } }

		public ClanRibbonSupplicant( Serial serial ) : base( serial )
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