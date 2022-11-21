using System;
using Server.Items;

namespace Server.Items
{
    public class Nurture : Pitchfork, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1072080; } } // Scepter of the Chief
		public override int ArtifactRarity{ get{ return 3; } }

		[Constructable]
		public Nurture() : base()
		{
			Name = "The Nurturer's Restrain";
			Hue = 1193;
			EngravedText = "Recovered From Corpser Guardians";
			
			//Slayer = SlayerName.Repond;
			
			Attributes.RegenMana = 5;
			Attributes.DefendChance = 10;
			Attributes.LowerManaCost = 5;
			//Attributes.Luck = 200;
			
			//WeaponAttributes.HitLowerDefend = 50;
			//WeaponAttributes.HitLeechMana = 100;
		}

		public Nurture( Serial serial ) : base( serial )
		{
		}		
		
		#region Mondain's Legacy
		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = 50; fire = 50; cold = 0; nrgy = 0; chaos = 0; direct = 0;
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