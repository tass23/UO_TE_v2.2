using System;
using Server;

namespace Server.Items
{
	public class ArmsOfTheGargDMSet : GargishPlateArms
	{
		public override SetItem SetID{ get{ return SetItem.GargDMSet; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int ArtifactRarity{ get{ return 15; } }
		
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ArmsOfTheGargDMSet()
		{
			Name = "Lineage of Kallibrus Arms";
			Hue = 1198;

			EnergyBonus = Utility.RandomMinMax(4,12);
			
			SetAttributes.BonusDex = 5;
			SetAttributes.BonusStr = 5;
			SetSkillBonuses.SetValues( 0, SkillName.Imbuing, 15.0 );
		}
		
		public ArmsOfTheGargDMSet( Serial serial ) : base( serial )
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