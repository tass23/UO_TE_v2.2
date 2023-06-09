using System;
using Server;

namespace Server.Items
{
	public class GlovesOfFortune : StuddedGloves
	{
		public override int LabelNumber{ get{ return 1061098; } } // Gloves of Fortune
		public override SetItem SetID{ get{ return SetItem.Fortune; } }
		public override int Pieces{ get{ return 5; } }
		public override int ArtifactRarity{ get{ return 11; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public GlovesOfFortune()
		{
			Name = "Gloves of Fortune";
			Hue = 0x501;
			ArmorAttributes.MageArmor = 1;
			
			SetAttributes.Luck = 200;
			SetAttributes.DefendChance = 15;
			SetAttributes.LowerRegCost = 40;
		}

		public GlovesOfFortune( Serial serial ) : base( serial )
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