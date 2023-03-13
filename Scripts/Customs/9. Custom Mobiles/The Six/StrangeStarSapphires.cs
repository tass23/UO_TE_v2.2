using System;
using Server;

namespace Server.Items
{
   	public class StrangeStarSapphire1 : Item
   	{
		[Constructable]
		public StrangeStarSapphire1()
		{
			Name = "star sapphire";
			ItemID = 3855;
			Weight = 1.0;
		}

		public StrangeStarSapphire1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
   	}

   	public class StrangeStarSapphire2 : Item
   	{
		[Constructable]
		public StrangeStarSapphire2()
		{
			Name = "star sapphire";
			ItemID = 3867;
			Weight = 1.0;
		}

		public StrangeStarSapphire2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
   	}
}