using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class DeedCraftingTool : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefDeedCrafting.CraftSystem; } }

		[Constructable]
		public DeedCraftingTool() : base( 0x0FBF )
		{
			Name = "a Deed Crafting Pen";
			Weight = 1.0;
			Hue = 0;
		}

		[Constructable]
		public DeedCraftingTool( int uses ) : base( uses, 0x1028 )
		{
			Weight = 1.0;
		}

		public DeedCraftingTool( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}