using System;
using Server;

namespace Server.Items
{
	public class MysticsGuard : GargishWoodenShield
	{
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 1; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override Race RequiredRace { get { return Race.Gargoyle; } }

		[Constructable]
		public MysticsGuard() : base()
		{
			ArmorAttributes.SoulCharge = 30;
			Attributes.SpellChanneling = 1;
			Attributes.DefendChance = 10;
			Attributes.CastRecovery = 2;
		}

		public MysticsGuard( Serial serial ) : base(serial)
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
