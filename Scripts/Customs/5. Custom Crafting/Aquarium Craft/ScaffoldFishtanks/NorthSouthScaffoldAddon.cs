/////////////////////////////////////////////////
//
// Created by Morrigan & Ashlar together forever. 
//
/////////////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class NorthSouthScaffoldAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new NorthSouthScaffoldAddonDeed();
			}
		}

		[ Constructable ]
		public NorthSouthScaffoldAddon()
		{
			AddonComponent ac = null;
	//Fishtank
		//Top
			ac = new AddonComponent( 4846 );
			ac.Name = "water";
			ac.Hue = 96;
			AddComponent( ac, 0, 0, 22 );
		//Bottom
			ac = new AddonComponent( 4846 );
			ac.Name = "water";
			ac.Hue = 96;
			AddComponent( ac, 0, 1, 22 );
	//Sand
		//Top
			ac = new AddonComponent( 4846 );
			ac.Name = "sand";
			ac.Hue = 348;
			AddComponent( ac, 0, 0, 1 );
		//Bottom
			ac = new AddonComponent( 4846 );
			ac.Name = "sand";
			ac.Hue = 348;
			AddComponent( ac, 0, 1, 1 );
	//Water
		//Bottom North
			ac = new AddonComponent( 6732 );
			ac.Name = "water";
			ac.Hue = 96;
			AddComponent( ac, 0, -1, 0 );
		//Top North
			ac = new AddonComponent( 6732 );
			ac.Name = "water";
			ac.Hue = 96;
			AddComponent( ac, 0, -1, 11 );
	//Scaffold
		//Top
			ac = new AddonComponent( 4786 );
			ac.Name = "fishtank";
			AddComponent( ac, 0, 0, 0 );
		//Bottom
			ac = new AddonComponent( 4785 );
			ac.Name = "fishtank";
			AddComponent( ac, 0, 1, 0 );

		}

		public NorthSouthScaffoldAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class NorthSouthScaffoldAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new NorthSouthScaffoldAddon();
			}
		}

		[Constructable]
		public NorthSouthScaffoldAddonDeed()
		{
			Name = "a north south fishtank deed";
		}

		public NorthSouthScaffoldAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}