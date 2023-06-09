using System;
using Server;

namespace Server.Items
{
	public class InquisitorsArms : PlateArms
	{
		public override int LabelNumber{ get{ return 1060206; } } // The Inquisitor's Arms
		public override SetItem SetID{ get{ return SetItem.Inquisitor; } }
		public override int Pieces{ get{ return 5; } }
		public override int ArtifactRarity{ get{ return 10; } }

		public override int BaseColdResistance{ get{ return 12; } }
		public override int BaseEnergyResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public InquisitorsArms()
		{
			Name = "The Inquisitor's Arms";
			Hue = 0x4F2;
			
			SetAttributes.CastRecovery = 3;
			SetAttributes.LowerManaCost = 8;
			ArmorAttributes.MageArmor = 1;
		}

		public InquisitorsArms( Serial serial ) : base( serial )
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