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
	public class DeepSeaNWithGlassAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new DeepSeaNWithGlassAddonDeed();
			}
		}

		[ Constructable ]
		public DeepSeaNWithGlassAddon()
		{
			AddonComponent ac = null;
	//Fishtank
		//Top
			ac = new AddonComponent( 4846 );
			ac.Name = "Water";
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
	//Fields
		//South
			ac = new AddonComponent( 14695 );
			ac.Name = "glass";
			ac.Hue = 96;
			AddComponent( ac, 0, 1, 0 );

		}

		public DeepSeaNWithGlassAddon( Serial serial ) : base( serial )
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

	public class DeepSeaNWithGlassAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DeepSeaNWithGlassAddon();
			}
		}

		[Constructable]
		public DeepSeaNWithGlassAddonDeed()
		{
			Name = "Deep Sea Fishtank North Piece deed";
		}

		public DeepSeaNWithGlassAddonDeed( Serial serial ) : base( serial )
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