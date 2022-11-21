using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2B02, 0x2B03 )]
	public class QuiverOfInfinity : BaseQuiver, ITokunoDyable
	{		
		public override int LabelNumber{ get{ return 1075201; } } // Quiver of Infinity
		
		[Constructable]
		public QuiverOfInfinity() : base( 0x2B02 )
        {
            DamageIncrease = 10;
			Attributes.DefendChance = 5;
			
			LowerAmmoCost = 20;
			WeightReduction = 30;
		}

		public QuiverOfInfinity( Serial serial ) : base( serial )
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