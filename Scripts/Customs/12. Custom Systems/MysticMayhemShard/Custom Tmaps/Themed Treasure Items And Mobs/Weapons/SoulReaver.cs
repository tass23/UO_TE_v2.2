using System;
using Server.Items;

namespace Server.Items
{
    public class SoulReaver : Halberd, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 13; } }

		[Constructable]
		public SoulReaver() : base()
		{
			Name = "Soul Reaver";
			Hue = 1714;
			EngravedText = "Recovered From Undead Guardians";
			
			//Slayer = SlayerName.Repond;
			
			Attributes.AttackChance = 15;
			Attributes.WeaponSpeed = 50;
			Attributes.BonusStr = 25;
			//Attributes.Luck = 200;
			
			WeaponAttributes.HitColdArea = 75;
			//WeaponAttributes.HitLightning = 100;
		}

		public SoulReaver( Serial serial ) : base( serial )
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