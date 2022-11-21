using System;
using Server;

namespace Server.Items
{
	public class InquisitorsTunic : PlateChest
	{
		public override int LabelNumber{ get{ return 1060206; } } // The Inquisitor's Tunic
		public override int ArtifactRarity{ get{ return 10; } }

		public override int BaseColdResistance{ get{ return 27; } }
		public override int BaseEnergyResistance{ get{ return 23; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public InquisitorsTunic()
		{
			Name = "The Inquisitor's Tunic";
			Hue = 0x4F2;
			Attributes.CastRecovery = 3;
			Attributes.LowerManaCost = 8;
			ArmorAttributes.MageArmor = 1;
		}

		public InquisitorsTunic( Serial serial ) : base( serial )
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

			if ( version < 1 )
			{
				ColdBonus = 0;
				EnergyBonus = 0;
			}
		}
	}
}