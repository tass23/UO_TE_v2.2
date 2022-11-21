using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class DraconisWrath : Katana
	{

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public DraconisWrath() 
		{
		Name = ("Draconi's Wrath");
		
		Hue = 1177;
		
		AbsorptionAttributes.EaterFire = 20;
		WeaponAttributes.HitFireball = 30;	
		Attributes.AttackChance = 10;
		Attributes.WeaponDamage = 50;
		WeaponAttributes.UseBestSkill = 1;	
		}

		public DraconisWrath( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}