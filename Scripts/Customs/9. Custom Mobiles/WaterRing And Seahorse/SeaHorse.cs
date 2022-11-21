using System;
using Server.Mobiles;
using Server.Network;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a sea horse corpse" )]
	public class SeaHorse : BaseMount
	{
		[Constructable]
		public SeaHorse() : this( "a sea horse" )
		{
		}

		[Constructable]
		public SeaHorse( string name ) : base( name, 0x90, 0x3EB3, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			InitStats( Utility.Random( 50, 30 ), Utility.Random( 50, 30 ), 10 );
			Skills[SkillName.MagicResist].Base = 25.0 + (Utility.RandomDouble() * 5.0);
			Skills[SkillName.Wrestling].Base = 35.0 + (Utility.RandomDouble() * 10.0);
			Skills[SkillName.Tactics].Base = 30.0 + (Utility.RandomDouble() * 15.0);
			Fame = 20000;
			Karma = -20000;
			
			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 30;

			PackGold( 300 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		

			if ( Utility.RandomDouble() < 0.15 )
				c.DropItem(new WaterRing());
		}
		
		public SeaHorse( Serial serial ) : base( serial )
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