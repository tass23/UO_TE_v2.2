
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
	public class DolphinFloorMosaicSouthAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {18280, 1, -1, 0}, {18275, 0, 0, 0}, {18281, -1, 0, 0}// 1	2	3	
			, {18285, 0, -2, 0}, {18283, 1, 0, 0}, {18277, 0, 1, 0}// 4	5	6	
			, {18276, 1, 1, 0}, {18289, 1, 2, 0}, {18278, -1, -1, 0}// 7	8	9	
			, {18288, 0, 2, 0}, {18287, -1, -2, 0}, {18282, -1, 1, 0}// 10	11	12	
			, {18279, 0, -1, 0}, {18286, 1, -2, 0}, {18284, -1, 2, 0}// 13	14	15	
					};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new DolphinFloorMosaicSouthAddonDeed();
			}
		}

		[ Constructable ]
		public DolphinFloorMosaicSouthAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public DolphinFloorMosaicSouthAddon( Serial serial ) : base( serial )
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

	public class DolphinFloorMosaicSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DolphinFloorMosaicSouthAddon();
			}
		}

		[Constructable]
		public DolphinFloorMosaicSouthAddonDeed()
		{
			Name = "DolphinFloorMosaicSouth";
		}

		public DolphinFloorMosaicSouthAddonDeed( Serial serial ) : base( serial )
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