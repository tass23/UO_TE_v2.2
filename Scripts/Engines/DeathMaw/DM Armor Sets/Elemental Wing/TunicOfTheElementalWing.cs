using System;
using Server;

namespace Server.Items
{
	public class TunicOfTheElementalWing : PlateChest
	{
		public override SetItem SetID{ get{ return SetItem.ElementalWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TunicOfTheElementalWing()
		{
			Name = "Mantle of Hephaestus Breast Plate";
			Hue = 2130;
			
			FireBonus = Utility.RandomMinMax(10,18);
			
			SetAttributes.BonusStr = 5;
			SetSkillBonuses.SetValues( 0, SkillName.Blacksmith, 20.0 );
		}

		public TunicOfTheElementalWing( Serial serial ) : base( serial )
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