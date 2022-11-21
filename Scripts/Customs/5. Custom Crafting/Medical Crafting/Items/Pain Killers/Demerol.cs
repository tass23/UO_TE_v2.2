using System;
using Server;

namespace Server.Items
{
	public class Demerol : BaseMedicalHealPotion
	{
		public override int MinHeal { get { return (Core.AOS ? 20 : 20); } }
		public override int MaxHeal { get { return (Core.AOS ? 35 : 35); } }
		public override double Delay{ get{ return (Core.AOS ? 7.0 : 7.0); } }

		[Constructable]
		public Demerol() : base( PotionEffect.Heal )
		{
			Name = "Demerol";
			Hue = 1301;
		}

		public Demerol( Serial serial ) : base( serial )
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