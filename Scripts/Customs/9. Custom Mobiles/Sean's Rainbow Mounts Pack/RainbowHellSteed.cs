/*
 created by:
     /\       
____/_ \____  ### ### ### ### #  ### ### # ##  ##  ###
\  ___\ \  /  #   #   # # # # #  # # # # # # # # # #
 \/ /  \/ /   ### ##  ### # # #  ### # # # # # ##  ##
 / /\__/_/\     # #   # # # # #  # # # # # # # # # #
/__\ \_____\  ### ### # # # ###  # # # ### ##  # # ###
    \  /             http://www.wftpradio.net/
     \/       
*/
using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a rainbow corpse" )]
	public class RainbowHellSteed : BaseMount
	{
		private static int[] m_IDs = new int[]
			{
				0xC8, 0x3E9F,
				0xE2, 0x3EA0,
				0xE4, 0x3EA1,
				0xCC, 0x3EA2
			};

        [Constructable]
		public RainbowHellSteed() : this( "a rainbow Hell Steed" )
		{
		}

		[Constructable]
		public RainbowHellSteed( string name ) : base( name, 0xE2, 0x3EA0, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			int random = Utility.Random( 4 );

			Body = 0x319;
			ItemID = 0x3EBB;
			BaseSoundID = 0x0;
            Hue = 1289;

			SetStr( 301, 310 );
			SetDex( 201, 210 );
			SetInt( 201, 215 );

			SetHits( 301, 320 );

			SetDamage( 30, 34 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Fire, 75 );

			SetResistance( ResistanceType.Physical, 70, 80 );
			SetResistance( ResistanceType.Fire, 90 );
			SetResistance( ResistanceType.Poison, 100 );

			SetSkill( SkillName.MagicResist, 100.1, 120.0 );
			SetSkill( SkillName.Tactics, 75.0 );
			SetSkill( SkillName.Wrestling, 100.1, 120.0 );

			Fame = 0;
			Karma = 0;

            Tamable = true;
            ControlSlots = 3;//set the control slots required here
            MinTameSkill = 108.7;//Set min taming skill here
		}

        public override bool SubdueBeforeTame { get { return false; } }//Add or remove any other things you want the steed to do.
        public override bool CanRummageCorpses { get { return false; } }
        public override bool HasBreath { get { return true; } }
		public override bool CanAngerOnTame{ get { return false; } }
		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 10; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }
        
        public override void GenerateLoot()//Edit what you want your drops to be below:
        {
            PackGold(5, 20);
            PackArmor(1, 2);
            PackWeapon(1, 2);
            PackWeapon(1, 2);
            PackSlayer();
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            if (!from.Alive && from.InRange(this.Location, 12))//I put the commands like this so it doesn't spam players.
            {
                this.Say("Double click me");
                this.Say("out of war mode");
                this.Say("to be resurrected!");
            }
            return base.HandlesOnSpeech(from);
        }

        public override void OnDoubleClickDead(Mobile from)//Edit what you want the steed to say when resurrecting below:
        {
            this.Say("There you go, Enjoy life!");
            if (!from.Alive)
            {
                from.Resurrect();
            }
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (!e.Handled && e.Mobile == ControlMaster && e.Mobile.InRange(this.Location, 5))//Edit/add what ever color you want below:
            {
                if (e.Speech == "change random")
                {
                    this.Say("As you wish!");
                    this.Hue = Utility.Random(2, 1200);//Edit which colors you want him to cycle through here.
                }
                if (e.Speech == "change red")
                {
                    this.Say("As you wish!");
                    this.Hue = 33;
                }
                if (e.Speech == "change black")
                {
                    this.Say("As you wish!");
                    this.Hue = 1;
                }
                if (e.Speech == "change blue")
                {
                    this.Say("As you wish!");
                    this.Hue = 2;
                }
                if (e.Speech == "change pink")
                {
                    this.Say("As you wish!");
                    this.Hue = 26;
                }
                if (e.Speech == "change orange")
                {
                    this.Say("As you wish!");
                    this.Hue = 45;
                }
                if (e.Speech == "change yellow")
                {
                    this.Say("As you wish!");
                    this.Hue = 55;
                }
                if (e.Speech == "change purple")
                {
                    this.Say("As you wish!");
                    this.Hue = 117;
                }
                if (e.Speech == "change green")
                {
                    this.Say("As you wish!");
                    this.Hue = 66;
                }
                if (e.Speech == "change brown")
                {
                    this.Say("As you wish!");
                    this.Hue = 1044;
                }
                if (e.Speech == "change gray")
                {
                    this.Say("As you wish!");
                    this.Hue = 941;
                }
                if (e.Speech == "change normal")
                {
                    this.Say("As you wish!");
                    this.Hue = 1289;
                }
                if (e.Speech == "change pumpkin")
                {
                    this.Say("As you wish!");
                    this.Hue = 1554;
                }
                if (e.Speech == "change green apple")
                {
                    this.Say("As you wish!");
                    this.Hue = 1755;
                }
                if (e.Speech == "change red rose")
                {
                    this.Say("As you wish!");
                    this.Hue = 1756;
                }
                if (e.Speech == "change sunshine")
                {
                    this.Say("As you wish!");
                    this.Hue = 1757;
                }
                if (e.Speech == "change blue velvet")
                {
                    this.Say("As you wish!");
                    this.Hue = 1758;
                }
                if (e.Speech == "change sky blue")
                {
                    this.Say("As you wish!");
                    this.Hue = 1759;
                }
                if (e.Speech == "change vampire slayer")
                {
                    this.Say("As you wish!");
                    this.Hue = 1760;
                }
                base.OnSpeech(e);
            }
        }
		public RainbowHellSteed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
	}
}