/* Copied from deamon, still have to get detailed information on Pit Fiend */
using System;
using Server;
using Server.Items;
using Server.Factions;

namespace Server.Mobiles
{
	[CorpseName( "a pit fiend corpse" )]
	public class PitFiend : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 125.0; } }
		public override double DispelFocus{ get{ return 45.0; } }

		public override Faction FactionAllegiance { get { return Shadowlords.Instance; } }
		public override Ethics.Ethic EthicAllegiance { get { return Ethics.Ethic.Evil; } }

		[Constructable]
		public PitFiend () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
            Name = "a Pit fiend";
            Body = 43;
            Hue = 1863;
			BaseSoundID = 357;

            SetStr(376, 405);
            SetDex(176, 195);
            SetInt(201, 225);

            SetHits(226, 243);

            SetDamage(15, 20);

            SetSkill(SkillName.EvalInt, 80.1, 90.0);
            SetSkill(SkillName.Magery, 80.1, 90.0);
            SetSkill(SkillName.MagicResist, 75.1, 85.0);
            SetSkill(SkillName.Tactics, 80.1, 90.0);
            SetSkill(SkillName.Wrestling, 80.1, 100.0);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 60, 70);
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

            Fame = 18000;
            Karma = -18000;

            VirtualArmor = 60;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 1; } }

		public PitFiend( Serial serial ) : base( serial )
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
