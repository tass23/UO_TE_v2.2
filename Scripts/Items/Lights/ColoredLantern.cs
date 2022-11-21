using System;
using Server;

namespace Server.Items
{
	public class ColoredLanternRed : BaseEquipableLight
	{
		public override int LitItemID
		{
			get
			{
				return 0x40FE;
			}
		}

		public override int UnlitItemID
		{
			get
			{
				return 0xA25;
			}
		}

		[Constructable]
		public ColoredLanternRed() : base( 0xA25 )
		{
			if ( Burnout )
				Duration = TimeSpan.FromMinutes( 20 );
			else
				Duration = TimeSpan.Zero;

			Burning = false;
			Light = LightType.Circle300;
			Weight = 2.0;
			Name = "Red Lantern";
		}

		public ColoredLanternRed( Serial serial ) : base( serial )
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

	public class ColoredLanternBlue : BaseEquipableLight
	{
		public override int LitItemID
		{
			get
			{
				return 0x40FF;
			}
		}

		public override int UnlitItemID
		{
			get
			{
				return 0xA25;
			}
		}

		[Constructable]
		public ColoredLanternBlue() : base( 0xA25 )
		{
			if ( Burnout )
				Duration = TimeSpan.FromMinutes( 20 );
			else
				Duration = TimeSpan.Zero;

			Burning = false;
			Light = LightType.Circle300;
			Weight = 2.0;
			Name = "Blue Lantern";
		}

		public ColoredLanternBlue( Serial serial ) : base( serial )
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

	public class ColoredLanternGreen : BaseEquipableLight
	{
		public override int LitItemID
		{
			get
			{
				return 0x4100;
			}
		}

		public override int UnlitItemID
		{
			get
			{
				return 0xA25;
			}
		}

		[Constructable]
		public ColoredLanternGreen() : base( 0xA25 )
		{
			if ( Burnout )
				Duration = TimeSpan.FromMinutes( 20 );
			else
				Duration = TimeSpan.Zero;

			Burning = false;
			Light = LightType.Circle300;
			Weight = 2.0;
			Name = "Green Lantern";
		}

		public ColoredLanternGreen( Serial serial ) : base( serial )
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

	public class ColoredLanternPurple : BaseEquipableLight
	{
		public override int LitItemID
		{
			get
			{
				return 0x4101;
			}
		}

		public override int UnlitItemID
		{
			get
			{
				return 0xA25;
			}
		}

		[Constructable]
		public ColoredLanternPurple() : base( 0xA25 )
		{
			if ( Burnout )
				Duration = TimeSpan.FromMinutes( 20 );
			else
				Duration = TimeSpan.Zero;

			Burning = false;
			Light = LightType.Circle300;
			Weight = 2.0;
			Name = "Purple Lantern";
		}

		public ColoredLanternPurple( Serial serial ) : base( serial )
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