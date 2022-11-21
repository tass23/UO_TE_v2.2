using System;
using Server;

namespace Server.Items
{
	public class AresCloak : Cloak
	{
		public override int LabelNumber{ get{ return 1061092; } } // Gauntlets of Nobility
		public override int ArtifactRarity{ get{ return 50; } }

		[Constructable]
		public AresCloak()
		{
			Hue = 1157;
			Name = "Bloodstained Cloak";
			Attributes.BonusHits = 50;
			Attributes.WeaponDamage = 10;
		}

		public AresCloak( Serial serial ) : base( serial )
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