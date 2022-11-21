using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x2B10, 0x2B11 )]
	public class CapOfTheDaemonWing : BaseArmor
	{
		public override SetItem SetID{ get{ return SetItem.DaemonWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 8; } }
		public override int BaseColdResistance{ get{ return 7; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 8; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int AosStrReq{ get{ return 25; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }
		
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public CapOfTheDaemonWing() : base( 0x2B10 )
		{
			Name = "Mephisto's Plate Helm";
			Hue = 1786;

			FireBonus = Utility.RandomMinMax(5,8);
			PoisonBonus = Utility.RandomMinMax(5,8);
			
			SetAttributes.BonusStr = 8;
		}

		public CapOfTheDaemonWing( Serial serial ) : base( serial )
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