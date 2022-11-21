using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2B02, 0x2B03 )]
	public class PestilenceQuiver: BaseQuiver, ITokunoDyable
	{		
		[Constructable]
		public PestilenceQuiver() : base( 0x2B02 )
        {
			Name = "Pestilence";
			Hue = 1151;
            DamageIncrease = 5;
			Attributes.DefendChance = 5;
			Attributes.AttackChance = 5;
			LowerAmmoCost = 5;
			WeightReduction = 50;
		}

		public PestilenceQuiver( Serial serial ) : base( serial )
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