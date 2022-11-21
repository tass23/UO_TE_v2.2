using System;
using Server.Network;
using Server.Targeting;
using Server.Items;

namespace Server.Items
{
	// Based off a Dagger
	[FlipableAttribute( 0x902, 0x406A )]
	public class StoneDragonsTooth : GargishDagger
	{
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override bool CanBeWornByGargoyles{ get{ return true; } }
		public override Race RequiredRace { get { return Race.Gargoyle; } }

		[Constructable]
		public StoneDragonsTooth() : base( )
		{
			Name = ("Stone Dragon's Tooth");
		
			Hue = 2407;
			
			Attributes.WeaponSpeed = 10;
			Attributes.WeaponDamage = 50;
			Attributes.RegenHits = 3;
			WeaponAttributes.HitMagicArrow = 40;
			WeaponAttributes.HitLowerDefend = 30;	
			WeaponAttributes.ResistFireBonus = 10;	
			AbsorptionAttributes.EaterPoison = 10;		
			AosElementDamages.Poison = 100;			
			
		}

		public StoneDragonsTooth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}