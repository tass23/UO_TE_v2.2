using System;
using Server.Items;

namespace Server.Items
{
    public class Reclusa : Spear, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 7; } }

		[Constructable]
		public Reclusa() : base()
		{
			Name = "Loxosceles Reclusa";
			Hue = 1153;
			EngravedText = "Recovered From Arachnid Guardians";
			
			Slayer = SlayerName.ArachnidDoom;
			
			Attributes.DefendChance = -5;
			Attributes.AttackChance = 30;
			Attributes.WeaponDamage = 25;
			//Attributes.Luck = 200;
			
			//WeaponAttributes.HitLowerDefend = 50;
			WeaponAttributes.HitPoisonArea = 100;
		}

		public Reclusa( Serial serial ) : base( serial )
		{
		}		
		
		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 0; fire = 0; cold = 0; nrgy = 0; chaos = 0; direct = 0;
			pois = 100;
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