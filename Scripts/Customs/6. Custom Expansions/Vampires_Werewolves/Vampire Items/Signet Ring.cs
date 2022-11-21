using System;

namespace Server.Items
{
	public class VampireSignetRing : GoldRing
	{
		[Constructable]
        public VampireSignetRing(): base()
		{
			Hue = 37;
            Name = "a Vampire's Signet Ring";
            Attributes.CastRecovery = 3;
            Attributes.CastSpeed = 1;
		}

        public VampireSignetRing(Serial serial) : base(serial)
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