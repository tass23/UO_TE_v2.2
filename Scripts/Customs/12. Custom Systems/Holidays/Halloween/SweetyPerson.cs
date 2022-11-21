using System;
using System.Collections;
using Server;
using Server.Misc;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "a Sweety Person's corpse" )]
	public class SweetyPerson : BaseCreature
	{
		[Constructable]
		public SweetyPerson() : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "Sweety Person";
			Body = 772;
			Hue = 1109;

			SetStr( 1000, 1100 );
			SetDex( 300, 350 );
			SetInt( 200, 225 );

			SetHits( 1000, 1200 );

			SetDamage( 24, 26 );

			SetDamageType( ResistanceType.Poison, 100 );

			SetResistance( ResistanceType.Physical, 90 );
			SetResistance( ResistanceType.Fire, 10 );
			SetResistance( ResistanceType.Cold, 100 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 100 );

			SetSkill( SkillName.EvalInt, 100.0 );
			SetSkill( SkillName.Magery, 70.1, 80.0 );
			SetSkill( SkillName.Meditation, 85.1, 95.0 );
			SetSkill( SkillName.MagicResist, 80.1, 100.0 );
			SetSkill( SkillName.Tactics, 70.1, 90.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );
			SetSkill( SkillName.Spellweaving, 175.1, 200.0 );

			Fame = 8000;
			Karma = 8000;

			VirtualArmor = 16;

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.MedScrolls, 2 );
			// TODO: Daemon bone ...
		}
		
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AutoDispel{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 3; } }

		public override bool InitialInnocent{ get{ return true; } }
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

			c.DropItem(new SweetyPersonHook());
        }

		public override int GetHurtSound()
		{
			return 0x14D;
		}

		public override int GetDeathSound()
		{
			return 0x314;
		}

		public override int GetAttackSound()
		{
			return 0x75;
		}

		private DateTime m_NextAbilityTime;

		private static Hashtable m_Table = new Hashtable();

		public static bool UnderEffect( Mobile m )
		{
			return m_Table.Contains( m );
		}

		public SweetyPerson( Serial serial ) : base( serial )
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