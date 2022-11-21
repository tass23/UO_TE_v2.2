/* Created by Makaar*/
/* Slightly Warped by Hammerhand*/

using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a fire rock elemental corpse" )]
	public class FireRockElemental : BaseCreature
	{
		[Constructable]
        public FireRockElemental()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
		{
			Name = "a FireRock Elemental";
			Body = 112;
			BaseSoundID = 268;

			Hue = 1258;

			SetStr( 286, 325 );
			SetDex( 226, 245 );
			SetInt( 171, 192 );

			SetHits( 1000, 1150 );

			SetDamage( 28 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Cold, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 80, 95 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 38;

			PackItem( new LargeFireRock(2) );
			PackItem( new SulfurousAsh( 3 ) );
           // PackItem(new IronOre(oreAmount));

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Gems, 4 );
		
                      switch (Utility.Random(10))  
              {
                  case 0: PackItem(new CrystalineFire());
                      break;
              }
               }
		public override bool AutoDispel{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
		public override bool HasBreath{ get{ return true; } } // fire breath enabled

		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( from != null )
			{
				int hitback = damage;
				AOS.Damage( from, this, hitback, 100, 0, 0, 0, 0 );
			}

			if ( from is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)from;

				if ( bc.Controlled || bc.BardTarget == this )
					damage = 0; // Immune to pets and provoked creatures
			}
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			reflect = true; // Every spell is reflected back to the caster
		}

		public FireRockElemental( Serial serial ) : base( serial )
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