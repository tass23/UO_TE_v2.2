using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0xF47, 0xF48 )]
	public class AxeOfAbandon : BattleAxe
	{


		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 70; } }

		[Constructable]
		public AxeOfAbandon() 
		{
			Name = ("Axe Of Abandon");
			
			Hue = 556;
			WeaponAttributes.HitLowerDefend = 40;
			WeaponAttributes.BattleLust = 1;		
			Attributes.AttackChance = 15;
			Attributes.DefendChance = 10;	
			Attributes.CastSpeed = 1;	
			Attributes.WeaponSpeed = 30;
			Attributes.WeaponDamage = 50;		
		}

		public AxeOfAbandon( Serial serial ) : base( serial )
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