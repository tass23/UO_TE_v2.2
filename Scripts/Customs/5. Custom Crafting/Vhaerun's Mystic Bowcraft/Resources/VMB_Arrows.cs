using System;

namespace Server.Items
{
	public class FireArrow : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Fire arrow" : "{0} Fire arrows", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}
		
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public FireArrow() : this( 1 )
		{
		}

		[Constructable]
		public FireArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x48E;
			Name = "Fire Arrow";
		}

		public FireArrow( Serial serial ) : base( serial )
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

	public class BloodArrow : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Blood arrow" : "{0} Blood arrows", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public BloodArrow() : this( 1 )
		{
		}

		[Constructable]
		public BloodArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x494;
			Name = "Blood Arrow";
		}

		public BloodArrow( Serial serial ) : base( serial )
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

	public class PoisonArrow : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Poison arrow" : "{0} Poison arrows", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public PoisonArrow() : this( 1 )
		{
		}

		[Constructable]
		public PoisonArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x48A;
			Name = "Poison Arrow";
		}

		public PoisonArrow( Serial serial ) : base( serial )
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

	public class DragonArrow : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Dragon arrow" : "{0} Dragon arrows", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public DragonArrow() : this( 1 )
		{
		}

		[Constructable]
		public DragonArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x487;
			Name = "Dragon Arrow";
		}

		public DragonArrow( Serial serial ) : base( serial )
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

	public class MoonArrow : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Moon arrow" : "{0} Moon arrows", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public MoonArrow() : this( 1 )
		{
		}

		[Constructable]
		public MoonArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x48E;
			Name = "Moon Arrow";
		}

		public MoonArrow( Serial serial ) : base( serial )
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

	public class FalconArrow : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Falcon arrow" : "{0} Falcon arrows", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public FalconArrow() : this( 1 )
		{
		}

		[Constructable]
		public FalconArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x456;
			Name = "Falcon Arrow";
		}

		public FalconArrow( Serial serial ) : base( serial )
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

	public class SunArrow : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Sun arrow" : "{0} Sun arrows", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public SunArrow() : this( 1 )
		{
		}

		[Constructable]
		public SunArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x465;
			Name = "Sun Arrow";
		}

		public SunArrow( Serial serial ) : base( serial )
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

	public class StarArrow : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Star arrow" : "{0} Star arrows", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public StarArrow() : this( 1 )
		{
		}

		[Constructable]
		public StarArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x498;
			Name = "Star Arrow";
		}

		public StarArrow( Serial serial ) : base( serial )
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

	public class DarkArrow : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Dark arrow" : "{0} Dark arrows", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public DarkArrow() : this( 1 )
		{
		}

		[Constructable]
		public DarkArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x3D6;
			Name = "Dark Arrow";
		}

		public DarkArrow( Serial serial ) : base( serial )
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

	public class ZodiacArrow : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Zodiac arrow" : "{0} Zodiac arrows", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public ZodiacArrow() : this( 1 )
		{
		}

		[Constructable]
		public ZodiacArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x492;
			Name = "Zodiac Arrow";
		}

		public ZodiacArrow( Serial serial ) : base( serial )
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

	public class AirBolt : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Air bolt" : "{0} Air bolts", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public AirBolt() : this( 1 )
		{
		}

		[Constructable]
		public AirBolt( int amount ) : base( 0x1BFB )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x259;
			Name = "Air Bolt";
		}

		public AirBolt( Serial serial ) : base( serial )
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

	public class WaterBolt : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Water bolt" : "{0} Water bolts", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public WaterBolt() : this( 1 )
		{
		}

		[Constructable]
		public WaterBolt( int amount ) : base( 0x1BFB )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x18E;
			Name = "Water Bolt";
		}

		public WaterBolt( Serial serial ) : base( serial )
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

	public class DeathBolt : Item, ICommodity
	{
		/*string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Death bolt" : "{0} Death bolts", Amount );
			}
		}
		*/
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public DeathBolt() : this( 1 )
		{
		}

		[Constructable]
		public DeathBolt( int amount ) : base( 0x1BFB )
		{
			Stackable = true;
			Amount = amount;
			Hue = 0x497;
			Name = "Death Bolt";
		}

		public DeathBolt( Serial serial ) : base( serial )
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