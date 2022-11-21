using System;
using Server;

namespace Server.Items
{
	public class HuntersGorget : LeatherGorget
	{
		public override int LabelNumber{ get{ return 1061595; } } // Hunter's Gorget

		public override int ArtifactRarity{ get{ return 11; } }

		public override int BaseColdResistance{ get{ return 20; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HuntersGorget()
		{
			Name = "Hunter's Gorget";
			Hue = 0x594;
			SkillBonuses.SetValues( 0, SkillName.Archery, 5 );
			Attributes.BonusDex = 6;
			Attributes.NightSight = 1;
			Attributes.AttackChance = 10;

		}

		public HuntersGorget( Serial serial ) : base( serial )
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