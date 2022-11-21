using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class Crab : Item//, ICarvable
	{

		[Constructable]
		public Crab() : this( 1 )
		{
		}

		[Constructable]
		public Crab( int amount ) : base( 0x44D1 )
		{
			Name = "Crab";
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}


		public Crab( Serial serial ) : base( serial )
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
	
	public class BlueCrab : Item//, ICarvable
	{

		[Constructable]
		public BlueCrab() : this( 1 )
		{
		}

		[Constructable]
		public BlueCrab( int amount ) : base( 0x44D1 )
		{
			Name = "Blue Crab";
			Hue = 201;
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}
		

		public BlueCrab( Serial serial ) : base( serial )
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
	
	public class SnowCrab : Item//, ICarvable
	{

		[Constructable]
		public SnowCrab() : this( 1 )
		{
		}

		[Constructable]
		public SnowCrab( int amount ) : base( 0x44D2 )
		{
			Name = "Snow Crab";
			Hue = 1151;
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}


		public SnowCrab( Serial serial ) : base( serial )
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
	
	public class RedRockCrab : Item//, ICarvable
	{

		[Constructable]
		public RedRockCrab() : this( 1 )
		{
		}

		[Constructable]
		public RedRockCrab( int amount ) : base(0x44D2 )
		{
			Name = "Red Rock Crab";
			Hue = 1172;
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

	
		public RedRockCrab( Serial serial ) : base( serial )
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
	
	public class Prawn : Item//, ICarvable
	{

		[Constructable]
		public Prawn() : base( 0x3B14 )
		{
			Name = "Prawn";
			Weight = 0.1;

		}


		public Prawn( Serial serial ) : base( serial )
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
