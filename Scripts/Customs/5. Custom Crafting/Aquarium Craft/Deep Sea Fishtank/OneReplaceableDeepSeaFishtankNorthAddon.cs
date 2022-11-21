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
	public class DeepSeaFishtankNReplaceableAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new DeepSeaFishtankNReplaceableAddonDeed();
			}
		}

		[ Constructable ]
		public DeepSeaFishtankNReplaceableAddon()
		{
			AddonComponent ac = null;
             
                  //Black on top of tank
			ac = new AddonComponent( 4846 );
			ac.Hue = 1;
			ac.Name = "deepsea fishtank lid";
			AddComponent( ac, 0, 0, 22 );

			//Black bottom of tank
			ac = new AddonComponent( 4846 );
			ac.Hue = 1;
			ac.Name = "deepsea fishtank base";
			AddComponent( ac, 0, 0, 0 );

			//Sand
			ac = new AddonComponent( 4846 );
			ac.Hue = 348;
			ac.Name = "sand";
			AddComponent( ac, 0, 0, 1 );

			//Water
			ac = new AddonComponent( 6732 );
			ac.Hue = 96;
			ac.Name = "water";
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 6732 );
			ac.Hue = 96;
			ac.Name = "water";
			AddComponent( ac, 0, -1, 0 );
		}

		public DeepSeaFishtankNReplaceableAddon( Serial serial ) : base( serial )
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

	public class DeepSeaFishtankNReplaceableAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DeepSeaFishtankNReplaceableAddon();
			}
		}

		[Constructable]
		public DeepSeaFishtankNReplaceableAddonDeed()
		{
			Name = "deed for a deep sea fishtank north piece with replaceable fish.";
		}

		public DeepSeaFishtankNReplaceableAddonDeed( Serial serial ) : base( serial )
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