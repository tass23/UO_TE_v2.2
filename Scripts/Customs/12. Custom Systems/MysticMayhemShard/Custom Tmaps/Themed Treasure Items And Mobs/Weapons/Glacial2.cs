using System;
using Server.Items;

namespace Server.Items
{
    public class Glacial2 : BlackStaff, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 14; } }

		[Constructable]
		public Glacial2() : base()
		{
			Name = "An Imbued Glacial Staff";
			Hue = 1152;
			EngravedText = "Recovered From Ice Guardians";
			
			//Slayer = SlayerName.Repond;
			
			//Attributes.BonusInt = 30;
			Attributes.BonusInt = 30;
			//Attributes.WeaponDamage = 25;
			Attributes.CastRecovery = 3;
			Attributes.CastSpeed = 2;
			//Attributes.MageWeapon = 10;
			//Attributes.WeaponDamage = 25;5;
			//Attributes.LowerManaCost = 10;
			//Attributes.ReflectPhysical = 75;
			
			WeaponAttributes.HitHarm = 50;
			WeaponAttributes.MageWeapon = 5;
		}

		public Glacial2( Serial serial ) : base( serial )
		{
		}		
		
		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 0; fire = 0; cold = 100; nrgy = 0; chaos = 0; direct = 0;
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