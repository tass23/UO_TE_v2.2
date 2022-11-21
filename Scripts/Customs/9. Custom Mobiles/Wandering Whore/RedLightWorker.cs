/*
 * Chief Cowtipper Productions
 * Based off of Alcore's Werewolf	
 * 
 */

using System;
using Server.Misc;
using Server.Network;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	public class StreetWalker: BaseCreature
	{

		public override bool ShowFameTitle{ get{ return false; } }
                 
		private static bool m_Talked; // flag to prevent spam 

		string[] kfcsay = new string[] 
		{
			"Who needs some Loving?? Only cost ya 5 gold pieces!!!",
			"I am nearly disease free!!!",
            "Come back here darling!!!!",
		};
		[Constructable]
		public StreetWalker():base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{ 
			Body = 0x191;
			Name = NameList.RandomName( "female" );
			Hue = 33784;
			
		        Title = "the Street Walker";
				
			SetStr( 150, 175 );
			SetDex( 105, 135 );
			SetInt( 85, 95 );
            SetHits( 105,125 );
			SetSkill( SkillName.Wrestling, 85.2, 95.6 );
			SetSkill( SkillName.Tactics, 91.5, 99.0 );
			SetSkill( SkillName.MagicResist, 90.6, 96.8);
		    SetSkill( SkillName.Anatomy, 100.1, 100.1 );
			
			this.Fame = 5000;
			this.Karma = -5000;
			this.VirtualArmor = 55;
			
			
            AddItem( new HookerBoots() ); 
            AddItem( new HookerSkirt() );
			AddItem( new GoldEarrings() );
			AddItem( new GoldBracelet() );
			AddItem( new GoldRing() );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public override int HitsMax { get { return 110; } }
		
		public StreetWalker( Serial serial ) : base( serial )
		{
		}
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m_Talked == false ) 
			{ 
				if ( m.InRange( this, 4 ) ) 
				{                
					m_Talked = true; 
					SayRandom( kfcsay, this ); 
					this.Move( GetDirectionTo( m.Location ) ); 
					// Start timer to prevent spam 
					SpamTimer t = new SpamTimer(); 
					t.Start(); 
				} 
			} 
		}

		private class SpamTimer : Timer 
		{ 
			public SpamTimer() : base( TimeSpan.FromSeconds( 8 ) ) 
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
		
		public override bool OnBeforeDeath()
		{
			
			Transvestite rm = new Transvestite();
			rm.Team = this.Team;
			rm.MoveToWorld( this.Location, this.Map );
			Effects.SendLocationEffect( Location,Map, 0x3709, 13, 0x3B2, 0 );
					
			this.Delete();
			
			return false;
	
		}
		

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
namespace Server.Mobiles
{
	[CorpseName( "a transvestite corpse" )]
	public class Transvestite : BaseCreature
	{

		private static bool m_Talked; // flag to prevent spam 

      	string[] kfcsay = new string[] 
		{
			"Okay!! Okay!! You got me...I am really a guy!!!",
			"I am still one sexy chick!!!",
            "Come back here.....you know you still want me!!!!",
		};
		[Constructable]
		public Transvestite () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "Male" );
			Body = 400;
			Hue = 33784;
			Title = " the Transvestite";

			SetStr( 350, 375 );
			SetDex( 300, 325 );
			SetInt( 46, 70 );

			SetHits( 300, 350 );

			SetDamage( 15, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 65, 70 );
			SetResistance( ResistanceType.Poison, 75, 95 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 125.1, 140.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 110.0, 125.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 50;

			AddItem( new HookerBoots() ); 
            AddItem( new HookerSkirt() );
			AddItem( new GoldEarrings() );
			AddItem( new GoldBracelet() );
			AddItem( new GoldRing() );

			
		
			PackItem( new Bone( Utility.RandomMinMax( 5, 14 ) ));
	        PackMagicItems( 1, 5 );
			 
        }  
		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 1);
		}
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return Core.AOS ? 3 : 0; } }
		public override int Meat{ get{ return 10; } }
		
		public Transvestite( Serial serial ) : base( serial )
		{
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m_Talked == false ) 
			{ 
				if ( m.InRange( this, 4 ) ) 
				{                
					m_Talked = true; 
					SayRandom( kfcsay, this ); 
					this.Move( GetDirectionTo( m.Location ) ); 
					// Start timer to prevent spam 
					SpamTimer t = new SpamTimer(); 
					t.Start(); 
				} 
			} 
		}

		private class SpamTimer : Timer 
		{ 
			public SpamTimer() : base( TimeSpan.FromSeconds( 8 ) ) 
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
