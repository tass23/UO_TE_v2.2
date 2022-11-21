using System;
using Server;

namespace Server.Items
{
	public class CandleShortColor : BaseLight
	{
		public override int LitItemID{ get { return 0x142C; } }
		public override int UnlitItemID{ get { return 0x142F; } }

		[Constructable]
		public CandleShortColor () : base( 0x142F )
		{
			if ( Burnout )
				Duration = TimeSpan.FromMinutes( 25 );
			else
				Duration = TimeSpan.Zero;

			Burning = false;
			Light = LightType.Circle150;
			Weight = 1.0;
			Name = "short candle";
			Hue = GetRandomHue();
		}

		protected static int GetRandomHue()
		{
			switch ( Utility.Random( 5 ) )
			{
				default:
				case 0: return Utility.RandomBlueHue();
				case 1: return Utility.RandomNeutralHue();
				case 2: return Utility.RandomGreenHue();
				case 3: return Utility.RandomYellowHue();
				case 4: return Utility.RandomRedHue();
			}
		}

		public CandleShortColor( Serial serial ) : base( serial )
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