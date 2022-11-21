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
	public class EastWestScaffoldAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new EastWestScaffoldAddonDeed();
			}
		}

		[ Constructable ]
		public EastWestScaffoldAddon()
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

		public EastWestScaffoldAddon( Serial serial ) : base( serial )
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

	public class EastWestScaffoldAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new EastWestScaffoldAddon();
			}
		}

		[Constructable]
		public EastWestScaffoldAddonDeed()
		{
			Name = "a east west fishtank deed";
		}

		public EastWestScaffoldAddonDeed( Serial serial ) : base( serial )
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