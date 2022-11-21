using System;
using Server.Items;
using Server.Network;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class JadeWarAxe : WarAxe
	{

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 80; } }

		public override HarvestSystem HarvestSystem{ get{ return null; } }

		[Constructable]
		public JadeWarAxe()
		{
		
			Name = ("Jade War Axe");
		
			Hue = 1162;
			
			AbsorptionAttributes.EaterFire = 10;
			Slayer = SlayerName.ReptilianDeath;
			WeaponAttributes.HitFireball = 30;	
			WeaponAttributes.HitLowerDefend = 60;		
			Attributes.WeaponSpeed = 20;
			Attributes.WeaponDamage = 50;
		}

		public JadeWarAxe( Serial serial ) : base( serial )
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