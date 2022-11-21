using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Engines.Quests;

namespace Server.Mobiles
{
	[CorpseName( "a Black Beast corpse" )]
	public class BlackBeast : BaseCreature
	{
		public override bool InitialInnocent{ get{ return true; } }

		[Constructable]
		public BlackBeast () : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "Black Beast of Aaaaarrrrrrggghhh";
			Body = 40;
			Hue = 967;
			BaseSoundID = 357;

			SetStr( 986, 1185 );
			SetDex( 177, 255 );
			SetInt( 151, 250 );
			SetHits( 1500, 2000 );
			SetDamage( 5, 10 );
			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );
			SetResistance( ResistanceType.Physical, 40, 80 );
			SetResistance( ResistanceType.Fire, 30, 80 );
			SetResistance( ResistanceType.Cold, 30, 60 );
			SetResistance( ResistanceType.Poison, 50 );
			SetResistance( ResistanceType.Energy, 30, 50 );
			SetSkill( SkillName.Anatomy, 25.1, 50.0 );
			SetSkill( SkillName.EvalInt, 50.1, 60.0 );
			SetSkill( SkillName.Magery, 50.5, 60.0 );
			SetSkill( SkillName.Meditation, 25.1, 50.0 );
			SetSkill( SkillName.MagicResist, 50.5, 60.0 );
			SetSkill( SkillName.Tactics, 50.1, 60.0 );
			SetSkill( SkillName.Wrestling, 50.1, 60.0 );

			Fame = 2400;
			Karma = -2400;
			VirtualArmor = 90;

			PackItem( new Longsword() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 4 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }

		public BlackBeast( Serial serial ) : base( serial )
		{
		}

		public static bool IsMurderer( Mobile from )
		{
			if ( from != null && from is PlayerMobile )
			{
				BaseQuest quest = QuestHelper.GetQuest( (PlayerMobile) from, typeof( HolyGrailQuest8 ) );

				if ( quest != null )
					return !quest.Completed;
			}
			return false;
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