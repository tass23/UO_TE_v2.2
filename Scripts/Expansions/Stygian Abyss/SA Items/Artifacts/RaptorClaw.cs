using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class RaptorClaw : Boomerang
	{
	
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override bool CanBeWornByGargoyles{ get{ return true; } }
		public override Race RequiredRace { get { return Race.Gargoyle; } }
		[Constructable]
		public RaptorClaw() : base( 0x8FF )
		{
			Name = ("Raptor Claw");
		
			Hue = 53;
			Slayer = SlayerName.Exorcism;
			Attributes.AttackChance = 12;			
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 45;
			WeaponAttributes.HitLeechStam = 40;
		}

		public RaptorClaw( Serial serial ) : base( serial )
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