using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class DemonHuntersStandard : Spear
	{


		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public DemonHuntersStandard() : base( 0xF62 )
		{
			Name = ("Demon Hunter's Standard");
		
			Hue = 1377;	
			
			Attributes.CastSpeed = 1;			
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 50;
			WeaponAttributes.HitLeechStam = 50;
			WeaponAttributes.HitLightning = 40;	
			WeaponAttributes.HitLowerDefend = 30;
			Slayer = SlayerName.DaemonDismissal;
			
			
		}

		public DemonHuntersStandard( Serial serial ) : base( serial )
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