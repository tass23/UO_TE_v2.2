using System;
using Server.Items;

namespace Server.Items
{
    public class QuiverOfRage : BaseQuiver, ITokunoDyable
	{
		public override int LabelNumber{ get{ return 1075038; } } // Quiver of Rage

		[Constructable]
		public QuiverOfRage() : base()
		{
			Hue = 0xEB;
			
			WeightReduction = 25;
			DamageIncrease = 10;
		}

        public override void AlterBowDamage(ref int phys, ref int fire, ref int cold, ref int pois, ref int nrgy, ref int chaos, ref int direct)
        {
            cold = pois = phys = fire = nrgy = 20;
            chaos = direct = 0;
        }

		public QuiverOfRage( Serial serial ) : base( serial )
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