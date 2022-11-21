using System;
using Server;

namespace Server.Items
{
	public class WaterSerpentVenom : BasePoisonPotion
	{
		public override Poison Poison{ get{ return Poison.Greater; } }

		public override double MinPoisoningSkill{ get{ return 80.0; } }
		public override double MaxPoisoningSkill{ get{ return 110.0; } }

		[Constructable]
		public WaterSerpentVenom() : base( PotionEffect.PoisonGreater )
		{
			Name = "water serpent venom";
			Hue = 1461;
		}

		public WaterSerpentVenom( Serial serial ) : base( serial )
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