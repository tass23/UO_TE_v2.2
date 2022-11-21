using System;
using Server;

namespace Server.Items
{
	public class Mangler : Broadsword
	{
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
		
		[Constructable]
		public Mangler() : base( )
		{
			Hue = 2001;
			
			Name = ("Mangler");
		
			WeaponAttributes.HitLeechMana = 50;
			Attributes.WeaponDamage = 50;
			Attributes.WeaponSpeed = 25;
			WeaponAttributes.HitHarm = 50;
			WeaponAttributes.UseBestSkill = 1;			
			WeaponAttributes.HitLowerDefend = 30;		

		}

		public Mangler( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); //version
		}
	}
}