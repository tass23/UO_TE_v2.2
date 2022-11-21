using System;
using Server;
using Server.Guilds;

namespace Server.Items
{
	public class SignOfChaos : GargishChaosShield
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 2; } }
		public override int BaseColdResistance{ get{ return 2; } }
		public override int BasePoisonResistance{ get{ return 2; } }
		public override int BaseEnergyResistance{ get{ return 2; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public SignOfChaos() : base()
		{
			ArmorAttributes.SoulCharge = 30;
			Attributes.AttackChance = 5;
			Attributes.DefendChance = 10;
			Attributes.CastSpeed = 1;
		}

		public SignOfChaos( Serial serial ) : base(serial)
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
			writer.Write( (int)0 );//version
		}
	}
}