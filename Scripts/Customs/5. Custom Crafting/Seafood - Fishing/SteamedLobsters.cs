using System;
using System.Collections;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class SteamedLobster : Food//, ICarvable
	{

		[Constructable]
		public SteamedLobster() : this( 1 )
		{
		}

		[Constructable]
		public SteamedLobster( int amount ) : base( amount, 0x44D3 )
		{
			Name = "Steamed Lobster";
			Hue = 33;
			this.Weight = 1.0;
			this.FillFactor = 10;
		}


		public SteamedLobster( Serial serial ) : base( serial )
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
	
	public class SteamedRockLobster : Food//, ICarvable
	{

		[Constructable]
		public SteamedRockLobster() : this( 1 )
		{
		}

		[Constructable]
		public SteamedRockLobster( int amount ) : base( amount, 0x44D4 )
		{
			Name = "Steamed Rock Lobster";
			Hue = 34;
			this.Weight = 1.0;
			this.FillFactor = 10;
		}
		

		public SteamedRockLobster( Serial serial ) : base( serial )
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
	
	public class SteamedSpineyLobster : Food//, ICarvable
	{

		[Constructable]
		public SteamedSpineyLobster() : this( 1 )
		{
		}

		[Constructable]
		public SteamedSpineyLobster( int amount ) : base( amount, 0x44D3 )
		{
			Name = "Steamed Spiney Lobster";
			Hue = 35;
			this.Weight = 1.0;
			this.FillFactor = 10;
		}


		public SteamedSpineyLobster( Serial serial ) : base( serial )
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
