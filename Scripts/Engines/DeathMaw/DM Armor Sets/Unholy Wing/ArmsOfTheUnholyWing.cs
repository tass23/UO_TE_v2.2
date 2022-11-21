using System;
using Server;

namespace Server.Items
{
	public class ArmsOfTheUnholyWing : BoneArms
	{
		public override SetItem SetID{ get{ return SetItem.UnholyWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ArmsOfTheUnholyWing()
		{
			Name = "Unholy Vestment Arms";
			Hue = 1108;

			ColdBonus = Utility.RandomMinMax(4,12);
			
			SetAttributes.RegenHits = 5;
			SetColdBonus = 10;
			SetSkillBonuses.SetValues( 0, SkillName.Necromancy, 20.0 );
		}

		public ArmsOfTheUnholyWing( Serial serial ) : base( serial )
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