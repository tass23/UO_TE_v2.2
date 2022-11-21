using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class AbyssalBlade : StoneWarSword
	{
		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 100; } }

		public override bool CanBeWornByGargoyles{ get{ return true; } }
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		[Constructable]
		public AbyssalBlade() 
		{
			Name = ("Abyssal Blade");
			
			Hue = 2404;	
			WeaponAttributes.HitManaDrain = 50;
			WeaponAttributes.HitFatigue = 50;
			WeaponAttributes.HitLeechHits = 60;
			WeaponAttributes.HitLeechStam = 60;
			Attributes.WeaponSpeed = 20;
			Attributes.WeaponDamage = 60;	
			AosElementDamages.Chaos = 100;	
		
		}

		public AbyssalBlade( Serial serial ) : base( serial )
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