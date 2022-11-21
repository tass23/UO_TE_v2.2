using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class ClawsOfTheBerserker	: Tekagi
	{

		public override int InitMinHits{ get{ return 35; } }
		public override int InitMaxHits{ get{ return 60; } }

		[Constructable]
		public ClawsOfTheBerserker() : base( 0x27AB )
		{
		Name = ("Claws Of The Berserker");
		
		Hue = 1172;	
		
		WeaponAttributes.HitLightning = 45;	
		WeaponAttributes.HitLowerDefend = 50;
		WeaponAttributes.BattleLust = 1;
		Attributes.CastSpeed = 1;	
		Attributes.WeaponSpeed = 25;
		Attributes.WeaponDamage = 60;
		}

		public ClawsOfTheBerserker( Serial serial ) : base( serial )
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