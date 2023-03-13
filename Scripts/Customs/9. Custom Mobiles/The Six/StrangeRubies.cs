using System;
using Server;

namespace Server.Items
{
   	public class StrangeRuby1 : Item
   	{
		[Constructable]
		public StrangeRuby1()
		{
			Name = "ruby";
			ItemID = 3860;
			Weight = 1.0;
		}

		public StrangeRuby1( Serial serial ) : base( serial )
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

   	public class StrangeRuby2 : Item 
   	{
		[Constructable]
		public StrangeRuby2()
		{
			Name = "ruby";
			ItemID = 3866;
			Weight = 1.0;
		}

		public StrangeRuby2( Serial serial ) : base( serial )
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
	
   	public class StrangeRuby3 : Item
   	{
		[Constructable]
		public StrangeRuby3()
		{
			Name = "ruby";
			ItemID = 3868;
			Weight = 1.0;
		}

		public StrangeRuby3( Serial serial ) : base( serial )
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

   	public class StrangeRuby4 : Item
   	{
		[Constructable]
		public StrangeRuby4()
		{
			Name = "ruby";
			ItemID = 3869;
			Weight = 1.0;
		}

		public StrangeRuby4( Serial serial ) : base( serial )
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

   	public class StrangeRuby5 : Item
   	{
		[Constructable]
		public StrangeRuby5()
		{
			Name = "ruby";
			ItemID = 3882;
			Weight = 1.0;
		}

		public StrangeRuby5( Serial serial ) : base( serial )
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

   	public class StrangeRuby6 : Item
   	{
		[Constructable]
		public StrangeRuby6()
		{
			Name = "ruby";
			ItemID = 3883;
			Weight = 1.0;
		}

		public StrangeRuby6( Serial serial ) : base( serial )
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