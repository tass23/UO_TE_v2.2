using System;
using Server.Items;

namespace Server.Items
{
    public class DragClaw : WarFork, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 24; } }

		[Constructable]
		public DragClaw() : base()
		{
			Name = "Claw of the Dragon";
			Hue = 2118;
			EngravedText = "Recovered From Dragon Guardians";
			
			Slayer = SlayerName.DragonSlaying;
			
			//Attributes.AttackChance = 15;
			Attributes.DefendChance = 15;
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 75;
			Attributes.RegenMana = -5;
			
			WeaponAttributes.HitFireball = 75;
			//WeaponAttributes.HitLightning = 100;
		}

		public DragClaw( Serial serial ) : base( serial )
		{
		}		
		
		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 0; fire = 100; cold = 0; nrgy = 0; chaos = 0; direct = 0;
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