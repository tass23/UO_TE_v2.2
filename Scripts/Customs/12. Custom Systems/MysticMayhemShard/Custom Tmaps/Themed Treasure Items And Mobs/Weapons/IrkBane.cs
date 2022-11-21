using System;
using Server.Items;

namespace Server.Items
{
    public class IrkBane : Hatchet, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 12; } }

		[Constructable]
		public IrkBane() : base()
		{
			Name = "Irk's Bane";
			Hue = 500;
			EngravedText = "Recovered From Fey Guardians";
			
			Slayer = SlayerName.Fey;
			
			Attributes.AttackChance = 30;
			Attributes.DefendChance = -15;
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 25;
			//Attributes.Luck = 200;
			
			WeaponAttributes.HitLowerDefend = 50;
			//WeaponAttributes.HitLeechMana = 100;
		}

		public IrkBane( Serial serial ) : base( serial )
		{
		}		
		
		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 0; fire = 0; cold = 0; nrgy = 100; chaos = 0; direct = 0;
			pois = 0;
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