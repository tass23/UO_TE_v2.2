using System;
using Server;

namespace Server.Items
{
	public class BouraTailShield : WoodenKiteShield
	{
		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 1; } }
		
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public BouraTailShield()
		{
			Name = ("Boura Tail Shield");
			ItemID = 0x1B78;
			Hue = 554;
			Attributes.ReflectPhysical = 10;
			ArmorAttributes.ReactiveParalyze = 1;
		}

		public BouraTailShield( Serial serial ) : base( serial )
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