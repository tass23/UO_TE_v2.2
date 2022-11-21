using System;
using Server;

namespace Server.Items
{
	public class HuntersArms : LeatherArms
	{
		public override int LabelNumber{ get{ return 1061595; } } // Hunter's Arms

		public override int ArtifactRarity{ get{ return 11; } }

		public override int BaseColdResistance{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HuntersArms()
		{
			Name = "Hunter's Arms";
			Hue = 0x594;
			SkillBonuses.SetValues( 0, SkillName.Archery, 5 );
			Attributes.BonusDex = 4;
			Attributes.NightSight = 1;
			Attributes.AttackChance = 10;

		}

		public HuntersArms( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					ColdBonus = 0;
					break;
				}
			}
		}
	}
}