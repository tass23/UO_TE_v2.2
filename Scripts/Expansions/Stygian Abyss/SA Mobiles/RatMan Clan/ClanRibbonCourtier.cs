using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a clan ribbon courtier corpse" )]
	public class ClanRibbonCourtier : BaseCreature
	{
		//public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Ratman; } }

		[Constructable]
		public ClanRibbonCourtier() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Clan Ribbon Courtier";
			Body = 42;
            Hue = 2207;
			BaseSoundID = 437;

			SetStr( 220, 250 );
			SetDex( 240, 260 );
			SetInt( 100, 150 );

			SetHits( 2054, 2100 );

			SetDamage( 7, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40 );
			SetResistance( ResistanceType.Fire, 10, 12 );
			SetResistance( ResistanceType.Cold, 15, 20);
			SetResistance( ResistanceType.Poison, 10, 12 );
			SetResistance( ResistanceType.Energy, 10, 12 );

			SetSkill( SkillName.MagicResist, 113.5, 115.0 );
			SetSkill( SkillName.Tactics, 65.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.5, 55.0 );

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 48;
		}

		public override void GenerateLoot()
		{
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.Average);
		}

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.10)
            {
                switch (Utility.Random(3))
                {
                    case 0: c.DropItem(new AbyssalCloth()); break;

                    case 1: c.DropItem(new DelicateScales()); break;

                    case 2: c.DropItem(new Lodestone()); break;

                   // case 3: c.DropItem(new SeedOfRenewal()); break;
                }
            }
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Hides{ get{ return 8; } }
		public override HideType HideType{ get{ return HideType.Spined; } }

		public ClanRibbonCourtier( Serial serial ) : base( serial )
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