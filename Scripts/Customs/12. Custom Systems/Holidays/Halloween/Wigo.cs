using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "Wigo's corpse" )]
	public class Wigo : BaseCreature
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
					case 0: Say("Death is but a doorway, time is but a window, I'll be return with a new carpet pad."); break;
					case 1: Say("Find me a child that I might party again!"); break;
					case 2: Say("On a mountain of skulls, in the castle of pain, I placed carpet on a throne of spoiled milk!"); break;
					case 3: Say("What was, will be! What is, will be no more! What will be, was more, but now on sale!"); break;
					case 4: Say("Now is the season of evil, but a wonderful time to save money on new carpet!"); break;
				};
		
			}
		}
		[Constructable]
		public Wigo() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Wigo the Carpetlayer";
			Body = 766;
			BaseSoundID = 0x48D;

			SetStr( 700 );
			SetDex( 500 );
			SetInt( 1000 );

			SetHits( 18000 );

			SetDamage( 20, 30 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 90 );
			SetResistance( ResistanceType.Fire, 100 );
			SetResistance( ResistanceType.Cold, 90 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 10 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.EvalInt, 77.6, 87.5 );
			SetSkill( SkillName.Magery, 200.0 );
			SetSkill( SkillName.Focus, 200.0 );
			SetSkill( SkillName.Meditation, 100.0 );
			SetSkill( SkillName.MagicResist, 50.1, 75.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			Fame = 20000;
			Karma = -20000;

			VirtualArmor = 44;
		}
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			c.DropItem(new SorrowOfMoldyovia());
			c.DropItem(new RewardScroll());
			if ( Utility.RandomDouble() < 0.10 )
				c.DropItem(new SlimedPainting());
				c.DropItem(new RewardScroll());
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

		public Wigo( Serial serial ) : base( serial )
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