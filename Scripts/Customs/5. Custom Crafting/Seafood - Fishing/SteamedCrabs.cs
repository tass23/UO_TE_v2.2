using System;
using System.Collections;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class SteamedCrab : Food//, ICarvable
	{

		[Constructable]
		public SteamedCrab() : this( 1 )
		{
		}

		[Constructable]
		public SteamedCrab( int amount ) : base( amount, 0x44D1 )
		{
			Name = "Steamed Crab";
			Hue = 33;
			this.Weight = 1.0;
			this.FillFactor = 10;
		}


		public SteamedCrab( Serial serial ) : base( serial )
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
	
	public class SteamedBlueCrab : Food//, ICarvable
	{

		[Constructable]
		public SteamedBlueCrab() : this( 1 )
		{
		}

		[Constructable]
		public SteamedBlueCrab( int amount ) : base( amount, 0x44D1 )
		{
			Name = "Steamed Blue Crab";
			Hue = 34;
			this.Weight = 1.0;
			this.FillFactor = 10;
		}
		

		public SteamedBlueCrab( Serial serial ) : base( serial )
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
	
	public class SteamedSnowCrab : Food//, ICarvable
	{

		[Constructable]
		public SteamedSnowCrab() : this( 1 )
		{
		}

		[Constructable]
		public SteamedSnowCrab( int amount ) : base( amount, 0x44D2 )
		{
			Name = "Steamed Snow Crab";
			Hue = 35;
			this.Weight = 1.0;
			this.FillFactor = 10;
		}


		public SteamedSnowCrab( Serial serial ) : base( serial )
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
	
	public class SteamedRedRockCrab : Food//, ICarvable
	{

		[Constructable]
		public SteamedRedRockCrab() : this( 1 )
		{
		}

		[Constructable]
		public SteamedRedRockCrab( int amount ) : base(amount, 0x44D2 )
		{
			Name = "Steamed Red Rock Crab";
			Hue = 32;
			this.Weight = 1.0;
			this.FillFactor = 10;
		}

	
		public SteamedRedRockCrab( Serial serial ) : base( serial )
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
	
	/*public class Prawn : Food//, ICarvable
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
	}*/
}
