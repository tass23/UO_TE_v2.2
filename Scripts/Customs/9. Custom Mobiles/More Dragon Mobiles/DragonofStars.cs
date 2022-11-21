using System;
using Server;
using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a dragon of stars corpse" )]
	public class DragonofStars : BaseCreature
	{
	
        [Constructable]
		public DragonofStars () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a dragon of stars";
			Body = 104;
			Hue = 1179;
            BaseSoundID = 0x488;

			SetStr( 898, 1030 );
			SetDex( 68, 200 );
			SetInt( 488, 620 );

			SetHits( 822, 983 );

			SetDamage( 35, 51 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 75, 80 );
			SetResistance( ResistanceType.Fire, 40, 60 );
			SetResistance( ResistanceType.Cold, 40, 60 );
			SetResistance( ResistanceType.Poison, 70, 80 );
			SetResistance( ResistanceType.Energy, 40, 60 );

			SetSkill( SkillName.EvalInt, 80.1, 110.0 );
			SetSkill( SkillName.Magery, 80.1, 110.0 );
			SetSkill( SkillName.MagicResist, 100.3, 120.0 );
			SetSkill( SkillName.Tactics, 97.6, 110.0 );
			SetSkill( SkillName.Wrestling, 97.6, 110.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 80;
		    
            //Tameable = True;//uncomment out this line to make tameable
            ControlSlots = 4;
            MinTameSkill = 116.7;
        }
        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

			c.DropItem(new RewardScroll());
        }
		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 4 );
			AddLoot( LootPack.Gems, 5 );
		}

		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } } // fire breath 
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 100; } }
		public override int BreathEffectHue{ get{ return 0x480; } }
		public override double BonusPetDamageScalar{ get{ return (Core.SE)? 3.0 : 1.0; } }
		
        public override bool AutoDispel{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }

		public DragonofStars( Serial serial ) : base( serial )
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