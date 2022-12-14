////////////////////////////////////////
//                                    //
//   Generated by CEO's YAAAG - V1.2  //
// (Yet Another Arya Addon Generator) //
//                                    //
////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EvilDeadTrapdoorAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {1203, 1, 0, 0}, {1204, 0, 0, 0}, {1202, 0, 1, 0}// 1	2	3	
			, {1201, 1, 1, 0}// 4	
		};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new EvilDeadTrapdoorAddonDeed();
			}
		}

		[ Constructable ]
		public EvilDeadTrapdoorAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public EvilDeadTrapdoorAddon( Serial serial ) : base( serial )
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

	public class EvilDeadTrapdoorAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new EvilDeadTrapdoorAddon();
			}
		}

		[Constructable]
		public EvilDeadTrapdoorAddonDeed()
		{
			Name = "Evil Dead Trapdoor";
		}

		public EvilDeadTrapdoorAddonDeed( Serial serial ) : base( serial )
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
}