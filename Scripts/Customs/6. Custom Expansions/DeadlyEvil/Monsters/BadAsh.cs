using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	public class BadAsh : BaseCreature
	{
        public override bool ClickTitle { get { return false; } }
        private bool m_TrueForm;

		private static bool m_Talked;
		string[] BadAshSay = new string[] 
		{ 
			"I'm BAD ASH and you're Good Ash!",
            "You're a goody little two-shoes!",
            "Goody little TWO-SHOES! HEHEHEHEHE!",
            "I got a bone to pick with you!", 
			"You're making me mad, you ugly son of a...!",
			"You're going down!",
			"I'll spoil those good looks, back stabber."
		};

		[Constructable]
        public BadAsh() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			SpeechHue = Utility.RandomDyedHue();
			Body = 400;
			Name = "Bad Ash";
			SetStr( 486, 600 );
			SetDex( 481, 595 );
			SetInt( 861, 975 );
			SetDamage( 25, 43 );

			SetSkill( SkillName.Fencing, 100.0, 117.5 );
			SetSkill( SkillName.MagicResist, 95.0, 117.5 );
			SetSkill( SkillName.Tactics, 119.0, 120.0 );
			SetSkill( SkillName.Swords, 119.0, 120.0 );
			SetSkill( SkillName.Wrestling, 105.0, 127.5 );

            Fame = 250;
            Karma = -250;

			switch ( Utility.Random( 4 ) )
			{
				case 0: AddItem( new Scimitar() ); break;
				case 1: AddItem( new Longsword() ); break;
				case 2: AddItem( new Cutlass() ); break;
				case 3: AddItem( new Broadsword() ); break;
			}
			
			ChainChest chest = new ChainChest();
			chest.Hue = 1327;
			AddItem( chest );

			Shirt shirt = new Shirt();
			shirt.Hue = 1327;
			AddItem( shirt );

			LeatherArms arms = new LeatherArms();
			arms.Hue = 1327;
			AddItem( arms );

			LeatherLegs legs = new LeatherLegs();
			legs.Hue = 2056;
			AddItem( legs );

			BodySash sash = new BodySash();
			sash.Hue = 2056;
			AddItem( sash );

			Cloak cloak = new Cloak();
			cloak.Hue = 2056;
			AddItem( cloak );

			switch ( Utility.Random( 3 ) )
			{
				case 0: PackItem( new Ribs() ); break;
				case 1: PackItem( new Shaft() ); break;
				case 2: PackItem( new Candle() ); break;
			}

			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
		}
        
		public override void OnMovement( Mobile m, Point3D oldLocation ) 
		{
       		if( m_Talked == false ) 
        	{ 
      		    if ( m.InRange( this, 3 ) && m is PlayerMobile) 
				{                
            		m_Talked = true; 
            		SayRandom( BadAshSay, this ); 
					this.Move( GetDirectionTo( m.Location ) );
             		SpamTimer t = new SpamTimer(); 
           		   	t.Start(); 
            	} 
        	} 
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
		
        private static readonly double[] m_Offsets = new double[]
		{
			Math.Cos( 000.0 / 180.0 * Math.PI ), Math.Sin( 000.0 / 180.0 * Math.PI ),
			Math.Cos( 040.0 / 180.0 * Math.PI ), Math.Sin( 040.0 / 180.0 * Math.PI ),
			Math.Cos( 080.0 / 180.0 * Math.PI ), Math.Sin( 080.0 / 180.0 * Math.PI ),
			Math.Cos( 120.0 / 180.0 * Math.PI ), Math.Sin( 120.0 / 180.0 * Math.PI ),
			Math.Cos( 160.0 / 180.0 * Math.PI ), Math.Sin( 160.0 / 180.0 * Math.PI ),
			Math.Cos( 200.0 / 180.0 * Math.PI ), Math.Sin( 200.0 / 180.0 * Math.PI ),
			Math.Cos( 240.0 / 180.0 * Math.PI ), Math.Sin( 240.0 / 180.0 * Math.PI ),
			Math.Cos( 280.0 / 180.0 * Math.PI ), Math.Sin( 280.0 / 180.0 * Math.PI ),
			Math.Cos( 320.0 / 180.0 * Math.PI ), Math.Sin( 320.0 / 180.0 * Math.PI ),
		};

		public override bool AlwaysMurderer{ get{ return true; } }

        public void Morph()
        {
            if (m_TrueForm)
                return;

            m_TrueForm = true;
            Name = "Evil Ash";
            Body = 400;
            Hue = 1143;
            SetStr(586, 700);
            Hits = HitsMax;
            Stam = StamMax;
            Mana = ManaMax;
            SetDamage(50, 67);

            SetDamageType(ResistanceType.Physical, 100);
            SetResistance(ResistanceType.Physical, 20, 25);
            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 5, 10);
            SetResistance(ResistanceType.Poison, 5, 10);
            SetResistance(ResistanceType.Energy, 10, 15);
			SetSkill( SkillName.Fencing, 100.0, 117.5 );
			SetSkill( SkillName.MagicResist, 95.0, 117.5 );
			SetSkill( SkillName.Tactics, 119.0, 120.0 );
			SetSkill( SkillName.Swords, 120.0, 127.5 );
			SetSkill( SkillName.Wrestling, 119.0, 120.0 );

            Fame = 250;
            Karma = -250;
            VirtualArmor = 45;
			
			switch ( Utility.Random( 4 ) )
			{
				case 0: AddItem( new Scimitar() ); break;
				case 1: AddItem( new Longsword() ); break;
				case 2: AddItem( new Cutlass() ); break;
				case 3: AddItem( new Broadsword() ); break;
			}

			BoneHelm helm = new BoneHelm();
			helm.Hue = 962;
			AddItem( helm );

			BoneChest chest = new BoneChest();
			chest.Hue = 2056;
			AddItem( chest );

			Shirt shirt = new Shirt();
			shirt.Hue = 1327;
			AddItem( shirt );

			LeatherArms arms = new LeatherArms();
			arms.Hue = 1327;
			AddItem( arms );

			LeatherLegs legs = new LeatherLegs();
			legs.Hue = 2056;
			AddItem( legs );

			BoneGloves gloves = new BoneGloves();
			gloves.Hue = 2108;
			AddItem( gloves );

			Cloak cloak = new Cloak();
			cloak.Hue = 2056;
			AddItem( cloak );
            ProcessDelta();
            Say("Goody little TWO-SHOES!"); 
        }

		[CommandProperty( AccessLevel.GameMaster )]
		public override int HitsMax{ get{ return m_TrueForm ? 6000 : 12000; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public override int ManaMax{ get{ return 5000; } }

       	public BadAsh( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
			if ( m_TrueForm )
			{
				return base.OnBeforeDeath();
			}
			else
			{
				Morph();
				return false;
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			writer.Write( m_TrueForm );			
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_TrueForm = reader.ReadBool();
					break;
				}
			}
        }
	}
}