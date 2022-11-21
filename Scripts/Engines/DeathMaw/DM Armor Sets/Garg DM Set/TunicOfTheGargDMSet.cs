using System;
using Server;

namespace Server.Items
{
	public class TunicOfTheGargDMSet : GargishPlateChest
	{
		public override SetItem SetID{ get{ return SetItem.GargDMSet; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TunicOfTheGargDMSet()
		{
			Name = "Lineage of Kallibrus Breast Plate";
			Hue = 1198;
			
			EnergyBonus = Utility.RandomMinMax(10,18);
			
			SetAttributes.BonusDex = 5;
			SetAttributes.BonusStr = 5;
			SetSkillBonuses.SetValues( 0, SkillName.Imbuing, 15.0 );
		}

		public TunicOfTheGargDMSet( Serial serial ) : base( serial )
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