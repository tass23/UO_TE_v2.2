using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class SithHoundImprisonedInCrystal : BaseImprisonedMobile
	{
		public override BaseCreature Summon{ get{ return new SmallSithHound(); } }
		
		[Constructable]
		public SithHoundImprisonedInCrystal() : base( 0x1F1C )
		{
			Name = "a Sith Hound imprisoned in crystal";
			Weight = 1.0;
			Hue = 1788;
		}

		public SithHoundImprisonedInCrystal( Serial serial ) : base( serial )
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

namespace Server.Mobiles
{
	public class SmallSithHound : BaseCreature
	{
		public override bool DeleteOnRelease{ get{ return true; } }
		
		[Constructable]
		public SmallSithHound() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Sith Hound";
			Body = 739;
			Hue = 1788;
			SetStr( 41, 48 );
			SetDex( 55 );
			SetInt( 75 );
			SetHits( 45, 50 );

			SetDamage( 7, 9 );
			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 10, 14 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 21, 25 );
			SetResistance( ResistanceType.Energy, 20, 25 );
			SetSkill( SkillName.MagicResist, 4.0 );
			SetSkill( SkillName.Tactics, 4.0 );
			SetSkill( SkillName.Wrestling, 4.0 );

			Tamable = true;	
			ControlSlots = 1;
			MinTameSkill = 29.1;
		}

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		
		public SmallSithHound( Serial serial ) : base( serial )
		{
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1049646 ); // (summoned)
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