using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "Friedy's corpse" )]
	public class FriedyBruger : BaseCreature
	{
     	public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
     	public DateTime m_NextTalk;
		
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( DateTime.Now >= m_NextTalk && InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) ) // check if its time to talk + Player in range.
			{
				m_NextTalk = DateTime.Now + TalkDelay;
				switch ( Utility.Random( 5 ))		   
				{
					case 0: Say("Come to Friedy!"); break;
					case 1: Say("Mommy can't help you now!"); break;
					case 2: Say("You are all my relatives now."); break;
					case 3: Say("Sorry, kid. I don't believe in fairy tales."); break;
					case 4: Say("You're mine now, little one."); break;
				};
		
			}
		}
		[Constructable]
		public FriedyBruger() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Friedy Bruger";
			Body = 0xB7;
			BaseSoundID = 0x48D;

			SetStr( 1500 );
			SetDex( 275, 300 );
			SetInt( 171, 220 );

			SetHits( 14000 );

			SetDamage( 34, 36 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 90 );
			SetResistance( ResistanceType.Fire, 10 );
			SetResistance( ResistanceType.Cold, 100 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 100 );

			SetSkill( SkillName.DetectHidden, 100.0 );
			SetSkill( SkillName.EvalInt, 77.6, 87.5 );
			SetSkill( SkillName.Magery, 77.6, 87.5 );
			SetSkill( SkillName.Meditation, 100.0 );
			SetSkill( SkillName.MagicResist, 80.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 20000;
			Karma = -20000;

			VirtualArmor = 44;

		}
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			c.DropItem(new FriedysMitts());
			c.DropItem(new RewardScroll());
			if ( Utility.RandomDouble() < 0.10 )
				c.DropItem(new FriedysHead());
		}
		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 8 );
		}
		
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public FriedyBruger( Serial serial ) : base( serial )
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