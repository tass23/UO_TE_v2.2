using System;
using Server;

namespace Server.Items
{
	public class ArmsOfTheElementalWing : PlateArms
	{
		public override SetItem SetID{ get{ return SetItem.ElementalWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ArmsOfTheElementalWing()
		{
			Name = "Mantle of Hephaestus Arms";
			Hue = 2130;

			FireBonus = Utility.RandomMinMax(4,12);
			
			SetAttributes.BonusStr = 5;
			SetSkillBonuses.SetValues( 0, SkillName.Blacksmith, 20.0 );
		}

		public ArmsOfTheElementalWing( Serial serial ) : base( serial )
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