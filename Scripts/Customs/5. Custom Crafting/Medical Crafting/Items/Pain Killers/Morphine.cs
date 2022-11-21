using System;
using Server;

namespace Server.Items
{
	public class Morphine : BaseMedicalHealPotion
	{
		public override int MinHeal { get { return (Core.AOS ? 100 : 100); } }
		public override int MaxHeal { get { return (Core.AOS ? 130 : 130); } }
		public override double Delay{ get{ return 60.0; } }

		[Constructable]
		public Morphine() : base( PotionEffect.HealGreater )
		{
			Name = "Morphine";
			Hue = 1346;
		}

		public Morphine( Serial serial ) : base( serial )
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