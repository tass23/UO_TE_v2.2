using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.ContextMenus;

namespace Server.Mobiles
{
    public class NeeKnight : BaseCreature
    {
    	private static bool m_Talked;

		string[] kfcsay = new string[]
		{
			"Ni!",
			"Ni, Ni, Ni!",
			"Ni"
		};

        [Constructable]
        public NeeKnight() : base( AIType.AI_Melee, FightMode.None, 10, 1, 0.8, 3.0 )
        {
        	Name = "a Knight Who Says 'Ni'";
        	Body = 0x190;
        	Hue = Utility.RandomSkinHue();
        	CantWalk = true;
        	Blessed = true;

            SetStr( 10, 30 );
            SetDex( 10, 30 );
            SetInt( 10, 30 );
            Fame = 50;
            Karma = 50;
	    	SetSkill( SkillName.Magery, 60.0, 70.0 );
	    	SetSkill( SkillName.Carpentry, 60.0, 70.0 );

	    	Item helm = new NorseHelm();
			helm.Movable = false;
			helm.Hue = 2051;
			AddItem( helm );

			Item leg = new PlateLegs();
			leg.Movable = false;
			leg.Hue = 2051;
			AddItem( leg );

			Item glove = new PlateGloves();
			glove.Movable = false;
			glove.Hue = 2051;
			AddItem( glove );

			Item neck = new PlateGorget();
			neck.Movable = false;
			neck.Hue = 2051;
			AddItem( neck );

			Item sword = new VikingSword();
			sword.Movable = false;
			sword.Hue = 2051;
			AddItem( sword );

			AddItem( new Robe( 2051 ) );
			AddItem( new Cloak( 2051 ) );

            SpeechHue = Utility.RandomDyedHue();
        }

        public NeeKnight( Serial serial ) : base( serial )
        {
        }
        
        public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m_Talked == false )
			{
				if ( m.InRange( this, 2 ) && m.AccessLevel == AccessLevel.Player )
				{
					m_Talked = true;
					SayRandom( kfcsay, this, m );
					this.Move( GetDirectionTo( m.Location ) );
					SpamTimer t = new SpamTimer();
					t.Start();
				}
			}
		}

		private class SpamTimer : Timer 
		{
			public SpamTimer() : base( TimeSpan.FromSeconds( 10 ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
			} 

			protected override void OnTick() 
			{ 
				m_Talked = false; 
			} 
		} 

		private static void SayRandom( string[] say, Mobile m, Mobile t ) 
		{ 
			m.Say( String.Format("{0}, " + say[Utility.Random( say.Length )] ,t.Name) ); 
		}

        private static int GetRandomHue()
        {
            switch ( Utility.Random( 6 ) )
            {
                default:
                case 0: return 0;
                case 1: return Utility.RandomBlueHue();
                case 2: return Utility.RandomGreenHue();
                case 3: return Utility.RandomRedHue();
                case 4: return Utility.RandomYellowHue();
                case 5: return Utility.RandomNeutralHue();
            }
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