using System;
using Server.Items;

namespace Server.Items
{
    public class JukaCrossbow : Crossbow, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 5; } }

		[Constructable]
		public JukaCrossbow() : base()
		{
			Name = "Crossbow Of The Juka King";
			Hue = 1150;
			EngravedText = "Recovered From Juka Guardians";
			
			Slayer = SlayerName.ReptilianDeath;
			
			Attributes.RegenMana = 5;
			Attributes.AttackChance = 15;
			Attributes.WeaponDamage = 40;
			//Attributes.Luck = 200;
			
			//WeaponAttributes.HitLowerDefend = 50;
			WeaponAttributes.HitMagicArrow = 25;
		}

		public JukaCrossbow( Serial serial ) : base( serial )
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