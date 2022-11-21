using System;
using Server.Network;
namespace Server.Items
{
	public class BarbecueSauce : Item
	{
		[Constructable]
		public BarbecueSauce() : base( 0xEFC )
		{
			Stackable = true;
			Name = "Barbecue Sauce";
			Hue = 0x278;
		}
		public BarbecueSauce( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
           	}
	}
	public class EnchiladaSauce : Item
	{
		[Constructable]
		public EnchiladaSauce() : base( 0xF04 )
		{
			Stackable = true;
			Name = "Enchilada Sauce";
			Hue = 0x1B5;
		}
		public EnchiladaSauce( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
           	}
	}
	public class HotSauce : Item
	{
		[Constructable]
		public HotSauce() : base( 0xEFD )
		{
			Stackable = true;
			Name = "Hot Sauce";
			Hue = 0x25;
		}
		public HotSauce( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
           	}
	}
	public class CookingOil : Item
	{
		[Constructable]
		public CookingOil() : base( 0xE2B )
		{
			Stackable = true;
			Name = "Cooking Oil";
			Hue = 0x2D6;
		}
		public CookingOil( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
           	}
	}
	public class SoySauce : Item
	{
		[Constructable]
		public SoySauce() : base( 0xE2C )
		{
			Stackable = true;
			Name = "Soy Sauce";
			Hue = 0x39E;
		}
		public SoySauce( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
           	}
	}
	public class Teriyaki : Item
	{
		[Constructable]
		public Teriyaki() : base( 0xE2C )
		{
			Stackable = true;
			Name = "Teriyaki";
			Hue = 0x362;
		}
		public Teriyaki( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
           	}
	}
	public class TomatoSauce : Item
	{
		[Constructable]
		public TomatoSauce() : base( 0x1006 )
		{
			Stackable = true;
			Name = "Tomato Sauce";
			Hue = 0x26;
		}
		public TomatoSauce( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
           	}
	}
	public class Vinegar : Item
	{
		[Constructable]
		public Vinegar() : base( 0x99B )
		{
			Stackable = true;
			Name = "Vinegar";
			Hue = 0x0;
		}
		public Vinegar( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
           	}
	}
	public class Cream : Item
	{
		[Constructable]
		public Cream() : base( 0x1F8C )
		{
			Stackable = true;
			Name = "Cream";
			Hue = 0x0;
		}
		public Cream( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
           	}
	}
	public class FruitJam : Item
	{
		[Constructable]
		public FruitJam() : base( 0x1006 )
		{
			Stackable = true;
			Name = "Mixed Fruit Jam";
			Hue = 0x18;
		}
		public FruitJam( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
           	}
	}
	public class PeanutButter : Item
	{
		[Constructable]
		public PeanutButter() : base( 0x1006 )
		{
			Stackable = true;
			Name = "Peanut Butter";
			Hue = 0x413;
		}
		public PeanutButter( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
           	}
	}
}