using System;
using Server;

namespace Server.Items
{
   	public class StrangeDiamond1 : Item
   	{
		[Constructable]
		public StrangeDiamond1()
		{
			Name = "diamond";
			ItemID = 3879;
			Weight = 1.0;
		}

		public StrangeDiamond1( Serial serial ) : base( serial )
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

   	public class StrangeDiamond2 : Item
   	{
		[Constructable]
		public StrangeDiamond2()
		{
			Name = "diamond";
			ItemID = 3880;
			Weight = 1.0;
		}
		
		public StrangeDiamond2( Serial serial ) : base( serial )
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
	
   	public class StrangeDiamond3 : Item 
   	{
		[Constructable]
		public StrangeDiamond3()
		{
			Name = "diamond";
			ItemID = 3881;
			Weight = 1.0;
		}

		public StrangeDiamond3( Serial serial ) : base( serial )
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

   	public class StrangeDiamond4 : Item
   	{
		[Constructable]
		public StrangeDiamond4()
		{
			Name = "diamond";
			ItemID = 3888;
			Weight = 1.0;
		}

		public StrangeDiamond4( Serial serial ) : base( serial )
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