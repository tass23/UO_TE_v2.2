using System;
using Server.Items;

namespace Server.Items
{
    public class ForestGuard : GnarledStaff, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 8; } }

		[Constructable]
		public ForestGuard() : base()
		{
			Name = "Guardian Of The Forest";
			Hue = 1192;
			EngravedText = "Recovered From Reaper Guardians";
			
			//Slayer = SlayerName.ArachnidDoom;
			
			//Attributes.DefendChance = -5;
			Attributes.SpellDamage = 25;
			Attributes.WeaponDamage = -25;
			//Attributes.Luck = 200;
			
			WeaponAttributes.HitMagicArrow = 25;
			WeaponAttributes.HitFireball = 25;
			WeaponAttributes.HitLightning = 25;
		}

		public ForestGuard( Serial serial ) : base( serial )
		{
		}		
		
		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 80; fire = 0; cold = 0; nrgy = 20; chaos = 0; direct = 0;
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