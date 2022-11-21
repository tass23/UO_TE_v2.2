using System;
using Server;

namespace Server.Items
{
	public class LegsOfTheElementalWing : PlateLegs
	{
		public override SetItem SetID{ get{ return SetItem.ElementalWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public LegsOfTheElementalWing()
		{
			Name = "Mantle of Hephaestus Legs";
			Hue = 2130;

			FireBonus = Utility.RandomMinMax(8,16);
			
			SetAttributes.BonusStr = 5;
			SetSkillBonuses.SetValues( 0, SkillName.Blacksmith, 20.0 );
		}

		public LegsOfTheElementalWing( Serial serial ) : base( serial )
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