using System;
using Server.Items;

namespace Server.Items
{
	public class MarkaGauntlets : PlateGloves
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public MarkaGauntlets()
		{
			Name = "Gauntlets of Marka Ragnos";
			ItemID = 11020;
			Hue = 1786;
			Attributes.RegenMana = Utility.RandomMinMax(2, 5);
			Attributes.BonusInt = Utility.RandomMinMax(5, 15);
			SkillBonuses.SetValues( 0, SkillName.Focus, 10.0);
		}

		public MarkaGauntlets( Serial serial ) : base( serial )
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