using System;
using Server;

namespace Server.Items
{
	public class TunicOfTheFeyWing : HideChest
	{
		public override SetItem SetID{ get{ return SetItem.FeyWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TunicOfTheFeyWing()
		{
			Name = "Galadriel's Boon Tunic";
			Hue = 2224;
			
			ArmorAttributes.MageArmor = 1;
			
			SetSkillBonuses.SetValues( 0, SkillName.Magery, 10.0 );
			SetSkillBonuses.SetValues( 0, SkillName.Inscribe, 10.0 );
			SetAttributes.BonusInt = 5;
		}

		public TunicOfTheFeyWing( Serial serial ) : base( serial )
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