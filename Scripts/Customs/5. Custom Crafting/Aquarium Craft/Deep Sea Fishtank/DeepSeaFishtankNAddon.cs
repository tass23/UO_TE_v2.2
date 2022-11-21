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
	public class DeepSeaFishtankNAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new DeepSeaFishtankNAddonDeed();
			}
		}

		[ Constructable ]
		public DeepSeaFishtankNAddon()
		{
			AddonComponent ac = null;
             
                  //Black on top of tank
			ac = new AddonComponent( 4846 );
			ac.Hue = 1;
			ac.Name = "deepsea fishtank lid";
			AddComponent( ac, 1, 1, 22 );

			//Black bottom of tank
			ac = new AddonComponent( 4846 );
			ac.Hue = 1;
			ac.Name = "deepsea fishtank base";
			AddComponent( ac, 1, 1, 0 );

			//Sand
			ac = new AddonComponent( 4846 );
			ac.Hue = 348;
			ac.Name = "sand";
			AddComponent( ac, 1, 1, 1 );

			//Water
			ac = new AddonComponent( 6732 );
			ac.Hue = 96;
			ac.Name = "water";
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 6732 );
			ac.Hue = 96;
			ac.Name = "water";
			AddComponent( ac, 1, 0, 0 );
		}

		public DeepSeaFishtankNAddon( Serial serial ) : base( serial )
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

	public class DeepSeaFishtankNAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DeepSeaFishtankNAddon();
			}
		}

		[Constructable]
		public DeepSeaFishtankNAddonDeed()
		{
			Name = "deed for a deep sea fishtank north piece";
		}

		public DeepSeaFishtankNAddonDeed( Serial serial ) : base( serial )
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