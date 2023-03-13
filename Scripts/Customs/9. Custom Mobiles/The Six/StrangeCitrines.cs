using System; 
using Server; 

namespace Server.Items
{
   	public class StrangeCitrine1 : Item
   	{
		[Constructable]
		public StrangeCitrine1()
		{
			Name = "citrine";
			ItemID = 3875;
			Weight = 1.0;
		}

		public StrangeCitrine1( Serial serial ) : base( serial )
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

   	public class StrangeCitrine2 : Item 
   	{
		[Constructable] 
		public StrangeCitrine2() 
		{
			Name = "citrine";
			ItemID = 3876;
			Weight = 1.0;
		}

		public StrangeCitrine2( Serial serial ) : base( serial )
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

   	public class StrangeCitrine3 : Item
   	{
		[Constructable]
		public StrangeCitrine3()
		{
			Name = "citrine";
			ItemID = 3884;
			Weight = 1.0;
		}

		public StrangeCitrine3( Serial serial ) : base( serial )
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