using System;
using Server.Items;

namespace Server.Items
{
    public class ChitanousStaff : QuarterStaff, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 11; } }

		[Constructable]
		public ChitanousStaff() : base()
		{
			Name = "Chitanous Staff";
			Hue = 2419;
			EngravedText = "Recovered From Solen Guardians";
			
			//Slayer = SlayerName.Exorcism;
			
			Attributes.DefendChance = 5;
			Attributes.ReflectPhysical = 100;
			Attributes.WeaponDamage = 50;
			Attributes.Luck = 200;
			
			//WeaponAttributes.HitDispel = 100;
			//WeaponAttributes.HitLeechMana = 100;
		}

		public ChitanousStaff( Serial serial ) : base( serial )
		{
		}		
		
		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 0; fire = 0; cold = 0; nrgy = 0; chaos = 100; direct = 0;
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