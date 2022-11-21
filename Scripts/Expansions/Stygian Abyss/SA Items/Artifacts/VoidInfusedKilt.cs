using System;
using Server.Items;

namespace Server.Items
{
	public class VoidInfusedKilt	: GargishPlateKilt
	{
		public override int BasePhysicalResistance{ get{ return 13; } }
		public override int BaseFireResistance{ get{ return 12; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override Race RequiredRace{ get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public VoidInfusedKilt() : base( )
		{
			Name = ("Void Infused Kilt");
		
			Hue = 2124;
			
			Attributes.AttackChance = 5;			
			Attributes.BonusStr = 5;	
			Attributes.BonusDex = 5;
			Attributes.RegenMana = 1;
			Attributes.RegenStam = 1;
			AbsorptionAttributes.EaterDamage = 10;
		}

		public override void OnAdded( object parent )
		{
			if ( parent is Mobile )
			{
				if ( ((Mobile)parent).Female )
					ItemID = 0x030B;
				else
					ItemID = 0x030C;
			}
		}

		public VoidInfusedKilt( Serial serial ) : base( serial )
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