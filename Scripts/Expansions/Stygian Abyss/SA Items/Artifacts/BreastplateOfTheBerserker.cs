using System;
using Server.Items;

namespace Server.Items
{
	public class BreastplateOfTheBerserker : GargishPlateChest
	{
		public override int BasePhysicalResistance{ get{ return 18; } }
		public override int BaseFireResistance{ get{ return 16; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 11; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		
		public override Race RequiredRace{ get { return Race.Gargoyle; } }
		public override bool CanBeWornByGargoyles{ get{ return true; } }

		[Constructable]
		public BreastplateOfTheBerserker() 
		{
			Name = ("Breastplate Of The Berserker");
		
			Hue = 1172;	
		
			Attributes.WeaponSpeed = 10;
			Attributes.WeaponDamage = 15;		
			Attributes.LowerManaCost = 4;
			Attributes.BonusHits = 5;			
		}

		public override void OnAdded( object parent )
		{
			if ( parent is Mobile )
			{
				if ( ((Mobile)parent).Female )
					ItemID = 0x0309;
				else
					ItemID = 0x030A;
			}
		}

		public BreastplateOfTheBerserker( Serial serial ) : base( serial )
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