using System;
using Server;

namespace Server.Items
{
	public class DivineTunic : LeatherChest
	{
		public override int LabelNumber{ get{ return 1061289; } } // Divine Tunic

		public override int ArtifactRarity{ get{ return 11; } }

		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 24; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public DivineTunic()
		{
			Name = "Divine Tunic";
			Hue = 0x482;
			Attributes.BonusInt = 10;
			Attributes.RegenMana = 3;
			Attributes.ReflectPhysical = 18;
			Attributes.LowerManaCost = 10;
		}

		public DivineTunic( Serial serial ) : base( serial )
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

			switch ( version )
			{
				case 0:
				{
					PhysicalBonus = 0;
					FireBonus = 0;
					ColdBonus = 0;
					EnergyBonus = 0;
					break;
				}
			}
		}
	}
}