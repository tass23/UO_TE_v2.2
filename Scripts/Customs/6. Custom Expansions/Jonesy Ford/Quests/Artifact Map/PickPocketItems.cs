using System; 
using Server; 
using Server.Items;
using Server.Gumps;

namespace Server.Items
{ 
	public class AncientStatue : Item 
	{ 
		[Constructable]
		public AncientStatue()
		{
			ItemID = 3589;
			Movable = true;
			Hue = 1068;
			Name = "an ancient statue";
		}

		public AncientStatue( Serial serial ) : base( serial ) 
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
	
	public class AncientBrush : Item 
	{ 
		[Constructable]
		public AncientBrush()
		{
			ItemID = 3779;
			Movable = true;
			Hue = 1165;
			Name = "an ancient hair brush";
		}

		public AncientBrush( Serial serial ) : base( serial ) 
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
	
	public class AncientLetter : Item 
	{ 
		[Constructable]
		public AncientLetter()
		{
			ItemID = 3636;
			Movable = true;
			Hue = 1063;
			Name = "an ancient letter";
		}

		public AncientLetter( Serial serial ) : base( serial ) 
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
	
	public class AncientSalt : Item 
	{ 
		[Constructable]
		public AncientSalt()
		{
			ItemID = 4102;
			Movable = true;
			Hue = 2040;
			Name = "an ancient salt shaker";
		}

		public AncientSalt( Serial serial ) : base( serial ) 
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
	
	public class AncientPepper : Item 
	{ 
		[Constructable]
		public AncientPepper()
		{
			ItemID = 4102;
			Movable = true;
			Hue = 1876;
			Name = "an ancient pepper shaker";
		}

		public AncientPepper( Serial serial ) : base( serial ) 
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
	
	public class AncientJewels : Item 
	{ 
		[Constructable]
		public AncientJewels()
		{
			ItemID = 4235;
			Movable = true;
			Hue = 1876;
			Name = "an ancient necklace";
		}

		public AncientJewels( Serial serial ) : base( serial ) 
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