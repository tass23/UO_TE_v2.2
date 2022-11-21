using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{
	[CorpseName( "gambled and lost" )] 
	public class KennyRogers : BaseCreature 
	{
		private static bool m_Talked; // flag to prevent spam 

		string[] kfcsay = new string[] // things to say while greating 
		{ 
			"Do you gamble?",
			"You gotta know when to hold em!",
			"Know when to fold em!",
			"Know when to walk away!",
			"Know when to run!",
			"Beat me and i might have a casino token for you!"
		};	
		
		[Constructable] 
		public KennyRogers() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Name = NameList.RandomName("male");
			Title = "The Gambler";
			Body = 400;
			Hue = 33770;  

			SetStr( 600, 650 );
			SetDex( 150, 200 );
			SetInt( 350, 400 );

			SetHits( 1000, 1500 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 0, 1 );
			SetResistance( ResistanceType.Fire, 0, 1 );
			SetResistance( ResistanceType.Poison, 0, 1 );
			SetResistance( ResistanceType.Energy, 0, 1 );

			SetSkill( SkillName.EvalInt, 85.0, 100.0 );
			SetSkill( SkillName.Tactics, 75.1, 100.0 );
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Wrestling, 100.2, 105.0 );
			SetSkill( SkillName.Meditation, 120.0);
			SetSkill( SkillName.Focus, 120.0);
			SetSkill( SkillName.Swords, 110.0, 120.0 );

			Fame = 250;
			Karma = -250;

			VirtualArmor = 35;

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			
				hair.Hue = 1150;
				hair.Layer = Layer.Hair;
				hair.Movable = false;
				AddItem( hair );
				AddItem( new Goatee ( 1150 ) );

				AddItem( new FancyShirt ( ) );
				AddItem( new LongPants ( ) );
				AddItem( new ThighBoots ( ) );
				AddItem( new LeatherGloves ( ) );
				AddItem( new JinBaori ( 1 ) );
				AddItem( new WizardsHat ( 1 ) );
			}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		

			if ( Utility.RandomDouble() < 0.15 )
				c.DropItem(new CasinoToken());
			if ( Utility.RandomDouble() < 0.02 )	
				c.DropItem( new GamblersCharm() );
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
	
		public KennyRogers( Serial serial ) : base( serial )
		{
		}
        public override bool AlwaysMurderer{ get{ return true; } }
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