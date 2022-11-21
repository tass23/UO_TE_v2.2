using System;
using Server;

namespace Server.Items
{
	public class TotemTunic : LeatherChest
	{
		public override int LabelNumber{ get{ return 1061599; } } // Totem Tunic

		public override int ArtifactRarity{ get{ return 11; } }

		public override int BasePhysicalResistance{ get{ return 20; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TotemTunic()
		{
			Name = "Totemic Tunic";
			Hue = 0x455;
			Attributes.BonusStr = 15;
			Attributes.ReflectPhysical = 10;
			Attributes.AttackChance = 10;
		}

		public TotemTunic( Serial serial ) : base( serial )
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
					break;
				}
			}
		}
	}
}