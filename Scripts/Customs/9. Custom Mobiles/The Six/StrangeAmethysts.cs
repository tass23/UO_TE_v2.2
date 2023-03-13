using System; 
using Server; 

namespace Server.Items 
{
   	public class StrangeAmethyst1 : Item 
   	{
		[Constructable] 
		public StrangeAmethyst1() 
		{
			Name = "amethyst";
			ItemID = 3863;
			Weight = 1.0;
		} 

		public StrangeAmethyst1( Serial serial ) : base( serial ) 
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

   	public class StrangeAmethyst2 : Item
   	{
		[Constructable] 
		public StrangeAmethyst2()
		{
			Name = "amethyst";
			ItemID = 3874;
			Weight = 1.0;
		}

		public StrangeAmethyst2( Serial serial ) : base( serial )
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

   	public class StrangeAmethyst3 : Item
   	{
		[Constructable] 
		public StrangeAmethyst3() 
		{
			Name = "amethyst";
			ItemID = 3886;
			Weight = 1.0;
		}

		public StrangeAmethyst3( Serial serial ) : base( serial )
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