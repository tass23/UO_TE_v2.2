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
	public class NorthSouthCornerScaffoldAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new NorthSouthCornerScaffoldAddonDeed();
			}
		}

		[ Constructable ]
		public NorthSouthCornerScaffoldAddon()
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
			AddComponent( ac, 0, 0, 0 );
		//Bottom
			ac = new AddonComponent( 4846 );
			ac.Name = "sand";
			ac.Hue = 348;
			AddComponent( ac, 0, 1, 0 );
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
		//Bottom West
			ac = new AddonComponent( 6686 );
			ac.Name = "water";
			ac.Hue = 96;
			AddComponent( ac, -1, 0, 0 );
		//Top West
			ac = new AddonComponent( 6686 );
			ac.Name = "water";
			ac.Hue = 96;
			AddComponent( ac, -1, 0, 11 );
		//Bottom West 2
			ac = new AddonComponent( 6686 );
			ac.Name = "water";
			ac.Hue = 96;
			AddComponent( ac, -1, 1, 0 );
		//Top West 2
			ac = new AddonComponent( 6686 );
			ac.Name = "water";
			ac.Hue = 96;
			AddComponent( ac, -1, 1, 11 );
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

		public NorthSouthCornerScaffoldAddon( Serial serial ) : base( serial )
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

	public class NorthSouthCornerScaffoldAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new NorthSouthCornerScaffoldAddon();
			}
		}

		[Constructable]
		public NorthSouthCornerScaffoldAddonDeed()
		{
			Name = "a north south corner fishtank deed";
		}

		public NorthSouthCornerScaffoldAddonDeed( Serial serial ) : base( serial )
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