using System;
using Server;
using Server.Items;
using AMA = Server.Items.ArmorMeditationAllowance;

namespace Server.Items
{
	[FlipableAttribute( 0x13eb, 0x13f2 )]
	public class PhoenixGloves : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 6; } }
		public override int BaseFireResistance{ get{ return 8; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override int AosStrReq{ get{ return 40; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int OldDexBonus{ get{ return 2; } }
		public override int OldStrBonus{ get{ return 1; } }
		public override AMA OldMedAllowance{ get{ return AMA.All; } }
		public override AMA AosMedAllowance{ get{ return AMA.All; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Ringmail; } }

		[Constructable]
		public PhoenixGloves() : base( 0x13EB )
		{
			Name = "Ringmail Gloves Of The Phoenix";
			Weight = 5.0;
			Hue = 1359;
			
		}

		public PhoenixGloves( Serial serial ) : base( serial )
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
				Weight = 15.0;
		}
	}
}
