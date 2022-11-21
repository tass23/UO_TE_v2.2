using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class StoneSlithClaw : Cyclone
	{
		public override int MinThrowRange{ get{ return 4; } }		// MaxRange 8

		
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override bool CanBeWornByGargoyles{ get{ return true; } }
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		[Constructable]
		public StoneSlithClaw()
		{
		Name = ("Stone Slith Claw");
		
			Hue = 1150;
			
			WeaponAttributes.HitHarm = 40;
			Slayer = SlayerName.DaemonDismissal;
			WeaponAttributes.HitLowerDefend = 40;	
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 45;
		

		}

		public StoneSlithClaw( Serial serial ) : base( serial )
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