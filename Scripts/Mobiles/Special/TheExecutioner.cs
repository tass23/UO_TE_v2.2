using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using Server.Engines.CannedEvil;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class TheExecutioner : BaseChampion
	{
		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Tortured; } }
		
        public override Type[] UniqueArtifacts{ get { return new Type[] {
			typeof( TheBeserkersMaul ) }; } }
			
		public override Type[] SharedArtifacts{ get { return new Type[] {
			typeof( ANecromancerShroud ),
			typeof( DetectiveBoots ) }; } }
			
        public override Type[] DecorationArtifacts{ get { return new Type[] {
            typeof( DecorativeBlood ) }; } }
			
        public override MonsterStatuetteType[] StatueTypes{ get{ return new MonsterStatuetteType[] { }; } }

		[Constructable]
		public TheExecutioner () : base( AIType.AI_Melee )
		{
			Name = "The Executioner";
			Hue = 1319;
			Body = 83;
			BaseSoundID = 427;

			SetStr( 967, 1145 );
			SetDex( 96, 105 );
			SetInt( 76, 100 );

			SetHits( 1276, 1552 );

			SetDamage( 25, 31 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.MagicResist, 125.1, 140.0 );
			SetSkill( SkillName.Tactics, 109.1, 140.0 );
			SetSkill( SkillName.Wrestling, 111.1, 135.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 50;

			PackItem( new ExecutionersAxe() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 4 );
            if (Utility.Random(100) < 98)
            {

                int level;

                double random = Utility.RandomDouble();

                if (0.30 >= random)
                    level = 110;

                else
                    level = 105;

                switch (Utility.Random(24))
                {
                    case 0: AddToBackpack(new PowerScroll(SkillName.Swords, level)); break;
                    case 1: AddToBackpack(new PowerScroll(SkillName.Fencing, level)); break;
                    case 2: AddToBackpack(new PowerScroll(SkillName.Archery, level)); break;
                    case 3: AddToBackpack(new PowerScroll(SkillName.Parry, level)); break;
                    case 4: AddToBackpack(new PowerScroll(SkillName.Tactics, level)); break;
                    case 5: AddToBackpack(new PowerScroll(SkillName.Anatomy, level)); break;
                    case 6: AddToBackpack(new PowerScroll(SkillName.Healing, level)); break;
                    case 7: AddToBackpack(new PowerScroll(SkillName.Magery, level)); break;
                    case 8: AddToBackpack(new PowerScroll(SkillName.Meditation, level)); break;
                    case 9: AddToBackpack(new PowerScroll(SkillName.EvalInt, level)); break;
                    case 10: AddToBackpack(new PowerScroll(SkillName.MagicResist, level)); break;
                    case 11: AddToBackpack(new PowerScroll(SkillName.Musicianship, level)); break;
                    case 12: AddToBackpack(new PowerScroll(SkillName.Provocation, level)); break;
                    case 13: AddToBackpack(new PowerScroll(SkillName.Discordance, level)); break;
                    case 14: AddToBackpack(new PowerScroll(SkillName.Peacemaking, level)); break;
                    case 15: AddToBackpack(new PowerScroll(SkillName.Chivalry, level)); break;
                    case 16: AddToBackpack(new PowerScroll(SkillName.Focus, level)); break;
                    case 17: AddToBackpack(new PowerScroll(SkillName.Necromancy, level)); break;
                    case 18: AddToBackpack(new PowerScroll(SkillName.Stealing, level)); break;
                    case 19: AddToBackpack(new PowerScroll(SkillName.Stealth, level)); break;
                    case 20: AddToBackpack(new PowerScroll(SkillName.Macing, level)); break;
                    case 21: AddToBackpack(new PowerScroll(SkillName.Wrestling, level)); break;
                    case 22: AddToBackpack(new PowerScroll(SkillName.Lockpicking, level)); break;
                    case 23: AddToBackpack(new PowerScroll(SkillName.Fishing, level)); break;
                }
            }

		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override int Meat{ get{ return 2; } }

		public TheExecutioner( Serial serial ) : base( serial )
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