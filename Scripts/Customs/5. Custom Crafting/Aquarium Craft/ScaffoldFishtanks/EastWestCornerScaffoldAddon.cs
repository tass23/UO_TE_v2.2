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
	public class EastWestCornerScaffoldAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new EastWestCornerScaffoldAddonDeed();
			}
		}

		[ Constructable ]
		public EastWestCornerScaffoldAddon()
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
			AddComponent( ac, 1, 0, 22 );
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
			AddComponent( ac, 1, 0, 1 );
	//Water
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
		//Bottom North 2
			ac = new AddonComponent( 6732 );
			ac.Name = "water";
			ac.Hue = 96;
			AddComponent( ac, 1, -1, 0 );
		//Top North 2
			ac = new AddonComponent( 6732 );
			ac.Name = "water";
			ac.Hue = 96;
			AddComponent( ac, 1, -1, 11 );
	//Scaffold
		//Top
			ac = new AddonComponent( 4808 );
			ac.Name = "fishtank";
			AddComponent( ac, 0, 0, 0 );
		//Bottom
			ac = new AddonComponent( 4809 );
			ac.Name = "fishtank";
			AddComponent( ac, 1, 0, 0 );

		}

		public EastWestCornerScaffoldAddon( Serial serial ) : base( serial )
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

	public class EastWestCornerScaffoldAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new EastWestCornerScaffoldAddon();
			}
		}

		[Constructable]
		public EastWestCornerScaffoldAddonDeed()
		{
			Name = "a east west corner fishtank deed";
		}

		public EastWestCornerScaffoldAddonDeed( Serial serial ) : base( serial )
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