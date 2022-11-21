using System;
using System.Collections;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class ChickenParmesian : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a chicken parmesian dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public ChickenParmesian() : base( 0x9DB )
		{
			Name = "Chicken Parmesian Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public ChickenParmesian( Serial serial ) : base( serial )
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

	public class CheeseEnchilada : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a cheese enchilada dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public CheeseEnchilada() : base( 0x9DB )
		{
			Name = "Cheese Enchilada Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public CheeseEnchilada( Serial serial ) : base( serial )
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

	public class ChickenEnchilada : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a chicken enchilada dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public ChickenEnchilada() : base( 0x9DB )
		{
			Name = "Chicken Enchilada Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public ChickenEnchilada( Serial serial ) : base( serial )
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

	public class Lasagna : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a lasagna dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public Lasagna() : base( 0x9DB )
		{
			Name = "Lasagna Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public Lasagna( Serial serial ) : base( serial )
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

	public class LemonChicken : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a lemon chicken dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public LemonChicken() : base( 0x9DB )
		{
			Name = "Lemon Chicken Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public LemonChicken( Serial serial ) : base( serial )
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

	public class OrangeChicken : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make an orange chicken dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public OrangeChicken() : base( 0x9DB )
		{
			Name = "Orange Chicken Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public OrangeChicken( Serial serial ) : base( serial )
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

	public class VealParmesian : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a veal parmesian dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public VealParmesian() : base( 0x9DB )
		{
			Name = "Veal Parmesian Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public VealParmesian( Serial serial ) : base( serial )
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

	public class BeefBBQRibs : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a beef barbecue rib dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public BeefBBQRibs() : base( 0x9DB )
		{
			Name = "Beef Barbecue Rib Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public BeefBBQRibs( Serial serial ) : base( serial )
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

	public class BeefBroccoli : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a beef and broccoli dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public BeefBroccoli() : base( 0x9DB )
		{
			Name = "Beef and Broccoli Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public BeefBroccoli( Serial serial ) : base( serial )
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

	public class ChoChoBeef : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a cho cho beef dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public ChoChoBeef() : base( 0x9DB )
		{
			Name = "Cho Cho Beef Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public ChoChoBeef( Serial serial ) : base( serial )
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

	public class BeefSnowpeas : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a beef and snowpeas dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public BeefSnowpeas() : base( 0x9DB )
		{
			Name = "Beef and Snowpeas Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public BeefSnowpeas( Serial serial ) : base( serial )
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

	public class Hamburger : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a hamburger dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public Hamburger() : base( 0x9DB )
		{
			Name = "Hamburger Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public Hamburger( Serial serial ) : base( serial )
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

	public class PorkBBQRibs : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a pork barbecue rib dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public PorkBBQRibs() : base( 0x9DB )
		{
			Name = "Pork Barbecue Rib Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public PorkBBQRibs( Serial serial ) : base( serial )
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

	public class BeefLoMein : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a beef lo mein dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public BeefLoMein() : base( 0x9DB )
		{
			Name = "Beef Lo Mein Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public BeefLoMein( Serial serial ) : base( serial )
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

	public class BeefStirfry : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a beef stirfry dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public BeefStirfry() : base( 0x9DB )
		{
			Name = "Beef Stirfry Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public BeefStirfry( Serial serial ) : base( serial )
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

	public class ChickenStirfry : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a chicken stirfry dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public ChickenStirfry() : base( 0x9DB )
		{
			Name = "Chicken Stirfry Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public ChickenStirfry( Serial serial ) : base( serial )
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

	public class MooShuPork : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a moo shu pork dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public MooShuPork() : base( 0x9DB )
		{
			Name = "Moo Shu Pork Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public MooShuPork( Serial serial ) : base( serial )
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

	public class MoPoTofu : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a mo po tofu dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public MoPoTofu() : base( 0x9DB )
		{
			Name = "Mo Po Tofu Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public MoPoTofu( Serial serial ) : base( serial )
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

	public class PorkStirfry : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a pork stirfry dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public PorkStirfry() : base( 0x9DB )
		{
			Name = "Pork Stirfry Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public PorkStirfry( Serial serial ) : base( serial )
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

	public class SweetSourChicken : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a sweet and sour chicken dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public SweetSourChicken() : base( 0x9DB )
		{
			Name = "Sweet and Sour Chicken Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public SweetSourChicken( Serial serial ) : base( serial )
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

	public class SweetSourPork : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a sweet and sour pork dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public SweetSourPork() : base( 0x9DB )
		{
			Name = "Sweet and Sour Pork Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public SweetSourPork( Serial serial ) : base( serial )
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

	public class Spaghetti : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return false; } }
		public override string CookedMessage	{get { return "You make a spaghetti dinner."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public Spaghetti() : base( 0x9DB )
		{
			Name = "Spaghetti Dinner";
			Uses = 3;
			FillFactor = 5;
		}

		public Spaghetti( Serial serial ) : base( serial )
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