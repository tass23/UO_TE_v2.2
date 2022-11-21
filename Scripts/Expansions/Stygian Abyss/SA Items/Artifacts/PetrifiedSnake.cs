using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class PetrifiedSnake : SerpentStoneStaff
	{

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override bool CanBeWornByGargoyles{ get{ return true; } }
		public override Race RequiredRace { get { return Race.Gargoyle; } }

		[Constructable]
		public PetrifiedSnake() : base( )
		{
			Name = ("Petrified Snake");
		
			Hue = 460;
			
			AbsorptionAttributes.EaterPoison = 20;	
			Slayer = SlayerName.ReptilianDeath;
			WeaponAttributes.HitMagicArrow = 30;
			WeaponAttributes.HitLowerDefend = 30;		
			Attributes.WeaponSpeed = 30;
			Attributes.WeaponDamage = 50;	
			AosElementDamages.Poison = 100;		
			WeaponAttributes.ResistPoisonBonus = 10;			
		}

		public PetrifiedSnake( Serial serial ) : base( serial )
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