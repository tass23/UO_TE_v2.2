using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x2B0A, 0x2B0B )]
	public class ArmsOfTheDaemonWing : BaseArmor
	{
		public override SetItem SetID{ get{ return SetItem.DaemonWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 11; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 7; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int AosStrReq{ get{ return 60; } }
		
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }
		
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ArmsOfTheDaemonWing() : base( 0x2B0A )
		{
			Name = "Mephisto's Plate Arms";
			Hue = 1786;

			FireBonus = Utility.RandomMinMax(4,12);
			PoisonBonus = Utility.RandomMinMax(4,12);
			
			SetAttributes.BonusStr = 8;
		}

		public ArmsOfTheDaemonWing( Serial serial ) : base( serial )
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