using System;
using Server;

namespace Server.Items
{
	public class CaptainJackSparrowsCutlass : Cutlass
	{
		public override int ArtifactRarity{ get{ return 100; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public CaptainJackSparrowsCutlass()
		{
			Name = "Captain Jack's Cutlass";
			Hue = 0x5C;
			Attributes.BonusDex = 5;
			Attributes.AttackChance = 5;
			Attributes.WeaponSpeed = 15;
			Attributes.WeaponDamage = 10;
			WeaponAttributes.UseBestSkill = 1;
		}

		public CaptainJackSparrowsCutlass( Serial serial ) : base( serial )
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