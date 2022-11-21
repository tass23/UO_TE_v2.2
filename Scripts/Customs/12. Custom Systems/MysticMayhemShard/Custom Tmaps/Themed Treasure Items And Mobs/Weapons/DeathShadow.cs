using System;
using Server.Items;

namespace Server.Items
{
    public class DeathShadow : BoneHarvester, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 17; } }

		[Constructable]
		public DeathShadow() : base()
		{
			Name = "Death's Shadow";
			Hue = 1175;
			EngravedText = "Recovered From Undead Guardians";
			
			//Slayer = SlayerName.Repond;
			
			//Attributes.BonusInt = 30;
			Attributes.BonusStr = 30;
			//Attributes.WeaponDamage = 25;
			Attributes.CastRecovery = 3;
			Attributes.CastSpeed = 1;
			//Attributes.WeaponDamage = 25;5;
			//Attributes.LowerManaCost = 10;
			//Attributes.ReflectPhysical = 75;
			
			WeaponAttributes.HitPoisonArea = 50;
			WeaponAttributes.HitLeechHits = 100;
		}

		public DeathShadow( Serial serial ) : base( serial )
		{
		}		
		
		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 50; fire = 0; cold = 0; nrgy = 0; chaos = 0; direct = 0;
			pois = 50;
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