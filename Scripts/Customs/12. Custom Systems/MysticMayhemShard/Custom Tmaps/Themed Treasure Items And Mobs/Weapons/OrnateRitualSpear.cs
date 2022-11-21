using System;
using Server.Items;

namespace Server.Items
{
    public class OrnateRitualSpear : ShortSpear, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 13; } }

		[Constructable]
		public OrnateRitualSpear() : base()
		{
			Name = "Ornate Ritual Spear";
			Hue = 2213;
			EngravedText = "Recovered From Savage Guardians";
			
			//Slayer = SlayerName.Repond;
			
			Attributes.AttackChance = 15;
			Attributes.WeaponSpeed = -25;
			Attributes.WeaponDamage = 75;
			//Attributes.Luck = 200;
			
			WeaponAttributes.HitLeechHits = 25;
			WeaponAttributes.HitLightning = 100;
		}

		public OrnateRitualSpear( Serial serial ) : base( serial )
		{
		}		
		
		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 100; fire = 0; cold = 0; nrgy = 0; chaos = 0; direct = 0;
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