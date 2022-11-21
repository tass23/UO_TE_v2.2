using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a tormented dreamer's corpse" )]
	public class TormentedDreamer : BaseCreature
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
					case 0: Say("Oh my god! Nooooo!"); break;
					case 1: Say("Ahhhh, it burns, it burns!"); break;
					case 2: Say("Make him stop! Make him stop!"); break;
					case 3: Say("The agony! The horror! Heeelp meeee!"); break;
					case 4: Say("Please, please kill me. If I die here, I'll be free!"); break;
				};
		
			}
		}
		
		[Constructable]
		public TormentedDreamer () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = ( "Tormented Dreamer" );
			Body = 747;
			BaseSoundID = 357;

			SetStr( 1000 );
			SetDex( 277, 300 );
			SetInt( 700 );

			SetHits( 1450, 1500 );

			SetDamage( 22, 29 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 65, 80 );
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 25.1, 50.0 );
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 95.5, 100.0 );
			SetSkill( SkillName.Meditation, 25.1, 50.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 90;

		}
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			c.DropItem(new TorturedSoul());
		}
		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }

		public TormentedDreamer( Serial serial ) : base( serial )
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