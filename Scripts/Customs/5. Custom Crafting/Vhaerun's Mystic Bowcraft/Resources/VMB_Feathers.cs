using System;

namespace Server.Items
{
	public class FireFeather : Item
	{
		[Constructable]
		public FireFeather() : this( 1 )
		{
		}

		[Constructable]
		public FireFeather( int amount ) : base( 0x1020 )
		{
		      	Weight = 0.01;
                  	Hue = 0x48E;
			Name = "Fire Feather";
			Stackable = true;
			Amount = amount;
            	}

		public FireFeather( Serial serial ) : base( serial )
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

	public class BloodFeather : Item
	{
		[Constructable]
		public BloodFeather() : this( 1 )
		{
		}

		[Constructable]
		public BloodFeather( int amount ) : base( 0x1020 )
		{
		      	Weight = 0.01;
                  	Hue = 0x494;
			Name = "Blood Feather";
			Stackable = true;
			Amount = amount;
            	}

		public BloodFeather( Serial serial ) : base( serial )
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

	public class PoisonFeather : Item
	{
		[Constructable]
		public PoisonFeather() : this( 1 )
		{
		}

		[Constructable]
		public PoisonFeather( int amount ) : base( 0x1020 )
		{
		      	Weight = 0.01;
                  	Hue = 0x48A;
			Name = "Poison Feather";
			Stackable = true;
			Amount = amount;
            	}

		public PoisonFeather( Serial serial ) : base( serial )
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

	public class DragonFeather : Item
	{
		[Constructable]
		public DragonFeather() : this( 1 )
		{
		}

		[Constructable]
		public DragonFeather( int amount ) : base( 0x1020 )
		{
		      	Weight = 0.01;
                  	Hue = 0x487;
			Name = "Dragon Feather";
			Stackable = true;
			Amount = amount;
            	}

		public DragonFeather( Serial serial ) : base( serial )
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

	public class AirFeather : Item
	{
		[Constructable]
		public AirFeather() : this( 1 )
		{
		}

		[Constructable]
		public AirFeather( int amount ) : base( 0x1020 )
		{
		      	Weight = 0.01;
                  	Hue = 0x47F;
			Name = "Air Feather";
			Stackable = true;
			Amount = amount;
            	}

		public AirFeather( Serial serial ) : base( serial )
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

	public class WaterFeather : Item
	{
		[Constructable]
		public WaterFeather() : this( 1 )
		{
		}

		[Constructable]
		public WaterFeather( int amount ) : base( 0x1020 )
		{
		      	Weight = 0.01;
                  	Hue = 0x18E;
			Name = "Water Feather";
			Stackable = true;
			Amount = amount;
            	}

		public WaterFeather( Serial serial ) : base( serial )
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