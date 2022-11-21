/////////////////////////////////////////////////////
//
// Created by Morrigan and Ashlar - together forever
//
/////////////////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DeepSeaNWCornerWithGlassAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new DeepSeaNWCornerWithGlassAddonDeed();
			}
		}

		[ Constructable ]
		public DeepSeaNWCornerWithGlassAddon()
		{
			AddonComponent ac = null;
	//Fishtank
		//Top
			ac = new AddonComponent( 4846 );
			ac.Name = "top";
			ac.Hue = 96;
			AddComponent( ac, 0, 0, 25 );
		//Base
			ac = new AddonComponent( 4846 );
			ac.Name = "base";
			ac.Hue = 1;
			AddComponent( ac, 0, 0, 0 );
		//Sand
			ac = new AddonComponent( 4846 );
			ac.Name = "sand";
			ac.Hue = 348;
			AddComponent( ac, 0, 0, 1 );
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
	//Fields
		//South
			ac = new AddonComponent( 14695 );
			ac.Name = "glass";
			ac.Hue = 96;
			AddComponent( ac, 0, 1, 0 );
		//East
			ac = new AddonComponent( 14713 );
			ac.Name = "glass";
			ac.Hue = 96;
			AddComponent( ac, 1, 0, 0 );
		}

		public DeepSeaNWCornerWithGlassAddon( Serial serial ) : base( serial )
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

	public class DeepSeaNWCornerWithGlassAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DeepSeaNWCornerWithGlassAddon();
			}
		}

		[Constructable]
		public DeepSeaNWCornerWithGlassAddonDeed()
		{
			Name = "Deep Sea Fishtank NorthWest Corner deed";
		}

		public DeepSeaNWCornerWithGlassAddonDeed( Serial serial ) : base( serial )
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