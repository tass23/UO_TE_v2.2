using System;
using Server;

namespace Server.Items
{
	public class TotemGorget : LeatherGorget
	{
		public override int LabelNumber{ get{ return 1061599; } } // Totem Gorget
		public override SetItem SetID{ get{ return SetItem.Totemic; } }
		public override int Pieces{ get{ return 5; } }

		public override int ArtifactRarity{ get{ return 11; } }

		public override int BasePhysicalResistance{ get{ return 12; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TotemGorget()
		{
			Name = "Totemic Gorget";
			Hue = 0x455;
			
			SetAttributes.BonusStr = 8;
			SetAttributes.ReflectPhysical = 8;
			SetAttributes.AttackChance = 8;
		}

		public TotemGorget( Serial serial ) : base( serial )
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