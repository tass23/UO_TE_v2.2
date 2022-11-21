using System;
using Server;

namespace Server.Items
{
	public class HolyWater : BaseSpecialPotion
	{
		public override int MinDamage { get { return Core.AOS ? 20 : 15; } }
		public override int MaxDamage { get { return Core.AOS ? 40 : 30; } }

		[Constructable]
		public HolyWater() : base( PotionEffect.ExplosionGreater )
		{
			Weight = 1.0;
			Hue = 1160;
			Name = "Holy Water Bomb";
		}

		public HolyWater( Serial serial ) : base( serial )
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