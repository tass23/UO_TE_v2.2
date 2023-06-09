using System;
using Server;

namespace Server.Items
{
	public class HuntersTunic : LeatherChest
	{
		public override int LabelNumber{ get{ return 1061595; } } // Hunter's Tunic
		public override SetItem SetID{ get{ return SetItem.Hunters; } }
		public override int Pieces{ get{ return 5; } }

		public override int ArtifactRarity{ get{ return 11; } }

		public override int BaseColdResistance{ get{ return 25; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HuntersTunic()
		{
			Name = "Hunter's Tunic";
			Hue = 0x594;
			
			SetSkillBonuses.SetValues( 0, SkillName.Archery, 5 );
			SetAttributes.BonusDex = 4;
			SetAttributes.NightSight = 1;
			SetAttributes.AttackChance = 10;

		}

		public HuntersTunic( Serial serial ) : base( serial )
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