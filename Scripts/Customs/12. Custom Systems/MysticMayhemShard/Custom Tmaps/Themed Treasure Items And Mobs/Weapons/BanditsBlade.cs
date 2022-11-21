using System;
using Server.Items;

namespace Server.Items
{
    public class BanditsBlade : Katana, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 14; } }

		[Constructable]
		public BanditsBlade() : base()
		{
			Name = "Bandit's Blade";
			Hue = 1194;
			EngravedText = "Recovered From Bandit Guardians";
			
			Slayer = SlayerName.Repond;
			
			Attributes.AttackChance = 5;
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 50;
			//Attributes.Luck = 200;
			
			WeaponAttributes.HitLowerDefend = 50;
			//WeaponAttributes.HitLeechMana = 100;
		}

		public BanditsBlade( Serial serial ) : base( serial )
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