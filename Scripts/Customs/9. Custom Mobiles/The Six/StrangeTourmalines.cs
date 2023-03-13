using System;
using Server;

namespace Server.Items
{
   	public class StrangeTourmaline1 : Item
   	{
		[Constructable]
		public StrangeTourmaline1()
		{
			Name = "tourmaline";
			ItemID = 3864;
			Weight = 1.0;
		}

		public StrangeTourmaline1( Serial serial ) : base( serial )
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

   	public class StrangeTourmaline2 : Item
   	{
		[Constructable]
		public StrangeTourmaline2()
		{
			Name = "tourmaline";
			ItemID = 3870;
			Weight = 1.0;
		}

		public StrangeTourmaline2( Serial serial ) : base( serial )
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

   	public class StrangeTourmaline3 : Item
   	{
		[Constructable]
		public StrangeTourmaline3()
		{
			Name = "tourmaline";
			ItemID = 3872;
			Weight = 1.0;
		}

		public StrangeTourmaline3( Serial serial ) : base( serial )
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