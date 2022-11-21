using System;

namespace Server.Items
{
	public class CertifiedDoctorInTheArtOfHealing : Item
	{
		[Constructable]
		public CertifiedDoctorInTheArtOfHealing() : base( 0x14F0 )
		{
			this.Weight = 0.1;
			Name = "Certified Doctor";
			Hue = 1000;
         	LootType=LootType.Blessed;
		}

		public CertifiedDoctorInTheArtOfHealing( Serial serial ) : base( serial )
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