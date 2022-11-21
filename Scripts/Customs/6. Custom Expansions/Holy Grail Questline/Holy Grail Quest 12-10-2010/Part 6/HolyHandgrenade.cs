using System;
using Server;

namespace Server.Items
{
	public class HolyHandgrenade : BaseConflagrationPotion
	{
		public override int MinDamage{ get{ return 4; } }
		public override int MaxDamage{ get{ return 8; } }

		[Constructable]
		public HolyHandgrenade() : base( PotionEffect.ConflagrationGreater )
		{
			ItemID = 0x2F5E;
			Hue = 1154;
			Stackable = false;
		    Weight = 1.0;
			Name = "The Holy Hand Grenade on Antioch";
		}

		public HolyHandgrenade( Serial serial ) : base( serial )
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