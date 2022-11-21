using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class GhostlySteed : BaseMount
	{
		[Constructable]
		public GhostlySteed() : this( "a ghostly steed" )
		{
		}

		[Constructable]
		public GhostlySteed( string name ) : base( name, 0xE4, 0x3EA7, AIType.AI_Necro, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 362;
			Hue = 23001;
			SetStr( 296, 325 );
			SetDex( 86, 105 );
			SetInt( 86, 125 );
			SetHits( 198, 215 );
			SetDamage( 12, 18 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Fire, 40 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.EvalInt, 10.4, 50.0 );
			SetSkill( SkillName.Magery, 10.4, 50.0 );
			SetSkill( SkillName.MagicResist, 85.3, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 80.5, 92.5 );

			Fame = 1000;
			Karma = -1000;
			VirtualArmor = 60;
			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 75.1;

			switch ( Utility.Random( 3 ) )
			{
				case 0:
				{
					BodyValue = 115;
					ItemID = 16039;
					break;
				}
				case 1:
				{
					BodyValue = 116;
					ItemID = 16041;
					break;
				}
				case 2:
				{
					BodyValue = 178;
					ItemID = 16055;
					break;
				}
			}

			PackGem();
			PackGold( 250, 350 );
			PackItem( new SulfurousAsh( Utility.RandomMinMax( 3, 5 ) ) );
			PackScroll( 1, 5 );
			PackPotion();

			PackNecroScroll( 1 ); // Blood Oath
			PackNecroScroll( 10 ); // Strangle
			PackNecroScroll( 5 ); // Horrific Beast
			PackNecroScroll( 13 ); // Vengeful Spirit
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 100; } }
		public override int BreathEffectHue{ get{ return 1150; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		
		public override bool IsEnemy( Mobile m )
		{
			if (m is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile) m;
				if (pm.Vampire > 0) return false;
			}
			return base.IsEnemy(m);
		}

		public GhostlySteed( Serial serial ) : base( serial )
		{
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