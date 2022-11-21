using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a wolf spider spider corpse" )]
	public class WolfSpider : BaseCreature
	{
		[Constructable]
		public WolfSpider() : base( AIType.AI_Melee, FightMode.Evil, 10, 1, 0.2, 0.4 )
		{
			Name = "a Wolf spider";
			Body = 737;
            Hue = 1141;

			SetStr( 250, 255 );
			SetDex( 150, 155 );
			SetInt( 300, 310 );

			SetHits( 150, 160 );

			SetDamage( 15, 18 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Poison, 30 );

			SetResistance( ResistanceType.Physical, 30 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 30, 35 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 30, 35 );

			SetSkill( SkillName.Anatomy, 80.0, 85.0 );
			SetSkill( SkillName.MagicResist, 65.0, 70.0 );
			SetSkill( SkillName.Poisoning, 65.0, 70.0 );
			SetSkill( SkillName.Tactics, 85.0, 90.0 );
			SetSkill( SkillName.Wrestling, 90.0, 95.0 );
            SetSkill( SkillName.Hiding, 105.0, 110.0 );
            SetSkill( SkillName.Stealth, 105.0, 110.0 );

            Tamable = true;
            ControlSlots = 1;
            MinTameSkill = 59.1;


            AddItem(new Gold(300));
            PackItem(new SpidersSilk(8));
            PackMagicItems(1, 2);

		}

		public override void GenerateLoot()
		{
            AddLoot(LootPack.Gems, 2);
		}

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.15)
                c.DropItem(new BottleIchor());
        }

		public override int GetIdleSound() { return 1605; } 
		public override int GetAngerSound() { return 1602; } 
		public override int GetHurtSound() { return 1604; } 
		public override int GetDeathSound()	{ return 1603; }

        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Arachnid; } }
        public override Poison PoisonImmune { get { return Poison.Regular; } }
        public override Poison HitPoison { get { return Poison.Regular; } }

		public WolfSpider( Serial serial ) : base( serial )
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