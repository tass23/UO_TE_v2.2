using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Engines.Quests;

namespace Server.Mobiles
{
	[CorpseName( "a taunting frenchmen's corpse" )]
	public class FrenchmenGuards : BaseCreature
	{
		public override bool InitialInnocent{ get{ return true; } }
		private static bool m_Talked;

		string[] FrenchmenGuardsSay = new string[]
		{
			"Your mother was a hamster!",
            "Your father smelt of eldeberries.",
            "You don't frighten us, English pig-dogs!",
            "I shall taunt you a second time-a!",
			"Ah, this one is for your mother!",
			"I blow my nose at you!",
			"Thppppt!"
		};

		public override WeaponAbility GetWeaponAbility()
		{
			return Utility.RandomBool() ? WeaponAbility.ConcussionBlow : WeaponAbility.CrushingBlow;
		}

		[Constructable]
		public FrenchmenGuards() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a Taunting Frenchman";
			Body = 0x190;
			Hue = 0x8400;

			SetStr( 86, 100 );
			SetDex( 81, 95 );
			SetInt( 61, 75 );
			SetDamage( 10, 12 );
			SetHits( 150, 200 );
			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 50 );
			SetResistance( ResistanceType.Fire, 50 );
			SetResistance( ResistanceType.Cold, 50 );
			SetResistance( ResistanceType.Poison, 50 );
			SetResistance( ResistanceType.Energy, 50 );
			SetSkill( SkillName.Fencing, 66.0, 70.5 );
			SetSkill( SkillName.Macing, 65.0, 70.5 );
			SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			SetSkill( SkillName.Swords, 65.0, 70.5 );
			SetSkill( SkillName.Tactics, 65.0, 70.5 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );
			
			Item helm = new ChainCoif();
			helm.Movable = false;
			helm.Hue = 2408;
			AddItem( helm );

			Item leg = new LeatherLegs();
			leg.Movable = false;
			leg.Hue = 2408;
			AddItem( leg );

			Item chest = new LeatherChest();
			chest.Movable = false;
			chest.Hue = 2408;
			AddItem( chest );

			Item arms = new LeatherArms();
			arms.Movable = false;
			arms.Hue = 2408;
			AddItem( arms );

			Item glove = new PlateGloves();
			glove.Movable = false;
			glove.Hue = 2408;
			AddItem( glove );

			Item neck = new PlateGorget();
			neck.Movable = false;
			neck.Hue = 2408;
			AddItem( neck );

			Item weapon = new Kryss();
			weapon.Movable = false;
			weapon.Hue = 2408;
			AddItem( weapon );

			AddItem( new ThighBoots(2408) );
			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 1 );
		}

		public static bool IsMurderer( Mobile from )
		{
			if ( from != null && from is PlayerMobile )
			{
				BaseQuest quest = QuestHelper.GetQuest( (PlayerMobile) from, typeof( TauntingFrenchmenQuest ) );

				if ( quest != null )
					return !quest.Completed;
			}
			return false;
		}

		public override bool BardImmune{ get{ return !Core.SE; } }
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
					SayRandom( FrenchmenGuardsSay, this );
					this.Move( GetDirectionTo( m.Location ) );
					SpamTimer t = new SpamTimer();
					t.Start();
				}
			}
		}

		public override bool OnBeforeDeath()
		{
			this.Say( "Toey!" );
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

		public FrenchmenGuards( Serial serial ) : base( serial )
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