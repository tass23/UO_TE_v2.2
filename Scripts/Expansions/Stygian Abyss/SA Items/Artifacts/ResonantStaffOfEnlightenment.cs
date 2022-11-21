using System; 
using Server; 
using Server.Mobiles;

namespace Server.Items
{
	public class ResonantStaffofEnlightenment : QuarterStaff
	{
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ResonantStaffofEnlightenment()
		{
			Name = ("Resonant Staff of Enlightenment");
		
			Hue = 2401;

			WeaponAttributes.HitMagicArrow = 40;
			WeaponAttributes.MageWeapon = 20;
			Attributes.SpellChanneling = 1;
			Attributes.DefendChance = 10;
			Attributes.WeaponSpeed = 20;
			Attributes.WeaponDamage = -40;
			Attributes.LowerManaCost = 5;			
			AbsorptionAttributes.ResonanceCold = 20;	
			AosElementDamages.Cold = 100;					
		}

		public ResonantStaffofEnlightenment( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}