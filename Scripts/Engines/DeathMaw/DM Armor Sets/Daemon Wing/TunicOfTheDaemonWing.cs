using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x2B08, 0x2B09 )]
	public class TunicOfTheDaemonWing : BaseArmor
	{
		public override SetItem SetID{ get{ return SetItem.DaemonWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 7; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 8; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int AosStrReq{ get{ return 65; } }
		
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }
		
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TunicOfTheDaemonWing() : base( 0x2B08 )
		{
			Name = "Mephisto's Plate Chest";
			Hue = 1786;
			
			FireBonus = Utility.RandomMinMax(10,18);
			PoisonBonus = Utility.RandomMinMax(10,18);
			
			SetAttributes.BonusStr = 8;
		}

		public TunicOfTheDaemonWing( Serial serial ) : base( serial )
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