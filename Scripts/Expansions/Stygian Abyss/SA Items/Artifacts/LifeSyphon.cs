using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class LifeSyphon : BloodBlade
	{
	
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override bool CanBeWornByGargoyles{ get{ return true; } }
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		[Constructable]
		public LifeSyphon() : base( )
		{
			Name = ("Life Syphon");
		
			Hue = 1172;
			
			WeaponAttributes.BloodDrinker = 1;	
			WeaponAttributes.HitHarm = 30;			
			WeaponAttributes.HitLeechHits = 100;	
			Attributes.BonusHits = 10;
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 50;	
		}

		public LifeSyphon( Serial serial ) : base( serial )
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