using System;
using Server.Items;

namespace Server.Items
{
    public class BucBow : Bow, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 17; } }

		[Constructable]
		public BucBow() : base()
		{
			Name = "Bow of the Buccaneer";
			Hue = 1106;
			EngravedText = "Recovered From Pirate Guardians";
			
			//Slayer = SlayerName.Repond;
			
			Attributes.AttackChance = 15;
			Attributes.WeaponSpeed = 50;
			Attributes.BonusDex = 25;
			Attributes.Luck = 250;
			
			WeaponAttributes.HitPoisonArea = 25;
			//WeaponAttributes.HitLightning = 100;
		}

		public BucBow( Serial serial ) : base( serial )
		{
		}		
		
		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 80; fire = 0; cold = 0; nrgy = 0; chaos = 0; direct = 0;
			pois = 20;
		}
		#endregion

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