using System;

namespace Server.Items
{
    public class FarmersBandOfMastery : GoldBracelet
	{
		[Constructable]
        public FarmersBandOfMastery()
            : base()
		{
            Name = "Farmer's Band of Mastery";
            Attributes.CastRecovery = 3;
            Attributes.CastSpeed = 1;
		}

        public FarmersBandOfMastery(Serial serial)
            : base(serial)
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
