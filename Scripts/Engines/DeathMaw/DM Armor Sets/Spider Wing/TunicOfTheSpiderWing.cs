using System;
using Server;

namespace Server.Items
{
	public class TunicOfTheSpiderWing : LeatherChest
	{
		public override SetItem SetID{ get{ return SetItem.SpiderWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TunicOfTheSpiderWing()
		{
			Name = "Ariadne's Bane Tunic";
			Hue = 1156;
			
			PoisonBonus = Utility.RandomMinMax(10,18);
			
			SetAttributes.RegenHits = 5;
			SetPoisonBonus = 10;
		}

		public TunicOfTheSpiderWing( Serial serial ) : base( serial )
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
		}
	}
}