using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Engines.Quests;

namespace Server.Mobiles
{
	[CorpseName( "a Smelly Pig corpse" )]
	public class SmellyPig : BaseCreature
	{
		public override bool InitialInnocent{ get{ return true; } }

		[Constructable]
		public SmellyPig() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a Smelly Pig";
			Body = 0xCB;
			Hue = 1820;
			BaseSoundID = 0xC4;

			SetStr( 20 );
			SetDex( 20 );
			SetInt( 5 );
			SetHits( 12 );
			SetMana( 0 );
			SetDamage( 2, 4 );
			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 10, 15 );
			SetSkill( SkillName.MagicResist, 5.0 );
			SetSkill( SkillName.Tactics, 5.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 10;
			Karma = 0;
			VirtualArmor = 12;
			Tamable = false;
		}

		public override int Meat{ get{ return 1; } }

		public static bool IsMurderer( Mobile from )
		{
			if ( from != null && from is PlayerMobile )
			{
				BaseQuest quest = QuestHelper.GetQuest( (PlayerMobile) from, typeof( HolyGrailQuest2 ) );

				if ( quest != null )
					return !quest.Completed;
			}
			return false;
		}

		public SmellyPig(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}