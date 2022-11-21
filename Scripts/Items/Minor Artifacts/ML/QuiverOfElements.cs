using System;
using Server.Items;

namespace Server.Items
{
    public class QuiverOfElements : BaseQuiver, ITokunoDyable
	{
		public override int LabelNumber{ get{ return 1075040; } } // Quiver of the Elements

		[Constructable]
		public QuiverOfElements() : base()
		{
			Hue = 0x104;

            DamageIncrease = 10;
			
			WeightReduction = 50;
		}

        public override void AlterBowDamage(ref int phys, ref int fire, ref int cold, ref int pois, ref int nrgy, ref int chaos, ref int direct)
        {
            phys = cold = fire = pois = nrgy = direct = 0;
            chaos = 100;
        }

		public QuiverOfElements( Serial serial ) : base( serial )
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