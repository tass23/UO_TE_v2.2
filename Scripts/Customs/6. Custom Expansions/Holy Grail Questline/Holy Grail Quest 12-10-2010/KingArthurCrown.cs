using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x2B6F, 0x3166 )]
	public class KingArthurCrown : BaseArmor
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int BasePhysicalResistance{ get{ return 20; } }
		public override int BaseFireResistance{ get{ return 20; } }
		public override int BaseColdResistance{ get{ return 20; } }
		public override int BasePoisonResistance{ get{ return 20; } }
		public override int BaseEnergyResistance{ get{ return 20; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }
		public override int ArmorBase{ get{ return 30; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public KingArthurCrown() : base( 0x2B6F )
		{
			Name = "King Arthur's Crown";
			Weight = 2.0;
			Hue = 1154;
			Attributes.Luck = 200;
		}

		public KingArthurCrown( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();

			if ( Hue == 2959 )
				Hue = 1154;
		}
	}
}