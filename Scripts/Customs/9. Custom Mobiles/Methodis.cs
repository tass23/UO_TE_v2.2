using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a corpse of Methodis" )]
	public class Methodis : BaseCreature
    {
    	[Constructable]
        public Methodis() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
        	Name = "Methodis";
            Hue = 2101;
            Body = 38;
       		BaseSoundID = 0;
          
           	SetStr( 1900, 2100 ); //change this for more or less str
            SetDex( 1250, 1350 );  //change this for more or less dex
            SetInt( 700, 850 );  //change this for more or less int

            SetHits( 2000, 2500 );  //change this for more or less hit points
            SetMana( 1000, 1250 ); // mana
            SetStam( 500, 750 );  //stamina

           	SetDamage( 25, 35 );  //min damage, max damage

            SetDamageType( ResistanceType.Physical, 50 );
            SetDamageType( ResistanceType.Cold, 50 );
            SetDamageType( ResistanceType.Energy, 40 );

            SetResistance( ResistanceType.Physical, 60, 70 );
            SetResistance( ResistanceType.Cold, 80, 90 );
            SetResistance( ResistanceType.Fire, 15, 30 );
            SetResistance( ResistanceType.Energy, 40, 60 );
            SetResistance( ResistanceType.Poison, 50, 60 );

            SetSkill( SkillName.EvalInt, 110, 120 );
            SetSkill( SkillName.Magery, 110, 115 );
            SetSkill( SkillName.Meditation, 110, 120 );
            SetSkill( SkillName.MagicResist, 95, 100 );
            SetSkill( SkillName.Wrestling, 110, 120 );

            Fame = 2500;
            Karma = -2500;
            VirtualArmor = 70;
            Tamable = false;
            
            switch ( Utility.Random( 5 ))
            {                                   
				case 0: AddItem( new GorgetOfMethodis() ); break;
				case 1: AddItem( new HelmetofMethodis() ); break;
				case 2: AddItem( new ArmsOfMethodis() ); break;
				case 3: AddItem( new ChestOfMethodis() ); break;
				case 4: AddItem( new GlovesOfMethodis() ); break;
				case 5: AddItem( new LegsOfMethodis() ); break;
				case 6: AddItem( new ShieldofMethodis() ); break;
				case 7: AddItem( new StickOfMethodis() ); break;
				case 8: AddItem( new EarringsofMethodis() ); break;
				case 9: AddItem( new NecklaceofMethodis() ); break;
				case 10: AddItem( new BraceletofMethodis() ); break;
				case 11: AddItem( new RingofMethodis() ); break;
				//case 12: AddItem( new () ); break;
            }
        }
            
        public override void GenerateLoot()
        {
            PackGold( 5000, 8000 );
            AddLoot( LootPack.FilthyRich, 2);
            AddLoot( LootPack.Gems, Utility.Random( 1, 5));
        }
          
        public override bool ReacquireOnMovement{ get{ return true; }}
        public override int TreasureMapLevel{ get{ return 6; }}
        public override int Meat{ get{ return 19; }}
        public override int Hides{ get{ return 20; }}
        public override HideType HideType{ get{ return HideType.Barbed; }}
        public override int Scales{ get{ return 125; }}
        public override ScaleType ScaleType{ get{ return ScaleType.Black; }}
        public override bool BardImmune{ get{ return true; } }

		public Methodis( Serial serial ) : base( serial )
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
			Body = 38;
        }
    }
}