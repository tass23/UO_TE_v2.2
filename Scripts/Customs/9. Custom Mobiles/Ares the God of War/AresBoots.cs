using System;
using Server;

namespace Server.Items
{
	public class AresBoots : Boots
	{
		public override int LabelNumber{ get{ return 1061092; } } // Gauntlets of Nobility
		public override int ArtifactRarity{ get{ return 50; } }

		[Constructable]
		public AresBoots()
		{
			Hue = 2949;
			Name = "Bloodstained Boots";
			ItemID = 12228;
			Attributes.BonusHits = 50;
			Attributes.WeaponDamage = 10;
		}

		public AresBoots( Serial serial ) : base( serial )
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
		}
	}
}