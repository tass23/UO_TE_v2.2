using System;
using Server;

namespace Server.Items
{
	public class TokenOfHolyFavor : GoldBracelet
	{
 		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TokenOfHolyFavor()
		{
		
			Name = ("Token Of Holy Favor");
		
			Hue = 96;
			Attributes.BonusHits = 5;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 2;
			Attributes.DefendChance = 10;
			Attributes.AttackChance = 10;
			Attributes.SpellDamage = 4;
			Resistances.Cold = 5;
			Resistances.Poison = 5;

		}

		public TokenOfHolyFavor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Hue == 0x12B )
				Hue = 0x554;
		}
	}
}