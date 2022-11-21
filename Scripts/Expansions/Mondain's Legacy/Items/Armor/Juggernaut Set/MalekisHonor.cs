using System;
using Server;

namespace Server.Items
{
	public class MalekisHonor : MetalKiteShield
	{
		public override int LabelNumber{ get{ return 1074312; } } // Maleki's Honor (Juggernaut Set)
	
		public override SetItem SetID{ get{ return SetItem.Juggernaut; } }
		public override int Pieces{ get{ return 2; } }
	
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		[Constructable]
		public MalekisHonor() : base()
		{
			SetHue = 0x76D;
			
			SetSelfRepair = 3;			
			SetAttributes.DefendChance = 10;
			SetAttributes.BonusStr = 10;
			SetAttributes.WeaponSpeed = 35;
		}

		public MalekisHonor( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );//version
		}
	}
}
