using System;
using Server;

namespace Server.Items
{
	public class VampiricEssence : Cutlass
	{

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public VampiricEssence()
		{
			Name = ("Vampiric Essence");
		
			Hue = 39;
			WeaponAttributes.HitLeechHits = 100;			
			WeaponAttributes.HitHarm = 50;
			Attributes.WeaponSpeed = 20;
			Attributes.WeaponDamage = 50;
			AosElementDamages.Cold = 100;
			WeaponAttributes.BloodDrinker = 1;
		}

		public VampiricEssence( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if( Attributes.AttackChance == 50 )
				Attributes.AttackChance = 10;
		}
	}
}