using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class AresGloves : DragonGloves
	{
		public override int LabelNumber{ get{ return 1061092; } } // Gauntlets of Nobility
		public override int ArtifactRarity{ get{ return 50; } }

		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 1000; } }
		public override int InitMaxHits{ get{ return 1000; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.Iron; } }

		[Constructable]
		public AresGloves()
		{
			Hue = 2949;
			Name = "Bloodstained Gauntlets";
			Attributes.BonusHits = 50;
			Attributes.WeaponDamage = 10;
		}

		public AresGloves( Serial serial ) : base( serial )
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