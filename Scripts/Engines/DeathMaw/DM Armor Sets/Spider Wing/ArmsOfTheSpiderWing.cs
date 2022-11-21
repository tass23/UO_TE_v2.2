using System;
using Server;

namespace Server.Items
{
	public class ArmsOfTheSpiderWing : LeatherArms
	{
		public override SetItem SetID{ get{ return SetItem.SpiderWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ArmsOfTheSpiderWing()
		{
			Name = "Ariadne's Bane Arms";
			Hue = 1156;

			Attributes.BonusDex = Utility.RandomMinMax(1,5);
			PoisonBonus = Utility.RandomMinMax(4,12);
			
			SetAttributes.RegenHits = 5;
			SetPoisonBonus = 10;
		}

		public ArmsOfTheSpiderWing( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}