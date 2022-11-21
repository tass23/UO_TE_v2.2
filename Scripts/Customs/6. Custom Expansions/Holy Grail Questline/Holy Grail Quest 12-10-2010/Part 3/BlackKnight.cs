using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Engines.Quests;

namespace Server.Mobiles
{
	[CorpseName( "a black knight corpse" )]
	public class BlackKnight : BaseCreature
	{
		public override bool InitialInnocent{ get{ return true; } }

		private static bool m_Talked;
		string[] BlackKnightSay = new string[]
		{
			"None shall pass.",
            "I move for no man.",
            "'Tis but a scratch.",
            "Victory is mine!",
			"Chicken!  Chicken!",
			"I'm invincible!",
			"Come 'ere!"
		};

		public override WeaponAbility GetWeaponAbility()
		{
			return Utility.RandomBool() ? WeaponAbility.ConcussionBlow : WeaponAbility.CrushingBlow;
		}

		[Constructable]
		public BlackKnight() : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "The Black Knight";
			Body = 311;
			Hue = 963;
			BaseSoundID = 412;

			SetStr( 250 );
			SetDex( 100 );
			SetInt( 100 );
			SetHits( 1000 );
			SetDamage( 5, 10 );
			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetResistance( ResistanceType.Physical, 50 );
			SetResistance( ResistanceType.Fire, 50 );
			SetResistance( ResistanceType.Cold, 50 );
			SetResistance( ResistanceType.Poison, 50 );
			SetResistance( ResistanceType.Energy, 50 );
			SetSkill( SkillName.Chivalry, 50.0 );
			SetSkill( SkillName.DetectHidden, 50.0 );
			SetSkill( SkillName.EvalInt, 50.0 );
			SetSkill( SkillName.Magery, 50.0 );
			SetSkill( SkillName.Meditation, 50.0 );
			SetSkill( SkillName.MagicResist, 50.0 );
			SetSkill( SkillName.Tactics, 50.0 );
			SetSkill( SkillName.Wrestling, 50.0 );

			Fame = 150;
			Karma = -150;
			VirtualArmor = 54;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 1 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			c.DropItem( new BlackKnightHelm() );
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}
		
		public static bool IsMurderer( Mobile from )
		{
			if ( from != null && from is PlayerMobile )
			{
				BaseQuest quest = QuestHelper.GetQuest( (PlayerMobile) from, typeof( HolyGrailQuest3 ) );

				if ( quest != null )
					return !quest.Completed;
			}
			return false;
		}

		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool AreaPeaceImmune{ get{ return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 4; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m_Talked == false )
			{
				if ( m.InRange( this, 3 ) && m is PlayerMobile)
				{
					m_Talked = true;
					SayRandom( BlackKnightSay, this );
					this.Move( GetDirectionTo( m.Location ) );
					SpamTimer t = new SpamTimer();
					t.Start();
				}
			}
		}

		public override bool OnBeforeDeath()
		{
			this.Say( "All right, we'll call it a draw." );
			return base.OnBeforeDeath();
		}

    	private class SpamTimer : Timer
		{
		   	public SpamTimer() : base( TimeSpan.FromSeconds( 12 ) )
	       	{
          		Priority = TimerPriority.OneSecond;
       		}

         	protected override void OnTick()
        	{
           		m_Talked = false;
        	}
      	}

		private static void SayRandom( string[] say, Mobile m )
	    {
			m.Say( say[Utility.Random( say.Length )] );
		}

		public BlackKnight( Serial serial ) : base( serial )
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