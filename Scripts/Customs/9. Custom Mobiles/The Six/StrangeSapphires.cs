using System;
using Server;

namespace Server.Items
{
   	public class StrangeSapphire1 : Item
   	{
		[Constructable]
		public StrangeSapphire1()
		{
			Name = "sapphire";
			ItemID = 3857;
			Weight = 1.0;
		}

		public StrangeSapphire1( Serial serial ) : base( serial )
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

   	public class StrangeSapphire2 : Item
   	{
		[Constructable]
		public StrangeSapphire2()
		{
			Name = "sapphire";
			ItemID = 3858;
			Weight = 1.0;
		}

		public StrangeSapphire2( Serial serial ) : base( serial )
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

   	public class StrangeSapphire3 : Item
   	{
		[Constructable]
		public StrangeSapphire3()
		{
			Name = "sapphire";
			ItemID = 3867;
			Weight = 1.0;
		}

		public StrangeSapphire3( Serial serial ) : base( serial )
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

   	public class StrangeSapphire4 : Item
   	{
		[Constructable]
		public StrangeSapphire4()
		{
			Name = "sapphire";
			ItemID = 3871;
			Weight = 1.0;
		}

		public StrangeSapphire4( Serial serial ) : base( serial )
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