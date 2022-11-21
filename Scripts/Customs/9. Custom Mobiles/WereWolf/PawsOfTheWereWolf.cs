using System;
using Server;

namespace Server.Items
{
	public class PawsOfTheWereWolf : FurBoots
	{
        public override int ArtifactRarity { get { return 100; } }
		
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		
        [Constructable]
        public PawsOfTheWereWolf()
            : base(0x47E)
        {
            Name = "Paws of the WereWolf";
            Hue = 2310;

            Attributes.NightSight = 1;
                   
        }
        public PawsOfTheWereWolf(Serial serial)
            : base(serial)
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