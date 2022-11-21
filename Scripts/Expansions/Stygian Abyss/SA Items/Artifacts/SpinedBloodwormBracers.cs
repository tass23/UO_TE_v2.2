using System;
using Server;

namespace Server.Items
{
	public class SpinedBloodwormBracers : GargishClothArms
	{
		public override int BasePhysicalResistance{ get{ return 11; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 15; } }
		public override int BasePoisonResistance{ get{ return 15; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		[Constructable]
		public SpinedBloodwormBracers()
		{
			Name = ("Spined Bloodworm Bracers");
		
			Hue = 1642;
			
			Attributes.RegenHits = 2;
			Attributes.RegenStam = 2;
			Attributes.WeaponDamage = 10;	
			Attributes.ReflectPhysical = 30;
			//AbsorptionAttributes.EaterKinetic = 10;

		}

		public SpinedBloodwormBracers( Serial serial ) : base( serial )
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