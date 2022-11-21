using System;

namespace Server.Items
{
	public class RingOfArcaneTactics : GoldRing
	{
		[Constructable]
        public RingOfArcaneTactics()
            : base()
		{
            Name = "Ring of Arcane Tactics";
            Attributes.CastRecovery = 3;
            Attributes.CastSpeed = 1;
		}

        public RingOfArcaneTactics(Serial serial)
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
