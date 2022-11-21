/////////////////////////////////////////////////
//                                             //
//             BurialSystem                    //
//               by LIACS                      //
//              02/19/2007                     //
/////////////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class Grave : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new GraveDeed();
			}
		}

		[ Constructable ]
		public Grave()
		{
            switch (Utility.Random(2))
            {
                case 0:
				{
					AddComponent(new AddonComponent(3795), 0, 1, 0);
					AddComponent(new AddonComponent(3809), 0, 0, 0);
					//AddonComponent ac;
					break;
				}
                case 1:
				{

					AddComponent(new AddonComponent(3810), 0, 0, 0);
					AddComponent(new AddonComponent(3808), 1, 0, 0);
					//AddonComponent ac;
					break;
				}
                default: break;
            }
		}

		public Grave( Serial serial ) : base( serial )
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

	public class GraveDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Grave();
			}
		}

		//[Constructable]
		public GraveDeed()
		{
			Name = "a grave";
		}

		public GraveDeed( Serial serial ) : base( serial )
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