using System;
using Server;

namespace Server.Items
{
public class Slither : BaseTalisman, ITokunoDyable
	{
	
		[Constructable]
		public Slither() : base( 0x2F5B )
		{
			Hue = 589;
			
			Name = ("Slither");
				
			Blessed = RandomTalisman.GetRandomBlessed();				
			
			Attributes.BonusHits = 10;
			Attributes.RegenHits = 2;
			Attributes.DefendChance = 10;
			
			}

		public Slither( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); //version
		}
	}
}