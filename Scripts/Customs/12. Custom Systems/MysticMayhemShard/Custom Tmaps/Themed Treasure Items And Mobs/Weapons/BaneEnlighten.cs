using System;
using Server.Items;

namespace Server.Items
{
    public class BaneEnlighten : Scythe, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 27; } }

		[Constructable]
		public BaneEnlighten() : base()
		{
			Name = "Bane Of Enlightenment";
			Hue = 2118;
			EngravedText = "Recovered From Demon Guardians";
			
			Slayer = SlayerName.Exorcism;
			
			//Attributes.AttackChance = 15;
			//Attributes.DefendChance = 15;
			Attributes.WeaponSpeed = 50;
			Attributes.WeaponDamage = 50;
			Attributes.RegenHits = -5;
			WeaponAttributes.HitFireball = 25;
			WeaponAttributes.HitLeechHits = 25;
			WeaponAttributes.HitLeechMana = 25;
			WeaponAttributes.HitLeechStam = 25;
			//WeaponAttributes.HitLightning = 100;
		}

		public BaneEnlighten( Serial serial ) : base( serial )
		{
		}		
		
		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 20; fire = 20; cold = 20; nrgy = 20; chaos = 0; direct = 0;
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