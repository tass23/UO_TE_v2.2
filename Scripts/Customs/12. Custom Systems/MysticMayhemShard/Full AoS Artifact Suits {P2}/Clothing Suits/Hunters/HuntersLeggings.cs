using System;
using Server;

namespace Server.Items
{
	public class HuntersLeggings : LeatherLegs
	{
		public override int LabelNumber{ get{ return 1061595; } } // Hunter's Leggings

		public override int ArtifactRarity{ get{ return 11; } }

		public override int BaseColdResistance{ get{ return 25; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HuntersLeggings()
		{
			Name = "Hunter's Leggings";
			Hue = 0x594;
			SkillBonuses.SetValues( 0, SkillName.Archery, 10 );
			Attributes.BonusDex = 8;
			Attributes.NightSight = 1;
			Attributes.AttackChance = 16;

		}

		public HuntersLeggings( Serial serial ) : base( serial )
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