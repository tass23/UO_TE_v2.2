using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class Lobster : Item//, ICarvable
	{

		[Constructable]
		public Lobster() : this( 1 )
		{
		}

		[Constructable]
		public Lobster( int amount ) : base( 0x44D3 )
		{
			Name = "Lobster";
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		

		public Lobster( Serial serial ) : base( serial )
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
	
	public class RockLobster : Item//, ICarvable
	{

		[Constructable]
		public RockLobster() : this( 1 )
		{
		}

		[Constructable]
		public RockLobster( int amount ) : base( 0x44D4 )
		{
			Name = "Rock Lobster";
			Hue = 1421;
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		

		public RockLobster( Serial serial ) : base( serial )
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
	
	public class SpineyLobster : Item//, ICarvable
	{

		[Constructable]
		public SpineyLobster() : this( 1 )
		{
		}

		[Constructable]
		public SpineyLobster( int amount ) : base( 0x44D3 )
		{
			Name = "Spiney Lobster";
			Hue = 2959;
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		

		public SpineyLobster( Serial serial ) : base( serial )
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
