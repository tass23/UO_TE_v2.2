using System;
using Server.Items;

namespace Server.Items
{
	public class MantleOfTheFallen : GargishClothChest
	{
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 8; } }
		public override int BaseColdResistance{ get{ return 11; } }
		public override int BasePoisonResistance{ get{ return 12; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override Race RequiredRace{ get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public MantleOfTheFallen() 
		{
		Name = ("Mantle Of The Fallen");
		
		Hue = 1512;	
		
		Attributes.LowerRegCost = 25;
		Attributes.BonusInt = 8;
		Attributes.BonusMana = 8;
		Attributes.RegenMana = 1;	
		//AbsorptionAttributes.CastingFocus = 3;
		Attributes.SpellDamage = 5;
		}

		public MantleOfTheFallen( Serial serial ) : base( serial )
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