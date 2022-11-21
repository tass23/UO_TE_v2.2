using System;
using Server;

namespace Server.Items
{
	public class Oxycontin : BaseMedicalHealPotion
	{
		public override int MinHeal { get { return (Core.AOS ? 45 : 45); } }
		public override int MaxHeal { get { return (Core.AOS ? 60 : 60); } }
		public override double Delay{ get{ return 30.0; } }

		[Constructable]
		public Oxycontin() : base( PotionEffect.HealGreater )
		{
			Name = "Oxycontin";
			Hue = 1714;
		}

		public Oxycontin( Serial serial ) : base( serial )
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