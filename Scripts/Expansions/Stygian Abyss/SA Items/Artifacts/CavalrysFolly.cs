using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class CavalrysFolly : BaseSpear
	{

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }


		[Constructable]
		public CavalrysFolly() : base( 0x26BD )
		{
		
			Name = ("Cavalry's Folly");
		
			Hue = 1165;
		
			Weight = 4.0;
			Attributes.BonusHits = 2;
			Attributes.AttackChance = 10;
			Attributes.WeaponDamage = 45;
			Attributes.WeaponSpeed = 35;
			WeaponAttributes.HitLowerDefend = 40;	
			WeaponAttributes.HitFireball = 40;
			
		}

		public CavalrysFolly( Serial serial ) : base( serial )
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