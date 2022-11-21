using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{	
	public class RideablePolarBear : EtherealMount
	{
		public override int LabelNumber { get { return 1076159; } } // Rideable Polar Bear 
        public override int EtherealHue { get { return 0; } }

		[Constructable]
		public RideablePolarBear() : base( 0x20E1, 0x3EC5 )
		{
		}

		public RideablePolarBear( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}
            
        public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}	
}