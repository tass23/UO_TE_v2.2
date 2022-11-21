using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a clan scratch henchrat corpse" )]
	public class ClanScratchHenchrat : BaseCreature
	{
		[Constructable]
		public ClanScratchHenchrat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Clan Scratch Henchrat";
			Body = 42;
			BaseSoundID = 437;

			SetStr( 200, 230 );
			SetDex( 181, 191 );
			SetInt( 80, 100 );

			SetHits( 2000, 2100 );

			SetDamage( 5, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 26, 30 );
			SetResistance( ResistanceType.Fire, 29, 35 );
			SetResistance( ResistanceType.Cold, 30, 35 );
			SetResistance( ResistanceType.Poison, 15, 20 );
			SetResistance( ResistanceType.Energy, 13, 15 );

			SetSkill( SkillName.MagicResist, 35.4, 40.0 );
			SetSkill( SkillName.Tactics, 61.1, 65.0 );
			SetSkill( SkillName.Wrestling, 64.0, 65.0 );
            SetSkill(SkillName.Anatomy, 74.0, 75.0);

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 48;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 3 );	
		}

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.10)
            {
                switch (Utility.Random(2))
                {
                    case 0: c.DropItem(new AbyssalCloth()); break;

                    case 1: c.DropItem(new PowderedIron()); break;
                }
            }
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Hides{ get{ return 8; } }
		public override HideType HideType{ get{ return HideType.Spined; } }

		public ClanScratchHenchrat( Serial serial ) : base( serial )
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