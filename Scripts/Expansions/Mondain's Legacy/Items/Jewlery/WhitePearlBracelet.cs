using System;

namespace Server.Items
{
	public class WhitePearlBracelet : GoldBracelet
	{
		public override int LabelNumber{ get{ return 1073456; } } // white pearl bracelet

		[Constructable]
		public WhitePearlBracelet() : base()
		{
			Weight = 1.0;
			
			Attributes.NightSight = 1;
			
			BaseRunicTool.ApplyAttributesTo( this, Utility.RandomMinMax( 3, 5 ), 0, 100 );
			
			if ( Utility.Random( 100 ) < 50 )
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0: Attributes.CastSpeed += 1; break;
					case 1: Attributes.CastRecovery += 2; break;
					case 2: Attributes.LowerRegCost += 10; break;
				}			
			}
		}

		public WhitePearlBracelet( Serial serial ) : base( serial )
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
