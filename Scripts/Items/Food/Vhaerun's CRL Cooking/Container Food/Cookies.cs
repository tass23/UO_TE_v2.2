using System;
using System.Collections;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class AlmondCookies : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return true; } }
		public override string CookedMessage	{get { return "You make some almond cookies."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public AlmondCookies() : base( 0x160C )
		{
			Name = "Almond Cookies";
			Uses = 3;
			FillFactor = 2;
		}

		public AlmondCookies( Serial serial ) : base( serial )
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

	public class ChocChipCookies : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return true; } }
		public override string CookedMessage	{get { return "You make some chocolate chip cookies."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public ChocChipCookies() : base( 0x160C )
		{
			Name = "Chocolate Chip Cookies";
			Uses = 3;
			FillFactor = 2;
		}

		public ChocChipCookies( Serial serial ) : base( serial )
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

	public class GingerSnaps : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return true; } }
		public override string CookedMessage	{get { return "You make some ginger snaps."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public GingerSnaps() : base( 0x160C )
		{
			Name = "Ginger Snaps";
			Uses = 3;
			FillFactor = 2;
		}

		public GingerSnaps( Serial serial ) : base( serial )
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

	public class OatmealCookies : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return true; } }
		public override string CookedMessage	{get { return "You make some oatmeal cookies."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public OatmealCookies() : base( 0x160C )
		{
			Name = "Oatmeal Cookies";
			Uses = 3;
			FillFactor = 2;
		}

		public OatmealCookies( Serial serial ) : base( serial )
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

	public class PeanutButterCookies : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return true; } }
		public override string CookedMessage	{get { return "You make some peanut butter cookies."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public PeanutButterCookies() : base( 0x160C )
		{
			Name = "Peanut Butter Cookies";
			Uses = 3;
			FillFactor = 2;
		}

		public PeanutButterCookies( Serial serial ) : base( serial )
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

	public class PumpkinCookies : ContainerFood
	{
		public override int MinSkill		{get { return 0; } }
		public override int MaxSkill		{get { return 100; } }
		public override bool NeedSilverware	{get { return true; } }
		public override string CookedMessage	{get { return "You make some pumpkin cookies."; } }
		public override Item FoodContainer	{get { return new DirtyPlate(); } }

		[Constructable]
		public PumpkinCookies() : base( 0x160C )
		{
			Name = "Pumpkin Cookies";
			Uses = 3;
			FillFactor = 2;
		}

		public PumpkinCookies( Serial serial ) : base( serial )
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