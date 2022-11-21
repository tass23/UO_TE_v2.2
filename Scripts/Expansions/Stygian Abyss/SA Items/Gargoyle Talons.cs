using System;

namespace Server.Items
{
	[FlipableAttribute( 0x41D8, 0x41D9 )]
	public class LeatherTalons : BaseShoes
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }
		
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public LeatherTalons() : this( 0 )
		{
		}

		[Constructable]
		public LeatherTalons( int hue ) : base( 0x41D8, hue )
		{
			Weight = 3.0;
		}

		public LeatherTalons( Serial serial ) : base( serial )
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
	
	[FlipableAttribute( 0x42DE, 0x42DF )]
	public class PlateTalons : BaseShoes
	{
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }
		
		public override CraftResource DefaultResource{ get{ return CraftResource.Iron; } }

		[Constructable]
		public PlateTalons() : this( 0 )
		{
		}

		[Constructable]
		public PlateTalons( int hue ) : base( 0x42DE, hue )
		{
			Weight = 5.0;
		}

		public PlateTalons( Serial serial ) : base( serial )
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