using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a puddle of water" )]
	public class RidableLargeBeetle : BaseMount
	{
		[Constructable]
		public RidableLargeBeetle() : this( null )
		{
		}

		[Constructable]
		public RidableLargeBeetle( string name ) : base( name, 220, 0x3EA6, AIType.AI_Animal, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if ( Utility.RandomDouble() < 0.25 )
			{
				Body = 222;
				Hue = 1594;
				Name = "an infused water elemental";
			}
			else if ( Utility.RandomDouble() < 0.15 )
			{
				Body = 226;
				Hue = 1459;
				Name = "an infused water elemental";
			}
			else if ( Utility.RandomDouble() < 0.10 )
			{
				Body = 204;
				Hue = 1589;
				Name = "an infused water elemental";
			}
			else if ( Utility.RandomDouble() < 0.05 )
			{
				Body = 164;
				Hue = 1589;
				Name = "an infused energy vortex";
			}
			else
			{
				Body = 220;
				Hue = 1594;
				Name = "an infused water elemental";
			}
			
			ActiveSpeed = 0.1;
			PassiveSpeed = 0.2;
			
			AddItem(new LightSource());
			
			BaseSoundID = 0x3F3;

			SetStr( 401, 460 );
			SetDex( 121, 170 );
			SetInt( 376, 450 );

			SetHits( 1201, 1360 );

			SetDamage( 25, 31 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Poison, 10 );
			SetDamageType( ResistanceType.Fire, 70 );

			SetResistance( ResistanceType.Physical, 40, 65 );
			SetResistance( ResistanceType.Fire, 55, 70 );
			SetResistance( ResistanceType.Cold, 35, 50 );
			SetResistance( ResistanceType.Poison, 75, 95 );
			SetResistance( ResistanceType.Energy, 40, 60 );

			SetSkill( SkillName.EvalInt, 100.1, 125.0 );
			SetSkill( SkillName.Magery, 100.1, 110.0 );
			SetSkill( SkillName.Poisoning, 120.1, 140.0 );
			SetSkill( SkillName.MagicResist, 95.1, 110.0 );
			SetSkill( SkillName.Tactics, 90.1, 101.0 );
			SetSkill( SkillName.Wrestling, 100.1, 110.5 );

			Fame = 300;
			Karma = -300;

			Tamable = false;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public override int GetAngerSound()
		{
			return 0x15;
		}

		public override int GetAttackSound()
		{
			return 0x28;
		}
		
		public override void OnThink()
		{
			base.OnThink();
			
			if ((Rider == null) && (Combatant == null) && (ControlMaster == null))
			{
				this.Delete();	
			}
		}
		
		public RidableLargeBeetle( Serial serial ) : base( serial )
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