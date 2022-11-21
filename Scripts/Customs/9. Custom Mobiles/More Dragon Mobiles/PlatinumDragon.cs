using System;
using Server;
using Server.Items;
using System.Collections;


namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class PlatinumDragon : BaseCreature
	{
	
        [Constructable]
		public PlatinumDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a platinum dragon";
			Body = 172;
			Hue = 1153;
            BaseSoundID = 362;

			SetStr( 2001, 2158 );
			SetDex( 250 );
			SetInt( 1052, 1106 );

			SetHits( 2502, 2704 );

			SetDamage( 50, 75 );

			SetDamageType( ResistanceType.Physical, 50 );
			
            SetResistance( ResistanceType.Physical, 80, 85 );
			SetResistance( ResistanceType.Fire, 80, 85 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 82, 87 );
			SetResistance( ResistanceType.Energy, 83, 88 );

			SetSkill( SkillName.EvalInt, 111.1, 115.0 );
			SetSkill( SkillName.Magery, 118.1, 120.0 );
			SetSkill( SkillName.Meditation, 117.5, 120.0 );
			SetSkill( SkillName.MagicResist, 116.5, 120.0 );
			SetSkill( SkillName.Tactics, 116.6, 120.0 );
			SetSkill( SkillName.Wrestling, 115.0, 120.0 );

		    Fame = 25000;
			Karma = -25000;

			VirtualArmor = 70;

            //Tamable = true;//Uncomment out this line to make tameable
            ControlSlots = 4;
            MinTameSkill = 119.1;
        }
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

			c.DropItem(new RewardScroll());
        }
		public override void GenerateLoot()
		{
            AddLoot( LootPack.AosSuperBoss, 8 );
			AddLoot( LootPack.Gems, 5 );
		}

		public override int GetIdleSound()
		{
			return 0x2D3;
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}

		public override bool ReacquireOnMovement{ get{ return true; } }
        public override bool BardImmune { get { return true; } }// Comment out this line to make it so u can peace
        public override bool Unprovokable { get { return true; } }
        public override bool Uncalmable { get { return true; } }
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return true; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override int Hides{ get{ return 40; } }
		public override int Meat{ get{ return 19; } }
		public override int Scales{ get{ return 12; } }
		public override ScaleType ScaleType{ get{ return (ScaleType)Utility.Random( 4 ); } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Utility.RandomBool() ? Poison.Deadly : Poison.Lethal; } } 
		public override int TreasureMapLevel{ get{ return 5; } }
    
        
            public PlatinumDragon( Serial serial ) : base( serial )
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